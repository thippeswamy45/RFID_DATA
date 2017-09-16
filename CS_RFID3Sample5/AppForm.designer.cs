namespace CS_RFID3Sample5
{
    partial class AppForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu appFormMenu;

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
            this.appFormMenu = new System.Windows.Forms.MainMenu();
            this.connectionMenuItem = new System.Windows.Forms.MenuItem();
            this.sampleAppMenuItem = new System.Windows.Forms.MenuItem();
            this.capMenuItem = new System.Windows.Forms.MenuItem();
            this.configMenuItem = new System.Windows.Forms.MenuItem();
            this.ResetMenuItem = new System.Windows.Forms.MenuItem();
            this.operationMenuItem = new System.Windows.Forms.MenuItem();
            this.accessMenuItem = new System.Windows.Forms.MenuItem();
            this.readMenuItem = new System.Windows.Forms.MenuItem();
            this.writeMenuItem = new System.Windows.Forms.MenuItem();
            this.lockMenuItem = new System.Windows.Forms.MenuItem();
            this.killMenuItem = new System.Windows.Forms.MenuItem();
            this.blockWriteMenuItem = new System.Windows.Forms.MenuItem();
            this.blockEraseMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.helpMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem31 = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.inventoryList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.dataContextMenu = new System.Windows.Forms.ContextMenu();
            this.tagDataMenuItem = new System.Windows.Forms.MenuItem();
            this.readContextMenuItem = new System.Windows.Forms.MenuItem();
            this.writeContextMenuItem = new System.Windows.Forms.MenuItem();
            this.lockContextMenuItem = new System.Windows.Forms.MenuItem();
            this.killContextMenuItem = new System.Windows.Forms.MenuItem();
            this.blockEraseContextMenuItem = new System.Windows.Forms.MenuItem();
            this.blockWriteContextMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.clearReportContextMenuItem = new System.Windows.Forms.MenuItem();
            this.memBank_CB = new System.Windows.Forms.ComboBox();
            this.readButton = new System.Windows.Forms.Button();
            this.readTimeLabel = new System.Windows.Forms.Label();
            this.totalTagLabel = new System.Windows.Forms.Label();
            this.totalTagValueLabel = new System.Windows.Forms.Label();
            this.readTimeValueLabel = new System.Windows.Forms.Label();
            this.functionCallStatusLabel = new Microsoft.WindowsCE.Forms.Notification();
            this.SuspendLayout();
            // 
            // appFormMenu
            // 
            this.appFormMenu.MenuItems.Add(this.connectionMenuItem);
            this.appFormMenu.MenuItems.Add(this.sampleAppMenuItem);
            // 
            // connectionMenuItem
            // 
            this.connectionMenuItem.Text = "Connection";
            this.connectionMenuItem.Click += new System.EventHandler(this.connectionMenuItem_Click);
            // 
            // sampleAppMenuItem
            // 
            this.sampleAppMenuItem.MenuItems.Add(this.capMenuItem);
            this.sampleAppMenuItem.MenuItems.Add(this.configMenuItem);
            this.sampleAppMenuItem.MenuItems.Add(this.operationMenuItem);
            this.sampleAppMenuItem.MenuItems.Add(this.menuItem1);
            this.sampleAppMenuItem.MenuItems.Add(this.menuItem31);
            this.sampleAppMenuItem.MenuItems.Add(this.exitMenuItem);
            this.sampleAppMenuItem.Text = "Menu";
            // 
            // capMenuItem
            // 
            this.capMenuItem.Text = "Capabilities...";
            this.capMenuItem.Click += new System.EventHandler(this.capMenuItem_Click);
            // 
            // configMenuItem
            // 
            this.configMenuItem.MenuItems.Add(this.ResetMenuItem);
            this.configMenuItem.Text = "Config";
            // 
            // ResetMenuItem
            // 
            this.ResetMenuItem.Text = "Reset To Factory Default";
            this.ResetMenuItem.Click += new System.EventHandler(this.ResetMenuItem_Click);
            // 
            // operationMenuItem
            // 
            this.operationMenuItem.MenuItems.Add(this.accessMenuItem);
            this.operationMenuItem.Text = "Operations";
            // 
            // accessMenuItem
            // 
            this.accessMenuItem.MenuItems.Add(this.readMenuItem);
            this.accessMenuItem.MenuItems.Add(this.writeMenuItem);
            this.accessMenuItem.MenuItems.Add(this.lockMenuItem);
            this.accessMenuItem.MenuItems.Add(this.killMenuItem);
            this.accessMenuItem.MenuItems.Add(this.blockWriteMenuItem);
            this.accessMenuItem.MenuItems.Add(this.blockEraseMenuItem);
            this.accessMenuItem.Text = "Access";
            // 
            // readMenuItem
            // 
            this.readMenuItem.Text = "Read";
            this.readMenuItem.Click += new System.EventHandler(this.readMenuItem_Click);
            // 
            // writeMenuItem
            // 
            this.writeMenuItem.Text = "Write";
            this.writeMenuItem.Click += new System.EventHandler(this.writeMenuItem_Click);
            // 
            // lockMenuItem
            // 
            this.lockMenuItem.Text = "Lock";
            this.lockMenuItem.Click += new System.EventHandler(this.lockMenuItem_Click);
            // 
            // killMenuItem
            // 
            this.killMenuItem.Text = "Kill";
            this.killMenuItem.Click += new System.EventHandler(this.killMenuItem_Click);
            // 
            // blockWriteMenuItem
            // 
            this.blockWriteMenuItem.Text = "Block Write";
            this.blockWriteMenuItem.Click += new System.EventHandler(this.blockWriteMenuItem_Click);
            // 
            // blockEraseMenuItem
            // 
            this.blockEraseMenuItem.Text = "Block Erase";
            this.blockEraseMenuItem.Click += new System.EventHandler(this.blockEraseMenuItem_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.helpMenuItem);
            this.menuItem1.Text = "Help";
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.Text = "About";
            this.helpMenuItem.Click += new System.EventHandler(this.helpMenuItem_Click);
            // 
            // menuItem31
            // 
            this.menuItem31.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // inventoryList
            // 
            this.inventoryList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.inventoryList.Columns.Add(this.columnHeader1);
            this.inventoryList.Columns.Add(this.columnHeader8);
            this.inventoryList.Columns.Add(this.columnHeader2);
            this.inventoryList.Columns.Add(this.columnHeader3);
            this.inventoryList.Columns.Add(this.columnHeader4);
            this.inventoryList.Columns.Add(this.columnHeader5);
            this.inventoryList.Columns.Add(this.columnHeader6);
            this.inventoryList.Columns.Add(this.columnHeader7);
            this.inventoryList.ContextMenu = this.dataContextMenu;
            this.inventoryList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.inventoryList.FullRowSelect = true;
            this.inventoryList.Location = new System.Drawing.Point(3, 32);
            this.inventoryList.Name = "inventoryList";
            this.inventoryList.Size = new System.Drawing.Size(236, 133);
            this.inventoryList.TabIndex = 3;
            this.inventoryList.View = System.Windows.Forms.View.Details;
            this.inventoryList.ItemActivate += new System.EventHandler(this.inventoryList_ItemActivate);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "EPC ID";
            this.columnHeader1.Width = 156;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Ant";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 35;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Ct";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 32;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "RSSI";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 36;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "PC Bits";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 47;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Memory Bank Data";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 185;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "MB";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 75;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Offset";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 46;
            // 
            // dataContextMenu
            // 
            this.dataContextMenu.MenuItems.Add(this.tagDataMenuItem);
            this.dataContextMenu.MenuItems.Add(this.readContextMenuItem);
            this.dataContextMenu.MenuItems.Add(this.writeContextMenuItem);
            this.dataContextMenu.MenuItems.Add(this.lockContextMenuItem);
            this.dataContextMenu.MenuItems.Add(this.killContextMenuItem);
            this.dataContextMenu.MenuItems.Add(this.blockEraseContextMenuItem);
            this.dataContextMenu.MenuItems.Add(this.blockWriteContextMenuItem);
            this.dataContextMenu.MenuItems.Add(this.menuItem3);
            this.dataContextMenu.MenuItems.Add(this.clearReportContextMenuItem);
            // 
            // tagDataMenuItem
            // 
            this.tagDataMenuItem.Text = "Tag Data";
            this.tagDataMenuItem.Click += new System.EventHandler(this.tagDataMenuItem_Click);
            // 
            // readContextMenuItem
            // 
            this.readContextMenuItem.Text = "Read";
            this.readContextMenuItem.Click += new System.EventHandler(this.readContextMenuItem_Click);
            // 
            // writeContextMenuItem
            // 
            this.writeContextMenuItem.Text = "Write";
            this.writeContextMenuItem.Click += new System.EventHandler(this.writeContextMenuItem_Click);
            // 
            // lockContextMenuItem
            // 
            this.lockContextMenuItem.Text = "Lock";
            this.lockContextMenuItem.Click += new System.EventHandler(this.lockContextMenuItem_Click);
            // 
            // killContextMenuItem
            // 
            this.killContextMenuItem.Text = "Kill";
            this.killContextMenuItem.Click += new System.EventHandler(this.killContextMenuItem_Click);
            // 
            // blockEraseContextMenuItem
            // 
            this.blockEraseContextMenuItem.Text = "Block Write";
            this.blockEraseContextMenuItem.Click += new System.EventHandler(this.blockWriteContextMenuItem_Click);
            // 
            // blockWriteContextMenuItem
            // 
            this.blockWriteContextMenuItem.Text = "Block Erase";
            this.blockWriteContextMenuItem.Click += new System.EventHandler(this.blockEraseContextMenuItem_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "-";
            // 
            // clearReportContextMenuItem
            // 
            this.clearReportContextMenuItem.Text = "Clear Reports";
            this.clearReportContextMenuItem.Click += new System.EventHandler(this.clearReportMenuItem_Click);
            // 
            // memBank_CB
            // 
            this.memBank_CB.Enabled = false;
            this.memBank_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.memBank_CB.ForeColor = System.Drawing.Color.Navy;
            this.memBank_CB.Items.Add("NONE");
            this.memBank_CB.Items.Add("RESERVED");
            this.memBank_CB.Items.Add("EPC");
            this.memBank_CB.Items.Add("TID");
            this.memBank_CB.Items.Add("USER");
            this.memBank_CB.Location = new System.Drawing.Point(167, 6);
            this.memBank_CB.Name = "memBank_CB";
            this.memBank_CB.Size = new System.Drawing.Size(72, 20);
            this.memBank_CB.TabIndex = 2;
            this.memBank_CB.SelectedIndexChanged += new System.EventHandler(this.memBank_CB_SelectedIndexChanged);
            // 
            // readButton
            // 
            this.readButton.Enabled = false;
            this.readButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.readButton.Location = new System.Drawing.Point(3, 6);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(75, 20);
            this.readButton.TabIndex = 1;
            this.readButton.Text = "Start Reading";
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // readTimeLabel
            // 
            this.readTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.readTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.readTimeLabel.Location = new System.Drawing.Point(130, 170);
            this.readTimeLabel.Name = "readTimeLabel";
            this.readTimeLabel.Size = new System.Drawing.Size(61, 16);
            this.readTimeLabel.Text = "Read Time:";
            // 
            // totalTagLabel
            // 
            this.totalTagLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.totalTagLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.totalTagLabel.Location = new System.Drawing.Point(3, 170);
            this.totalTagLabel.Name = "totalTagLabel";
            this.totalTagLabel.Size = new System.Drawing.Size(59, 16);
            this.totalTagLabel.Text = "Total Tags: ";
            // 
            // totalTagValueLabel
            // 
            this.totalTagValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.totalTagValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.totalTagValueLabel.Location = new System.Drawing.Point(70, 170);
            this.totalTagValueLabel.Name = "totalTagValueLabel";
            this.totalTagValueLabel.Size = new System.Drawing.Size(41, 16);
            this.totalTagValueLabel.Text = "0(0)";
            // 
            // readTimeValueLabel
            // 
            this.readTimeValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.readTimeValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.readTimeValueLabel.Location = new System.Drawing.Point(199, 170);
            this.readTimeValueLabel.Name = "readTimeValueLabel";
            this.readTimeValueLabel.Size = new System.Drawing.Size(40, 12);
            this.readTimeValueLabel.Text = "0 Sec";
            // 
            // functionCallStatusLabel
            // 
            this.functionCallStatusLabel.Caption = "API Call Result";
            this.functionCallStatusLabel.Text = "Function Success";
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.readTimeValueLabel);
            this.Controls.Add(this.totalTagValueLabel);
            this.Controls.Add(this.totalTagLabel);
            this.Controls.Add(this.readTimeLabel);
            this.Controls.Add(this.memBank_CB);
            this.Controls.Add(this.inventoryList);
            this.Menu = this.appFormMenu;
            this.MinimizeBox = false;
            this.Name = "AppForm";
            this.Text = "CS_RFID3Sample5";
            this.Load += new System.EventHandler(this.AppForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.AppForm_Closing);
            this.ResumeLayout(false);

        }
        #endregion

        internal System.Windows.Forms.ListView inventoryList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ComboBox memBank_CB;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label readTimeLabel;
        private System.Windows.Forms.Label totalTagLabel;
        private System.Windows.Forms.Label totalTagValueLabel;
        private System.Windows.Forms.Label readTimeValueLabel;
        internal Microsoft.WindowsCE.Forms.Notification functionCallStatusLabel;
        private System.Windows.Forms.MenuItem sampleAppMenuItem;
        private System.Windows.Forms.MenuItem configMenuItem;
        private System.Windows.Forms.MenuItem ResetMenuItem;
        private System.Windows.Forms.MenuItem operationMenuItem;
        private System.Windows.Forms.MenuItem accessMenuItem;
        private System.Windows.Forms.MenuItem readMenuItem;
        private System.Windows.Forms.MenuItem writeMenuItem;
        private System.Windows.Forms.MenuItem lockMenuItem;
        private System.Windows.Forms.MenuItem killMenuItem;
        private System.Windows.Forms.MenuItem blockWriteMenuItem;
        private System.Windows.Forms.MenuItem blockEraseMenuItem;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem helpMenuItem;
        private System.Windows.Forms.MenuItem menuItem31;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.MenuItem clearReportContextMenuItem;
        private System.Windows.Forms.MenuItem menuItem3;
        internal System.Windows.Forms.ContextMenu dataContextMenu;
        private System.Windows.Forms.MenuItem tagDataMenuItem;
        private System.Windows.Forms.MenuItem readContextMenuItem;
        private System.Windows.Forms.MenuItem writeContextMenuItem;
        private System.Windows.Forms.MenuItem lockContextMenuItem;
        private System.Windows.Forms.MenuItem killContextMenuItem;
        private System.Windows.Forms.MenuItem blockEraseContextMenuItem;
        private System.Windows.Forms.MenuItem blockWriteContextMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.MenuItem capMenuItem;
        private System.Windows.Forms.MenuItem connectionMenuItem;
    }
}