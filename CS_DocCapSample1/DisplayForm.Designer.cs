namespace CS_DocCapSample1
{
    partial class DisplayForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.Back_Btn = new System.Windows.Forms.Button();
            this.BarcodeData_Lb = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(5, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.Text = "Barcode: ";
            // 
            // Back_Btn
            // 
            this.Back_Btn.Location = new System.Drawing.Point(4, 232);
            this.Back_Btn.Name = "Back_Btn";
            this.Back_Btn.Size = new System.Drawing.Size(62, 23);
            this.Back_Btn.TabIndex = 15;
            this.Back_Btn.Text = "Back";
            this.Back_Btn.Click += new System.EventHandler(this.Back_Btn_Click);
            // 
            // BarcodeData_Lb
            // 
            this.BarcodeData_Lb.Location = new System.Drawing.Point(73, 7);
            this.BarcodeData_Lb.Name = "BarcodeData_Lb";
            this.BarcodeData_Lb.Size = new System.Drawing.Size(163, 17);
            this.BarcodeData_Lb.Text = "<NO DATA>";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(4, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(234, 185);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // DisplayForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Back_Btn);
            this.Controls.Add(this.BarcodeData_Lb);
            this.Controls.Add(this.pictureBox1);
            this.MinimizeBox = false;
            this.Name = "DisplayForm";
            this.Text = "DisplayForm";
            this.Load += new System.EventHandler(this.DisplayForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Back_Btn;
        public System.Windows.Forms.Label BarcodeData_Lb;
        public System.Windows.Forms.PictureBox pictureBox1;



    }
}