
namespace CS_RFID2_Sample
{
    partial class FrmSettings
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
          this.label2 = new System.Windows.Forms.Label();
          this.lblTagID = new System.Windows.Forms.Label();
          this.txtTagID = new System.Windows.Forms.TextBox();
          this.button1 = new System.Windows.Forms.Button();
          this.button2 = new System.Windows.Forms.Button();
          this.TagFilter = new System.Windows.Forms.Label();
          this.comboBox1 = new System.Windows.Forms.ComboBox();
          this.SuspendLayout();
          // 
          // label2
          // 
          this.label2.Location = new System.Drawing.Point(5, 14);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(83, 21);
          this.label2.Text = "Tag Type:";
          // 
          // lblTagID
          // 
          this.lblTagID.Location = new System.Drawing.Point(5, 51);
          this.lblTagID.Name = "lblTagID";
          this.lblTagID.Size = new System.Drawing.Size(60, 21);
          this.lblTagID.Text = "Tag ID:";
          this.lblTagID.Visible = false;
          // 
          // txtTagID
          // 
          this.txtTagID.Location = new System.Drawing.Point(82, 49);
          this.txtTagID.MaxLength = 512;
          this.txtTagID.Name = "txtTagID";
          this.txtTagID.Size = new System.Drawing.Size(151, 23);
          this.txtTagID.TabIndex = 12;
          this.txtTagID.Visible = false;
          // 
          // button1
          // 
          this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
          this.button1.Location = new System.Drawing.Point(102, 87);
          this.button1.Name = "button1";
          this.button1.Size = new System.Drawing.Size(61, 20);
          this.button1.TabIndex = 13;
          this.button1.Text = "OK";
          this.button1.Click += new System.EventHandler(this.button1_Click);
          // 
          // button2
          // 
          this.button2.Location = new System.Drawing.Point(169, 87);
          this.button2.Name = "button2";
          this.button2.Size = new System.Drawing.Size(61, 20);
          this.button2.TabIndex = 14;
          this.button2.Text = "Cancel";
          this.button2.Click += new System.EventHandler(this.button2_Click);
          // 
          // TagFilter
          // 
          this.TagFilter.Location = new System.Drawing.Point(6, 52);
          this.TagFilter.Name = "TagFilter";
          this.TagFilter.Size = new System.Drawing.Size(71, 20);
          this.TagFilter.Text = "Tag Filter";
          this.TagFilter.Visible = false;
          // 
          // comboBox1
          // 
          this.comboBox1.Items.Add("CLASS1GEN2");
          this.comboBox1.Items.Add("CLASS1");
          this.comboBox1.Items.Add("CLASS0");
          this.comboBox1.Items.Add("CLASS0+");
          this.comboBox1.Location = new System.Drawing.Point(82, 14);
          this.comboBox1.Name = "comboBox1";
          this.comboBox1.Size = new System.Drawing.Size(151, 23);
          this.comboBox1.TabIndex = 32;
          this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
          // 
          // FrmSettings
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
          this.AutoScroll = true;
          this.ClientSize = new System.Drawing.Size(242, 122);
          this.Controls.Add(this.comboBox1);
          this.Controls.Add(this.button2);
          this.Controls.Add(this.button1);
          this.Controls.Add(this.txtTagID);
          this.Controls.Add(this.lblTagID);
          this.Controls.Add(this.label2);
          this.Controls.Add(this.TagFilter);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "FrmSettings";
          this.Text = "Set Parameters";
          this.Load += new System.EventHandler(this.FrmSettings_Load);
          this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmSettings_Closing);
          this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTagID;
        private System.Windows.Forms.TextBox txtTagID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label TagFilter;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}