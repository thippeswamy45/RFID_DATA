using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CS_RFID2_Host_Sample
{
	/// <summary>
	/// Summary description for FrmAbout.
	/// </summary>
	public class FrmAbout : System.Windows.Forms.Form
    {
        private GroupBox groupBox1;
        private LinkLabel m_llblWebSiteLink;
        private Button m_btnOK;
        private Label m_lblReaderVersion;
        private Label m_lblCopyRight;
        private Label m_lblVersion;
        private Label m_lblApplicationName;
        private PictureBox m_pbxIcon;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmAbout()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_llblWebSiteLink = new System.Windows.Forms.LinkLabel();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_lblReaderVersion = new System.Windows.Forms.Label();
            this.m_lblCopyRight = new System.Windows.Forms.Label();
            this.m_lblVersion = new System.Windows.Forms.Label();
            this.m_lblApplicationName = new System.Windows.Forms.Label();
            this.m_pbxIcon = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_llblWebSiteLink);
            this.groupBox1.Controls.Add(this.m_btnOK);
            this.groupBox1.Controls.Add(this.m_lblReaderVersion);
            this.groupBox1.Controls.Add(this.m_lblCopyRight);
            this.groupBox1.Controls.Add(this.m_lblVersion);
            this.groupBox1.Controls.Add(this.m_lblApplicationName);
            this.groupBox1.Controls.Add(this.m_pbxIcon);
            this.groupBox1.Location = new System.Drawing.Point(10, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 258);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // m_llblWebSiteLink
            // 
            this.m_llblWebSiteLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_llblWebSiteLink.Location = new System.Drawing.Point(125, 181);
            this.m_llblWebSiteLink.Name = "m_llblWebSiteLink";
            this.m_llblWebSiteLink.Size = new System.Drawing.Size(183, 24);
            this.m_llblWebSiteLink.TabIndex = 7;
            this.m_llblWebSiteLink.TabStop = true;
            this.m_llblWebSiteLink.Text = "http://www.motorola.com/";
            this.m_llblWebSiteLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_llblWebSiteLink_LinkClicked);
            // 
            // m_btnOK
            // 
            this.m_btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnOK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnOK.ForeColor = System.Drawing.Color.Black;
            this.m_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOK.Image")));
            this.m_btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnOK.Location = new System.Drawing.Point(177, 209);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(78, 38);
            this.m_btnOK.TabIndex = 6;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnOK.Click += new System.EventHandler(this.m_btnOK_Click);
            // 
            // m_lblReaderVersion
            // 
            this.m_lblReaderVersion.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblReaderVersion.Location = new System.Drawing.Point(49, 158);
            this.m_lblReaderVersion.Name = "m_lblReaderVersion";
            this.m_lblReaderVersion.Size = new System.Drawing.Size(295, 24);
            this.m_lblReaderVersion.TabIndex = 4;
            this.m_lblReaderVersion.Text = "RFID Device Models  :XR400, XR440, XR450, XR480, RD5000";
            // 
            // m_lblCopyRight
            // 
            this.m_lblCopyRight.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblCopyRight.Location = new System.Drawing.Point(136, 115);
            this.m_lblCopyRight.Name = "m_lblCopyRight";
            this.m_lblCopyRight.Size = new System.Drawing.Size(271, 24);
            this.m_lblCopyRight.TabIndex = 3;
            this.m_lblCopyRight.Text = "?Copyright 2007 Motorola, Inc. All rights reserved.";
            // 
            // m_lblVersion
            // 
            this.m_lblVersion.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblVersion.Location = new System.Drawing.Point(15, 110);
            this.m_lblVersion.Name = "m_lblVersion";
            this.m_lblVersion.Size = new System.Drawing.Size(127, 24);
            this.m_lblVersion.TabIndex = 2;
            this.m_lblVersion.Text = "Version 1.0";
            this.m_lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lblApplicationName
            // 
            this.m_lblApplicationName.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblApplicationName.Location = new System.Drawing.Point(91, 79);
            this.m_lblApplicationName.Name = "m_lblApplicationName";
            this.m_lblApplicationName.Size = new System.Drawing.Size(246, 32);
            this.m_lblApplicationName.TabIndex = 1;
            this.m_lblApplicationName.Text = "CS_RFID2_Host_Sample";
            this.m_lblApplicationName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_pbxIcon
            // 
            this.m_pbxIcon.Image = global::CS_RFID2_Host_Sample.Properties.Resources.MotorolaLogo11;
            this.m_pbxIcon.Location = new System.Drawing.Point(170, 9);
            this.m_pbxIcon.Name = "m_pbxIcon";
            this.m_pbxIcon.Size = new System.Drawing.Size(91, 65);
            this.m_pbxIcon.TabIndex = 8;
            this.m_pbxIcon.TabStop = false;
            // 
            // FrmAbout
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(438, 271);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.FrmAbout_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_pbxIcon)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void m_btnOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            try
            {
                m_llblWebSiteLink.Links.Add(0, m_llblWebSiteLink.Text.Length, m_llblWebSiteLink.Text);
                m_lblVersion.Text = "Version " + ClsReader.SDKVersion;
            }
            catch { }
        }

        private void m_llblWebSiteLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
            }
            catch { }
        }
	}
}
