namespace CS_DocCapSample1
{
    partial class MainForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Exit_Btn = new System.Windows.Forms.Button();
            this.Mode_FreeForm_RB = new System.Windows.Forms.RadioButton();
            this.Mode_BarcodeLinked_RB = new System.Windows.Forms.RadioButton();
            this.Mode_Barcode_Anchored_RB = new System.Windows.Forms.RadioButton();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.Text = "DocCapSample v2.0";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic);
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 48);
            this.label2.Text = "Use \"DocCap.pdf\" distributed with EMDK for testing this sample.";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.Text = "Select Mode:";
            // 
            // Exit_Btn
            // 
            this.Exit_Btn.Location = new System.Drawing.Point(13, 201);
            this.Exit_Btn.Name = "Exit_Btn";
            this.Exit_Btn.Size = new System.Drawing.Size(72, 20);
            this.Exit_Btn.TabIndex = 3;
            this.Exit_Btn.Text = "Exit";
            this.Exit_Btn.Click += new System.EventHandler(this.Exit_Btn_Click);
            // 
            // Mode_FreeForm_RB
            // 
            this.Mode_FreeForm_RB.Location = new System.Drawing.Point(95, 97);
            this.Mode_FreeForm_RB.Name = "Mode_FreeForm_RB";
            this.Mode_FreeForm_RB.Size = new System.Drawing.Size(100, 20);
            this.Mode_FreeForm_RB.TabIndex = 4;
            this.Mode_FreeForm_RB.Text = "Free-form";
            this.Mode_FreeForm_RB.CheckedChanged += new System.EventHandler(this.Mode_FreeForm_RB_CheckedChanged);
            // 
            // Mode_BarcodeLinked_RB
            // 
            this.Mode_BarcodeLinked_RB.Location = new System.Drawing.Point(95, 123);
            this.Mode_BarcodeLinked_RB.Name = "Mode_BarcodeLinked_RB";
            this.Mode_BarcodeLinked_RB.Size = new System.Drawing.Size(113, 20);
            this.Mode_BarcodeLinked_RB.TabIndex = 5;
            this.Mode_BarcodeLinked_RB.Text = "Barcode-linked";
            this.Mode_BarcodeLinked_RB.CheckedChanged += new System.EventHandler(this.Mode_BarcodeLinked_RB_CheckedChanged);
            // 
            // Mode_Barcode_Anchored_RB
            // 
            this.Mode_Barcode_Anchored_RB.Location = new System.Drawing.Point(95, 149);
            this.Mode_Barcode_Anchored_RB.Name = "Mode_Barcode_Anchored_RB";
            this.Mode_Barcode_Anchored_RB.Size = new System.Drawing.Size(131, 20);
            this.Mode_Barcode_Anchored_RB.TabIndex = 6;
            this.Mode_Barcode_Anchored_RB.Text = "Barcode-anchored";
            this.Mode_Barcode_Anchored_RB.CheckedChanged += new System.EventHandler(this.Mode_Barcode_Anchored_RB_CheckedChanged);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 272);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(240, 22);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.Mode_Barcode_Anchored_RB);
            this.Controls.Add(this.Mode_BarcodeLinked_RB);
            this.Controls.Add(this.Mode_FreeForm_RB);
            this.Controls.Add(this.Exit_Btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "CS_DocCapSample1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Exit_Btn;
        private System.Windows.Forms.RadioButton Mode_FreeForm_RB;
        private System.Windows.Forms.RadioButton Mode_BarcodeLinked_RB;
        private System.Windows.Forms.RadioButton Mode_Barcode_Anchored_RB;
        private System.Windows.Forms.StatusBar statusBar1;
    }
}

