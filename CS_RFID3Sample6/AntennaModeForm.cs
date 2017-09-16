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
    public partial class AntennaModeForm : Form
    {
        private AppForm m_AppForm;
        private bool m_IsLoaded;

        public AntennaModeForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();
        }

        private void AntennaModeForm_Load(object sender, EventArgs e)
        {
                try
                {
                    antennaMode_CB.SelectedIndex = (int)m_AppForm.m_ReaderMgmt.AntennaMode;
                }
                catch (OperationFailureException ex)
                {
                    this.m_AppForm.notifyUser(ex.VendorMessage, "Antenna Mode");
                    this.Close();
                }
                m_IsLoaded = true;
        }

        private void AntennaModeForm_Closing(object sender, EventArgs e)
        {
        }

        private void antennaModeButton_Click(object sender, EventArgs e)
        {

            if (m_IsLoaded)
            {
                try
                {
                    m_AppForm.m_ReaderMgmt.AntennaMode = (ANTENNA_MODE)antennaMode_CB.SelectedIndex;
                    this.Close();
                }
                catch (OperationFailureException ex)
                {
                    this.m_AppForm.notifyUser(ex.VendorMessage, "Antenna Mode");
                }
            }
        }
    }
}