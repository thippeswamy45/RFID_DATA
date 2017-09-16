namespace CS_RFID3Sample6
{
    partial class FirmwareUpdateForm
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
            this.password_TB = new System.Windows.Forms.TextBox();
            this.username_TB = new System.Windows.Forms.TextBox();
            this.IPLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.firmwareApplyButton = new System.Windows.Forms.Button();
            this.updateDesc_TB = new System.Windows.Forms.TextBox();
            this.location_TB = new System.Windows.Forms.TextBox();
            this.update_PB = new System.Windows.Forms.ProgressBar();
            this.ftp_GB = new System.Windows.Forms.Panel();
            this.ftp_label = new System.Windows.Forms.Label();
            this.update_CB = new System.Windows.Forms.ComboBox();
            this.browseFileButton = new System.Windows.Forms.Button();
            this.ftp_GB.SuspendLayout();
            this.SuspendLayout();
            // 
            // password_TB
            // 
            this.password_TB.Enabled = false;
            this.password_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.password_TB.Location = new System.Drawing.Point(90, 30);
            this.password_TB.Name = "password_TB";
            this.password_TB.Size = new System.Drawing.Size(128, 19);
            this.password_TB.TabIndex = 1;
            // 
            // username_TB
            // 
            this.username_TB.Enabled = false;
            this.username_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.username_TB.Location = new System.Drawing.Point(90, 3);
            this.username_TB.Name = "username_TB";
            this.username_TB.Size = new System.Drawing.Size(128, 19);
            this.username_TB.TabIndex = 1;
            this.username_TB.Text = "admin";
            // 
            // IPLabel
            // 
            this.IPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.IPLabel.Location = new System.Drawing.Point(9, 90);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(60, 19);
            this.IPLabel.Text = "Location";
            // 
            // passwordLabel
            // 
            this.passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.passwordLabel.Location = new System.Drawing.Point(11, 36);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.Text = "Password";
            // 
            // userNameLabel
            // 
            this.userNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.userNameLabel.Location = new System.Drawing.Point(4, 16);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(60, 13);
            this.userNameLabel.Text = "User Name";
            // 
            // firmwareApplyButton
            // 
            this.firmwareApplyButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.firmwareApplyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.firmwareApplyButton.Location = new System.Drawing.Point(10, 114);
            this.firmwareApplyButton.Name = "firmwareApplyButton";
            this.firmwareApplyButton.Size = new System.Drawing.Size(75, 20);
            this.firmwareApplyButton.TabIndex = 4;
            this.firmwareApplyButton.Text = "Start";
            this.firmwareApplyButton.Click += new System.EventHandler(this.firmwareApplyButton_Click);
            // 
            // updateDesc_TB
            // 
            this.updateDesc_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.updateDesc_TB.Location = new System.Drawing.Point(9, 166);
            this.updateDesc_TB.Name = "updateDesc_TB";
            this.updateDesc_TB.ReadOnly = true;
            this.updateDesc_TB.Size = new System.Drawing.Size(222, 19);
            this.updateDesc_TB.TabIndex = 0;
            // 
            // location_TB
            // 
            this.location_TB.Enabled = false;
            this.location_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.location_TB.Location = new System.Drawing.Point(61, 89);
            this.location_TB.Name = "location_TB";
            this.location_TB.Size = new System.Drawing.Size(170, 19);
            this.location_TB.TabIndex = 2;
            // 
            // update_PB
            // 
            this.update_PB.Location = new System.Drawing.Point(9, 140);
            this.update_PB.Name = "update_PB";
            this.update_PB.Size = new System.Drawing.Size(222, 20);
            // 
            // ftp_GB
            // 
            this.ftp_GB.Controls.Add(this.ftp_label);
            this.ftp_GB.Controls.Add(this.password_TB);
            this.ftp_GB.Controls.Add(this.username_TB);
            this.ftp_GB.Controls.Add(this.passwordLabel);
            this.ftp_GB.Controls.Add(this.userNameLabel);
            this.ftp_GB.Location = new System.Drawing.Point(9, 3);
            this.ftp_GB.Name = "ftp_GB";
            this.ftp_GB.Size = new System.Drawing.Size(228, 53);
            // 
            // ftp_label
            // 
            this.ftp_label.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.ftp_label.Location = new System.Drawing.Point(11, 0);
            this.ftp_label.Name = "ftp_label";
            this.ftp_label.Size = new System.Drawing.Size(64, 15);
            this.ftp_label.Text = "FTP Info";
            // 
            // update_CB
            // 
            this.update_CB.Location = new System.Drawing.Point(9, 60);
            this.update_CB.Name = "update_CB";
            this.update_CB.Size = new System.Drawing.Size(222, 22);
            this.update_CB.TabIndex = 9;
            this.update_CB.SelectedValueChanged += new System.EventHandler(this.update_CB_SelectedValueChanged);
            // 
            // browseFileButton
            // 
            this.browseFileButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.browseFileButton.Location = new System.Drawing.Point(159, 114);
            this.browseFileButton.Name = "browseFileButton";
            this.browseFileButton.Size = new System.Drawing.Size(72, 20);
            this.browseFileButton.TabIndex = 13;
            this.browseFileButton.Text = "Browse";
            this.browseFileButton.Click += new System.EventHandler(this.browseFileButton_Click);
            // FirmwareUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.browseFileButton);
            this.Controls.Add(this.update_CB);
            this.Controls.Add(this.ftp_GB);
            this.Controls.Add(this.update_PB);
            this.Controls.Add(this.location_TB);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.firmwareApplyButton);
            this.Controls.Add(this.updateDesc_TB);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "FirmwareUpdateForm";
            this.Text = "Firmware Update";
            this.Load += new System.EventHandler(this.FirmwareUpdateForm_Load);
            this.ftp_GB.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Button firmwareApplyButton;
        private System.Windows.Forms.TextBox updateDesc_TB;
        internal System.Windows.Forms.TextBox password_TB;
        internal System.Windows.Forms.TextBox username_TB;
        internal System.Windows.Forms.TextBox location_TB;
        private System.Windows.Forms.ProgressBar update_PB;
        private System.Windows.Forms.Panel ftp_GB;
        private System.Windows.Forms.Label ftp_label;
        private System.Windows.Forms.ComboBox update_CB;
        private System.Windows.Forms.Button browseFileButton;
    }
}