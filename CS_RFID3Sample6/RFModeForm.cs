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
    public partial class RFModeForm : Form
    {
        private AppForm m_AppForm;
        internal bool m_IsLoaded;

        public RFModeForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();
        }

        private void RFModeForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (m_AppForm.m_ReaderAPI.IsConnected)
                {
                    ushort[] antID = m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas;

                    if (antID.Length > 0)
                    {
                        antenna_CB.Items.Clear();
                        foreach (ushort id in antID)
                            antenna_CB.Items.Add(id);
                        antenna_CB.SelectedIndex = 0;
                        Antennas.RFMode antRFMode = null;

                        try
                        {
                            antRFMode = m_AppForm.m_ReaderAPI.Config.Antennas[antID[antenna_CB.SelectedIndex]].GetRFMode();
                        }
                        catch (OperationFailureException ex)
                        {
                            this.m_AppForm.notifyUser(ex.VendorMessage, "RF-Mode");
                        }

                        int numberRFModes = m_AppForm.m_ReaderAPI.ReaderCapabilities.RFModes[0].Length;

                        rfModeTable_CB.Items.Clear();
                        for (int j = 0; j < numberRFModes; j++)
                            {
                            rfModeTable_CB.Items.Add(j);

                        }
                        if (antRFMode != null)
                            rfModeTable_CB.SelectedIndex = (int)antRFMode.TableIndex;

                    }
                    m_IsLoaded = true;
                }
            }
            catch (OperationFailureException ofe)
            {
                this.m_AppForm.notifyUser(ofe.VendorMessage, "RF-Mode");
            }           
        }

        private void RFModeForm_Closing(object sender, EventArgs e)
        {
        }

        private void antenna_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = int.Parse(antenna_CB.SelectedItem.ToString());
            Antennas.RFMode antRFMode = null;
            try
            {
                antRFMode = m_AppForm.m_ReaderAPI.Config.Antennas[index].GetRFMode();
                if (rfModeTable_CB.Items.Count > antRFMode.TableIndex)
                {
                    rfModeTable_CB.SelectedIndex = (int)antRFMode.TableIndex;
                }
            }
            catch (OperationFailureException ofe)
            {
                this.m_AppForm.notifyUser(ofe.VendorMessage, "RF-Mode");
            }
            if (antRFMode != null)
            {
                tari_TB.Text = antRFMode.Tari.ToString();
            }
        }

        private void rfModeTable_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rfModeTable_CB.SelectedItem != null)
            {
                int index = rfModeTable_CB.SelectedIndex;
                if (index >= 0)
                {
                    RFModeTableEntry rfTableEntry = m_AppForm.m_ReaderAPI.ReaderCapabilities.RFModes[0][index];
                            for (int k = 0; k < rfModelistView.Items.Count; k++)
                            {
                                if (rfModelistView.Items[k].SubItems.Count > 1)
                                    rfModelistView.Items[k].SubItems.RemoveAt(1);
                            }
                    rfModelistView.Items[0].SubItems.Add(rfTableEntry.ModeIdentifier.ToString());
                    rfModelistView.Items[1].SubItems.Add(rfTableEntry.DivideRatio.ToString());
                    rfModelistView.Items[2].SubItems.Add(rfTableEntry.BdrValue.ToString());
                    rfModelistView.Items[3].SubItems.Add(rfTableEntry.Modulation.ToString());
                    rfModelistView.Items[4].SubItems.Add(rfTableEntry.ForwardLinkModulationType.ToString());
                    rfModelistView.Items[5].SubItems.Add(rfTableEntry.PieValue.ToString());
                    rfModelistView.Items[6].SubItems.Add(rfTableEntry.MinTariValue.ToString());
                    rfModelistView.Items[7].SubItems.Add(rfTableEntry.MaxTariValue.ToString());
                    rfModelistView.Items[8].SubItems.Add(rfTableEntry.StepTariValue.ToString());
                    rfModelistView.Items[9].SubItems.Add(rfTableEntry.SpectralMaskIndicator.ToString());
                    rfModelistView.Items[10].SubItems.Add(rfTableEntry.EPCHAGTCConformance.ToString());

                }
            }
        }

        private void rfModeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_AppForm.m_ReaderAPI.IsConnected)
                {
                    int index = int.Parse(antenna_CB.SelectedItem.ToString());
                    Antennas.RFMode antRFMode = m_AppForm.m_ReaderAPI.Config.Antennas[index].GetRFMode();

                    antRFMode.Tari = uint.Parse(tari_TB.Text);
                    antRFMode.TableIndex = (uint)(rfModeTable_CB.SelectedIndex);

                    m_AppForm.m_ReaderAPI.Config.Antennas[index].SetRFMode(antRFMode);
                }
                this.Close();
            }
            catch (InvalidUsageException ex)
            {
                this.m_AppForm.notifyUser(ex.Info, "RF-Mode");
            }
            catch (OperationFailureException ofe)
            {
                this.m_AppForm.notifyUser(ofe.VendorMessage, "RF-Mode");
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "RF-Mode");
            }

        }
    }
}