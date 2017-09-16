//--------------------------------------------------------------------
// FILENAME: ImagerForm.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION:
//
// NOTES:
//
// 
//--------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using Symbol.Imaging2;

namespace CS_Imager2Sample1
{
	/// <summary>
	/// ImagerForm class.
	/// </summary>
	public class ImagerForm : System.Windows.Forms.Form
    {
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.MenuItem menuItemOptions;
		private System.Windows.Forms.MenuItem menuItemHelp;
		private System.Windows.Forms.MenuItem menuItemStart;
		private System.Windows.Forms.MenuItem menuItemGet;
		private System.Windows.Forms.MenuItem menuItemSave;
		private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItemResolution;
		private System.Windows.Forms.MenuItem menuItemLamp;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.StatusBar statusBar1;

        private Imaging2 myImaging2 = null;
        private MenuItem []resolutionMenuItems = null;
        private int lastCheckedresolutionMenuItem = 0;

        private ImageData imageData;
        private byte[] imageDataInBytes = null;
        ArrayList triggerList;

        private static bool bPortrait = true;   // The default dispaly orientation 
        // has been set to Portrait.

        private bool bSkipMaxLen = false;    // The restriction on the maximum 
        // physical length is considered by default.

        private bool bInitialScale = true;   // The flag to track whether the 
        // scaling logic is applied for
        // the first time (from scatch) or not.
        // Based on that, the (outer) width/height values
        // of the form will be set or not.
        // Initially set to true.

