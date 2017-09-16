namespace CS_RFID3Sample6
{
    partial class TriggerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.start_TP = new System.Windows.Forms.TabPage();
            this.startDateLabel = new System.Windows.Forms.Label();
            this.startDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.startPeriodLabel = new System.Windows.Forms.Label();
            this.startperiod_TB = new System.Windows.Forms.TextBox();
            this.startLowHigh_CB = new System.Windows.Forms.CheckBox();
            this.startHighLow_CB = new System.Windows.Forms.CheckBox();
            this.startPort_CB = new System.Windows.Forms.ComboBox();
            this.startPortLabel = new System.Windows.Forms.Label();
            this.startTriggerPressed_CB = new System.Windows.Forms.CheckBox();
            this.startTriggerReleased_CB = new System.Windows.Forms.CheckBox();
            this.startEventLabel = new System.Windows.Forms.Label();
            this.stop_TP = new System.Windows.Forms.TabPage();
            this.stopDurationLabel = new System.Windows.Forms.Label();
            this.stopDuration_TB = new System.Windows.Forms.TextBox();
            this.stopTimeout_TB = new System.Windows.Forms.TextBox();
            this.stopLowHigh_CB = new System.Windows.Forms.CheckBox();
            this.stopHighLow_CB = new System.Windows.Forms.CheckBox();
            this.stopPort_CB = new System.Windows.Forms.ComboBox();
            this.stopPortLabel = new System.Windows.Forms.Label();
            this.stopTagObservationLabel = new System.Windows.Forms.Label();
            this.stopTagObservation_TB = new System.Windows.Forms.TextBox();
            this.stopTagObservTimeoutLabel = new System.Windows.Forms.Label();
            this.stopTagObservTimeout_TB = new System.Windows.Forms.TextBox();
            this.stopNAttemptsLabel = new System.Windows.Forms.Label();
            this.stopNAttempts_TB = new System.Windows.Forms.TextBox();
            this.stopNAttemptsTimeoutLabel = new System.Windows.Forms.Label();
            this.stopNAttemptsTimeout_TB = new System.Windows.Forms.TextBox();
            this.stopTriggerTimeout_TB = new System.Windows.Forms.TextBox();
            this.stopTimeoutLabel = new System.Windows.Forms.Label();
            this.stopTriggerPressed_CB = new System.Windows.Forms.CheckBox();
            this.stopTriggerReleased_CB = new System.Windows.Forms.CheckBox();
            this.stopEventLabel = new System.Windows.Forms.Label();
            this.report_TP = new System.Windows.Forms.TabPage();
            this.backTag_TB = new System.Windows.Forms.TextBox();
            this.invisibleTag_TB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.newTag_TB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backTag_CB = new System.Windows.Forms.ComboBox();
            this.invisibleTag_CB = new System.Windows.Forms.ComboBox();
            this.newTag_CB = new System.Windows.Forms.ComboBox();
            this.stopTriggerType_CB = new System.Windows.Forms.ComboBox();
            this.stopTriggerTypeLabel = new System.Windows.Forms.Label();
            this.startTriggerType_CB = new System.Windows.Forms.ComboBox();
            this.startTriggerTypeLabel = new System.Windows.Forms.Label();
            this.tagReportTriggerTB = new System.Windows.Forms.TextBox();
            this.tagReportTriggerLabel = new System.Windows.Forms.Label();
            this.triggerApplyButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.start_TP.SuspendLayout();
            this.stop_TP.SuspendLayout();
            this.report_TP.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl.Controls.Add(this.start_TP);
            this.tabControl.Controls.Add(this.stop_TP);
            this.tabControl.Controls.Add(this.report_TP);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.None;
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(240, 159);
            this.tabControl.TabIndex = 0;
            // 
            // start_TP
            // 
            this.start_TP.Controls.Add(this.startDateLabel);
            this.start_TP.Controls.Add(this.startDateTimePicker);
            this.start_TP.Controls.Add(this.startPeriodLabel);
            this.start_TP.Controls.Add(this.startperiod_TB);
            this.start_TP.Controls.Add(this.startLowHigh_CB);
            this.start_TP.Controls.Add(this.startHighLow_CB);
            this.start_TP.Controls.Add(this.startPort_CB);
            this.start_TP.Controls.Add(this.startPortLabel);
            this.start_TP.Controls.Add(this.startTriggerPressed_CB);
            this.start_TP.Controls.Add(this.startTriggerReleased_CB);
            this.start_TP.Controls.Add(this.startEventLabel);
            this.start_TP.Location = new System.Drawing.Point(0, 0);
            this.start_TP.Name = "start_TP";
            this.start_TP.Size = new System.Drawing.Size(240, 136);
            this.start_TP.Text = "Start Trigger";
            // 
            // startDateLabel
            // 
            this.startDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.startDateLabel.Location = new System.Drawing.Point(7, 60);
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Size = new System.Drawing.Size(67, 20);
            this.startDateLabel.Text = "Start Date";
            // 
            // startDateTimePicker
            // 
            this.startDateTimePicker.CustomFormat = "MMM/dd/yy  hh:mm:ss tt";
            this.startDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.startDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDateTimePicker.Location = new System.Drawing.Point(95, 60);
            this.startDateTimePicker.Name = "startDateTimePicker";
            this.startDateTimePicker.Size = new System.Drawing.Size(138, 20);
            this.startDateTimePicker.TabIndex = 5;
            this.startDateTimePicker.Value = new System.DateTime(2011, 2, 17, 14, 13, 35, 930);
            // 
            // startPeriodLabel
            // 
            this.startPeriodLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.startPeriodLabel.Location = new System.Drawing.Point(7, 106);
            this.startPeriodLabel.Name = "startPeriodLabel";
            this.startPeriodLabel.Size = new System.Drawing.Size(82, 20);
            this.startPeriodLabel.Text = "Period(ms)";
            // 
            // startperiod_TB
            // 
            this.startperiod_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.startperiod_TB.Location = new System.Drawing.Point(95, 103);
            this.startperiod_TB.Name = "startperiod_TB";
            this.startperiod_TB.Size = new System.Drawing.Size(118, 19);
            this.startperiod_TB.TabIndex = 8;
            // 
            // startLowHigh_CB
            // 
            this.startLowHigh_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.startLowHigh_CB.Location = new System.Drawing.Point(95, 106);
            this.startLowHigh_CB.Name = "startLowHigh_CB";
            this.startLowHigh_CB.Size = new System.Drawing.Size(118, 20);
            this.startLowHigh_CB.TabIndex = 9;
            this.startLowHigh_CB.Text = "Low To High";
            // 
            // startHighLow_CB
            // 
            this.startHighLow_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.startHighLow_CB.Location = new System.Drawing.Point(95, 80);
            this.startHighLow_CB.Name = "startHighLow_CB";
            this.startHighLow_CB.Size = new System.Drawing.Size(118, 20);
            this.startHighLow_CB.TabIndex = 8;
            this.startHighLow_CB.Text = "High To Low";
            // 
            // startPort_CB
            // 
            this.startPort_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.startPort_CB.Location = new System.Drawing.Point(95, 44);
            this.startPort_CB.Name = "startPort_CB";
            this.startPort_CB.Size = new System.Drawing.Size(120, 20);
            this.startPort_CB.TabIndex = 6;
            // 
            // startPortLabel
            // 
            this.startPortLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.startPortLabel.Location = new System.Drawing.Point(7, 44);
            this.startPortLabel.Name = "startPortLabel";
            this.startPortLabel.Size = new System.Drawing.Size(67, 20);
            this.startPortLabel.Text = "Port";
            // 
            // startTriggerPressed_CB
            // 
            this.startTriggerPressed_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.startTriggerPressed_CB.Location = new System.Drawing.Point(95, 80);
            this.startTriggerPressed_CB.Name = "startTriggerPressed_CB";
            this.startTriggerPressed_CB.Size = new System.Drawing.Size(130, 20);
            this.startTriggerPressed_CB.TabIndex = 9;
            this.startTriggerPressed_CB.Text = "Trigger Pressed";
            this.startTriggerPressed_CB.Click += new System.EventHandler(this.startTriggerPressed_CB_Click);
            // 
            // startTriggerReleased_CB
            // 
            this.startTriggerReleased_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.startTriggerReleased_CB.Location = new System.Drawing.Point(95, 54);
            this.startTriggerReleased_CB.Name = "startTriggerReleased_CB";
            this.startTriggerReleased_CB.Size = new System.Drawing.Size(130, 20);
            this.startTriggerReleased_CB.TabIndex = 8;
            this.startTriggerReleased_CB.Text = "Trigger Released";
            this.startTriggerReleased_CB.Click += new System.EventHandler(this.startTriggerReleased_CB_Click);
            // 
            // startEventLabel
            // 
            this.startEventLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.startEventLabel.Location = new System.Drawing.Point(7, 69);
            this.startEventLabel.Name = "startEventLabel";
            this.startEventLabel.Size = new System.Drawing.Size(67, 20);
            this.startEventLabel.Text = "Event";
            // 
            // stop_TP
            // 
            this.stop_TP.Controls.Add(this.stopDurationLabel);
            this.stop_TP.Controls.Add(this.stopDuration_TB);
            this.stop_TP.Controls.Add(this.stopTimeout_TB);
            this.stop_TP.Controls.Add(this.stopLowHigh_CB);
            this.stop_TP.Controls.Add(this.stopHighLow_CB);
            this.stop_TP.Controls.Add(this.stopPort_CB);
            this.stop_TP.Controls.Add(this.stopPortLabel);
            this.stop_TP.Controls.Add(this.stopTagObservationLabel);
            this.stop_TP.Controls.Add(this.stopTagObservation_TB);
            this.stop_TP.Controls.Add(this.stopTagObservTimeoutLabel);
            this.stop_TP.Controls.Add(this.stopTagObservTimeout_TB);
            this.stop_TP.Controls.Add(this.stopNAttemptsLabel);
            this.stop_TP.Controls.Add(this.stopNAttempts_TB);
            this.stop_TP.Controls.Add(this.stopNAttemptsTimeoutLabel);
            this.stop_TP.Controls.Add(this.stopNAttemptsTimeout_TB);
            this.stop_TP.Controls.Add(this.stopTriggerTimeout_TB);
            this.stop_TP.Controls.Add(this.stopTimeoutLabel);
            this.stop_TP.Controls.Add(this.stopTriggerPressed_CB);
            this.stop_TP.Controls.Add(this.stopTriggerReleased_CB);
            this.stop_TP.Controls.Add(this.stopEventLabel);
            this.stop_TP.Location = new System.Drawing.Point(0, 0);
            this.stop_TP.Name = "stop_TP";
            this.stop_TP.Size = new System.Drawing.Size(232, 133);
            this.stop_TP.Text = "Stop Trigger";
            // 
            // stopDurationLabel
            // 
            this.stopDurationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopDurationLabel.Location = new System.Drawing.Point(7, 60);
            this.stopDurationLabel.Name = "stopDurationLabel";
            this.stopDurationLabel.Size = new System.Drawing.Size(84, 20);
            this.stopDurationLabel.Text = "Duration(ms)";
            // 
            // stopDuration_TB
            // 
            this.stopDuration_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopDuration_TB.Location = new System.Drawing.Point(95, 59);
            this.stopDuration_TB.Name = "stopDuration_TB";
            this.stopDuration_TB.Size = new System.Drawing.Size(120, 19);
            this.stopDuration_TB.TabIndex = 6;
            // 
            // stopTimeout_TB
            // 
            this.stopTimeout_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopTimeout_TB.Location = new System.Drawing.Point(95, 71);
            this.stopTimeout_TB.Name = "stopTimeout_TB";
            this.stopTimeout_TB.Size = new System.Drawing.Size(120, 19);
            this.stopTimeout_TB.TabIndex = 19;
            // 
            // stopLowHigh_CB
            // 
            this.stopLowHigh_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopLowHigh_CB.Location = new System.Drawing.Point(95, 119);
            this.stopLowHigh_CB.Name = "stopLowHigh_CB";
            this.stopLowHigh_CB.Size = new System.Drawing.Size(120, 20);
            this.stopLowHigh_CB.TabIndex = 14;
            this.stopLowHigh_CB.Text = "Low To High";
            // 
            // stopHighLow_CB
            // 
            this.stopHighLow_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopHighLow_CB.Location = new System.Drawing.Point(95, 97);
            this.stopHighLow_CB.Name = "stopHighLow_CB";
            this.stopHighLow_CB.Size = new System.Drawing.Size(120, 20);
            this.stopHighLow_CB.TabIndex = 13;
            this.stopHighLow_CB.Text = "High To Low";
            // 
            // stopPort_CB
            // 
            this.stopPort_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopPort_CB.Location = new System.Drawing.Point(95, 41);
            this.stopPort_CB.Name = "stopPort_CB";
            this.stopPort_CB.Size = new System.Drawing.Size(120, 20);
            this.stopPort_CB.TabIndex = 12;
            // 
            // stopPortLabel
            // 
            this.stopPortLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopPortLabel.Location = new System.Drawing.Point(7, 41);
            this.stopPortLabel.Name = "stopPortLabel";
            this.stopPortLabel.Size = new System.Drawing.Size(67, 20);
            this.stopPortLabel.Text = "Port";
            // 
            // stopTagObservationLabel
            // 
            this.stopTagObservationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopTagObservationLabel.Location = new System.Drawing.Point(7, 47);
            this.stopTagObservationLabel.Name = "stopTagObservationLabel";
            this.stopTagObservationLabel.Size = new System.Drawing.Size(82, 38);
            this.stopTagObservationLabel.Text = "Tag Observation";
            // 
            // stopTagObservation_TB
            // 
            this.stopTagObservation_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopTagObservation_TB.Location = new System.Drawing.Point(95, 48);
            this.stopTagObservation_TB.Name = "stopTagObservation_TB";
            this.stopTagObservation_TB.Size = new System.Drawing.Size(120, 19);
            this.stopTagObservation_TB.TabIndex = 20;
            // 
            // stopTagObservTimeoutLabel
            // 
            this.stopTagObservTimeoutLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopTagObservTimeoutLabel.Location = new System.Drawing.Point(7, 86);
            this.stopTagObservTimeoutLabel.Name = "stopTagObservTimeoutLabel";
            this.stopTagObservTimeoutLabel.Size = new System.Drawing.Size(82, 20);
            this.stopTagObservTimeoutLabel.Text = "Timeout(ms)";
            // 
            // stopTagObservTimeout_TB
            // 
            this.stopTagObservTimeout_TB.Location = new System.Drawing.Point(95, 85);
            this.stopTagObservTimeout_TB.Name = "stopTagObservTimeout_TB";
            this.stopTagObservTimeout_TB.Size = new System.Drawing.Size(120, 21);
            this.stopTagObservTimeout_TB.TabIndex = 21;
            // 
            // stopNAttemptsLabel
            // 
            this.stopNAttemptsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopNAttemptsLabel.Location = new System.Drawing.Point(7, 49);
            this.stopNAttemptsLabel.Name = "stopNAttemptsLabel";
            this.stopNAttemptsLabel.Size = new System.Drawing.Size(82, 36);
            this.stopNAttemptsLabel.Text = "No. of Attempts";
            // 
            // stopNAttempts_TB
            // 
            this.stopNAttempts_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopNAttempts_TB.Location = new System.Drawing.Point(95, 48);
            this.stopNAttempts_TB.Name = "stopNAttempts_TB";
            this.stopNAttempts_TB.Size = new System.Drawing.Size(120, 19);
            this.stopNAttempts_TB.TabIndex = 22;
            // 
            // stopNAttemptsTimeoutLabel
            // 
            this.stopNAttemptsTimeoutLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopNAttemptsTimeoutLabel.Location = new System.Drawing.Point(7, 86);
            this.stopNAttemptsTimeoutLabel.Name = "stopNAttemptsTimeoutLabel";
            this.stopNAttemptsTimeoutLabel.Size = new System.Drawing.Size(82, 20);
            this.stopNAttemptsTimeoutLabel.Text = "Timeout(ms)";
            // 
            // stopNAttemptsTimeout_TB
            // 
            this.stopNAttemptsTimeout_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopNAttemptsTimeout_TB.Location = new System.Drawing.Point(95, 85);
            this.stopNAttemptsTimeout_TB.Name = "stopNAttemptsTimeout_TB";
            this.stopNAttemptsTimeout_TB.Size = new System.Drawing.Size(120, 19);
            this.stopNAttemptsTimeout_TB.TabIndex = 23;
            // 
            // stopTriggerTimeout_TB
            // 
            this.stopTriggerTimeout_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopTriggerTimeout_TB.Location = new System.Drawing.Point(95, 71);
            this.stopTriggerTimeout_TB.Name = "stopTriggerTimeout_TB";
            this.stopTriggerTimeout_TB.Size = new System.Drawing.Size(120, 19);
            this.stopTriggerTimeout_TB.TabIndex = 19;
            // 
            // stopTimeoutLabel
            // 
            this.stopTimeoutLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopTimeoutLabel.Location = new System.Drawing.Point(7, 71);
            this.stopTimeoutLabel.Name = "stopTimeoutLabel";
            this.stopTimeoutLabel.Size = new System.Drawing.Size(82, 21);
            this.stopTimeoutLabel.Text = "Timeout(ms)";
            // 
            // stopTriggerPressed_CB
            // 
            this.stopTriggerPressed_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopTriggerPressed_CB.Location = new System.Drawing.Point(95, 118);
            this.stopTriggerPressed_CB.Name = "stopTriggerPressed_CB";
            this.stopTriggerPressed_CB.Size = new System.Drawing.Size(130, 20);
            this.stopTriggerPressed_CB.TabIndex = 14;
            this.stopTriggerPressed_CB.Text = "Trigger Pressed";
            this.stopTriggerPressed_CB.Click += new System.EventHandler(this.stopTriggerPressed_CB_Click);
            // 
            // stopTriggerReleased_CB
            // 
            this.stopTriggerReleased_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopTriggerReleased_CB.Location = new System.Drawing.Point(95, 96);
            this.stopTriggerReleased_CB.Name = "stopTriggerReleased_CB";
            this.stopTriggerReleased_CB.Size = new System.Drawing.Size(130, 20);
            this.stopTriggerReleased_CB.TabIndex = 13;
            this.stopTriggerReleased_CB.Text = "Trigger Released";
            this.stopTriggerReleased_CB.Click += new System.EventHandler(this.stopTriggerReleased_CB_Click);
            // 
            // stopEventLabel
            // 
            this.stopEventLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopEventLabel.Location = new System.Drawing.Point(7, 97);
            this.stopEventLabel.Name = "stopEventLabel";
            this.stopEventLabel.Size = new System.Drawing.Size(53, 20);
            this.stopEventLabel.Text = "Event";
            // 
            // report_TP
            // 
            this.report_TP.Controls.Add(this.backTag_TB);
            this.report_TP.Controls.Add(this.invisibleTag_TB);
            this.report_TP.Controls.Add(this.label3);
            this.report_TP.Controls.Add(this.label2);
            this.report_TP.Controls.Add(this.newTag_TB);
            this.report_TP.Controls.Add(this.label1);
            this.report_TP.Controls.Add(this.backTag_CB);
            this.report_TP.Controls.Add(this.invisibleTag_CB);
            this.report_TP.Controls.Add(this.newTag_CB);
            this.report_TP.Location = new System.Drawing.Point(0, 0);
            this.report_TP.Name = "report_TP";
            this.report_TP.Size = new System.Drawing.Size(232, 133);
            this.report_TP.Text = "Report Trigger";
            // 
            // backTag_TB
            // 
            this.backTag_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.backTag_TB.Location = new System.Drawing.Point(174, 100);
            this.backTag_TB.Name = "backTag_TB";
            this.backTag_TB.Size = new System.Drawing.Size(59, 19);
            this.backTag_TB.TabIndex = 10;
            this.backTag_TB.Text = "500";
            // 
            // invisibleTag_TB
            // 
            this.invisibleTag_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.invisibleTag_TB.Location = new System.Drawing.Point(174, 62);
            this.invisibleTag_TB.Name = "invisibleTag_TB";
            this.invisibleTag_TB.Size = new System.Drawing.Size(59, 19);
            this.invisibleTag_TB.TabIndex = 9;
            this.invisibleTag_TB.Text = "500";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(7, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 30);
            this.label3.Text = "Tag back to visibility";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(7, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.Text = "Tag Invisible";
            // 
            // newTag_TB
            // 
            this.newTag_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.newTag_TB.Location = new System.Drawing.Point(174, 24);
            this.newTag_TB.Name = "newTag_TB";
            this.newTag_TB.Size = new System.Drawing.Size(59, 19);
            this.newTag_TB.TabIndex = 4;
            this.newTag_TB.Text = "500";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.Text = "New Tag";
            // 
            // backTag_CB
            // 
            this.backTag_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.backTag_CB.Items.Add("Never");
            this.backTag_CB.Items.Add("Immediate");
            this.backTag_CB.Items.Add("Moderated");
            this.backTag_CB.Location = new System.Drawing.Point(95, 100);
            this.backTag_CB.Name = "backTag_CB";
            this.backTag_CB.Size = new System.Drawing.Size(71, 20);
            this.backTag_CB.TabIndex = 2;
            this.backTag_CB.SelectedIndexChanged += new System.EventHandler(this.backTag_CB_SelectedIndexChanged);
            // 
            // invisibleTag_CB
            // 
            this.invisibleTag_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.invisibleTag_CB.Items.Add("Never");
            this.invisibleTag_CB.Items.Add("Immediate");
            this.invisibleTag_CB.Items.Add("Moderated");
            this.invisibleTag_CB.Location = new System.Drawing.Point(95, 61);
            this.invisibleTag_CB.Name = "invisibleTag_CB";
            this.invisibleTag_CB.Size = new System.Drawing.Size(71, 20);
            this.invisibleTag_CB.TabIndex = 1;
            this.invisibleTag_CB.SelectedIndexChanged += new System.EventHandler(this.invisibleTag_CB_SelectedIndexChanged);
            // 
            // newTag_CB
            // 
            this.newTag_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.newTag_CB.Items.Add("Never");
            this.newTag_CB.Items.Add("Immediate");
            this.newTag_CB.Items.Add("Moderated");
            this.newTag_CB.Location = new System.Drawing.Point(95, 22);
            this.newTag_CB.Name = "newTag_CB";
            this.newTag_CB.Size = new System.Drawing.Size(71, 20);
            this.newTag_CB.TabIndex = 0;
            this.newTag_CB.SelectedIndexChanged += new System.EventHandler(this.newTag_CB_SelectedIndexChanged);
            // 
            // stopTriggerType_CB
            // 
            this.stopTriggerType_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopTriggerType_CB.Items.Add("Immediate");
            this.stopTriggerType_CB.Items.Add("Duration");
            this.stopTriggerType_CB.Items.Add("GPI with Timeout");
            this.stopTriggerType_CB.Items.Add("Tag Observation");
            this.stopTriggerType_CB.Items.Add("N Attempts");
            this.stopTriggerType_CB.Items.Add("Handheld Trigger with Timeout");
            this.stopTriggerType_CB.Location = new System.Drawing.Point(95, 11);
            this.stopTriggerType_CB.Name = "stopTriggerType_CB";
            this.stopTriggerType_CB.Size = new System.Drawing.Size(120, 20);
            this.stopTriggerType_CB.TabIndex = 3;
            this.stopTriggerType_CB.SelectedIndexChanged += new System.EventHandler(this.stopTriggerType_CB_SelectedIndexChanged);
            // 
            // stopTriggerTypeLabel
            // 
            this.stopTriggerTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.stopTriggerTypeLabel.Location = new System.Drawing.Point(7, 14);
            this.stopTriggerTypeLabel.Name = "stopTriggerTypeLabel";
            this.stopTriggerTypeLabel.Size = new System.Drawing.Size(82, 17);
            this.stopTriggerTypeLabel.Text = "Trigger Type";
            // 
            // startTriggerType_CB
            // 
            this.startTriggerType_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.startTriggerType_CB.Items.Add("Immediate");
            this.startTriggerType_CB.Items.Add("Periodic");
            this.startTriggerType_CB.Items.Add("GPI");
            this.startTriggerType_CB.Items.Add("Handheld Trigger");
            this.startTriggerType_CB.Location = new System.Drawing.Point(95, 11);
            this.startTriggerType_CB.Name = "startTriggerType_CB";
            this.startTriggerType_CB.Size = new System.Drawing.Size(120, 20);
            this.startTriggerType_CB.TabIndex = 3;
            this.startTriggerType_CB.SelectedIndexChanged += new System.EventHandler(this.startTriggerType_CB_SelectedIndexChanged);
            // 
            // startTriggerTypeLabel
            // 
            this.startTriggerTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.startTriggerTypeLabel.Location = new System.Drawing.Point(7, 14);
            this.startTriggerTypeLabel.Name = "startTriggerTypeLabel";
            this.startTriggerTypeLabel.Size = new System.Drawing.Size(88, 17);
            this.startTriggerTypeLabel.Text = "Trigger Type";
            // 
            // tagReportTriggerTB
            // 
            this.tagReportTriggerTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tagReportTriggerTB.Location = new System.Drawing.Point(100, 164);
            this.tagReportTriggerTB.Name = "tagReportTriggerTB";
            this.tagReportTriggerTB.Size = new System.Drawing.Size(63, 21);
            this.tagReportTriggerTB.TabIndex = 6;
            this.tagReportTriggerTB.Text = "1";
            // 
            // tagReportTriggerLabel
            // 
            this.tagReportTriggerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tagReportTriggerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagReportTriggerLabel.Location = new System.Drawing.Point(0, 166);
            this.tagReportTriggerLabel.Name = "tagReportTriggerLabel";
            this.tagReportTriggerLabel.Size = new System.Drawing.Size(97, 21);
            this.tagReportTriggerLabel.Text = "Tag Report Trigger";
            // 
            // triggerApplyButton
            // 
            this.triggerApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.triggerApplyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.triggerApplyButton.Location = new System.Drawing.Point(186, 165);
            this.triggerApplyButton.Name = "triggerApplyButton";
            this.triggerApplyButton.Size = new System.Drawing.Size(51, 20);
            this.triggerApplyButton.TabIndex = 18;
            this.triggerApplyButton.Text = "Apply";
            this.triggerApplyButton.Click += new System.EventHandler(this.triggerApplyButton_Click);
            // 
            // TriggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.triggerApplyButton);
            this.Controls.Add(this.tagReportTriggerTB);
            this.Controls.Add(this.tagReportTriggerLabel);
            this.Controls.Add(this.tabControl);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "TriggerForm";
            this.Text = "Trigger";
            this.Load += new System.EventHandler(this.TriggerForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.TriggerForm_Closing);
            this.tabControl.ResumeLayout(false);
            this.start_TP.ResumeLayout(false);
            this.stop_TP.ResumeLayout(false);
            this.report_TP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

     
        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ComboBox stopTriggerType_CB;
        private System.Windows.Forms.Label stopTriggerTypeLabel;
        private System.Windows.Forms.TextBox stopDuration_TB;
        private System.Windows.Forms.Label stopDurationLabel;
        private System.Windows.Forms.ComboBox startTriggerType_CB;
        private System.Windows.Forms.Label startTriggerTypeLabel;
        private System.Windows.Forms.DateTimePicker startDateTimePicker;
        private System.Windows.Forms.Label startDateLabel;
        private System.Windows.Forms.Label startPeriodLabel;
        private System.Windows.Forms.TextBox startperiod_TB;
        private System.Windows.Forms.CheckBox startHighLow_CB;
        private System.Windows.Forms.Label startEventLabel;
        private System.Windows.Forms.ComboBox startPort_CB;
        private System.Windows.Forms.Label startPortLabel;
        private System.Windows.Forms.CheckBox startLowHigh_CB;
        private System.Windows.Forms.TextBox tagReportTriggerTB;
        private System.Windows.Forms.Label tagReportTriggerLabel;
        internal System.Windows.Forms.TabPage start_TP;
        internal System.Windows.Forms.TabPage stop_TP;
        private System.Windows.Forms.CheckBox stopLowHigh_CB;
        private System.Windows.Forms.CheckBox stopHighLow_CB;
        private System.Windows.Forms.Label stopEventLabel;
        private System.Windows.Forms.ComboBox stopPort_CB;
        private System.Windows.Forms.Label stopPortLabel;
        private System.Windows.Forms.TextBox stopTimeout_TB;
        private System.Windows.Forms.Label stopTimeoutLabel;
        
        private System.Windows.Forms.TextBox stopTagObservation_TB;
        private System.Windows.Forms.Label stopTagObservationLabel;
        private System.Windows.Forms.TextBox stopTagObservTimeout_TB;
        private System.Windows.Forms.Label stopTagObservTimeoutLabel;
        private System.Windows.Forms.TextBox stopNAttempts_TB;
        private System.Windows.Forms.Label stopNAttemptsLabel;
        private System.Windows.Forms.TextBox stopNAttemptsTimeout_TB;
        private System.Windows.Forms.Label stopNAttemptsTimeoutLabel;
        private System.Windows.Forms.TabPage report_TP;
        private System.Windows.Forms.Button triggerApplyButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox newTag_TB;
        internal System.Windows.Forms.ComboBox backTag_CB;
        internal System.Windows.Forms.ComboBox invisibleTag_CB;
        internal System.Windows.Forms.ComboBox newTag_CB;
        internal System.Windows.Forms.TextBox backTag_TB;
        internal System.Windows.Forms.TextBox invisibleTag_TB;
      
        private System.Windows.Forms.CheckBox startTriggerReleased_CB;
        private System.Windows.Forms.CheckBox startTriggerPressed_CB;
        private System.Windows.Forms.CheckBox stopTriggerPressed_CB;
        private System.Windows.Forms.CheckBox stopTriggerReleased_CB;
        private System.Windows.Forms.TextBox stopTriggerTimeout_TB;
      

    }
}