namespace CS_RFID3Sample6
{
    partial class RFModeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu;

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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.rfModelistView = new System.Windows.Forms.ListView();
            this.parameterHeader = new System.Windows.Forms.ColumnHeader();
            this.valueHeader1 = new System.Windows.Forms.ColumnHeader();
            this.rfModeTable_CB = new System.Windows.Forms.ComboBox();
            this.rfModeTablelabel = new System.Windows.Forms.Label();
            this.tari_TB = new System.Windows.Forms.TextBox();
            this.tariValueLabel = new System.Windows.Forms.Label();
            this.antenna_CB = new System.Windows.Forms.ComboBox();
            this.antennaIDLlabel = new System.Windows.Forms.Label();
            this.rfModeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rfModelistView
            // 
            this.rfModelistView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.rfModelistView.Columns.Add(this.parameterHeader);
            this.rfModelistView.Columns.Add(this.valueHeader1);
            this.rfModelistView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.rfModelistView.FullRowSelect = true;
            listViewItem1.Text = "Mode Identifier";
            listViewItem2.Text = "DR";
            listViewItem3.Text = "Bdr";
            listViewItem4.Text = "M";
            listViewItem5.Text = "Forward Link Modulation";
            listViewItem6.Text = "PIE";
            listViewItem7.Text = "Min Tari";
            listViewItem8.Text = "Max Tari";
            listViewItem9.Text = "Step tari";
            listViewItem10.Text = "Spectral Mask Indicator";
            listViewItem11.Text = "EPC HAG TCConformance";
            this.rfModelistView.Items.Add(listViewItem1);
            this.rfModelistView.Items.Add(listViewItem2);
            this.rfModelistView.Items.Add(listViewItem3);
            this.rfModelistView.Items.Add(listViewItem4);
            this.rfModelistView.Items.Add(listViewItem5);
            this.rfModelistView.Items.Add(listViewItem6);
            this.rfModelistView.Items.Add(listViewItem7);
            this.rfModelistView.Items.Add(listViewItem8);
            this.rfModelistView.Items.Add(listViewItem9);
            this.rfModelistView.Items.Add(listViewItem10);
            this.rfModelistView.Items.Add(listViewItem11);
            this.rfModelistView.Location = new System.Drawing.Point(3, 84);
            this.rfModelistView.Name = "rfModelistView";
            this.rfModelistView.Size = new System.Drawing.Size(232, 71);
            this.rfModelistView.TabIndex = 4;
            this.rfModelistView.View = System.Windows.Forms.View.Details;
            // 
            // parameterHeader
            // 
            this.parameterHeader.Text = "Parameter";
            this.parameterHeader.Width = 120;
            // 
            // valueHeader1
            // 
            this.valueHeader1.Text = "Value";
            this.valueHeader1.Width = 97;
            // 
            // rfModeTable_CB
            // 
            this.rfModeTable_CB.Location = new System.Drawing.Point(127, 56);
            this.rfModeTable_CB.Name = "rfModeTable_CB";
            this.rfModeTable_CB.Size = new System.Drawing.Size(108, 22);
            this.rfModeTable_CB.TabIndex = 2;
            this.rfModeTable_CB.SelectedIndexChanged += new System.EventHandler(this.rfModeTable_CB_SelectedIndexChanged);
            // 
            // rfModeTablelabel
            // 
            this.rfModeTablelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.rfModeTablelabel.Location = new System.Drawing.Point(3, 56);
            this.rfModeTablelabel.Name = "rfModeTablelabel";
            this.rfModeTablelabel.Size = new System.Drawing.Size(91, 22);
            this.rfModeTablelabel.Text = "RF Mode Table";
            // 
            // tari_TB
            // 
            this.tari_TB.Location = new System.Drawing.Point(127, 29);
            this.tari_TB.Name = "tari_TB";
            this.tari_TB.Size = new System.Drawing.Size(108, 21);
            this.tari_TB.TabIndex = 3;
            // 
            // tariValueLabel
            // 
            this.tariValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tariValueLabel.Location = new System.Drawing.Point(3, 29);
            this.tariValueLabel.Name = "tariValueLabel";
            this.tariValueLabel.Size = new System.Drawing.Size(91, 22);
            this.tariValueLabel.Text = "Tari Value";
            // 
            // antenna_CB
            // 
            this.antenna_CB.Location = new System.Drawing.Point(127, 3);
            this.antenna_CB.Name = "antenna_CB";
            this.antenna_CB.Size = new System.Drawing.Size(108, 22);
            this.antenna_CB.TabIndex = 1;
            this.antenna_CB.SelectedIndexChanged += new System.EventHandler(this.antenna_CB_SelectedIndexChanged);
            // 
            // antennaIDLlabel
            // 
            this.antennaIDLlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.antennaIDLlabel.Location = new System.Drawing.Point(3, 6);
            this.antennaIDLlabel.Name = "antennaIDLlabel";
            this.antennaIDLlabel.Size = new System.Drawing.Size(91, 22);
            this.antennaIDLlabel.Text = "Antenna ID";
            // 
            // rfModeButton
            // 
            this.rfModeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rfModeButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.rfModeButton.Location = new System.Drawing.Point(184, 161);
            this.rfModeButton.Name = "rfModeButton";
            this.rfModeButton.Size = new System.Drawing.Size(51, 20);
            this.rfModeButton.TabIndex = 18;
            this.rfModeButton.Text = "Apply";
            this.rfModeButton.Click += new System.EventHandler(this.rfModeButton_Click);
            // 
            // RFModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.rfModeButton);
            this.Controls.Add(this.rfModelistView);
            this.Controls.Add(this.rfModeTable_CB);
            this.Controls.Add(this.rfModeTablelabel);
            this.Controls.Add(this.tari_TB);
            this.Controls.Add(this.tariValueLabel);
            this.Controls.Add(this.antenna_CB);
            this.Controls.Add(this.antennaIDLlabel);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "RFModeForm";
            this.Text = "RF Mode";
            this.Load += new System.EventHandler(this.RFModeForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.RFModeForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView rfModelistView;
        private System.Windows.Forms.ColumnHeader parameterHeader;
        private System.Windows.Forms.ColumnHeader valueHeader1;
        internal System.Windows.Forms.ComboBox rfModeTable_CB;
        private System.Windows.Forms.Label rfModeTablelabel;
        internal System.Windows.Forms.TextBox tari_TB;
        private System.Windows.Forms.Label tariValueLabel;
        internal System.Windows.Forms.ComboBox antenna_CB;
        private System.Windows.Forms.Label antennaIDLlabel;
        private System.Windows.Forms.Button rfModeButton;
    }
}