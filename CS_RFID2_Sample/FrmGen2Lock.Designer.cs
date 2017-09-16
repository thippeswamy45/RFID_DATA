namespace CS_RFID2_Sample
{
  partial class FrmGen2Lock
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
        this.label10 = new System.Windows.Forms.Label();
        this.cmbLockAction = new System.Windows.Forms.ComboBox();
        this.label11 = new System.Windows.Forms.Label();
        this.cmbRegion = new System.Windows.Forms.ComboBox();
        this.label8 = new System.Windows.Forms.Label();
        this.txtEpcid = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.cmbAntenna = new System.Windows.Forms.ComboBox();
        this.btn_Quit = new System.Windows.Forms.Button();
        this.cmbSession = new System.Windows.Forms.ComboBox();
        this.label7 = new System.Windows.Forms.Label();
        this.cmbSel = new System.Windows.Forms.ComboBox();
        this.label9 = new System.Windows.Forms.Label();
        this.lblTagID = new System.Windows.Forms.Label();
        this.cmbTarget = new System.Windows.Forms.ComboBox();
        this.btn_LOCK = new System.Windows.Forms.Button();
        this.listView2 = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.btn_Read = new System.Windows.Forms.Button();
        this.btn_Clr = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // label13
        // 
        this.label13.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label13.Location = new System.Drawing.Point(112, 133);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(46, 19);
        this.label13.Text = "AccPwd";
        // 
        // txtAccPwd
        // 
        this.txtAccPwd.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.txtAccPwd.Location = new System.Drawing.Point(160, 131);
        this.txtAccPwd.MaxLength = 10;
        this.txtAccPwd.Multiline = true;
        this.txtAccPwd.Name = "txtAccPwd";
        this.txtAccPwd.Size = new System.Drawing.Size(58, 19);
        this.txtAccPwd.TabIndex = 141;
        this.txtAccPwd.Text = "0x00000000";
        this.txtAccPwd.GotFocus += new System.EventHandler(this.txtAccPwd_GotFocus);
        // 
        // txtRFAtten
        // 
        this.txtRFAtten.Enabled = false;
        this.txtRFAtten.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.txtRFAtten.Location = new System.Drawing.Point(160, 109);
        this.txtRFAtten.MaxLength = 24;
        this.txtRFAtten.Multiline = true;
        this.txtRFAtten.Name = "txtRFAtten";
        this.txtRFAtten.Size = new System.Drawing.Size(38, 19);
        this.txtRFAtten.TabIndex = 140;
        this.txtRFAtten.Text = "000";
        this.txtRFAtten.Visible = false;
        // 
        // label12
        // 
        this.label12.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label12.Location = new System.Drawing.Point(111, 155);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(42, 17);
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
        this.cmbStartQ.Location = new System.Drawing.Point(160, 153);
        this.cmbStartQ.Name = "cmbStartQ";
        this.cmbStartQ.Size = new System.Drawing.Size(74, 19);
        this.cmbStartQ.TabIndex = 139;
        // 
        // label10
        // 
        this.label10.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label10.Location = new System.Drawing.Point(113, 177);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(47, 17);
        this.label10.Text = "Region";
        // 
        // cmbLockAction
        // 
        this.cmbLockAction.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbLockAction.Items.Add("Write with, w/o Pswd");
        this.cmbLockAction.Items.Add("Write Permanently");
        this.cmbLockAction.Items.Add("Write  with Password");
        this.cmbLockAction.Items.Add("Write Disabled Permanently ");
        this.cmbLockAction.Location = new System.Drawing.Point(42, 204);
        this.cmbLockAction.Name = "cmbLockAction";
        this.cmbLockAction.Size = new System.Drawing.Size(191, 19);
        this.cmbLockAction.TabIndex = 138;
        // 
        // label11
        // 
        this.label11.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label11.Location = new System.Drawing.Point(4, 200);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(42, 33);
        this.label11.Text = "Lock Action";
        // 
        // cmbRegion
        // 
        this.cmbRegion.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbRegion.Items.Add("KILL Pswd");
        this.cmbRegion.Items.Add("ACC Pswd");
        this.cmbRegion.Items.Add("EPC  bank");
        this.cmbRegion.Items.Add("TID   bank");
        this.cmbRegion.Items.Add("USR bank");
        this.cmbRegion.Location = new System.Drawing.Point(160, 178);
        this.cmbRegion.Name = "cmbRegion";
        this.cmbRegion.Size = new System.Drawing.Size(74, 19);
        this.cmbRegion.TabIndex = 137;
        // 
        // label8
        // 
        this.label8.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label8.Location = new System.Drawing.Point(3, 89);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(43, 15);
        this.label8.Text = "EPC Id";
        // 
        // txtEpcid
        // 
        this.txtEpcid.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.txtEpcid.Location = new System.Drawing.Point(53, 85);
        this.txtEpcid.MaxLength = 32;
        this.txtEpcid.Name = "txtEpcid";
        this.txtEpcid.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
        this.txtEpcid.Size = new System.Drawing.Size(184, 19);
        this.txtEpcid.TabIndex = 136;
        this.txtEpcid.Text = "max 32 bytes of hex ";
        this.txtEpcid.GotFocus += new System.EventHandler(this.txtEpcid_GotFocus);
        // 
        // label6
        // 
        this.label6.Enabled = false;
        this.label6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label6.Location = new System.Drawing.Point(112, 112);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(51, 16);
        this.label6.Text = "RFAtten";
        this.label6.Visible = false;
        // 
        // label4
        // 
        this.label4.Enabled = false;
        this.label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label4.Location = new System.Drawing.Point(0, 109);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(47, 18);
        this.label4.Text = "Antenna";
        this.label4.Visible = false;
        // 
        // cmbAntenna
        // 
        this.cmbAntenna.Enabled = false;
        this.cmbAntenna.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbAntenna.Items.Add("A0");
        this.cmbAntenna.Items.Add("A1");
        this.cmbAntenna.Location = new System.Drawing.Point(53, 109);
        this.cmbAntenna.Name = "cmbAntenna";
        this.cmbAntenna.Size = new System.Drawing.Size(53, 19);
        this.cmbAntenna.TabIndex = 134;
        this.cmbAntenna.Visible = false;
        // 
        // btn_Quit
        // 
        this.btn_Quit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btn_Quit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.btn_Quit.Location = new System.Drawing.Point(201, 235);
        this.btn_Quit.Name = "btn_Quit";
        this.btn_Quit.Size = new System.Drawing.Size(32, 17);
        this.btn_Quit.TabIndex = 133;
        this.btn_Quit.Text = "Quit";
        this.btn_Quit.Click += new System.EventHandler(this.btn_Quit_Click);
        // 
        // cmbSession
        // 
        this.cmbSession.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbSession.Items.Add("Sess0");
        this.cmbSession.Items.Add("Sess1");
        this.cmbSession.Items.Add("Sess2");
        this.cmbSession.Items.Add("Sess3");
        this.cmbSession.Location = new System.Drawing.Point(53, 152);
        this.cmbSession.Name = "cmbSession";
        this.cmbSession.Size = new System.Drawing.Size(53, 19);
        this.cmbSession.TabIndex = 131;
        // 
        // label7
        // 
        this.label7.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label7.Location = new System.Drawing.Point(2, 131);
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
        this.cmbSel.Location = new System.Drawing.Point(53, 130);
        this.cmbSel.Name = "cmbSel";
        this.cmbSel.Size = new System.Drawing.Size(53, 19);
        this.cmbSel.TabIndex = 130;
        // 
        // label9
        // 
        this.label9.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label9.Location = new System.Drawing.Point(2, 154);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(50, 18);
        this.label9.Text = "Session";
        // 
        // lblTagID
        // 
        this.lblTagID.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.lblTagID.Location = new System.Drawing.Point(2, 175);
        this.lblTagID.Name = "lblTagID";
        this.lblTagID.Size = new System.Drawing.Size(42, 19);
        this.lblTagID.Text = "Target";
        // 
        // cmbTarget
        // 
        this.cmbTarget.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbTarget.Items.Add("_A_");
        this.cmbTarget.Items.Add("_B_");
        this.cmbTarget.Location = new System.Drawing.Point(53, 174);
        this.cmbTarget.Name = "cmbTarget";
        this.cmbTarget.Size = new System.Drawing.Size(53, 19);
        this.cmbTarget.TabIndex = 126;
        // 
        // btn_LOCK
        // 
        this.btn_LOCK.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
        this.btn_LOCK.Location = new System.Drawing.Point(96, 231);
        this.btn_LOCK.Name = "btn_LOCK";
        this.btn_LOCK.Size = new System.Drawing.Size(57, 22);
        this.btn_LOCK.TabIndex = 171;
        this.btn_LOCK.Text = "LOCK";
        this.btn_LOCK.Click += new System.EventHandler(this.btn_LOCK_Click);
        // 
        // listView2
        // 
        this.listView2.Columns.Add(this.columnHeader1);
        this.listView2.Columns.Add(this.columnHeader2);
        this.listView2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
        this.listView2.Location = new System.Drawing.Point(4, 3);
        this.listView2.Name = "listView2";
        this.listView2.Size = new System.Drawing.Size(233, 76);
        this.listView2.TabIndex = 182;
        this.listView2.View = System.Windows.Forms.View.Details;
        this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
        // 
        // columnHeader1
        // 
        this.columnHeader1.Text = "#";
        this.columnHeader1.Width = 25;
        // 
        // columnHeader2
        // 
        this.columnHeader2.Text = "EPC : Reserved Kill & Access Pswds";
        this.columnHeader2.Width = 1024;
        // 
        // btn_Read
        // 
        this.btn_Read.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.btn_Read.Location = new System.Drawing.Point(42, 236);
        this.btn_Read.Name = "btn_Read";
        this.btn_Read.Size = new System.Drawing.Size(37, 17);
        this.btn_Read.TabIndex = 194;
        this.btn_Read.Text = "Read";
        this.btn_Read.Click += new System.EventHandler(this.btn_Read_Click);
        // 
        // btn_Clr
        // 
        this.btn_Clr.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.btn_Clr.Location = new System.Drawing.Point(4, 236);
        this.btn_Clr.Name = "btn_Clr";
        this.btn_Clr.Size = new System.Drawing.Size(32, 17);
        this.btn_Clr.TabIndex = 229;
        this.btn_Clr.Text = "Clear";
        this.btn_Clr.Click += new System.EventHandler(this.btn_Clr_Click);
        // 
        // FrmGen2Lock
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.AutoScroll = true;
        this.ClientSize = new System.Drawing.Size(241, 258);
        this.Controls.Add(this.btn_Clr);
        this.Controls.Add(this.btn_Read);
        this.Controls.Add(this.listView2);
        this.Controls.Add(this.btn_LOCK);
        this.Controls.Add(this.label13);
        this.Controls.Add(this.txtAccPwd);
        this.Controls.Add(this.txtRFAtten);
        this.Controls.Add(this.label12);
        this.Controls.Add(this.cmbStartQ);
        this.Controls.Add(this.label10);
        this.Controls.Add(this.cmbLockAction);
        this.Controls.Add(this.label11);
        this.Controls.Add(this.cmbRegion);
        this.Controls.Add(this.label8);
        this.Controls.Add(this.txtEpcid);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.cmbAntenna);
        this.Controls.Add(this.btn_Quit);
        this.Controls.Add(this.cmbSession);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.cmbSel);
        this.Controls.Add(this.label9);
        this.Controls.Add(this.lblTagID);
        this.Controls.Add(this.cmbTarget);
        this.Name = "FrmGen2Lock";
        this.Text = "Gen2 Lock";
        this.Load += new System.EventHandler(this.FrmGen2Lock_Load);
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.TextBox txtAccPwd;
    private System.Windows.Forms.TextBox txtRFAtten;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.ComboBox cmbStartQ;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.ComboBox cmbLockAction;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.ComboBox cmbRegion;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtEpcid;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cmbAntenna;
    private System.Windows.Forms.Button btn_Quit;
    private System.Windows.Forms.ComboBox cmbSession;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox cmbSel;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label lblTagID;
    private System.Windows.Forms.ComboBox cmbTarget;
    private System.Windows.Forms.Button btn_LOCK;
    private System.Windows.Forms.ListView listView2;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.Button btn_Read;
    private System.Windows.Forms.Button btn_Clr;
  }
}