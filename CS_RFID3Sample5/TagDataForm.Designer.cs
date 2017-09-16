namespace CS_RFID3Sample5
{
    partial class TagDataForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.tagDataView = new System.Windows.Forms.ListView();
            this.itemColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.valueColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // tagDataView
            // 
            this.tagDataView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tagDataView.Columns.Add(this.itemColumnHeader);
            this.tagDataView.Columns.Add(this.valueColumnHeader);
            this.tagDataView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagDataView.FullRowSelect = true;
            listViewItem1.Text = "TagID";
            listViewItem2.Text = "Antenna";
            listViewItem3.Text = "RSSI";
            listViewItem4.Text = "PC Bits";
            listViewItem5.Text = "Memory Bank";
            listViewItem6.Text = "Data";
            listViewItem7.Text = "Offset";
            listViewItem8.Text = "Length";
            this.tagDataView.Items.Add(listViewItem1);
            this.tagDataView.Items.Add(listViewItem2);
            this.tagDataView.Items.Add(listViewItem3);
            this.tagDataView.Items.Add(listViewItem4);
            this.tagDataView.Items.Add(listViewItem5);
            this.tagDataView.Items.Add(listViewItem6);
            this.tagDataView.Items.Add(listViewItem7);
            this.tagDataView.Items.Add(listViewItem8);
            this.tagDataView.Location = new System.Drawing.Point(0, 0);
            this.tagDataView.Name = "tagDataView";
            this.tagDataView.Size = new System.Drawing.Size(240, 188);
            this.tagDataView.TabIndex = 2;
            this.tagDataView.View = System.Windows.Forms.View.Details;
            // 
            // itemColumnHeader
            // 
            this.itemColumnHeader.Text = "Item";
            this.itemColumnHeader.Width = 86;
            // 
            // valueColumnHeader
            // 
            this.valueColumnHeader.Text = "Value";
            this.valueColumnHeader.Width = 142;
            // 
            // TagDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.tagDataView);
            this.Menu = this.mainMenu1;
            this.Name = "TagDataForm";
            this.Text = "Tag Data";
            this.Load += new System.EventHandler(this.TagDataForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView tagDataView;
        private System.Windows.Forms.ColumnHeader itemColumnHeader;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
    }
}