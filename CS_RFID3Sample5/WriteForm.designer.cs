namespace CS_RFID3Sample5
{
    partial class WriteForm
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
            this.data_TB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Password_TB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.writeButton = new System.Windows.Forms.Button();
            this.length_TB = new System.Windows.Forms.TextBox();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.tagID_TB = new System.Windows.Forms.TextBox();
            this.tagMaskLabel = new System.Windows.Forms.Label();
            this.offset_TB = new System.Windows.Forms.TextBox();
            this.offsetLabel = new System.Windows.Forms.Label();
            this.memBank_CB = new System.Windows.Forms.ComboBox();
            this.memBankLabel1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // data_TB
            // 
            this.data_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.data_TB.Location = new System.Drawing.Point(81, 104);
            this.data_TB.Multiline = true;
            this.data_TB.Name = "data_TB";
            this.data_TB.Size = new System.Drawing.Size(156, 50);
            this.data_TB.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(3, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.Text = "Data (Hex)";
            // 
            // Password_TB
            // 
            this.Password_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.Password_TB.Location = new System.Drawing.Point(81, 28);
            this.Password_TB.Name = "Password_TB";
            this.Password_TB.Size = new System.Drawing.Size(155, 19);
            this.Password_TB.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.Text = "Password (Hex)";
            // 
            // writeButton
            // 
            this.writeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.writeButton.Location = new System.Drawing.Point(163, 159);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(70, 23);
            this.writeButton.TabIndex = 8;
            this.writeButton.Text = "Write";
            this.writeButton.Click += new System.EventHandler(this.writeButton_Click);
            // 
            // length_TB
            // 
            this.length_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.length_TB.Location = new System.Drawing.Point(187, 79);
            this.length_TB.Name = "length_TB";
            this.length_TB.Size = new System.Drawing.Size(50, 19);
            this.length_TB.TabIndex = 5;
            // 
            // lengthLabel
            // 
            this.lengthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.lengthLabel.Location = new System.Drawing.Point(141, 76);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size(40, 25);
            this.lengthLabel.Text = "Length (Bytes)";
            // 
            // tagID_TB
            // 
            this.tagID_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagID_TB.Location = new System.Drawing.Point(81, 3);
            this.tagID_TB.Name = "tagID_TB";
            this.tagID_TB.Size = new System.Drawing.Size(155, 19);
            this.tagID_TB.TabIndex = 1;
            // 
            // tagMaskLabel
            // 
            this.tagMaskLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.tagMaskLabel.Location = new System.Drawing.Point(3, 9);
            this.tagMaskLabel.Name = "tagMaskLabel";
            this.tagMaskLabel.Size = new System.Drawing.Size(72, 13);
            this.tagMaskLabel.Text = "Tag ID (Hex)";
            // 
            // offset_TB
            // 
            this.offset_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.offset_TB.Location = new System.Drawing.Point(81, 79);
            this.offset_TB.Name = "offset_TB";
            this.offset_TB.Size = new System.Drawing.Size(50, 19);
            this.offset_TB.TabIndex = 4;
            // 
            // offsetLabel
            // 
            this.offsetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.offsetLabel.Location = new System.Drawing.Point(3, 83);
            this.offsetLabel.Name = "offsetLabel";
            this.offsetLabel.Size = new System.Drawing.Size(72, 13);
            this.offsetLabel.Text = "Offset (Bytes)";
            // 
            // memBank_CB
            // 
            this.memBank_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.memBank_CB.ForeColor = System.Drawing.Color.Navy;
            this.memBank_CB.Items.Add("RESERVED");
            this.memBank_CB.Items.Add("EPC");
            this.memBank_CB.Items.Add("TID");
            this.memBank_CB.Items.Add("USER");
            this.memBank_CB.Location = new System.Drawing.Point(81, 53);
            this.memBank_CB.Name = "memBank_CB";
            this.memBank_CB.Size = new System.Drawing.Size(110, 20);
            this.memBank_CB.TabIndex = 3;
            // 
            // memBankLabel1
            // 
            this.memBankLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.memBankLabel1.Location = new System.Drawing.Point(3, 57);
            this.memBankLabel1.Name = "memBankLabel1";
            this.memBankLabel1.Size = new System.Drawing.Size(72, 13);
            this.memBankLabel1.Text = "Memory Bank";
            // 
            // WriteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.data_TB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Password_TB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.writeButton);
            this.Controls.Add(this.length_TB);
            this.Controls.Add(this.lengthLabel);
            this.Controls.Add(this.tagID_TB);
            this.Controls.Add(this.tagMaskLabel);
            this.Controls.Add(this.offset_TB);
            this.Controls.Add(this.offsetLabel);
            this.Controls.Add(this.memBank_CB);
            this.Controls.Add(this.memBankLabel1);
            this.Menu = this.mainMenu1;
            this.Name = "WriteForm";
            this.Text = "Write/Block Write";
            this.Load += new System.EventHandler(this.WriteForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox data_TB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Password_TB;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button writeButton;
        internal System.Windows.Forms.TextBox length_TB;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.TextBox tagID_TB;
        private System.Windows.Forms.Label tagMaskLabel;
        internal System.Windows.Forms.TextBox offset_TB;
        private System.Windows.Forms.Label offsetLabel;
        internal System.Windows.Forms.ComboBox memBank_CB;
        private System.Windows.Forms.Label memBankLabel1;
    }
}