namespace CS_RFID3Sample5
{
    partial class CapabilitiesForm
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
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.capabilitiesView = new System.Windows.Forms.ListView();
            this.CapabilityCol = new System.Windows.Forms.ColumnHeader();
            this.ValueCol = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // capabilitiesView
            // 
            this.capabilitiesView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.capabilitiesView.Columns.Add(this.CapabilityCol);
            this.capabilitiesView.Columns.Add(this.ValueCol);
            this.capabilitiesView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.capabilitiesView.FullRowSelect = true;
            listViewItem1.Text = "Reader ID";
            listViewItem2.Text = "Firmware Version";
            listViewItem3.Text = "Model Name";
            listViewItem4.Text = "No. of Antennas";
            listViewItem5.Text = "No. of GPI";
            listViewItem6.Text = "No. of GPIO";
            listViewItem7.Text = "Max Ops in Access Sequence";
            listViewItem8.Text = "Max No. Of Pre-Filters";
            listViewItem9.Text = "Country Code";
            listViewItem10.Text = "Communication Standard";
            listViewItem11.Text = "UTC Clock";
            listViewItem12.Text = "Block Erase";
            listViewItem13.Text = "Block Write";
            listViewItem14.Text = "Block Permalock";
            listViewItem15.Text = "Recommission";
            listViewItem16.Text = "Write UMI";
            listViewItem17.Text = "State-aware Singulation";
            listViewItem18.Text = "Tag Event Reporting";
            listViewItem19.Text = "RSSI Filtering";
            this.capabilitiesView.Items.Add(listViewItem1);
            this.capabilitiesView.Items.Add(listViewItem2);
            this.capabilitiesView.Items.Add(listViewItem3);
            this.capabilitiesView.Items.Add(listViewItem4);
            this.capabilitiesView.Items.Add(listViewItem5);
            this.capabilitiesView.Items.Add(listViewItem6);
            this.capabilitiesView.Items.Add(listViewItem7);
            this.capabilitiesView.Items.Add(listViewItem8);
            this.capabilitiesView.Items.Add(listViewItem9);
            this.capabilitiesView.Items.Add(listViewItem10);
            this.capabilitiesView.Items.Add(listViewItem11);
            this.capabilitiesView.Items.Add(listViewItem12);
            this.capabilitiesView.Items.Add(listViewItem13);
            this.capabilitiesView.Items.Add(listViewItem14);
            this.capabilitiesView.Items.Add(listViewItem15);
            this.capabilitiesView.Items.Add(listViewItem16);
            this.capabilitiesView.Items.Add(listViewItem17);
            this.capabilitiesView.Items.Add(listViewItem18);
            this.capabilitiesView.Items.Add(listViewItem19);
            this.capabilitiesView.Location = new System.Drawing.Point(3, 0);
            this.capabilitiesView.Name = "capabilitiesView";
            this.capabilitiesView.Size = new System.Drawing.Size(234, 214);
            this.capabilitiesView.TabIndex = 2;
            this.capabilitiesView.View = System.Windows.Forms.View.Details;
            // 
            // CapabilityCol
            // 
            this.CapabilityCol.Text = "Capability";
            this.CapabilityCol.Width = 119;
            // 
            // ValueCol
            // 
            this.ValueCol.Text = "Value";
            this.ValueCol.Width = 100;
            // 
            // CapabilitiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 214);
            this.Controls.Add(this.capabilitiesView);
            this.Name = "CapabilitiesForm";
            this.Text = "Capabilities";
            this.Load += new System.EventHandler(this.CapabilitiesForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView capabilitiesView;
        private System.Windows.Forms.ColumnHeader CapabilityCol;
        private System.Windows.Forms.ColumnHeader ValueCol;
    }
}