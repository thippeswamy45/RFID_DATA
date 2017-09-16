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
    public partial class SingulationForm : Form
    {
        private AppForm m_AppForm;
        internal bool m_IsLoaded;

        public SingulationForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();
        }

        private void SingulationForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (m_AppForm.m_ReaderAPI.IsConnected)
                {
                    ushort[] antID = m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas;

                    if (antID.Length > 0)
                    {
                        antennaID_CB.Items.Clear();
                        foreach (ushort id in antID)
                            antennaID_CB.Items.Add(id);
                        antennaID_CB.SelectedIndex = 0;
                    }
                    m_IsLoaded = true;
                }
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Singulation Control");
            }
        }

        private void SingulationForm_Closing(object sender, EventArgs e)
        {
        }

        private void antennaID_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_AppForm.m_ReaderAPI.IsConnected)
                {
                    ushort[] antID = m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas;
                    Antennas.SingulationControl singularControl =
                        m_AppForm.m_ReaderAPI.Config.Antennas[antID[antennaID_CB.SelectedIndex]].GetSingulationControl();

                    session_CB.SelectedIndex = (int)singularControl.Session;

                    tagPopulation_TB.Text = singularControl.TagPopulation.ToString();
                    tagTransit_TB.Text = singularControl.TagTransitTime.ToString();
                    stateAware_CB.Checked = singularControl.Action.PerformStateAwareSingulationAction;
                    if (singularControl.Action.PerformStateAwareSingulationAction)
                    {
                        Antennas.SingulationControl.SingulationAction action =
                            singularControl.Action;

                        inventoryState_CB.SelectedIndex = (int)action.InventoryState;
                        SLFlag_CB.SelectedIndex = (int)action.SLFlag;
                    }
                    else
                    {
                        inventoryState_CB.Enabled = false;
                        SLFlag_CB.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Singulation Control");
            }
        }

        private void singulationButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_AppForm.m_ReaderAPI.IsConnected)
                {
                    ushort[] antID = m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas;
                    Antennas.SingulationControl singularControl =
                        m_AppForm.m_ReaderAPI.Config.Antennas[antID[antennaID_CB.SelectedIndex]].GetSingulationControl();

                    singularControl.Session = (SESSION)session_CB.SelectedIndex;

                    singularControl.TagPopulation = ushort.Parse(tagPopulation_TB.Text);
                    singularControl.TagTransitTime = ushort.Parse(tagTransit_TB.Text);

                    singularControl.Action.PerformStateAwareSingulationAction = stateAware_CB.Checked;
                    if (singularControl.Action.PerformStateAwareSingulationAction)
                    {
                        singularControl.Action.InventoryState
                            = (INVENTORY_STATE)inventoryState_CB.SelectedIndex;

                        singularControl.Action.SLFlag
                            = (SL_FLAG)SLFlag_CB.SelectedIndex;
                    }
                    m_AppForm.m_ReaderAPI.Config.Antennas[antID[antennaID_CB.SelectedIndex]].SetSingulationControl(singularControl);
                }
                this.Close();
            }
            catch (OperationFailureException ofe)
            {
                this.m_AppForm.notifyUser(ofe.VendorMessage, "Singulation Control");
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Singulation Control");
            }
        }

        private void stateAware_CB_CheckStateChanged(object sender, EventArgs e)
        {
            inventoryState_CB.Enabled = stateAware_CB.Checked;
            SLFlag_CB.Enabled = stateAware_CB.Checked;
            if (stateAware_CB.Checked)
                stateAware_PB.BackColor = Color.White;
            else
                stateAware_PB.BackColor = Color.Gray;
        }
    }
}