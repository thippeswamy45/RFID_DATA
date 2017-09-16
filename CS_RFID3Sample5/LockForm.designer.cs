namespace CS_RFID3Sample5
{
    partial class LockForm
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
            this.Password_TB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tagID_TB = new System.Windows.Forms.TextBox();
            this.tagMaskLabel = new System.Windows.Forms.Label();
            this.memBank_CB = new System.Windows.Forms.ComboBox();
            this.memBankLabel1 = new System.Windows.Forms.Label();
            this.lockPrivilege_CB = new System.Windows.Forms.ComboBox();
            this.lockPrivilegeLabel = new System.Windows.Forms.Label();
            this.lockButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Password_TB
            // 
            this.Password_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.Password_TB.Location = new System.Drawing.Point(86, 37);
            this.Password_TB.Name = "Password_TB";
            this.Password_TB.Size = new System.Drawing.Size(147, 19);
            this.Password_TB.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(7, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.Text = "Password (Hex)";
            // 
            // tagID_TB
            // 
            this.tagID_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagID_TB.Location = new System.Drawing.Point(86, 12);
            this.tagID_TB.Name = "tagID_TB";
            this.tagID_TB.Size = new System.Drawing.Size(147, 19);
            this.tagID_TB.TabIndex = 1;
            // 
            // tagMaskLabel
            // 
            this.tagMaskLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagMaskLabel.Location = new System.Drawing.Point(7, 15);
            this.tagMaskLabel.Name = "tagMaskLabel";
            this.tagMaskLabel.Size = new System.Drawing.Size(72, 13);
            this.tagMaskLabel.Text = "Tag ID (Hex)";
            // 
            // memBank_CB
            // 
            this.memBank_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.memBank_CB.ForeColor = System.Drawing.Color.Navy;
            this.memBank_CB.Items.Add("Kill Password");
            this.memBank_CB.Items.Add("Access Password");
            this.memBank_CB.Items.Add("EPC Memory");
            this.memBank_CB.Items.Add("TID Memory");
            this.memBank_CB.Items.Add("User Memory");
            this.memBank_CB.Location = new System.Drawing.Point(86, 62);
            this.memBank_CB.Name = "memBank_CB";
            this.memBank_CB.Size = new System.Drawing.Size(147, 20);
            this.memBank_CB.TabIndex = 3;
            // 
            // memBankLabel1
            // 
            this.memBankLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.memBankLabel1.Location = new System.Drawing.Point(7, 65);
            this.memBankLabel1.Name = "memBankLabel1";
            this.memBankLabel1.Size = new System.Drawing.Size(72, 13);
            this.memBankLabel1.Text = "Memory Bank";
            // 
            // lockPrivilege_CB
            // 
            this.lockPrivilege_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.lockPrivilege_CB.ForeColor = System.Drawing.Color.Navy;            
            this.lockPrivilege_CB.Items.Add("None");
            this.lockPrivilege_CB.Items.Add("Read & Write");
            this.lockPrivilege_CB.Items.Add("Perma Lock");
            this.lockPrivilege_CB.Items.Add("Perma Unlock");
            this.lockPrivilege_CB.Items.Add("Unlock");
            this.lockPrivilege_CB.Location = new System.Drawing.Point(86, 90);
            this.lockPrivilege_CB.Name = "lockPrivilege_CB";
            this.lockPrivilege_CB.Size = new System.Drawing.Size(147, 20);
            this.lockPrivilege_CB.TabIndex = 4;
            // 
            // lockPrivilegeLabel
            // 
            this.lockPrivilegeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.lockPrivilegeLabel.Location = new System.Drawing.Point(7, 93);
            this.lockPrivilegeLabel.Name = "lockPrivilegeLabel";
            this.lockPrivilegeLabel.Size = new System.Drawing.Size(74, 13);
            this.lockPrivilegeLabel.Text = "Lock Privilege";
            // 
            // lockButton
            // 
            this.lockButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.lockButton.Location = new System.Drawing.Point(167, 116);
            this.lockButton.Name = "lockButton";
            this.lockButton.Size = new System.Drawing.Size(66, 27);
            this.lockButton.TabIndex = 6;
            this.lockButton.Text = "Lock";
            this.lockButton.Click += new System.EventHandler(this.lockButton_Click);
            // 
            // LockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.Password_TB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tagID_TB);
            this.Controls.Add(this.tagMaskLabel);
            this.Controls.Add(this.memBank_CB);
            this.Controls.Add(this.memBankLabel1);
            this.Controls.Add(this.lockPrivilege_CB);
            this.Controls.Add(this.lockPrivilegeLabel);
            this.Controls.Add(this.lockButton);
            this.Menu = this.mainMenu1;
            this.Name = "LockForm";
            this.Text = "Lock";
            this.Load += new System.EventHandler(this.LockForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox Password_TB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tagID_TB;
        private System.Windows.Forms.Label tagMaskLabel;
        internal System.Windows.Forms.ComboBox memBank_CB;
        private System.Windows.Forms.Label memBankLabel1;
        private System.Windows.Forms.ComboBox lockPrivilege_CB;
        private System.Windows.Forms.Label lockPrivilegeLabel;
        internal System.Windows.Forms.Button lockButton;
    }
}