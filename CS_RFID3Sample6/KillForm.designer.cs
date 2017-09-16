namespace CS_RFID3Sample6
{
    partial class KillForm
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
            this.accessFilterButton = new System.Windows.Forms.Button();
            this.killButton = new System.Windows.Forms.Button();
            this.killPwd_TB = new System.Windows.Forms.TextBox();
            this.tagID_TB = new System.Windows.Forms.TextBox();
            this.tagIDLabel = new System.Windows.Forms.Label();
            this.killPwdLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // accessFilterButton
            // 
            this.accessFilterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.accessFilterButton.Location = new System.Drawing.Point(81, 83);
            this.accessFilterButton.Name = "accessFilterButton";
            this.accessFilterButton.Size = new System.Drawing.Size(75, 27);
            this.accessFilterButton.TabIndex = 3;
            this.accessFilterButton.Text = "Access Filter";
            this.accessFilterButton.Click += new System.EventHandler(this.accessFilterButton_Click);
            // 
            // killButton
            // 
            this.killButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.killButton.Location = new System.Drawing.Point(168, 83);
            this.killButton.Name = "killButton";
            this.killButton.Size = new System.Drawing.Size(66, 27);
            this.killButton.TabIndex = 4;
            this.killButton.Text = "Kill";
            this.killButton.Click += new System.EventHandler(this.killButton_Click);
            // 
            // killPwd_TB
            // 
            this.killPwd_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.killPwd_TB.Location = new System.Drawing.Point(81, 48);
            this.killPwd_TB.Name = "killPwd_TB";
            this.killPwd_TB.Size = new System.Drawing.Size(153, 19);
            this.killPwd_TB.TabIndex = 2;
            // 
            // tagID_TB
            // 
            this.tagID_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagID_TB.Location = new System.Drawing.Point(81, 14);
            this.tagID_TB.Name = "tagID_TB";
            this.tagID_TB.Size = new System.Drawing.Size(153, 19);
            this.tagID_TB.TabIndex = 1;
            this.tagID_TB.TextChanged += new System.EventHandler(this.tagID_TB_TextChanged);
            // 
            // tagIDLabel
            // 
            this.tagIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagIDLabel.Location = new System.Drawing.Point(6, 18);
            this.tagIDLabel.Name = "tagIDLabel";
            this.tagIDLabel.Size = new System.Drawing.Size(69, 13);
            this.tagIDLabel.Text = "Tag ID (Hex)";
            // 
            // killPwdLabel
            // 
            this.killPwdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.killPwdLabel.Location = new System.Drawing.Point(6, 51);
            this.killPwdLabel.Name = "killPwdLabel";
            this.killPwdLabel.Size = new System.Drawing.Size(81, 29);
            this.killPwdLabel.Text = "Kill Password (Hex)";
            // 
            // KillForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.accessFilterButton);
            this.Controls.Add(this.killButton);
            this.Controls.Add(this.killPwd_TB);
            this.Controls.Add(this.tagID_TB);
            this.Controls.Add(this.tagIDLabel);
            this.Controls.Add(this.killPwdLabel);
            this.Menu = this.mainMenu1;
            this.Name = "KillForm";
            this.Text = "Kill";
            this.Load += new System.EventHandler(this.KillForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button accessFilterButton;
        internal System.Windows.Forms.Button killButton;
        private System.Windows.Forms.TextBox killPwd_TB;
        private System.Windows.Forms.TextBox tagID_TB;
        private System.Windows.Forms.Label tagIDLabel;
        private System.Windows.Forms.Label killPwdLabel;
    }
}