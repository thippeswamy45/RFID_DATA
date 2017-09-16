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
    public partial class TagStorageForm : Form
    {
        private AppForm m_AppForm;
        private bool m_IsLoaded;
        private TagStorageSettings m_Settings;

        public TagStorageForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();
            m_Settings = m_AppForm.m_ReaderAPI.Config.GetTagStorageSettings();
        }

        internal void Reset()
        {
            m_Settings = m_AppForm.m_ReaderAPI.Config.GetTagStorageSettings();
        }

        private void TagStorageForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (m_AppForm.m_ReaderAPI.IsConnected)
                {
                    maxCount_TB.Text = m_Settings.MaxTagCount.ToString();
                    idLength_TB.Text = m_Settings.MaxTagIDLength.ToString();
                    memoryBankSize_TB.Text = m_Settings.MaxSizeMemoryBank.ToString();
                    m_IsLoaded = true;
                }
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Tag Storage");
            }
        }

        private void TagStorageForm_Closing(object sender, EventArgs e)
        {

        }

        private void tagStorageSettingButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_AppForm.m_ReaderAPI.IsConnected)
                {
                    m_Settings.MaxTagCount = uint.Parse(maxCount_TB.Text);
                    m_Settings.MaxTagIDLength = uint.Parse(idLength_TB.Text);
                    m_Settings.MaxSizeMemoryBank = uint.Parse(memoryBankSize_TB.Text);
                    m_AppForm.m_ReaderAPI.Config.SetTagStorageSettings(m_Settings);
                }
                this.Close();
            }
            catch (InvalidUsageException iue)
            {
                this.m_AppForm.notifyUser(iue.Info, "Tag Storage");
            }
            catch (OperationFailureException ofe)
            {
                this.m_AppForm.notifyUser(ofe.VendorMessage, "Tag Storage");
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Tag Storage");
            }
        }
    }
}