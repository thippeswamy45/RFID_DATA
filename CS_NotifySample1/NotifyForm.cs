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

namespace CS_NotifySample1
{
	/// <summary>
	/// CS_NotifySample1 Form class.
	/// </summary>
	public class NotifyForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label labelState;
		private System.Windows.Forms.RadioButton radioButtonOn;
		private System.Windows.Forms.RadioButton radioButtonOff;
		private System.Windows.Forms.RadioButton radioButtonCycle;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Button buttonSwitch;
		private System.Windows.Forms.Button buttonExit;

		private Symbol.Notification.Device device;
		private Symbol.Notification.Beeper	beeper = null;
		private Symbol.Notification.LED		led = null;
		private Symbol.Notification.Vibrator vibrator = null;
		private System.Windows.Forms.Button buttonAbout;
		private Symbol.Notification.NotifyObject objNotify = null;

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

		/// <summary>
		/// NotifyForm constructor.
		/// </summary>
		public NotifyForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this.radioButtonOff.Focus();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if (beeper != null)
				beeper.Dispose();
			if (led != null)
				led.Dispose();
			if (vibrator != null)
				vibrator.Dispose();

			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.labelState = new System.Windows.Forms.Label();
            this.radioButtonOn = new System.Windows.Forms.RadioButton();
            this.radioButtonOff = new System.Windows.Forms.RadioButton();
            this.radioButtonCycle = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.buttonSwitch = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelState
            // 
            this.labelState.Location = new System.Drawing.Point(16, 10);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(56, 32);
            this.labelState.Text = "State:";
            // 
            // radioButtonOn
            // 
            this.radioButtonOn.Location = new System.Drawing.Point(24, 45);
            this.radioButtonOn.Name = "radioButtonOn";
            this.radioButtonOn.Size = new System.Drawing.Size(56, 24);
            this.radioButtonOn.TabIndex = 0;
            this.radioButtonOn.Text = "On";
            this.radioButtonOn.CheckedChanged += new System.EventHandler(this.radioButtonOn_CheckedChanged);
            // 
            // radioButtonOff
            // 
            this.radioButtonOff.Checked = true;
            this.radioButtonOff.Location = new System.Drawing.Point(92, 45);
            this.radioButtonOff.Name = "radioButtonOff";
            this.radioButtonOff.Size = new System.Drawing.Size(56, 24);
            this.radioButtonOff.TabIndex = 1;
            this.radioButtonOff.Text = "Off";
            this.radioButtonOff.CheckedChanged += new System.EventHandler(this.radioButtonOff_CheckedChanged);
            // 
            // radioButtonCycle
            // 
            this.radioButtonCycle.Location = new System.Drawing.Point(160, 45);
            this.radioButtonCycle.Name = "radioButtonCycle";
            this.radioButtonCycle.Size = new System.Drawing.Size(64, 24);
            this.radioButtonCycle.TabIndex = 2;
            this.radioButtonCycle.Text = "Cycle";
            this.radioButtonCycle.CheckedChanged += new System.EventHandler(this.radioButtonCycle_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 22);
            this.label1.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(144, 85);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(80, 23);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "textBox1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 22);
            this.label2.Text = "label2";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(144, 117);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(80, 23);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "textBox2";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 22);
            this.label3.Text = "label3";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(144, 149);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(80, 23);
            this.textBox3.TabIndex = 5;
            this.textBox3.Text = "textBox3";
            // 
            // buttonSwitch
            // 
            this.buttonSwitch.Location = new System.Drawing.Point(40, 184);
            this.buttonSwitch.Name = "buttonSwitch";
            this.buttonSwitch.Size = new System.Drawing.Size(160, 24);
            this.buttonSwitch.TabIndex = 6;
            this.buttonSwitch.Text = "Change Notify Object";
            this.buttonSwitch.Click += new System.EventHandler(this.buttonSwitch_Click);
            this.buttonSwitch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonSwitch_KeyDown);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(136, 224);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(64, 24);
            this.buttonExit.TabIndex = 8;
            this.buttonExit.Text = "Exit";
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            this.buttonExit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonExit_KeyDown);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Location = new System.Drawing.Point(40, 224);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(64, 24);
            this.buttonAbout.TabIndex = 7;
            this.buttonAbout.Text = "About";
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            this.buttonAbout.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonAbout_KeyDown);
            // 
            // NotifyForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 270);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonSwitch);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButtonCycle);
            this.Controls.Add(this.radioButtonOff);
            this.Controls.Add(this.radioButtonOn);
            this.Controls.Add(this.labelState);
            this.Name = "NotifyForm";
            this.Text = "CS_NotifySample1";
            this.Load += new System.EventHandler(this.NotifyForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NotifyForm_KeyUp);
            this.Resize += new System.EventHandler(this.NotifyForm_Resize);
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
		/// Occurs before the form is displayed for the first time.
		/// </summary>
		private void NotifyForm_Load(object sender, System.EventArgs e)
		{
			SelectNotifyObject();
			if (device == null)
			{
				this.Close();

				return;
			}

			// Add MainMenu if Pocket PC
			if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
			{
				this.Menu = new MainMenu();
			}

		}

		/// <summary>
		/// Display SelectDevice dialog to select a NotifyObject and initialize it.
		/// </summary>
		private void SelectNotifyObject()
		{
			// Show the SelectDevice dialog.
			device = (Symbol.Notification.Device)
				Symbol.StandardForms.SelectDevice.Select(
				"Notify Object", 
				Symbol.Notification.Device.AvailableDevices);

            if (device == null)
            {
                return;
            }

            // Dispose the old notify object. 
            if (objNotify != null)
                objNotify.Dispose();

			// Create a NotifyObject for the device selected
			// and update UI controls accroding to its properties.
			switch (device.ObjectType)
			{
				case Symbol.Notification.NotifyType.BEEPER:

					beeper = new Symbol.Notification.Beeper(device);
					objNotify = beeper;
					label1.Text = "Duration (ms)";
					textBox1.Text = beeper.Duration.ToString();
					label2.Text = "Frequency (hz)";
					textBox2.Text = beeper.Frequency.ToString();
					label3.Text = "Volume";
					textBox3.Text = beeper.Volume.ToString();
                    Text = device.DeviceName;
                    //These have to be restored to true because selecting vibrator makes them false 
                    label2.Visible = true;
                    textBox2.Visible = true;
                    label3.Visible = true;
                    textBox3.Visible = true;
					break;

				case Symbol.Notification.NotifyType.LED:

					led = new Symbol.Notification.LED(device);
					objNotify = led;
					label1.Text = "On Duration (ms)";
					textBox1.Text = led.OnDuration.ToString();
					label2.Text = "Off Duration (ms)";
					textBox2.Text = led.OffDuration.ToString();
					label3.Text = "Cycle Count";
					textBox3.Text = led.CycleCount.ToString();
                    Text = device.DeviceName;
                    //These have to be restored to true because selecting vibrator makes them false 
                    label2.Visible = true;
                    textBox2.Visible = true;
                    label3.Visible = true;
                    textBox3.Visible = true;
					break;

				case Symbol.Notification.NotifyType.VIBRATOR:

					vibrator = new Symbol.Notification.Vibrator(device);
					objNotify = vibrator;
					label1.Text = "Duration (ms)";
					textBox1.Text = vibrator.Duration.ToString();
					label2.Visible = false;
					textBox2.Visible = false;
					label3.Visible = false;
					textBox3.Visible = false;
					Text = device.DeviceName;
					break;

				default:

					objNotify = null;
					throw (new Exception("Unknown notify device"));

			}
			
			UpdateStateButtons();

            objNotify.StateChange +=new EventHandler(objNotify_StateChange);

			this.Refresh();
		}

		/// <summary>
		/// Update state radio buttons based on State of the current NotifyObject. 
		/// </summary>
		private void UpdateStateButtons()
		{
			switch (objNotify.State)
			{
				case Symbol.Notification.NotifyState.ON:
					this.radioButtonOn.Checked = true;
					this.radioButtonOff.Checked = false;
					this.radioButtonCycle.Checked = false;
					break;

				case Symbol.Notification.NotifyState.OFF:
					this.radioButtonOn.Checked = false;
					this.radioButtonOff.Checked = true;
					this.radioButtonCycle.Checked = false;
					break;

				case Symbol.Notification.NotifyState.CYCLE:
					this.radioButtonOn.Checked = false;
					this.radioButtonOff.Checked = false;
					this.radioButtonCycle.Checked = true;
					break;

				default:
					throw (new Exception("Unknown state of notify object!"));
		
			}
		}

		/// <summary>
		/// Handle the StateChange event of a NotifyObject.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void objNotify_StateChange(object sender, EventArgs e)
		{
			UpdateStateButtons();
		}

		/// <summary>
		/// Called when Exit button is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExit_Click(object sender, System.EventArgs e)
		{
            if (objNotify != null)
            {
                objNotify.State = Symbol.Notification.NotifyState.OFF;
                objNotify.Dispose();
            }

			this.Close();
		}

		/// <summary>
		/// Update parameters of the NotifyObject according to values from UI controls.
		/// </summary>
		private void UpdateNotifyParameters()
		{
			switch (objNotify.Type)
			{
				case Symbol.Notification.NotifyType.BEEPER:
					try
					{
						beeper.Duration = Int32.Parse(textBox1.Text);
						beeper.Frequency = Int32.Parse(textBox2.Text);
						beeper.Volume = Int32.Parse(textBox3.Text);
					}
					catch
					{
						MessageBox.Show("Invalid parameters! Restored to previous value.", this.Text);
						textBox1.Text = beeper.Duration.ToString();
						textBox2.Text = beeper.Frequency.ToString();
						textBox3.Text = beeper.Volume.ToString();
					}
					break;

				case Symbol.Notification.NotifyType.LED:
					try
					{
						led.OnDuration = Int32.Parse(textBox1.Text);
						led.OffDuration = Int32.Parse(textBox2.Text);
						led.CycleCount = Int32.Parse(textBox3.Text);
					}
					catch
					{
						MessageBox.Show("Invalid parameters! Restored to previous value.", this.Text);
						textBox1.Text = led.OnDuration.ToString();
						textBox2.Text = led.OffDuration.ToString();
						textBox3.Text = led.CycleCount.ToString();
					}
					break;

				case Symbol.Notification.NotifyType.VIBRATOR:
					try
					{
						vibrator.Duration = Int32.Parse(textBox1.Text);
					}
					catch
					{
						MessageBox.Show("Invalid parameters! Restored to previous value.", this.Text);
						textBox1.Text = vibrator.Duration.ToString();
					}
					break;
			}
			
		}

		/// <summary>
		/// Set the NotifyObject to state ON when radioButtonOn is checked. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonOn_CheckedChanged(object sender, System.EventArgs e)
		{
            try
            {
                if (radioButtonOn.Checked == true
                        && objNotify.State != Symbol.Notification.NotifyState.ON)
                {
                    UpdateNotifyParameters();
                    objNotify.State = Symbol.Notification.NotifyState.ON;
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
		}

		/// <summary>
		/// Set the NotifyObject to state CYCLE when radioButtonCycle is checked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonCycle_CheckedChanged(object sender, System.EventArgs e)
		{
            try
            {
                if (radioButtonCycle.Checked == true
                        && objNotify.State != Symbol.Notification.NotifyState.CYCLE)
                {
                    UpdateNotifyParameters();
                    objNotify.State = Symbol.Notification.NotifyState.CYCLE;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
		}

		/// <summary>
		/// Set the NotifyObject to state OFF when radioButtonOff is checked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonOff_CheckedChanged(object sender, System.EventArgs e)
		{
            try
            {
                if (radioButtonOff.Checked == true
                    && objNotify.State != Symbol.Notification.NotifyState.OFF)
                {
                    UpdateNotifyParameters();
                    objNotify.State = Symbol.Notification.NotifyState.OFF;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
		}

		/// <summary>
		/// Select a new NotifyObject when buttonSwitch is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSwitch_Click(object sender, System.EventArgs e)
		{
			SelectNotifyObject();
		}

		/// <summary>
		/// Display about dialog when buttonAbout is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAbout_Click(object sender, System.EventArgs e)
		{
			string sVerInfo = "CS_NotifySample1 \t - v1.1.1.2\r\n" +
							  "Assembly Version \t - v" + 
							  objNotify.Version.Assembly + "\r\n" +
							  "CAPI Version     \t - v" + 
							  objNotify.Version.CAPI + "\r\n";

			Symbol.StandardForms.About.Run(null, sVerInfo);
            this.buttonAbout.Focus();
		}

        private void buttonSwitch_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                buttonSwitch_Click(this, e);
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
            this.radioButtonOff.Focus();
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
