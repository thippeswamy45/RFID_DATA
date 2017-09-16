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
    public partial class RadioPowerForm : Form
    {
        private AppForm m_AppForm;
        internal bool m_IsLoaded;

        public RadioPowerForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();
        }

        private void RadioPowerForm_Load(object sender, EventArgs e)
        {
            if (!m_IsLoaded)
            {
                try
                {
                    if (m_AppForm.m_IsConnected)
                    {
                        if (m_AppForm.m_ReaderAPI.ReaderCapabilities.IsRadioPowerControlSupported == true)
                        {
                            radioState_CB.SelectedIndex = (int)m_AppForm.m_ReaderAPI.Config.RadioPowerState;
                        }
                        else
                        {
                            m_AppForm.functionCallStatusLabel.Text = "Radio Power Control Not Supported";
                            this.Close();
                        }
                    }
                    else
                    {
                        m_AppForm.functionCallStatusLabel.Text = "Please connect to a reader";
                        this.Close();
                    }
                }
                catch (OperationFailureException ex)
                {
                    m_AppForm.functionCallStatusLabel.Text = ex.Result.ToString();
                    this.Close();
                }
                m_IsLoaded = true;
            }
        }

        private void RadioPowerForm_Closing(object sender, EventArgs e)
        {
            try
            {
                if (m_AppForm.m_IsConnected)
                {
                    if (m_AppForm.m_ReaderAPI.ReaderCapabilities.IsRadioPowerControlSupported == true)
                    {
                        m_AppForm.m_ReaderAPI.Config.RadioPowerState = (RADIO_POWER_STATE)radioState_CB.SelectedIndex;
                        m_AppForm.functionCallStatusLabel.Text = "Set Radio Power Successfully";
                    }
                    else
                    {
                        m_AppForm.functionCallStatusLabel.Text = "Please connect to a reader";
                    }
                }
                else
                {
                    m_AppForm.functionCallStatusLabel.Text = "Please connect to a reader";
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                m_AppForm.functionCallStatusLabel.Text = ex.Message;
            }
        }
    }
}