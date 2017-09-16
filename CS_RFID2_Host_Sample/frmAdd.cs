using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

using Symbol.RFID2;
using System.Net;

//using WTSDK;

namespace CS_RFID2_Host_Sample
{
    /// <summary>
    /// Summary description for frmAdd.
    /// </summary>
    public class frmAdd : System.Windows.Forms.Form
    {

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Label m_lblReaderName;
      private System.Windows.Forms.Label m_lblDescription;
      private System.Windows.Forms.Label m_lblIpAddress;
      private System.Windows.Forms.Label m_lblPort;
      private System.Windows.Forms.TextBox m_txtDescription;
      private System.Windows.Forms.TextBox m_txtIPAddress;
      private System.Windows.Forms.TextBox m_txtPort;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.Button m_btnCancel;
      private System.Windows.Forms.Button m_btnAdd;
      private System.Windows.Forms.ComboBox m_cboModel;
      private System.Windows.Forms.Label m_lblModel;
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      private GroupBox groupBox4;
      private TextBox m_txtlNotifiParam2;
      private TextBox m_txtlNotifiParam1;
      private Label m_lblNotifiParam1;
      private Label m_lblNotifiParam2;
      private TextBox m_txtPassword;
      private TextBox m_txtUserName;
      private Label m_lblPassword;
      private Label m_lblUserName;
      private TextBox m_txtReaderName;
      private TextBox m_txtHttpPort;
      private Label m_lblHttpPort;


      private GroupBox groupBox3;



      private string txtReaderName = null;
      private string txtDescription = null;
      private string txtIPAddress = null;
      private string txtPort = null;
      private string txtHttpPort = null;
      private string txtlNotifiParam1 = null;
      private string txtlNotifiParam2 = null;
      private string txtUserName = null;
      private string txtPassword = null;
        
