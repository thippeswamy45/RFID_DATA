namespace CS_RFID3Sample6
{
    partial class PreFilterForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.filter1_TP = new System.Windows.Forms.TabPage();
            this.tagMaskLabel1 = new System.Windows.Forms.Label();
            this.filter_CB1 = new System.Windows.Forms.CheckBox();
            this.antennaID_CB1 = new System.Windows.Forms.ComboBox();
            this.antIDLabel1 = new System.Windows.Forms.Label();
            this.target_CB1 = new System.Windows.Forms.ComboBox();
            this.targetLabel1 = new System.Windows.Forms.Label();
            this.action_CB1 = new System.Windows.Forms.ComboBox();
            this.actionLabel1 = new System.Windows.Forms.Label();
            this.filterAction_CB1 = new System.Windows.Forms.ComboBox();
            this.filterActionLabel1 = new System.Windows.Forms.Label();
            this.tagMask_TB1 = new System.Windows.Forms.TextBox();
            this.offset_TB1 = new System.Windows.Forms.TextBox();
            this.offsetLabel1 = new System.Windows.Forms.Label();
            this.memBank_CB1 = new System.Windows.Forms.ComboBox();
            this.memBankLabel1 = new System.Windows.Forms.Label();
            this.filter2_TP = new System.Windows.Forms.TabPage();
            this.filter_CB2 = new System.Windows.Forms.CheckBox();
            this.antennaID_CB2 = new System.Windows.Forms.ComboBox();
            this.antIDLabel2 = new System.Windows.Forms.Label();
            this.target_CB2 = new System.Windows.Forms.ComboBox();
            this.targetLabel2 = new System.Windows.Forms.Label();
            this.action_CB2 = new System.Windows.Forms.ComboBox();
            this.actionLabel2 = new System.Windows.Forms.Label();
            this.filterAction_CB2 = new System.Windows.Forms.ComboBox();
            this.filterActionLabel2 = new System.Windows.Forms.Label();
            this.tagMask_TB2 = new System.Windows.Forms.TextBox();
            this.tagMaskLabel2 = new System.Windows.Forms.Label();
            this.offset_TB2 = new System.Windows.Forms.TextBox();
            this.offsetLabel2 = new System.Windows.Forms.Label();
            this.memBank_CB2 = new System.Windows.Forms.ComboBox();
            this.memBankLabel2 = new System.Windows.Forms.Label();
            this.presFilterButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.filter1_TP.SuspendLayout();
            this.filter2_TP.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.filter1_TP);
            this.tabControl1.Controls.Add(this.filter2_TP);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 159);
            this.tabControl1.TabIndex = 0;
            // 
            // filter1_TP
            // 
            this.filter1_TP.Controls.Add(this.tagMaskLabel1);
            this.filter1_TP.Controls.Add(this.filter_CB1);
            this.filter1_TP.Controls.Add(this.antennaID_CB1);
            this.filter1_TP.Controls.Add(this.antIDLabel1);
            this.filter1_TP.Controls.Add(this.target_CB1);
            this.filter1_TP.Controls.Add(this.targetLabel1);
            this.filter1_TP.Controls.Add(this.action_CB1);
            this.filter1_TP.Controls.Add(this.actionLabel1);
            this.filter1_TP.Controls.Add(this.filterAction_CB1);
            this.filter1_TP.Controls.Add(this.filterActionLabel1);
            this.filter1_TP.Controls.Add(this.tagMask_TB1);
            this.filter1_TP.Controls.Add(this.offset_TB1);
            this.filter1_TP.Controls.Add(this.offsetLabel1);
            this.filter1_TP.Controls.Add(this.memBank_CB1);
            this.filter1_TP.Controls.Add(this.memBankLabel1);
            this.filter1_TP.Location = new System.Drawing.Point(0, 0);
            this.filter1_TP.Name = "filter1_TP";
            this.filter1_TP.Size = new System.Drawing.Size(240, 136);
            this.filter1_TP.Text = "Filter 1";
            // 
            // tagMaskLabel1
            // 
            this.tagMaskLabel1.Enabled = false;
            this.tagMaskLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagMaskLabel1.Location = new System.Drawing.Point(7, 58);
            this.tagMaskLabel1.Name = "tagMaskLabel1";
            this.tagMaskLabel1.Size = new System.Drawing.Size(72, 25);
            this.tagMaskLabel1.Text = "Tag Pattern (Hex)";
            // 
            // filter_CB1
            // 
            this.filter_CB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.filter_CB1.Location = new System.Drawing.Point(143, 5);
            this.filter_CB1.Name = "filter_CB1";
            this.filter_CB1.Size = new System.Drawing.Size(87, 20);
            this.filter_CB1.TabIndex = 1;
            this.filter_CB1.Text = "Use Filter 1";
            this.filter_CB1.CheckStateChanged += new System.EventHandler(this.filter_CB1_CheckStateChanged);
            // 
            // antennaID_CB1
            // 
            this.antennaID_CB1.Enabled = false;
            this.antennaID_CB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.antennaID_CB1.ForeColor = System.Drawing.Color.Navy;
            this.antennaID_CB1.Location = new System.Drawing.Point(82, 5);
            this.antennaID_CB1.Name = "antennaID_CB1";
            this.antennaID_CB1.Size = new System.Drawing.Size(40, 20);
            this.antennaID_CB1.TabIndex = 2;
            // 
            // antIDLabel1
            // 
            this.antIDLabel1.Enabled = false;
            this.antIDLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.antIDLabel1.Location = new System.Drawing.Point(7, 10);
            this.antIDLabel1.Name = "antIDLabel1";
            this.antIDLabel1.Size = new System.Drawing.Size(61, 19);
            this.antIDLabel1.Text = "Antenna ID";
            // 
            // target_CB1
            // 
            this.target_CB1.Enabled = false;
            this.target_CB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.target_CB1.ForeColor = System.Drawing.Color.Navy;
            this.target_CB1.Items.Add("S0");
            this.target_CB1.Items.Add("S1");
            this.target_CB1.Items.Add("S2");
            this.target_CB1.Items.Add("S3");
            this.target_CB1.Location = new System.Drawing.Point(197, 108);
            this.target_CB1.Name = "target_CB1";
            this.target_CB1.Size = new System.Drawing.Size(33, 20);
            this.target_CB1.TabIndex = 8;
            // 
            // targetLabel1
            // 
            this.targetLabel1.Enabled = false;
            this.targetLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.targetLabel1.Location = new System.Drawing.Point(155, 111);
            this.targetLabel1.Name = "targetLabel1";
            this.targetLabel1.Size = new System.Drawing.Size(36, 13);
            this.targetLabel1.Text = "Target";
            // 
            // action_CB1
            // 
            this.action_CB1.Enabled = false;
            this.action_CB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.action_CB1.ForeColor = System.Drawing.Color.Navy;
            this.action_CB1.Location = new System.Drawing.Point(45, 108);
            this.action_CB1.Name = "action_CB1";
            this.action_CB1.Size = new System.Drawing.Size(110, 20);
            this.action_CB1.TabIndex = 7;
            // 
            // actionLabel1
            // 
            this.actionLabel1.Enabled = false;
            this.actionLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.actionLabel1.Location = new System.Drawing.Point(7, 113);
            this.actionLabel1.Name = "actionLabel1";
            this.actionLabel1.Size = new System.Drawing.Size(37, 13);
            this.actionLabel1.Text = "Action";
            // 
            // filterAction_CB1
            // 
            this.filterAction_CB1.Enabled = false;
            this.filterAction_CB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.filterAction_CB1.ForeColor = System.Drawing.Color.Navy;
            this.filterAction_CB1.Items.Add("DEFAULT");
            this.filterAction_CB1.Items.Add("STATE AWARE");
            this.filterAction_CB1.Items.Add("STATE UNAWARE");
            this.filterAction_CB1.Location = new System.Drawing.Point(89, 82);
            this.filterAction_CB1.Name = "filterAction_CB1";
            this.filterAction_CB1.Size = new System.Drawing.Size(141, 20);
            this.filterAction_CB1.TabIndex = 6;
            this.filterAction_CB1.SelectedIndexChanged += new System.EventHandler(this.filterAction_CB1_SelectedIndexChanged);
            // 
            // filterActionLabel1
            // 
            this.filterActionLabel1.Enabled = false;
            this.filterActionLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.filterActionLabel1.Location = new System.Drawing.Point(7, 85);
            this.filterActionLabel1.Name = "filterActionLabel1";
            this.filterActionLabel1.Size = new System.Drawing.Size(62, 13);
            this.filterActionLabel1.Text = "Filter Action";
            // 
            // tagMask_TB1
            // 
            this.tagMask_TB1.Enabled = false;
            this.tagMask_TB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagMask_TB1.Location = new System.Drawing.Point(89, 57);
            this.tagMask_TB1.Name = "tagMask_TB1";
            this.tagMask_TB1.Size = new System.Drawing.Size(141, 19);
            this.tagMask_TB1.TabIndex = 5;
            // 
            // offset_TB1
            // 
            this.offset_TB1.Enabled = false;
            this.offset_TB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.offset_TB1.Location = new System.Drawing.Point(201, 33);
            this.offset_TB1.Name = "offset_TB1";
            this.offset_TB1.Size = new System.Drawing.Size(29, 19);
            this.offset_TB1.TabIndex = 4;
            this.offset_TB1.Text = "32";
            // 
            // offsetLabel1
            // 
            this.offsetLabel1.Enabled = false;
            this.offsetLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.offsetLabel1.Location = new System.Drawing.Point(128, 33);
            this.offsetLabel1.Name = "offsetLabel1";
            this.offsetLabel1.Size = new System.Drawing.Size(67, 13);
            this.offsetLabel1.Text = "Offset (Bits)";
            // 
            // memBank_CB1
            // 
            this.memBank_CB1.Enabled = false;
            this.memBank_CB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.memBank_CB1.ForeColor = System.Drawing.Color.Navy;
            this.memBank_CB1.Items.Add("EPC");
            this.memBank_CB1.Items.Add("TID");
            this.memBank_CB1.Items.Add("USER");
            this.memBank_CB1.Location = new System.Drawing.Point(82, 33);
            this.memBank_CB1.Name = "memBank_CB1";
            this.memBank_CB1.Size = new System.Drawing.Size(40, 20);
            this.memBank_CB1.TabIndex = 3;
            // 
            // memBankLabel1
            // 
            this.memBankLabel1.Enabled = false;
            this.memBankLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.memBankLabel1.Location = new System.Drawing.Point(7, 33);
            this.memBankLabel1.Name = "memBankLabel1";
            this.memBankLabel1.Size = new System.Drawing.Size(72, 13);
            this.memBankLabel1.Text = "Memory Bank";
            // 
            // filter2_TP
            // 
            this.filter2_TP.Controls.Add(this.filter_CB2);
            this.filter2_TP.Controls.Add(this.antennaID_CB2);
            this.filter2_TP.Controls.Add(this.antIDLabel2);
            this.filter2_TP.Controls.Add(this.target_CB2);
            this.filter2_TP.Controls.Add(this.targetLabel2);
            this.filter2_TP.Controls.Add(this.action_CB2);
            this.filter2_TP.Controls.Add(this.actionLabel2);
            this.filter2_TP.Controls.Add(this.filterAction_CB2);
            this.filter2_TP.Controls.Add(this.filterActionLabel2);
            this.filter2_TP.Controls.Add(this.tagMask_TB2);
            this.filter2_TP.Controls.Add(this.tagMaskLabel2);
            this.filter2_TP.Controls.Add(this.offset_TB2);
            this.filter2_TP.Controls.Add(this.offsetLabel2);
            this.filter2_TP.Controls.Add(this.memBank_CB2);
            this.filter2_TP.Controls.Add(this.memBankLabel2);
            this.filter2_TP.Location = new System.Drawing.Point(0, 0);
            this.filter2_TP.Name = "filter2_TP";
            this.filter2_TP.Size = new System.Drawing.Size(232, 133);
            this.filter2_TP.Text = "Filter 2";
            // 
            // filter_CB2
            // 
            this.filter_CB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.filter_CB2.Location = new System.Drawing.Point(143, 5);
            this.filter_CB2.Name = "filter_CB2";
            this.filter_CB2.Size = new System.Drawing.Size(87, 20);
            this.filter_CB2.TabIndex = 10;
            this.filter_CB2.Text = "Use Filter 2";
            this.filter_CB2.CheckStateChanged += new System.EventHandler(this.filter_CB2_CheckStateChanged);
            // 
            // antennaID_CB2
            // 
            this.antennaID_CB2.Enabled = false;
            this.antennaID_CB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.antennaID_CB2.ForeColor = System.Drawing.Color.Navy;
            this.antennaID_CB2.Location = new System.Drawing.Point(82, 5);
            this.antennaID_CB2.Name = "antennaID_CB2";
            this.antennaID_CB2.Size = new System.Drawing.Size(40, 20);
            this.antennaID_CB2.TabIndex = 9;
            // 
            // antIDLabel2
            // 
            this.antIDLabel2.Enabled = false;
            this.antIDLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.antIDLabel2.Location = new System.Drawing.Point(7, 10);
            this.antIDLabel2.Name = "antIDLabel2";
            this.antIDLabel2.Size = new System.Drawing.Size(61, 19);
            this.antIDLabel2.Text = "Antenna ID";
            // 
            // target_CB2
            // 
            this.target_CB2.Enabled = false;
            this.target_CB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.target_CB2.ForeColor = System.Drawing.Color.Navy;
            this.target_CB2.Items.Add("S0");
            this.target_CB2.Items.Add("S1");
            this.target_CB2.Items.Add("S2");
            this.target_CB2.Items.Add("S3");
            this.target_CB2.Location = new System.Drawing.Point(197, 108);
            this.target_CB2.Name = "target_CB2";
            this.target_CB2.Size = new System.Drawing.Size(33, 20);
            this.target_CB2.TabIndex = 16;
            // 
            // targetLabel2
            // 
            this.targetLabel2.Enabled = false;
            this.targetLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.targetLabel2.Location = new System.Drawing.Point(155, 111);
            this.targetLabel2.Name = "targetLabel2";
            this.targetLabel2.Size = new System.Drawing.Size(36, 13);
            this.targetLabel2.Text = "Target";
            // 
            // action_CB2
            // 
            this.action_CB2.Enabled = false;
            this.action_CB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.action_CB2.ForeColor = System.Drawing.Color.Navy;
            this.action_CB2.Location = new System.Drawing.Point(45, 108);
            this.action_CB2.Name = "action_CB2";
            this.action_CB2.Size = new System.Drawing.Size(110, 20);
            this.action_CB2.TabIndex = 15;
            // 
            // actionLabel2
            // 
            this.actionLabel2.Enabled = false;
            this.actionLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.actionLabel2.Location = new System.Drawing.Point(7, 113);
            this.actionLabel2.Name = "actionLabel2";
            this.actionLabel2.Size = new System.Drawing.Size(37, 13);
            this.actionLabel2.Text = "Action";
            // 
            // filterAction_CB2
            // 
            this.filterAction_CB2.Enabled = false;
            this.filterAction_CB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.filterAction_CB2.ForeColor = System.Drawing.Color.Navy;
            this.filterAction_CB2.Items.Add("DEFAULT");
            this.filterAction_CB2.Items.Add("STATE AWARE");
            this.filterAction_CB2.Items.Add("STATE UNAWARE");
            this.filterAction_CB2.Location = new System.Drawing.Point(89, 82);
            this.filterAction_CB2.Name = "filterAction_CB2";
            this.filterAction_CB2.Size = new System.Drawing.Size(141, 20);
            this.filterAction_CB2.TabIndex = 14;
            this.filterAction_CB2.SelectedIndexChanged += new System.EventHandler(this.filterAction_CB2_SelectedIndexChanged);
            // 
            // filterActionLabel2
            // 
            this.filterActionLabel2.Enabled = false;
            this.filterActionLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.filterActionLabel2.Location = new System.Drawing.Point(7, 85);
            this.filterActionLabel2.Name = "filterActionLabel2";
            this.filterActionLabel2.Size = new System.Drawing.Size(62, 13);
            this.filterActionLabel2.Text = "Filter Action";
            // 
            // tagMask_TB2
            // 
            this.tagMask_TB2.Enabled = false;
            this.tagMask_TB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagMask_TB2.Location = new System.Drawing.Point(89, 57);
            this.tagMask_TB2.Name = "tagMask_TB2";
            this.tagMask_TB2.Size = new System.Drawing.Size(141, 19);
            this.tagMask_TB2.TabIndex = 13;
            // 
            // tagMaskLabel2
            // 
            this.tagMaskLabel2.Enabled = false;
            this.tagMaskLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagMaskLabel2.Location = new System.Drawing.Point(7, 58);
            this.tagMaskLabel2.Name = "tagMaskLabel2";
            this.tagMaskLabel2.Size = new System.Drawing.Size(61, 25);
            this.tagMaskLabel2.Text = "Tag Pattern (Hex)";
            // 
            // offset_TB2
            // 
            this.offset_TB2.Enabled = false;
            this.offset_TB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.offset_TB2.Location = new System.Drawing.Point(201, 33);
            this.offset_TB2.Name = "offset_TB2";
            this.offset_TB2.Size = new System.Drawing.Size(29, 19);
            this.offset_TB2.TabIndex = 12;
            this.offset_TB2.Text = "32";
            // 
            // offsetLabel2
            // 
            this.offsetLabel2.Enabled = false;
            this.offsetLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.offsetLabel2.Location = new System.Drawing.Point(128, 33);
            this.offsetLabel2.Name = "offsetLabel2";
            this.offsetLabel2.Size = new System.Drawing.Size(65, 13);
            this.offsetLabel2.Text = "Offset (Bits)";
            // 
            // memBank_CB2
            // 
            this.memBank_CB2.Enabled = false;
            this.memBank_CB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.memBank_CB2.ForeColor = System.Drawing.Color.Navy;
            this.memBank_CB2.Items.Add("EPC");
            this.memBank_CB2.Items.Add("TID");
            this.memBank_CB2.Items.Add("USER");
            this.memBank_CB2.Location = new System.Drawing.Point(82, 33);
            this.memBank_CB2.Name = "memBank_CB2";
            this.memBank_CB2.Size = new System.Drawing.Size(40, 20);
            this.memBank_CB2.TabIndex = 11;
            // 
            // memBankLabel2
            // 
            this.memBankLabel2.Enabled = false;
            this.memBankLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.memBankLabel2.Location = new System.Drawing.Point(7, 33);
            this.memBankLabel2.Name = "memBankLabel2";
            this.memBankLabel2.Size = new System.Drawing.Size(72, 13);
            this.memBankLabel2.Text = "Memory Bank";
            // 
            // presFilterButton
            // 
            this.presFilterButton.Location = new System.Drawing.Point(182, 164);
            this.presFilterButton.Name = "presFilterButton";
            this.presFilterButton.Size = new System.Drawing.Size(51, 20);
            this.presFilterButton.TabIndex = 17;
            this.presFilterButton.Text = "Apply";
            this.presFilterButton.Click += new System.EventHandler(this.preFilterButton_Click);
            // 
            // PreFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.presFilterButton);
            this.Controls.Add(this.tabControl1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "PreFilterForm";
            this.Text = "PreFilter";
            this.Load += new System.EventHandler(this.PreFilterForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.PreFilterForm_Closing);
            this.tabControl1.ResumeLayout(false);
            this.filter1_TP.ResumeLayout(false);
            this.filter2_TP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage filter1_TP;
        private System.Windows.Forms.ComboBox antennaID_CB1;
        private System.Windows.Forms.Label antIDLabel1;
        private System.Windows.Forms.ComboBox target_CB1;
        private System.Windows.Forms.Label targetLabel1;
        private System.Windows.Forms.ComboBox action_CB1;
        private System.Windows.Forms.Label actionLabel1;
        private System.Windows.Forms.ComboBox filterAction_CB1;
        private System.Windows.Forms.Label filterActionLabel1;
        private System.Windows.Forms.TextBox tagMask_TB1;
        private System.Windows.Forms.TextBox offset_TB1;
        private System.Windows.Forms.Label offsetLabel1;
        private System.Windows.Forms.ComboBox memBank_CB1;
        private System.Windows.Forms.Label memBankLabel1;
        private System.Windows.Forms.TabPage filter2_TP;
        private System.Windows.Forms.CheckBox filter_CB1;
        private System.Windows.Forms.CheckBox filter_CB2;
        private System.Windows.Forms.ComboBox antennaID_CB2;
        private System.Windows.Forms.Label antIDLabel2;
        private System.Windows.Forms.ComboBox target_CB2;
        private System.Windows.Forms.Label targetLabel2;
        private System.Windows.Forms.ComboBox action_CB2;
        private System.Windows.Forms.Label actionLabel2;
        private System.Windows.Forms.ComboBox filterAction_CB2;
        private System.Windows.Forms.Label filterActionLabel2;
        private System.Windows.Forms.TextBox tagMask_TB2;
        private System.Windows.Forms.Label tagMaskLabel2;
        private System.Windows.Forms.TextBox offset_TB2;
        private System.Windows.Forms.Label offsetLabel2;
        private System.Windows.Forms.ComboBox memBank_CB2;
        private System.Windows.Forms.Label memBankLabel2;
        private System.Windows.Forms.Button presFilterButton;
        private System.Windows.Forms.Label tagMaskLabel1;

    }
}