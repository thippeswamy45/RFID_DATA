namespace WANSampleTest
{
	partial class FormMain
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
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonHangup = new System.Windows.Forms.Button();
            this.buttonDial = new System.Windows.Forms.Button();
            this.textBoxPhNumber = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.listViewSettings = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.tabPagePhone = new System.Windows.Forms.TabPage();
            this.buttonAnswer = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.listBoxPhoneStatus = new System.Windows.Forms.ListBox();
            this.tabPageData = new System.Windows.Forms.TabPage();
            this.checkBoxConn = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listBoxDataStatus = new System.Windows.Forms.ListBox();
            this.tabPageSMS = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMsg = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.buttonSendSms = new System.Windows.Forms.Button();
            this.listBoxSmsStatus = new System.Windows.Forms.ListBox();
            this.checkBoxEnableRadio = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tabPagePhone.SuspendLayout();
            this.tabPageData.SuspendLayout();
            this.tabPageSMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 229);
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.buttonExit.Location = new System.Drawing.Point(135, 183);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(51, 20);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "Exit";
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            this.buttonExit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonExit_KeyDown);
            // 
            // buttonHangup
            // 
            this.buttonHangup.Enabled = false;
            this.buttonHangup.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.buttonHangup.Location = new System.Drawing.Point(59, 49);
            this.buttonHangup.Name = "buttonHangup";
            this.buttonHangup.Size = new System.Drawing.Size(48, 19);
            this.buttonHangup.TabIndex = 2;
            this.buttonHangup.Text = "Hangup";
            this.buttonHangup.Click += new System.EventHandler(this.buttonHangup_Click);
            this.buttonHangup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonHangup_KeyDown);
            // 
            // buttonDial
            // 
            this.buttonDial.Enabled = false;
            this.buttonDial.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.buttonDial.Location = new System.Drawing.Point(5, 49);
            this.buttonDial.Name = "buttonDial";
            this.buttonDial.Size = new System.Drawing.Size(48, 19);
            this.buttonDial.TabIndex = 1;
            this.buttonDial.Text = "Dial";
            this.buttonDial.Click += new System.EventHandler(this.buttonDial_Click);
            this.buttonDial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonDial_KeyDown);
            // 
            // textBoxPhNumber
            // 
            this.textBoxPhNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxPhNumber.Location = new System.Drawing.Point(3, 20);
            this.textBoxPhNumber.Name = "textBoxPhNumber";
            this.textBoxPhNumber.Size = new System.Drawing.Size(173, 19);
            this.textBoxPhNumber.TabIndex = 0;
            this.textBoxPhNumber.Text = "7384909";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Controls.Add(this.tabPagePhone);
            this.tabControl1.Controls.Add(this.tabPageData);
            this.tabControl1.Controls.Add(this.tabPageSMS);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tabControl1.Location = new System.Drawing.Point(6, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(190, 178);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.listViewSettings);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Size = new System.Drawing.Size(182, 152);
            this.tabPageSettings.Text = "Settings";
            // 
            // listViewSettings
            // 
            this.listViewSettings.Columns.Add(this.columnHeader1);
            this.listViewSettings.Columns.Add(this.columnHeader2);
            this.listViewSettings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewSettings.Location = new System.Drawing.Point(3, 4);
            this.listViewSettings.Name = "listViewSettings";
            this.listViewSettings.Size = new System.Drawing.Size(181, 145);
            this.listViewSettings.TabIndex = 0;
            this.listViewSettings.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Setting";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 95;
            // 
            // tabPagePhone
            // 
            this.tabPagePhone.Controls.Add(this.buttonAnswer);
            this.tabPagePhone.Controls.Add(this.label6);
            this.tabPagePhone.Controls.Add(this.listBoxPhoneStatus);
            this.tabPagePhone.Controls.Add(this.buttonDial);
            this.tabPagePhone.Controls.Add(this.buttonHangup);
            this.tabPagePhone.Controls.Add(this.textBoxPhNumber);
            this.tabPagePhone.Location = new System.Drawing.Point(4, 22);
            this.tabPagePhone.Name = "tabPagePhone";
            this.tabPagePhone.Size = new System.Drawing.Size(182, 152);
            this.tabPagePhone.Text = "Phone";
            // 
            // buttonAnswer
            // 
            this.buttonAnswer.Enabled = false;
            this.buttonAnswer.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.buttonAnswer.Location = new System.Drawing.Point(128, 49);
            this.buttonAnswer.Name = "buttonAnswer";
            this.buttonAnswer.Size = new System.Drawing.Size(48, 19);
            this.buttonAnswer.TabIndex = 3;
            this.buttonAnswer.Text = "Answer";
            this.buttonAnswer.Click += new System.EventHandler(this.buttonAnswer_Click);
            this.buttonAnswer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonAnswer_KeyDown);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(3, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 14);
            this.label6.Text = "Status";
            // 
            // listBoxPhoneStatus
            // 
            this.listBoxPhoneStatus.BackColor = System.Drawing.SystemColors.Menu;
            this.listBoxPhoneStatus.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.listBoxPhoneStatus.Items.Add("Phone Status");
            this.listBoxPhoneStatus.Location = new System.Drawing.Point(3, 110);
            this.listBoxPhoneStatus.Name = "listBoxPhoneStatus";
            this.listBoxPhoneStatus.Size = new System.Drawing.Size(173, 41);
            this.listBoxPhoneStatus.TabIndex = 4;
            // 
            // tabPageData
            // 
            this.tabPageData.Controls.Add(this.checkBoxConn);
            this.tabPageData.Controls.Add(this.label5);
            this.tabPageData.Controls.Add(this.listBoxDataStatus);
            this.tabPageData.Location = new System.Drawing.Point(4, 22);
            this.tabPageData.Name = "tabPageData";
            this.tabPageData.Size = new System.Drawing.Size(182, 152);
            this.tabPageData.Text = "Data";
            // 
            // checkBoxConn
            // 
            this.checkBoxConn.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxConn.Location = new System.Drawing.Point(3, 42);
            this.checkBoxConn.Name = "checkBoxConn";
            this.checkBoxConn.Size = new System.Drawing.Size(128, 20);
            this.checkBoxConn.TabIndex = 0;
            this.checkBoxConn.Text = "Connection Manager";
            this.checkBoxConn.Click += new System.EventHandler(this.checkBoxConn_Click);
            this.checkBoxConn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.checkBoxConn_KeyDown);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(3, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 14);
            this.label5.Text = "Status";
            // 
            // listBoxDataStatus
            // 
            this.listBoxDataStatus.BackColor = System.Drawing.SystemColors.Menu;
            this.listBoxDataStatus.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.listBoxDataStatus.Items.Add("Data Status");
            this.listBoxDataStatus.Location = new System.Drawing.Point(3, 111);
            this.listBoxDataStatus.Name = "listBoxDataStatus";
            this.listBoxDataStatus.Size = new System.Drawing.Size(173, 41);
            this.listBoxDataStatus.TabIndex = 2;
            // 
            // tabPageSMS
            // 
            this.tabPageSMS.Controls.Add(this.label1);
            this.tabPageSMS.Controls.Add(this.textBoxMsg);
            this.tabPageSMS.Controls.Add(this.label2);
            this.tabPageSMS.Controls.Add(this.textBoxAddress);
            this.tabPageSMS.Controls.Add(this.buttonSendSms);
            this.tabPageSMS.Controls.Add(this.listBoxSmsStatus);
            this.tabPageSMS.Location = new System.Drawing.Point(4, 22);
            this.tabPageSMS.Name = "tabPageSMS";
            this.tabPageSMS.Size = new System.Drawing.Size(182, 152);
            this.tabPageSMS.Text = "SMS";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 14);
            this.label1.Text = "Status";
            // 
            // textBoxMsg
            // 
            this.textBoxMsg.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxMsg.Location = new System.Drawing.Point(3, 29);
            this.textBoxMsg.Multiline = true;
            this.textBoxMsg.Name = "textBoxMsg";
            this.textBoxMsg.Size = new System.Drawing.Size(173, 31);
            this.textBoxMsg.TabIndex = 1;
            this.textBoxMsg.Text = "My Message";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(5, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.Text = "To (10 digits)";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxAddress.Location = new System.Drawing.Point(76, 4);
            this.textBoxAddress.MaxLength = 10;
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(100, 19);
            this.textBoxAddress.TabIndex = 0;
            // 
            // buttonSendSms
            // 
            this.buttonSendSms.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.buttonSendSms.Location = new System.Drawing.Point(3, 63);
            this.buttonSendSms.Name = "buttonSendSms";
            this.buttonSendSms.Size = new System.Drawing.Size(48, 19);
            this.buttonSendSms.TabIndex = 2;
            this.buttonSendSms.Text = "Send";
            this.buttonSendSms.Click += new System.EventHandler(this.buttonSendSms_Click);
            this.buttonSendSms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonSendSms_KeyDown);
            // 
            // listBoxSmsStatus
            // 
            this.listBoxSmsStatus.BackColor = System.Drawing.SystemColors.Menu;
            this.listBoxSmsStatus.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.listBoxSmsStatus.Items.Add("SMS Status");
            this.listBoxSmsStatus.Location = new System.Drawing.Point(3, 110);
            this.listBoxSmsStatus.Name = "listBoxSmsStatus";
            this.listBoxSmsStatus.Size = new System.Drawing.Size(173, 41);
            this.listBoxSmsStatus.TabIndex = 3;
            // 
            // checkBoxEnableRadio
            // 
            this.checkBoxEnableRadio.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxEnableRadio.Location = new System.Drawing.Point(6, 182);
            this.checkBoxEnableRadio.Name = "checkBoxEnableRadio";
            this.checkBoxEnableRadio.Size = new System.Drawing.Size(61, 18);
            this.checkBoxEnableRadio.TabIndex = 3;
            this.checkBoxEnableRadio.Text = "Radio";
            this.checkBoxEnableRadio.Click += new System.EventHandler(this.checkBoxEnableRadio_Click);
            this.checkBoxEnableRadio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.checkBoxEnableRadio_KeyDown);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusBar1
            // 
            this.statusBar1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.statusBar1.Location = new System.Drawing.Point(3, 209);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(193, 20);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.buttonAbout.Location = new System.Drawing.Point(96, 183);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(37, 20);
            this.buttonAbout.TabIndex = 0;
            this.buttonAbout.Text = "About";
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            this.buttonAbout.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonAbout_KeyDown);
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(196, 229);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.checkBoxEnableRadio);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.splitter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Menu = this.mainMenu1;
            this.Name = "FormMain";
            this.Text = "WANSample";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabPagePhone.ResumeLayout(false);
            this.tabPageData.ResumeLayout(false);
            this.tabPageSMS.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Button buttonHangup;
		private System.Windows.Forms.Button buttonDial;
		private System.Windows.Forms.TextBox textBoxPhNumber;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPagePhone;
		private System.Windows.Forms.TabPage tabPageSettings;
		private System.Windows.Forms.TabPage tabPageData;
		private System.Windows.Forms.TabPage tabPageSMS;
		private System.Windows.Forms.ListBox listBoxPhoneStatus;
		private System.Windows.Forms.ListBox listBoxDataStatus;
		private System.Windows.Forms.ListBox listBoxSmsStatus;
		private System.Windows.Forms.Button buttonSendSms;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxAddress;
		private System.Windows.Forms.TextBox textBoxMsg;
		private System.Windows.Forms.CheckBox checkBoxEnableRadio;
		private System.Windows.Forms.ListView listViewSettings;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonAnswer;
		private System.Windows.Forms.CheckBox checkBoxConn;
		private System.Windows.Forms.Button buttonAbout;
	}
}

