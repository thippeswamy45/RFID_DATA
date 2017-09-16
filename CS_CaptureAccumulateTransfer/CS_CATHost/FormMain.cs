using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace CS_CATHost
{

    
    public partial class FormMain : Form
    {
        private NotifyEvents notifyEvent;
        Object notifyRcvdData = null;
        Object notifySentData = null;
        Object notifyIp = null;
        public ThreadedTcpSrvr hs = null;

        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //This sample works with the IPv4 address.
            //So have to exclude any IPv6 addresses.
            for (int i = 0; i < Dns.GetHostEntry("").AddressList.Length; i++)
            {
                if (Dns.GetHostEntry("").AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    StatusBarTop1.Text = "Host IP: " + Dns.GetHostEntry("").AddressList[i];
                    break;
                }
            }

            StatusBarTop2.Text = "Host Port: 10200";

            hs = new ThreadedTcpSrvr();
            hs.Notify += new ThreadedTcpSrvr.NotifyEventHandler(OnSocket);

            hs.Start();
        }

        // Catch socket notification events
        private void OnSocket(NotifyEvents nEvent, object ip, object rcvdData, object sentData)
        {
            try
            {
                lock (this)
                {
                    // save arguments to class fields				
                    notifyEvent = nEvent;
                    notifyRcvdData = rcvdData;
                    notifySentData = sentData;
                    notifyIp = ip;

                    Invoke(new EventHandler(ProcessNotifications));
                }
            }
            catch
            {
            }
        }

        // Process socket notifications
        private void ProcessNotifications(object sender, EventArgs args)
        {
            switch (notifyEvent)
            {
                case NotifyEvents.Waiting:
                    statusBar2.Text = "Waiting... ";
                    break;

                case NotifyEvents.Connected:
                    DisplayData(notifyIp.ToString(), "Connected...", "...");
                    statusBar2.Text = "Connected to client: " + notifyIp.ToString();
                    break;

                case NotifyEvents.DataSent:
                    DisplayData(notifyIp.ToString(), notifyRcvdData.ToString(), notifySentData.ToString());
                    statusBar2.Text = "Data sent to client: " + notifySentData.ToString();
                    break;

                case NotifyEvents.DataReceived:
                    DisplayData(notifyIp.ToString(), notifyRcvdData.ToString(), notifySentData.ToString());
                    statusBar2.Text = "Data from client: " + notifyRcvdData.ToString();
                    break;

                case NotifyEvents.Disconnected:
                    DisplayData(notifyIp.ToString(), "Disconnected...", "...");
                    statusBar2.Text = "Disconnected from client: " + notifyIp.ToString();
                    break;

                case NotifyEvents.ConnectError:
                case NotifyEvents.SendError:
                case NotifyEvents.ReceiveError:
                case NotifyEvents.OtherError:
                    statusBar2.Text = "Socket error\r\n" + notifyRcvdData.ToString() + ";" + notifySentData.ToString();
                    break;
            }

            Application.DoEvents();

        }


        private void DisplayData(string ip, string rcvd, string sent)
        {
            ListViewItem listItem1 = null;

            listItem1 = listView1.Items.Add(ip);
            listItem1.SubItems.Add(sent);
            listItem1.SubItems.Add(rcvd);

            listItem1.Selected = true;
            listItem1.EnsureVisible();
       }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            hs.Stop();
        }

    }
}