//--------------------------------------------------------------------
// FILENAME: ControlForm.cs
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

namespace CS_DisplaySample1
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class ControlForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TrackBar BacklightLeveltrackBar;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button AboutButton;
		private System.Windows.Forms.Button ExitButton;
		private System.Windows.Forms.TrackBar ContrastLeveltrackBar;
		private System.Windows.Forms.Label Contrastlabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox BacklightCheckBox;
		private Symbol.Display.Controller MyDisplayController=null;

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

		public ControlForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this.AboutButton.Focus();
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
            this.BacklightLeveltrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.Contrastlabel = new System.Windows.Forms.Label();
            this.ContrastLeveltrackBar = new System.Windows.Forms.TrackBar();
            this.AboutButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BacklightCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // BacklightLeveltrackBar
            // 
            this.BacklightLeveltrackBar.LargeChange = 1;
            this.BacklightLeveltrackBar.Location = new System.Drawing.Point(16, 93);
            this.BacklightLeveltrackBar.Name = "BacklightLeveltrackBar";
            this.BacklightLeveltrackBar.Size = new System.Drawing.Size(152, 45);
            this.BacklightLeveltrackBar.TabIndex = 3;
            this.BacklightLeveltrackBar.ValueChanged += new System.EventHandler(this.BacklightLeveltrackBar_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 27);
            this.label1.Text = "Backlight Levels";
            // 
            // Contrastlabel
            // 
            this.Contrastlabel.Location = new System.Drawing.Point(16, 152);
            this.Contrastlabel.Name = "Contrastlabel";
            this.Contrastlabel.Size = new System.Drawing.Size(152, 21);
            this.Contrastlabel.Text = "Contrast Levels";
            // 
            // ContrastLeveltrackBar
            // 
            this.ContrastLeveltrackBar.LargeChange = 1;
            this.ContrastLeveltrackBar.Location = new System.Drawing.Point(16, 181);
            this.ContrastLeveltrackBar.Name = "ContrastLeveltrackBar";
            this.ContrastLeveltrackBar.Size = new System.Drawing.Size(160, 45);
            this.ContrastLeveltrackBar.TabIndex = 4;
            this.ContrastLeveltrackBar.ValueChanged += new System.EventHandler(this.ContrastLeveltrackBar_ValueChanged);
            // 
            // AboutButton
            // 
            this.AboutButton.Location = new System.Drawing.Point(8, 240);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(88, 24);
            this.AboutButton.TabIndex = 0;
            this.AboutButton.Text = "About";
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            this.AboutButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AboutButton_KeyDown);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(160, 240);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(72, 24);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.Text = "Exit";
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            this.ExitButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExitButton_KeyDown);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 24);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(152, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 24);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 24);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(152, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 24);
            // 
            // BacklightCheckBox
            // 
            this.BacklightCheckBox.Location = new System.Drawing.Point(24, 11);
            this.BacklightCheckBox.Name = "BacklightCheckBox";
            this.BacklightCheckBox.Size = new System.Drawing.Size(191, 39);
            this.BacklightCheckBox.TabIndex = 2;
            this.BacklightCheckBox.Text = "Backlight (checked = ON)";
            this.BacklightCheckBox.CheckStateChanged += new System.EventHandler(this.BacklightCheckBox_CheckStateChanged);
            this.BacklightCheckBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BacklightCheckBox_KeyDown);
            // 
            // ControlForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 270);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Contrastlabel);
            this.Controls.Add(this.BacklightCheckBox);
            this.Controls.Add(this.BacklightLeveltrackBar);
            this.Controls.Add(this.ContrastLeveltrackBar);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.ExitButton);
            this.Name = "ControlForm";
            this.Text = "CS_DisplaySample1";
            this.Load += new System.EventHandler(this.ControlForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ControlForm_KeyUp);
            this.Resize += new System.EventHandler(this.ControlForm_Resize);
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
        private static void Scale(ControlForm frm)
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
            ControlForm cf = new ControlForm();
            cf.DoScale();
            Application.Run(cf);
		}

		private void ControlForm_Load(object sender, System.EventArgs e)
		{

			//Select a device from listing
			Symbol.Display.Device MyDevice=(Symbol.Display.Device)Symbol.StandardForms.SelectDevice.Select(
				Symbol.Display.Controller.Title,
				Symbol.Display.Device.AvailableDevices);

			if(MyDevice ==null)
			{
			
				MessageBox.Show("No Device Selected", "SelectDevice");

				//close the form
				this.Close();

				return;
			}

			//Check the device type
			switch (MyDevice.DisplayType)
			{
					// if device is standard device
				case Symbol.Display.DisplayType.StandardDisplay:
					MyDisplayController = new Symbol.Display.StandardDisplay(MyDevice);
					break;

					//if device is simulated device
				case Symbol.Display.DisplayType.SimulatedDisplay:
					MyDisplayController = new Symbol.Display.SimulatedDisplay(MyDevice);
					break;
				default :
					throw new Symbol.Exceptions.InvalidDataTypeException("Unknown Device Type");

			}

			//add a event  handler  to handle event change
			this.MyDisplayController.ChangeNotify+=new EventHandler(MyDisplayController_ChangeNotify);


			//if backlightIntensitylevel is supported assign the max and min value to intensity
			if ( this.MyDisplayController.BacklightIntensityLevels > 1 )
			{
			
				this.BacklightLeveltrackBar.Minimum=0;
				this.BacklightLeveltrackBar.Maximum=(int)this.MyDisplayController.BacklightIntensityLevels-1;
                this.BacklightLeveltrackBar.Value = (int)this.MyDisplayController.BacklightIntensityLevel;
                this.BacklightCheckBox.Checked = this.MyDisplayController.BacklightState==Symbol.Display.BacklightState.ON; 

				this.label2.Text=this.BacklightLeveltrackBar.Minimum.ToString();
				this.label3.Text=this.BacklightLeveltrackBar.Maximum.ToString();
				
				this.BacklightLeveltrackBar.Show();
				this.BacklightCheckBox.Show();
				this.label2.Show();
				this.label3.Show();
			}
			else
			{
			
				this.BacklightLeveltrackBar.Hide();
				this.BacklightCheckBox.Hide();
				this.label2.Hide();
				this.label3.Hide();
			}

			if ( this.MyDisplayController.ContrastLevels > 1 )
			{

				this.ContrastLeveltrackBar.Minimum=0;
				this.ContrastLeveltrackBar.Maximum=this.MyDisplayController.ContrastLevels-1;
				this.ContrastLeveltrackBar.Value=this.MyDisplayController.ContrastLevel;
				this.label4.Text=this.ContrastLeveltrackBar.Minimum.ToString();
				this.label5.Text=this.ContrastLeveltrackBar.Maximum.ToString();
				this.label4.Show();
				this.label5.Show();
				this.Contrastlabel.Show();
				this.ContrastLeveltrackBar.Show();
			}
			else
			{
			
				this.label4.Hide();
				this.label5.Hide();
				this.Contrastlabel.Hide();
				this.ContrastLeveltrackBar.Hide();
			}
		
			// Add MainMenu if Pocket PC
			if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
			{
				this.Menu = new MainMenu();
			}
		}

		private void AboutButton_Click(object sender, System.EventArgs e)
		{
			string sVerInfo="CS_DisplaySample1 - C# Display Control Sample 1"+"\r\n"+"\n"+
				"CAPI Version\t"+this.MyDisplayController.Version.CAPIVersion+"\r\n" +
				"Assembly Version\t"+this.MyDisplayController.Version.AssemblyVersion;
			
			//Display The about Dialog
			Symbol.StandardForms.About.Run(null,sVerInfo);
            this.AboutButton.Focus();

		}

		private void ExitButton_Click(object sender, System.EventArgs e)
		{
			//Release all the resource before exiting
			this.MyDisplayController.Dispose ();

			//close the form
			this.Close();

		}

        private void AboutButton_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                AboutButton_Click(this, e);
        }

        private void ExitButton_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                ExitButton_Click(this, e);
        }

        private void ControlForm_KeyUp(object sender, KeyEventArgs e)
        {
            this.AboutButton.Focus();
        }

		private void MyDisplayController_ChangeNotify(object sender, System.EventArgs  e)
		{

			//to Get the next status of display controller
			Symbol.Display.DisplayStatus MyStatus=this.MyDisplayController.GetNextStatus ();

			if(MyStatus != null)
			{
				switch(MyStatus.Change)
				{
					case Symbol.Display.ChangeType.BACKLIGHT://Backlight change

						if(this.MyDisplayController.BacklightState==Symbol.Display.BacklightState.ON )
							this.BacklightCheckBox.Checked = true;
						else
							this.BacklightCheckBox.Checked = false;
						break;

					case Symbol.Display.ChangeType.INTENSITY://Intensity change
						this.BacklightLeveltrackBar.Value  =this.MyDisplayController.BacklightIntensityLevel;
						break;

					case Symbol.Display.ChangeType.CONTRAST://contrast change
						this.ContrastLeveltrackBar.Value  =this.MyDisplayController.ContrastLevel;
						break;
				}
				
			}

		}

		private void BacklightLeveltrackBar_ValueChanged(object sender, System.EventArgs e)
		{
			this.MyDisplayController.BacklightIntensityLevel=this.BacklightLeveltrackBar.Value;
		}
		private void BacklightCheckBox_CheckStateChanged(object sender, System.EventArgs e)
		{
		
			if(this.BacklightCheckBox.Checked)

				this.MyDisplayController.BacklightState=Symbol.Display.BacklightState.ON;
				
			else

				this.MyDisplayController.BacklightState=Symbol.Display.BacklightState.OFF;

		}

        private void BacklightCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
            {
                this.BacklightCheckBox.Checked = !(this.BacklightCheckBox.Checked);
                BacklightCheckBox_CheckStateChanged(this, e);
            }
        }


		private void ContrastLeveltrackBar_ValueChanged(object sender, System.EventArgs e)
		{
			this.MyDisplayController.ContrastLevel=this.ContrastLeveltrackBar.Value;
		}

        private void ControlForm_Resize(object sender, EventArgs e)
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
