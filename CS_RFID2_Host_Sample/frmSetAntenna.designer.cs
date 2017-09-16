namespace CS_RFID2_Host_Sample
{
    partial class frmSetAntenna
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.chkGen2 = new System.Windows.Forms.CheckBox();
            this.chkClass0 = new System.Windows.Forms.CheckBox();
            this.chkClass1 = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.chkConnected = new System.Windows.Forms.CheckBox();
            this.txtTxPower = new System.Windows.Forms.TextBox();
            this.lblTx = new System.Windows.Forms.Label();
            this.lblAntenna = new System.Windows.Forms.Label();
            this.m_cmbAntennaName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRxPower = new System.Windows.Forms.TextBox();
            this.lblRxPower = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnCancel.Location = new System.Drawing.Point(195, 194);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 23);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtRxPower);
            this.groupBox1.Controls.Add(this.lblRxPower);
            this.groupBox1.Controls.Add(this.lblUnit);
            this.groupBox1.Controls.Add(this.chkGen2);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.chkClass0);
            this.groupBox1.Controls.Add(this.chkClass1);
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Controls.Add(this.chkConnected);
            this.groupBox1.Controls.Add(this.txtTxPower);
            this.groupBox1.Controls.Add(this.lblTx);
            this.groupBox1.Controls.Add(this.lblAntenna);
            this.groupBox1.Controls.Add(this.m_cmbAntennaName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 224);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            // 
            // lblUnit
            // 
            this.lblUnit.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblUnit.Location = new System.Drawing.Point(116, 139);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(22, 20);
            this.lblUnit.TabIndex = 52;
            this.lblUnit.Text = "DB";
            // 
            // chkGen2
            // 
            this.chkGen2.Checked = true;
            this.chkGen2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGen2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkGen2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkGen2.Location = new System.Drawing.Point(132, 75);
            this.chkGen2.Name = "chkGen2";
            this.chkGen2.Size = new System.Drawing.Size(113, 20);
            this.chkGen2.TabIndex = 51;
            this.chkGen2.Text = "CLASS1GEN2";
            // 
            // chkClass0
            // 
            this.chkClass0.Checked = true;
            this.chkClass0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClass0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkClass0.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkClass0.Location = new System.Drawing.Point(23, 101);
            this.chkClass0.Name = "chkClass0";
            this.chkClass0.Size = new System.Drawing.Size(90, 20);
            this.chkClass0.TabIndex = 50;
            this.chkClass0.Text = "CLASS0";
            // 
            // chkClass1
            // 
            this.chkClass1.Checked = true;
            this.chkClass1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClass1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkClass1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkClass1.Location = new System.Drawing.Point(23, 75);
            this.chkClass1.Name = "chkClass1";
            this.chkClass1.Size = new System.Drawing.Size(90, 20);
            this.chkClass1.TabIndex = 49;
            this.chkClass1.Text = "CLASS1";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnOk.Location = new System.Drawing.Point(103, 194);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(78, 23);
            this.btnOk.TabIndex = 45;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // chkConnected
            // 
            this.chkConnected.Checked = true;
            this.chkConnected.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConnected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkConnected.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkConnected.Location = new System.Drawing.Point(23, 168);
            this.chkConnected.Name = "chkConnected";
            this.chkConnected.Size = new System.Drawing.Size(85, 20);
            this.chkConnected.TabIndex = 44;
            this.chkConnected.Text = "Enable";
            // 
            // txtTxPower
            // 
            this.txtTxPower.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTxPower.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtTxPower.Location = new System.Drawing.Point(83, 136);
            this.txtTxPower.MaxLength = 3;
            this.txtTxPower.Name = "txtTxPower";
            this.txtTxPower.Size = new System.Drawing.Size(30, 22);
            this.txtTxPower.TabIndex = 43;
            this.txtTxPower.Text = "25";
            this.txtTxPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTx
            // 
            this.lblTx.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblTx.Location = new System.Drawing.Point(8, 139);
            this.lblTx.Name = "lblTx";
            this.lblTx.Size = new System.Drawing.Size(69, 20);
            this.lblTx.TabIndex = 46;
            this.lblTx.Text = "Tx Power:";
            // 
            // lblAntenna
            // 
            this.lblAntenna.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblAntenna.Location = new System.Drawing.Point(8, 22);
            this.lblAntenna.Name = "lblAntenna";
            this.lblAntenna.Size = new System.Drawing.Size(100, 20);
            this.lblAntenna.TabIndex = 47;
            this.lblAntenna.Text = "Antenna Name:";
            // 
            // m_cmbAntennaName
            // 
            this.m_cmbAntennaName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbAntennaName.Font = new System.Drawing.Font("Tahoma", 9F);
            this.m_cmbAntennaName.Location = new System.Drawing.Point(114, 19);
            this.m_cmbAntennaName.Name = "m_cmbAntennaName";
            this.m_cmbAntennaName.Size = new System.Drawing.Size(157, 22);
            this.m_cmbAntennaName.TabIndex = 42;
            this.m_cmbAntennaName.SelectedIndexChanged += new System.EventHandler(this.m_cmbAntennaName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(8, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 48;
            this.label1.Text = "Tag Types:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(255, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 20);
            this.label2.TabIndex = 55;
            this.label2.Text = "DB";
            // 
            // txtRxPower
            // 
            this.txtRxPower.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRxPower.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtRxPower.Location = new System.Drawing.Point(219, 136);
            this.txtRxPower.MaxLength = 3;
            this.txtRxPower.Name = "txtRxPower";
            this.txtRxPower.Size = new System.Drawing.Size(30, 22);
            this.txtRxPower.TabIndex = 53;
            this.txtRxPower.Text = "25";
            this.txtRxPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblRxPower
            // 
            this.lblRxPower.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblRxPower.Location = new System.Drawing.Point(144, 139);
            this.lblRxPower.Name = "lblRxPower";
            this.lblRxPower.Size = new System.Drawing.Size(69, 20);
            this.lblRxPower.TabIndex = 54;
            this.lblRxPower.Text = "Rx Power:";
            // 
            // frmSetAntenna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 243);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetAntenna";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Antenna Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSetAntenna_FormClosing);
            this.Load += new System.EventHandler(this.frmSetAntenna_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkGen2;
        private System.Windows.Forms.CheckBox chkClass0;
        private System.Windows.Forms.CheckBox chkClass1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox chkConnected;
        private System.Windows.Forms.TextBox txtTxPower;
        private System.Windows.Forms.Label lblTx;
        private System.Windows.Forms.Label lblAntenna;
        private System.Windows.Forms.ComboBox m_cmbAntennaName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRxPower;
        private System.Windows.Forms.Label lblRxPower;
    }
}