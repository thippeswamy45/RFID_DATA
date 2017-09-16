namespace CS_RFID2_Sample
{
  partial class FrmSelectRecord
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
        this.btn_OK = new System.Windows.Forms.Button();
        this.btn_Cancel = new System.Windows.Forms.Button();
        this.lblDescCount = new System.Windows.Forms.Label();
        this.lblTagID = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.cmbMemBank = new System.Windows.Forms.ComboBox();
        this.txtLength = new System.Windows.Forms.TextBox();
        this.txtMask = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.txtBitPtr = new System.Windows.Forms.TextBox();
        this.cmbAction = new System.Windows.Forms.ComboBox();
        this.label7 = new System.Windows.Forms.Label();
        this.cmbTarget = new System.Windows.Forms.ComboBox();
        this.label9 = new System.Windows.Forms.Label();
        this.txtTruncate = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.cmbIndex = new System.Windows.Forms.ComboBox();
        this.label5 = new System.Windows.Forms.Label();
        this.panel2 = new System.Windows.Forms.Panel();
        this.btn_Refresh = new System.Windows.Forms.Button();
        this.btn_Remove = new System.Windows.Forms.Button();
        this.btn_Add = new System.Windows.Forms.Button();
        this.txtInUse = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.panel2.SuspendLayout();
        this.SuspendLayout();
        // 
        // btn_OK
        // 
        this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.btn_OK.Location = new System.Drawing.Point(61, 223);
        this.btn_OK.Name = "btn_OK";
        this.btn_OK.Size = new System.Drawing.Size(61, 17);
        this.btn_OK.TabIndex = 31;
        this.btn_OK.Text = "OK";
        this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
        // 
        // btn_Cancel
        // 
        this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btn_Cancel.Location = new System.Drawing.Point(128, 223);
        this.btn_Cancel.Name = "btn_Cancel";
        this.btn_Cancel.Size = new System.Drawing.Size(61, 17);
        this.btn_Cancel.TabIndex = 32;
        this.btn_Cancel.Text = "Cancel";
        this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
        // 
        // lblDescCount
        // 
        this.lblDescCount.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.lblDescCount.Location = new System.Drawing.Point(10, 190);
        this.lblDescCount.Name = "lblDescCount";
        this.lblDescCount.Size = new System.Drawing.Size(52, 15);
        this.lblDescCount.Text = "Truncate";
        // 
        // lblTagID
        // 
        this.lblTagID.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.lblTagID.Location = new System.Drawing.Point(9, 88);
        this.lblTagID.Name = "lblTagID";
        this.lblTagID.Size = new System.Drawing.Size(42, 19);
        this.lblTagID.Text = "Bank";
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.label1.Location = new System.Drawing.Point(11, 144);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(44, 19);
        this.label1.Text = "Length";
        // 
        // cmbMemBank
        // 
        this.cmbMemBank.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.cmbMemBank.Items.Add("EPC");
        this.cmbMemBank.Items.Add("TID");
        this.cmbMemBank.Items.Add("USER");
        this.cmbMemBank.Location = new System.Drawing.Point(64, 88);
        this.cmbMemBank.Name = "cmbMemBank";
        this.cmbMemBank.Size = new System.Drawing.Size(69, 19);
        this.cmbMemBank.TabIndex = 41;
        // 
        // txtLength
        // 
        this.txtLength.Location = new System.Drawing.Point(63, 136);
        this.txtLength.MaxLength = 24;
        this.txtLength.Multiline = true;
        this.txtLength.Name = "txtLength";
        this.txtLength.Size = new System.Drawing.Size(70, 19);
        this.txtLength.TabIndex = 43;
        // 
        // txtMask
        // 
        this.txtMask.Location = new System.Drawing.Point(63, 163);
        this.txtMask.MaxLength = 24;
        this.txtMask.Multiline = true;
        this.txtMask.Name = "txtMask";
        this.txtMask.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
        this.txtMask.Size = new System.Drawing.Size(161, 18);
        this.txtMask.TabIndex = 44;
        this.txtMask.Text = "Max 32 bytes of hex ";
        this.txtMask.GotFocus += new System.EventHandler(this.txtMask_GotFocus);
        // 
        // label2
        // 
        this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.label2.Location = new System.Drawing.Point(11, 117);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(40, 19);
        this.label2.Text = "Bit Ptr";
        // 
        // label3
        // 
        this.label3.Location = new System.Drawing.Point(11, 164);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(44, 18);
        this.label3.Text = "Mask";
        // 
        // txtBitPtr
        // 
        this.txtBitPtr.Location = new System.Drawing.Point(63, 112);
        this.txtBitPtr.MaxLength = 4;
        this.txtBitPtr.Multiline = true;
        this.txtBitPtr.Name = "txtBitPtr";
        this.txtBitPtr.Size = new System.Drawing.Size(70, 19);
        this.txtBitPtr.TabIndex = 42;
        // 
        // cmbAction
        // 
        this.cmbAction.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.cmbAction.Items.Add("AB     0");
        this.cmbAction.Items.Add("AN     1 ");
        this.cmbAction.Items.Add("NB     2 ");
        this.cmbAction.Items.Add("ABBA 3");
        this.cmbAction.Items.Add("BA      4");
        this.cmbAction.Items.Add("BN      5");
        this.cmbAction.Items.Add("NA      6");
        this.cmbAction.Items.Add("GB      7");
        this.cmbAction.Location = new System.Drawing.Point(64, 65);
        this.cmbAction.Name = "cmbAction";
        this.cmbAction.Size = new System.Drawing.Size(69, 19);
        this.cmbAction.TabIndex = 68;
        // 
        // label7
        // 
        this.label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.label7.Location = new System.Drawing.Point(8, 44);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(41, 18);
        this.label7.Text = "Target";
        // 
        // cmbTarget
        // 
        this.cmbTarget.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.cmbTarget.Items.Add("Sess 0");
        this.cmbTarget.Items.Add("Sess 1");
        this.cmbTarget.Items.Add("Sess 2");
        this.cmbTarget.Items.Add("Sess 3");
        this.cmbTarget.Items.Add("_SL_");
        this.cmbTarget.Location = new System.Drawing.Point(64, 43);
        this.cmbTarget.Name = "cmbTarget";
        this.cmbTarget.Size = new System.Drawing.Size(69, 19);
        this.cmbTarget.TabIndex = 67;
        // 
        // label9
        // 
        this.label9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.label9.Location = new System.Drawing.Point(8, 67);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(41, 18);
        this.label9.Text = "Action";
        // 
        // txtTruncate
        // 
        this.txtTruncate.Location = new System.Drawing.Point(63, 190);
        this.txtTruncate.MaxLength = 24;
        this.txtTruncate.Multiline = true;
        this.txtTruncate.Name = "txtTruncate";
        this.txtTruncate.Size = new System.Drawing.Size(106, 19);
        this.txtTruncate.TabIndex = 76;
        this.txtTruncate.Text = "0";
        // 
        // label4
        // 
        this.label4.Font = new System.Drawing.Font("Arial", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
        this.label4.Location = new System.Drawing.Point(41, 9);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(50, 18);
        this.label4.Text = "INDEX";
        // 
        // cmbIndex
        // 
        this.cmbIndex.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.cmbIndex.Items.Add("0");
        this.cmbIndex.Items.Add("1");
        this.cmbIndex.Items.Add("2");
        this.cmbIndex.Items.Add("3");
        this.cmbIndex.Location = new System.Drawing.Point(98, 9);
        this.cmbIndex.Name = "cmbIndex";
        this.cmbIndex.Size = new System.Drawing.Size(36, 19);
        this.cmbIndex.TabIndex = 85;
        this.cmbIndex.SelectedIndexChanged += new System.EventHandler(this.cmbIndex_SelectedIndexChanged);
        // 
        // label5
        // 
        this.label5.Font = new System.Drawing.Font("Arial", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
        this.label5.Location = new System.Drawing.Point(8, 7);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(68, 18);
        this.label5.Text = "ACTIONS";
        // 
        // panel2
        // 
        this.panel2.Controls.Add(this.btn_Refresh);
        this.panel2.Controls.Add(this.btn_Remove);
        this.panel2.Controls.Add(this.btn_Add);
        this.panel2.Controls.Add(this.label5);
        this.panel2.Location = new System.Drawing.Point(139, 43);
        this.panel2.Name = "panel2";
        this.panel2.Size = new System.Drawing.Size(86, 110);
        // 
        // btn_Refresh
        // 
        this.btn_Refresh.Location = new System.Drawing.Point(9, 82);
        this.btn_Refresh.Name = "btn_Refresh";
        this.btn_Refresh.Size = new System.Drawing.Size(56, 16);
        this.btn_Refresh.TabIndex = 108;
        this.btn_Refresh.Text = "Refresh";
        this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
        // 
        // btn_Remove
        // 
        this.btn_Remove.Location = new System.Drawing.Point(10, 58);
        this.btn_Remove.Name = "btn_Remove";
        this.btn_Remove.Size = new System.Drawing.Size(56, 16);
        this.btn_Remove.TabIndex = 106;
        this.btn_Remove.Text = "Remove";
        this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
        // 
        // btn_Add
        // 
        this.btn_Add.Location = new System.Drawing.Point(10, 34);
        this.btn_Add.Name = "btn_Add";
        this.btn_Add.Size = new System.Drawing.Size(56, 16);
        this.btn_Add.TabIndex = 93;
        this.btn_Add.Text = "Add";
        this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
        // 
        // txtInUse
        // 
        this.txtInUse.Enabled = false;
        this.txtInUse.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.txtInUse.Location = new System.Drawing.Point(194, 10);
        this.txtInUse.MaxLength = 24;
        this.txtInUse.Multiline = true;
        this.txtInUse.Name = "txtInUse";
        this.txtInUse.Size = new System.Drawing.Size(21, 21);
        this.txtInUse.TabIndex = 93;
        this.txtInUse.Text = "0";
        // 
        // label6
        // 
        this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
        this.label6.Location = new System.Drawing.Point(152, 13);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(38, 15);
        this.label6.Text = "In Use";
        // 
        // FrmSelectRecord
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.AutoScroll = true;
        this.ClientSize = new System.Drawing.Size(246, 268);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.txtInUse);
        this.Controls.Add(this.panel2);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.cmbIndex);
        this.Controls.Add(this.txtTruncate);
        this.Controls.Add(this.cmbAction);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.cmbTarget);
        this.Controls.Add(this.label9);
        this.Controls.Add(this.lblDescCount);
        this.Controls.Add(this.lblTagID);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.cmbMemBank);
        this.Controls.Add(this.txtLength);
        this.Controls.Add(this.txtMask);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.txtBitPtr);
        this.Controls.Add(this.btn_OK);
        this.Controls.Add(this.btn_Cancel);
        this.Name = "FrmSelectRecord";
        this.Text = "SelRec Manager";
        this.Load += new System.EventHandler(this.FrmSelectRecord_Load);
        this.panel2.ResumeLayout(false);
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btn_OK;
    private System.Windows.Forms.Button btn_Cancel;
    private System.Windows.Forms.Label lblDescCount;
    private System.Windows.Forms.Label lblTagID;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbMemBank;
    private System.Windows.Forms.TextBox txtLength;
    private System.Windows.Forms.TextBox txtMask;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtBitPtr;
    private System.Windows.Forms.ComboBox cmbAction;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox cmbTarget;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtTruncate;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cmbIndex;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button btn_Add;
    private System.Windows.Forms.Button btn_Remove;
    private System.Windows.Forms.Button btn_Refresh;
    private System.Windows.Forms.TextBox txtInUse;
    private System.Windows.Forms.Label label6;
  }
}