namespace CS_MagStripe2ControlSample1
{
    partial class Form1
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
            this.StatusBar1 = new System.Windows.Forms.StatusBar();
            this.ButtonExit = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.magStripe21 = new Symbol.MagStripe2.Design.MagStripe2();
            this.SuspendLayout();
            // 
            // StatusBar1
            // 
            this.StatusBar1.Location = new System.Drawing.Point(0, 244);
            this.StatusBar1.Name = "StatusBar1";
            this.StatusBar1.Size = new System.Drawing.Size(240, 24);
            this.StatusBar1.Text = "StatusBar1";
            // 
            // ButtonExit
            // 
            this.ButtonExit.Location = new System.Drawing.Point(3, 193);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(72, 20);
            this.ButtonExit.TabIndex = 4;
            this.ButtonExit.Text = "Exit";
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            this.ButtonExit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonExit_KeyDown);
            // 
            // listBox1
            // 
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(191, 178);
            this.listBox1.TabIndex = 2;
            // 
            // magStripe21
            // 
            this.magStripe21.EnableMagstripe = true;
            this.magStripe21.OnSwipe += new Symbol.MagStripe2.Design.MagStripe2.OnSwipeEventHandler(this.magStripe21_OnSwipe);
            this.magStripe21.OnStatus += new Symbol.MagStripe2.Design.MagStripe2.OnStatusEventHandler(this.magStripe21_OnStatus);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.StatusBar1);
            this.Controls.Add(this.ButtonExit);
            this.Name = "Form1";
            this.Text = "CS_MagStripe2ControlSample1";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.StatusBar StatusBar1;
        internal System.Windows.Forms.Button ButtonExit;
        internal System.Windows.Forms.ListBox listBox1;
        private Symbol.MagStripe2.Design.MagStripe2 magStripe21;
    }
}

