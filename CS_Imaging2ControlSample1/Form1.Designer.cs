namespace CS_Imaging2ControlSample1
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
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.buttonExit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.imaging21 = new Symbol.Imaging2.Design.Imaging2();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 268);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(240, 26);
            this.statusBar1.Text = "Press Trigger to Capture Image.";
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(3, 215);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(72, 20);
            this.buttonExit.TabIndex = 0;
            this.buttonExit.Text = "Exit";
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            this.buttonExit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonExit_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(234, 199);
            // 
            // imaging21
            // 
            this.imaging21.Config.ImageFileName = "MyPicture.bmp";
            this.imaging21.Config.ImageFormat.FileFormat = Symbol.Imaging2.Design.FileFormats.Default;
            this.imaging21.Config.ImageFormat.FlipHorizontal = Symbol.Imaging2.Design.BOOL_VALUES.Default;
            this.imaging21.Config.ImageFormat.FlipVertical = Symbol.Imaging2.Design.BOOL_VALUES.Default;
            this.imaging21.Config.ImageFormat.JPEGQuality = -1;
            this.imaging21.Config.ImageFormat.JPEGSize = -1;
            this.imaging21.Config.ImageFormat.Negative = Symbol.Imaging2.Design.BOOL_VALUES.Default;
            this.imaging21.Config.Trigger = Symbol.Imaging2.Triggers.ALLTRIGGERS;
            this.imaging21.Config.ViewFinder = this.pictureBox1;
            this.imaging21.DeviceType = Symbol.Imaging2.DEVICETYPES.FIRSTAVAILABLE;
            this.imaging21.EnableImager = true;
            this.imaging21.OnCapture += new Symbol.Imaging2.Design.Imaging2.CaptureEventHandler(this.imaging21_OnCapture);
            this.imaging21.OnStatus += new Symbol.Imaging2.Design.Imaging2.StatusEventHandler(this.imaging21_OnStatus);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.buttonExit);
            this.Name = "Form1";
            this.Text = "CS_Imaging2ControlSample1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Symbol.Imaging2.Design.Imaging2 imaging21;
    }
}

