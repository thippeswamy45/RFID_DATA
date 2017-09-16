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
    public partial class TriggerForm : Form
    {
        private AppForm m_AppForm = null;
        private bool m_IsLoaded;
        private Symbol.RFID3.TriggerInfo m_TriggerInfo = null;
 
        public TriggerForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();
            ClearStartGroupBox();
            ClearStopGroupBox();

        }

        public void Reset()
        {
            newTag_CB.SelectedIndex = backTag_CB.SelectedIndex = invisibleTag_CB.SelectedIndex = 2;
            newTag_CB.Enabled = m_AppForm.m_ReaderAPI.IsConnected ? m_AppForm.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported : false;
            backTag_CB.Enabled = m_AppForm.m_ReaderAPI.IsConnected ? m_AppForm.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported : false;
            invisibleTag_CB.Enabled = m_AppForm.m_ReaderAPI.IsConnected ? m_AppForm.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported : false;
            newTag_TB.Enabled = m_AppForm.m_ReaderAPI.IsConnected ? m_AppForm.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported : false;
            backTag_TB.Enabled = m_AppForm.m_ReaderAPI.IsConnected ? m_AppForm.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported : false;
            invisibleTag_TB.Enabled = m_AppForm.m_ReaderAPI.IsConnected ? m_AppForm.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported : false;
        }

        public Symbol.RFID3.TriggerInfo getTriggerInfo()
        {
            if (null == m_TriggerInfo)
            {
                m_TriggerInfo = new TriggerInfo();
            }
            m_TriggerInfo.EnableTagEventReport = m_AppForm.autonomousMode_CB.Checked;
            return m_TriggerInfo;
        }

        internal void ClearStartGroupBox()
        {
            this.startDateLabel.Visible = false;
            this.startDateTimePicker.Visible = false;
            this.startPeriodLabel.Visible = false;
            this.startperiod_TB.Visible = false;
           

            this.startLowHigh_CB.Visible = false;
            this.startHighLow_CB.Visible = false;
            this.startEventLabel.Visible = false;
            this.startPort_CB.Visible = false;
            this.startPortLabel.Visible = false;

            this.startTriggerReleased_CB.Visible = false;
            this.startTriggerPressed_CB.Visible = false;
            this.stopTriggerPressed_CB.Visible = false;

        }

        private void startTriggerType_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearStartGroupBox();

            if (startTriggerType_CB.SelectedIndex == (int)START_TRIGGER_TYPE.START_TRIGGER_TYPE_IMMEDIATE)
            {

            }
            else if (startTriggerType_CB.SelectedIndex == (int)START_TRIGGER_TYPE.START_TRIGGER_TYPE_PERIODIC)
            {
                this.startDateLabel.Visible = true;
                this.startDateTimePicker.Visible = true;
                this.startPeriodLabel.Visible = true;
                this.startperiod_TB.Visible = true;

            }
            else if (startTriggerType_CB.SelectedIndex == (int)START_TRIGGER_TYPE.START_TRIGGER_TYPE_GPI)
            {
                this.startLowHigh_CB.Visible = true;
                this.startHighLow_CB.Visible = true;
                this.startEventLabel.Visible = true;
                this.startPort_CB.Visible = true;
                this.startPortLabel.Visible = true;

                try
                {
                    if (m_AppForm.m_ReaderAPI.IsConnected)
                    {
                        int numGPIPorts = m_AppForm.m_ReaderAPI.ReaderCapabilities.NumGPIPorts;
                        startPort_CB.Items.Clear();                        
                        for (int port = 1; port <= numGPIPorts; port++)
                            startPort_CB.Items.Add(port);
                        if (numGPIPorts > 0)
                            startPort_CB.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    this.m_AppForm.notifyUser(ex.Message, "Trigger Info");
                }
            }
            else if (startTriggerType_CB.SelectedIndex == (int)START_TRIGGER_TYPE.START_TRIGGER_TYPE_HANDHELD)
            {
                this.startTriggerPressed_CB.Visible = true;
                this.startTriggerReleased_CB.Visible = true;
                this.startEventLabel.Visible = true;
                
            }
        }

        private void ClearStopGroupBox()
        {
            this.stopDuration_TB.Visible = false;
            this.stopDurationLabel.Visible = false;

            this.stopTimeout_TB.Visible = false;
            this.stopTimeoutLabel.Visible = false;
            this.stopLowHigh_CB.Visible = false;
            this.stopHighLow_CB.Visible = false;
            this.stopEventLabel.Visible = false;
            this.stopPort_CB.Visible = false;
            this.stopPortLabel.Visible = false;

            this.stopTagObservationLabel.Visible = false;
            this.stopTagObservation_TB.Visible = false;
            this.stopTagObservTimeoutLabel.Visible = false;
            this.stopTagObservTimeout_TB.Visible = false;

            this.stopNAttemptsLabel.Visible = false;
            this.stopNAttempts_TB.Visible = false;
            this.stopNAttemptsTimeoutLabel.Visible = false;
            this.stopNAttemptsTimeout_TB.Visible = false;

            this.stopTriggerPressed_CB.Visible = false;
            this.stopTriggerReleased_CB.Visible = false;
            this.stopTriggerTimeout_TB.Visible = false;
        }

        private void stopTriggerType_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearStopGroupBox();

            if (stopTriggerType_CB.SelectedIndex == (int)STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_IMMEDIATE)
            {

            }
            else if (stopTriggerType_CB.SelectedIndex == (int)STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_DURATION)
            {
                this.stopDurationLabel.Visible = true;
                this.stopDuration_TB.Visible = true;
            }
            else if (stopTriggerType_CB.SelectedIndex == (int)STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_GPI_WITH_TIMEOUT)
            {
                this.stopTimeout_TB.Visible = true;
                this.stopTimeoutLabel.Visible = true;
                this.stopLowHigh_CB.Visible = true;
                this.stopHighLow_CB.Visible = true;
                this.stopEventLabel.Visible = true;
                this.stopPort_CB.Visible = true;
                this.stopPortLabel.Visible = true;
                
                
                try
                {
                    if (m_AppForm.m_ReaderAPI.IsConnected)
                    {
                        int numGPIPorts = m_AppForm.m_ReaderAPI.ReaderCapabilities.NumGPIPorts;
                        this.stopPort_CB.Items.Clear();
                        for (int port = 1; port <= numGPIPorts; port++)
                            this.stopPort_CB.Items.Add(port);
                        if (numGPIPorts > 0)
                            this.stopPort_CB.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    this.m_AppForm.notifyUser(ex.Message, "Trigger Info");
                }                
            }
            else if (stopTriggerType_CB.SelectedIndex == (int)STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_TAG_OBSERVATION_WITH_TIMEOUT)
            {
                this.stopTagObservationLabel.Visible = true;
                this.stopTagObservation_TB.Visible = true;
                this.stopTagObservTimeoutLabel.Visible = true;
                this.stopTagObservTimeout_TB.Visible = true;
            }
            else if (stopTriggerType_CB.SelectedIndex == (int)STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_N_ATTEMPTS_WITH_TIMEOUT)
            {
                this.stopNAttemptsLabel.Visible = true;
                this.stopNAttempts_TB.Visible = true;
                this.stopNAttemptsTimeoutLabel.Visible = true;
                this.stopNAttemptsTimeout_TB.Visible = true;
            }
            else if (stopTriggerType_CB.SelectedIndex == (int)STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_HANDHELD_WITH_TIMEOUT)
            {
                this.stopTriggerTimeout_TB.Visible = true;
                this.stopTimeoutLabel.Visible = true;
                this.stopTriggerPressed_CB.Visible = true;
                this.stopTriggerReleased_CB.Visible = true;
                this.stopEventLabel.Visible = true;
            }
        }

        private void TriggerForm_Load(object sender, EventArgs e)
        {
            if (!m_IsLoaded)
            {
                this.start_TP.Controls.Add(this.startTriggerTypeLabel);
                this.start_TP.Controls.Add(this.startTriggerType_CB);

                this.stop_TP.Controls.Add(this.stopTriggerTypeLabel);
                this.stop_TP.Controls.Add(this.stopTriggerType_CB);

                this.startperiod_TB.Text = "1";
                this.tagReportTriggerTB.Text = "0";
                m_IsLoaded = true;
                this.stopTriggerTimeout_TB.Text = "0";

            }
        }

        private void TriggerForm_Closing(object sender, EventArgs e)
        {
        }

        private void triggerApplyButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_AppForm.m_ReaderAPI.IsConnected)
                {
                    if (null == m_TriggerInfo)
                    {
                        m_TriggerInfo = new TriggerInfo();
                    }
                    m_AppForm.m_DurationTriggerTime = 0;

                    if (startTriggerType_CB.SelectedIndex == (int)START_TRIGGER_TYPE.START_TRIGGER_TYPE_IMMEDIATE)
                    {
                        m_TriggerInfo.StartTrigger.Type = START_TRIGGER_TYPE.START_TRIGGER_TYPE_IMMEDIATE;
                    }
                    else if (startTriggerType_CB.SelectedIndex == (int)START_TRIGGER_TYPE.START_TRIGGER_TYPE_PERIODIC)
                    {
                        m_TriggerInfo.StartTrigger.Type = START_TRIGGER_TYPE.START_TRIGGER_TYPE_PERIODIC;

                        m_TriggerInfo.StartTrigger.Periodic.StartTime = this.startDateTimePicker.Value.ToUniversalTime();
                        m_TriggerInfo.StartTrigger.Periodic.Period = uint.Parse(this.startperiod_TB.Text);
                    }
                    else if (startTriggerType_CB.SelectedIndex == (int)START_TRIGGER_TYPE.START_TRIGGER_TYPE_GPI)
                    {
                        m_TriggerInfo.StartTrigger.Type = START_TRIGGER_TYPE.START_TRIGGER_TYPE_GPI;
                        m_TriggerInfo.StartTrigger.GPI.PortNumber = startPort_CB.SelectedIndex + 1;

                        if (this.startLowHigh_CB.Checked || this.startLowHigh_CB.Checked)
                            m_TriggerInfo.StartTrigger.GPI.GPIEvent = true;
                        else
                            m_TriggerInfo.StartTrigger.GPI.GPIEvent = false;
                    }
                    else if (startTriggerType_CB.SelectedIndex == (int)START_TRIGGER_TYPE.START_TRIGGER_TYPE_HANDHELD)
                    {
                        m_TriggerInfo.StartTrigger.Type = START_TRIGGER_TYPE.START_TRIGGER_TYPE_HANDHELD;

                        if (startTriggerPressed_CB.Checked)
                            m_TriggerInfo.StartTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED;
                        else
                            m_TriggerInfo.StartTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_RELEASED;

                    }

                    if (stopTriggerType_CB.SelectedIndex == (int)STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_IMMEDIATE)
                    {
                        m_TriggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_IMMEDIATE;
                    }
                    else if (stopTriggerType_CB.SelectedIndex == (int)STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_DURATION)
                    {
                        m_TriggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_DURATION;
                        m_TriggerInfo.StopTrigger.Duration = uint.Parse(this.stopDuration_TB.Text);
                        m_AppForm.m_DurationTriggerTime = m_TriggerInfo.StopTrigger.Duration;
                    }
                    else if (stopTriggerType_CB.SelectedIndex == (int)STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_GPI_WITH_TIMEOUT)
                    {
                        m_TriggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_GPI_WITH_TIMEOUT;
                        m_TriggerInfo.StopTrigger.GPI.PortNumber = stopPort_CB.SelectedIndex + 1;
                        m_TriggerInfo.StopTrigger.GPI.Timeout = uint.Parse(stopTimeout_TB.Text);

                        if (this.stopLowHigh_CB.Checked || this.stopLowHigh_CB.Checked)
                            m_TriggerInfo.StopTrigger.GPI.GPIEvent = true;
                        else
                            m_TriggerInfo.StopTrigger.GPI.GPIEvent = false;
                    }
                    else if (stopTriggerType_CB.SelectedIndex == (int)STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_TAG_OBSERVATION_WITH_TIMEOUT)
                    {
                        m_TriggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_TAG_OBSERVATION_WITH_TIMEOUT;
                        m_TriggerInfo.StopTrigger.TagObservation.N = ushort.Parse(stopTagObservation_TB.Text);
                        m_TriggerInfo.StopTrigger.TagObservation.Timeout = uint.Parse(this.stopTagObservTimeout_TB.Text);
                    }
                    else if (stopTriggerType_CB.SelectedIndex == (int)STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_N_ATTEMPTS_WITH_TIMEOUT)
                    {
                        m_TriggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_N_ATTEMPTS_WITH_TIMEOUT;
                        m_TriggerInfo.StopTrigger.NumAttempts.N = ushort.Parse(this.stopNAttempts_TB.Text);
                        m_TriggerInfo.StopTrigger.NumAttempts.Timeout = uint.Parse(this.stopNAttemptsTimeout_TB.Text);
                    }
                    else if (stopTriggerType_CB.SelectedIndex == (int)STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_HANDHELD_WITH_TIMEOUT)
                    {
                        m_TriggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_HANDHELD_WITH_TIMEOUT;

                        if (stopTriggerPressed_CB.Checked)
                            m_TriggerInfo.StopTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED;
                        else
                            m_TriggerInfo.StopTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_RELEASED;

                        m_TriggerInfo.StopTrigger.Handheld.Timeout = uint.Parse(this.stopTriggerTimeout_TB.Text);

                    }
                    if (m_AppForm.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported)
                    {
                        m_TriggerInfo.TagEventReportInfo.ReportNewTagEvent = (TAG_EVENT_REPORT_TRIGGER)newTag_CB.SelectedIndex;
                        m_TriggerInfo.TagEventReportInfo.ReportTagBackToVisibilityEvent = (TAG_EVENT_REPORT_TRIGGER)backTag_CB.SelectedIndex;
                        m_TriggerInfo.TagEventReportInfo.ReportTagInvisibleEvent = (TAG_EVENT_REPORT_TRIGGER)invisibleTag_CB.SelectedIndex;
                        m_TriggerInfo.TagEventReportInfo.NewTagEventModeratedTimeoutMilliseconds = ushort.Parse(newTag_TB.Text);
                        m_TriggerInfo.TagEventReportInfo.TagBackToVisibilityModeratedTimeoutMilliseconds = ushort.Parse(backTag_TB.Text);
                        m_TriggerInfo.TagEventReportInfo.TagInvisibleEventModeratedTimeoutMilliseconds = ushort.Parse(invisibleTag_TB.Text);
                    }
                    m_TriggerInfo.TagReportTrigger = uint.Parse(tagReportTriggerTB.Text);
                    this.Close();
                }
                else
                {
                    this.m_AppForm.notifyUser("Please connect to Reader", "Trigger Info");
                }
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Trigger Info");
            }
        }
        private void newTag_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newTag_CB.SelectedIndex == (int)TAG_EVENT_REPORT_TRIGGER.IMMEDIATE)
                newTag_TB.Enabled = false;
        }

        private void invisibleTag_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (invisibleTag_CB.SelectedIndex == (int)TAG_EVENT_REPORT_TRIGGER.IMMEDIATE)
                invisibleTag_TB.Enabled = false;
        }

        private void backTag_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (backTag_CB.SelectedIndex == (int)TAG_EVENT_REPORT_TRIGGER.IMMEDIATE)
                backTag_TB.Enabled = false;
        }

        void startTriggerPressed_CB_Click(object sender, System.EventArgs e)
        {
            this.startTriggerReleased_CB.Checked = false;
        }

        void startTriggerReleased_CB_Click(object sender, System.EventArgs e)
        {
            this.startTriggerPressed_CB.Checked = false;
        }

        void stopTriggerPressed_CB_Click(object sender, System.EventArgs e)
        {
            this.stopTriggerReleased_CB.Checked = false;
        }

        void stopTriggerReleased_CB_Click(object sender, System.EventArgs e)
        {
            this.stopTriggerPressed_CB.Checked = false;
        }
    }
}