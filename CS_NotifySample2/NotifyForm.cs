//--------------------------------------------------------------------
// FILENAME: NotifyForm.cs
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

namespace CS_NotifySample2
{
	/// <summary>
	/// Summary description for NotifyForm.
	/// </summary>
	public class NotifyForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Button buttonAbout;
		private System.Windows.Forms.Button buttonExit;

		// control arrays to be created at run time
		private System.Windows.Forms.TabPage[] tabPages;
		private System.Windows.Forms.Label[] labelA;
		private System.Windows.Forms.Label[] labelB;
		private System.Windows.Forms.Label[] labelC;
		private System.Windows.Forms.Label[] labelState;
		private System.Windows.Forms.RadioButton[] radioButtonCycle;
		private System.Windows.Forms.RadioButton[] radioButtonOff;
		private System.Windows.Forms.RadioButton[] radioButtonOn;
		private System.Windows.Forms.TextBox[] textBoxA;
		private System.Windows.Forms.TextBox[] textBoxB;
		private System.Windows.Forms.TextBox[] textBoxC;

		private Symbol.Notification.NotifyObject[] notifyObjects;

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

        private int resHeightReference = 295;  // The (cached) height of the form.
        // INITIALLY HAS TO BE SET TO THE HEIGHT OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private const double maxLength = 5.5;  // The maximum physical width/height of the sample (in inches).
        // The actual value on the device may slightly deviate from this
        // since the calculations based on the (received) DPI & resolution values 
        // would provide only an approximation, so not 100% accurate.

		public NotifyForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitializeNotifyObjects();

			InitializeTabPages();

