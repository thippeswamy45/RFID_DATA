using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Symbol.RFID3;

namespace CS_RFID3Sample6
{

    public partial class LocateForm : Form
    {
        AppForm m_AppForm = null;
        internal MotoProgressBar Locate_PB = null;
        internal int lastLocatedTagTimeStamp;

        public LocateForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();

            // this.Locate_PB = new VerticalProgressBar();
            Locate_PB = new MotoProgressBar();

            Locate_PB.Location = new System.Drawing.Point(100, 90);
            Locate_PB.Name = "Indicator_PB";

            Locate_PB.Size = new System.Drawing.Size(40, 147);
            Locate_PB.Maximum = 100;
            Locate_PB.Minimum = 0;
            Controls.Add(this.Locate_PB);

            // Set initial value to 0
            Locate_PB.Value = 0;

        }

        internal void stopLocationing()
        {
            try
            {
                m_AppForm.m_ReaderAPI.Actions.TagLocationing.Stop();
                locateButton.Text = "Start";
                Locate_PB.Value = 0;
            }
            catch (InvalidOperationException ioe)
            {
                m_AppForm.notifyUser(ioe.Message, "Stop Locate");
            }
            catch (OperationFailureException ofe)
            {
                m_AppForm.notifyUser(ofe.StatusDescription, "Stop Locate");
            }
            catch (InvalidUsageException iue)
            {
                m_AppForm.notifyUser(iue.Info, "Stop Locate");
            }
            catch (Exception ex)
            {
                m_AppForm.notifyUser(ex.Message, "Stop Locate");
            }
        }

        private void locateButton_Click(object sender, EventArgs e)
        {
            if (null != m_AppForm.m_ReaderAPI && m_AppForm.m_IsConnected)
            {
                if (locateButton.Text == "Start")
                {
                    if (tagID_TB.Text.Length >= 64)
                        m_AppForm.notifyUser("Cannot Locate tags with tagID length 64 characters or more. Please choose a subset of the tagID to locate tag", "Locate Operation");
                    else
                    {
                        ushort[] antennaList = new ushort[1] { 1 };
                        OPERATION_QUALIFER[] opList = new OPERATION_QUALIFER[1] { OPERATION_QUALIFER.LOCATE_TAG };
                        AntennaInfo antennaInfo = new AntennaInfo(antennaList, opList);
                        try
                        {
                            m_AppForm.m_ReaderAPI.Actions.TagLocationing.Perform(tagID_TB.Text, antennaInfo);
                            locateButton.Text = "Stop";
                        }
                        catch (InvalidOperationException ioe)
                        {
                            m_AppForm.notifyUser(ioe.Message, "Locate Operation");
                        }
                        catch (OperationFailureException ofe)
                        {
                            m_AppForm.notifyUser(ofe.StatusDescription, "Locate Operation");
                        }
                        catch (InvalidUsageException iue)
                        {
                            m_AppForm.notifyUser(iue.Info, "Locate Operation");
                        }
                        catch (Exception ex)
                        {
                            m_AppForm.notifyUser(ex.Message, "Locate Operation");
                        }
                    }
                }
                else if (locateButton.Text == "Stop")
                {
                    stopLocationing();
                }
            }
            else
            {
                this.m_AppForm.notifyUser("Please connect to a reader", "Locate Operation");
            }
        }

        private void LocateForm_Load(object sender, EventArgs e)
        {
            this.Locate_PB.Value = 0;
            if (m_AppForm.inventoryList.SelectedIndices.Count > 0)
            {
                int selectedIndex = m_AppForm.inventoryList.SelectedIndices[0];
                ListViewItem item = m_AppForm.inventoryList.Items[selectedIndex];
                this.tagID_TB.Text = item.Text;
            }
            else
            {
                tagID_TB.Text = "";
            }
        }

        private void LocateForm_Closing(object sender, CancelEventArgs e)
        {
            if (locateButton.Text == "Stop")
            {
                stopLocationing();
            }
        }



        private void timer_Tick(object sender, System.EventArgs e)
        {
            int currentTimeStamp = System.Environment.TickCount;
            if ((currentTimeStamp - lastLocatedTagTimeStamp) > this.timer.Interval)
            {
                this.Locate_PB.Value = 0;
            }
        }
    }
}