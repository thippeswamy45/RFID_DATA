namespace CS_RFID3_Host_Sample2
{
    partial class TriggersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.startGroupBox = new System.Windows.Forms.GroupBox();
            this.startTriggerType_CB = new System.Windows.Forms.ComboBox();
            this.triggerTypeLabel = new System.Windows.Forms.Label();
            this.startHandHeldTriggerGroupBox = new System.Windows.Forms.GroupBox();
            this.startTriggerReleased_CB = new System.Windows.Forms.CheckBox();
            this.startTriggerPressed_CB = new System.Windows.Forms.CheckBox();
            this.gpiParamsGroupBox = new System.Windows.Forms.GroupBox();
            this.lowToHigh_CB = new System.Windows.Forms.CheckBox();
            this.highToLow_CB = new System.Windows.Forms.CheckBox();
            this.eventLabel = new System.Windows.Forms.Label();
            this.startPort_CB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.periodicGroupBox = new System.Windows.Forms.GroupBox();
            this.startDate_PK = new System.Windows.Forms.DateTimePicker();
            this.S = new System.Windows.Forms.Label();
            this.startPeriod_TB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GPITimeoutGB = new System.Windows.Forms.GroupBox();
            this.stopTimeout_TB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.stopEventLH_CB = new System.Windows.Forms.CheckBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.stopEventHL_CB = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.stopGPIPort_CB = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nAttemptsGB = new System.Windows.Forms.GroupBox();
            this.stopTriggerNAttTimeOut_TB = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.stopNumAttempt_TB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tagObservationGB = new System.Windows.Forms.GroupBox();
            this.stopTriggerTagOb_TB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.stopTagObservation_TB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.durationGB = new System.Windows.Forms.GroupBox();
            this.stopDuration_TB = new System.Windows.Forms.TextBox();
            this.durationLabel = new System.Windows.Forms.Label();
            this.tagReportTriggerLabel = new System.Windows.Forms.Label();
            this.tagReportTriggerTB = new System.Windows.Forms.TextBox();
            this.antInfoApplyButton = new System.Windows.Forms.Button();
            this.trigger_TB = new System.Windows.Forms.TabControl();
            this.startTrigger_TP = new System.Windows.Forms.TabPage();
            this.stopTrigger_TP = new System.Windows.Forms.TabPage();
            this.stopGroupBox = new System.Windows.Forms.GroupBox();
            this.stopHandHeldTriggerGroupBox = new System.Windows.Forms.GroupBox();
            this.handHeldTriggerTimeout_TB = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.stopTriggerReleased_CB = new System.Windows.Forms.CheckBox();
            this.stopTriggerPressed_CB = new System.Windows.Forms.CheckBox();
            this.stopTriggerType_CB = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.reportTrigger_TP = new System.Windows.Forms.TabPage();
            this.backTag_CB = new System.Windows.Forms.ComboBox();
            this.backTag_TB = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.invisibleTag_CB = new System.Windows.Forms.ComboBox();
            this.invisibleTag_TB = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.newTag_CB = new System.Windows.Forms.ComboBox();
            this.newTag_TB = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.startGroupBox.SuspendLayout();
            this.startHandHeldTriggerGroupBox.SuspendLayout();
            this.gpiParamsGroupBox.SuspendLayout();
            this.periodicGroupBox.SuspendLayout();
            this.GPITimeoutGB.SuspendLayout();
            this.nAttemptsGB.SuspendLayout();
            this.tagObservationGB.SuspendLayout();
            this.durationGB.SuspendLayout();
            this.trigger_TB.SuspendLayout();
            this.startTrigger_TP.SuspendLayout();
            this.stopTrigger_TP.SuspendLayout();
            this.stopGroupBox.SuspendLayout();
            this.stopHandHeldTriggerGroupBox.SuspendLayout();
            this.reportTrigger_TP.SuspendLayout();
            this.SuspendLayout();
            // 
            // startGroupBox
            // 
            this.startGroupBox.Controls.Add(this.startTriggerType_CB);
            this.startGroupBox.Controls.Add(this.triggerTypeLabel);
            this.startGroupBox.Location = new System.Drawing.Point(-4, -10);
            this.startGroupBox.Name = "startGroupBox";
            this.startGroupBox.Size = new System.Drawing.Size(294, 177);
            this.startGroupBox.TabIndex = 0;
            this.startGroupBox.TabStop = false;
            // 
            // startTriggerType_CB
            // 
            this.startTriggerType_CB.FormattingEnabled = true;
            this.startTriggerType_CB.Items.AddRange(new object[] {
            "Immediate",
            "Periodic",
            "GPI",
            "Handheld Trigger"});
            this.startTriggerType_CB.Location = new System.Drawing.Point(136, 17);
            this.startTriggerType_CB.Name = "startTriggerType_CB";
            this.startTriggerType_CB.Size = new System.Drawing.Size(121, 21);
            this.startTriggerType_CB.TabIndex = 1;
            this.startTriggerType_CB.Text = "Immediate";
            this.startTriggerType_CB.SelectedIndexChanged += new System.EventHandler(this.startTriggerComboBox_SelectedIndexChanged);
            // 
            // triggerTypeLabel
            // 
            this.triggerTypeLabel.AutoSize = true;
            this.triggerTypeLabel.Location = new System.Drawing.Point(6, 20);
            this.triggerTypeLabel.Name = "triggerTypeLabel";
            this.triggerTypeLabel.Size = new System.Drawing.Size(67, 13);
            this.triggerTypeLabel.TabIndex = 0;
            this.triggerTypeLabel.Text = "Trigger Type";
            // 
            // startHandHeldTriggerGroupBox
            // 
            this.startHandHeldTriggerGroupBox.Controls.Add(this.startTriggerReleased_CB);
            this.startHandHeldTriggerGroupBox.Controls.Add(this.startTriggerPressed_CB);
            this.startHandHeldTriggerGroupBox.Location = new System.Drawing.Point(11, 51);
            this.startHandHeldTriggerGroupBox.Name = "startHandHeldTriggerGroupBox";
            this.startHandHeldTriggerGroupBox.Size = new System.Drawing.Size(267, 74);
            this.startHandHeldTriggerGroupBox.TabIndex = 1;
            this.startHandHeldTriggerGroupBox.TabStop = false;
            this.startHandHeldTriggerGroupBox.Text = "Handheld Trigger Params";
            // 
            // triggerReleased_CB
            // 
            this.startTriggerReleased_CB.AutoSize = true;
            this.startTriggerReleased_CB.Location = new System.Drawing.Point(125, 43);
            this.startTriggerReleased_CB.Name = "triggerReleased_CB";
            this.startTriggerReleased_CB.Size = new System.Drawing.Size(100, 17);
            this.startTriggerReleased_CB.TabIndex = 1;
            this.startTriggerReleased_CB.Text = "Trigger Released";
            this.startTriggerReleased_CB.UseVisualStyleBackColor = true;
            this.startTriggerReleased_CB.Click += new System.EventHandler(startTriggerReleased_CB_Click);
            // 
            // triggerPressed_CB
            // 
            this.startTriggerPressed_CB.AutoSize = true;
            this.startTriggerPressed_CB.Location = new System.Drawing.Point(125, 20);
            this.startTriggerPressed_CB.Name = "triggerPressed_CB";
            this.startTriggerPressed_CB.Size = new System.Drawing.Size(107, 17);
            this.startTriggerPressed_CB.TabIndex = 0;
            this.startTriggerPressed_CB.Text = "Trigger Pressed";
            this.startTriggerPressed_CB.UseVisualStyleBackColor = true;
            this.startTriggerPressed_CB.Click += new System.EventHandler(startTriggerPressed_CB_Click);
            // 
            // gpiParamsGroupBox
            // 
            this.gpiParamsGroupBox.Controls.Add(this.lowToHigh_CB);
            this.gpiParamsGroupBox.Controls.Add(this.highToLow_CB);
            this.gpiParamsGroupBox.Controls.Add(this.eventLabel);
            this.gpiParamsGroupBox.Controls.Add(this.startPort_CB);
            this.gpiParamsGroupBox.Controls.Add(this.label1);
            this.gpiParamsGroupBox.Location = new System.Drawing.Point(9, 42);
            this.gpiParamsGroupBox.Name = "gpiParamsGroupBox";
            this.gpiParamsGroupBox.Size = new System.Drawing.Size(248, 101);
            this.gpiParamsGroupBox.TabIndex = 2;
            this.gpiParamsGroupBox.TabStop = false;
            this.gpiParamsGroupBox.Text = "GPI Params";
            // 
            // lowToHigh_CB
            // 
            this.lowToHigh_CB.AutoSize = true;
            this.lowToHigh_CB.Location = new System.Drawing.Point(155, 53);
            this.lowToHigh_CB.Name = "lowToHigh_CB";
            this.lowToHigh_CB.Size = new System.Drawing.Size(87, 17);
            this.lowToHigh_CB.TabIndex = 5;
            this.lowToHigh_CB.Text = "Low To High";
            this.lowToHigh_CB.UseVisualStyleBackColor = true;
            // 
            // highToLow_CB
            // 
            this.highToLow_CB.AutoSize = true;
            this.highToLow_CB.Location = new System.Drawing.Point(62, 53);
            this.highToLow_CB.Name = "highToLow_CB";
            this.highToLow_CB.Size = new System.Drawing.Size(87, 17);
            this.highToLow_CB.TabIndex = 4;
            this.highToLow_CB.Text = "High To Low";
            this.highToLow_CB.UseVisualStyleBackColor = true;
            // 
            // eventLabel
            // 
            this.eventLabel.AutoSize = true;
            this.eventLabel.Location = new System.Drawing.Point(15, 53);
            this.eventLabel.Name = "eventLabel";
            this.eventLabel.Size = new System.Drawing.Size(35, 13);
            this.eventLabel.TabIndex = 3;
            this.eventLabel.Text = "Event";
            // 
            // startPort_CB
            // 
            this.startPort_CB.FormattingEnabled = true;
            this.startPort_CB.Location = new System.Drawing.Point(127, 19);
            this.startPort_CB.Name = "startPort_CB";
            this.startPort_CB.Size = new System.Drawing.Size(115, 21);
            this.startPort_CB.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // periodicGroupBox
            // 
            this.periodicGroupBox.Controls.Add(this.startDate_PK);
            this.periodicGroupBox.Controls.Add(this.S);
            this.periodicGroupBox.Controls.Add(this.startPeriod_TB);
            this.periodicGroupBox.Controls.Add(this.label3);
            this.periodicGroupBox.Location = new System.Drawing.Point(9, 44);
            this.periodicGroupBox.Name = "periodicGroupBox";
            this.periodicGroupBox.Size = new System.Drawing.Size(248, 100);
            this.periodicGroupBox.TabIndex = 3;
            this.periodicGroupBox.TabStop = false;
            this.periodicGroupBox.Text = "Periodic Params";
            // 
            // startDate_PK
            // 
            this.startDate_PK.CustomFormat = "MMM/dd/yyyy  hh:mm:ss tt";
            this.startDate_PK.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDate_PK.Location = new System.Drawing.Point(83, 19);
            this.startDate_PK.Name = "startDate_PK";
            this.startDate_PK.Size = new System.Drawing.Size(159, 20);
            this.startDate_PK.TabIndex = 7;
            // 
            // S
            // 
            this.S.Location = new System.Drawing.Point(12, 23);
            this.S.Name = "S";
            this.S.Size = new System.Drawing.Size(65, 31);
            this.S.TabIndex = 3;
            this.S.Text = "Start Date";
            // 
            // startPeriod_TB
            // 
            this.startPeriod_TB.Location = new System.Drawing.Point(83, 51);
            this.startPeriod_TB.Name = "startPeriod_TB";
            this.startPeriod_TB.Size = new System.Drawing.Size(159, 20);
            this.startPeriod_TB.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Period";
            // 
            // GPITimeoutGB
            // 
            this.GPITimeoutGB.Controls.Add(this.stopTimeout_TB);
            this.GPITimeoutGB.Controls.Add(this.label2);
            this.GPITimeoutGB.Controls.Add(this.stopEventLH_CB);
            this.GPITimeoutGB.Controls.Add(this.textBox6);
            this.GPITimeoutGB.Controls.Add(this.label8);
            this.GPITimeoutGB.Controls.Add(this.stopEventHL_CB);
            this.GPITimeoutGB.Controls.Add(this.label7);
            this.GPITimeoutGB.Controls.Add(this.stopGPIPort_CB);
            this.GPITimeoutGB.Controls.Add(this.label6);
            this.GPITimeoutGB.Location = new System.Drawing.Point(9, 44);
            this.GPITimeoutGB.Name = "GPITimeoutGB";
            this.GPITimeoutGB.Size = new System.Drawing.Size(248, 100);
            this.GPITimeoutGB.TabIndex = 2;
            this.GPITimeoutGB.TabStop = false;
            this.GPITimeoutGB.Text = "GPI with Timeout Params";
            // 
            // stopTimeout_TB
            // 
            this.stopTimeout_TB.Location = new System.Drawing.Point(130, 46);
            this.stopTimeout_TB.Name = "stopTimeout_TB";
            this.stopTimeout_TB.Size = new System.Drawing.Size(110, 20);
            this.stopTimeout_TB.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Time Out";
            // 
            // stopEventLH_CB
            // 
            this.stopEventLH_CB.AutoSize = true;
            this.stopEventLH_CB.Location = new System.Drawing.Point(155, 77);
            this.stopEventLH_CB.Name = "stopEventLH_CB";
            this.stopEventLH_CB.Size = new System.Drawing.Size(87, 17);
            this.stopEventLH_CB.TabIndex = 7;
            this.stopEventLH_CB.Text = "Low To High";
            this.stopEventLH_CB.UseVisualStyleBackColor = true;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(98, 106);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(85, 20);
            this.textBox6.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Timeout";
            // 
            // stopEventHL_CB
            // 
            this.stopEventHL_CB.AutoSize = true;
            this.stopEventHL_CB.Location = new System.Drawing.Point(62, 77);
            this.stopEventHL_CB.Name = "stopEventHL_CB";
            this.stopEventHL_CB.Size = new System.Drawing.Size(87, 17);
            this.stopEventHL_CB.TabIndex = 4;
            this.stopEventHL_CB.Text = "High To Low";
            this.stopEventHL_CB.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Event";
            // 
            // stopGPIPort_CB
            // 
            this.stopGPIPort_CB.FormattingEnabled = true;
            this.stopGPIPort_CB.Location = new System.Drawing.Point(130, 14);
            this.stopGPIPort_CB.Name = "stopGPIPort_CB";
            this.stopGPIPort_CB.Size = new System.Drawing.Size(110, 21);
            this.stopGPIPort_CB.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Port";
            // 
            // nAttemptsGB
            // 
            this.nAttemptsGB.Controls.Add(this.stopTriggerNAttTimeOut_TB);
            this.nAttemptsGB.Controls.Add(this.label11);
            this.nAttemptsGB.Controls.Add(this.stopNumAttempt_TB);
            this.nAttemptsGB.Controls.Add(this.label10);
            this.nAttemptsGB.Location = new System.Drawing.Point(9, 44);
            this.nAttemptsGB.Name = "nAttemptsGB";
            this.nAttemptsGB.Size = new System.Drawing.Size(245, 100);
            this.nAttemptsGB.TabIndex = 2;
            this.nAttemptsGB.TabStop = false;
            this.nAttemptsGB.Text = "N Attempts Params";
            // 
            // stopTriggerNAttTimeOut_TB
            // 
            this.stopTriggerNAttTimeOut_TB.Location = new System.Drawing.Point(124, 56);
            this.stopTriggerNAttTimeOut_TB.Name = "stopTriggerNAttTimeOut_TB";
            this.stopTriggerNAttTimeOut_TB.Size = new System.Drawing.Size(115, 20);
            this.stopTriggerNAttTimeOut_TB.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Time Out";
            // 
            // stopNumAttempt_TB
            // 
            this.stopNumAttempt_TB.Location = new System.Drawing.Point(124, 23);
            this.stopNumAttempt_TB.Name = "stopNumAttempt_TB";
            this.stopNumAttempt_TB.Size = new System.Drawing.Size(115, 20);
            this.stopNumAttempt_TB.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "No. of Attempts";
            // 
            // tagObservationGB
            // 
            this.tagObservationGB.Controls.Add(this.stopTriggerTagOb_TB);
            this.tagObservationGB.Controls.Add(this.label4);
            this.tagObservationGB.Controls.Add(this.stopTagObservation_TB);
            this.tagObservationGB.Controls.Add(this.label9);
            this.tagObservationGB.Location = new System.Drawing.Point(9, 44);
            this.tagObservationGB.Name = "tagObservationGB";
            this.tagObservationGB.Size = new System.Drawing.Size(245, 100);
            this.tagObservationGB.TabIndex = 2;
            this.tagObservationGB.TabStop = false;
            this.tagObservationGB.Text = "Tag Observation Params";
            // 
            // stopTriggerTagOb_TB
            // 
            this.stopTriggerTagOb_TB.Location = new System.Drawing.Point(124, 55);
            this.stopTriggerTagOb_TB.Name = "stopTriggerTagOb_TB";
            this.stopTriggerTagOb_TB.Size = new System.Drawing.Size(115, 20);
            this.stopTriggerTagOb_TB.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Time Out";
            // 
            // stopTagObservation_TB
            // 
            this.stopTagObservation_TB.Location = new System.Drawing.Point(124, 24);
            this.stopTagObservation_TB.Name = "stopTagObservation_TB";
            this.stopTagObservation_TB.Size = new System.Drawing.Size(115, 20);
            this.stopTagObservation_TB.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Tag Observation";
            // 
            // durationGB
            // 
            this.durationGB.Controls.Add(this.stopDuration_TB);
            this.durationGB.Controls.Add(this.durationLabel);
            this.durationGB.Location = new System.Drawing.Point(9, 44);
            this.durationGB.Name = "durationGB";
            this.durationGB.Size = new System.Drawing.Size(245, 100);
            this.durationGB.TabIndex = 2;
            this.durationGB.TabStop = false;
            this.durationGB.Text = "Duration Params";
            // 
            // stopDuration_TB
            // 
            this.stopDuration_TB.Location = new System.Drawing.Point(124, 24);
            this.stopDuration_TB.Name = "stopDuration_TB";
            this.stopDuration_TB.Size = new System.Drawing.Size(115, 20);
            this.stopDuration_TB.TabIndex = 1;
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.Location = new System.Drawing.Point(9, 27);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(69, 13);
            this.durationLabel.TabIndex = 0;
            this.durationLabel.Text = "Duration (ms)";
            // 
            // tagReportTriggerLabel
            // 
            this.tagReportTriggerLabel.AutoSize = true;
            this.tagReportTriggerLabel.Location = new System.Drawing.Point(16, 210);
            this.tagReportTriggerLabel.Name = "tagReportTriggerLabel";
            this.tagReportTriggerLabel.Size = new System.Drawing.Size(97, 13);
            this.tagReportTriggerLabel.TabIndex = 2;
            this.tagReportTriggerLabel.Text = "Tag Report Trigger";
            // 
            // tagReportTriggerTB
            // 
            this.tagReportTriggerTB.Location = new System.Drawing.Point(119, 207);
            this.tagReportTriggerTB.Name = "tagReportTriggerTB";
            this.tagReportTriggerTB.Size = new System.Drawing.Size(60, 20);
            this.tagReportTriggerTB.TabIndex = 3;
            this.tagReportTriggerTB.Text = "1";
            // 
            // antInfoApplyButton
            // 
            this.antInfoApplyButton.Location = new System.Drawing.Point(215, 207);
            this.antInfoApplyButton.Name = "antInfoApplyButton";
            this.antInfoApplyButton.Size = new System.Drawing.Size(81, 22);
            this.antInfoApplyButton.TabIndex = 39;
            this.antInfoApplyButton.Text = "Apply";
            this.antInfoApplyButton.UseVisualStyleBackColor = true;
            this.antInfoApplyButton.Click += new System.EventHandler(this.antInfoApplyButton_Click);
            // 
            // trigger_TB
            // 
            this.trigger_TB.Controls.Add(this.startTrigger_TP);
            this.trigger_TB.Controls.Add(this.stopTrigger_TP);
            this.trigger_TB.Controls.Add(this.reportTrigger_TP);
            this.trigger_TB.Location = new System.Drawing.Point(12, 12);
            this.trigger_TB.Name = "trigger_TB";
            this.trigger_TB.SelectedIndex = 0;
            this.trigger_TB.Size = new System.Drawing.Size(288, 182);
            this.trigger_TB.TabIndex = 40;
            // 
            // startTrigger_TP
            // 
            this.startTrigger_TP.Controls.Add(this.startGroupBox);
            this.startTrigger_TP.Location = new System.Drawing.Point(4, 22);
            this.startTrigger_TP.Name = "startTrigger_TP";
            this.startTrigger_TP.Padding = new System.Windows.Forms.Padding(3);
            this.startTrigger_TP.Size = new System.Drawing.Size(280, 156);
            this.startTrigger_TP.TabIndex = 0;
            this.startTrigger_TP.Text = "Start Trigger";
            this.startTrigger_TP.UseVisualStyleBackColor = true;
            // 
            // stopTrigger_TP
            // 
            this.stopTrigger_TP.Controls.Add(this.stopGroupBox);
            this.stopTrigger_TP.Location = new System.Drawing.Point(4, 22);
            this.stopTrigger_TP.Name = "stopTrigger_TP";
            this.stopTrigger_TP.Padding = new System.Windows.Forms.Padding(3);
            this.stopTrigger_TP.Size = new System.Drawing.Size(280, 156);
            this.stopTrigger_TP.TabIndex = 1;
            this.stopTrigger_TP.Text = "Stop Trigger";
            this.stopTrigger_TP.UseVisualStyleBackColor = true;
            // 
            // stopGroupBox
            // 
            this.stopGroupBox.Controls.Add(this.stopTriggerType_CB);
            this.stopGroupBox.Controls.Add(this.label5);
            this.stopGroupBox.Location = new System.Drawing.Point(-4, -10);
            this.stopGroupBox.Name = "stopGroupBox";
            this.stopGroupBox.Size = new System.Drawing.Size(294, 177);
            this.stopGroupBox.TabIndex = 1;
            this.stopGroupBox.TabStop = false;
            this.stopGroupBox.Enter += new System.EventHandler(this.stopGroupBox_Enter);
            // 
            // stopHandHeldTriggerGroupBox
            // 
            this.stopHandHeldTriggerGroupBox.Controls.Add(this.handHeldTriggerTimeout_TB);
            this.stopHandHeldTriggerGroupBox.Controls.Add(this.label15);
            this.stopHandHeldTriggerGroupBox.Controls.Add(this.stopTriggerReleased_CB);
            this.stopHandHeldTriggerGroupBox.Controls.Add(this.stopTriggerPressed_CB);
            this.stopHandHeldTriggerGroupBox.Location = new System.Drawing.Point(11, 51);
            this.stopHandHeldTriggerGroupBox.Name = "stopHandHeldTriggerGroupBox";
            this.stopHandHeldTriggerGroupBox.Size = new System.Drawing.Size(267, 74);
            this.stopHandHeldTriggerGroupBox.TabIndex = 3;
            this.stopHandHeldTriggerGroupBox.TabStop = false;
            this.stopHandHeldTriggerGroupBox.Text = "Handheld Trigger with Timeout Params";
            // 
            // textBox1
            // 
            this.handHeldTriggerTimeout_TB.Location = new System.Drawing.Point(9, 37);
            this.handHeldTriggerTimeout_TB.Name = "textBox1";
            this.handHeldTriggerTimeout_TB.Size = new System.Drawing.Size(100, 20);
            this.handHeldTriggerTimeout_TB.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Timeout (ms)";
            // 
            // stopTriggerReleased
            // 
            this.stopTriggerReleased_CB.AutoSize = true;
            this.stopTriggerReleased_CB.Location = new System.Drawing.Point(125, 43);
            this.stopTriggerReleased_CB.Name = "stopTriggerReleased";
            this.stopTriggerReleased_CB.Size = new System.Drawing.Size(100, 17);
            this.stopTriggerReleased_CB.TabIndex = 1;
            this.stopTriggerReleased_CB.Text = "Trigger Released";
            this.stopTriggerReleased_CB.UseVisualStyleBackColor = true;
            this.stopTriggerReleased_CB.Click += new System.EventHandler(stopTriggerReleased_CB_Click);
            // 
            // stopTriggerPressed
            // 
            this.stopTriggerPressed_CB.AutoSize = true;
            this.stopTriggerPressed_CB.Location = new System.Drawing.Point(125, 20);
            this.stopTriggerPressed_CB.Name = "stopTriggerPressed";
            this.stopTriggerPressed_CB.Size = new System.Drawing.Size(107, 17);
            this.stopTriggerPressed_CB.TabIndex = 0;
            this.stopTriggerPressed_CB.Text = "Trigger Pressed";
            this.stopTriggerPressed_CB.UseVisualStyleBackColor = true;
            this.stopTriggerPressed_CB.Click += new System.EventHandler(stopTriggerPressed_CB_Click);
            // 
            // stopTriggerType_CB
            // 
            this.stopTriggerType_CB.FormattingEnabled = true;
            this.stopTriggerType_CB.Items.AddRange(new object[] {
            "Immediate",
            "Duration",
            "GPI with Timeout",
            "Tag Observation",
            "N Attempts",
            "Handheld Trigger with Timeout"});
            this.stopTriggerType_CB.Location = new System.Drawing.Point(136, 17);
            this.stopTriggerType_CB.Name = "stopTriggerType_CB";
            this.stopTriggerType_CB.Size = new System.Drawing.Size(121, 21);
            this.stopTriggerType_CB.TabIndex = 1;
            this.stopTriggerType_CB.Text = "Immediate";
            this.stopTriggerType_CB.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Trigger Type";
            // 
            // reportTrigger_TP
            // 
            this.reportTrigger_TP.Controls.Add(this.backTag_CB);
            this.reportTrigger_TP.Controls.Add(this.backTag_TB);
            this.reportTrigger_TP.Controls.Add(this.label14);
            this.reportTrigger_TP.Controls.Add(this.invisibleTag_CB);
            this.reportTrigger_TP.Controls.Add(this.invisibleTag_TB);
            this.reportTrigger_TP.Controls.Add(this.label13);
            this.reportTrigger_TP.Controls.Add(this.newTag_CB);
            this.reportTrigger_TP.Controls.Add(this.newTag_TB);
            this.reportTrigger_TP.Controls.Add(this.label12);
            this.reportTrigger_TP.Location = new System.Drawing.Point(4, 22);
            this.reportTrigger_TP.Name = "reportTrigger_TP";
            this.reportTrigger_TP.Padding = new System.Windows.Forms.Padding(3);
            this.reportTrigger_TP.Size = new System.Drawing.Size(280, 156);
            this.reportTrigger_TP.TabIndex = 2;
            this.reportTrigger_TP.Text = "Report Trigger";
            this.reportTrigger_TP.UseVisualStyleBackColor = true;
            // 
            // backTag_CB
            // 
            this.backTag_CB.FormattingEnabled = true;
            this.backTag_CB.Items.AddRange(new object[] {
            "Never",
            "Immediate",
            "Moderated"});
            this.backTag_CB.Location = new System.Drawing.Point(111, 111);
            this.backTag_CB.Name = "backTag_CB";
            this.backTag_CB.Size = new System.Drawing.Size(84, 21);
            this.backTag_CB.TabIndex = 8;
            this.backTag_CB.SelectedIndexChanged += new System.EventHandler(this.backTag_CB_SelectedIndexChanged);
            // 
            // backTag_TB
            // 
            this.backTag_TB.Location = new System.Drawing.Point(210, 112);
            this.backTag_TB.Name = "backTag_TB";
            this.backTag_TB.Size = new System.Drawing.Size(63, 20);
            this.backTag_TB.TabIndex = 7;
            this.backTag_TB.Text = "500";
            this.backTag_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 115);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(104, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "Tab Back to visibility";
            // 
            // invisibleTag_CB
            // 
            this.invisibleTag_CB.FormattingEnabled = true;
            this.invisibleTag_CB.Items.AddRange(new object[] {
            "Never",
            "Immediate",
            "Moderated"});
            this.invisibleTag_CB.Location = new System.Drawing.Point(112, 63);
            this.invisibleTag_CB.Name = "invisibleTag_CB";
            this.invisibleTag_CB.Size = new System.Drawing.Size(84, 21);
            this.invisibleTag_CB.TabIndex = 5;
            this.invisibleTag_CB.SelectedIndexChanged += new System.EventHandler(this.invisibleTag_CB_SelectedIndexChanged);
            // 
            // invisibleTag_TB
            // 
            this.invisibleTag_TB.Location = new System.Drawing.Point(210, 64);
            this.invisibleTag_TB.Name = "invisibleTag_TB";
            this.invisibleTag_TB.Size = new System.Drawing.Size(63, 20);
            this.invisibleTag_TB.TabIndex = 4;
            this.invisibleTag_TB.Text = "500";
            this.invisibleTag_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Tag Invisible";
            // 
            // newTag_CB
            // 
            this.newTag_CB.FormattingEnabled = true;
            this.newTag_CB.Items.AddRange(new object[] {
            "Never",
            "Immediate",
            "Moderated"});
            this.newTag_CB.Location = new System.Drawing.Point(112, 15);
            this.newTag_CB.Name = "newTag_CB";
            this.newTag_CB.Size = new System.Drawing.Size(84, 21);
            this.newTag_CB.TabIndex = 2;
            this.newTag_CB.SelectedIndexChanged += new System.EventHandler(this.newTag_CB_SelectedIndexChanged);
            // 
            // newTag_TB
            // 
            this.newTag_TB.Location = new System.Drawing.Point(211, 16);
            this.newTag_TB.Name = "newTag_TB";
            this.newTag_TB.Size = new System.Drawing.Size(63, 20);
            this.newTag_TB.TabIndex = 1;
            this.newTag_TB.Text = "500";
            this.newTag_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "New Tag";
            // 
            // TriggersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 244);
            this.Controls.Add(this.trigger_TB);
            this.Controls.Add(this.antInfoApplyButton);
            this.Controls.Add(this.tagReportTriggerTB);
            this.Controls.Add(this.tagReportTriggerLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TriggersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Triggers";
            this.Load += new System.EventHandler(this.Triggers_Load);
            this.startGroupBox.ResumeLayout(false);
            this.startGroupBox.PerformLayout();
            this.startHandHeldTriggerGroupBox.ResumeLayout(false);
            this.startHandHeldTriggerGroupBox.PerformLayout();
            this.gpiParamsGroupBox.ResumeLayout(false);
            this.gpiParamsGroupBox.PerformLayout();
            this.periodicGroupBox.ResumeLayout(false);
            this.periodicGroupBox.PerformLayout();
            this.GPITimeoutGB.ResumeLayout(false);
            this.GPITimeoutGB.PerformLayout();
            this.nAttemptsGB.ResumeLayout(false);
            this.nAttemptsGB.PerformLayout();
            this.tagObservationGB.ResumeLayout(false);
            this.tagObservationGB.PerformLayout();
            this.durationGB.ResumeLayout(false);
            this.durationGB.PerformLayout();
            this.trigger_TB.ResumeLayout(false);
            this.startTrigger_TP.ResumeLayout(false);
            this.stopTrigger_TP.ResumeLayout(false);
            this.stopGroupBox.ResumeLayout(false);
            this.stopGroupBox.PerformLayout();
            this.stopHandHeldTriggerGroupBox.ResumeLayout(false);
            this.stopHandHeldTriggerGroupBox.PerformLayout();
            this.reportTrigger_TP.ResumeLayout(false);
            this.reportTrigger_TP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    
        #endregion

        private System.Windows.Forms.GroupBox startGroupBox;
        private System.Windows.Forms.Label triggerTypeLabel;
        private System.Windows.Forms.ComboBox startTriggerType_CB;
        private System.Windows.Forms.GroupBox gpiParamsGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox startPort_CB;
        private System.Windows.Forms.CheckBox lowToHigh_CB;
        private System.Windows.Forms.CheckBox highToLow_CB;
        private System.Windows.Forms.Label eventLabel;
        private System.Windows.Forms.GroupBox periodicGroupBox;
        private System.Windows.Forms.Label S;
        private System.Windows.Forms.TextBox startPeriod_TB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label tagReportTriggerLabel;
        private System.Windows.Forms.TextBox tagReportTriggerTB;
        private System.Windows.Forms.Button antInfoApplyButton;
        private System.Windows.Forms.GroupBox durationGB;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.TextBox stopDuration_TB;
        private System.Windows.Forms.GroupBox GPITimeoutGB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox stopEventHL_CB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox stopGPIPort_CB;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.CheckBox stopEventLH_CB;
        private System.Windows.Forms.GroupBox tagObservationGB;
        private System.Windows.Forms.TextBox stopTagObservation_TB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox nAttemptsGB;
        private System.Windows.Forms.TextBox stopNumAttempt_TB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker startDate_PK;
        private System.Windows.Forms.TextBox stopTimeout_TB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox stopTriggerTagOb_TB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox stopTriggerNAttTimeOut_TB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabControl trigger_TB;
        private System.Windows.Forms.TabPage startTrigger_TP;
        private System.Windows.Forms.TabPage stopTrigger_TP;
        private System.Windows.Forms.TabPage reportTrigger_TP;
        private System.Windows.Forms.GroupBox stopGroupBox;
        private System.Windows.Forms.ComboBox stopTriggerType_CB;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.ComboBox invisibleTag_CB;
        internal System.Windows.Forms.TextBox invisibleTag_TB;
        private System.Windows.Forms.Label label13;
        internal System.Windows.Forms.ComboBox newTag_CB;
        internal System.Windows.Forms.TextBox newTag_TB;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.ComboBox backTag_CB;
        internal System.Windows.Forms.TextBox backTag_TB;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox startHandHeldTriggerGroupBox;
        private System.Windows.Forms.CheckBox startTriggerReleased_CB;
        private System.Windows.Forms.CheckBox startTriggerPressed_CB;
        private System.Windows.Forms.GroupBox stopHandHeldTriggerGroupBox;
        private System.Windows.Forms.TextBox handHeldTriggerTimeout_TB;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox stopTriggerReleased_CB;
        private System.Windows.Forms.CheckBox stopTriggerPressed_CB;
    }
}