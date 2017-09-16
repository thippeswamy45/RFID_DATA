namespace CS_RFID3Sample6
{
    partial class LoginForm
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
            this.readerType_CB = new System.Windows.Forms.ComboBox();
            this.password_TB = new System.Windows.Forms.TextBox();
            this.username_TB = new System.Windows.Forms.TextBox();
            this.IPLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.readerTypeLabel = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.hostname_TB = new System.Windows.Forms.TextBox();
            this.forceLogin_CB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // readerType_CB
            // 
            this.readerType_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.readerType_CB.Items.Add("XR");
            this.readerType_CB.Items.Add("FX");
            this.readerType_CB.Items.Add("MC");
            this.readerType_CB.Location = new System.Drawing.Point(101, 12);
            this.readerType_CB.Name = "readerType_CB";
            this.readerType_CB.Size = new System.Drawing.Size(128, 20);
            this.readerType_CB.TabIndex = 1;
            this.readerType_CB.SelectedIndexChanged += new System.EventHandler(this.readerType_CB_SelectedIndexChanged);
            // 
            // password_TB
            // 
            this.password_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.password_TB.Location = new System.Drawing.Point(101, 83);
            this.password_TB.Name = "password_TB";
            this.password_TB.PasswordChar = '*';
            this.password_TB.Size = new System.Drawing.Size(128, 19);
            this.password_TB.TabIndex = 3;
            // 
            // username_TB
            // 
            this.username_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.username_TB.Location = new System.Drawing.Point(101, 46);
            this.username_TB.Name = "username_TB";
            this.username_TB.Size = new System.Drawing.Size(128, 19);
            this.username_TB.TabIndex = 2;
            this.username_TB.Text = "admin";
            // 
            // IPLabel
            // 
            this.IPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.IPLabel.Location = new System.Drawing.Point(58, 124);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(17, 13);
            this.IPLabel.Text = "IP";
            // 
            // passwordLabel
            // 
            this.passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.passwordLabel.Location = new System.Drawing.Point(22, 86);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.Text = "Password";
            // 
            // userNameLabel
            // 
            this.userNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.userNameLabel.Location = new System.Drawing.Point(15, 49);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(60, 13);
            this.userNameLabel.Text = "User Name";
            // 
            // readerTypeLabel
            // 
            this.readerTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.readerTypeLabel.Location = new System.Drawing.Point(9, 15);
            this.readerTypeLabel.Name = "readerTypeLabel";
            this.readerTypeLabel.Size = new System.Drawing.Size(69, 13);
            this.readerTypeLabel.Text = "Reader Type";
            // 
            // loginButton
            // 
            this.loginButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.loginButton.Location = new System.Drawing.Point(154, 156);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 23);
            this.loginButton.TabIndex = 5;
            this.loginButton.Text = "Login";
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // hostname_TB
            // 
            this.hostname_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.hostname_TB.Location = new System.Drawing.Point(101, 120);
            this.hostname_TB.Name = "hostname_TB";
            this.hostname_TB.Size = new System.Drawing.Size(128, 19);
            this.hostname_TB.TabIndex = 4;
            // 
            // forceLogin_CB
            // 
            this.forceLogin_CB.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.forceLogin_CB.Location = new System.Drawing.Point(22, 156);
            this.forceLogin_CB.Name = "forceLogin_CB";
            this.forceLogin_CB.Size = new System.Drawing.Size(100, 20);
            this.forceLogin_CB.TabIndex = 9;
            this.forceLogin_CB.Text = "Force Login";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.forceLogin_CB);
            this.Controls.Add(this.hostname_TB);
            this.Controls.Add(this.readerType_CB);
            this.Controls.Add(this.password_TB);
            this.Controls.Add(this.username_TB);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userNameLabel);
            this.Controls.Add(this.readerTypeLabel);
            this.Controls.Add(this.loginButton);
            this.Menu = this.mainMenu1;
            this.Name = "LoginForm";
            this.Text = "Login/Logout";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox readerType_CB;
        private System.Windows.Forms.TextBox password_TB;
        private System.Windows.Forms.TextBox username_TB;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Label readerTypeLabel;
        internal System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.TextBox hostname_TB;
        private System.Windows.Forms.CheckBox forceLogin_CB;
    }
}