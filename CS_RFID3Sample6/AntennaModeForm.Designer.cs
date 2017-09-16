namespace CS_RFID3Sample6
{
    partial class AntennaModeForm
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
            this.antennaMode_CB = new System.Windows.Forms.ComboBox();
            this.antennaModeLabel = new System.Windows.Forms.Label();
            this.rfModeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // antennaMode_CB
            // 
            this.antennaMode_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.antennaMode_CB.Items.Add("BiStatic");
            this.antennaMode_CB.Items.Add("MonoStatic");
            this.antennaMode_CB.Location = new System.Drawing.Point(137, 32);
            this.antennaMode_CB.Name = "antennaMode_CB";
            this.antennaMode_CB.Size = new System.Drawing.Size(89, 20);
            this.antennaMode_CB.TabIndex = 5;
            // 
            // antennaModeLabel
            // 
            this.antennaModeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.antennaModeLabel.Location = new System.Drawing.Point(19, 34);
            this.antennaModeLabel.Name = "antennaModeLabel";
            this.antennaModeLabel.Size = new System.Drawing.Size(78, 15);
            this.antennaModeLabel.Text = "Mode";
            // 
            // rfModeButton
            // 
            this.rfModeButton.Location = new System.Drawing.Point(175, 165);
            this.rfModeButton.Name = "rfModeButton";
            this.rfModeButton.Size = new System.Drawing.Size(51, 20);
            this.rfModeButton.TabIndex = 21;
            this.rfModeButton.Text = "Apply";
            this.rfModeButton.Click += new System.EventHandler(this.antennaModeButton_Click);
            // 
            // AntennaModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.rfModeButton);
            this.Controls.Add(this.antennaMode_CB);
            this.Controls.Add(this.antennaModeLabel);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "AntennaModeForm";
            this.Text = "Antenna Mode";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.AntennaModeForm_Closing);
            this.Load += new System.EventHandler(this.AntennaModeForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox antennaMode_CB;
        private System.Windows.Forms.Label antennaModeLabel;
        private System.Windows.Forms.Button rfModeButton;
    }
}