            radioButtonOff[0].Focus();

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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 226);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Location = new System.Drawing.Point(16, 232);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(64, 24);
            this.buttonAbout.TabIndex = 0;
            this.buttonAbout.Text = "About";
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            this.buttonAbout.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonAbout_KeyDown);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(160, 232);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(64, 24);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "Exit";
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            this.buttonExit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonExit_KeyDown);
            // 
            // NotifyForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 270);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.tabControl1);
            this.Name = "NotifyForm";
            this.Text = "CS_NotifySample2";
            this.Load += new System.EventHandler(this.NotifyForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NotifyForm_KeyUp);
            this.Resize += new System.EventHandler(this.NotifyForm_Resize);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Create notifyObjects array from availabe notification devices
		/// </summary>
		private void InitializeNotifyObjects()
		{
			int numObjs = Symbol.Notification.Device.AvailableDevices.Length;
			notifyObjects = new Symbol.Notification.NotifyObject[numObjs];
			for (int i=0; i<numObjs; i++)
			{
				switch (Symbol.Notification.Device.AvailableDevices[i].ObjectType)
				{
					case Symbol.Notification.NotifyType.BEEPER:

						notifyObjects[i] = new Symbol.Notification.Beeper(
							Symbol.Notification.Device.AvailableDevices[i]);
						
						break;

					case Symbol.Notification.NotifyType.LED:

						notifyObjects[i] = new Symbol.Notification.LED(
							Symbol.Notification.Device.AvailableDevices[i]);
						
						break;

					case Symbol.Notification.NotifyType.VIBRATOR:

						notifyObjects[i] = new Symbol.Notification.Vibrator(
							Symbol.Notification.Device.AvailableDevices[i]);
						
						break;

					default:

						throw (new Exception("Unknown notify device"));
				}
				// set up event handler to hande StateChange events
				notifyObjects[i].StateChange +=new EventHandler(notifyObject_StateChange);
			}
		}

		/// <summary>
		/// Create a tab page (and the controls it contains) for each notify object
		/// </summary>
		private void InitializeTabPages()
		{
			int numObjs = notifyObjects.Length;
			if (numObjs <= 0)
				return;

			this.tabPages = new TabPage[numObjs];
			this.labelA = new Label[numObjs];
			this.labelB = new Label[numObjs];
			this.labelC = new Label[numObjs];
			this.labelState = new Label[numObjs];
			this.radioButtonCycle = new RadioButton[numObjs];
			this.radioButtonOff = new RadioButton[numObjs];
			this.radioButtonOn = new RadioButton[numObjs];
			this.textBoxA = new TextBox[numObjs];
			this.textBoxB = new TextBox[numObjs];
			this.textBoxC = new TextBox[numObjs];

			for (int i=0; i<numObjs; i++)
			{
				tabPages[i] = new TabPage();
				tabControl1.Controls.Add(tabPages[i]);

				labelState[i] = new Label();
				labelState[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
				labelState[i].Location = new System.Drawing.Point(12, 8);
				labelState[i].Size = new System.Drawing.Size(56, 22);
				labelState[i].Text = "State:";

				radioButtonOn[i] = new RadioButton();
				radioButtonOn[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
				radioButtonOn[i].Location = new System.Drawing.Point(20, 32);
				radioButtonOn[i].Size = new System.Drawing.Size(56, 24);
				radioButtonOn[i].Text = "On";
				radioButtonOn[i].CheckedChanged += new System.EventHandler(radioButtonOn_CheckedChanged);

				radioButtonOff[i] = new RadioButton();
				radioButtonOff[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
				radioButtonOff[i].Location = new System.Drawing.Point(88, 32);
				radioButtonOff[i].Size = new System.Drawing.Size(56, 24);
				radioButtonOff[i].Text = "Off";
				radioButtonOff[i].CheckedChanged += new System.EventHandler(radioButtonOff_CheckedChanged);

				radioButtonCycle[i] = new RadioButton();
				radioButtonCycle[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
				radioButtonCycle[i].Location = new System.Drawing.Point(156, 32);
				radioButtonCycle[i].Size = new System.Drawing.Size(56, 24);
				radioButtonCycle[i].Text = "Cycle";
				radioButtonCycle[i].CheckedChanged += new System.EventHandler(radioButtonCycle_CheckedChanged);
				
				tabPages[i].Controls.Add(labelState[i]);
				tabPages[i].Controls.Add(radioButtonOn[i]);
				tabPages[i].Controls.Add(radioButtonOff[i]);
				tabPages[i].Controls.Add(radioButtonCycle[i]);
				tabPages[i].Location = new System.Drawing.Point(4, 4);
				tabPages[i].Size = new System.Drawing.Size(232, 182);
				tabPages[i].Text = notifyObjects[i].Name;

				switch (notifyObjects[i].Type)
				{
					case Symbol.Notification.NotifyType.BEEPER:
						
						labelA[i] = new Label();
						labelA[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
						labelA[i].Location = new System.Drawing.Point(12, 75);
						labelA[i].Size = new System.Drawing.Size(100, 22);
						labelA[i].Text = "Duration (ms)";

						labelB[i] = new Label();
						labelB[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
						labelB[i].Location = new System.Drawing.Point(12, 107);
						labelB[i].Size = new System.Drawing.Size(100, 22);
						labelB[i].Text = "Frequency (hz)";

						labelC[i] = new Label();
						labelC[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
						labelC[i].Location = new System.Drawing.Point(12, 139);
						labelC[i].Size = new System.Drawing.Size(92, 22);
						labelC[i].Text = "Volume";

						textBoxA[i] = new TextBox();
						textBoxA[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
						textBoxA[i].Location = new System.Drawing.Point(128, 72);
						textBoxA[i].Size = new System.Drawing.Size(88, 22);
						textBoxA[i].Text = ((Symbol.Notification.Beeper)notifyObjects[i]).Duration.ToString();

						textBoxB[i] = new TextBox();
						textBoxB[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
						textBoxB[i].Location = new System.Drawing.Point(128, 104);
						textBoxB[i].Size = new System.Drawing.Size(88, 22);
						textBoxB[i].Text = ((Symbol.Notification.Beeper)notifyObjects[i]).Frequency.ToString();

						textBoxC[i] = new TextBox();
						textBoxC[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
						textBoxC[i].Location = new System.Drawing.Point(128, 136);
						textBoxC[i].Size = new System.Drawing.Size(88, 22);
						textBoxC[i].Text = ((Symbol.Notification.Beeper)notifyObjects[i]).Volume.ToString();

						tabPages[i].Controls.Add(labelA[i]);
						tabPages[i].Controls.Add(labelB[i]);
						tabPages[i].Controls.Add(labelC[i]);
						tabPages[i].Controls.Add(textBoxA[i]);
						tabPages[i].Controls.Add(textBoxB[i]);
						tabPages[i].Controls.Add(textBoxC[i]);

						break;

					case Symbol.Notification.NotifyType.LED:

						labelA[i] = new Label();
						labelA[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
						labelA[i].Location = new System.Drawing.Point(12, 75);
						labelA[i].Size = new System.Drawing.Size(100, 22);
						labelA[i].Text = "On Duration (ms)";

						labelB[i] = new Label();
						labelB[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
						labelB[i].Location = new System.Drawing.Point(12, 107);
						labelB[i].Size = new System.Drawing.Size(100, 22);
						labelB[i].Text = "Off Duration (ms)";

						labelC[i] = new Label();
						labelC[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
						labelC[i].Location = new System.Drawing.Point(12, 139);
						labelC[i].Size = new System.Drawing.Size(92, 22);
						labelC[i].Text = "Cycle Count";

						textBoxA[i] = new TextBox();
						textBoxA[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
						textBoxA[i].Location = new System.Drawing.Point(128, 72);
						textBoxA[i].Size = new System.Drawing.Size(88, 22);
						textBoxA[i].Text = ((Symbol.Notification.LED)notifyObjects[i]).OnDuration.ToString();

						textBoxB[i] = new TextBox();
						textBoxB[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
						textBoxB[i].Location = new System.Drawing.Point(128, 104);
						textBoxB[i].Size = new System.Drawing.Size(88, 22);
						textBoxB[i].Text = ((Symbol.Notification.LED)notifyObjects[i]).OffDuration.ToString();
						

						textBoxC[i] = new TextBox();
						textBoxC[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
						textBoxC[i].Location = new System.Drawing.Point(128, 136);
						textBoxC[i].Size = new System.Drawing.Size(88, 22);
						textBoxC[i].Text = ((Symbol.Notification.LED)notifyObjects[i]).CycleCount.ToString();

						tabPages[i].Controls.Add(labelA[i]);
						tabPages[i].Controls.Add(labelB[i]);
						tabPages[i].Controls.Add(labelC[i]);
						tabPages[i].Controls.Add(textBoxA[i]);
						tabPages[i].Controls.Add(textBoxB[i]);
						tabPages[i].Controls.Add(textBoxC[i]);
						
						break;

					case Symbol.Notification.NotifyType.VIBRATOR:

						labelA[i] = new Label();
						labelA[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
						labelA[i].Location = new System.Drawing.Point(12, 75);
						labelA[i].Size = new System.Drawing.Size(100, 22);
						labelA[i].Text = "Duration (ms)";

						textBoxA[i] = new TextBox();
						textBoxA[i].Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
						textBoxA[i].Location = new System.Drawing.Point(128, 72);
						textBoxA[i].Size = new System.Drawing.Size(88, 22);
						textBoxA[i].Text = ((Symbol.Notification.Vibrator)notifyObjects[i]).Duration.ToString();
						
						tabPages[i].Controls.Add(labelA[i]);
						tabPages[i].Controls.Add(textBoxA[i]);
						
						break;
				}
			}

			UpdateStateButtons(0);
		}

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
        private static void Scale(NotifyForm frm)
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
            NotifyForm nf = new NotifyForm();
            nf.DoScale();
            Application.Run(nf);

		}

		/// <summary>
		/// Show about dialog.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAbout_Click(object sender, System.EventArgs e)
		{
			string sVerInfo = "CS_NotifySample2 \t - v1.1.1.2\r\n";
			if (notifyObjects != null && notifyObjects.Length >0)
			{
				sVerInfo += "Assembly Version \t - v" + 
							notifyObjects[0].Version.Assembly + "\r\n" +
							"CAPI Version     \t - v" + 
							notifyObjects[0].Version.CAPI + "\r\n";
			}

			Symbol.StandardForms.About.Run(null, sVerInfo);
            this.buttonAbout.Focus();
		}

		/// <summary>
		/// Dispose all notify objects when Exit button is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExit_Click(object sender, System.EventArgs e)
		{
            foreach (Symbol.Notification.NotifyObject obj in notifyObjects)
            {
                obj.State = Symbol.Notification.NotifyState.OFF;
                obj.Dispose();
            }

			this.Close();
		}

        private void buttonAbout_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                buttonAbout_Click(this, e);
        }

        private void buttonExit_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                buttonExit_Click(this, e);
        }

        private void NotifyForm_KeyUp(object sender, KeyEventArgs e)
        {
            this.buttonAbout.Focus();
        }

		/// <summary>
		/// Refresh the state of the selected notify object when a new tab page is selected.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateStateButtons(tabControl1.SelectedIndex);
		}

		/// <summary>
		/// Set the state radio buttons to indicate the right state of the notify object.
		/// </summary>
		/// <param name="index"></param>
		private void UpdateStateButtons(int index)
		{
			switch (notifyObjects[index].State)
			{
				case Symbol.Notification.NotifyState.ON:
					this.radioButtonOn[index].Checked = true;
					this.radioButtonOff[index].Checked = false;
					this.radioButtonCycle[index].Checked = false;
					break;

				case Symbol.Notification.NotifyState.OFF:
					this.radioButtonOn[index].Checked = false;
					this.radioButtonOff[index].Checked = true;
					this.radioButtonCycle[index].Checked = false;
					break;

				case Symbol.Notification.NotifyState.CYCLE:
					this.radioButtonOn[index].Checked = false;
					this.radioButtonOff[index].Checked = false;
					this.radioButtonCycle[index].Checked = true;
					break;

				default:
					throw (new Exception("Unknown state of notify object!"));

			}
		}

		/// <summary>
		/// Handle the StateChange event of a notify object.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void notifyObject_StateChange(object sender, EventArgs e)
		{
			// Update the UI only if the sender object is currently visible
			if (notifyObjects[tabControl1.SelectedIndex] == sender)
				UpdateStateButtons(tabControl1.SelectedIndex);
		}

		/// <summary>
		/// Update notify object properties according to the UI values.
		/// </summary>
		/// <param name="index"></param>
		private void UpdateNotifyParameters(int index)
		{
			switch (notifyObjects[index].Type)
			{
				case Symbol.Notification.NotifyType.BEEPER:
					Symbol.Notification.Beeper beeper = 
						(Symbol.Notification.Beeper)notifyObjects[index];
					try
					{
						beeper.Duration = Int32.Parse(textBoxA[index].Text);
						beeper.Frequency = Int32.Parse(textBoxB[index].Text);
						beeper.Volume = Int32.Parse(textBoxC[index].Text);
					}
					catch
					{
						MessageBox.Show("Invalid parameters! Restored to previous value.", this.Text);
						textBoxA[index].Text = beeper.Duration.ToString();
						textBoxB[index].Text = beeper.Frequency.ToString();
						textBoxC[index].Text = beeper.Volume.ToString();
					}
					break;

				case Symbol.Notification.NotifyType.LED:
					Symbol.Notification.LED led = 
						(Symbol.Notification.LED)notifyObjects[index];
					try
					{
						led.OnDuration = Int32.Parse(textBoxA[index].Text);
						led.OffDuration = Int32.Parse(textBoxB[index].Text);
						led.CycleCount = Int32.Parse(textBoxC[index].Text);	
					}
					catch
					{
						MessageBox.Show("Invalid parameters! Restored to previous value.", this.Text);
						textBoxA[index].Text = led.OnDuration.ToString();
						textBoxB[index].Text = led.OffDuration.ToString();
						textBoxC[index].Text = led.CycleCount.ToString();
					}
					break;

				case Symbol.Notification.NotifyType.VIBRATOR:
					Symbol.Notification.Vibrator vibrator =
						(Symbol.Notification.Vibrator)notifyObjects[index];
					try
					{
						vibrator.Duration = Int32.Parse(textBoxA[index].Text);
					}
					catch
					{
						MessageBox.Show("Invalid parameters! Restored to previous value.", this.Text);
						textBoxA[index].Text = vibrator.Duration.ToString();
					}
					break;

				default:
					throw (new Exception("Unknow notify object type!"));
			}
		}

		/// <summary>
		/// Set notify object state to ON when radioButtonOn is checked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonOn_CheckedChanged(object sender, System.EventArgs e)
		{
            try
            {
                int index = tabControl1.SelectedIndex;
                if (sender != radioButtonOn[index])
                    return;
                if (radioButtonOn[index].Checked == true
                    && notifyObjects[index].State != Symbol.Notification.NotifyState.ON)
                {
                    UpdateNotifyParameters(index);
                    notifyObjects[index].State = Symbol.Notification.NotifyState.ON;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
		}

		/// <summary>
		/// Set notify object state to CYCLE when radioButtonCycle is checked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonCycle_CheckedChanged(object sender, System.EventArgs e)
		{
            try
            {
                int index = tabControl1.SelectedIndex;
                if (sender != radioButtonCycle[index])
                    return;
                if (radioButtonCycle[index].Checked == true
                    && notifyObjects[index].State != Symbol.Notification.NotifyState.CYCLE)
                {
                    UpdateNotifyParameters(index);
                    notifyObjects[index].State = Symbol.Notification.NotifyState.CYCLE;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
		}

		/// <summary>
		/// Set notify object state to OFF when radioButtonOff is checked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonOff_CheckedChanged(object sender, System.EventArgs e)
		{
            try
            {
                int index = tabControl1.SelectedIndex;
                if (sender != radioButtonOff[index])
                    return;
                if (radioButtonOff[index].Checked == true
                    && notifyObjects[index].State != Symbol.Notification.NotifyState.OFF)
                {
                    UpdateNotifyParameters(index);
                    notifyObjects[index].State = Symbol.Notification.NotifyState.OFF;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
		}

		private void NotifyForm_Load(object sender, System.EventArgs e)
		{
			// Add MainMenu if Pocket PC
			if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
			{
				this.Menu = new MainMenu();
			}
		}

        private void NotifyForm_Resize(object sender, EventArgs e)
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
