using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID3;
using System.Threading;

namespace CS_RFID3Sample6
{
    public partial class FirmwareUpdateForm : Form
    {
        private AppForm m_AppForm;


        public FirmwareUpdateForm(AppForm appForm)
        {
            InitializeComponent();
            m_AppForm = appForm;
            firmwareApplyButton.Enabled = false;
        }

        private void FirmwareUpdateForm_Load(object sender, EventArgs e)
        {
            Reset();

        }

        internal void Reset()
        {
            update_CB.Items.Clear();
            location_TB.Enabled = true;
            if (m_AppForm.m_ReaderType == READER_TYPE.MC)
            {
                update_CB.Items.Clear();

                update_CB.Items.Add("Radio Firmware Update");
                update_CB.Items.Add("Radio Config Update");
                update_CB.SelectedIndex = 0;
                ftp_GB.Enabled = false;
                username_TB.Enabled = false;
                password_TB.Enabled = false;
                location_TB.Text = "";
                updateDesc_TB.Text = "";
            }
            else
            {
                update_CB.Visible = false;
                ftp_GB.Enabled = true;
                username_TB.Enabled = true;
                password_TB.Enabled = true;
            }
        }
        private void firmwareApplyButton_Click(object sender, EventArgs e)
        {
            try
            {
                updateWorker();
            }
            catch (OperationFailureException ex)
            {
                this.m_AppForm.notifyUser(ex.VendorMessage, "Software Update");
            }
        }


        private void updateWorker()
        {
            Symbol.RFID3.UpdateStatus updateStatus;
            firmwareApplyButton.Enabled = false;
            location_TB.Enabled = false;
            username_TB.Enabled = false;
            password_TB.Enabled = false;

            try
            {
            if (m_AppForm.m_ReaderType == READER_TYPE.MC)
            {
                if (update_CB.SelectedIndex == 0)
                {
                    // Do Radio Firmware Update
                    m_AppForm.m_ReaderMgmt.RadioFirmwareUpdate.Update(location_TB.Text);
                }
                else
                {
                    // Do Radio Config Update
                    m_AppForm.m_ReaderMgmt.RadioConfigUpdate.Update(location_TB.Text);
                }
            }
            else
            {
                SoftwareUpdateInfo info = new SoftwareUpdateInfo(
                    location_TB.Text, username_TB.Text, password_TB.Text);
                m_AppForm.m_ReaderMgmt.SoftwareUpdate.Update(info);
            }

            uint updatePercent = 0;
            while (updatePercent < 100)
                {
                    if (this.m_AppForm.m_ReaderType == READER_TYPE.MC)
                    {
                        if (this.update_CB.SelectedIndex == 1)
                            updateStatus = m_AppForm.m_ReaderMgmt.RadioFirmwareUpdate.UpdateStatus;
                        else
                            updateStatus = m_AppForm.m_ReaderMgmt.RadioConfigUpdate.UpdateStatus;
                    }
                    else
                    {
                        updateStatus = m_AppForm.m_ReaderMgmt.SoftwareUpdate.UpdateStatus;
                    }
                    updatePercent = updateStatus.Percentage;
                    update_PB.Value = (int)updatePercent;
                    updateDesc_TB.Text = updateStatus.UpdateInfo;
                    Thread.Sleep(1000);
                }
                this.m_AppForm.m_ReaderMgmt.Logout();
                this.Close();
            }
                catch (InvalidOperationException ioe)
                {
                m_AppForm.notifyUser(ioe.ToString(), this.update_CB.Text);

                }
                catch (InvalidUsageException iue)
                {
                m_AppForm.notifyUser(iue.Info, this.update_CB.Text);

                }
                catch (OperationFailureException ofe)
                {
                m_AppForm.notifyUser(ofe.StatusDescription + ofe.VendorMessage, this.update_CB.Text);

                }
                catch (Exception ex)
                {
                    m_AppForm.notifyUser(ex.Message.ToString(), this.update_CB.Text);

            }
            firmwareApplyButton.Enabled = true;
            location_TB.Enabled = true;
            username_TB.Enabled = true;
            password_TB.Enabled = true;
        }

        private void browseFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (update_CB.Text == "Radio Firmware Update")
                fileDialog.Filter = "Firmware Files|*.a79";
            else if (update_CB.Text == "Radio Config Update")
                fileDialog.Filter = "OEM Config Files|*.dmp";

            fileDialog.InitialDirectory = @"\";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                location_TB.Text = fileDialog.FileName;
                firmwareApplyButton.Enabled = true;
            }
            else
            {
                location_TB.Text = "";
                if (String.IsNullOrEmpty(location_TB.Text))
                    firmwareApplyButton.Enabled = false;
            }           
        }

        private void update_CB_SelectedValueChanged(object sender, EventArgs e)
        {
            if (update_CB.SelectedIndex != -1)
            {
                location_TB.Text = "";
            }
        }
    }
}