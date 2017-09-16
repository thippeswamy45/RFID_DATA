namespace CS_RFID3Sample6
{
    partial class SystemInfoForm
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
            this.systemInfoView = new System.Windows.Forms.ListView();
            this.SystemCol = new System.Windows.Forms.ColumnHeader();
            this.ValueCol = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // systemInfoView
            // 
            this.systemInfoView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.systemInfoView.Columns.Add(this.SystemCol);
            this.systemInfoView.Columns.Add(this.ValueCol);
            this.systemInfoView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.systemInfoView.FullRowSelect = true;
            this.systemInfoView.Location = new System.Drawing.Point(0, 0);
            this.systemInfoView.Name = "systemInfoView";
            this.systemInfoView.Size = new System.Drawing.Size(240, 188);
            this.systemInfoView.TabIndex = 2;
            this.systemInfoView.View = System.Windows.Forms.View.Details;
            // 
            // SystemCol
            // 
            this.SystemCol.Text = "System";
            this.SystemCol.Width = 102;
            // 
            // ValueCol
            // 
            this.ValueCol.Text = "Value";
            this.ValueCol.Width = 116;
            // 
            // SystemInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.systemInfoView);
            this.Menu = this.mainMenu1;
            this.Name = "SystemInfoForm";
            this.Text = "System Information";
            this.Load += new System.EventHandler(this.SystemInfoForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView systemInfoView;
        private System.Windows.Forms.ColumnHeader SystemCol;
        private System.Windows.Forms.ColumnHeader ValueCol;
    }
}