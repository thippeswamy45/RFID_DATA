namespace CS_RFID3Sample6
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.capabilitiesView = new System.Windows.Forms.ListView();
            this.CapabilityCol = new System.Windows.Forms.ColumnHeader();
            this.ValueCol = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // capabilitiesView
            // 
            this.capabilitiesView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.capabilitiesView.Columns.Add(this.CapabilityCol);
            this.capabilitiesView.Columns.Add(this.ValueCol);
            this.capabilitiesView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.capabilitiesView.FullRowSelect = true;
            this.capabilitiesView.Location = new System.Drawing.Point(0, 0);
            this.capabilitiesView.Name = "capabilitiesView";
            this.capabilitiesView.Size = new System.Drawing.Size(240, 188);
            this.capabilitiesView.TabIndex = 2;
            this.capabilitiesView.View = System.Windows.Forms.View.Details;
            // 
            // CapabilityCol
            // 
            this.CapabilityCol.Text = "Capability";
            this.CapabilityCol.Width = 155;
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
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.capabilitiesView);
            this.Menu = this.mainMenu1;
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