        private int resWidthReference = 242;   // The (cached) width of the form. 
        // INITIALLY HAS TO BE SET TO THE WIDTH OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private int resHeightReference = 295;
        private MenuItem menuItemJPEGQuality;
        private MenuItem menuItemJPEGQuality100;
        private MenuItem menuItemJPEGQuality50;
        private MenuItem menuItemJPEGQuality25;
        private MenuItem menuItemJPEGQuality75;
        private PictureBox pictureBox1;  // The (cached) height of the form.
        // INITIALLY HAS TO BE SET TO THE HEIGHT OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private const double maxLength = 5.5;  // The maximum physical width/height of the sample (in inches).
        // The actual value on the device may slightly deviate from this
        // since the calculations based on the (received) DPI & resolution values 
        // would provide only an approximation, so not 100% accurate.

	
		public ImagerForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemStart = new System.Windows.Forms.MenuItem();
            this.menuItemGet = new System.Windows.Forms.MenuItem();
            this.menuItemSave = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItemOptions = new System.Windows.Forms.MenuItem();
            this.menuItemResolution = new System.Windows.Forms.MenuItem();
            this.menuItemJPEGQuality = new System.Windows.Forms.MenuItem();
            this.menuItemJPEGQuality100 = new System.Windows.Forms.MenuItem();
            this.menuItemJPEGQuality75 = new System.Windows.Forms.MenuItem();
            this.menuItemJPEGQuality50 = new System.Windows.Forms.MenuItem();
            this.menuItemJPEGQuality25 = new System.Windows.Forms.MenuItem();
            this.menuItemLamp = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemFile);
            this.mainMenu1.MenuItems.Add(this.menuItemOptions);
            this.mainMenu1.MenuItems.Add(this.menuItemHelp);
            // 
            // menuItemFile
            // 
            this.menuItemFile.MenuItems.Add(this.menuItemStart);
            this.menuItemFile.MenuItems.Add(this.menuItemGet);
            this.menuItemFile.MenuItems.Add(this.menuItemSave);
            this.menuItemFile.MenuItems.Add(this.menuItemExit);
            this.menuItemFile.Text = "File";
            // 
            // menuItemStart
            // 
            this.menuItemStart.Text = "Start Acquisition";
            this.menuItemStart.Click += new System.EventHandler(this.menuItemStart_Click);
            // 
            // menuItemGet
            // 
            this.menuItemGet.Text = "Capture Image";
            this.menuItemGet.Click += new System.EventHandler(this.menuItemGet_Click);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Text = "Save As";
            this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemOptions
            // 
            this.menuItemOptions.MenuItems.Add(this.menuItemResolution);
            this.menuItemOptions.MenuItems.Add(this.menuItemJPEGQuality);
            this.menuItemOptions.MenuItems.Add(this.menuItemLamp);
            this.menuItemOptions.Text = "Options";
            // 
            // menuItemResolution
            // 
            this.menuItemResolution.Text = "Resolution";
            // 
            // menuItemJPEGQuality
            // 
            this.menuItemJPEGQuality.MenuItems.Add(this.menuItemJPEGQuality100);
            this.menuItemJPEGQuality.MenuItems.Add(this.menuItemJPEGQuality75);
            this.menuItemJPEGQuality.MenuItems.Add(this.menuItemJPEGQuality50);
            this.menuItemJPEGQuality.MenuItems.Add(this.menuItemJPEGQuality25);
            this.menuItemJPEGQuality.Text = "JPEGQuality";
            // 
            // menuItemJPEGQuality100
            // 
            this.menuItemJPEGQuality100.Text = "100%";
            this.menuItemJPEGQuality100.Click += new System.EventHandler(this.menuItemJPEGQuality_Click);
            // 
            // menuItemJPEGQuality75
            // 
            this.menuItemJPEGQuality75.Checked = true;
            this.menuItemJPEGQuality75.Text = "75%";
            this.menuItemJPEGQuality75.Click += new System.EventHandler(this.menuItemJPEGQuality_Click);
            // 
            // menuItemJPEGQuality50
            // 
            this.menuItemJPEGQuality50.Text = "50%";
            this.menuItemJPEGQuality50.Click += new System.EventHandler(this.menuItemJPEGQuality_Click);
            // 
            // menuItemJPEGQuality25
            // 
            this.menuItemJPEGQuality25.Text = "25%";
            this.menuItemJPEGQuality25.Click += new System.EventHandler(this.menuItemJPEGQuality_Click);
            // 
            // menuItemLamp
            // 
            this.menuItemLamp.Text = "Illumination";
            this.menuItemLamp.Click += new System.EventHandler(this.menuItemLamp_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.MenuItems.Add(this.menuItemAbout);
            this.menuItemHelp.Text = "Help";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Text = "About";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "doc1";
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 248);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(240, 22);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(248, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // ImagerForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 270);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu1;
            this.Name = "ImagerForm";
            this.Text = "CS_Imager2Sample1";
            this.Load += new System.EventHandler(this.ImagerForm_Load);
            this.Closed += new System.EventHandler(this.ImagerForm_Closed);
            this.Resize += new System.EventHandler(this.ImagerForm_Resize);
            this.ResumeLayout(false);

		}
		#endregion
        /// <summary>
        /// This function does the (initial) scaling of the form
        /// by re-setting the related parameters (if required) &
        /// then calling the Scale(...) internally. 
        /// </summary>
        /// 
        public void DoScale()
        {
            if (Screen.PrimaryScreen.Bounds.Width > Screen.PrimaryScreen.Bounds.Height)
            {
                bPortrait = false; // If the display orientation is not portrait (so it's landscape), set the flag to false.
            }

            if (this.WindowState == FormWindowState.Maximized)    // If the form is maximized by default.
            {
                this.bSkipMaxLen = true; // we need to skip the max. length restriction
            }

            if ((Symbol.Win32.PlatformType.IndexOf("WinCE") != -1) || (Symbol.Win32.PlatformType.IndexOf("WindowsCE") != -1) || (Symbol.Win32.PlatformType.IndexOf("Windows CE") != -1)) // Only on Windows CE devices
            {
                this.resWidthReference = this.Width;   // The width of the form at design time (in pixels) is obtained from the platorm.
                this.resHeightReference = this.Height; // The height of the form at design time (in pixels) is obtained from the platform.
            }

            Scale(this); // Initial scaling of the GUI
        }

