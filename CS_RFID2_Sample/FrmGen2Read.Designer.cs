namespace CS_RFID2_Sample
{
  partial class FrmGen2Read
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
        this.label13 = new System.Windows.Forms.Label();
        this.txtAccPwd = new System.Windows.Forms.TextBox();
        this.txtRFAtten = new System.Windows.Forms.TextBox();
        this.label12 = new System.Windows.Forms.Label();
        this.cmbStartQ = new System.Windows.Forms.ComboBox();
        this.label6 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.cmbMemBank = new System.Windows.Forms.ComboBox();
        this.label4 = new System.Windows.Forms.Label();
        this.cmbAntenna = new System.Windows.Forms.ComboBox();
        this.btn_OK = new System.Windows.Forms.Button();
        this.cmbSession = new System.Windows.Forms.ComboBox();
        this.label7 = new System.Windows.Forms.Label();
        this.cmbSel = new System.Windows.Forms.ComboBox();
        this.label9 = new System.Windows.Forms.Label();
        this.lblTagID = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.cmbTarget = new System.Windows.Forms.ComboBox();
        this.txtWrdCnt = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.txtWrdPtr = new System.Windows.Forms.TextBox();
        this.listView2 = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.btn_Read = new System.Windows.Forms.Button();
        this.btn_Clear = new System.Windows.Forms.Button();
        this.btn_Auto = new System.Windows.Forms.Button();
        this.chkInventory = new System.Windows.Forms.CheckBox();
        this.SuspendLayout();
        // 
        // label13
        // 
        this.label13.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label13.Location = new System.Drawing.Point(113, 154);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(46, 19);
        this.label13.Text = "AccPwd";
        // 
        // txtAccPwd
        // 
        this.txtAccPwd.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.txtAccPwd.Location = new System.Drawing.Point(167, 152);
        this.txtAccPwd.MaxLength = 16;
        this.txtAccPwd.Multiline = true;
        this.txtAccPwd.Name = "txtAccPwd";
        this.txtAccPwd.Size = new System.Drawing.Size(70, 19);
        this.txtAccPwd.TabIndex = 141;
        this.txtAccPwd.Text = "0";
        // 
        // txtRFAtten
        // 
        this.txtRFAtten.Enabled = false;
        this.txtRFAtten.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.txtRFAtten.Location = new System.Drawing.Point(167, 131);
        this.txtRFAtten.MaxLength = 24;
        this.txtRFAtten.Multiline = true;
        this.txtRFAtten.Name = "txtRFAtten";
        this.txtRFAtten.Size = new System.Drawing.Size(36, 19);
        this.txtRFAtten.TabIndex = 140;
        this.txtRFAtten.Text = "000";
        this.txtRFAtten.Visible = false;
        // 
        // label12
        // 
        this.label12.Enabled = false;
        this.label12.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label12.Location = new System.Drawing.Point(4, 219);
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
        this.cmbStartQ.Location = new System.Drawing.Point(59, 219);
        this.cmbStartQ.Name = "cmbStartQ";
        this.cmbStartQ.Size = new System.Drawing.Size(47, 19);
        this.cmbStartQ.TabIndex = 139;
        // 
        // label6
        // 
        this.label6.Enabled = false;
        this.label6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label6.Location = new System.Drawing.Point(112, 134);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(51, 16);
        this.label6.Text = "RFAtten";
        this.label6.Visible = false;
        // 
        // label5
        // 
        this.label5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label5.Location = new System.Drawing.Point(114, 177);
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
        this.cmbMemBank.Location = new System.Drawing.Point(167, 174);
        this.cmbMemBank.Name = "cmbMemBank";
        this.cmbMemBank.Size = new System.Drawing.Size(70, 19);
        this.cmbMemBank.TabIndex = 135;
        // 
        // label4
        // 
        this.label4.Enabled = false;
        this.label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label4.Location = new System.Drawing.Point(3, 132);
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
        this.cmbAntenna.Location = new System.Drawing.Point(59, 131);
        this.cmbAntenna.Name = "cmbAntenna";
        this.cmbAntenna.Size = new System.Drawing.Size(47, 19);
        this.cmbAntenna.TabIndex = 134;
        this.cmbAntenna.Visible = false;
        // 
        // btn_OK
        // 
        this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.btn_OK.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.btn_OK.Location = new System.Drawing.Point(206, 248);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(33, 17);
        this.btn_OK.TabIndex = 132;
        this.btn_OK.Text = "Quit";
        // 
        // cmbSession
        // 
        this.cmbSession.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbSession.Items.Add("Sess0");
        this.cmbSession.Items.Add("Sess1");
        this.cmbSession.Items.Add("Sess2");
        this.cmbSession.Items.Add("Sess3");
        this.cmbSession.Location = new System.Drawing.Point(59, 174);
        this.cmbSession.Name = "cmbSession";
        this.cmbSession.Size = new System.Drawing.Size(47, 19);
        this.cmbSession.TabIndex = 131;
        // 
        // label7
        // 
        this.label7.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label7.Location = new System.Drawing.Point(4, 153);
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
        this.cmbSel.Location = new System.Drawing.Point(59, 152);
        this.cmbSel.Name = "cmbSel";
        this.cmbSel.Size = new System.Drawing.Size(47, 19);
        this.cmbSel.TabIndex = 130;
        // 
        // label9
        // 
        this.label9.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label9.Location = new System.Drawing.Point(4, 176);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(46, 18);
        this.label9.Text = "Session";
        // 
        // lblTagID
        // 
        this.lblTagID.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.lblTagID.Location = new System.Drawing.Point(4, 197);
        this.lblTagID.Name = "lblTagID";
        this.lblTagID.Size = new System.Drawing.Size(41, 19);
        this.lblTagID.Text = "Target";
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label1.Location = new System.Drawing.Point(114, 219);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(44, 19);
        this.label1.Text = "Length";
        // 
        // cmbTarget
        // 
        this.cmbTarget.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbTarget.Items.Add("_A_");
        this.cmbTarget.Items.Add("_B_");
        this.cmbTarget.Location = new System.Drawing.Point(59, 197);
        this.cmbTarget.Name = "cmbTarget";
        this.cmbTarget.Size = new System.Drawing.Size(47, 19);
        this.cmbTarget.TabIndex = 126;
        // 
        // txtWrdCnt
        // 
        this.txtWrdCnt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
        this.txtWrdCnt.Location = new System.Drawing.Point(167, 217);
        this.txtWrdCnt.MaxLength = 24;
        this.txtWrdCnt.Multiline = true;
        this.txtWrdCnt.Name = "txtWrdCnt";
        this.txtWrdCnt.Size = new System.Drawing.Size(36, 19);
        this.txtWrdCnt.TabIndex = 128;
        // 
        // label2
        // 
        this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
        this.label2.Location = new System.Drawing.Point(113, 196);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(46, 19);
        this.label2.Text = "Wrd Ptr";
        // 
        // txtWrdPtr
        // 
        this.txtWrdPtr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
        this.txtWrdPtr.Location = new System.Drawing.Point(167, 194);
        this.txtWrdPtr.MaxLength = 4;
        this.txtWrdPtr.Multiline = true;
        this.txtWrdPtr.Name = "txtWrdPtr";
        this.txtWrdPtr.Size = new System.Drawing.Size(36, 19);
        this.txtWrdPtr.TabIndex = 127;
        // 
        // listView2
        // 
        this.listView2.Columns.Add(this.columnHeader1);
        this.listView2.Columns.Add(this.columnHeader2);
        this.listView2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
        this.listView2.Location = new System.Drawing.Point(4, 1);
        this.listView2.Name = "listView2";
        this.listView2.Size = new System.Drawing.Size(233, 124);
        this.listView2.TabIndex = 155;
        this.listView2.View = System.Windows.Forms.View.Details;
        // 
        // columnHeader1
        // 
        this.columnHeader1.Text = "#";
        this.columnHeader1.Width = 25;
        // 
        // columnHeader2
        // 
        this.columnHeader2.Text = "EPC : Requested Data";
        this.columnHeader2.Width = 1024;
        // 
        // btn_Read
        // 
        this.btn_Read.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
        this.btn_Read.Location = new System.Drawing.Point(64, 243);
        this.btn_Read.Name = "btn_Read";
        this.btn_Read.Size = new System.Drawing.Size(56, 20);
        this.btn_Read.TabIndex = 156;
        this.btn_Read.Text = "READ";
        this.btn_Read.Click += new System.EventHandler(this.btn_Read_Click);
        // 
        // btn_Clear
        // 
        this.btn_Clear.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.btn_Clear.Location = new System.Drawing.Point(4, 243);
        this.btn_Clear.Name = "btn_Clear";
        this.btn_Clear.Size = new System.Drawing.Size(31, 20);
        this.btn_Clear.TabIndex = 167;
        this.btn_Clear.Text = "Clear";
        this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
        // 
        // btn_Auto
        // 
        this.btn_Auto.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
        this.btn_Auto.Location = new System.Drawing.Point(132, 243);
        this.btn_Auto.Name = "btn_Auto";
        this.btn_Auto.Size = new System.Drawing.Size(56, 20);
        this.btn_Auto.TabIndex = 189;
        this.btn_Auto.Text = "AUTO";
        this.btn_Auto.Click += new System.EventHandler(this.btn_Auto_Click);
        // 
        // chkInventory
        // 
        this.chkInventory.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.chkInventory.Location = new System.Drawing.Point(206, 217);
        this.chkInventory.Name = "chkInventory";
        this.chkInventory.Size = new System.Drawing.Size(31, 21);
        this.chkInventory.TabIndex = 200;
        this.chkInventory.Text = "Invent";
        this.chkInventory.Visible = false;
        // 
        // FrmGen2Read
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.AutoScroll = true;
        this.ClientSize = new System.Drawing.Size(244, 269);
        this.Controls.Add(this.chkInventory);
        this.Controls.Add(this.btn_Auto);
        this.Controls.Add(this.btn_Clear);
        this.Controls.Add(this.btn_Read);
        this.Controls.Add(this.listView2);
        this.Controls.Add(this.label13);
        this.Controls.Add(this.txtAccPwd);
        this.Controls.Add(this.txtRFAtten);
        this.Controls.Add(this.label12);
        this.Controls.Add(this.cmbStartQ);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.cmbMemBank);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.cmbAntenna);
        this.Controls.Add(this.btn_OK);
        this.Controls.Add(this.cmbSession);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.cmbSel);
        this.Controls.Add(this.label9);
        this.Controls.Add(this.lblTagID);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.cmbTarget);
        this.Controls.Add(this.txtWrdCnt);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.txtWrdPtr);
        this.Name = "FrmGen2Read";
        this.Text = "Gen2 Read";
        this.Load += new System.EventHandler(this.FrmGen2Read_Load);
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.TextBox txtAccPwd;
    private System.Windows.Forms.TextBox txtRFAtten;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.ComboBox cmbStartQ;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cmbMemBank;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cmbAntenna;
    private System.Windows.Forms.Button btn_OK;
    private System.Windows.Forms.ComboBox cmbSession;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox cmbSel;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label lblTagID;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbTarget;
    private System.Windows.Forms.TextBox txtWrdCnt;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtWrdPtr;
    private System.Windows.Forms.ListView listView2;
    private System.Windows.Forms.Button btn_Read;
    private System.Windows.Forms.Button btn_Clear;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.Button btn_Auto;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.CheckBox chkInventory;

  }
}