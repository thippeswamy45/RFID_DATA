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
    public partial class AntennaConfigForm : Form
    {
        private AppForm m_AppForm;
        private bool m_IsChanged;
        internal bool m_IsLoaded;

        public AntennaConfigForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();
        }

        internal void updateConfig(int antennaID)
        {
            if (m_IsLoaded)
            {
                
            }
        }

        private void AntennaConfigForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (m_AppForm.m_ReaderAPI.IsConnected)
                {                    
                    ushort[] antID = m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas;
                    int[] rxValues = m_AppForm.m_ReaderAPI.ReaderCapabilities.ReceiveSensitivityValues;
                    int[] txValues = m_AppForm.m_ReaderAPI.ReaderCapabilities.TransmitPowerLevelValues;

                    if (antID.Length > 0)
                    {                        
                        receiveSensitivity_CB.Items.Clear();
                        foreach (int rx in rxValues)
                            receiveSensitivity_CB.Items.Add(rx);

                        transmitPower_CB.Items.Clear();
                        foreach (int tx in txValues)
                            transmitPower_CB.Items.Add(tx);
                       
                        if (m_AppForm.m_ReaderAPI.ReaderCapabilities.IsHoppingEnabled)
                        {
                            this.hopTableIndexLabel.Visible = true;
                            this.hopTableIndex_CB.Visible = true;
                            this.hopFrequencies_TB.Visible = true;
                            this.txFreqLabel.Visible = false;
                            this.txFreq_CB.Visible = false;
                            hopTableIndex_CB.Items.Clear();
                            FrequencyHopInfo hopInfo = m_AppForm.m_ReaderAPI.ReaderCapabilities.FrequencyHopInfo;
                            for (int i = 0; i < hopInfo.Length; i++)
                            {
                                hopTableIndex_CB.Items.Add(hopInfo[i].HopTableID);
                            }
                        }
                        else
                        {
                            this.hopTableIndexLabel.Visible = false;
                            this.hopTableIndex_CB.Visible = false;
                            this.hopFrequencies_TB.Visible = false;
                            this.txFreqLabel.Visible = true;
                            this.txFreq_CB.Visible = true;

                            this.Controls.Add(this.txFreq_CB);
                            txFreq_CB.Items.Clear();
                            int[] freq = m_AppForm.m_ReaderAPI.ReaderCapabilities.FixedFreqValues;
                            for (int i = 0; i < freq.Length; i++)
                                txFreq_CB.Items.Add(freq[i].ToString());
                        }

                        antennaID_CB.Items.Clear();
                        foreach (ushort id in antID)
                            antennaID_CB.Items.Add(id);

                        m_IsLoaded = true;
                        this.ResumeLayout(false);
                    }
                }
                // This triggers the content update
                antennaID_CB.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Antenna Configuration");
            }
            m_IsChanged = false;
        }

        private void AntennaConfigForm_Closing(object sender, EventArgs e)
        {
  
        }

        private void hopTableIndex_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_AppForm.m_ReaderAPI.IsConnected)
                {
                    if (m_AppForm.m_ReaderAPI.ReaderCapabilities.IsHoppingEnabled)
                    {
                        FrequencyHopInfo hopInfo = m_AppForm.m_ReaderAPI.ReaderCapabilities.FrequencyHopInfo;
                        int index = int.Parse(hopTableIndex_CB.SelectedItem.ToString());
                        int[] freqs = hopInfo[index - 1].FrequencyHopValues;

                        string hopTableFreqListMultiline = "";
                        hopFrequencies_TB.Text = "";
                        foreach (int freq in freqs)
                        {
                            if (hopTableFreqListMultiline != "")
                            {
                                hopTableFreqListMultiline = hopTableFreqListMultiline + ", ";
                            }
                            hopTableFreqListMultiline = hopTableFreqListMultiline + freq.ToString();
                        }
                        hopFrequencies_TB.Text = hopTableFreqListMultiline;
                    }
                }
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Hop Table");
            }
            m_IsChanged = true;
        }

        private void antennaID_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (antennaID_CB.SelectedIndex != -1)
            {
                ushort[] antID = m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas;
                if (antID.Length > 0)
                {
                    Antennas.Config antConfig =
                        m_AppForm.m_ReaderAPI.Config.Antennas[antID[antennaID_CB.SelectedIndex]].GetConfig();

                    receiveSensitivity_CB.SelectedIndex = antConfig.ReceiveSensitivityIndex;
                    transmitPower_CB.SelectedIndex = antConfig.TransmitPowerIndex;

                    if (m_AppForm.m_ReaderAPI.ReaderCapabilities.IsHoppingEnabled)
                        hopTableIndex_CB.SelectedIndex = antConfig.TransmitFrequencyIndex - 1;
                    else
                        txFreq_CB.SelectedIndex = 0;

                    m_IsChanged = true;
                }
            }
        }

        private void receiveSensitivity_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_IsChanged = true;
        }

        private void transmitPower_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_IsChanged = true;
        }

        private void antennaConfigButton_Click(object sender, EventArgs e)
        {
            if (m_IsChanged == false)
            {
                this.m_AppForm.notifyUser("No changes made", "Antenna Config");
                return;
            }
            try
            {

                if (m_AppForm.m_IsConnected)
                {
                    ushort[] antID = m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas;
                    Antennas.Config antConfig =
                        m_AppForm.m_ReaderAPI.Config.Antennas[antID[antennaID_CB.SelectedIndex]].GetConfig();

                    antConfig.ReceiveSensitivityIndex = (ushort)receiveSensitivity_CB.SelectedIndex;

                    antConfig.TransmitPowerIndex = (ushort)transmitPower_CB.SelectedIndex;

                    if (!m_AppForm.m_ReaderAPI.ReaderCapabilities.IsHoppingEnabled)
                    {
                        antConfig.TransmitFrequencyIndex = (ushort)(txFreq_CB.SelectedIndex + 1);
                    }
                    else
                    {
                        antConfig.TransmitFrequencyIndex = (ushort)(hopTableIndex_CB.SelectedIndex + 1);
                    }

                    m_AppForm.m_ReaderAPI.Config.Antennas[antID[antennaID_CB.SelectedIndex]].SetConfig(antConfig);
                }
                else
                {
                    this.m_AppForm.notifyUser("Please connect to a reader", "Antenna Config");
                }
                this.Close();
            }
            catch (InvalidUsageException iue)
            {
                this.m_AppForm.notifyUser(iue.Info, "Antenna Config");
            }
            catch (OperationFailureException ofe)
            {
                this.m_AppForm.notifyUser(ofe.VendorMessage, "Antenna Config");
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Antenna Config");
            }
        }
    }
}