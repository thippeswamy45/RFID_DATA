namespace CS_RFID2_Host_Sample
{
    partial class frmReaderInfo
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
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSdkVersion = new System.Windows.Forms.Label();
            this.lblFwVersion = new System.Windows.Forms.Label();
            this.lblSerialno = new System.Windows.Forms.Label();
            this.lblNotifiPort = new System.Windows.Forms.Label();
            this.lblNotifiAddr = new System.Windows.Forms.Label();
            this.lblbHttpPort = new System.Windows.Forms.Label();
            this.lblbTcpPort = new System.Windows.Forms.Label();
            this.lblIpAddress = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblReadername = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.m_lblSerialNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_lblFWVersion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnOk.Location = new System.Drawing.Point(158, 310);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(91, 24);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSdkVersion);
            this.groupBox1.Controls.Add(this.lblFwVersion);
            this.groupBox1.Controls.Add(this.lblSerialno);
            this.groupBox1.Controls.Add(this.lblNotifiPort);
            this.groupBox1.Controls.Add(this.lblNotifiAddr);
            this.groupBox1.Controls.Add(this.lblbHttpPort);
            this.groupBox1.Controls.Add(this.lblbTcpPort);
            this.groupBox1.Controls.Add(this.lblIpAddress);
            this.groupBox1.Controls.Add(this.lblDesc);
            this.groupBox1.Controls.Add(this.lblModel);
            this.groupBox1.Controls.Add(this.lblReadername);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.m_lblSerialNumber);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.m_lblFWVersion);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 353);
            this.groupBox1.TabIndex = 93;
            this.groupBox1.TabStop = false;
            // 
            // lblSdkVersion
            // 
            this.lblSdkVersion.BackColor = System.Drawing.Color.White;
            this.lblSdkVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSdkVersion.Enabled = false;
            this.lblSdkVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSdkVersion.Location = new System.Drawing.Point(156, 268);
            this.lblSdkVersion.Name = "lblSdkVersion";
            this.lblSdkVersion.Size = new System.Drawing.Size(224, 19);
            this.lblSdkVersion.TabIndex = 103;
            this.lblSdkVersion.Text = "    ---------";
            this.lblSdkVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFwVersion
            // 
            this.lblFwVersion.BackColor = System.Drawing.Color.White;
            this.lblFwVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFwVersion.Enabled = false;
            this.lblFwVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFwVersion.Location = new System.Drawing.Point(156, 243);
            this.lblFwVersion.Name = "lblFwVersion";
            this.lblFwVersion.Size = new System.Drawing.Size(224, 19);
            this.lblFwVersion.TabIndex = 102;
            this.lblFwVersion.Text = "    ---------";
            this.lblFwVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSerialno
            // 
            this.lblSerialno.BackColor = System.Drawing.Color.White;
            this.lblSerialno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSerialno.Enabled = false;
            this.lblSerialno.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialno.Location = new System.Drawing.Point(156, 218);
            this.lblSerialno.Name = "lblSerialno";
            this.lblSerialno.Size = new System.Drawing.Size(224, 19);
            this.lblSerialno.TabIndex = 101;
            this.lblSerialno.Text = "    ---------";
            this.lblSerialno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNotifiPort
            // 
            this.lblNotifiPort.BackColor = System.Drawing.Color.White;
            this.lblNotifiPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNotifiPort.Enabled = false;
            this.lblNotifiPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotifiPort.Location = new System.Drawing.Point(156, 193);
            this.lblNotifiPort.Name = "lblNotifiPort";
            this.lblNotifiPort.Size = new System.Drawing.Size(224, 19);
            this.lblNotifiPort.TabIndex = 100;
            this.lblNotifiPort.Text = "    ---------";
            this.lblNotifiPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNotifiAddr
            // 
            this.lblNotifiAddr.BackColor = System.Drawing.Color.White;
            this.lblNotifiAddr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNotifiAddr.Enabled = false;
            this.lblNotifiAddr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotifiAddr.Location = new System.Drawing.Point(156, 168);
            this.lblNotifiAddr.Name = "lblNotifiAddr";
            this.lblNotifiAddr.Size = new System.Drawing.Size(224, 19);
            this.lblNotifiAddr.TabIndex = 99;
            this.lblNotifiAddr.Text = "    ---------";
            this.lblNotifiAddr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblbHttpPort
            // 
            this.lblbHttpPort.BackColor = System.Drawing.Color.White;
            this.lblbHttpPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblbHttpPort.Enabled = false;
            this.lblbHttpPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbHttpPort.Location = new System.Drawing.Point(157, 144);
            this.lblbHttpPort.Name = "lblbHttpPort";
            this.lblbHttpPort.Size = new System.Drawing.Size(224, 19);
            this.lblbHttpPort.TabIndex = 98;
            this.lblbHttpPort.Text = "    ---------";
            this.lblbHttpPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblbTcpPort
            // 
            this.lblbTcpPort.BackColor = System.Drawing.Color.White;
            this.lblbTcpPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblbTcpPort.Enabled = false;
            this.lblbTcpPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbTcpPort.Location = new System.Drawing.Point(157, 120);
            this.lblbTcpPort.Name = "lblbTcpPort";
            this.lblbTcpPort.Size = new System.Drawing.Size(224, 19);
            this.lblbTcpPort.TabIndex = 97;
            this.lblbTcpPort.Text = "    ---------";
            this.lblbTcpPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIpAddress
            // 
            this.lblIpAddress.BackColor = System.Drawing.Color.White;
            this.lblIpAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIpAddress.Enabled = false;
            this.lblIpAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIpAddress.Location = new System.Drawing.Point(157, 96);
            this.lblIpAddress.Name = "lblIpAddress";
            this.lblIpAddress.Size = new System.Drawing.Size(224, 19);
            this.lblIpAddress.TabIndex = 96;
            this.lblIpAddress.Text = "    ---------";
            this.lblIpAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.Color.White;
            this.lblDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDesc.Enabled = false;
            this.lblDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.Location = new System.Drawing.Point(157, 72);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(224, 19);
            this.lblDesc.TabIndex = 95;
            this.lblDesc.Text = "    ---------";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblModel
            // 
            this.lblModel.BackColor = System.Drawing.Color.White;
            this.lblModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblModel.Enabled = false;
            this.lblModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModel.Location = new System.Drawing.Point(157, 48);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(224, 19);
            this.lblModel.TabIndex = 94;
            this.lblModel.Text = "    ---------";
            this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblReadername
            // 
            this.lblReadername.BackColor = System.Drawing.Color.White;
            this.lblReadername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReadername.Enabled = false;
            this.lblReadername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReadername.Location = new System.Drawing.Point(157, 24);
            this.lblReadername.Name = "lblReadername";
            this.lblReadername.Size = new System.Drawing.Size(224, 19);
            this.lblReadername.TabIndex = 93;
            this.lblReadername.Text = "    ---------";
            this.lblReadername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(21, 171);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 19);
            this.label9.TabIndex = 106;
            this.label9.Text = "Notification Address";
            // 
            // m_lblSerialNumber
            // 
            this.m_lblSerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblSerialNumber.Location = new System.Drawing.Point(21, 221);
            this.m_lblSerialNumber.Name = "m_lblSerialNumber";
            this.m_lblSerialNumber.Size = new System.Drawing.Size(117, 19);
            this.m_lblSerialNumber.TabIndex = 113;
            this.m_lblSerialNumber.Text = "Serial Number";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 272);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 19);
            this.label1.TabIndex = 104;
            this.label1.Text = "SDK Version";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 19);
            this.label2.TabIndex = 114;
            this.label2.Text = "Reader Name";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 19);
            this.label3.TabIndex = 105;
            this.label3.Text = "Notification Port";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 19);
            this.label4.TabIndex = 112;
            this.label4.Text = "Model ";
            // 
            // m_lblFWVersion
            // 
            this.m_lblFWVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblFWVersion.Location = new System.Drawing.Point(21, 246);
            this.m_lblFWVersion.Name = "m_lblFWVersion";
            this.m_lblFWVersion.Size = new System.Drawing.Size(117, 19);
            this.m_lblFWVersion.TabIndex = 107;
            this.m_lblFWVersion.Text = "FW Version";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 19);
            this.label5.TabIndex = 111;
            this.label5.Text = "Description";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(21, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 19);
            this.label8.TabIndex = 108;
            this.label8.Text = "HTTP Port";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(21, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 19);
            this.label6.TabIndex = 110;
            this.label6.Text = "IP Address";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(21, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 19);
            this.label7.TabIndex = 109;
            this.label7.Text = "TCP Port";
            // 
            // frmReaderInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 374);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReaderInfo";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reader Information";
            this.Load += new System.EventHandler(this.frmReaderInfo_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSdkVersion;
        private System.Windows.Forms.Label lblFwVersion;
        private System.Windows.Forms.Label lblSerialno;
        private System.Windows.Forms.Label lblNotifiPort;
        private System.Windows.Forms.Label lblNotifiAddr;
        private System.Windows.Forms.Label lblbHttpPort;
        private System.Windows.Forms.Label lblbTcpPort;
        private System.Windows.Forms.Label lblIpAddress;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblReadername;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label m_lblSerialNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label m_lblFWVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}