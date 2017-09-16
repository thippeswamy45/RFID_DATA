namespace CS_RFID3Sample6
{
    partial class HelpForm
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
            this.versionLabel = new System.Windows.Forms.Label();
            this.copyRightLabel = new System.Windows.Forms.Label();
            this.dllVersionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // versionLabel
            // 
            this.versionLabel.Location = new System.Drawing.Point(12, 35);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(195, 20);
            this.versionLabel.Text = "CS_RFID3Sample6";
            // 
            // copyRightLabel
            // 
            this.copyRightLabel.Location = new System.Drawing.Point(12, 94);
            this.copyRightLabel.Name = "copyRightLabel";
            this.copyRightLabel.Size = new System.Drawing.Size(123, 20);
            this.copyRightLabel.Text = "Copyright (C) 2010";
            // 
            // dllVersionLabel
            // 
            this.dllVersionLabel.Location = new System.Drawing.Point(12, 65);
            this.dllVersionLabel.Name = "dllVersionLabel";
            this.dllVersionLabel.Size = new System.Drawing.Size(216, 20);
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.dllVersionLabel);
            this.Controls.Add(this.copyRightLabel);
            this.Controls.Add(this.versionLabel);
            this.Menu = this.mainMenu1;
            this.Name = "HelpForm";
            this.Text = "About CS_RFID3Sample6";
            this.Load += new System.EventHandler(this.HelpForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label copyRightLabel;
        private System.Windows.Forms.Label dllVersionLabel;
    }
}