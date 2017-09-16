namespace CS_RFID2_Sample
{
    partial class FrmReadData
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
          this.button2 = new System.Windows.Forms.Button();
          this.button1 = new System.Windows.Forms.Button();
          this.lblTagID = new System.Windows.Forms.Label();
          this.txtWordCount = new System.Windows.Forms.TextBox();
          this.label1 = new System.Windows.Forms.Label();
          this.txtPointer = new System.Windows.Forms.TextBox();
          this.label2 = new System.Windows.Forms.Label();
          this.txtData = new System.Windows.Forms.TextBox();
          this.label3 = new System.Windows.Forms.Label();
          this.cmbMemBank = new System.Windows.Forms.ComboBox();
          this.label4 = new System.Windows.Forms.Label();
          this.lblDescCount = new System.Windows.Forms.Label();
          this.panel1 = new System.Windows.Forms.Panel();
          this.label6 = new System.Windows.Forms.Label();
          this.lblPassword = new System.Windows.Forms.Label();
          this.txtpassword = new System.Windows.Forms.TextBox();
          this.panel2 = new System.Windows.Forms.Panel();
          this.label9 = new System.Windows.Forms.Label();
          this.cmbSel = new System.Windows.Forms.ComboBox();
          this.label8 = new System.Windows.Forms.Label();
          this.label7 = new System.Windows.Forms.Label();
          this.cmbSession = new System.Windows.Forms.ComboBox();
          this.cmbTarget = new System.Windows.Forms.ComboBox();
          this.lblQParam = new System.Windows.Forms.Label();
          this.numStartQ = new System.Windows.Forms.NumericUpDown();
          this.label5 = new System.Windows.Forms.Label();
          this.panel1.SuspendLayout();
          this.panel2.SuspendLayout();
          this.SuspendLayout();
          // 
          // button2
          // 
          this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.button2.Location = new System.Drawing.Point(134, 266);
          this.button2.Name = "button2";
          this.button2.Size = new System.Drawing.Size(61, 17);
          this.button2.TabIndex = 17;
          this.button2.Text = "Cancel";
          this.button2.Click += new System.EventHandler(this.button2_Click);
          // 
          // button1
          // 
          this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
          this.button1.Location = new System.Drawing.Point(67, 266);
          this.button1.Name = "button1";
          this.button1.Size = new System.Drawing.Size(61, 17);
          this.button1.TabIndex = 4;
          this.button1.Text = "OK";
          this.button1.Click += new System.EventHandler(this.button1_Click);
          // 
          // lblTagID
          // 
          this.lblTagID.Location = new System.Drawing.Point(13, 25);
          this.lblTagID.Name = "lblTagID";
          this.lblTagID.Size = new System.Drawing.Size(94, 18);
          this.lblTagID.Text = "Memory Bank";
          // 
          // txtWordCount
          // 
          this.txtWordCount.Location = new System.Drawing.Point(107, 91);
          this.txtWordCount.MaxLength = 24;
          this.txtWordCount.Multiline = true;
          this.txtWordCount.Name = "txtWordCount";
          this.txtWordCount.Size = new System.Drawing.Size(68, 18);
          this.txtWordCount.TabIndex = 2;
          // 
          // label1
          // 
          this.label1.Location = new System.Drawing.Point(14, 91);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(78, 18);
          this.label1.Text = "Word Count:";
          // 
          // txtPointer
          // 
          this.txtPointer.Location = new System.Drawing.Point(109, 49);
          this.txtPointer.MaxLength = 4;
          this.txtPointer.Multiline = true;
          this.txtPointer.Name = "txtPointer";
          this.txtPointer.Size = new System.Drawing.Size(68, 18);
          this.txtPointer.TabIndex = 1;
          // 
          // label2
          // 
          this.label2.Location = new System.Drawing.Point(13, 49);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(87, 18);
          this.label2.Text = "Word Pointer:";
          // 
          // txtData
          // 
          this.txtData.Location = new System.Drawing.Point(106, 91);
          this.txtData.MaxLength = 24;
          this.txtData.Multiline = true;
          this.txtData.Name = "txtData";
          this.txtData.Size = new System.Drawing.Size(141, 36);
          this.txtData.TabIndex = 3;
          // 
          // label3
          // 
          this.label3.Location = new System.Drawing.Point(14, 89);
          this.label3.Name = "label3";
          this.label3.Size = new System.Drawing.Size(74, 18);
          this.label3.Text = "Write Data:";
          // 
          // cmbMemBank
          // 
          this.cmbMemBank.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
          this.cmbMemBank.Items.Add("EPC");
          this.cmbMemBank.Items.Add("TID");
          this.cmbMemBank.Items.Add("USER");
          this.cmbMemBank.Location = new System.Drawing.Point(108, 25);
          this.cmbMemBank.Name = "cmbMemBank";
          this.cmbMemBank.Size = new System.Drawing.Size(100, 19);
          this.cmbMemBank.TabIndex = 0;
          // 
          // label4
          // 
          this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
          this.label4.Location = new System.Drawing.Point(12, 70);
          this.label4.Name = "label4";
          this.label4.Size = new System.Drawing.Size(216, 16);
          this.label4.Text = "16 bits start address";
          // 
          // lblDescCount
          // 
          this.lblDescCount.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
          this.lblDescCount.Location = new System.Drawing.Point(14, 113);
          this.lblDescCount.Name = "lblDescCount";
          this.lblDescCount.Size = new System.Drawing.Size(129, 14);
          this.lblDescCount.Text = "Number of words to read.";
          // 
          // panel1
          // 
          this.panel1.Controls.Add(this.label6);
          this.panel1.Controls.Add(this.label4);
          this.panel1.Controls.Add(this.lblDescCount);
          this.panel1.Controls.Add(this.lblTagID);
          this.panel1.Controls.Add(this.label1);
          this.panel1.Controls.Add(this.cmbMemBank);
          this.panel1.Controls.Add(this.txtWordCount);
          this.panel1.Controls.Add(this.txtData);
          this.panel1.Controls.Add(this.label2);
          this.panel1.Controls.Add(this.label3);
          this.panel1.Controls.Add(this.txtPointer);
          this.panel1.Location = new System.Drawing.Point(3, 3);
          this.panel1.Name = "panel1";
          this.panel1.Size = new System.Drawing.Size(258, 132);
          // 
          // label6
          // 
          this.label6.Location = new System.Drawing.Point(14, 3);
          this.label6.Name = "label6";
          this.label6.Size = new System.Drawing.Size(194, 19);
          this.label6.Text = "Tag data location parameters:";
          // 
          // lblPassword
          // 
          this.lblPassword.Location = new System.Drawing.Point(15, 242);
          this.lblPassword.Name = "lblPassword";
          this.lblPassword.Size = new System.Drawing.Size(107, 18);
          this.lblPassword.Text = "Access Password";
          // 
          // txtpassword
          // 
          this.txtpassword.Location = new System.Drawing.Point(136, 242);
          this.txtpassword.MaxLength = 24;
          this.txtpassword.Multiline = true;
          this.txtpassword.Name = "txtpassword";
          this.txtpassword.Size = new System.Drawing.Size(87, 18);
          this.txtpassword.TabIndex = 27;
          // 
          // panel2
          // 
          this.panel2.Controls.Add(this.label5);
          this.panel2.Controls.Add(this.numStartQ);
          this.panel2.Controls.Add(this.lblQParam);
          this.panel2.Controls.Add(this.cmbTarget);
          this.panel2.Controls.Add(this.cmbSession);
          this.panel2.Controls.Add(this.label7);
          this.panel2.Controls.Add(this.label8);
          this.panel2.Controls.Add(this.cmbSel);
          this.panel2.Controls.Add(this.label9);
          this.panel2.Location = new System.Drawing.Point(3, 138);
          this.panel2.Name = "panel2";
          this.panel2.Size = new System.Drawing.Size(258, 99);
          // 
          // label9
          // 
          this.label9.Location = new System.Drawing.Point(12, 44);
          this.label9.Name = "label9";
          this.label9.Size = new System.Drawing.Size(57, 18);
          this.label9.Text = "Session";
          // 
          // cmbSel
          // 
          this.cmbSel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
          this.cmbSel.Items.Add(" Ignore SL");
          this.cmbSel.Items.Add(" SL Not Set");
          this.cmbSel.Items.Add(" SL Set");
          this.cmbSel.Location = new System.Drawing.Point(83, 20);
          this.cmbSel.Name = "cmbSel";
          this.cmbSel.Size = new System.Drawing.Size(100, 19);
          this.cmbSel.TabIndex = 0;
          this.cmbSel.SelectedIndexChanged += new System.EventHandler(this.cmbSel_SelectedIndexChanged);
          // 
          // label8
          // 
          this.label8.Location = new System.Drawing.Point(130, 44);
          this.label8.Name = "label8";
          this.label8.Size = new System.Drawing.Size(49, 18);
          this.label8.Text = "Target";
          // 
          // label7
          // 
          this.label7.Location = new System.Drawing.Point(13, 21);
          this.label7.Name = "label7";
          this.label7.Size = new System.Drawing.Size(42, 18);
          this.label7.Text = "Sel";
          // 
          // cmbSession
          // 
          this.cmbSession.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
          this.cmbSession.Items.Add("S0");
          this.cmbSession.Items.Add("S1");
          this.cmbSession.Items.Add("S2");
          this.cmbSession.Items.Add("S3");
          this.cmbSession.Location = new System.Drawing.Point(83, 44);
          this.cmbSession.Name = "cmbSession";
          this.cmbSession.Size = new System.Drawing.Size(44, 19);
          this.cmbSession.TabIndex = 5;
          // 
          // cmbTarget
          // 
          this.cmbTarget.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
          this.cmbTarget.Items.Add("Bit A");
          this.cmbTarget.Items.Add("Bit B");
          this.cmbTarget.Location = new System.Drawing.Point(178, 44);
          this.cmbTarget.Name = "cmbTarget";
          this.cmbTarget.Size = new System.Drawing.Size(67, 19);
          this.cmbTarget.TabIndex = 6;
          // 
          // lblQParam
          // 
          this.lblQParam.Location = new System.Drawing.Point(11, 69);
          this.lblQParam.Name = "lblQParam";
          this.lblQParam.Size = new System.Drawing.Size(66, 18);
          this.lblQParam.Text = "Starting Q";
          // 
          // numStartQ
          // 
          this.numStartQ.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
          this.numStartQ.Location = new System.Drawing.Point(83, 69);
          this.numStartQ.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
          this.numStartQ.Name = "numStartQ";
          this.numStartQ.Size = new System.Drawing.Size(50, 22);
          this.numStartQ.TabIndex = 10;
          this.numStartQ.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
          // 
          // label5
          // 
          this.label5.Location = new System.Drawing.Point(11, 2);
          this.label5.Name = "label5";
          this.label5.Size = new System.Drawing.Size(117, 15);
          this.label5.Text = "Gen2 Parameters:";
          // 
          // FrmReadData
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
          this.AutoScroll = true;
          this.ClientSize = new System.Drawing.Size(264, 294);
          this.Controls.Add(this.lblPassword);
          this.Controls.Add(this.txtpassword);
          this.Controls.Add(this.panel2);
          this.Controls.Add(this.panel1);
          this.Controls.Add(this.button2);
          this.Controls.Add(this.button1);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "FrmReadData";
          this.Text = "Read Data";
          this.Load += new System.EventHandler(this.FrmReadData_Load);
          this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmReadData_Closing);
          this.panel1.ResumeLayout(false);
          this.panel2.ResumeLayout(false);
          this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblTagID;
        private System.Windows.Forms.TextBox txtWordCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPointer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMemBank;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDescCount;
      private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPassword;
      private System.Windows.Forms.TextBox txtpassword;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.NumericUpDown numStartQ;
      private System.Windows.Forms.Label lblQParam;
      private System.Windows.Forms.ComboBox cmbTarget;
      private System.Windows.Forms.ComboBox cmbSession;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.ComboBox cmbSel;
      private System.Windows.Forms.Label label9;
    }
}