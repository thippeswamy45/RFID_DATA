namespace CS_RFID3Sample6
{
    partial class AntennaInfoForm
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
            this.selectAll_CB = new System.Windows.Forms.CheckBox();
            this.antennaInfoButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selectAll_CB
            // 
            this.selectAll_CB.Location = new System.Drawing.Point(3, 13);
            this.selectAll_CB.Name = "selectAll_CB";
            this.selectAll_CB.Size = new System.Drawing.Size(100, 29);
            this.selectAll_CB.TabIndex = 0;
            this.selectAll_CB.Text = "Select All";
            this.selectAll_CB.CheckStateChanged += new System.EventHandler(this.selectAll_CB_CheckStateChanged);
            // 
            // antennaInfoButton
            // 
            this.antennaInfoButton.Location = new System.Drawing.Point(186, 165);
            this.antennaInfoButton.Name = "antennaInfoButton";
            this.antennaInfoButton.Size = new System.Drawing.Size(51, 20);
            this.antennaInfoButton.TabIndex = 18;
            this.antennaInfoButton.Text = "Apply";
            this.antennaInfoButton.Click += new System.EventHandler(this.antennaInfoButton_Click);
            // 
            // AntennaInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.selectAll_CB);
            this.Controls.Add(this.antennaInfoButton);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "AntennaInfoForm";
            this.Text = "Antenna Info";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.AntennaInfoForm_Closing);
            this.Load += new System.EventHandler(this.AntennaInfoForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox selectAll_CB;
        private System.Windows.Forms.Button antennaInfoButton;


    }
}