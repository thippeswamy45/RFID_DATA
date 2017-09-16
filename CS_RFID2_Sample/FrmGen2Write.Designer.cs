namespace CS_RFID2_Sample
{
  partial class FrmGen2Write
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
        this.cmbSession = new System.Windows.Forms.ComboBox();
        this.label7 = new System.Windows.Forms.Label();
        this.cmbSel = new System.Windows.Forms.ComboBox();
        this.label9 = new System.Windows.Forms.Label();
        this.lblTagID = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.cmbTarget = new System.Windows.Forms.ComboBox();
        this.txtWrdCnt = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.txtWrdPtr = new System.Windows.Forms.TextBox();
        this.btn_Quit = new System.Windows.Forms.Button();
        this.label4 = new System.Windows.Forms.Label();
        this.cmbAntenna = new System.Windows.Forms.ComboBox();
        this.label5 = new System.Windows.Forms.Label();
        this.cmbMemBank = new System.Windows.Forms.ComboBox();
        this.label6 = new System.Windows.Forms.Label();
        this.label10 = new System.Windows.Forms.Label();
        this.cmbLockOptions = new System.Windows.Forms.ComboBox();
        this.label11 = new System.Windows.Forms.Label();
        this.cmbOptions = new System.Windows.Forms.ComboBox();
        this.label12 = new System.Windows.Forms.Label();
        this.cmbStartQ = new System.Windows.Forms.ComboBox();
        this.txtRFAtten = new System.Windows.Forms.TextBox();
        this.label13 = new System.Windows.Forms.Label();
        this.txtAccPwd = new System.Windows.Forms.TextBox();
        this.listView2 = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.btn_Write = new System.Windows.Forms.Button();
        this.btn_Read = new System.Windows.Forms.Button();
        this.txtMask = new System.Windows.Forms.TextBox();
        this.btn_Clr = new System.Windows.Forms.Button();
        this.txtWrite = new System.Windows.Forms.TextBox();
        this.label8 = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // cmbSession
        // 
        this.cmbSession.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbSession.Items.Add("Sess0");
        this.cmbSession.Items.Add("Sess1");
        this.cmbSession.Items.Add("Sess2");
        this.cmbSession.Items.Add("Sess3");
        this.cmbSession.Location = new System.Drawing.Point(59, 162);
        this.cmbSession.Name = "cmbSession";
        this.cmbSession.Size = new System.Drawing.Size(47, 19);
        this.cmbSession.TabIndex = 80;
        // 
        // label7
        // 
        this.label7.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label7.Location = new System.Drawing.Point(4, 141);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(41, 18);
        this.label7.Text = "Sel";
        // 
        // cmbSel
        // 
        this.cmbSel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbSel.Items.Add("SLign");
        this.cmbSel.Items.Add("SLclr");
        this.cmbSel.Items.Add("SLset");
        this.cmbSel.Location = new System.Drawing.Point(59, 141);
        this.cmbSel.Name = "cmbSel";
        this.cmbSel.Size = new System.Drawing.Size(47, 19);
        this.cmbSel.TabIndex = 79;
        // 
        // label9
        // 
        this.label9.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label9.Location = new System.Drawing.Point(4, 164);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(46, 18);
        this.label9.Text = "Session";
        // 
        // lblTagID
        // 
        this.lblTagID.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.lblTagID.Location = new System.Drawing.Point(4, 182);
        this.lblTagID.Name = "lblTagID";
        this.lblTagID.Size = new System.Drawing.Size(41, 19);
        this.lblTagID.Text = "Target";
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label1.Location = new System.Drawing.Point(111, 227);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(44, 19);
        this.label1.Text = "Length";
        // 
        // cmbTarget
        // 
        this.cmbTarget.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbTarget.Items.Add("_A_");
        this.cmbTarget.Items.Add("_B_");
        this.cmbTarget.Location = new System.Drawing.Point(59, 182);
        this.cmbTarget.Name = "cmbTarget";
        this.cmbTarget.Size = new System.Drawing.Size(47, 19);
        this.cmbTarget.TabIndex = 75;
        // 
        // txtWrdCnt
        // 
        this.txtWrdCnt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
        this.txtWrdCnt.Location = new System.Drawing.Point(167, 225);
        this.txtWrdCnt.MaxLength = 24;
        this.txtWrdCnt.Multiline = true;
        this.txtWrdCnt.Name = "txtWrdCnt";
        this.txtWrdCnt.Size = new System.Drawing.Size(36, 19);
        this.txtWrdCnt.TabIndex = 77;
        // 
        // label2
        // 
        this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
        this.label2.Location = new System.Drawing.Point(4, 226);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(46, 19);
        this.label2.Text = "Wrd Ptr";
        // 
        // label3
        // 
        this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.label3.Location = new System.Drawing.Point(2, 100);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(32, 18);
        this.label3.Text = "Mask";
        // 
        // txtWrdPtr
        // 
        this.txtWrdPtr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
        this.txtWrdPtr.Location = new System.Drawing.Point(58, 224);
        this.txtWrdPtr.MaxLength = 4;
        this.txtWrdPtr.Multiline = true;
        this.txtWrdPtr.Name = "txtWrdPtr";
        this.txtWrdPtr.Size = new System.Drawing.Size(36, 19);
        this.txtWrdPtr.TabIndex = 76;
        // 
        // btn_Quit
        // 
        this.btn_Quit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btn_Quit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.btn_Quit.Location = new System.Drawing.Point(204, 249);
        this.btn_Quit.Name = "btn_Quit";
        this.btn_Quit.Size = new System.Drawing.Size(32, 17);
        this.btn_Quit.TabIndex = 88;
        this.btn_Quit.Text = "Quit";
        this.btn_Quit.Click += new System.EventHandler(this.btn_Quit_Click);
        // 
        // label4
        // 
        this.label4.Enabled = false;
        this.label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label4.Location = new System.Drawing.Point(3, 120);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(42, 18);
        this.label4.Text = "Ant";
        this.label4.Visible = false;
        // 
        // cmbAntenna
        // 
        this.cmbAntenna.Enabled = false;
        this.cmbAntenna.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbAntenna.Items.Add("A0");
        this.cmbAntenna.Items.Add("A1");
        this.cmbAntenna.Location = new System.Drawing.Point(59, 120);
        this.cmbAntenna.Name = "cmbAntenna";
        this.cmbAntenna.Size = new System.Drawing.Size(47, 19);
        this.cmbAntenna.TabIndex = 90;
        this.cmbAntenna.Visible = false;
        // 
        // label5
        // 
        this.label5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label5.Location = new System.Drawing.Point(112, 203);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(37, 19);
        this.label5.Text = "Bank";
        // 
        // cmbMemBank
        // 
        this.cmbMemBank.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbMemBank.Items.Add("RSRV");
        this.cmbMemBank.Items.Add("EPC");
        this.cmbMemBank.Items.Add("TID");
        this.cmbMemBank.Items.Add("USER");
        this.cmbMemBank.Location = new System.Drawing.Point(167, 202);
        this.cmbMemBank.Name = "cmbMemBank";
        this.cmbMemBank.Size = new System.Drawing.Size(70, 19);
        this.cmbMemBank.TabIndex = 93;
        // 
        // label6
        // 
        this.label6.Enabled = false;
        this.label6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label6.Location = new System.Drawing.Point(112, 122);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(51, 16);
        this.label6.Text = "RFAtten";
        this.label6.Visible = false;
        // 
        // label10
        // 
        this.label10.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label10.Location = new System.Drawing.Point(112, 183);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(47, 19);
        this.label10.Text = "LckOpt";
        // 
        // cmbLockOptions
        // 
        this.cmbLockOptions.Enabled = false;
        this.cmbLockOptions.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbLockOptions.Items.Add("0");
        this.cmbLockOptions.Items.Add("PLockBnk");
        this.cmbLockOptions.Items.Add("UsePLckB");
        this.cmbLockOptions.Items.Add("PwdWrtBnk");
        this.cmbLockOptions.Items.Add("UsePwdWB");
        this.cmbLockOptions.Items.Add("PLckBank2");
        this.cmbLockOptions.Items.Add("UsePLckB2");
        this.cmbLockOptions.Items.Add("PwdWrtBnk2");
        this.cmbLockOptions.Items.Add("UsePwdWB2");
        this.cmbLockOptions.Location = new System.Drawing.Point(167, 181);
        this.cmbLockOptions.Name = "cmbLockOptions";
        this.cmbLockOptions.Size = new System.Drawing.Size(70, 19);
        this.cmbLockOptions.TabIndex = 103;
        // 
        // label11
        // 
        this.label11.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label11.Location = new System.Drawing.Point(112, 162);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(47, 19);
        this.label11.Text = "Options";
        // 
        // cmbOptions
        // 
        this.cmbOptions.Enabled = false;
        this.cmbOptions.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbOptions.Items.Add("0");
        this.cmbOptions.Items.Add("BlockWrt");
        this.cmbOptions.Items.Add("BlkErsFrst");
        this.cmbOptions.Items.Add("RFU");
        this.cmbOptions.Items.Add("RFU");
        this.cmbOptions.Items.Add("RFU");
        this.cmbOptions.Items.Add("RFU");
        this.cmbOptions.Items.Add("RFU");
        this.cmbOptions.Items.Add("RFU");
        this.cmbOptions.Items.Add("RFU");
        this.cmbOptions.Location = new System.Drawing.Point(167, 160);
        this.cmbOptions.Name = "cmbOptions";
        this.cmbOptions.Size = new System.Drawing.Size(70, 19);
        this.cmbOptions.TabIndex = 102;
        // 
        // label12
        // 
        this.label12.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label12.Location = new System.Drawing.Point(4, 202);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(41, 19);
        this.label12.Text = "StartQ";
        // 
        // cmbStartQ
        // 
        this.cmbStartQ.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbStartQ.Items.Add("00");
        this.cmbStartQ.Items.Add("01");
        this.cmbStartQ.Items.Add("02");
        this.cmbStartQ.Items.Add("03");
        this.cmbStartQ.Items.Add("04");
        this.cmbStartQ.Items.Add("05");
        this.cmbStartQ.Items.Add("06");
        this.cmbStartQ.Items.Add("07");
        this.cmbStartQ.Items.Add("08");
        this.cmbStartQ.Items.Add("09");
        this.cmbStartQ.Items.Add("10");
        this.cmbStartQ.Items.Add("11");
        this.cmbStartQ.Items.Add("12");
        this.cmbStartQ.Items.Add("13");
        this.cmbStartQ.Items.Add("14");
        this.cmbStartQ.Items.Add("15");
        this.cmbStartQ.Location = new System.Drawing.Point(59, 202);
        this.cmbStartQ.Name = "cmbStartQ";
        this.cmbStartQ.Size = new System.Drawing.Size(47, 19);
        this.cmbStartQ.TabIndex = 107;
        // 
        // txtRFAtten
        // 
        this.txtRFAtten.Enabled = false;
        this.txtRFAtten.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.txtRFAtten.Location = new System.Drawing.Point(167, 119);
        this.txtRFAtten.MaxLength = 24;
        this.txtRFAtten.Multiline = true;
        this.txtRFAtten.Name = "txtRFAtten";
        this.txtRFAtten.Size = new System.Drawing.Size(36, 19);
        this.txtRFAtten.TabIndex = 109;
        this.txtRFAtten.Text = "000";
        this.txtRFAtten.Visible = false;
        // 
        // label13
        // 
        this.label13.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label13.Location = new System.Drawing.Point(113, 142);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(46, 19);
        this.label13.Text = "AccPwd";
        // 
        // txtAccPwd
        // 
        this.txtAccPwd.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.txtAccPwd.Location = new System.Drawing.Point(167, 140);
        this.txtAccPwd.MaxLength = 16;
        this.txtAccPwd.Multiline = true;
        this.txtAccPwd.Name = "txtAccPwd";
        this.txtAccPwd.Size = new System.Drawing.Size(70, 19);
        this.txtAccPwd.TabIndex = 111;
        this.txtAccPwd.Text = "0";
        // 
        // listView2
        // 
        this.listView2.Columns.Add(this.columnHeader1);
        this.listView2.Columns.Add(this.columnHeader2);
        this.listView2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
        this.listView2.Location = new System.Drawing.Point(4, 4);
        this.listView2.Name = "listView2";
        this.listView2.Size = new System.Drawing.Size(233, 71);
        this.listView2.TabIndex = 156;
        this.listView2.View = System.Windows.Forms.View.Details;
        this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
        // 
        // columnHeader1
        // 
        this.columnHeader1.Text = "#";
        this.columnHeader1.Width = 18;
        // 
        // columnHeader2
        // 
        this.columnHeader2.Text = "EPC : Requested Data";
        this.columnHeader2.Width = 1024;
        // 
        // btn_Write
        // 
        this.btn_Write.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.btn_Write.Location = new System.Drawing.Point(128, 248);
        this.btn_Write.Name = "btn_Write";
        this.btn_Write.Size = new System.Drawing.Size(51, 16);
        this.btn_Write.TabIndex = 170;
        this.btn_Write.Text = "WRITE";
        this.btn_Write.Click += new System.EventHandler(this.btn_Write_Click);
        // 
        // btn_Read
        // 
        this.btn_Read.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.btn_Read.Location = new System.Drawing.Point(59, 248);
        this.btn_Read.Name = "btn_Read";
        this.btn_Read.Size = new System.Drawing.Size(51, 16);
        this.btn_Read.TabIndex = 184;
        this.btn_Read.Text = "READ";
        this.btn_Read.Click += new System.EventHandler(this.btn_Read_Click);
        // 
        // txtMask
        // 
        this.txtMask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
        this.txtMask.Location = new System.Drawing.Point(33, 97);
        this.txtMask.MaxLength = 1024;
        this.txtMask.Multiline = true;
        this.txtMask.Name = "txtMask";
        this.txtMask.Size = new System.Drawing.Size(203, 19);
        this.txtMask.TabIndex = 198;
        // 
        // btn_Clr
        // 
        this.btn_Clr.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.btn_Clr.Location = new System.Drawing.Point(2, 248);
        this.btn_Clr.Name = "btn_Clr";
        this.btn_Clr.Size = new System.Drawing.Size(32, 18);
        this.btn_Clr.TabIndex = 228;
        this.btn_Clr.Text = "Clear";
        this.btn_Clr.Click += new System.EventHandler(this.btn_Clr_Click);
        // 
        // txtWrite
        // 
        this.txtWrite.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
        this.txtWrite.Location = new System.Drawing.Point(33, 77);
        this.txtWrite.MaxLength = 1024;
        this.txtWrite.Multiline = true;
        this.txtWrite.Name = "txtWrite";
        this.txtWrite.Size = new System.Drawing.Size(203, 19);
        this.txtWrite.TabIndex = 243;
        // 
        // label8
        // 
        this.label8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.label8.Location = new System.Drawing.Point(2, 80);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(32, 18);
        this.label8.Text = "Write";
        // 
        // FrmGen2Write
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.AutoScroll = true;
        this.ClientSize = new System.Drawing.Size(244, 269);
        this.Controls.Add(this.txtWrite);
        this.Controls.Add(this.label8);
        this.Controls.Add(this.btn_Clr);
        this.Controls.Add(this.txtMask);
        this.Controls.Add(this.btn_Read);
        this.Controls.Add(this.btn_Write);
        this.Controls.Add(this.listView2);
        this.Controls.Add(this.label13);
        this.Controls.Add(this.txtAccPwd);
        this.Controls.Add(this.txtRFAtten);
        this.Controls.Add(this.label12);
        this.Controls.Add(this.cmbStartQ);
        this.Controls.Add(this.label10);
        this.Controls.Add(this.cmbLockOptions);
        this.Controls.Add(this.label11);
        this.Controls.Add(this.cmbOptions);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.cmbMemBank);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.cmbAntenna);
        this.Controls.Add(this.btn_Quit);
        this.Controls.Add(this.cmbSession);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.cmbSel);
        this.Controls.Add(this.label9);
        this.Controls.Add(this.lblTagID);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.cmbTarget);
        this.Controls.Add(this.txtWrdCnt);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.txtWrdPtr);
        this.Name = "FrmGen2Write";
        this.Text = "Gen2 Write";
        this.Load += new System.EventHandler(this.FrmGen2Write_Load);
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox cmbSession;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox cmbSel;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label lblTagID;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbTarget;
    private System.Windows.Forms.TextBox txtWrdCnt;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtWrdPtr;
    private System.Windows.Forms.Button btn_Quit;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cmbAntenna;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cmbMemBank;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.ComboBox cmbLockOptions;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.ComboBox cmbOptions;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.ComboBox cmbStartQ;
    private System.Windows.Forms.TextBox txtRFAtten;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.TextBox txtAccPwd;
    private System.Windows.Forms.ListView listView2;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.Button btn_Write;
    private System.Windows.Forms.Button btn_Read;
    private System.Windows.Forms.TextBox txtMask;
    private System.Windows.Forms.Button btn_Clr;
    private System.Windows.Forms.TextBox txtWrite;
    private System.Windows.Forms.Label label8;
  }
}