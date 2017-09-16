using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Symbol.MagStripe2;

namespace CS_MagStripe2Sample1
{
	/// <summary>
	/// Summary description for SelectDevForm.
	/// </summary>
	public class SelectDevForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ComboBox cbDevices;
		private System.Windows.Forms.Label label1;

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

		private int resWidthReference = 232;   // The (cached) width of the form. 
		// INITIALLY HAS TO BE SET TO THE WIDTH OF THE FORM AT DESIGN TIME (IN PIXELS).
		// This setting is also obtained from the platform only on
		// Windows CE devices before running the application on the device, as a verification.
		// For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

		private int resHeightReference = 160;  // The (cached) height of the form.
		// INITIALLY HAS TO BE SET TO THE HEIGHT OF THE FORM AT DESIGN TIME (IN PIXELS).
		// This setting is also obtained from the platform only on
		// Windows CE devices before running the application on the device, as a verification.
		// For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

		private const double maxLength = 3.5;  // The maximum physical width/height of the sample (in inches).
		// The actual value on the device may slightly deviate from this
		// since the calculations based on the (received) DPI & resolution values 
		// would provide only an approximation, so not 100% accurate.


		private string devName = null;

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
		private static void Scale(SelectDevForm frm)
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

			float dpiX = GetDPIX(); // Get the horizontal DPI value.

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

			float dpiY = GetDPIY();

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

		
		// DPI values have to be taken via PInvokes on VS2003/CF1.0. The 2 proerties, System.Drawing.Graphics.DpiX
		// & System.Drawing.Graphics.DpiY, are not supported by VS2003/CF1.0.

		// For DpiX
		private static float GetDPIX()
		{
			IntPtr hwnd = (IntPtr)(GetCapture()); 
 
			float dpiX = GetDeviceCaps(hwnd, 88); // 88 for DPIX value

			return dpiX;
            
		}

		// For DpiY
		private static float GetDPIY()
		{
			IntPtr hwnd = (IntPtr)(GetCapture()); 
 
			float dpiY = GetDeviceCaps(hwnd, 90); // 90 for DPIY value

			return dpiY;

		}
	
		public SelectDevForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			foreach (Device device in Devices.SupportedDevices)
			{
				cbDevices.Items.Add(device.FriendlyName);
			}
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
			this.cbDevices = new System.Windows.Forms.ComboBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			// 
			// cbDevices
			// 
			this.cbDevices.Location = new System.Drawing.Point(24, 48);
			this.cbDevices.Size = new System.Drawing.Size(160, 22);
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(24, 96);
			this.buttonOK.Size = new System.Drawing.Size(80, 24);
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(128, 96);
			this.buttonCancel.Size = new System.Drawing.Size(80, 24);
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Size = new System.Drawing.Size(152, 24);
			this.label1.Text = "Select the MSR device";
			// 
			// SelectDevForm
			// 
			this.ClientSize = new System.Drawing.Size(226, 135);
			this.ControlBox = false;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.cbDevices);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Text = "Select Device";
			this.Resize += new System.EventHandler(this.SelectDevForm_Resize);
			this.Load += new System.EventHandler(this.SelectDevForm_Load);

		}
		#endregion

		private void SelectDevForm_Load(object sender, System.EventArgs e)
		{
            cbDevices.SelectedItem = Devices.SupportedDevices[0].FriendlyName;
		}

		public string GetDeviceName()
		{
			return devName;
		}
		
		private void buttonOK_Click(object sender, System.EventArgs e)
		{
            devName = Devices.SupportedDevices[cbDevices.SelectedIndex].DeviceName;
			this.Close();
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
            devName = null;
			this.Close();
		}

		private void SelectDevForm_Resize(object sender, System.EventArgs e)
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

		[DllImport("coredll.dll")]  
		public static extern Int32 GetDeviceCaps(IntPtr hdc, Int32 index);
	
		[DllImport("coredll.dll")]
		public static extern Int32 GetCapture();

	}
}
