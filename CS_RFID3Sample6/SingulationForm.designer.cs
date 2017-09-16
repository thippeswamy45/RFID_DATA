namespace CS_RFID3Sample6
{
    partial class SingulationForm
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
            this.tagTransit_TB = new System.Windows.Forms.TextBox();
            this.tagtransmitLabel = new System.Windows.Forms.Label();
            this.tagPopulation_TB = new System.Windows.Forms.TextBox();
            this.tagPopulationLabel = new System.Windows.Forms.Label();
            this.session_CB = new System.Windows.Forms.ComboBox();
            this.sessionLabel = new System.Windows.Forms.Label();
            this.antennaID_CB = new System.Windows.Forms.ComboBox();
            this.antennaIDLlabel = new System.Windows.Forms.Label();
            this.SLFlag_CB = new System.Windows.Forms.ComboBox();
            this.inventoryStateLabel = new System.Windows.Forms.Label();
            this.SLFlagLabel = new System.Windows.Forms.Label();
            this.inventoryState_CB = new System.Windows.Forms.ComboBox();
            this.singulationButton = new System.Windows.Forms.Button();
            this.stateAware_CB = new System.Windows.Forms.CheckBox();
            this.stateAware_PB = new System.Windows.Forms.Panel();
            this.stateAware_PB.SuspendLayout();
            this.SuspendLayout();
            // 
            // tagTransit_TB
            // 
            this.tagTransit_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagTransit_TB.Location = new System.Drawing.Point(174, 71);
            this.tagTransit_TB.Name = "tagTransit_TB";
            this.tagTransit_TB.Size = new System.Drawing.Size(61, 19);
            this.tagTransit_TB.TabIndex = 4;
            // 
            // tagtransmitLabel
            // 
            this.tagtransmitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagtransmitLabel.Location = new System.Drawing.Point(13, 71);
            this.tagtransmitLabel.Name = "tagtransmitLabel";
            this.tagtransmitLabel.Size = new System.Drawing.Size(87, 13);
            this.tagtransmitLabel.Text = "Tag Transit Time";
            // 
            // tagPopulation_TB
            // 
            this.tagPopulation_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagPopulation_TB.Location = new System.Drawing.Point(174, 49);
            this.tagPopulation_TB.Name = "tagPopulation_TB";
            this.tagPopulation_TB.Size = new System.Drawing.Size(61, 19);
            this.tagPopulation_TB.TabIndex = 3;
            // 
            // tagPopulationLabel
            // 
            this.tagPopulationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagPopulationLabel.Location = new System.Drawing.Point(14, 49);
            this.tagPopulationLabel.Name = "tagPopulationLabel";
            this.tagPopulationLabel.Size = new System.Drawing.Size(79, 13);
            this.tagPopulationLabel.Text = "Tag Population";
            // 
            // session_CB
            // 
            this.session_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.session_CB.Items.Add("S0");
            this.session_CB.Items.Add("S1");
            this.session_CB.Items.Add("S2");
            this.session_CB.Items.Add("S3");
            this.session_CB.Location = new System.Drawing.Point(174, 26);
            this.session_CB.Name = "session_CB";
            this.session_CB.Size = new System.Drawing.Size(61, 20);
            this.session_CB.TabIndex = 2;
            // 
            // sessionLabel
            // 
            this.sessionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.sessionLabel.Location = new System.Drawing.Point(14, 26);
            this.sessionLabel.Name = "sessionLabel";
            this.sessionLabel.Size = new System.Drawing.Size(44, 13);
            this.sessionLabel.Text = "Session";
            // 
            // antennaID_CB
            // 
            this.antennaID_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.antennaID_CB.Location = new System.Drawing.Point(174, 3);
            this.antennaID_CB.Name = "antennaID_CB";
            this.antennaID_CB.Size = new System.Drawing.Size(61, 20);
            this.antennaID_CB.TabIndex = 1;
            this.antennaID_CB.SelectedIndexChanged += new System.EventHandler(this.antennaID_CB_SelectedIndexChanged);
            // 
            // antennaIDLlabel
            // 
            this.antennaIDLlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.antennaIDLlabel.Location = new System.Drawing.Point(14, 3);
            this.antennaIDLlabel.Name = "antennaIDLlabel";
            this.antennaIDLlabel.Size = new System.Drawing.Size(61, 13);
            this.antennaIDLlabel.Text = "Antenna ID";
            // 
            // SLFlag_CB
            // 
            this.SLFlag_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.SLFlag_CB.Items.Add("ASSERTED");
            this.SLFlag_CB.Items.Add("DEASSERTED");
            this.SLFlag_CB.Location = new System.Drawing.Point(121, 30);
            this.SLFlag_CB.Name = "SLFlag_CB";
            this.SLFlag_CB.Size = new System.Drawing.Size(104, 20);
            this.SLFlag_CB.TabIndex = 6;
            // 
            // inventoryStateLabel
            // 
            this.inventoryStateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.inventoryStateLabel.Location = new System.Drawing.Point(5, 11);
            this.inventoryStateLabel.Name = "inventoryStateLabel";
            this.inventoryStateLabel.Size = new System.Drawing.Size(79, 13);
            this.inventoryStateLabel.Text = "Inventory State";
            // 
            // SLFlagLabel
            // 
            this.SLFlagLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.SLFlagLabel.Location = new System.Drawing.Point(5, 32);
            this.SLFlagLabel.Name = "SLFlagLabel";
            this.SLFlagLabel.Size = new System.Drawing.Size(43, 13);
            this.SLFlagLabel.Text = "SL Flag";
            // 
            // inventoryState_CB
            // 
            this.inventoryState_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.inventoryState_CB.Items.Add("STATE A");
            this.inventoryState_CB.Items.Add("STATE B");
            this.inventoryState_CB.Location = new System.Drawing.Point(122, 4);
            this.inventoryState_CB.Name = "inventoryState_CB";
            this.inventoryState_CB.Size = new System.Drawing.Size(103, 20);
            this.inventoryState_CB.TabIndex = 5;
            // 
            // singulationButton
            // 
            this.singulationButton.Location = new System.Drawing.Point(184, 161);
            this.singulationButton.Name = "singulationButton";
            this.singulationButton.Size = new System.Drawing.Size(51, 20);
            this.singulationButton.TabIndex = 22;
            this.singulationButton.Text = "Apply";
            this.singulationButton.Click += new System.EventHandler(this.singulationButton_Click);
            // 
            // stateAware_CB
            // 
            this.stateAware_CB.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.stateAware_CB.Location = new System.Drawing.Point(10, 87);
            this.stateAware_CB.Name = "stateAware_CB";
            this.stateAware_CB.Size = new System.Drawing.Size(90, 24);
            this.stateAware_CB.TabIndex = 30;
            this.stateAware_CB.Text = "State Aware";
            this.stateAware_CB.CheckStateChanged += new System.EventHandler(this.stateAware_CB_CheckStateChanged);
            // 
            // stateAware_PB
            // 
            this.stateAware_PB.Controls.Add(this.SLFlag_CB);
            this.stateAware_PB.Controls.Add(this.inventoryStateLabel);
            this.stateAware_PB.Controls.Add(this.SLFlagLabel);
            this.stateAware_PB.Controls.Add(this.inventoryState_CB);
            this.stateAware_PB.Location = new System.Drawing.Point(10, 100);
            this.stateAware_PB.Name = "stateAware_PB";
            this.stateAware_PB.Size = new System.Drawing.Size(227, 55);
            // 
            // SingulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.stateAware_CB);
            this.Controls.Add(this.singulationButton);
            this.Controls.Add(this.tagTransit_TB);
            this.Controls.Add(this.tagtransmitLabel);
            this.Controls.Add(this.tagPopulation_TB);
            this.Controls.Add(this.tagPopulationLabel);
            this.Controls.Add(this.session_CB);
            this.Controls.Add(this.sessionLabel);
            this.Controls.Add(this.antennaID_CB);
            this.Controls.Add(this.antennaIDLlabel);
            this.Controls.Add(this.stateAware_PB);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "SingulationForm";
            this.Text = "Singulation";
            this.Load += new System.EventHandler(this.SingulationForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.SingulationForm_Closing);
            this.stateAware_PB.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tagTransit_TB;
        private System.Windows.Forms.Label tagtransmitLabel;
        private System.Windows.Forms.TextBox tagPopulation_TB;
        private System.Windows.Forms.Label tagPopulationLabel;
        private System.Windows.Forms.ComboBox session_CB;
        private System.Windows.Forms.Label sessionLabel;
        private System.Windows.Forms.ComboBox antennaID_CB;
        private System.Windows.Forms.Label antennaIDLlabel;
        private System.Windows.Forms.ComboBox SLFlag_CB;
        private System.Windows.Forms.Label inventoryStateLabel;
        private System.Windows.Forms.Label SLFlagLabel;
        private System.Windows.Forms.ComboBox inventoryState_CB;
        private System.Windows.Forms.Button singulationButton;
        private System.Windows.Forms.CheckBox stateAware_CB;
        private System.Windows.Forms.Panel stateAware_PB;

    }
}