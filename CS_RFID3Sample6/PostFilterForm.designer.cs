namespace CS_RFID3Sample6
{
    partial class PostFilterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tagPatternA_TP = new System.Windows.Forms.TabPage();
            this.tagMask_TB1 = new System.Windows.Forms.TextBox();
            this.filterActionLabel1 = new System.Windows.Forms.Label();
            this.MembankData_TB1 = new System.Windows.Forms.TextBox();
            this.tagMaskLabel1 = new System.Windows.Forms.Label();
            this.offset_TB1 = new System.Windows.Forms.TextBox();
            this.offsetLabel1 = new System.Windows.Forms.Label();
            this.memBank_CB1 = new System.Windows.Forms.ComboBox();
            this.memBankLabel1 = new System.Windows.Forms.Label();
            this.tagPatternB_TP = new System.Windows.Forms.TabPage();
            this.tagMask_TB2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MembankData_TB2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.offset_TB2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.memBank_CB2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.matchPattern_CB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.accessFilterButton = new System.Windows.Forms.Button();
            this.userFilter_CB = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tagPatternA_TP.SuspendLayout();
            this.tagPatternB_TP.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tagPatternA_TP);
            this.tabControl1.Controls.Add(this.tagPatternB_TP);
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 135);
            this.tabControl1.TabIndex = 0;
            // 
            // tagPatternA_TP
            // 
            this.tagPatternA_TP.Controls.Add(this.tagMask_TB1);
            this.tagPatternA_TP.Controls.Add(this.filterActionLabel1);
            this.tagPatternA_TP.Controls.Add(this.MembankData_TB1);
            this.tagPatternA_TP.Controls.Add(this.tagMaskLabel1);
            this.tagPatternA_TP.Controls.Add(this.offset_TB1);
            this.tagPatternA_TP.Controls.Add(this.offsetLabel1);
            this.tagPatternA_TP.Controls.Add(this.memBank_CB1);
            this.tagPatternA_TP.Controls.Add(this.memBankLabel1);
            this.tagPatternA_TP.Location = new System.Drawing.Point(0, 0);
            this.tagPatternA_TP.Name = "tagPatternA_TP";
            this.tagPatternA_TP.Size = new System.Drawing.Size(240, 112);
            this.tagPatternA_TP.Text = "Tag Pattern A";
            // 
            // tagMask_TB1
            // 
            this.tagMask_TB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagMask_TB1.Location = new System.Drawing.Point(104, 86);
            this.tagMask_TB1.Name = "tagMask_TB1";
            this.tagMask_TB1.Size = new System.Drawing.Size(129, 19);
            this.tagMask_TB1.TabIndex = 4;
            // 
            // filterActionLabel1
            // 
            this.filterActionLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.filterActionLabel1.Location = new System.Drawing.Point(4, 90);
            this.filterActionLabel1.Name = "filterActionLabel1";
            this.filterActionLabel1.Size = new System.Drawing.Size(90, 13);
            this.filterActionLabel1.Text = "Tag Mask (Hex)";
            // 
            // MembankData_TB1
            // 
            this.MembankData_TB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.MembankData_TB1.Location = new System.Drawing.Point(104, 61);
            this.MembankData_TB1.Name = "MembankData_TB1";
            this.MembankData_TB1.Size = new System.Drawing.Size(129, 19);
            this.MembankData_TB1.TabIndex = 3;
            // 
            // tagMaskLabel1
            // 
            this.tagMaskLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagMaskLabel1.Location = new System.Drawing.Point(4, 66);
            this.tagMaskLabel1.Name = "tagMaskLabel1";
            this.tagMaskLabel1.Size = new System.Drawing.Size(94, 24);
            this.tagMaskLabel1.Text = "Tag Pattern (Hex)";
            // 
            // offset_TB1
            // 
            this.offset_TB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.offset_TB1.Location = new System.Drawing.Point(104, 36);
            this.offset_TB1.Name = "offset_TB1";
            this.offset_TB1.Size = new System.Drawing.Size(82, 19);
            this.offset_TB1.TabIndex = 2;
            // 
            // offsetLabel1
            // 
            this.offsetLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.offsetLabel1.Location = new System.Drawing.Point(4, 40);
            this.offsetLabel1.Name = "offsetLabel1";
            this.offsetLabel1.Size = new System.Drawing.Size(90, 13);
            this.offsetLabel1.Text = "Offset (Bits)";
            // 
            // memBank_CB1
            // 
            this.memBank_CB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.memBank_CB1.ForeColor = System.Drawing.Color.Navy;
            this.memBank_CB1.Items.Add("RESERVED");
            this.memBank_CB1.Items.Add("EPC");
            this.memBank_CB1.Items.Add("TID");
            this.memBank_CB1.Items.Add("USER");
            this.memBank_CB1.Location = new System.Drawing.Point(104, 11);
            this.memBank_CB1.Name = "memBank_CB1";
            this.memBank_CB1.Size = new System.Drawing.Size(82, 20);
            this.memBank_CB1.TabIndex = 1;
            // 
            // memBankLabel1
            // 
            this.memBankLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.memBankLabel1.Location = new System.Drawing.Point(4, 13);
            this.memBankLabel1.Name = "memBankLabel1";
            this.memBankLabel1.Size = new System.Drawing.Size(72, 13);
            this.memBankLabel1.Text = "Memory Bank";
            // 
            // tagPatternB_TP
            // 
            this.tagPatternB_TP.Controls.Add(this.tagMask_TB2);
            this.tagPatternB_TP.Controls.Add(this.label2);
            this.tagPatternB_TP.Controls.Add(this.MembankData_TB2);
            this.tagPatternB_TP.Controls.Add(this.label3);
            this.tagPatternB_TP.Controls.Add(this.offset_TB2);
            this.tagPatternB_TP.Controls.Add(this.label4);
            this.tagPatternB_TP.Controls.Add(this.memBank_CB2);
            this.tagPatternB_TP.Controls.Add(this.label5);
            this.tagPatternB_TP.Location = new System.Drawing.Point(0, 0);
            this.tagPatternB_TP.Name = "tagPatternB_TP";
            this.tagPatternB_TP.Size = new System.Drawing.Size(240, 112);
            this.tagPatternB_TP.Text = "Tag Pattern B";
            // 
            // tagMask_TB2
            // 
            this.tagMask_TB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagMask_TB2.Location = new System.Drawing.Point(104, 86);
            this.tagMask_TB2.Name = "tagMask_TB2";
            this.tagMask_TB2.Size = new System.Drawing.Size(129, 19);
            this.tagMask_TB2.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(4, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.Text = "Tag Mask (Hex)";
            // 
            // MembankData_TB2
            // 
            this.MembankData_TB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.MembankData_TB2.Location = new System.Drawing.Point(104, 61);
            this.MembankData_TB2.Name = "MembankData_TB2";
            this.MembankData_TB2.Size = new System.Drawing.Size(129, 19);
            this.MembankData_TB2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(4, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 24);
            this.label3.Text = "Tag Pattern (Hex)";
            // 
            // offset_TB2
            // 
            this.offset_TB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.offset_TB2.Location = new System.Drawing.Point(104, 36);
            this.offset_TB2.Name = "offset_TB2";
            this.offset_TB2.Size = new System.Drawing.Size(82, 19);
            this.offset_TB2.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(4, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.Text = "Offset (Bits)";
            // 
            // memBank_CB2
            // 
            this.memBank_CB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.memBank_CB2.ForeColor = System.Drawing.Color.Navy;
            this.memBank_CB2.Items.Add("RESERVED");
            this.memBank_CB2.Items.Add("EPC");
            this.memBank_CB2.Items.Add("TID");
            this.memBank_CB2.Items.Add("USER");
            this.memBank_CB2.Location = new System.Drawing.Point(104, 11);
            this.memBank_CB2.Name = "memBank_CB2";
            this.memBank_CB2.Size = new System.Drawing.Size(82, 20);
            this.memBank_CB2.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(4, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.Text = "Memory Bank";
            // 
            // matchPattern_CB
            // 
            this.matchPattern_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.matchPattern_CB.Items.Add("A AND B");
            this.matchPattern_CB.Items.Add("NOTA AND B");
            this.matchPattern_CB.Items.Add("NOTA AND NOTB");
            this.matchPattern_CB.Items.Add("A AND NOTB");
            this.matchPattern_CB.Items.Add("A");
            this.matchPattern_CB.Location = new System.Drawing.Point(83, 141);
            this.matchPattern_CB.Name = "matchPattern_CB";
            this.matchPattern_CB.Size = new System.Drawing.Size(81, 20);
            this.matchPattern_CB.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(8, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.Text = "Match Pattern";
            // 
            // accessFilterButton
            // 
            this.accessFilterButton.Location = new System.Drawing.Point(182, 164);
            this.accessFilterButton.Name = "accessFilterButton";
            this.accessFilterButton.Size = new System.Drawing.Size(51, 20);
            this.accessFilterButton.TabIndex = 10;
            this.accessFilterButton.Text = "Apply";
            this.accessFilterButton.Click += new System.EventHandler(this.accessFilterButton_Click);
            // 
            // userFilter_CB
            // 
            this.userFilter_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.userFilter_CB.Location = new System.Drawing.Point(14, 164);
            this.userFilter_CB.Name = "userFilter_CB";
            this.userFilter_CB.Size = new System.Drawing.Size(100, 20);
            this.userFilter_CB.TabIndex = 12;
            this.userFilter_CB.Text = "Use Filter";
            this.userFilter_CB.CheckStateChanged += new System.EventHandler(this.userFilter_CB_CheckStateChanged);
            // 
            // PostFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.userFilter_CB);
            this.Controls.Add(this.accessFilterButton);
            this.Controls.Add(this.matchPattern_CB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "PostFilterForm";
            this.Text = "PostFilter";
            this.Load += new System.EventHandler(this.PostFilterForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.PostFilterForm_Closing);
            this.tabControl1.ResumeLayout(false);
            this.tagPatternA_TP.ResumeLayout(false);
            this.tagPatternB_TP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tagPatternA_TP;
        private System.Windows.Forms.TabPage tagPatternB_TP;
        private System.Windows.Forms.ComboBox matchPattern_CB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tagMask_TB1;
        private System.Windows.Forms.Label filterActionLabel1;
        private System.Windows.Forms.TextBox MembankData_TB1;
        private System.Windows.Forms.Label tagMaskLabel1;
        private System.Windows.Forms.TextBox offset_TB1;
        private System.Windows.Forms.Label offsetLabel1;
        private System.Windows.Forms.ComboBox memBank_CB1;
        private System.Windows.Forms.Label memBankLabel1;
        private System.Windows.Forms.TextBox tagMask_TB2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MembankData_TB2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox offset_TB2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox memBank_CB2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button accessFilterButton;
        private System.Windows.Forms.CheckBox userFilter_CB;
    }
}