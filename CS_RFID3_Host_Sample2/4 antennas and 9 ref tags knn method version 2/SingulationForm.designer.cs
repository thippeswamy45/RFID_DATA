namespace CS_RFID3_Host_Sample2
{
    partial class SingulationForm
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
            this.antennaID_CB = new System.Windows.Forms.ComboBox();
            this.antennaIDLlabel = new System.Windows.Forms.Label();
            this.session_CB = new System.Windows.Forms.ComboBox();
            this.sessionLabel = new System.Windows.Forms.Label();
            this.tagPopulation_TB = new System.Windows.Forms.TextBox();
            this.tagPopulationLabel = new System.Windows.Forms.Label();
            this.tagTransit_TB = new System.Windows.Forms.TextBox();
            this.tagtransmitLabel = new System.Windows.Forms.Label();
            this.stateAwareGroupBox = new System.Windows.Forms.GroupBox();
            this.stateAwareSingulation_CB = new System.Windows.Forms.CheckBox();
            this.SLFlag_CB = new System.Windows.Forms.ComboBox();
            this.inventoryStateLabel = new System.Windows.Forms.Label();
            this.SLFlagLabel = new System.Windows.Forms.Label();
            this.inventoryState_CB = new System.Windows.Forms.ComboBox();
            this.singulationButton = new System.Windows.Forms.Button();
            this.stateAwareGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // antennaID_CB
            // 
            this.antennaID_CB.FormattingEnabled = true;
            this.antennaID_CB.Location = new System.Drawing.Point(230, 18);
            this.antennaID_CB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.antennaID_CB.Name = "antennaID_CB";
            this.antennaID_CB.Size = new System.Drawing.Size(90, 28);
            this.antennaID_CB.TabIndex = 1;
            this.antennaID_CB.SelectedIndexChanged += new System.EventHandler(this.antennaID_CB_SelectedIndexChanged);
            // 
            // antennaIDLlabel
            // 
            this.antennaIDLlabel.AutoSize = true;
            this.antennaIDLlabel.Location = new System.Drawing.Point(39, 23);
            this.antennaIDLlabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.antennaIDLlabel.Name = "antennaIDLlabel";
            this.antennaIDLlabel.Size = new System.Drawing.Size(91, 20);
            this.antennaIDLlabel.TabIndex = 4;
            this.antennaIDLlabel.Text = "Antenna ID";
            // 
            // session_CB
            // 
            this.session_CB.FormattingEnabled = true;
            this.session_CB.Items.AddRange(new object[] {
            "S0",
            "S1",
            "S2"});
            this.session_CB.Location = new System.Drawing.Point(230, 65);
            this.session_CB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.session_CB.Name = "session_CB";
            this.session_CB.Size = new System.Drawing.Size(90, 28);
            this.session_CB.TabIndex = 2;
            // 
            // sessionLabel
            // 
            this.sessionLabel.AutoSize = true;
            this.sessionLabel.Location = new System.Drawing.Point(39, 65);
            this.sessionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sessionLabel.Name = "sessionLabel";
            this.sessionLabel.Size = new System.Drawing.Size(66, 20);
            this.sessionLabel.TabIndex = 6;
            this.sessionLabel.Text = "Session";
            // 
            // tagPopulation_TB
            // 
            this.tagPopulation_TB.Location = new System.Drawing.Point(230, 106);
            this.tagPopulation_TB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tagPopulation_TB.Name = "tagPopulation_TB";
            this.tagPopulation_TB.Size = new System.Drawing.Size(90, 26);
            this.tagPopulation_TB.TabIndex = 3;
            // 
            // tagPopulationLabel
            // 
            this.tagPopulationLabel.AutoSize = true;
            this.tagPopulationLabel.Location = new System.Drawing.Point(39, 111);
            this.tagPopulationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tagPopulationLabel.Name = "tagPopulationLabel";
            this.tagPopulationLabel.Size = new System.Drawing.Size(115, 20);
            this.tagPopulationLabel.TabIndex = 8;
            this.tagPopulationLabel.Text = "Tag Population";
            // 
            // tagTransit_TB
            // 
            this.tagTransit_TB.Location = new System.Drawing.Point(230, 146);
            this.tagTransit_TB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tagTransit_TB.Name = "tagTransit_TB";
            this.tagTransit_TB.Size = new System.Drawing.Size(90, 26);
            this.tagTransit_TB.TabIndex = 4;
            // 
            // tagtransmitLabel
            // 
            this.tagtransmitLabel.AutoSize = true;
            this.tagtransmitLabel.Location = new System.Drawing.Point(39, 157);
            this.tagtransmitLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tagtransmitLabel.Name = "tagtransmitLabel";
            this.tagtransmitLabel.Size = new System.Drawing.Size(126, 20);
            this.tagtransmitLabel.TabIndex = 10;
            this.tagtransmitLabel.Text = "Tag Transit Time";
            // 
            // stateAwareGroupBox
            // 
            this.stateAwareGroupBox.Controls.Add(this.stateAwareSingulation_CB);
            this.stateAwareGroupBox.Controls.Add(this.SLFlag_CB);
            this.stateAwareGroupBox.Controls.Add(this.inventoryStateLabel);
            this.stateAwareGroupBox.Controls.Add(this.SLFlagLabel);
            this.stateAwareGroupBox.Controls.Add(this.inventoryState_CB);
            this.stateAwareGroupBox.Location = new System.Drawing.Point(18, 200);
            this.stateAwareGroupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stateAwareGroupBox.Name = "stateAwareGroupBox";
            this.stateAwareGroupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stateAwareGroupBox.Size = new System.Drawing.Size(324, 160);
            this.stateAwareGroupBox.TabIndex = 12;
            this.stateAwareGroupBox.TabStop = false;
            // 
            // stateAwareSingulation_CB
            // 
            this.stateAwareSingulation_CB.AutoSize = true;
            this.stateAwareSingulation_CB.Location = new System.Drawing.Point(0, 0);
            this.stateAwareSingulation_CB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stateAwareSingulation_CB.Name = "stateAwareSingulation_CB";
            this.stateAwareSingulation_CB.Size = new System.Drawing.Size(292, 24);
            this.stateAwareSingulation_CB.TabIndex = 16;
            this.stateAwareSingulation_CB.Text = "State Aware Singulation Parameters";
            this.stateAwareSingulation_CB.UseVisualStyleBackColor = true;
            this.stateAwareSingulation_CB.CheckedChanged += new System.EventHandler(this.stateAwareSingulation_CB_CheckedChanged);
            // 
            // SLFlag_CB
            // 
            this.SLFlag_CB.FormattingEnabled = true;
            this.SLFlag_CB.Items.AddRange(new object[] {
            "ASSERTED",
            "DEASSERTED"});
            this.SLFlag_CB.Location = new System.Drawing.Point(148, 103);
            this.SLFlag_CB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SLFlag_CB.Name = "SLFlag_CB";
            this.SLFlag_CB.Size = new System.Drawing.Size(152, 28);
            this.SLFlag_CB.TabIndex = 6;
            // 
            // inventoryStateLabel
            // 
            this.inventoryStateLabel.AutoSize = true;
            this.inventoryStateLabel.Location = new System.Drawing.Point(21, 57);
            this.inventoryStateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.inventoryStateLabel.Name = "inventoryStateLabel";
            this.inventoryStateLabel.Size = new System.Drawing.Size(117, 20);
            this.inventoryStateLabel.TabIndex = 13;
            this.inventoryStateLabel.Text = "Inventory State";
            // 
            // SLFlagLabel
            // 
            this.SLFlagLabel.AutoSize = true;
            this.SLFlagLabel.Location = new System.Drawing.Point(21, 103);
            this.SLFlagLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SLFlagLabel.Name = "SLFlagLabel";
            this.SLFlagLabel.Size = new System.Drawing.Size(64, 20);
            this.SLFlagLabel.TabIndex = 15;
            this.SLFlagLabel.Text = "SL Flag";
            // 
            // inventoryState_CB
            // 
            this.inventoryState_CB.FormattingEnabled = true;
            this.inventoryState_CB.Items.AddRange(new object[] {
            "STATE A",
            "STATE B"});
            this.inventoryState_CB.Location = new System.Drawing.Point(148, 45);
            this.inventoryState_CB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.inventoryState_CB.Name = "inventoryState_CB";
            this.inventoryState_CB.Size = new System.Drawing.Size(152, 28);
            this.inventoryState_CB.TabIndex = 5;
            // 
            // singulationButton
            // 
            this.singulationButton.Location = new System.Drawing.Point(230, 369);
            this.singulationButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.singulationButton.Name = "singulationButton";
            this.singulationButton.Size = new System.Drawing.Size(112, 35);
            this.singulationButton.TabIndex = 7;
            this.singulationButton.Text = "Apply";
            this.singulationButton.UseVisualStyleBackColor = true;
            this.singulationButton.Click += new System.EventHandler(this.singulationButton_Click);
            // 
            // SingulationForm
            // 
            this.AcceptButton = this.singulationButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 414);
            this.Controls.Add(this.singulationButton);
            this.Controls.Add(this.stateAwareGroupBox);
            this.Controls.Add(this.tagTransit_TB);
            this.Controls.Add(this.tagtransmitLabel);
            this.Controls.Add(this.tagPopulation_TB);
            this.Controls.Add(this.tagPopulationLabel);
            this.Controls.Add(this.session_CB);
            this.Controls.Add(this.sessionLabel);
            this.Controls.Add(this.antennaID_CB);
            this.Controls.Add(this.antennaIDLlabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SingulationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Singulation";
            this.Load += new System.EventHandler(this.Singulation_Load);
            this.stateAwareGroupBox.ResumeLayout(false);
            this.stateAwareGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox antennaID_CB;
        private System.Windows.Forms.Label antennaIDLlabel;
        private System.Windows.Forms.ComboBox session_CB;
        private System.Windows.Forms.Label sessionLabel;
        private System.Windows.Forms.TextBox tagPopulation_TB;
        private System.Windows.Forms.Label tagPopulationLabel;
        private System.Windows.Forms.TextBox tagTransit_TB;
        private System.Windows.Forms.Label tagtransmitLabel;
        private System.Windows.Forms.GroupBox stateAwareGroupBox;
        private System.Windows.Forms.ComboBox SLFlag_CB;
        private System.Windows.Forms.Label inventoryStateLabel;
        private System.Windows.Forms.Label SLFlagLabel;
        private System.Windows.Forms.ComboBox inventoryState_CB;
        private System.Windows.Forms.Button singulationButton;
        private System.Windows.Forms.CheckBox stateAwareSingulation_CB;
    }
}