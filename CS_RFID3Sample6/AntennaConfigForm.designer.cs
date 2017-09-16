 namespace CS_RFID3Sample6
{
    partial class AntennaConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu;

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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.transmitPower_CB = new System.Windows.Forms.ComboBox();
            this.transmitPowerLabel = new System.Windows.Forms.Label();
            this.receiveSensitivity_CB = new System.Windows.Forms.ComboBox();
            this.receiveSenLabel = new System.Windows.Forms.Label();
            this.antennaID_CB = new System.Windows.Forms.ComboBox();
            this.antennaIDLlabel = new System.Windows.Forms.Label();
            this.txFreq_CB = new System.Windows.Forms.ComboBox();
            this.txFreqLabel = new System.Windows.Forms.Label();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.hopTableIndex_CB = new System.Windows.Forms.ComboBox();
            this.hopTableIndexLabel = new System.Windows.Forms.Label();
            this.antennaConfigButton = new System.Windows.Forms.Button();
            this.hopFrequencies_TB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // transmitPower_CB
            // 
            this.transmitPower_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.transmitPower_CB.Location = new System.Drawing.Point(135, 57);
            this.transmitPower_CB.Name = "transmitPower_CB";
            this.transmitPower_CB.Size = new System.Drawing.Size(100, 20);
            this.transmitPower_CB.TabIndex = 3;
            this.transmitPower_CB.SelectedIndexChanged += new System.EventHandler(this.transmitPower_CB_SelectedIndexChanged);
            // 
            // transmitPowerLabel
            // 
            this.transmitPowerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.transmitPowerLabel.Location = new System.Drawing.Point(5, 58);
            this.transmitPowerLabel.Name = "transmitPowerLabel";
            this.transmitPowerLabel.Size = new System.Drawing.Size(119, 26);
            this.transmitPowerLabel.Text = "Transmit Power (dBm)";
            // 
            // receiveSensitivity_CB
            // 
            this.receiveSensitivity_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.receiveSensitivity_CB.Location = new System.Drawing.Point(135, 29);
            this.receiveSensitivity_CB.Name = "receiveSensitivity_CB";
            this.receiveSensitivity_CB.Size = new System.Drawing.Size(100, 20);
            this.receiveSensitivity_CB.TabIndex = 2;
            this.receiveSensitivity_CB.SelectedIndexChanged += new System.EventHandler(this.receiveSensitivity_CB_SelectedIndexChanged);
            // 
            // receiveSenLabel
            // 
            this.receiveSenLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.receiveSenLabel.Location = new System.Drawing.Point(5, 32);
            this.receiveSenLabel.Name = "receiveSenLabel";
            this.receiveSenLabel.Size = new System.Drawing.Size(126, 29);
            this.receiveSenLabel.Text = "Receive Sensitivity (dB)";
            // 
            // antennaID_CB
            // 
            this.antennaID_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.antennaID_CB.Location = new System.Drawing.Point(135, 3);
            this.antennaID_CB.Name = "antennaID_CB";
            this.antennaID_CB.Size = new System.Drawing.Size(100, 20);
            this.antennaID_CB.TabIndex = 1;
            this.antennaID_CB.SelectedIndexChanged += new System.EventHandler(this.antennaID_CB_SelectedIndexChanged);
            // 
            // antennaIDLlabel
            // 
            this.antennaIDLlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.antennaIDLlabel.Location = new System.Drawing.Point(5, 7);
            this.antennaIDLlabel.Name = "antennaIDLlabel";
            this.antennaIDLlabel.Size = new System.Drawing.Size(82, 13);
            this.antennaIDLlabel.Text = "Antenna ID";
            // 
            // txFreq_CB
            // 
            this.txFreq_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.txFreq_CB.Location = new System.Drawing.Point(135, 81);
            this.txFreq_CB.Name = "txFreq_CB";
            this.txFreq_CB.Size = new System.Drawing.Size(95, 20);
            this.txFreq_CB.TabIndex = 16;
            // 
            // txFreqLabel
            // 
            this.txFreqLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.txFreqLabel.Location = new System.Drawing.Point(5, 83);
            this.txFreqLabel.Name = "txFreqLabel";
            this.txFreqLabel.Size = new System.Drawing.Size(119, 30);
            this.txFreqLabel.Text = "Transmit Frequencies";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Frequencies";
            this.columnHeader1.Width = 97;
            // 
            // hopTableIndex_CB
            // 
            this.hopTableIndex_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.hopTableIndex_CB.Location = new System.Drawing.Point(135, 81);
            this.hopTableIndex_CB.Name = "hopTableIndex_CB";
            this.hopTableIndex_CB.Size = new System.Drawing.Size(100, 20);
            this.hopTableIndex_CB.TabIndex = 4;
            this.hopTableIndex_CB.SelectedIndexChanged += new System.EventHandler(this.hopTableIndex_CB_SelectedIndexChanged);
            // 
            // hopTableIndexLabel
            // 
            this.hopTableIndexLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.hopTableIndexLabel.Location = new System.Drawing.Point(5, 84);
            this.hopTableIndexLabel.Name = "hopTableIndexLabel";
            this.hopTableIndexLabel.Size = new System.Drawing.Size(91, 13);
            this.hopTableIndexLabel.Text = "Hop Table Index";
            // 
            // antennaConfigButton
            // 
            this.antennaConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.antennaConfigButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.antennaConfigButton.Location = new System.Drawing.Point(184, 161);
            this.antennaConfigButton.Name = "antennaConfigButton";
            this.antennaConfigButton.Size = new System.Drawing.Size(51, 20);
            this.antennaConfigButton.TabIndex = 20;
            this.antennaConfigButton.Text = "Apply";
            this.antennaConfigButton.Click += new System.EventHandler(this.antennaConfigButton_Click);
            // 
            // hopFrequencies_TB
            // 
            this.hopFrequencies_TB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.hopFrequencies_TB.Location = new System.Drawing.Point(5, 108);
            this.hopFrequencies_TB.Multiline = true;
            this.hopFrequencies_TB.Name = "hopFrequencies_TB";
            this.hopFrequencies_TB.ReadOnly = true;
            this.hopFrequencies_TB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.hopFrequencies_TB.Size = new System.Drawing.Size(230, 47);
            this.hopFrequencies_TB.TabIndex = 26;
            // 
            // AntennaConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.hopFrequencies_TB);
            this.Controls.Add(this.antennaConfigButton);
            this.Controls.Add(this.hopTableIndexLabel);
            this.Controls.Add(this.hopTableIndex_CB);
            this.Controls.Add(this.txFreqLabel);
            this.Controls.Add(this.txFreq_CB);
            this.Controls.Add(this.transmitPower_CB);
            this.Controls.Add(this.transmitPowerLabel);
            this.Controls.Add(this.receiveSensitivity_CB);
            this.Controls.Add(this.receiveSenLabel);
            this.Controls.Add(this.antennaID_CB);
            this.Controls.Add(this.antennaIDLlabel);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "AntennaConfigForm";
            this.Text = "Antenna Config";
            this.Load += new System.EventHandler(this.AntennaConfigForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.AntennaConfigForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ComboBox transmitPower_CB;
        private System.Windows.Forms.Label transmitPowerLabel;
        internal System.Windows.Forms.ComboBox receiveSensitivity_CB;
        private System.Windows.Forms.Label receiveSenLabel;
        internal System.Windows.Forms.ComboBox antennaID_CB;
        private System.Windows.Forms.Label antennaIDLlabel;
        internal System.Windows.Forms.ComboBox txFreq_CB;
        private System.Windows.Forms.Label txFreqLabel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        internal System.Windows.Forms.ComboBox hopTableIndex_CB;
        private System.Windows.Forms.Label hopTableIndexLabel;
        private System.Windows.Forms.Button antennaConfigButton;
        private System.Windows.Forms.TextBox hopFrequencies_TB;

    }
}