        public frmAdd()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            m_cboModel.SelectedIndex = 0;
            this.Text = "Add New Reader ";

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdd));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_txtHttpPort = new System.Windows.Forms.TextBox();
            this.m_lblHttpPort = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.m_txtlNotifiParam2 = new System.Windows.Forms.TextBox();
            this.m_txtlNotifiParam1 = new System.Windows.Forms.TextBox();
            this.m_lblNotifiParam1 = new System.Windows.Forms.Label();
            this.m_lblNotifiParam2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_txtPassword = new System.Windows.Forms.TextBox();
            this.m_txtUserName = new System.Windows.Forms.TextBox();
            this.m_lblPassword = new System.Windows.Forms.Label();
            this.m_lblUserName = new System.Windows.Forms.Label();
            this.m_lblModel = new System.Windows.Forms.Label();
            this.m_cboModel = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnAdd = new System.Windows.Forms.Button();
            this.m_txtPort = new System.Windows.Forms.TextBox();
            this.m_txtIPAddress = new System.Windows.Forms.TextBox();
            this.m_txtDescription = new System.Windows.Forms.TextBox();
            this.m_txtReaderName = new System.Windows.Forms.TextBox();
            this.m_lblPort = new System.Windows.Forms.Label();
            this.m_lblIpAddress = new System.Windows.Forms.Label();
            this.m_lblDescription = new System.Windows.Forms.Label();
            this.m_lblReaderName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_txtHttpPort);
            this.groupBox1.Controls.Add(this.m_lblHttpPort);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.m_lblModel);
            this.groupBox1.Controls.Add(this.m_cboModel);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.m_txtPort);
            this.groupBox1.Controls.Add(this.m_txtIPAddress);
            this.groupBox1.Controls.Add(this.m_txtDescription);
            this.groupBox1.Controls.Add(this.m_txtReaderName);
            this.groupBox1.Controls.Add(this.m_lblPort);
            this.groupBox1.Controls.Add(this.m_lblIpAddress);
            this.groupBox1.Controls.Add(this.m_lblDescription);
            this.groupBox1.Controls.Add(this.m_lblReaderName);
            this.groupBox1.Location = new System.Drawing.Point(21, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(508, 500);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // m_txtHttpPort
            // 
            this.m_txtHttpPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtHttpPort.Enabled = false;
            this.m_txtHttpPort.Location = new System.Drawing.Point(172, 205);
            this.m_txtHttpPort.MaxLength = 5;
            this.m_txtHttpPort.Name = "m_txtHttpPort";
            this.m_txtHttpPort.Size = new System.Drawing.Size(283, 20);
            this.m_txtHttpPort.TabIndex = 20;
            this.m_txtHttpPort.Text = "80";
            this.m_txtHttpPort.Validating += new System.ComponentModel.CancelEventHandler(this.m_txtReaderName_Validating);
            // 
            // m_lblHttpPort
            // 
            this.m_lblHttpPort.Location = new System.Drawing.Point(62, 207);
            this.m_lblHttpPort.Name = "m_lblHttpPort";
            this.m_lblHttpPort.Size = new System.Drawing.Size(112, 16);
            this.m_lblHttpPort.TabIndex = 21;
            this.m_lblHttpPort.Text = "HTTP Port";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.m_txtlNotifiParam2);
            this.groupBox4.Controls.Add(this.m_txtlNotifiParam1);
            this.groupBox4.Controls.Add(this.m_lblNotifiParam1);
            this.groupBox4.Controls.Add(this.m_lblNotifiParam2);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(45, 229);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(417, 87);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "  Notification Parameters";
            // 
            // m_txtlNotifiParam2
            // 
            this.m_txtlNotifiParam2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtlNotifiParam2.Location = new System.Drawing.Point(127, 55);
            this.m_txtlNotifiParam2.MaxLength = 5;
            this.m_txtlNotifiParam2.Name = "m_txtlNotifiParam2";
            this.m_txtlNotifiParam2.Size = new System.Drawing.Size(281, 21);
            this.m_txtlNotifiParam2.TabIndex = 6;
            this.m_txtlNotifiParam2.Text = "4000";
            this.m_txtlNotifiParam2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmAdd_KeyPress);
            this.m_txtlNotifiParam2.Validating += new System.ComponentModel.CancelEventHandler(this.m_txtReaderName_Validating);
            // 
            // m_txtlNotifiParam1
            // 
            this.m_txtlNotifiParam1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtlNotifiParam1.Location = new System.Drawing.Point(127, 28);
            this.m_txtlNotifiParam1.MaxLength = 16;
            this.m_txtlNotifiParam1.Name = "m_txtlNotifiParam1";
            this.m_txtlNotifiParam1.Size = new System.Drawing.Size(281, 21);
            this.m_txtlNotifiParam1.TabIndex = 5;
            this.m_txtlNotifiParam1.Validating += new System.ComponentModel.CancelEventHandler(this.m_txtReaderName_Validating);
            // 
            // m_lblNotifiParam1
            // 
            this.m_lblNotifiParam1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblNotifiParam1.Location = new System.Drawing.Point(17, 32);
            this.m_lblNotifiParam1.Name = "m_lblNotifiParam1";
            this.m_lblNotifiParam1.Size = new System.Drawing.Size(104, 16);
            this.m_lblNotifiParam1.TabIndex = 20;
            this.m_lblNotifiParam1.Text = "IP Address";
            // 
            // m_lblNotifiParam2
            // 
            this.m_lblNotifiParam2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblNotifiParam2.Location = new System.Drawing.Point(17, 60);
            this.m_lblNotifiParam2.Name = "m_lblNotifiParam2";
            this.m_lblNotifiParam2.Size = new System.Drawing.Size(104, 16);
            this.m_lblNotifiParam2.TabIndex = 21;
            this.m_lblNotifiParam2.Text = "Port";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_txtPassword);
            this.groupBox3.Controls.Add(this.m_txtUserName);
            this.groupBox3.Controls.Add(this.m_lblPassword);
            this.groupBox3.Controls.Add(this.m_lblUserName);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(45, 324);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(417, 87);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Authentication";
            // 
            // m_txtPassword
            // 
            this.m_txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtPassword.Location = new System.Drawing.Point(129, 55);
            this.m_txtPassword.MaxLength = 32;
            this.m_txtPassword.Name = "m_txtPassword";
            this.m_txtPassword.Size = new System.Drawing.Size(281, 21);
            this.m_txtPassword.TabIndex = 8;
            this.m_txtPassword.UseSystemPasswordChar = true;
            this.m_txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmAdd_KeyPress);
            this.m_txtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.m_txtReaderName_Validating);
            // 
            // m_txtUserName
            // 
            this.m_txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtUserName.Location = new System.Drawing.Point(129, 24);
            this.m_txtUserName.MaxLength = 32;
            this.m_txtUserName.Name = "m_txtUserName";
            this.m_txtUserName.Size = new System.Drawing.Size(281, 21);
            this.m_txtUserName.TabIndex = 7;
            this.m_txtUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmAdd_KeyPress);
            this.m_txtUserName.Validating += new System.ComponentModel.CancelEventHandler(this.m_txtReaderName_Validating);
            // 
            // m_lblPassword
            // 
            this.m_lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblPassword.Location = new System.Drawing.Point(18, 57);
            this.m_lblPassword.Name = "m_lblPassword";
            this.m_lblPassword.Size = new System.Drawing.Size(100, 16);
            this.m_lblPassword.TabIndex = 23;
            this.m_lblPassword.Text = "Password";
            // 
            // m_lblUserName
            // 
            this.m_lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblUserName.Location = new System.Drawing.Point(17, 27);
            this.m_lblUserName.Name = "m_lblUserName";
            this.m_lblUserName.Size = new System.Drawing.Size(106, 16);
            this.m_lblUserName.TabIndex = 22;
            this.m_lblUserName.Text = "User Name";
            // 
            // m_lblModel
            // 
            this.m_lblModel.Location = new System.Drawing.Point(62, 82);
            this.m_lblModel.Name = "m_lblModel";
            this.m_lblModel.Size = new System.Drawing.Size(100, 16);
            this.m_lblModel.TabIndex = 16;
            this.m_lblModel.Text = "Model";
            // 
            // m_cboModel
            // 
            this.m_cboModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboModel.Items.AddRange(new object[] {
            "XR400",
            "XR440",
            "XR450",
            "XR480",
            "RD5000"});
            this.m_cboModel.Location = new System.Drawing.Point(174, 78);
            this.m_cboModel.Name = "m_cboModel";
            this.m_cboModel.Size = new System.Drawing.Size(281, 21);
            this.m_cboModel.TabIndex = 1;
            this.m_cboModel.SelectedIndexChanged += new System.EventHandler(this.m_cboModel_SelectedIndexChanged);
            this.m_cboModel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmAdd_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_btnCancel);
            this.groupBox2.Controls.Add(this.m_btnAdd);
            this.groupBox2.Location = new System.Drawing.Point(45, 421);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(417, 56);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.BackColor = System.Drawing.Color.White;
            this.m_btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.m_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCancel.Image")));
            this.m_btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnCancel.Location = new System.Drawing.Point(268, 16);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(84, 32);
            this.m_btnCancel.TabIndex = 10;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnCancel.UseVisualStyleBackColor = false;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.BackColor = System.Drawing.Color.White;
            this.m_btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAdd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAdd.Image")));
            this.m_btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnAdd.Location = new System.Drawing.Point(108, 16);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.Size = new System.Drawing.Size(84, 32);
            this.m_btnAdd.TabIndex = 9;
            this.m_btnAdd.Text = "Add";
            this.m_btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnAdd.UseVisualStyleBackColor = false;
            this.m_btnAdd.Click += new System.EventHandler(this.m_btnAdd_Click);
            // 
            // m_txtPort
            // 
            this.m_txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtPort.Location = new System.Drawing.Point(174, 174);
            this.m_txtPort.MaxLength = 5;
            this.m_txtPort.Name = "m_txtPort";
            this.m_txtPort.Size = new System.Drawing.Size(281, 20);
            this.m_txtPort.TabIndex = 4;
            this.m_txtPort.Text = "3000";
            this.m_txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmAdd_KeyPress);
            this.m_txtPort.Validating += new System.ComponentModel.CancelEventHandler(this.m_txtReaderName_Validating);
            // 
            // m_txtIPAddress
            // 
            this.m_txtIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtIPAddress.Location = new System.Drawing.Point(174, 142);
            this.m_txtIPAddress.MaxLength = 16;
            this.m_txtIPAddress.Name = "m_txtIPAddress";
            this.m_txtIPAddress.Size = new System.Drawing.Size(281, 20);
            this.m_txtIPAddress.TabIndex = 3;
            this.m_txtIPAddress.Text = "192.168.0.10";
            this.m_txtIPAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmAdd_KeyPress);
            this.m_txtIPAddress.Validating += new System.ComponentModel.CancelEventHandler(this.m_txtReaderName_Validating);
            // 
            // m_txtDescription
            // 
            this.m_txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtDescription.Location = new System.Drawing.Point(174, 110);
            this.m_txtDescription.MaxLength = 32;
            this.m_txtDescription.Name = "m_txtDescription";
            this.m_txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_txtDescription.Size = new System.Drawing.Size(281, 20);
            this.m_txtDescription.TabIndex = 2;
            this.m_txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmAdd_KeyPress);
            this.m_txtDescription.Validating += new System.ComponentModel.CancelEventHandler(this.m_txtReaderName_Validating);
            // 
            // m_txtReaderName
            // 
            this.m_txtReaderName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtReaderName.Location = new System.Drawing.Point(174, 46);
            this.m_txtReaderName.MaxLength = 32;
            this.m_txtReaderName.Name = "m_txtReaderName";
            this.m_txtReaderName.Size = new System.Drawing.Size(281, 20);
            this.m_txtReaderName.TabIndex = 0;
            this.m_txtReaderName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmAdd_KeyPress);
            this.m_txtReaderName.Validating += new System.ComponentModel.CancelEventHandler(this.m_txtReaderName_Validating);
            // 
            // m_lblPort
            // 
            this.m_lblPort.Location = new System.Drawing.Point(62, 178);
            this.m_lblPort.Name = "m_lblPort";
            this.m_lblPort.Size = new System.Drawing.Size(112, 16);
            this.m_lblPort.TabIndex = 19;
            this.m_lblPort.Text = "TCP Port";
            // 
            // m_lblIpAddress
            // 
            this.m_lblIpAddress.Location = new System.Drawing.Point(62, 146);
            this.m_lblIpAddress.Name = "m_lblIpAddress";
            this.m_lblIpAddress.Size = new System.Drawing.Size(112, 16);
            this.m_lblIpAddress.TabIndex = 18;
            this.m_lblIpAddress.Text = "IP Address";
            // 
            // m_lblDescription
            // 
            this.m_lblDescription.Location = new System.Drawing.Point(62, 114);
            this.m_lblDescription.Name = "m_lblDescription";
            this.m_lblDescription.Size = new System.Drawing.Size(112, 16);
            this.m_lblDescription.TabIndex = 17;
            this.m_lblDescription.Text = "Description";
            // 
            // m_lblReaderName
            // 
            this.m_lblReaderName.Location = new System.Drawing.Point(62, 50);
            this.m_lblReaderName.Name = "m_lblReaderName";
            this.m_lblReaderName.Size = new System.Drawing.Size(112, 16);
            this.m_lblReaderName.TabIndex = 15;
            this.m_lblReaderName.Text = "Reader Name";
            // 
            // frmAdd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(550, 528);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " Reader";
            this.Load += new System.EventHandler(this.frmAdd_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmAdd_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private void m_btnAdd_Click(object sender, System.EventArgs e)
        {
            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            IRFIDReader objReader = null;
            ReaderModel reader_Model = (ReaderModel)Enum.Parse(typeof(ReaderModel), m_cboModel.Text);
            try
            {
                if (!IsValid())
                {
                    if (!IsExist())
                    {

                        switch (reader_Model)
                        {
                            case ReaderModel.XR480:
                            case ReaderModel.XR450:
                            case ReaderModel.XR440:
                            case ReaderModel.XR400:
                                {
                                    string configStr = CreateConfigString();
                                    MemoryStream configstream = CreateConfigStream();

                                    // right now, only this method provided from ReaderFactory can support XR450 model
                                    objReader = ReaderFactory.CreateReader(m_txtReaderName.Text, reader_Model, configStr);
                                  
                                    if (objReader.ReaderStatus == ReaderStatus.ONLINE)
                                        objReader.ReaderCycleTimeInMS = 30000;
                                      
                                    break;

                                }
                            case ReaderModel.RD5000:
                                {
                                    string configStr = CreatConfigStrRD5000();
                                    objReader = ReaderFactory.CreateReader(m_txtReaderName.Text,
                                                     reader_Model, m_txtIPAddress.Text, Convert.ToInt32(m_txtPort.Text));

                                    break;
                                }
                            default:
                                throw new ApplicationException(m_cboModel.Text + " model is not supported.");
                        }
                        if (objReader != null)
                        {
                            frmTest objTest = new frmTest(objReader);
                            frmMain objMain = (frmMain)this.Owner;
                            objMain.m_hashTestForm.Add(m_txtReaderName.Text, objTest);
                            objMain.RefreshTree();
                            objMain.OpenTestWindow(m_txtReaderName.Text);
                            this.Close();
                        }
                    }
                    else
                        MessageBox.Show("Reader name is already used.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    MessageBox.Show("Unable to Add Reader : " + ex.Message + ". \n" + ex.InnerException.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Unable to Add Reader : " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }


        private void m_btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private bool IsExist()
        {
            frmMain objMain = (frmMain)this.Owner;
            if (objMain.m_hashTestForm.Contains(m_txtReaderName.Text))
                return true;
            foreach (frmTest obj in objMain.m_hashTestForm.Values)
            {
                if (obj.ReaderInfo.IndexOf(m_txtIPAddress.Text) > -1)
                    return true;
            }
            return false;
        }

        private bool IsValidAlphaNum32(string arg)
        {
            string patternAlphaNum = @"[a-zA-Z\d\-_# ]*";//includes space
            if (arg.Length <= 32 & arg.Length != 0)
            {
                if (new Regex(patternAlphaNum).Match(arg).Length == arg.Length)
                    return true;
            }
            return false;
        }

        private bool IsValidAlphaNum32WS(string arg)
        {
            string patternAlphaNum = @"((\w)*(\s)*(\w)*[.]*)*";
            if (arg.Length <= 32)
            {
                if (new Regex(patternAlphaNum).Match(arg).Length != arg.Length)
                    return false;
                else
                    return true;
            }
            return false;

        }

        private bool IsValid()
        {
            Validation objValid = new Validation();
            string msg = "";
            objValid.IsNotEmpty(m_txtReaderName.Text.Trim(), out msg);

            if (!msg.Equals(string.Empty))
                throw new ValidationException(msg + "Reader Name ");

            if (m_txtReaderName.Text.Trim() == "Readers")
                throw new ValidationException("Readers is not a Valid name");

            if (!IsValidAlphaNum32(m_txtReaderName.Text.Trim()))
                throw new ValidationException("Please Provide Reader Name in Alphanumeric characters and no space");

            //objValid.IsValidAlphaNum(m_txtReaderName.Text.Trim(), out msg);

            //if(!msg.Equals(string.Empty))
            //    throw new ValidationException("Please provide valid Reader Name ");

            if (!m_txtDescription.Text.Trim().Equals(""))
            {
                //objValid.IsNotEmpty(m_txtDescription.Text.Trim(),out msg);
                //if(!msg.Equals(""))
                //    throw new ValidationException(msg+" Description ");

                if (!IsValidAlphaNum32WS(m_txtDescription.Text.Trim()))
                    throw new ValidationException("Please provide valid Description in Alphanumeric characters");
            }

            objValid.IsValidIPAddress(m_txtIPAddress.Text.Trim(), out msg);
            if (!msg.Equals(string.Empty))
                throw new ValidationException(msg + "IPAddress Number");

            objValid.IsValidNumber(m_txtPort.Text.Trim(), out msg);
            if (!msg.Equals(string.Empty))
                throw new ValidationException("Please provide Port Number");
            if (  m_cboModel.SelectedItem.ToString() == "XR480" ||
                  m_cboModel.SelectedItem.ToString() == "XR400" ||
                  m_cboModel.SelectedItem.ToString() == "XR440" ||
                  m_cboModel.SelectedItem.ToString() == "XR450"
               )
            {
                objValid.IsValidIPAddress(m_txtlNotifiParam1.Text.Trim(), out msg);
                if (!msg.Equals(string.Empty))
                    throw new ValidationException(msg + "Notification Parameter:IPAddress Number");

                objValid.IsValidNumber(m_txtlNotifiParam2.Text.Trim(), out msg);
                if (!msg.Equals(string.Empty))
                    throw new ValidationException("Please provide Notification Parameter:Port Number");

                objValid.IsNotEmpty(m_txtUserName.Text.Trim(), out msg);
                if (!msg.Equals(string.Empty))
                    throw new ValidationException(msg + "User Name ");

                if (!IsValidAlphaNum32(m_txtUserName.Text.Trim()))
                    throw new ValidationException("User Name:Only Alphanumeric characters allowed ");


                objValid.IsNotEmpty(m_txtPassword.Text.Trim(), out msg);
                if (!msg.Equals(string.Empty))
                    throw new ValidationException(msg + "Password");

                if (!IsValidAlphaNum32(m_txtPassword.Text.Trim()))
                    throw new ValidationException("Password:Only Alphanumeric characters allowed");

            }
            return false;
        }

        private void frmAdd_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
                m_btnAdd_Click(null, null);
        }

        private string CreateConfigString()
        {
            string configStreamStr = string.Empty;

            try
            {
                XmlTextWriter wr = null;
                MemoryStream configStream = new MemoryStream();

                wr = new XmlTextWriter(configStream, Encoding.UTF8);

                wr.WriteStartDocument();

                //wr.WriteStartElement("", "ReaderConfig", @"  http://www.w3.org/2001/XMLSchema-instance");

                wr.WriteStartElement("ReaderConfig");
                wr.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                wr.Flush();


                wr.WriteStartElement("CommunicationSettings");

                wr.WriteStartElement("IPAddress");
                wr.WriteString(m_txtIPAddress.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("TcpPort");
                wr.WriteString(m_txtPort.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("HttpPort");
                wr.WriteString(m_txtHttpPort.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("NotificationAddress");
                wr.WriteString(m_txtlNotifiParam1.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("NotificationPort");
                wr.WriteString(m_txtlNotifiParam2.Text);
                wr.WriteEndElement();

                wr.WriteEndElement();

                wr.WriteStartElement("Authentification");

                wr.WriteStartElement("UserName");
                wr.WriteString(m_txtUserName.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("Password");
                wr.WriteString(m_txtPassword.Text);
                wr.WriteEndElement();

                wr.WriteEndElement();

                wr.WriteStartElement("ReaderInfo");
                wr.WriteStartElement("ReaderName");
                wr.WriteString(m_txtReaderName.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("Description");
                wr.WriteString(m_txtDescription.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("Model");
                wr.WriteString(m_cboModel.SelectedText);
                wr.WriteEndElement();

                wr.WriteEndElement();

                wr.WriteEndElement();

                wr.WriteEndDocument();
                wr.Flush();

                configStream.Seek(0, SeekOrigin.Begin);

                byte[] configbytes = new byte[Convert.ToInt32(configStream.Length)];

                configStream.Read(configbytes, 0, configbytes.Length);
                configStreamStr = System.Text.Encoding.UTF8.GetString(configbytes, 0, configbytes.Length);


            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    MessageBox.Show("Error in CreateFile:" + ex.Message + " " + ex.InnerException, "SymbolReader WinApp");
                else
                    MessageBox.Show("Error in CreateFile:" + ex.Message, "SymbolReader WinApp");
            }

            finally
            {

            }

            return configStreamStr;
        }

        private string CreatConfigStrRD5000()
        {
            string configStreamStr = string.Empty;

            try
            {
                XmlTextWriter wr = null;
                MemoryStream configStream = new MemoryStream();

                wr = new XmlTextWriter(configStream, Encoding.UTF8);

                wr.WriteStartDocument();

                wr.WriteStartElement("ReaderConfig");
                wr.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                wr.Flush();

                wr.WriteStartElement("EthernetSettings");

                wr.WriteStartElement("IPAddress");
                wr.WriteString(m_txtIPAddress.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("TcpPort");
                wr.WriteString(m_txtPort.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("HttpPort");
                wr.WriteString(m_txtHttpPort.Text);
                wr.WriteEndElement();

                wr.WriteEndElement();

                wr.WriteStartElement("ComPortSettings");
                wr.WriteStartElement("COMPort");
                wr.WriteString("COM7");//to do:
                wr.WriteEndElement();
                wr.WriteStartElement("BaudRate");
                wr.WriteString("57600");//to do:
                wr.WriteEndElement();
                wr.WriteEndElement();


                wr.WriteStartElement("ReaderInfo");
                wr.WriteStartElement("ReaderName");
                wr.WriteString(m_txtReaderName.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("Description");
                wr.WriteString(m_txtDescription.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("Model");
                wr.WriteString(m_cboModel.SelectedText);
                wr.WriteEndElement();

                wr.WriteEndElement();

                wr.WriteEndElement();

                wr.WriteEndDocument();
                wr.Flush();

                configStream.Seek(0, SeekOrigin.Begin);

                byte[] configbytes = new byte[Convert.ToInt32(configStream.Length)];

                configStream.Read(configbytes, 0, configbytes.Length);
                configStreamStr = System.Text.Encoding.UTF8.GetString(configbytes, 0, configbytes.Length);


            }
            catch
            {

            }

            finally
            {

            }

            return configStreamStr;

        }

        private void frmAdd_Load(object sender, EventArgs e)
        {

            try
            {
                ReadConfig();
                IncrementNotificationPort();
            }
            catch
            {


                //throw new ApplicationException("Error in setting configuration:" + ex.Message);

            }


        }

        private void ReadConfig()
        {

            Stream configStream = null;
            string hostName = Dns.GetHostName();
            IPHostEntry host = Dns.GetHostEntry(hostName);
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                if (!basePath.EndsWith(@"\"))
                    basePath += @"\";

                basePath += @"SymbolReaderXR480.Config";


                configStream = (new StreamReader(basePath)).BaseStream;


                if (configStream == null)
                    throw new ApplicationException("Invalid config file");

                configStream.Seek(0, SeekOrigin.Begin);
                XmlDocument xDoc = new XmlDocument();
                XmlNode xNode = null;
                xDoc.Load(configStream);

                xNode = xDoc.GetElementsByTagName("ReaderName").Item(0);
                m_txtReaderName.Text = xNode.InnerText.Trim();

                xNode = xDoc.GetElementsByTagName("Description").Item(0);
                m_txtDescription.Text = xNode.InnerText.Trim();

                xNode = xDoc.GetElementsByTagName("Model").Item(0);
                //int i = 0;
                //for( i = 0 ; i < m_cboModel.Items.Count;i++)
                //{
                //    if(m_cboModel.Items[i].ToString() == xNode.InnerText.Trim())
                //        break;

                //}

                //m_cboModel.SelectedIndex = i;

                xNode = xDoc.GetElementsByTagName("IPAddress").Item(0);
                m_txtIPAddress.Text = xNode.InnerText.Trim();

                xNode = xDoc.GetElementsByTagName("TcpPort").Item(0);
                m_txtPort.Text = xNode.InnerText.Trim();

                xNode = xDoc.GetElementsByTagName("HttpPort").Item(0);
                m_txtHttpPort.Text = xNode.InnerText.Trim();

                xNode = xDoc.GetElementsByTagName("NotificationPort").Item(0);
                m_txtlNotifiParam2.Text = xNode.InnerText.Trim();

                xNode = xDoc.GetElementsByTagName("UserName").Item(0);
                m_txtUserName.Text = xNode.InnerText.Trim();

                xNode = xDoc.GetElementsByTagName("Password").Item(0);
                m_txtPassword.Text = xNode.InnerText.Trim();


                m_txtlNotifiParam1.Text = host.AddressList[0].ToString();

            }
            catch
            {
                m_txtReaderName.Text = "Reader1";
                m_txtDescription.Text = "Symbol Reader";
//####                m_txtIPAddress.Text = "192.168.127.254";
                m_txtIPAddress.Text = "192.168.127.254";
                m_txtPort.Text = "3000";
                m_txtHttpPort.Text = "80";
                m_txtlNotifiParam2.Text = "4000";
                //m_txtlNotifiParam1.Text = host.AddressList[0].ToString();
                // modified by Ray: choose the last Ethernet adapter IP address
                m_txtlNotifiParam1.Text = host.AddressList[host.AddressList.Length-1].ToString();
                m_txtUserName.Text = "admin";
                m_txtPassword.Text = "change";

            }
            finally
            {
                if (configStream != null)
                    configStream.Close();
            }
        }

        private MemoryStream CreateConfigStream()
        {

            MemoryStream configStream = new MemoryStream();
            try
            {
                XmlTextWriter wr = null;


                wr = new XmlTextWriter(configStream, Encoding.UTF8);

                wr.WriteStartDocument();

                //wr.WriteStartElement("", "ReaderConfig", @"  http://www.w3.org/2001/XMLSchema-instance");

                wr.WriteStartElement("ReaderConfig");
                wr.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                wr.Flush();


                wr.WriteStartElement("CommunicationSettings");

                wr.WriteStartElement("IPAddress");
                wr.WriteString(m_txtIPAddress.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("TcpPort");
                wr.WriteString(m_txtPort.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("HttpPort");
                wr.WriteString(m_txtHttpPort.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("NotificationPort");
                wr.WriteString(m_txtlNotifiParam2.Text);
                wr.WriteEndElement();

                wr.WriteEndElement();

                wr.WriteStartElement("Authentification");

                wr.WriteStartElement("UserName");
                wr.WriteString(m_txtUserName.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("Password");
                wr.WriteString(m_txtPassword.Text);
                wr.WriteEndElement();

                wr.WriteEndElement();

                wr.WriteStartElement("ReaderInfo");
                wr.WriteStartElement("ReaderName");
                wr.WriteString(m_txtReaderName.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("Description");
                wr.WriteString(m_txtDescription.Text);
                wr.WriteEndElement();

                wr.WriteStartElement("Model");
                wr.WriteString(m_cboModel.SelectedText);
                wr.WriteEndElement();

                wr.WriteEndElement();

                wr.WriteEndElement();

                wr.WriteEndDocument();
                wr.Flush();

                configStream.Seek(0, SeekOrigin.Begin);





            }
            catch
            {
                //if (ex.InnerException != null)
                //    MessageBox.Show("Error in CreateFile:" + ex.Message + " " + ex.InnerException, "SymbolReader WinApp");
                //else
                //    MessageBox.Show("Error in CreateFile:" + ex.Message, "SymbolReader WinApp");
            }

            finally
            {

            }

            return configStream;
        }

        private void ChangeAddScreen(int index)
        {

            bool disableValue = true;
            if (index == 1)
            {
                disableValue = true;
            }
            if (index == 2)
            {
                disableValue = false;
                m_txtDescription.Text = "";
                m_txtlNotifiParam1.Text = "";
                m_txtlNotifiParam2.Text = "";
                m_txtPassword.Text = "";
                m_txtHttpPort.Text = "";
                m_txtUserName.Text = "";
            }

            m_txtDescription.Enabled = disableValue;
            m_txtlNotifiParam1.Enabled = disableValue;
            m_txtlNotifiParam2.Enabled = disableValue;
            m_txtPassword.Enabled = disableValue;
            m_txtHttpPort.Enabled = disableValue;
            m_txtUserName.Enabled = disableValue;
        }

        private void ReadConfigRD5000()
        {
            Stream configStream = null;
            string hostName = Dns.GetHostName();
            IPHostEntry host = Dns.GetHostEntry(hostName);
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                if (!basePath.EndsWith(@"\"))
                    basePath += @"\";

                basePath += @"SymbolReaderRD5000.Config";

                configStream = (new StreamReader(basePath)).BaseStream;


                if (configStream == null)
                    throw new ApplicationException("Invalid config file");

                configStream.Seek(0, SeekOrigin.Begin);
                XmlDocument xDoc = new XmlDocument();
                XmlNode xNode = null;
                xDoc.Load(configStream);

                xNode = xDoc.GetElementsByTagName("ReaderName").Item(0);
                m_txtReaderName.Text = xNode.InnerText.Trim();

                xNode = xDoc.GetElementsByTagName("Description").Item(0);
                m_txtDescription.Text = xNode.InnerText.Trim();


                xNode = xDoc.GetElementsByTagName("IPAddress").Item(0);
                m_txtIPAddress.Text = xNode.InnerText.Trim();

                xNode = xDoc.GetElementsByTagName("TcpPort").Item(0);
                m_txtPort.Text = xNode.InnerText.Trim();

                ////xNode = xDoc.GetElementsByTagName("COMPort").Item(0);
                ////m_txtHttpPort.Text = xNode.InnerText.Trim();//to do comport text box

                ////xNode = xDoc.GetElementsByTagName("BaudRate").Item(0);
                ////m_txtlNotifiParam2.Text = xNode.InnerText.Trim();

                m_txtlNotifiParam1.Text = host.AddressList[0].ToString();

            }
            catch
            {
                m_txtReaderName.Text = "Reader1";
                m_txtDescription.Text = "Symbol Reader";

                if (m_cboModel.SelectedIndex == 1)
                    m_txtIPAddress.Text = "192.168.0.80";

                if (m_cboModel.SelectedIndex == 2)
                    m_txtIPAddress.Text = "192.168.0.90";

                m_txtPort.Text = "4000";
            }
            finally
            {
                if (configStream != null)
                    configStream.Close();
            }
        }

        private void m_cboModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (m_cboModel.SelectedItem.ToString())
            {
                case "XR480":
                case "XR450":
                case "XR440":
                case "XR400":
                    ReadConfig();
                    ChangeAddScreen(1);
                    break;
                case "RD5000":
                    ReadConfigRD5000();
                    ChangeAddScreen(2);

                    break;
            }

            IncrementNotificationPort();

            if (txtReaderName != null)
                m_txtReaderName.Text = txtReaderName;

            if (txtDescription != null)
                m_txtDescription.Text = txtDescription;

            if (txtIPAddress != null)
                m_txtIPAddress.Text = txtIPAddress;

            if (txtPort != null)
                m_txtPort.Text = txtPort;

            if (txtHttpPort != null)
                m_txtHttpPort.Text = txtHttpPort;

            if (txtlNotifiParam1 != null)
                m_txtlNotifiParam1.Text = txtlNotifiParam1;

            if (txtlNotifiParam2 != null)
                m_txtlNotifiParam2.Text = txtlNotifiParam2;

            if (txtUserName != null)
                m_txtUserName.Text = txtUserName;

            if (txtPassword != null)
                m_txtPassword.Text = txtPassword;

        }

        private void m_txtReaderName_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                switch (((TextBox)sender).Name)
                {
                    case "m_txtReaderName":
                        txtReaderName = ((TextBox)sender).Text;
                        break;

                    case "m_txtDescription":
                        txtDescription = ((TextBox)sender).Text;
                        break;

                    case "m_txtIPAddress":
                        txtIPAddress = ((TextBox)sender).Text;
                        break;

                    case "m_txtPort":
                        txtPort = ((TextBox)sender).Text;
                        break;

                    case "m_txtHttpPort":
                        txtHttpPort = ((TextBox)sender).Text;
                        break;

                    case "m_txtlNotifiParam1":
                        txtlNotifiParam1 = ((TextBox)sender).Text;
                        break;

                    case "m_txtlNotifiParam2":
                        txtlNotifiParam2 = ((TextBox)sender).Text;
                        break;

                    case "m_txtUserName":
                        txtUserName = ((TextBox)sender).Text;
                        break;

                    case "m_txtPassword":
                        txtPassword = ((TextBox)sender).Text;
                        break;
                }
            }
            catch { }

        }

        private void IncrementNotificationPort()
        {
            try
            {
                int notifiyPort = 4000;
                string[] readers = ReaderFactory.GetAllReaderNames();

                foreach (string reader_Name in readers)
                {
                    IRFIDReader reader = ReaderFactory.GetReader(reader_Name);
                    if (reader.Model == ReaderModel.XR400 ||
                        reader.Model == ReaderModel.XR440 ||
                        reader.Model == ReaderModel.XR450 ||
                        reader.Model == ReaderModel.XR480)
                    {
                        if (notifiyPort < int.Parse(reader.NotificationPort))
                        {
                            notifiyPort = int.Parse(reader.NotificationPort);
                        }
                    }

                }
                notifiyPort++;
                m_txtlNotifiParam2.Text = notifiyPort.ToString();
            }
            catch
            { }
        }
    }
}
