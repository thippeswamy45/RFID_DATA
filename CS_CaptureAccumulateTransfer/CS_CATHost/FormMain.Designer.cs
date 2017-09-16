namespace CS_CATHost
{
    partial class FormMain
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
            this.statusBar1 = new System.Windows.Forms.StatusStrip();
            this.statusBar2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.statusStripTop = new System.Windows.Forms.StatusStrip();
            this.StatusBarTop1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusBarTop2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar1.SuspendLayout();
            this.statusStripTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBar2});
            this.statusBar1.Location = new System.Drawing.Point(0, 241);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(496, 22);
            this.statusBar1.SizingGrip = false;
            this.statusBar1.TabIndex = 1;
            this.statusBar1.Text = "statusStrip1";
            // 
            // statusBar2
            // 
            this.statusBar2.Name = "statusBar2";
            this.statusBar2.Size = new System.Drawing.Size(0, 17);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(4, 23);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(488, 207);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "Device IP";
            this.columnHeader1.Width = 85;
            // 
            // columnHeader2
            // 
            this.columnHeader2.DisplayIndex = 2;
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.Text = "Date Time Sent";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.DisplayIndex = 1;
            this.columnHeader3.Name = "columnHeader3";
            this.columnHeader3.Text = "Data Received";
            this.columnHeader3.Width = 195;
            // 
            // statusStripTop
            // 
            this.statusStripTop.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.statusStripTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStripTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBarTop1,
            this.StatusBarTop2});
            this.statusStripTop.Location = new System.Drawing.Point(0, 0);
            this.statusStripTop.Name = "statusStripTop";
            this.statusStripTop.Size = new System.Drawing.Size(496, 24);
            this.statusStripTop.SizingGrip = false;
            this.statusStripTop.TabIndex = 3;
            this.statusStripTop.Text = "statusStrip1";
            // 
            // StatusBarTop1
            // 
            this.StatusBarTop1.AutoSize = false;
            this.StatusBarTop1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusBarTop1.Name = "StatusBarTop1";
            this.StatusBarTop1.Size = new System.Drawing.Size(250, 19);
            this.StatusBarTop1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusBarTop2
            // 
            this.StatusBarTop2.AutoSize = false;
            this.StatusBarTop2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusBarTop2.Name = "StatusBarTop2";
            this.StatusBarTop2.Size = new System.Drawing.Size(230, 19);
            this.StatusBarTop2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 263);
            this.Controls.Add(this.statusStripTop);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.statusBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Capture Accumulate  Transfer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusBar1.ResumeLayout(false);
            this.statusBar1.PerformLayout();
            this.statusStripTop.ResumeLayout(false);
            this.statusStripTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar1;
        private System.Windows.Forms.ToolStripStatusLabel statusBar2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.StatusStrip statusStripTop;
        private System.Windows.Forms.ToolStripStatusLabel StatusBarTop1;
        private System.Windows.Forms.ToolStripStatusLabel StatusBarTop2;
    }
}

