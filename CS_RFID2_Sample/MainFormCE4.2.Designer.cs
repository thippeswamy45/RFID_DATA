namespace CS_RFID2_Sample
{
    partial class MainFormMobil
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
          this.menuCapability = new System.Windows.Forms.MenuItem();
          this.menuItem1 = new System.Windows.Forms.MenuItem();
          this.menuReaderInfo = new System.Windows.Forms.MenuItem();
          this.menuAutoMode = new System.Windows.Forms.MenuItem();
          this.menuReaderMode = new System.Windows.Forms.MenuItem();
          this.menuOnDemand = new System.Windows.Forms.MenuItem();
          this.menuTriggerMode = new System.Windows.Forms.MenuItem();
          this.menuExit = new System.Windows.Forms.MenuItem();
          this.menuReader = new System.Windows.Forms.MenuItem();
          this.menuConnect = new System.Windows.Forms.MenuItem();
          this.menuDisconnect = new System.Windows.Forms.MenuItem();
          this.menuSetAntenna = new System.Windows.Forms.MenuItem();
          this.menuItem2 = new System.Windows.Forms.MenuItem();
          this.lblReaderMode = new System.Windows.Forms.Label();
          this.mainMenu1 = new System.Windows.Forms.MainMenu();
          this.menuCommands = new System.Windows.Forms.MenuItem();
          this.menuGettingTags = new System.Windows.Forms.MenuItem();
          this.menuGen2Read = new System.Windows.Forms.MenuItem();
          this.menuGetTags = new System.Windows.Forms.MenuItem();
          this.menuProgrammingTags = new System.Windows.Forms.MenuItem();
          this.menuGen2Write = new System.Windows.Forms.MenuItem();
          this.menuProg = new System.Windows.Forms.MenuItem();
          this.menuKillingTags = new System.Windows.Forms.MenuItem();
          this.menuGen2Kill = new System.Windows.Forms.MenuItem();
          this.menuKillTags = new System.Windows.Forms.MenuItem();
          this.menuEraseTag = new System.Windows.Forms.MenuItem();
          this.menuLocking = new System.Windows.Forms.MenuItem();
          this.menuGen2Lock = new System.Windows.Forms.MenuItem();
          this.menuSelectRecords = new System.Windows.Forms.MenuItem();
          this.menuGen2SelRec = new System.Windows.Forms.MenuItem();
          this.menuReadData = new System.Windows.Forms.MenuItem();
          this.menuWriteTag = new System.Windows.Forms.MenuItem();
          this.menuWrite = new System.Windows.Forms.MenuItem();
          this.btnClear = new System.Windows.Forms.Button();
          this.Tag_Type = new System.Windows.Forms.ColumnHeader();
          this.TAGID = new System.Windows.Forms.ColumnHeader();
          this.listView1 = new System.Windows.Forms.ListView();
          this.Time_Stamp = new System.Windows.Forms.ColumnHeader();
          this.Antenna = new System.Windows.Forms.ColumnHeader();
          this.tabPage2 = new System.Windows.Forms.TabPage();
          this.listView2 = new System.Windows.Forms.ListView();
          this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
          this.tabControl1 = new System.Windows.Forms.TabControl();
          this.tabPage1 = new System.Windows.Forms.TabPage();
          this.tabTrigger = new System.Windows.Forms.TabPage();
          this.lblTrig = new System.Windows.Forms.Label();
          this.label1 = new System.Windows.Forms.Label();
          this.lblConnect = new System.Windows.Forms.Label();
          this.tabPage2.SuspendLayout();
          this.tabControl1.SuspendLayout();
          this.tabPage1.SuspendLayout();
          this.tabTrigger.SuspendLayout();
          this.SuspendLayout();
          // 
          // menuCapability
          // 
          this.menuCapability.Text = "Capability";
          this.menuCapability.Click += new System.EventHandler(this.menuCapability_Click);
          // 
          // menuItem1
          // 
          this.menuItem1.Text = "Reader Info";
          this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
          // 
          // menuReaderInfo
          // 
          this.menuReaderInfo.Text = "Antenna Info";
          this.menuReaderInfo.Click += new System.EventHandler(this.menuReaderInfo_Click);
          // 
          // menuAutoMode
          // 
          this.menuAutoMode.Text = "Autonomous";
          this.menuAutoMode.Click += new System.EventHandler(this.menuAutoMode_Click);
          // 
          // menuReaderMode
          // 
          this.menuReaderMode.MenuItems.Add(this.menuAutoMode);
          this.menuReaderMode.MenuItems.Add(this.menuOnDemand);
          this.menuReaderMode.MenuItems.Add(this.menuTriggerMode);
          this.menuReaderMode.Text = "Read Mode";
          // 
          // menuOnDemand
          // 
          this.menuOnDemand.Checked = true;
          this.menuOnDemand.Enabled = false;
          this.menuOnDemand.Text = "On Demand";
          this.menuOnDemand.Click += new System.EventHandler(this.menuOnDemand_Click);
          // 
          // menuTriggerMode
          // 
          this.menuTriggerMode.Text = "Triggered Read";
          this.menuTriggerMode.Click += new System.EventHandler(this.menuItem2_Click);
          // 
          // menuExit
          // 
          this.menuExit.Text = "Exit";
          this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
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
          this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click_1);
          // 
          // lblReaderMode
          // 
          this.lblReaderMode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
          this.lblReaderMode.Location = new System.Drawing.Point(61, 237);
          this.lblReaderMode.Name = "lblReaderMode";
          this.lblReaderMode.Size = new System.Drawing.Size(71, 20);
          this.lblReaderMode.Text = "Autonomous";
          // 
          // mainMenu1
          // 
          this.mainMenu1.MenuItems.Add(this.menuReader);
          this.mainMenu1.MenuItems.Add(this.menuReaderMode);
          this.mainMenu1.MenuItems.Add(this.menuCommands);
          // 
          // menuCommands
          // 
          this.menuCommands.MenuItems.Add(this.menuGettingTags);
          this.menuCommands.MenuItems.Add(this.menuProgrammingTags);
          this.menuCommands.MenuItems.Add(this.menuKillingTags);
          this.menuCommands.MenuItems.Add(this.menuEraseTag);
          this.menuCommands.MenuItems.Add(this.menuLocking);
          this.menuCommands.MenuItems.Add(this.menuSelectRecords);
          this.menuCommands.Text = "Commands";
          // 
          // menuGettingTags
          // 
          this.menuGettingTags.MenuItems.Add(this.menuGen2Read);
          this.menuGettingTags.MenuItems.Add(this.menuGetTags);
          this.menuGettingTags.Text = "Read Tag ID";
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
          // 
          // menuGen2Write
          // 
          this.menuGen2Write.Text = "Gen2 Write";
          this.menuGen2Write.Click += new System.EventHandler(this.menuGen2Write_Click);
          // 
          // menuProg
          // 
          this.menuProg.Text = "Write Tag ID";
          this.menuProg.Click += new System.EventHandler(this.menuProg_Click);
          // 
          // menuKillingTags
          // 
          this.menuKillingTags.MenuItems.Add(this.menuGen2Kill);
          this.menuKillingTags.MenuItems.Add(this.menuKillTags);
          this.menuKillingTags.Text = "Kill Tags";
          // 
          // menuGen2Kill
          // 
          this.menuGen2Kill.Text = "Gen2 Kill";
          this.menuGen2Kill.Click += new System.EventHandler(this.menuGen2Kill_Click);
          // 
          // menuKillTags
          // 
          this.menuKillTags.Text = "Kill Tags";
          this.menuKillTags.Click += new System.EventHandler(this.menuKillTags_Click);
          // 
          // menuEraseTag
          // 
          this.menuEraseTag.Text = "Erase Tags";
          this.menuEraseTag.Click += new System.EventHandler(this.menuEraseTag_Click);
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
          // menuSelectRecords
          // 
          this.menuSelectRecords.MenuItems.Add(this.menuGen2SelRec);
          this.menuSelectRecords.Text = "Sel Records";
          // 
          // menuGen2SelRec
          // 
          this.menuGen2SelRec.Text = "Gen2 Sel Rec";
          this.menuGen2SelRec.Click += new System.EventHandler(this.menuGen2SelRec_Click);
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
          // btnClear
          // 
          this.btnClear.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
          this.btnClear.Location = new System.Drawing.Point(133, 233);
          this.btnClear.Name = "btnClear";
          this.btnClear.Size = new System.Drawing.Size(88, 23);
          this.btnClear.TabIndex = 22;
          this.btnClear.Text = "Clear Response";
          this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
          // 
          // Tag_Type
          // 
          this.Tag_Type.Text = "TagType";
          this.Tag_Type.Width = 79;
          // 
          // TAGID
          // 
          this.TAGID.Text = "Tag ID";
          this.TAGID.Width = 156;
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
          this.listView1.Size = new System.Drawing.Size(202, 202);
          this.listView1.TabIndex = 2;
          this.listView1.View = System.Windows.Forms.View.Details;
          // 
          // Time_Stamp
          // 
          this.Time_Stamp.Text = "TimeStamp";
          this.Time_Stamp.Width = 60;
          // 
          // Antenna
          // 
          this.Antenna.Text = "Antenna";
          this.Antenna.Width = 60;
          // 
          // tabPage2
          // 
          this.tabPage2.Controls.Add(this.listView2);
          this.tabPage2.Location = new System.Drawing.Point(4, 22);
          this.tabPage2.Name = "tabPage2";
          this.tabPage2.Size = new System.Drawing.Size(202, 202);
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
          this.listView2.Size = new System.Drawing.Size(202, 202);
          this.listView2.TabIndex = 3;
          this.listView2.View = System.Windows.Forms.View.Details;
          // 
          // columnHeader1
          // 
          this.columnHeader1.Text = "ColumnHeader";
          this.columnHeader1.Width = 222;
          // 
          // tabControl1
          // 
          this.tabControl1.Controls.Add(this.tabPage2);
          this.tabControl1.Controls.Add(this.tabPage1);
          this.tabControl1.Controls.Add(this.tabTrigger);
          this.tabControl1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
          this.tabControl1.Location = new System.Drawing.Point(9, 2);
          this.tabControl1.Name = "tabControl1";
          this.tabControl1.SelectedIndex = 0;
          this.tabControl1.Size = new System.Drawing.Size(210, 228);
          this.tabControl1.TabIndex = 21;
          // 
          // tabPage1
          // 
          this.tabPage1.Controls.Add(this.listView1);
          this.tabPage1.Location = new System.Drawing.Point(4, 22);
          this.tabPage1.Name = "tabPage1";
          this.tabPage1.Size = new System.Drawing.Size(202, 202);
          this.tabPage1.Text = "Live Tags";
          // 
          // tabTrigger
          // 
          this.tabTrigger.Controls.Add(this.lblTrig);
          this.tabTrigger.Controls.Add(this.label1);
          this.tabTrigger.Location = new System.Drawing.Point(4, 22);
          this.tabTrigger.Name = "tabTrigger";
          this.tabTrigger.Size = new System.Drawing.Size(202, 202);
          this.tabTrigger.Text = "Trigger";
          // 
          // lblTrig
          // 
          this.lblTrig.Location = new System.Drawing.Point(99, 31);
          this.lblTrig.Name = "lblTrig";
          this.lblTrig.Size = new System.Drawing.Size(100, 20);
          // 
          // label1
          // 
          this.label1.Location = new System.Drawing.Point(3, 31);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(84, 20);
          this.label1.Text = "TriggerState";
          // 
          // lblConnect
          // 
          this.lblConnect.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
          this.lblConnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
          this.lblConnect.Location = new System.Drawing.Point(0, 238);
          this.lblConnect.Name = "lblConnect";
          this.lblConnect.Size = new System.Drawing.Size(62, 18);
          this.lblConnect.Text = "Connected";
          // 
          // MainFormMobil
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
          this.AutoScroll = true;
          this.ClientSize = new System.Drawing.Size(227, 287);
          this.Controls.Add(this.lblReaderMode);
          this.Controls.Add(this.btnClear);
          this.Controls.Add(this.tabControl1);
          this.Controls.Add(this.lblConnect);
          this.Menu = this.mainMenu1;
          this.Name = "MainFormMobil";
          this.Text = "CS_RFID2_Sample ";
          this.Load += new System.EventHandler(this.Form1_Load);
          this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
          this.tabPage2.ResumeLayout(false);
          this.tabControl1.ResumeLayout(false);
          this.tabPage1.ResumeLayout(false);
          this.tabTrigger.ResumeLayout(false);
          this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuCapability;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuReaderInfo;
        private System.Windows.Forms.MenuItem menuAutoMode;
        private System.Windows.Forms.MenuItem menuReaderMode;
        private System.Windows.Forms.MenuItem menuOnDemand;
        private System.Windows.Forms.MenuItem menuExit;
        private System.Windows.Forms.MenuItem menuReader;
        private System.Windows.Forms.MenuItem menuConnect;
        private System.Windows.Forms.MenuItem menuDisconnect;
        private System.Windows.Forms.MenuItem menuSetAntenna;
        private System.Windows.Forms.Label lblReaderMode;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuCommands;
        private System.Windows.Forms.MenuItem menuGettingTags;
        private System.Windows.Forms.MenuItem menuProgrammingTags;
        private System.Windows.Forms.MenuItem menuKillingTags;
        private System.Windows.Forms.MenuItem menuEraseTag;
        private System.Windows.Forms.MenuItem menuLocking;
        private System.Windows.Forms.MenuItem menuWrite;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ColumnHeader Tag_Type;
        private System.Windows.Forms.ColumnHeader TAGID;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblConnect;
        private System.Windows.Forms.MenuItem menuTriggerMode;
        private System.Windows.Forms.TabPage tabTrigger;
        private System.Windows.Forms.Label lblTrig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader Time_Stamp;
        private System.Windows.Forms.ColumnHeader Antenna;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuReadData;
        private System.Windows.Forms.MenuItem menuWriteTag;
      private System.Windows.Forms.MenuItem menuGen2Write;
      private System.Windows.Forms.MenuItem menuProg;
      private System.Windows.Forms.MenuItem menuGen2Kill;
      private System.Windows.Forms.MenuItem menuKillTags;
      private System.Windows.Forms.MenuItem menuGen2Lock;
      private System.Windows.Forms.MenuItem menuGen2Read;
      private System.Windows.Forms.MenuItem menuGetTags;
      private System.Windows.Forms.MenuItem menuSelectRecords;
      private System.Windows.Forms.MenuItem menuGen2SelRec;
    }
}