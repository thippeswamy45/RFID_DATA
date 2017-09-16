namespace CS_RFID2_Sample
{
  partial class FrmGen2Kill
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
        this.txtRFAtten = new System.Windows.Forms.TextBox();
        this.label12 = new System.Windows.Forms.Label();
        this.cmbStartQ = new System.Windows.Forms.ComboBox();
        this.label8 = new System.Windows.Forms.Label();
        this.txtEpcid = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.cmbAntenna = new System.Windows.Forms.ComboBox();
        this.btn_Cancel = new System.Windows.Forms.Button();
        this.cmbSession = new System.Windows.Forms.ComboBox();
        this.label7 = new System.Windows.Forms.Label();
        this.cmbSel = new System.Windows.Forms.ComboBox();
        this.label9 = new System.Windows.Forms.Label();
        this.lblTagID = new System.Windows.Forms.Label();
        this.cmbTarget = new System.Windows.Forms.ComboBox();
        this.txtKillPwd = new System.Windows.Forms.TextBox();
        this.btn_KILL = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // label13
        // 
        this.label13.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label13.Location = new System.Drawing.Point(137, 95);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(47, 19);
        this.label13.Text = "KillPwd";
        // 
        // txtRFAtten
        // 
        this.txtRFAtten.Enabled = false;
        this.txtRFAtten.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.txtRFAtten.Location = new System.Drawing.Point(185, 72);
        this.txtRFAtten.MaxLength = 24;
        this.txtRFAtten.Name = "txtRFAtten";
        this.txtRFAtten.Size = new System.Drawing.Size(40, 19);
        this.txtRFAtten.TabIndex = 162;
        this.txtRFAtten.Text = "000";
        this.txtRFAtten.Visible = false;
        // 
        // label12
        // 
        this.label12.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label12.Location = new System.Drawing.Point(7, 160);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(42, 19);
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
        this.cmbStartQ.Location = new System.Drawing.Point(62, 160);
        this.cmbStartQ.Name = "cmbStartQ";
        this.cmbStartQ.Size = new System.Drawing.Size(70, 19);
        this.cmbStartQ.TabIndex = 161;
        // 
        // label8
        // 
        this.label8.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label8.Location = new System.Drawing.Point(5, 11);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(44, 15);
        this.label8.Text = "EPC Id";
        // 
        // txtEpcid
        // 
        this.txtEpcid.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.txtEpcid.Location = new System.Drawing.Point(51, 11);
        this.txtEpcid.MaxLength = 64;
        this.txtEpcid.Name = "txtEpcid";
        this.txtEpcid.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
        this.txtEpcid.Size = new System.Drawing.Size(182, 19);
        this.txtEpcid.TabIndex = 158;
        this.txtEpcid.Text = "Needed for Filtered Lock";
        this.txtEpcid.GotFocus += new System.EventHandler(this.txtEpcid_GotFocus);
        // 
        // label6
        // 
        this.label6.Enabled = false;
        this.label6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label6.Location = new System.Drawing.Point(137, 75);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(46, 16);
        this.label6.Text = "RFAtten";
        this.label6.Visible = false;
        // 
        // label4
        // 
        this.label4.Enabled = false;
        this.label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label4.Location = new System.Drawing.Point(6, 73);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(51, 18);
        this.label4.Text = "Antenna";
        this.label4.Visible = false;
        // 
        // cmbAntenna
        // 
        this.cmbAntenna.Enabled = false;
        this.cmbAntenna.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbAntenna.Items.Add("A0");
        this.cmbAntenna.Items.Add("A1");
        this.cmbAntenna.Location = new System.Drawing.Point(62, 72);
        this.cmbAntenna.Name = "cmbAntenna";
        this.cmbAntenna.Size = new System.Drawing.Size(70, 19);
        this.cmbAntenna.TabIndex = 157;
        this.cmbAntenna.Visible = false;
        // 
        // btn_Cancel
        // 
        this.btn_Cancel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.btn_Cancel.Location = new System.Drawing.Point(173, 225);
        this.btn_Cancel.Name = "btn_Cancel";
        this.btn_Cancel.Size = new System.Drawing.Size(52, 16);
        this.btn_Cancel.TabIndex = 156;
        this.btn_Cancel.Text = "Finished";
        this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
        // 
        // cmbSession
        // 
        this.cmbSession.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbSession.Items.Add("Sess0");
        this.cmbSession.Items.Add("Sess1");
        this.cmbSession.Items.Add("Sess2");
        this.cmbSession.Items.Add("Sess3");
        this.cmbSession.Location = new System.Drawing.Point(62, 115);
        this.cmbSession.Name = "cmbSession";
        this.cmbSession.Size = new System.Drawing.Size(70, 19);
        this.cmbSession.TabIndex = 154;
        // 
        // label7
        // 
        this.label7.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label7.Location = new System.Drawing.Point(7, 94);
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
        this.cmbSel.Location = new System.Drawing.Point(62, 93);
        this.cmbSel.Name = "cmbSel";
        this.cmbSel.Size = new System.Drawing.Size(70, 19);
        this.cmbSel.TabIndex = 153;
        // 
        // label9
        // 
        this.label9.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.label9.Location = new System.Drawing.Point(7, 117);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(49, 18);
        this.label9.Text = "Session";
        // 
        // lblTagID
        // 
        this.lblTagID.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.lblTagID.Location = new System.Drawing.Point(7, 138);
        this.lblTagID.Name = "lblTagID";
        this.lblTagID.Size = new System.Drawing.Size(42, 19);
        this.lblTagID.Text = "Target";
        // 
        // cmbTarget
        // 
        this.cmbTarget.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.cmbTarget.Items.Add("_A_");
        this.cmbTarget.Items.Add("_B_");
        this.cmbTarget.Location = new System.Drawing.Point(62, 138);
        this.cmbTarget.Name = "cmbTarget";
        this.cmbTarget.Size = new System.Drawing.Size(70, 19);
        this.cmbTarget.TabIndex = 152;
        // 
        // txtKillPwd
        // 
        this.txtKillPwd.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
        this.txtKillPwd.Location = new System.Drawing.Point(184, 97);
        this.txtKillPwd.MaxLength = 16;
        this.txtKillPwd.Multiline = true;
        this.txtKillPwd.Name = "txtKillPwd";
        this.txtKillPwd.Size = new System.Drawing.Size(41, 19);
        this.txtKillPwd.TabIndex = 180;
        this.txtKillPwd.Text = "0";
        this.txtKillPwd.GotFocus += new System.EventHandler(this.txtKillPwd_GotFocus);
        // 
        // btn_KILL
        // 
        this.btn_KILL.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.btn_KILL.Location = new System.Drawing.Point(74, 204);
        this.btn_KILL.Name = "btn_KILL";
        this.btn_KILL.Size = new System.Drawing.Size(77, 16);
        this.btn_KILL.TabIndex = 189;
        this.btn_KILL.Text = "KILL TAG";
        this.btn_KILL.Click += new System.EventHandler(this.btn_KILL_Click);
        // 
        // FrmGen2Kill
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.AutoScroll = true;
        this.ClientSize = new System.Drawing.Size(236, 258);
        this.Controls.Add(this.btn_KILL);
        this.Controls.Add(this.txtKillPwd);
        this.Controls.Add(this.label13);
        this.Controls.Add(this.txtRFAtten);
        this.Controls.Add(this.label12);
        this.Controls.Add(this.cmbStartQ);
        this.Controls.Add(this.label8);
        this.Controls.Add(this.txtEpcid);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.cmbAntenna);
        this.Controls.Add(this.btn_Cancel);
        this.Controls.Add(this.cmbSession);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.cmbSel);
        this.Controls.Add(this.label9);
        this.Controls.Add(this.lblTagID);
        this.Controls.Add(this.cmbTarget);
        this.Name = "FrmGen2Kill";
        this.Text = "Gen2 Kill ";
        this.Load += new System.EventHandler(this.FrmGen2Kill_Load);
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.TextBox txtRFAtten;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.ComboBox cmbStartQ;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtEpcid;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cmbAntenna;
    private System.Windows.Forms.Button btn_Cancel;
    private System.Windows.Forms.ComboBox cmbSession;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox cmbSel;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label lblTagID;
    private System.Windows.Forms.ComboBox cmbTarget;
    private System.Windows.Forms.TextBox txtKillPwd;
    private System.Windows.Forms.Button btn_KILL;
  }
}