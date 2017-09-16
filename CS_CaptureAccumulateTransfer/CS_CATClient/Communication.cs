using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Reflection;
using System.Threading;
using CS_CATClient;

namespace CS_CATClient
{
    // Read data from the DB and send it to the host via socket. Read the response from the host
    class Communication
    {
        private bool stopThread = false;
        DBComponent dbComponent = null;
        DeviceSocket devSocket = null;
        string rcvdStr = null;

        // notification event
        public delegate void NotifyEventHandler(NotifyEvents nEvent, object data);
        public event NotifyEventHandler Notify;

        // Used to synchronize the shutdown process, terminate
        // any pending async calls before Disconnect returns
        ManualResetEvent asyncEvent = new ManualResetEvent(true);

        public void Open(string hostIp, int port)
        {
            stopThread = false;

            try
            {

                devSocket = new DeviceSocket();
                devSocket.Notify += new DeviceSocket.NotifyEventHandler(OnSocket);

                devSocket.Connect(hostIp, port);

                // Gather records from DB
                dbComponent = new DBComponent();
                dbComponent.DBDelete = true;
                dbComponent.DBEncrypt = true;

                String curFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
                dbComponent.DBName = curFolder + "\\CAT_DB.sdf";
                dbComponent.DBPassword = "d@$123";
                dbComponent.DBOpen();

                // Run this module on a new thread
                new Thread(new ThreadStart(CommunicationThreadProc)).Start();
            }
            catch(Exception ex)
            {
                this.Close();
                NotifyCaller(NotifyEvents.ConnectError, ex.Message);
            }

        }

        public void Close()
        {
            if (devSocket == null || dbComponent == null) return;

            try
            {
                asyncEvent.Reset();

                stopThread = true;

                asyncEvent.WaitOne();

                if (devSocket != null) devSocket.Disconnect();
                if (dbComponent != null) dbComponent.DBClose();

                devSocket = null;
                dbComponent = null;

                NotifyCaller(NotifyEvents.Disconnected, null);
            }
            catch (Exception ex)
            {
                NotifyCaller(NotifyEvents.DisconnectError, ex.Message);
            }
        }


        private void CommunicationThreadProc()
        {
            while (!stopThread)
            {
                try
                {
                    if (devSocket.IsConnected)
                    {
                        dbComponent.DBQuery("SELECT * FROM DataQueue WHERE SentFlag = 0 ORDER BY UniqueId");

                        foreach (DataRow row in dbComponent.myDataSet.Tables[0].Rows)
                        {
                            int uniqueId = Convert.ToInt32(row["UniqueId"].ToString());
                            string dataSent = row["DataSent"].ToString();

                            devSocket.Send(uniqueId.ToString() + "," + dataSent);
                            Thread.Sleep(300);
                        }
                    }
                }
                catch
                {
                    break;
                }

                Thread.Sleep(500);
            }

            asyncEvent.Set();
        }

        // Catch socket notification events
        private void OnSocket(NotifyEvents nEvent, object data)
        {
            try
            {
                lock (this)
                {
                    switch (nEvent)
                    {
                        case NotifyEvents.Connected:
                        case NotifyEvents.Disconnected:
                            NotifyCaller(nEvent, null);
                            break;

                        case NotifyEvents.DataSent:
                            NotifyCaller(nEvent, null);
                            devSocket.Receive();
                            break;

                        case NotifyEvents.DataReceived:
                            rcvdStr = devSocket.receiveBuf;
                            if (rcvdStr.Length > 0)
                                ProcessReceiveNotifications();
                            NotifyCaller(nEvent, data);
                            break;

                        case NotifyEvents.ConnectError:
                        case NotifyEvents.SendError:
                        case NotifyEvents.ReceiveError:
                        case NotifyEvents.DisconnectError:
                        case NotifyEvents.OtherError:
                            this.Close();
                            NotifyCaller(nEvent, data);
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                NotifyCaller(NotifyEvents.OtherError, ex.Message);
            }
        }

        // Process socket notification
        private void ProcessReceiveNotifications()
        {
            int idx = rcvdStr.IndexOf(',');
            if (idx != -1) 
            {
                string cmd = "UPDATE DataQueue SET SentFlag=1, DataRcvd='" + rcvdStr.Substring(idx + 1) + "' WHERE UniqueId=" + rcvdStr.Substring(0, idx);

                dbComponent.DBExecute(cmd);            
            }

        }

        // Notify the app
        private void NotifyCaller(NotifyEvents nEvent, object data)
        {
            if (this.Notify != null)
            {
                Notify(nEvent, data);
            }
        }
    }
}
