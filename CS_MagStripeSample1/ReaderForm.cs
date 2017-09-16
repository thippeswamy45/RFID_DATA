//--------------------------------------------------------------------
// FILENAME: ReaderForm.cs
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
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CS_MagStripeSample1 
{
	/// <summary>
	/// CS_MagStripeSample1 Form class.
	/// </summary>
	public class ReaderForm : System.Windows.Forms.Form
	{
		public ReaderForm()
		{
			InitializeComponent();

			this.AboutButton.Focus();
		}
#if COMPLETE_FRAMEWORK
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}
#endif
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.CloseButton = new System.Windows.Forms.Button();
			this.AboutButton = new System.Windows.Forms.Button();
			this.ReaderDataListBox = new System.Windows.Forms.ListBox();
			this.ReaderDataLabel = new System.Windows.Forms.Label();
			// 
			// CloseButton
			// 
			this.CloseButton.Location = new System.Drawing.Point(169, 240);
			this.CloseButton.Size = new System.Drawing.Size(64, 25);
			this.CloseButton.Text = "Exit";
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			this.CloseButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CloseButton_KeyDown);
			// 
			// AboutButton
			// 
			this.AboutButton.Location = new System.Drawing.Point(9, 240);
			this.AboutButton.Size = new System.Drawing.Size(64, 25);
			this.AboutButton.Text = "About";
			this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
			this.AboutButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AboutButton_KeyDown);
			// 
			// ReaderDataListBox
			// 
			this.ReaderDataListBox.Location = new System.Drawing.Point(9, 31);
			this.ReaderDataListBox.Size = new System.Drawing.Size(224, 184);
			// 
			// ReaderDataLabel
			// 
			this.ReaderDataLabel.Location = new System.Drawing.Point(8, 8);
			this.ReaderDataLabel.Size = new System.Drawing.Size(224, 16);
			this.ReaderDataLabel.Text = "MagStripe Data:";
			// 
			// ReaderForm
			// 
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(240, 270);
			this.Controls.Add(this.ReaderDataLabel);
			this.Controls.Add(this.ReaderDataListBox);
			this.Controls.Add(this.AboutButton);
			this.Controls.Add(this.CloseButton);
			this.Text = "CS_MagStripeSample1";
			this.Resize += new System.EventHandler(this.ReaderForm_Resize);
			this.Load += new System.EventHandler(this.ReaderForm_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ReaderForm_KeyUp);

		}
		#endregion

		private System.Windows.Forms.Button AboutButton;
		private System.Windows.Forms.Button CloseButton;

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

		private int resWidthReference = 246;   // The (cached) width of the form. 
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


		private Symbol.MagStripe.Reader MyReader = null;
		private Symbol.MagStripe.ReaderData MyReaderData = null;
		private System.EventHandler MyEventHandler = null;
		private System.Windows.Forms.ListBox ReaderDataListBox;
		private System.Windows.Forms.Label ReaderDataLabel;


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
		private static void Scale(ReaderForm frm)
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

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main() 
		{
			ReaderForm rf = new ReaderForm();
			rf.DoScale();
			Application.Run(rf);
		}

		/// <summary>
		/// Occurs before the form is displayed for the first time.
		/// </summary>
		private void ReaderForm_Load(object sender, System.EventArgs e)
		{
			// Add MainMenu if Pocket PC
			if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
			{
				this.Menu = new MainMenu();
			}

			// If we can initialize the Reader
			if ( this.InitReader() )
			{
				// Start a read on the reader
				this.StartRead();
			}
			else
			{
				MessageBox.Show("Unable to initialize MSR!","Error");

				// If not, close this form
				this.Close();

				return;
			}
            
		}

		/// <summary>
		/// Initialize the reader.
		/// </summary>
		private bool InitReader()
		{
			// If reader is already present then fail initialize
			if ( this.MyReader != null )
			{
				return false;
			}

			try
			{
				if(Symbol.MagStripe.Device.AvailableDevices.Length>1)

				{
					SelectDevForm devFrm = new SelectDevForm();
					devFrm.DoScale();
					devFrm.ShowDialog();
					string devName = devFrm.GetDeviceName();
					if((devName !=null)&&(devName !=""))
					{
						this.MyReader = new Symbol.MagStripe.Reader(new Symbol.MagStripe.Device(devName));
					}
					else
					{
						MessageBox.Show("No device selected.");
						return false;
					}

				}
				else
				{
					// Create new reader, first available reader will be used.
					this.MyReader = new Symbol.MagStripe.Reader();
				}

				// Create reader data
				this.MyReaderData = new Symbol.MagStripe.ReaderData();

				// Enable reader
				this.MyReader.Actions.Enable();
	
				// Attach handler for read notification
				this.MyEventHandler	= new EventHandler(MyReader_ReadNotify);

				// Attach to activate and deactivate events
				this.Activated +=new EventHandler(ReaderForm_Activated);
				this.Deactivate +=new EventHandler(ReaderForm_Deactivate);
			}
			catch
			{
				return false;
			}
	
			return true;
		}

		/// <summary>
		/// Application is closing
		/// </summary>
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			// Terminate reader
			this.TermReader();
			base.OnClosing(e);
		}

		/// <summary>
		/// Stop reading and disable/close reader
		/// </summary>
		private void TermReader()
		{
			// If we have a reader
			if ( this.MyReader != null )
			{
				// remove read notification handler
				this.MyReader.ReadNotify -= null;

				// Disable reader, with wait cursor
				this.MyReader.Actions.Disable();
		
				// Free it up
				this.MyReader.Dispose();

				// Indicate we no longer have one
				this.MyReader = null;
			}

			// If we have a reader data
			if ( this.MyReaderData != null )
			{
				// Free it up
				this.MyReaderData.Dispose();

				// Indicate we no longer have one
				this.MyReaderData = null;
			}
		}

		/// <summary>
		/// Start a read on the reader
		/// </summary>
		private void StartRead()
		{
			// If we have both a reader and a reader data
			if((this.MyReader != null) && (this.MyReaderData != null))
			{
				// Submit a read
				this.MyReader.ReadNotify += this.MyEventHandler;
				this.MyReader.Actions.Read(this.MyReaderData);
			}
		}

		/// <summary>
		/// Stop all reads on the reader
		/// </summary>
		private void StopRead()
		{
			// If we have a reader
			if ( this.MyReader != null )
			{
				// Flush (Cancel all pending reads)
				this.MyReader.ReadNotify -= this.MyEventHandler;
				this.MyReader.Actions.Flush();
			}
		}

		/// <summary>
		/// Display about box
		/// </summary>
		private void AboutButton_Click(object sender, System.EventArgs e)
		{
			string sVerInfo = "CS_MagStripeSample1 - v1.1.1.1\r\n" + 
				"API Version - " + MyReader.Version.APIVersion + "\r\n" +
				"DLL Version - " + MyReader.Version.DLLVersion + "\r\n" +
				"HW Version - " + MyReader.Version.FWVersion + "\r\n";

			Symbol.StandardForms.About.Run(	null,sVerInfo);
			this.AboutButton.Focus();
		}

		/// <summary>
		/// Click from the close button
		/// </summary>
		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			// Close this form
			this.Close();
		}

		private void AboutButton_KeyDown(object sender, KeyEventArgs e)
		{
			// Checks if the key pressed was an enter button (character code 13)
			if (e.KeyValue == (char)13)
				AboutButton_Click(this, e);
		}

		private void CloseButton_KeyDown(object sender, KeyEventArgs e)
		{
			// Checks if the key pressed was an enter button (character code 13)
			if (e.KeyValue == (char)13)
				CloseButton_Click(this, e);
		}

		private void ReaderForm_KeyUp(object sender, KeyEventArgs e)
		{
			this.AboutButton.Focus();
		}

		/// <summary>
		/// Read complete or failure notification
		/// </summary>
		private void MyReader_ReadNotify(object sender, EventArgs e)
		{
			// Checks if the Invoke method is required because the ReadNotify delegate is called by a different thread
            if (this.InvokeRequired)
            {
				// Executes the ReadNotify delegate on the main thread
                this.Invoke(MyEventHandler, new object[] { sender, e });
            }
            else
            {
                Symbol.MagStripe.ReaderData TheReaderData =
                    this.MyReader.GetNextReaderData();

                // If it is a successful read (as opposed to a failed one)
                if (TheReaderData.Result == Symbol.Results.SUCCESS)
                {
                    // Handle the data from this read
                    this.HandleData(TheReaderData);

                    // Start the next read
                    this.StartRead();
                }
                else if (TheReaderData.Result == Symbol.Results.CANCELED)
                {
                    // Previous read was not successful, start the next read
                    this.StartRead();
                }
            }
		}

	
		/// <summary>
		/// Handle data from the reader
		/// </summary>
		private void HandleData(Symbol.MagStripe.ReaderData TheReaderData)
		{
			System.Threading.Thread.Sleep(25);

			// If we have a track 1
			if ( TheReaderData.Track1Decode )
			{
				// Add data for track 1
				this.ReaderDataListBox.Items.Add("Track1:");
				this.ReaderDataListBox.Items.Add("  " + MyReaderData.Track1Data);
			}

			// If we have a track 2
			if ( TheReaderData.Track2Decode )
			{
				// Add data for track 2
				this.ReaderDataListBox.Items.Add("Track2:");
				this.ReaderDataListBox.Items.Add("  " + MyReaderData.Track2Data);
			}

			// If we have a track 3
			if ( TheReaderData.Track3Decode )
			{
				// Add data for track 3
				this.ReaderDataListBox.Items.Add("Track3:");
				this.ReaderDataListBox.Items.Add("  " + MyReaderData.Track3Data);
			}

			// Add a blank line
			this.ReaderDataListBox.Items.Add("");

			// While we have too many items to fit without scrolling
			while ( ReaderDataListBox.Items.Count > 13 )
			{
				// Remove the oldest item
				ReaderDataListBox.Items.RemoveAt(0);
			}
		}

		private void ReaderForm_Activated(object sender, EventArgs e)
		{
			// If there are no reads pending on MyReader start a new read
			if ( this.MyReader.Info.PendingReads == 0 )
				this.StartRead();
		}

		private void ReaderForm_Deactivate(object sender, EventArgs e)
		{
			this.StopRead();
		}

		private void ReaderForm_Resize(object sender, System.EventArgs e)
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
