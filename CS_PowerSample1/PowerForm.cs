//--------------------------------------------------------------------
// FILENAME: PowerForm.cs
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
using System.Text;
using System.Threading;

namespace CS_PowerSample1
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnSuspend;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.Button btnBacklightOff;
		private System.Windows.Forms.Button btnBacklightOn;
		private System.Windows.Forms.ProgressBar batteryProgressBar;
		private System.Windows.Forms.Label powerTypeLabel;
		private System.Windows.Forms.Label batteryPercentLabel;
		private System.Windows.Forms.Label powerSourceLabel;
		private System.Windows.Forms.Label batteryMainLabel;
		private System.Windows.Forms.Label batteryBackupLabel;
		private System.Windows.Forms.ProgressBar batteryProgressBar2;
		private System.Windows.Forms.Label batteryPercentLabel2;
        private Button btnExit;

		private PowerManagement MyPowerManager;

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

        private int resHeightReference = 301;  // The (cached) height of the form.
        // INITIALLY HAS TO BE SET TO THE HEIGHT OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private const double maxLength = 5.5;  // The maximum physical width/height of the sample (in inches).
        // The actual value on the device may slightly deviate from this
        // since the calculations based on the (received) DPI & resolution values 
        // would provide only an approximation, so not 100% accurate.

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            btnBacklightOn.Focus();
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
            this.btnSuspend = new System.Windows.Forms.Button();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.btnBacklightOff = new System.Windows.Forms.Button();
            this.btnBacklightOn = new System.Windows.Forms.Button();
            this.batteryProgressBar = new System.Windows.Forms.ProgressBar();
            this.powerTypeLabel = new System.Windows.Forms.Label();
            this.batteryPercentLabel = new System.Windows.Forms.Label();
            this.powerSourceLabel = new System.Windows.Forms.Label();
            this.batteryMainLabel = new System.Windows.Forms.Label();
            this.batteryBackupLabel = new System.Windows.Forms.Label();
            this.batteryPercentLabel2 = new System.Windows.Forms.Label();
            this.batteryProgressBar2 = new System.Windows.Forms.ProgressBar();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSuspend
            // 
            this.btnSuspend.Location = new System.Drawing.Point(56, 24);
            this.btnSuspend.Name = "btnSuspend";
            this.btnSuspend.Size = new System.Drawing.Size(136, 24);
            this.btnSuspend.TabIndex = 2;
            this.btnSuspend.Text = "Suspend Device";
            this.btnSuspend.Click += new System.EventHandler(this.btnSuspend_Click);
            this.btnSuspend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnSuspend_KeyDown);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 252);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(240, 24);
            // 
            // btnBacklightOff
            // 
            this.btnBacklightOff.Location = new System.Drawing.Point(8, 192);
            this.btnBacklightOff.Name = "btnBacklightOff";
            this.btnBacklightOff.Size = new System.Drawing.Size(112, 24);
            this.btnBacklightOff.TabIndex = 3;
            this.btnBacklightOff.Text = "Disable Backlight";
            this.btnBacklightOff.Click += new System.EventHandler(this.btnBacklightOff_Click);
            this.btnBacklightOff.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnBacklightOff_KeyDown);
            // 
            // btnBacklightOn
            // 
            this.btnBacklightOn.Location = new System.Drawing.Point(120, 192);
            this.btnBacklightOn.Name = "btnBacklightOn";
            this.btnBacklightOn.Size = new System.Drawing.Size(112, 24);
            this.btnBacklightOn.TabIndex = 0;
            this.btnBacklightOn.Text = "Enable Backlight";
            this.btnBacklightOn.Click += new System.EventHandler(this.btnBacklightOn_Click);
            this.btnBacklightOn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnBacklightOn_KeyDown);
            // 
            // batteryProgressBar
            // 
            this.batteryProgressBar.Location = new System.Drawing.Point(104, 112);
            this.batteryProgressBar.Name = "batteryProgressBar";
            this.batteryProgressBar.Size = new System.Drawing.Size(88, 20);
            // 
            // powerTypeLabel
            // 
            this.powerTypeLabel.Location = new System.Drawing.Point(156, 72);
            this.powerTypeLabel.Name = "powerTypeLabel";
            this.powerTypeLabel.Size = new System.Drawing.Size(48, 20);
            this.powerTypeLabel.Text = "Battery";
            // 
            // batteryPercentLabel
            // 
            this.batteryPercentLabel.Location = new System.Drawing.Point(200, 112);
            this.batteryPercentLabel.Name = "batteryPercentLabel";
            this.batteryPercentLabel.Size = new System.Drawing.Size(40, 20);
            this.batteryPercentLabel.Text = "0%";
            // 
            // powerSourceLabel
            // 
            this.powerSourceLabel.Location = new System.Drawing.Point(36, 72);
            this.powerSourceLabel.Name = "powerSourceLabel";
            this.powerSourceLabel.Size = new System.Drawing.Size(96, 24);
            this.powerSourceLabel.Text = "Power Source:";
            // 
            // batteryMainLabel
            // 
            this.batteryMainLabel.Location = new System.Drawing.Point(8, 112);
            this.batteryMainLabel.Name = "batteryMainLabel";
            this.batteryMainLabel.Size = new System.Drawing.Size(88, 20);
            this.batteryMainLabel.Text = "Main Battery :";
            // 
            // batteryBackupLabel
            // 
            this.batteryBackupLabel.Location = new System.Drawing.Point(8, 144);
            this.batteryBackupLabel.Name = "batteryBackupLabel";
            this.batteryBackupLabel.Size = new System.Drawing.Size(96, 20);
            this.batteryBackupLabel.Text = "Backup Battery :";
            // 
            // batteryPercentLabel2
            // 
            this.batteryPercentLabel2.Location = new System.Drawing.Point(200, 144);
            this.batteryPercentLabel2.Name = "batteryPercentLabel2";
            this.batteryPercentLabel2.Size = new System.Drawing.Size(40, 20);
            this.batteryPercentLabel2.Text = "0%";
            // 
            // batteryProgressBar2
            // 
            this.batteryProgressBar2.Location = new System.Drawing.Point(104, 144);
            this.batteryProgressBar2.Name = "batteryProgressBar2";
            this.batteryProgressBar2.Size = new System.Drawing.Size(88, 20);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(120, 222);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(111, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnExit_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 276);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.batteryBackupLabel);
            this.Controls.Add(this.batteryPercentLabel2);
            this.Controls.Add(this.batteryProgressBar2);
            this.Controls.Add(this.batteryMainLabel);
            this.Controls.Add(this.powerSourceLabel);
            this.Controls.Add(this.batteryPercentLabel);
            this.Controls.Add(this.powerTypeLabel);
            this.Controls.Add(this.batteryProgressBar);
            this.Controls.Add(this.btnBacklightOn);
            this.Controls.Add(this.btnBacklightOff);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.btnSuspend);
            this.Name = "Form1";
            this.Text = "CS_PowerSample1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
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
        private static void Scale(Form1 frm)
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
            Form1 pf = new Form1();
            pf.DoScale();
            Application.Run(pf);
		}

		/// <summary>
		/// Occurs before the form is displayed for the first time.
		/// </summary>
		private void Form1_Load(object sender, System.EventArgs e)
		{
			// Create a new instance of the PowerManagement class
			MyPowerManager = new PowerManagement();

			// Hook up the Power notify event. This event would not be activated 
			// until EnableNotifications is called. 
			MyPowerManager.PowerNotify += new EventHandler(OnPowerNotify);

			// Enable power notifications. This will cause a thread to start
			// that will fire the PowerNotify event when any power notification 
			// is received.
			MyPowerManager.EnableNotifications();

			// Get the current power state. 
			StringBuilder systemStateName = new StringBuilder(20);
			PowerManagement.SystemPowerStates systemState;
			int nError = MyPowerManager.GetSystemPowerState(systemStateName, out systemState);

			// Display the current power state on the status bar
			if (nError != 0)
				statusBar1.Text = "GetSystemPowerState Failed. Error: " + nError.ToString();
			else
				statusBar1.Text = "System Power State: " + systemStateName;
		}

		/// <summary>
		/// Occurs before when form is closing.
		/// </summary>
		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Disable power notifications. Causes the monitor thread to shut down
			MyPowerManager.DisableNotifications();
		}

		/// <summary>
		/// Click from the Suspend button. Suspends the device.
		/// </summary>
		private void btnSuspend_Click(object sender, System.EventArgs e)
		{
			// Suspend the device
			int nError = MyPowerManager.SetSystemPowerState(PowerManagement.SystemPowerStates.Suspend);

			if (nError != 0)
				statusBar1.Text = "Suspend Failed. Error: " + nError.ToString();
		}

		/// <summary>
		/// Click from the Backlight Off button. Causes the backlight 
		/// to turn off.
		/// </summary>
		private void btnBacklightOff_Click(object sender, System.EventArgs e)
		{
			// Notify the backlight device driver to turn off power.
			int nError = MyPowerManager.DevicePowerNotify("BKL1:", PowerManagement.DevicePowerStates.Off);

			if (nError != 0)
				statusBar1.Text = "Disabling Backlight Failed. Error: " + nError.ToString() ;		
		}

		/// <summary>
		/// Click from the Backlight On button. Causes the backlight 
		/// to turn on.
		/// </summary>
		private void btnBacklightOn_Click(object sender, System.EventArgs e)
		{
			// Notify the backlight device driver to turn on power.
			int nError = MyPowerManager.DevicePowerNotify("BKL1:", PowerManagement.DevicePowerStates.FullOn);

			if (nError != 0)
				statusBar1.Text = "Enabling Backlight Failed. Error: " + nError.ToString() ;			
		}

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            //this.Dispose();
            this.Close();

        }

        private void btnSuspend_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                btnSuspend_Click(this, e);
        }

        private void btnBacklightOff_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                btnBacklightOff_Click(this, e);
        }

        private void btnBacklightOn_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                btnBacklightOn_Click(this, e);
        }

        private void btnExit_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                btnExit_Click(this, e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            btnBacklightOn.Focus();
        }
		/// <summary>
		/// Power event delegate. Invokes the UpdateGUI delegate.
		/// </summary>
		private void OnPowerNotify(object sender, EventArgs e)
		{
			// We are not in the UI thread so we need to Invoke to get there
			// Unfortunately we cannot pass arguements across the thread boundary
			// as this is a limitation of the CF
			this.Invoke(new EventHandler(UpdateGUI));
		}

		/// <summary>
		/// UpdateGUI delegate. Processes and display the power information
		/// provided by the power management on the system.
		/// </summary>
		private void UpdateGUI(object sender, EventArgs e)
		{
			// Get the information
			PowerManagement.PowerInfo powerInfo = MyPowerManager.GetNextPowerInfo();

			// Determine if we are on battery or AC
			if ( powerInfo.Message == PowerManagement.MessageTypes.Status )
			{
				if ( powerInfo.ACLineStatus == PowerManagement.ACLineStatus.OnLine )
					powerTypeLabel.Text = "AC";
				else
				{
					powerTypeLabel.Text = "Battery";
                }
				// Update Main Battery information
				batteryProgressBar.Value = powerInfo.BatteryLifePercent;
				batteryPercentLabel.Text = powerInfo.BatteryLifePercent.ToString() + "%";
                
                // Update Backup Battery information
				batteryProgressBar2.Value = powerInfo.BackupBatteryLifePercent;
				batteryPercentLabel2.Text = powerInfo.BackupBatteryLifePercent.ToString() + "%";
			}
			else if ( powerInfo.Flags == PowerManagement.SystemPowerStates.Suspend )
			{
				// The notification of the loss of power does not actually occur 
				// until immediately after it is back.
				statusBar1.Text = "Device resumed from a suspend. ";
			}
		}

        private void Form1_Resize(object sender, EventArgs e)
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

	}
}
