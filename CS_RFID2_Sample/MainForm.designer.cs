namespace CS_RFID2_Sample
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.TAGID = new System.Windows.Forms.ColumnHeader();
            this.Tag_Type = new System.Windows.Forms.ColumnHeader();
            this.Time_Stamp = new System.Windows.Forms.ColumnHeader();
            this.Antenna = new System.Windows.Forms.ColumnHeader();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.zMotionProgBar = new System.Windows.Forms.ProgressBar();
            this.ymotionProgBar = new System.Windows.Forms.ProgressBar();
            this.xmotionprogBar = new System.Windows.Forms.ProgressBar();
            this.proximityProgbar = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtZMotion = new System.Windows.Forms.TextBox();
            this.txtYMotion = new System.Windows.Forms.TextBox();
            this.txtXMotion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtProximity = new System.Windows.Forms.TextBox();
            this.lblConnect = new System.Windows.Forms.Label();
            this.mainMenu2 = new System.Windows.Forms.MainMenu();
            this.menuReader = new System.Windows.Forms.MenuItem();
            this.menuConnect = new System.Windows.Forms.MenuItem();
            this.menuDisconnect = new System.Windows.Forms.MenuItem();
            this.menuSetAntenna = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuReaderInfo = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuCapability = new System.Windows.Forms.MenuItem();
            this.menuExit = new System.Windows.Forms.MenuItem();
            this.menuReaderMode = new System.Windows.Forms.MenuItem();
            this.menuAutoMode = new System.Windows.Forms.MenuItem();
            this.menuOnDemand = new System.Windows.Forms.MenuItem();
            this.menuCommands = new System.Windows.Forms.MenuItem();
            this.menuGettingTags = new System.Windows.Forms.MenuItem();
            this.menuGen2Read = new System.Windows.Forms.MenuItem();
            this.menuGetTags = new System.Windows.Forms.MenuItem();
            this.menuProgrammingTags = new System.Windows.Forms.MenuItem();
            this.menuGen2Write = new System.Windows.Forms.MenuItem();
            this.menuProg = new System.Windows.Forms.MenuItem();
            this.menuLocking = new System.Windows.Forms.MenuItem();
            this.menuGen2Lock = new System.Windows.Forms.MenuItem();
            this.menuKillingTags = new System.Windows.Forms.MenuItem();
            this.menuGen2Kill = new System.Windows.Forms.MenuItem();
            this.menuKillTags = new System.Windows.Forms.MenuItem();
            this.menuSelectRecords = new System.Windows.Forms.MenuItem();
            this.menuGen2SelRec = new System.Windows.Forms.MenuItem();
            this.menuEraseTag = new System.Windows.Forms.MenuItem();
            this.menuMotionSensor = new System.Windows.Forms.MenuItem();
            this.menuProxSensor = new System.Windows.Forms.MenuItem();
            this.menuReadData = new System.Windows.Forms.MenuItem();
            this.menuWriteTag = new System.Windows.Forms.MenuItem();
            this.menuWrite = new System.Windows.Forms.MenuItem();
            this.lblReaderMode = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.tabControl1.Location = new System.Drawing.Point(3, 55);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(234, 228);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(226, 202);
            this.tabPage2.Text = "Command Output";
            // 
            // listView2
            // 
            this.listView2.Columns.Add(this.columnHeader1);
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView2.Location = new System.Drawing.Point(0, 0);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(226, 202);
            this.listView2.TabIndex = 3;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ColumnHeader";
            this.columnHeader1.Width = 222;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(226, 202);
            this.tabPage1.Text = "Live Tags";
            // 
            // listView1
            // 
            this.listView1.Columns.Add(this.TAGID);
            this.listView1.Columns.Add(this.Tag_Type);
            this.listView1.Columns.Add(this.Time_Stamp);
            this.listView1.Columns.Add(this.Antenna);
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(226, 202);
            this.listView1.TabIndex = 2;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // TAGID
            // 
            this.TAGID.Text = "Tag ID";
            this.TAGID.Width = 82;
            // 
            // Tag_Type
            // 
            this.Tag_Type.Text = "TagType";
            this.Tag_Type.Width = 80;
            // 
            // Time_Stamp
            // 
            this.Time_Stamp.Text = "Time Stamp";
            this.Time_Stamp.Width = 64;
            // 
            // Antenna
            // 
            this.Antenna.Text = "Antenna";
            this.Antenna.Width = 60;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.zMotionProgBar);
            this.tabPage3.Controls.Add(this.ymotionProgBar);
            this.tabPage3.Controls.Add(this.xmotionprogBar);
            this.tabPage3.Controls.Add(this.proximityProgbar);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.txtZMotion);
            this.tabPage3.Controls.Add(this.txtYMotion);
            this.tabPage3.Controls.Add(this.txtXMotion);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.txtProximity);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(226, 202);
            this.tabPage3.Text = "Sensor";
            // 
            // zMotionProgBar
            // 
            this.zMotionProgBar.Enabled = false;
            this.zMotionProgBar.Location = new System.Drawing.Point(113, 156);
            this.zMotionProgBar.Maximum = 3500;
            this.zMotionProgBar.Name = "zMotionProgBar";
            this.zMotionProgBar.Size = new System.Drawing.Size(102, 23);
            // 
            // ymotionProgBar
            // 
            this.ymotionProgBar.Enabled = false;
            this.ymotionProgBar.Location = new System.Drawing.Point(113, 117);
            this.ymotionProgBar.Maximum = 3500;
            this.ymotionProgBar.Name = "ymotionProgBar";
            this.ymotionProgBar.Size = new System.Drawing.Size(102, 23);
            // 
            // xmotionprogBar
            // 
            this.xmotionprogBar.Enabled = false;
            this.xmotionprogBar.Location = new System.Drawing.Point(113, 80);
            this.xmotionprogBar.Maximum = 3500;
            this.xmotionprogBar.Name = "xmotionprogBar";
            this.xmotionprogBar.Size = new System.Drawing.Size(102, 23);
            // 
            // proximityProgbar
            // 
            this.proximityProgbar.Enabled = false;
            this.proximityProgbar.Location = new System.Drawing.Point(113, 23);
            this.proximityProgbar.Maximum = 255;
            this.proximityProgbar.Name = "proximityProgbar";
            this.proximityProgbar.Size = new System.Drawing.Size(102, 23);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(23, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 20);
            this.label5.Text = "z:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(23, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 20);
            this.label4.Text = "y : ";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(23, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 20);
            this.label3.Text = "x : ";
            // 
            // txtZMotion
            // 
            this.txtZMotion.Enabled = false;
            this.txtZMotion.Location = new System.Drawing.Point(73, 156);
            this.txtZMotion.Name = "txtZMotion";
            this.txtZMotion.Size = new System.Drawing.Size(37, 23);
            this.txtZMotion.TabIndex = 14;
            // 
            // txtYMotion
            // 
            this.txtYMotion.Enabled = false;
            this.txtYMotion.Location = new System.Drawing.Point(73, 117);
            this.txtYMotion.Name = "txtYMotion";
            this.txtYMotion.Size = new System.Drawing.Size(37, 23);
            this.txtYMotion.TabIndex = 13;
            // 
            // txtXMotion
            // 
            this.txtXMotion.Enabled = false;
            this.txtXMotion.Location = new System.Drawing.Point(73, 80);
            this.txtXMotion.Name = "txtXMotion";
            this.txtXMotion.Size = new System.Drawing.Size(37, 23);
            this.txtXMotion.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Motion";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 23);
            this.label6.Text = "Proximity";
            // 
            // txtProximity
            // 
            this.txtProximity.Enabled = false;
            this.txtProximity.Location = new System.Drawing.Point(75, 23);
            this.txtProximity.Name = "txtProximity";
            this.txtProximity.Size = new System.Drawing.Size(35, 23);
            this.txtProximity.TabIndex = 8;
            // 
            // lblConnect
            // 
            this.lblConnect.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblConnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblConnect.Location = new System.Drawing.Point(2, 34);
            this.lblConnect.Name = "lblConnect";
            this.lblConnect.Size = new System.Drawing.Size(69, 18);
            this.lblConnect.Text = "Connected";
            // 
            // mainMenu2
            // 
            this.mainMenu2.MenuItems.Add(this.menuReader);
            this.mainMenu2.MenuItems.Add(this.menuReaderMode);
            this.mainMenu2.MenuItems.Add(this.menuCommands);
            // 
            // menuReader
            // 
            this.menuReader.MenuItems.Add(this.menuConnect);
            this.menuReader.MenuItems.Add(this.menuDisconnect);
            this.menuReader.MenuItems.Add(this.menuSetAntenna);
            this.menuReader.MenuItems.Add(this.menuItem2);
            this.menuReader.MenuItems.Add(this.menuReaderInfo);
            this.menuReader.MenuItems.Add(this.menuItem1);
            this.menuReader.MenuItems.Add(this.menuCapability);
            this.menuReader.MenuItems.Add(this.menuExit);
            this.menuReader.Text = "Reader";
            // 
            // menuConnect
            // 
            this.menuConnect.Text = "Connect";
            this.menuConnect.Click += new System.EventHandler(this.menuConnect_Click);
            // 
            // menuDisconnect
            // 
            this.menuDisconnect.Text = "Disconnect";
            this.menuDisconnect.Click += new System.EventHandler(this.menuDisconnect_Click);
            // 
            // menuSetAntenna
            // 
            this.menuSetAntenna.Text = "Set Antenna";
            this.menuSetAntenna.Click += new System.EventHandler(this.menuSetAntenna_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Reader Settings";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuReaderInfo
            // 
            this.menuReaderInfo.Text = "Antenna Info";
            this.menuReaderInfo.Click += new System.EventHandler(this.menuReaderInfo_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Reader Info";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuCapability
            // 
            this.menuCapability.Text = "Capability";
            this.menuCapability.Click += new System.EventHandler(this.menuCapability_Click);
            // 
            // menuExit
            // 
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuReaderMode
            // 
            this.menuReaderMode.MenuItems.Add(this.menuAutoMode);
            this.menuReaderMode.MenuItems.Add(this.menuOnDemand);
            this.menuReaderMode.Text = "Read Mode";
            // 
            // menuAutoMode
            // 
            this.menuAutoMode.Text = "Autonomous";
            this.menuAutoMode.Click += new System.EventHandler(this.menuAutoMode_Click);
            // 
            // menuOnDemand
            // 
            this.menuOnDemand.Checked = true;
            this.menuOnDemand.Enabled = false;
            this.menuOnDemand.Text = "On Demand";
            this.menuOnDemand.Click += new System.EventHandler(this.menuOnDemand_Click);
            // 
            // menuCommands
            // 
            this.menuCommands.MenuItems.Add(this.menuGettingTags);
            this.menuCommands.MenuItems.Add(this.menuProgrammingTags);
            this.menuCommands.MenuItems.Add(this.menuLocking);
            this.menuCommands.MenuItems.Add(this.menuKillingTags);
            this.menuCommands.MenuItems.Add(this.menuSelectRecords);
            this.menuCommands.MenuItems.Add(this.menuEraseTag);
            this.menuCommands.MenuItems.Add(this.menuMotionSensor);
            this.menuCommands.MenuItems.Add(this.menuProxSensor);
            this.menuCommands.Text = "Commands";
            // 
            // menuGettingTags
            // 
            this.menuGettingTags.MenuItems.Add(this.menuGen2Read);
            this.menuGettingTags.MenuItems.Add(this.menuGetTags);
            this.menuGettingTags.Text = "Read Tag ID";
            this.menuGettingTags.Click += new System.EventHandler(this.menuGetTags_Click);
            // 
            // menuGen2Read
            // 
            this.menuGen2Read.Text = "Gen2 Read";
            this.menuGen2Read.Click += new System.EventHandler(this.menuGen2Read_Click);
            // 
            // menuGetTags
            // 
            this.menuGetTags.Text = "Get Tags";
            this.menuGetTags.Click += new System.EventHandler(this.menuGetTags_Click);
            // 
            // menuProgrammingTags
            // 
            this.menuProgrammingTags.MenuItems.Add(this.menuGen2Write);
            this.menuProgrammingTags.MenuItems.Add(this.menuProg);
            this.menuProgrammingTags.Text = "Write Tag ID";
            this.menuProgrammingTags.Click += new System.EventHandler(this.menuProg_Click);
            // 
            // menuGen2Write
            // 
            this.menuGen2Write.Text = "Gen2Write";
            this.menuGen2Write.Click += new System.EventHandler(this.menuGen2Write_Click);
            // 
            // menuProg
            // 
            this.menuProg.Text = "WriteTagID";
            // 
            // menuLocking
            // 
            this.menuLocking.MenuItems.Add(this.menuGen2Lock);
            this.menuLocking.Text = "Lock Tags";
            this.menuLocking.Click += new System.EventHandler(this.menuLock_Click);
            // 
            // menuGen2Lock
            // 
            this.menuGen2Lock.Text = "Gen2 Lock";
            this.menuGen2Lock.Click += new System.EventHandler(this.menuGen2Lock_Click);
            // 
            // menuKillingTags
            // 
            this.menuKillingTags.MenuItems.Add(this.menuGen2Kill);
            this.menuKillingTags.MenuItems.Add(this.menuKillTags);
            this.menuKillingTags.Text = "Kill Tags";
            this.menuKillingTags.Click += new System.EventHandler(this.menuKillTags_Click);
            // 
            // menuGen2Kill
            // 
            this.menuGen2Kill.Text = "Gen2 Kill";
            this.menuGen2Kill.Click += new System.EventHandler(this.menuGen2Kill_Click);
            // 
            // menuKillTags
            // 
            this.menuKillTags.Text = "Kill Tag";
            // 
            // menuSelectRecords
            // 
            this.menuSelectRecords.MenuItems.Add(this.menuGen2SelRec);
            this.menuSelectRecords.Text = "Filters";
            // 
            // menuGen2SelRec
            // 
            this.menuGen2SelRec.Text = "Gen2 Sel Rec";
            this.menuGen2SelRec.Click += new System.EventHandler(this.menuGen2SelRec_Click);
            // 
            // menuEraseTag
            // 
            this.menuEraseTag.Text = "Erase Tags";
            this.menuEraseTag.Click += new System.EventHandler(this.menuEraseTag_Click);
            // 
            // menuMotionSensor
            // 
            this.menuMotionSensor.Text = "Enable MotionSensor";
            this.menuMotionSensor.Click += new System.EventHandler(this.menuMotionSensor_Click);
            // 
            // menuProxSensor
            // 
            this.menuProxSensor.Text = "Enable ProximitySensor";
            this.menuProxSensor.Click += new System.EventHandler(this.menuProxSensor_Click);
            // 
            // menuReadData
            // 
            this.menuReadData.Text = "Read Data";
            this.menuReadData.Click += new System.EventHandler(this.menuReadData_Click);
            // 
            // menuWriteTag
            // 
            this.menuWriteTag.Text = "Write Tag";
            this.menuWriteTag.Click += new System.EventHandler(this.menuWriteTag_Click);
            // 
            // menuWrite
            // 
            this.menuWrite.Text = "Write Tags";
            // 
            // lblReaderMode
            // 
            this.lblReaderMode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblReaderMode.Location = new System.Drawing.Point(75, 33);
            this.lblReaderMode.Name = "lblReaderMode";
            this.lblReaderMode.Size = new System.Drawing.Size(66, 20);
            this.lblReaderMode.Text = "Autonomous";
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnClear.Location = new System.Drawing.Point(144, 29);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 23);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear Response";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 284);
            this.Controls.Add(this.lblReaderMode);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblConnect);
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu2;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "CS_RFID2_Sample ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label lblConnect;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader TAGID;
        private System.Windows.Forms.ColumnHeader Tag_Type;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.MainMenu mainMenu2;
        private System.Windows.Forms.MenuItem menuReader;
        private System.Windows.Forms.MenuItem menuConnect;
        private System.Windows.Forms.MenuItem menuReaderInfo;
        private System.Windows.Forms.MenuItem menuReaderMode;
        private System.Windows.Forms.MenuItem menuAutoMode;
        private System.Windows.Forms.MenuItem menuOnDemand;
        private System.Windows.Forms.MenuItem menuCommands;
        private System.Windows.Forms.MenuItem menuGettingTags;
        private System.Windows.Forms.MenuItem menuWrite;
        private System.Windows.Forms.MenuItem menuLocking;
        private System.Windows.Forms.MenuItem menuProgrammingTags;
        private System.Windows.Forms.MenuItem menuKillingTags;
        private System.Windows.Forms.Label lblReaderMode;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.MenuItem menuEraseTag;
        private System.Windows.Forms.MenuItem menuDisconnect;
        private System.Windows.Forms.MenuItem menuExit;
        private System.Windows.Forms.MenuItem menuCapability;
        private System.Windows.Forms.MenuItem menuSetAntenna;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.TabPage tabPage3;
        //private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtZMotion;
        private System.Windows.Forms.TextBox txtYMotion;
        private System.Windows.Forms.TextBox txtXMotion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProximity;
        private System.Windows.Forms.MenuItem menuMotionSensor;
        private System.Windows.Forms.MenuItem menuProxSensor;
        private System.Windows.Forms.ProgressBar proximityProgbar;
        private System.Windows.Forms.ProgressBar ymotionProgBar;
        private System.Windows.Forms.ProgressBar xmotionprogBar;
        private System.Windows.Forms.ProgressBar zMotionProgBar;
        private System.Windows.Forms.ColumnHeader Time_Stamp;
        private System.Windows.Forms.ColumnHeader Antenna;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuReadData;
        private System.Windows.Forms.MenuItem menuWriteTag;
      private System.Windows.Forms.MenuItem menuGen2Read;
      private System.Windows.Forms.MenuItem menuGetTags;
      private System.Windows.Forms.MenuItem menuGen2Write;
      private System.Windows.Forms.MenuItem menuProg;
      private System.Windows.Forms.MenuItem menuGen2Kill;
      private System.Windows.Forms.MenuItem menuKillTags;
      private System.Windows.Forms.MenuItem menuGen2Lock;
      private System.Windows.Forms.MenuItem menuSelectRecords;
      private System.Windows.Forms.MenuItem menuGen2SelRec;
    }
}

