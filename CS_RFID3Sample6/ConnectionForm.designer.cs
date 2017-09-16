namespace CS_RFID3Sample6
{
    partial class ConnectionForm
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
            this.readerIPLabel = new System.Windows.Forms.Label();
            this.connectionButton = new System.Windows.Forms.Button();
            this.portLabel = new System.Windows.Forms.Label();
            this.Port_TB = new System.Windows.Forms.TextBox();
            this.IP_TB = new System.Windows.Forms.TextBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.SuspendLayout();
            // 
            // readerIPLabel
            // 
            this.readerIPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.readerIPLabel.Location = new System.Drawing.Point(7, 20);
            this.readerIPLabel.Name = "readerIPLabel";
            this.readerIPLabel.Size = new System.Drawing.Size(118, 13);
            this.readerIPLabel.Text = "Host Name/Reader IP";
            // 
            // connectionButton
            // 
            this.connectionButton.Location = new System.Drawing.Point(80, 114);
            this.connectionButton.Name = "connectionButton";
            this.connectionButton.Size = new System.Drawing.Size(92, 23);
            this.connectionButton.TabIndex = 3;
            this.connectionButton.Text = "Connect";
            this.connectionButton.Click += new System.EventHandler(this.connectionButton_Click);
            // 
            // portLabel
            // 
            this.portLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.portLabel.Location = new System.Drawing.Point(7, 58);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(26, 13);
            this.portLabel.Text = "Port";
            // 
            // Port_TB
            // 
            this.Port_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.Port_TB.Location = new System.Drawing.Point(126, 58);
            this.Port_TB.Name = "Port_TB";
            this.Port_TB.Size = new System.Drawing.Size(99, 19);
            this.Port_TB.TabIndex = 2;
            // 
            // IP_TB
            // 
            this.IP_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.IP_TB.Location = new System.Drawing.Point(126, 20);
            this.IP_TB.Name = "IP_TB";
            this.IP_TB.Size = new System.Drawing.Size(99, 19);
            this.IP_TB.TabIndex = 1;
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.IP_TB);
            this.Controls.Add(this.Port_TB);
            this.Controls.Add(this.connectionButton);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.readerIPLabel);
            this.Menu = this.mainMenu1;
            this.Name = "ConnectionForm";
            this.Text = "Connection";
            this.Load += new System.EventHandler(this.ConnectionForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label readerIPLabel;
        internal System.Windows.Forms.Button connectionButton;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox Port_TB;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.TextBox IP_TB;
    }
}