using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Windows.Forms;

namespace CS_RFID2_Host_Sample  
{
    public partial class frmReaderInfo : Form
    {
        ClsReader m_Reader = null;
        
        public frmReaderInfo(ClsReader m_ClsReader)
        {
            InitializeComponent();
            m_Reader = m_ClsReader;
        }
     
        private void frmReaderInfo_Load(object sender, EventArgs e)
        {
            try
            {
                string hostName = Dns.GetHostName();
                IPHostEntry host = Dns.GetHostEntry(hostName);
               if(m_Reader.reader_Name.Trim()!="")
                    lblReadername.Text = m_Reader.reader_Name;
                if (m_Reader.model.ToString().Trim() != "")
                    lblModel.Text = m_Reader.model.ToString();
                if (m_Reader.reader_Desc.Trim() != "")
                    lblDesc.Text = m_Reader.reader_Desc;
                if (m_Reader.ipAddress.Trim() != "")
                    lblIpAddress.Text = m_Reader.ipAddress;
                if (m_Reader.tcpPort.Trim() != "")
                    lblbTcpPort.Text = m_Reader.tcpPort;
                if (m_Reader.httpPort.Trim() != "")
                    lblbHttpPort.Text = m_Reader.httpPort;
                if (m_Reader.model != Symbol.RFID2.ReaderModel.RD5000  &&
                    m_Reader.notificationAddress.Trim() != "")
                    lblNotifiAddr.Text = m_Reader.notificationAddress;
                if (m_Reader.model != Symbol.RFID2.ReaderModel.RD5000 
                        && m_Reader.notificationPort.Trim() != "" )
                    lblNotifiPort.Text = m_Reader.notificationPort;
                if (!m_Reader.GetReaderInfo().Equals(null))
                {
                    if (m_Reader.deviceSerialNo.Trim() != "")
                        lblSerialno.Text = m_Reader.deviceSerialNo;
                    if (m_Reader.firmwareVersion.Trim() != "")
                        lblFwVersion.Text = m_Reader.firmwareVersion;
                }
                if (m_Reader.GetSDKVersionNumber().Trim() != "")
                    lblSdkVersion.Text = m_Reader.GetSDKVersionNumber();

                btnOk.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to Display Reader Info", "Error loading ReaderInfo", MessageBoxButtons.OKCancel);
                this.Close();
                throw ex;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            
            this.Close();
            
        }

       
    }
}