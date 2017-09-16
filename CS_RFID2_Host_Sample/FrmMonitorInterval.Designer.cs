namespace CS_RFID2_Host_Sample
{
    partial class FrmMonitorInterval
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
            this.grpMonitorInt = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.lblMonitorInt = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpMonitorInt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMonitorInt
            // 
            this.grpMonitorInt.Controls.Add(this.btnCancel);
            this.grpMonitorInt.Controls.Add(this.numericUpDown1);
            this.grpMonitorInt.Controls.Add(this.lblMonitorInt);
            this.grpMonitorInt.Controls.Add(this.btnOk);
            this.grpMonitorInt.Location = new System.Drawing.Point(10, 10);
            this.grpMonitorInt.Name = "grpMonitorInt";
            this.grpMonitorInt.Size = new System.Drawing.Size(272, 119);
            this.grpMonitorInt.TabIndex = 0;
            this.grpMonitorInt.TabStop = false;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.InterceptArrowKeys = false;
            this.numericUpDown1.Location = new System.Drawing.Point(138, 25);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(78, 20);
            this.numericUpDown1.TabIndex = 108;
            this.numericUpDown1.ThousandsSeparator = true;
            this.numericUpDown1.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // lblMonitorInt
            // 
            this.lblMonitorInt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonitorInt.Location = new System.Drawing.Point(15, 25);
            this.lblMonitorInt.Name = "lblMonitorInt";
            this.lblMonitorInt.Size = new System.Drawing.Size(117, 28);
            this.lblMonitorInt.TabIndex = 107;
            this.lblMonitorInt.Text = "Status Monitor Interval (in sec)";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnOk.Location = new System.Drawing.Point(54, 86);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(78, 24);
            this.btnOk.TabIndex = 105;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnCancel.Location = new System.Drawing.Point(138, 86);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 24);
            this.btnCancel.TabIndex = 109;
            this.btnCancel.Text = "Cancel";
            // 
            // FrmMonitorInterval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 143);
            this.Controls.Add(this.grpMonitorInt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMonitorInterval";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Status Monitor Interval";
            this.Load += new System.EventHandler(this.FrmMonitorInterval_Load);
            this.grpMonitorInt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMonitorInt;
        private System.Windows.Forms.Label lblMonitorInt;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnCancel;
    }
}