        /// <summary>
        /// This function scales the given Form & its child controls in order to
        /// make them completely viewable, based on the screen width & height.
        /// </summary>
        private static void Scale(ImagerForm frm)
        {
            int PSWAW = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;    // The width of the working area (in pixels).
            int PSWAH = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;   // The height of the working area (in pixels).

            // The entire screen has been taken in to account below 
            // in order to decide the half (S)VGA settings etc.
            if (!((Screen.PrimaryScreen.Bounds.Width <= (1.5) * (Screen.PrimaryScreen.Bounds.Height))
            && (Screen.PrimaryScreen.Bounds.Height <= (1.5) * (Screen.PrimaryScreen.Bounds.Width))))
            {
                if ((Screen.PrimaryScreen.Bounds.Width) > (Screen.PrimaryScreen.Bounds.Height))
                {
                    PSWAW = (int)((1.33) * PSWAH);  // If the width/height ratio goes beyond 1.5,
                    // the (longer) effective width is made shorter.
                }

            }

            System.Drawing.Graphics graphics = frm.CreateGraphics();

            float dpiX = graphics.DpiX; // Get the horizontal DPI value.

            if (frm.bInitialScale == true) // If an initial scale (from scratch)
            {
                if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1) // If the platform is either Pocket PC or Windows Mobile
                {
                    frm.Width = PSWAW;  // Set the form width. However this setting
                    // would be the ultimate one for Pocket PC (& Windows Mobile)devices.
                    // Just for the sake of consistency, it's explicitely specified here.
                }
                else
                {
                    frm.Width = (int)((frm.Width) * (PSWAW)) / (frm.resWidthReference); // Set the form width for others (Windows CE devices).

                }
            }
            if ((frm.Width <= maxLength * dpiX) || frm.bSkipMaxLen == true) // The calculation of the width & left values for each control
            // without taking the maximum length restriction into consideration.
            {
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {
                    cntrl.Width = ((cntrl.Width) * (frm.Width)) / (frm.resWidthReference);
                    cntrl.Left = ((cntrl.Left) * (frm.Width)) / (frm.resWidthReference);

                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Width = (((cntrl2.Width) * (frm.Width)) / (frm.resWidthReference));
                                cntrl2.Left = (((cntrl2.Left) * (frm.Width)) / (frm.resWidthReference));
                            }
                        }
                    }
                }

            }
            else
            {   // The calculation of the width & left values for each control
                // with the maximum length restriction taken into consideration.
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {
                    cntrl.Width = (int)(((cntrl.Width) * (PSWAW) * (maxLength * dpiX)) / (frm.resWidthReference * (frm.Width)));
                    cntrl.Left = (int)(((cntrl.Left) * (PSWAW) * (maxLength * dpiX)) / (frm.resWidthReference * (frm.Width)));

                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Width = (int)(((cntrl2.Width) * (PSWAW) * (maxLength * dpiX)) / (frm.resWidthReference * (frm.Width)));
                                cntrl2.Left = (int)(((cntrl2.Left) * (PSWAW) * (maxLength * dpiX)) / (frm.resWidthReference * (frm.Width)));
                            }
                        }
                    }
                }

                frm.Width = (int)((frm.Width) * (maxLength * dpiX)) / (frm.Width);

            }

            frm.resWidthReference = frm.Width; // Set the reference width to the new value.


            // A similar calculation is performed below for the height & top values for each control ...

            if (!((Screen.PrimaryScreen.Bounds.Width <= (1.5) * (Screen.PrimaryScreen.Bounds.Height))
            && (Screen.PrimaryScreen.Bounds.Height <= (1.5) * (Screen.PrimaryScreen.Bounds.Width))))
            {
                if ((Screen.PrimaryScreen.Bounds.Height) > (Screen.PrimaryScreen.Bounds.Width))
                {
                    PSWAH = (int)((1.33) * PSWAW);
                }

            }

            float dpiY = graphics.DpiY;

            if (frm.bInitialScale == true)
            {
                if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
                {
                    frm.Height = PSWAH;
                }
                else
                {
                    frm.Height = (int)((frm.Height) * (PSWAH)) / (frm.resHeightReference);

                }
            }

            if ((frm.Height <= maxLength * dpiY) || frm.bSkipMaxLen == true)
            {
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {

                    cntrl.Height = ((cntrl.Height) * (frm.Height)) / (frm.resHeightReference);
                    cntrl.Top = ((cntrl.Top) * (frm.Height)) / (frm.resHeightReference);


                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Height = ((cntrl2.Height) * (frm.Height)) / (frm.resHeightReference);
                                cntrl2.Top = ((cntrl2.Top) * (frm.Height)) / (frm.resHeightReference);
                            }
                        }
                    }

                }

            }
            else
            {
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {

                    cntrl.Height = (int)(((cntrl.Height) * (PSWAH) * (maxLength * dpiY)) / (frm.resHeightReference * (frm.Height)));
                    cntrl.Top = (int)(((cntrl.Top) * (PSWAH) * (maxLength * dpiY)) / (frm.resHeightReference * (frm.Height)));


                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Height = (int)(((cntrl2.Height) * (PSWAH) * (maxLength * dpiY)) / (frm.resHeightReference * (frm.Height)));
                                cntrl2.Top = (int)(((cntrl2.Top) * (PSWAH) * (maxLength * dpiY)) / (frm.resHeightReference * (frm.Height)));
                            }
                        }
                    }

                }

                frm.Height = (int)((frm.Height) * (maxLength * dpiY)) / (frm.Height);

            }

            frm.resHeightReference = frm.Height;

            if (frm.bInitialScale == true)
            {
                frm.bInitialScale = false; // If this was the initial scaling (from scratch), it's now complete.
            }
            if (frm.bSkipMaxLen == true)
            {
                frm.bSkipMaxLen = false; // No need to consider the maximum length restriction now.
            }


        }

        /// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
            ImagerForm imgFrm = new ImagerForm();
            imgFrm.DoScale();
            Application.Run(imgFrm);
		}

		private void ImagerForm_Load(object sender, System.EventArgs e)
		{
            triggerList = new ArrayList();

            InitImager();
		}

        private void InitImager()
        {
            Device MyDevice =
                        SelectDevice.Select(
                        "Imager",
                        Symbol.Imaging2.Devices.AvailableDevices);

            if (MyDevice == null)
            {
                MessageBox.Show(Resources.GetString("NO_DEV_SELECTED"), Resources.GetString("SELECT_DEVICE"));
                this.Close();
                return;
            }

            myImaging2 = new Imaging2(MyDevice);
            myImaging2.Config.Activators.Triggers = new Triggers[] { Triggers.ALLTRIGGERS };

            AdjustPictureBox();

            myImaging2.Config.ImageCapability.FileFormat.Value = FileFormats.TWFF_JPEG;
            myImaging2.Config.AcquisitionCapability.LampOn.Value = this.menuItemLamp.Checked;

            myImaging2.OnCapture += new Imaging2.OnCaptureHandler(myImaging2_OnCapture);
            myImaging2.OnStatus += new Imaging2.OnStatusHandler(myImaging2_OnStatus);

            if (myImaging2.Config.DeviceCapability.Resolution.IsSupported == true)
            {
                Array resolutionAllowedValues = myImaging2.Config.DeviceCapability.Resolution.AllowedValues;
                resolutionMenuItems = new MenuItem[resolutionAllowedValues.Length];

                for (int i = 0; i < resolutionAllowedValues.Length; i++)
                {
                    resolutionMenuItems[i] = new MenuItem();
                    resolutionMenuItems[i].Text = ((Resolutions)resolutionAllowedValues.GetValue(i)).ToString();
                    resolutionMenuItems[i].Click += new EventHandler(resolutionMenuItems_Click);
                    if (resolutionMenuItems[i].Text == ((Resolutions)myImaging2.Config.DeviceCapability.Resolution.DefaultValue).ToString())
                    {
                        resolutionMenuItems[i].Checked = true;
                        lastCheckedresolutionMenuItem = i;
                    }
                    menuItemResolution.MenuItems.Add(resolutionMenuItems[i]);
                }
            }
            else
            {
                resolutionMenuItems = new MenuItem[1];
                resolutionMenuItems[0] = new MenuItem();
                resolutionMenuItems[0].Text = Resources.GetString("NOTSUPPORTED");
                resolutionMenuItems[0].Enabled = false;
                menuItemResolution.MenuItems.Add(resolutionMenuItems[0]);
            }

            StartAcquisition();
        }

        void resolutionMenuItems_Click(object sender, EventArgs e)
        {
            if (((MenuItem)sender).Checked == true)
            {
                return;
            }
            myImaging2.CaptureCancel();
            myImaging2.StopAcquisition();
            statusBar1.Text = Resources.GetString("CHANGING_RES");
            pictureBox1.Image = null;
            Application.DoEvents();
            myImaging2.Config.DeviceCapability.Resolution.Value = (Resolutions)Enum.Parse(typeof(Resolutions), ((MenuItem)sender).Text, true);
            myImaging2.StartAcquisition(this.pictureBox1);
            myImaging2.CaptureImage();
            statusBar1.Text = Resources.GetString("PRESS_TRIG_CAP_IMAGE");

            resolutionMenuItems[lastCheckedresolutionMenuItem].Checked = false;
            ((MenuItem)sender).Checked = true;
            lastCheckedresolutionMenuItem = 0;
            for (int i = 0; i < resolutionMenuItems.Length; i++)
            {
                if (resolutionMenuItems[i].Text == ((MenuItem)sender).Text)
                {
                    lastCheckedresolutionMenuItem = i;
                    break;
                }
            }
        }

        void myImaging2_OnStatus(StatusData statusData)
        {
            if (statusData.EventType == STATUSEVENTTYPES.UNFREEZE)
            {
                myImaging2.CaptureImage();

                this.statusBar1.Text = Resources.GetString("PRESS_TRIG_CAP_IMAGE");
                this.menuItemStart.Enabled = false;
                this.menuItemGet.Enabled = true;
                this.menuItemSave.Enabled = false;
            }

            if (statusData.EventType == STATUSEVENTTYPES.FREEZE)
            {
                this.statusBar1.Text = Resources.GetString("PRESS_TRIG_ACQ");
            }
        }

        private void DeInitImager()
        {
            if (myImaging2 != null)
            {
                myImaging2.StopAcquisition();
                myImaging2.Dispose();
            }
        }

        void myImaging2_OnCapture(ImageData imageData)
        {
            if (imageData.Results == IMGResults.SUCCESS)
            {
                this.imageDataInBytes = imageData.MemoryStream.GetBuffer();

                this.menuItemStart.Enabled = true;
                this.menuItemGet.Enabled = false;
                this.menuItemSave.Enabled = true;
            }
            else
            {
                MessageBox.Show(imageData.Results.ToString(), Resources.GetString("ERROR"));
                myImaging2.CaptureImage();
            }
        }

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void menuItemStart_Click(object sender, System.EventArgs e)
		{
			StartAcquisition();
		}

		private void menuItemGet_Click(object sender, System.EventArgs e)
		{
			GetImage();
		}

        private void menuItemLamp_Click(object sender, System.EventArgs e)
		{
			this.menuItemLamp.Checked = !this.menuItemLamp.Checked;
            myImaging2.Config.AcquisitionCapability.LampOn.Value = this.menuItemLamp.Checked;
		}

		private void menuItemAbout_Click(object sender, System.EventArgs e)
		{
			string sVerInfo = "CS_Imager2Sample1\t - v1.0.0\r\n";
			
			sVerInfo += "Assembly Version\t - v" + 
				myImaging2.Version.AssemblyVersion + "\r\n" +
				"CAPI Version\t - v" +
                myImaging2.Version.CAPIVersion + "\r\n" +
				"MDD Version\t - v" +
                myImaging2.Version.MDDVersion + "\r\n" +
				"PDD Version\t - v" +
                myImaging2.Version.PDDVersion + "\r\n" + 
				"Hardware Version\t - v" +
                myImaging2.Version.HardwareVersion + "\r\n" +
				"JPEG Lib Version\t - v" +
                myImaging2.Version.JPEGLibVersion + "\r\n";
			
			Symbol.StandardForms.About.Run(null, sVerInfo);
		}

		private void ImagerForm_Closed(object sender, System.EventArgs e)
		{
            DeInitImager();
		}

		private void menuItemSave_Click(object sender, System.EventArgs e)
		{
			this.saveFileDialog1.Filter = "jpg files (*.jpg)|*.jpg";
			if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				System.IO.FileStream file = new System.IO.FileStream(this.saveFileDialog1.FileName, 
					System.IO.FileMode.Create,	System.IO.FileAccess.Write);
                file.Write(imageDataInBytes, 0, imageDataInBytes.Length);
                file.Close();
			}
		}

		private void StartAcquisition()
		{
            try
            {
                myImaging2.StartAcquisition(this.pictureBox1);
                myImaging2.CaptureImage();
                this.statusBar1.Text = Resources.GetString("PRESS_TRIG_CAP_IMAGE");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.GetString("START_IMG_FAIL") + ex.Message, Resources.GetString("ERROR"));
                this.Close();
                return;
            }

            this.menuItemStart.Enabled = false;
            this.menuItemGet.Enabled = true;
            this.menuItemSave.Enabled = false;
		}

		private void GetImage()
		{
            //Cancel the current asynchronous capture.
            myImaging2.CaptureCancel();

            //Issue a synchronous direct capture which does not use triggers/keys
			imageData = myImaging2.CaptureImageNow();
            myImaging2.StopViewfinder();

			this.menuItemStart.Enabled = true;
			this.menuItemGet.Enabled = false;
			this.menuItemSave.Enabled = true;

            if (this.pictureBox1.Image != null)
            {
                this.pictureBox1.Image.Dispose();
                this.pictureBox1.Image = null;
            }

            try
            {
                this.pictureBox1.Image = imageData.GetBitmap();
                this.imageDataInBytes = imageData.MemoryStream.GetBuffer();
            }
            catch (NullReferenceException)
            {
                //NullReferenceException is thrown here if the user selects the capture option via the menu while the previous image is still being captured.
                //The scenario is mainly seen with camera with the additional delay involved there.
                MessageBox.Show(Resources.GetString("IMG_CAPTURE_IN_PROGRESS"));
            }

			this.statusBar1.Text = Resources.GetString("START_ACQ");		
		}	

		private void AdjustPictureBox()
		{
			// Calculate the location and size of the picture box.
			int top = this.ClientRectangle.Top + SystemInformation.MenuHeight; //Adjust top for height of Menu Bar
			int left = this.ClientRectangle.Left;
			int width = this.ClientRectangle.Width;
			//Adjust height of Picture Box for height of Menu and Status Bars
			int height = this.ClientRectangle.Height - this.statusBar1.Height - SystemInformation.MenuHeight; 
			float aspectRatio = (float)width / (float)height;
			float x = (float)myImaging2.Config.DeviceCapability.XNativeResolution.Value;
            float y = (float)myImaging2.Config.DeviceCapability.YNativeResolution.Value;
			if (x/y > aspectRatio)
			{	// Match width and adjust height
				int h = (int)(width / x * y);
				top += (height > h)? (height - h)/2 : 0;
				height = h;
			}
			else
			{	// Match height and adjust width
				int w = (int)(height / y * x);
				left += (width > w)? (width - w)/2 : 0;
				width = w;
			}
			this.pictureBox1.Left = left;
			this.pictureBox1.Top = top;
			this.pictureBox1.Width = width;
			this.pictureBox1.Height = height;
		}

        private void ImagerForm_Resize(object sender, EventArgs e)
        {
            if (bInitialScale == true)
            {
                return; // Return if the initial scaling (from scratch)is not complete.
            }

            if (Screen.PrimaryScreen.Bounds.Width > Screen.PrimaryScreen.Bounds.Height) // If landscape orientation
            {
                if (bPortrait != false) // If an orientation change has occured to landscape
                {
                    bPortrait = false; // Set the orientation flag accordingly.
                    bInitialScale = true; // An initial scaling is required due to orientation change.
                    Scale(this); // Scale the GUI.
                }
                else
                {   // No orientation change has occured
                    bSkipMaxLen = true; // Initial scaling is now complete, so skipping the max. length restriction is now possible.
                    Scale(this); // Scale the GUI.
                }
            }
            else
            {
                // Similarly for the portrait orientation...
                if (bPortrait != true)
                {
                    bPortrait = true;
                    bInitialScale = true;
                    Scale(this);
                }
                else
                {
                    bSkipMaxLen = true;
                    Scale(this);
                }
            }

        }

        private void menuItemJPEGQuality_Click(object sender, EventArgs e)
        {
            menuItemJPEGQuality100.Checked = false;
            menuItemJPEGQuality75.Checked = false;
            menuItemJPEGQuality50.Checked = false;
            menuItemJPEGQuality25.Checked = false;

            ((MenuItem)sender).Checked = true;

            switch (((MenuItem)sender).Text)
            {
                case "100%":
                    myImaging2.Config.ImageCapability.JpegQuality.Value = 100;
                    break;
                case "75%":
                    myImaging2.Config.ImageCapability.JpegQuality.Value = 75;
                    break;
                case "50%":
                    myImaging2.Config.ImageCapability.JpegQuality.Value = 50;
                    break;
                case "25%":
                    myImaging2.Config.ImageCapability.JpegQuality.Value = 25;
                    break;
            }
        }
	}
}
