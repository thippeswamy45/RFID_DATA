namespace CS_RFID3Sample6
{
    partial class TagStorageForm
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
            this.memoryBankSize_TB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.idLength_TB = new System.Windows.Forms.TextBox();
            this.maxCount_TB = new System.Windows.Forms.TextBox();
            this.hostNameLabel = new System.Windows.Forms.Label();
            this.filenameLabel = new System.Windows.Forms.Label();
            this.tagStorageSettingButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // memoryBankSize_TB
            // 
            this.memoryBankSize_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.memoryBankSize_TB.Location = new System.Drawing.Point(158, 80);
            this.memoryBankSize_TB.Name = "memoryBankSize_TB";
            this.memoryBankSize_TB.Size = new System.Drawing.Size(77, 19);
            this.memoryBankSize_TB.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(5, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 26);
            this.label1.Text = "Max Size of Memory Bank (Bytes)";
            // 
            // idLength_TB
            // 
            this.idLength_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.idLength_TB.Location = new System.Drawing.Point(158, 50);
            this.idLength_TB.Name = "idLength_TB";
            this.idLength_TB.Size = new System.Drawing.Size(77, 19);
            this.idLength_TB.TabIndex = 2;
            // 
            // maxCount_TB
            // 
            this.maxCount_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.maxCount_TB.Location = new System.Drawing.Point(158, 20);
            this.maxCount_TB.Name = "maxCount_TB";
            this.maxCount_TB.Size = new System.Drawing.Size(77, 19);
            this.maxCount_TB.TabIndex = 1;
            // 
            // hostNameLabel
            // 
            this.hostNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.hostNameLabel.Location = new System.Drawing.Point(5, 55);
            this.hostNameLabel.Name = "hostNameLabel";
            this.hostNameLabel.Size = new System.Drawing.Size(147, 14);
            this.hostNameLabel.Text = "Max Tag ID Length (Bytes)";
            // 
            // filenameLabel
            // 
            this.filenameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.filenameLabel.Location = new System.Drawing.Point(5, 25);
            this.filenameLabel.Name = "filenameLabel";
            this.filenameLabel.Size = new System.Drawing.Size(147, 19);
            this.filenameLabel.Text = "Maximum Tag Count";
            // 
            // tagStorageSettingButton
            // 
            this.tagStorageSettingButton.Location = new System.Drawing.Point(184, 161);
            this.tagStorageSettingButton.Name = "tagStorageSettingButton";
            this.tagStorageSettingButton.Size = new System.Drawing.Size(51, 20);
            this.tagStorageSettingButton.TabIndex = 23;
            this.tagStorageSettingButton.Text = "Apply";
            this.tagStorageSettingButton.Click += new System.EventHandler(this.tagStorageSettingButton_Click);
            // 
            // TagStorageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.tagStorageSettingButton);
            this.Controls.Add(this.memoryBankSize_TB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.idLength_TB);
            this.Controls.Add(this.maxCount_TB);
            this.Controls.Add(this.hostNameLabel);
            this.Controls.Add(this.filenameLabel);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "TagStorageForm";
            this.Text = "Tag Storage Settings";
            this.Load += new System.EventHandler(this.TagStorageForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.TagStorageForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox memoryBankSize_TB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox idLength_TB;
        private System.Windows.Forms.TextBox maxCount_TB;
        private System.Windows.Forms.Label hostNameLabel;
        private System.Windows.Forms.Label filenameLabel;
        private System.Windows.Forms.Button tagStorageSettingButton;
    }
}