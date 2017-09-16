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
using Symbol.MagStripe2;

namespace CS_MagStripe2Sample1 
{
	/// <summary>
	/// CS_MagStripe2Sample1 Form class.
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
            this.ReaderDataLabel = new System.Windows.Forms.Label();
            this.SwipeDataTextBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(169, 240);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(64, 25);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "Exit";
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            this.CloseButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CloseButton_KeyDown);
            // 
            // AboutButton
            // 
            this.AboutButton.Location = new System.Drawing.Point(9, 240);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(64, 25);
            this.AboutButton.TabIndex = 2;
            this.AboutButton.Text = "About";
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            this.AboutButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AboutButton_KeyDown);
            // 
            // ReaderDataLabel
            // 
            this.ReaderDataLabel.Location = new System.Drawing.Point(8, 8);
            this.ReaderDataLabel.Name = "ReaderDataLabel";
            this.ReaderDataLabel.Size = new System.Drawing.Size(224, 16);
            this.ReaderDataLabel.Text = "MagStripe Data:";
            // 
            // SwipeDataTextBox1
            // 
            this.SwipeDataTextBox1.Location = new System.Drawing.Point(9, 27);
            this.SwipeDataTextBox1.Multiline = true;
            this.SwipeDataTextBox1.Name = "SwipeDataTextBox1";
            this.SwipeDataTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SwipeDataTextBox1.Size = new System.Drawing.Size(223, 198);
            this.SwipeDataTextBox1.TabIndex = 4;
            // 
            // ReaderForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 270);
            this.Controls.Add(this.SwipeDataTextBox1);
            this.Controls.Add(this.ReaderDataLabel);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.CloseButton);
            this.Name = "ReaderForm";
            this.Text = "CS_MagStripe2Sample1";
            this.Load += new System.EventHandler(this.ReaderForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ReaderForm_KeyUp);
            this.Resize += new System.EventHandler(this.ReaderForm_Resize);
            this.ResumeLayout(false);

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

        
		private System.Windows.Forms.Label ReaderDataLabel;

        // All the magstripe related operations in this sample would be carried out  
        //  by using this reference of myMag2SampleAPI which is an instance of the class 
        //  API. Will be initialized later in the code.
        private API myMag2SampleAPI = null;
        private MagStripe2.OnSwipeHandler mySwipeHandler = null;
        private TextBox SwipeDataTextBox1;

        // The flag to track whether the MagStripe2 object has been initialized or not.
        private bool isMagStripeInitiated = false;

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

            // Attach to activate and deactivate events
            this.Activated += new EventHandler(ReaderForm_Activated);
            this.Deactivate += new EventHandler(ReaderForm_Deactivate);

            // Initialize the API reference.
            this.myMag2SampleAPI = new API();

            this.isMagStripeInitiated = this.myMag2SampleAPI.InitMagStripe();

            if (!(this.isMagStripeInitiated))// If the MagStripe object has not been initialized
            {
                MessageBox.Show("Unable to initialize MSR!", "Error");
                Application.Exit();
            }
            else // If the MagStripe object has been initialized
            {
                // Attach a scan notification handler.
                this.mySwipeHandler = new MagStripe2.OnSwipeHandler(myMagStripe2_SwipeNotify);
                myMag2SampleAPI.AttachSwipeNotify(mySwipeHandler);

                myMag2SampleAPI.StartSwipe();
            }
            
		}

		/// <summary>
		/// Application is closing
		/// </summary>
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			// Terminate magstripe object
            if (isMagStripeInitiated)
            {
                myMag2SampleAPI.TermMagStripe();
            }

			base.OnClosing(e);
		}

		/// <summary>
		/// Display about box
		/// </summary>
		private void AboutButton_Click(object sender, System.EventArgs e)
		{
			string sVerInfo = "CS_MagStripe2Sample1 - v1.1.1.1\r\n" + 
				"API Version - " + myMag2SampleAPI.MagStripe2.Version.APIVersion + "\r\n" +
                "DLL Version - " + myMag2SampleAPI.MagStripe2.Version.DLLVersion + "\r\n" +
                "HW Version - " + myMag2SampleAPI.MagStripe2.Version.FWVersion + "\r\n";

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
		/// Swipe complete or failure notification
		/// </summary>
        private void myMagStripe2_SwipeNotify(MagStripeData magData)
        {
            // If it is a successful read (as opposed to a failed one)
            if (magData.Result == Results.SUCCESS)
            {
                // Handle the data from this swipe
                this.HandleData(magData);
                if (myMag2SampleAPI.MagStripe2.Config.IsDCR == true &&
                    magData.PANData != string.Empty)
                {
                    this.SwipeDataTextBox1.Text += "\r\nEnter PIN on DCR:\r\n";

                    // Read DCR PIN data
                    PINData PINData = myMag2SampleAPI.MagStripe2.EnterDebitPINWait(10000, magData.PANData);

                    this.SwipeDataTextBox1.Text += "\r\nEncoded PIN Data:\r\n";
                    string PINEncryptedData = null;
                    for (int i = 0; i < PINData.PINEncryptedData.Length; i++)
                    {
                        PINEncryptedData += PINData.PINEncryptedData[i].ToString("X2");
                    }
                    this.SwipeDataTextBox1.Text += "    " + PINEncryptedData + "\r\n";
                }
            }
            else
            {
                string sMsg = "Swipe Failed\r\n"
                        + "Result = "
                        + (magData.Result).ToString();

                this.SwipeDataTextBox1.Text = sMsg;
            }

            this.SwipeDataTextBox1.Text += "\r\n";
            this.SwipeDataTextBox1.Text += "Swipe again...";

            // Start the next read
            myMag2SampleAPI.StartSwipe();
        }

	
		/// <summary>
		/// Handle data from the reader
		/// </summary>
		private void HandleData(MagStripeData magData)
		{
            this.SwipeDataTextBox1.Text = string.Empty;

            if (magData.CreditCardData.IsValid)
            {
                this.SwipeDataTextBox1.Text += "Credit card\r\n";

                this.SwipeDataTextBox1.Text += "  Card Holder Name:\r\n";
                this.SwipeDataTextBox1.Text += "    " + magData.CreditCardData.CardHolderName + "\r\n";

                this.SwipeDataTextBox1.Text += "  Card Number:\r\n";
                this.SwipeDataTextBox1.Text += "    " + magData.CreditCardData.CardNumber + "\r\n";

                this.SwipeDataTextBox1.Text += "  Expiration Date:\r\n";
                this.SwipeDataTextBox1.Text += "    " + magData.CreditCardData.ExpirationDate + "\r\n";
            }
            else
            {
                this.SwipeDataTextBox1.Text += "Not a credit card\r\n";

                // Display all text data.
                this.SwipeDataTextBox1.Text += "\r\nMagStripeData:\r\n";
                this.SwipeDataTextBox1.Text += "  " + magData.Data + "\r\n";
            }
		}

		private void ReaderForm_Activated(object sender, EventArgs e)
		{
            myMag2SampleAPI.AttachSwipeNotify(mySwipeHandler);

            // If there are no reads pending on MagStripe2 start a new read
			if ( myMag2SampleAPI.MagStripe2.IsSwipePending == false )
                myMag2SampleAPI.StartSwipe();
		}

		private void ReaderForm_Deactivate(object sender, EventArgs e)
		{
            myMag2SampleAPI.DetachSwipeNotify(mySwipeHandler);
            myMag2SampleAPI.StopSwipe();
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
