using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CS_RFID3Sample6
{
    public partial class ConnectionForm : Form
    {
        private AppForm m_AppForm;
        private bool m_IsLoaded;
        private string ipAddress;
        private string port;

        public ConnectionForm(AppForm appForm) 
        {                     
            InitializeComponent();
            m_AppForm = appForm;

            ipAddress = "127.0.0.1";
            port = "5084";
        }

        private void ConnectionForm_Load(object sender, EventArgs e)
        {
            if (!m_IsLoaded)
            {
                IP_TB.Text = ipAddress;
                Port_TB.Text = port;
                m_IsLoaded = true;
            }
        }

        public string IpText
        {
            get
            {
                return ipAddress;
            }
        }

        public string PortText
        {
            get
            {
                return port;
            }
        }

        private void connectionButton_Click(object sender, EventArgs e)
        {
            ipAddress = IP_TB.Text;
            port = Port_TB.Text;
            try
            {
                m_AppForm.Connect(connectionButton.Text);
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Connect");
            }
        }
    }
}