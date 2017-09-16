using System;
using System.Collections;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace CS_CATHost
{

    // Socket notification events.
    public enum NotifyEvents
    {
        Waiting,
        Connected,
        DataSent,
        DataReceived,
        Disconnected,
        ConnectError,
        SendError,
        ReceiveError,
        OtherError
    }
    
    public class ThreadedTcpSrvr
    {
        private const int portNum = 10200;
        internal bool stopListenerThread = false;

        // notification event
        public delegate void NotifyEventHandler(NotifyEvents nEvent, object ip, object rcvdData, object sentdata);
        public event NotifyEventHandler Notify;

        ConnectionThread newconnection = null;
        
        public void Start()
        {
            new Thread(new ThreadStart(StartThread)).Start();
        }

        public void Stop()
        {
            lock ((object)stopListenerThread)
            {
                stopListenerThread = true;
            }

            try
            {
                lock ((object)newconnection.stopConnectionThread)
                {
                    newconnection.stopConnectionThread = true;
                }
            }
            catch { }
        }

        private void StartThread()
        {
            try
            {
                IPAddress ipAddress = null;

                //This sample works with the IPv4 address.
                //So have to exclude any IPv6 addresses.
                for (int i = 0; i < Dns.GetHostEntry("").AddressList.Length; i++)
                {
                    if (Dns.GetHostEntry("").AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ipAddress = Dns.GetHostEntry("").AddressList[i];
                        break;
                    }
                }

                TcpListener listener = new TcpListener(ipAddress, portNum);

                listener.Start();
                NotifyCaller(NotifyEvents.Waiting, null, null, null);

                while (!stopListenerThread)
                {
                    if (listener.Pending())
                    {
                        newconnection = new ConnectionThread();
                        newconnection.Notify +=new ConnectionThread.NotifyEventHandler(NotifyCaller);

                        newconnection.threadListener = listener;
                        Thread newthread = new Thread(new ThreadStart(newconnection.HandleConnection));
                        newthread.Start();
                    }

                    Thread.Sleep(300);
                }

                listener.Stop();
            }
            catch (Exception ex)
            {
                String msg = ex.Message;
            }
        }

        // Notify the app
        private void NotifyCaller(NotifyEvents nEvent, object ip, object rcvdData, object sentData)
        {
            if ((this.Notify != null)) Notify(nEvent, ip, rcvdData, sentData);
        }
    }


    // Reads data from the client socket
    class ConnectionThread : ThreadedTcpSrvr
    {
        public TcpListener threadListener;
        // notification event
        public new delegate void NotifyEventHandler(NotifyEvents nEvent, object ip, object rcvdData, object sentData);
        public new event NotifyEventHandler Notify;
        public bool stopConnectionThread = false;


        public void HandleConnection()
        {
            int recv;
            byte[] bRcvdData = new byte[1024];
            string sRcvdData, sSentData;
            string[] sParsedData = null;
            string ip, delimiterStr = ",", sDateTime;

            TcpClient client = threadListener.AcceptTcpClient();
            NetworkStream ns = client.GetStream();

            // ip = ((IPEndPoint)client.Client.LocalEndPoint).Address.ToString();
            ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            NotifyCaller(NotifyEvents.Connected, ip, "", "");

            bRcvdData = new byte[1024];


            while (true)
            {
                if (stopConnectionThread == true) break;

                ns.ReadTimeout = 1000;

                try
                {
                    recv = ns.Read(bRcvdData, 0, bRcvdData.Length);
                    if (recv == 0) break;

                sRcvdData = Encoding.ASCII.GetString(bRcvdData, 0, recv);

                // Parse the data to get the id and data
                sParsedData = sRcvdData.Split(delimiterStr.ToCharArray(), 2);

                if (sParsedData.Length == 2)
                {
                    sDateTime = DateTime.Now.ToString();
                    sSentData = sParsedData[0] + delimiterStr + sDateTime;

                    ns.Write(Encoding.ASCII.GetBytes(sSentData), 0, sSentData.Length);

                    NotifyCaller(NotifyEvents.DataSent, ip, sParsedData[1], sDateTime);
                }

                }
                catch 
                {
                }

            Thread.Sleep(400);
            }

            ns.Close();
            client.Close();
            
            NotifyCaller(NotifyEvents.Disconnected, ip, null, null);
        }

        // Notify the app
        private void NotifyCaller(NotifyEvents nEvent, object ip, object rcvdData, object sentData)
        {
            if ((this.Notify != null)) Notify(nEvent, ip, rcvdData, sentData);
        }
    }
}
