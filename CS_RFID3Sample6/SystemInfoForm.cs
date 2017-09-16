using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID3;

namespace CS_RFID3Sample6
{
    public partial class SystemInfoForm : Form
    {
        private AppForm m_AppForm;

        public SystemInfoForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();
        }

        private void SystemInfoForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (m_AppForm.m_ReaderMgmt.IsLoggedIn)
                {
                    ListView infoView = this.systemInfoView;
                    SystemInfo info = m_AppForm.m_ReaderMgmt.GetSystemInfo();
                    ListViewItem item;

                    infoView.BeginUpdate();
                    infoView.Items.Clear();
                    item = new ListViewItem("ReaderName");
                    item.SubItems.Add(info.ReaderName);
                    infoView.Items.Add(item);
                    item = new ListViewItem("RadioFirmwareVersion");
                    item.SubItems.Add(info.RadioFirmwareVersion);
                    infoView.Items.Add(item);
                    item = new ListViewItem("FPGAVersion");
                    item.SubItems.Add(info.FPGAVersion);
                    infoView.Items.Add(item);
                    item = new ListViewItem("RAMAvailable");
                    item.SubItems.Add(info.RAMAvailable.ToString() + " bytes");
                    infoView.Items.Add(item);
                    item = new ListViewItem("FlashAvailable");
                    item.SubItems.Add(info.FlashAvailable.ToString() + " bytes");
                    infoView.Items.Add(item);
                    item = new ListViewItem("UpTime");
                    item.SubItems.Add(info.UpTime);
                    infoView.Items.Add(item);
                    item = new ListViewItem("ReaderLocation");
                    item.SubItems.Add(info.ReaderLocation);
                    infoView.Items.Add(item);
                    item = new ListViewItem("CPUUsageForSystemProcesses");
                    item.SubItems.Add(info.CPUUsageForSystemProcesses.ToString());
                    infoView.Items.Add(item);
                    item = new ListViewItem("CPUUsageForUserProcesses");
                    item.SubItems.Add(info.CPUUsageForUserProcesses.ToString());
                    infoView.Items.Add(item);
                    infoView.EndUpdate();
                }
                else
                {
                    this.m_AppForm.notifyUser("Please login to a reader via ReaderMgmt", "System Info");
                }
            }
            catch (OperationFailureException ex)
            {
                this.m_AppForm.notifyUser(ex.VendorMessage, "System Info");
                this.Close();
            }
        }
    }
}