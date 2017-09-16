namespace CS_ScanRSM
{
    partial class UserInputForm
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
            this.StaticMessage1_Lb = new System.Windows.Forms.Label();
            this.OK_Btn = new System.Windows.Forms.Button();
            this.Cancel_Btn = new System.Windows.Forms.Button();
            this.Value_TB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StaticMessage1_Lb
            // 
            this.StaticMessage1_Lb.Location = new System.Drawing.Point(19, 67);
            this.StaticMessage1_Lb.Name = "StaticMessage1_Lb";
            this.StaticMessage1_Lb.Size = new System.Drawing.Size(198, 22);
            this.StaticMessage1_Lb.Text = "Static Message1";
            this.StaticMessage1_Lb.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // OK_Btn
            // 
            this.OK_Btn.Location = new System.Drawing.Point(30, 162);
            this.OK_Btn.Name = "OK_Btn";
            this.OK_Btn.Size = new System.Drawing.Size(73, 35);
            this.OK_Btn.TabIndex = 1;
            this.OK_Btn.Text = "OK";
            this.OK_Btn.Click += new System.EventHandler(this.OK_Btn_Click);
            this.OK_Btn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OK_Btn_KeyDown);
            // 
            // Cancel_Btn
            // 
            this.Cancel_Btn.Location = new System.Drawing.Point(122, 162);
            this.Cancel_Btn.Name = "Cancel_Btn";
            this.Cancel_Btn.Size = new System.Drawing.Size(73, 35);
            this.Cancel_Btn.TabIndex = 2;
            this.Cancel_Btn.Text = "CANCEL";
            this.Cancel_Btn.Click += new System.EventHandler(this.Cancel_Btn_Click);
            this.Cancel_Btn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Cancel_Btn_KeyDown);
            // 
            // Value_TB
            // 
            this.Value_TB.Location = new System.Drawing.Point(63, 110);
            this.Value_TB.Name = "Value_TB";
            this.Value_TB.Size = new System.Drawing.Size(100, 23);
            this.Value_TB.TabIndex = 3;
            this.Value_TB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Value_TB_KeyDown);
            // 
            // UserInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 272);
            this.ControlBox = false;
            this.Controls.Add(this.Value_TB);
            this.Controls.Add(this.Cancel_Btn);
            this.Controls.Add(this.OK_Btn);
            this.Controls.Add(this.StaticMessage1_Lb);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserInputForm";
            this.Text = "Title";
            this.Resize += new System.EventHandler(this.UserInputForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label StaticMessage1_Lb;
        private System.Windows.Forms.Button OK_Btn;
        private System.Windows.Forms.Button Cancel_Btn;
        private System.Windows.Forms.TextBox Value_TB;
    }
}