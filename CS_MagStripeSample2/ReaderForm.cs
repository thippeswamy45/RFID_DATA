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
using System.Collections;

namespace CS_MagStripeSample2 
{
	/// <summary>
	/// CS_MagStripeSample2 Form class.
	/// </summary>
	public class ReaderForm : System.Windows.Forms.Form
	{
		public ReaderForm()
		{
			//
			// Required for Windows Form Designer support
			//
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
            this.EventTextBox = new System.Windows.Forms.ListBox();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.ReaderDataTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(169, 240);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(64, 25);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "Exit";
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            this.CloseButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CloseButton_KeyDown);
            // 
            // AboutButton
            // 
            this.AboutButton.Location = new System.Drawing.Point(9, 240);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(64, 25);
            this.AboutButton.TabIndex = 0;
            this.AboutButton.Text = "About";
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            this.AboutButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AboutButton_KeyDown);
            // 
            // ReaderDataLabel
            // 
            this.ReaderDataLabel.Location = new System.Drawing.Point(10, 8);
            this.ReaderDataLabel.Name = "ReaderDataLabel";
            this.ReaderDataLabel.Size = new System.Drawing.Size(222, 15);
            this.ReaderDataLabel.Text = "MagStripe Data:";
            // 
            // EventTextBox
            // 
            this.EventTextBox.Location = new System.Drawing.Point(7, 192);
            this.EventTextBox.Name = "EventTextBox";
            this.EventTextBox.Size = new System.Drawing.Size(225, 34);
            this.EventTextBox.TabIndex = 4;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Location = new System.Drawing.Point(8, 173);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(113, 16);
            this.StatusLabel.Text = "MagStripe Status:";
            // 
            // ReaderDataTextBox
            // 
            this.ReaderDataTextBox.Location = new System.Drawing.Point(10, 26);
            this.ReaderDataTextBox.Multiline = true;
            this.ReaderDataTextBox.Name = "ReaderDataTextBox";
            this.ReaderDataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ReaderDataTextBox.Size = new System.Drawing.Size(222, 144);
            this.ReaderDataTextBox.TabIndex = 3;
            // 
            // ReaderForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 270);
            this.Controls.Add(this.ReaderDataTextBox);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.ReaderDataLabel);
            this.Controls.Add(this.EventTextBox);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.CloseButton);
            this.Name = "ReaderForm";
            this.Text = "CS_MagStripeSample2";
            this.Load += new System.EventHandler(this.ReaderForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ReaderForm_KeyUp);
            this.Resize += new System.EventHandler(this.ReaderForm_Resize);
            this.ResumeLayout(false);

		}
		#endregion

		private Symbol.MagStripe.Reader MyReader = null;
		private System.EventHandler MyReadNotifyHandler = null;
		private System.EventHandler MyStatusNotifyHandler = null;
		private Symbol.MagStripe.CreditCardReaderData MyCCReaderData = null;
        private Symbol.MagStripe.ReaderData MyReaderData = null;

        private System.Windows.Forms.Label ReaderDataLabel;
        private TextBox ReaderDataTextBox;  
		private System.Windows.Forms.ListBox EventTextBox;
		private System.Windows.Forms.Button AboutButton;
		private System.Windows.Forms.Label StatusLabel;
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

        private int resWidthReference = 242;   // The (cached) width of the form. 
        // INITIALLY HAS TO BE SET TO THE WIDTH OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private int resHeightReference = 295;   // The (cached) height of the form.
        // INITIALLY HAS TO BE SET TO THE HEIGHT OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private const double maxLength = 5.5;  // The maximum physical width/height of the sample (in inches).
        // The actual value on the device may slightly deviate from this
        // since the calculations based on the (received) DPI & resolution values 
        // would provide only an approximation, so not 100% accurate.

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

			// Initialize the Reader
			if ( this.InitReader() )
			{
				// Start a read on the reader
				this.StartStatus();
				this.StartRead();
			}
			else
			{
				MessageBox.Show("Unable to initialize MSR!", "Error");

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

			Symbol.Generic.Device MyDevice
				= Symbol.StandardForms.SelectDevice.Select(
				Symbol.MagStripe.Device.Title,
				Symbol.MagStripe.Device.AvailableDevices);
		
			if ( MyDevice == null )
			{
				MessageBox.Show("No Device Selected","SelectDevice");

				return false;
			}

			try
			{
				// Create new reader, first available reader will be used.
				this.MyReader = new Symbol.MagStripe.Reader(MyDevice);

				// Create reader data
				this.MyCCReaderData = new Symbol.MagStripe.CreditCardReaderData();

				// Enable reader
				this.MyReader.Actions.Enable();

				// create event handlers
				this.MyReadNotifyHandler = new EventHandler(MyReader_ReadNotify);
				this.MyStatusNotifyHandler = new EventHandler(MyReader_StatusNotify);

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
			// Terminate reader, call base class
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
				// stop all notifications
				this.StopRead();
				this.StopStatus();
                this.MyReader.Actions.Flush();

				// Disable the reader
				this.MyReader.Actions.Disable();

				this.MyReader.Dispose();

				this.MyReader = null;
			}

			if ( this.MyCCReaderData != null )
			{
				this.MyCCReaderData.Dispose();

				this.MyCCReaderData = null;
			}
		}

	
		/// <summary>
		/// Start a read on the reader
		/// </summary>
		private void StartRead()
		{
			// If we have both a reader and a reader data
			if ( ( this.MyReader != null ) &&
				( this.MyCCReaderData != null ) )
			{

				this.MyReader.ReadNotify += MyReadNotifyHandler;
				if(this.MyCCReaderData != null)
				{
					this.MyReader.Actions.Read(this.MyCCReaderData);
				}
			}
		}

		/// <summary>
		/// Stop all reads on the reader
		/// </summary>
		private void StopRead()
		{
			// If we do not have a reader, then do nothing
			if ( this.MyReader != null )
			{
				this.MyReader.ReadNotify -= MyReadNotifyHandler;
			}
		}

		/// <summary>
		/// Start status notifications
		/// </summary>
		private void StartStatus()
		{
			if( this.MyReader != null)
			{
				// Attach status notification handler
				this.MyReader.StatusNotify += this.MyStatusNotifyHandler;
			}
		}

		/// <summary>
		/// Stop all status notifications
		/// </summary>
		private void StopStatus()
		{
			if(this.MyReader != null)
			{
				// Detach status notification handler
				this.MyReader.StatusNotify -= this.MyStatusNotifyHandler;
			}
		}

		/// <summary>
		/// Click from the about button
		/// </summary>
		private void AboutButton_Click(object sender, System.EventArgs e)
		{
			string sVerInfo = "CS_MagStripeSample2 - v1.1.1.1\r\n" + 
				"API Version - " + MyReader.Version.APIVersion + "\r\n" +
				"DLL Version - " + MyReader.Version.DLLVersion + "\r\n" +
				"HW Version - " + MyReader.Version.FWVersion + "\r\n";

			Symbol.StandardForms.About.Run(
				null,
				sVerInfo);
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
		private void MyReader_ReadNotify(object Sender,EventArgs e)
		{
			// Checks if the Invoke method is required because the ReadNotify delegate is called by a different thread
            if (this.InvokeRequired)
            {
				// Executes the ReadNotify delegate on the main thread
                this.Invoke(MyReadNotifyHandler, new object[] { Sender, e });
            }
            else
            {
                MyReaderData = MyReader.GetNextReaderData();
                Symbol.MagStripe.CreditCardReaderData TheReaderData = (Symbol.MagStripe.CreditCardReaderData)MyReaderData;


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
                    this.ReaderDataTextBox.Text += "  Cancelled\r\n";
                    this.StartRead();
                }
                else if (TheReaderData.Result == Symbol.Results.MSR_ERR_TIMEOUT)
                {
                    // Previous read timed-out, start the next read
                    this.ReaderDataTextBox.Text += "  Time out\r\n";
                    this.StartRead();
                }
                else if (TheReaderData.Result == Symbol.Results.MSR_ERR_NODATA)
                {
                    // Previous read was not successful, start the next read
                    this.ReaderDataTextBox.Text += "  Incorrect data\r\n";
                    this.StartRead();
                }
            }
		}

		/// <summary>
		/// Handle data from the reader
		/// </summary>
		private void HandleData(Symbol.MagStripe.CreditCardReaderData TheReaderData)
		{
            System.Threading.Thread.Sleep(25);

            try
            {
                this.ReaderDataTextBox.Text = "";

                if (TheReaderData.IsValid)
                {
                    if (TheReaderData.PINEncryptedData == null)
                    {
                        this.ReaderDataTextBox.Text += "Credit card:\r\n";


                        this.ReaderDataTextBox.Text += "  Card Holder Name:\r\n";
                        this.ReaderDataTextBox.Text += "    " + TheReaderData.CardHolderName + "\r\n";

                        this.ReaderDataTextBox.Text += "  Card Number:\r\n";
                        this.ReaderDataTextBox.Text += "    " + TheReaderData.CardNumber + "\r\n";

                        this.ReaderDataTextBox.Text += "  Expiration Date:\r\n";
                        this.ReaderDataTextBox.Text += "    " + TheReaderData.ExpirationDate + "\r\n";

                        if (TheReaderData.PINCapable)
                        {
                            // Enable PIN read
                            this.MyCCReaderData.PINRead = true;

                            this.ReaderDataTextBox.Text += "  Enter PIN on DCR:\r\n";
                        }
                    }
                    else
                    {
                        this.ReaderDataTextBox.Text += "  Encoded PIN Data:\r\n";
                        string PINEncryptedData = null;
                        for (int i = 0; i < TheReaderData.PINEncryptedData.Length; i++)
                        {
                            PINEncryptedData += TheReaderData.PINEncryptedData[i].ToString("X2");
                        }
                        this.ReaderDataTextBox.Text += "    " + PINEncryptedData + "\r\n";
                    }

                }
                else
                {
                    this.ReaderDataTextBox.Text += "Not a credit card:\r\n";

                    if (TheReaderData.PINEncryptedData == null)
                    {
                        // Display all text data.
                        this.ReaderDataTextBox.Text += "\r\nReaderData.Text:\r\n";
                        this.ReaderDataTextBox.Text += "  " + TheReaderData.Text + "\r\n";

                        if (TheReaderData.PINCapable)
                        {
                            // Enable PIN read
                            this.MyCCReaderData.PINRead = true;

                            this.ReaderDataTextBox.Text += "\r\nEnter PIN on DCR:\r\n";
                        }
                    }
                    else
                    {
                        this.ReaderDataTextBox.Text += "\r\nEncoded PIN Data:\r\n";
                        string PINEncryptedData = null;
                        for (int i = 0; i < TheReaderData.PINEncryptedData.Length; i++)
                        {
                            PINEncryptedData += TheReaderData.PINEncryptedData[i].ToString("X2");
                        }
                        this.ReaderDataTextBox.Text += "    " + PINEncryptedData + "\r\n";
                    }

                    // The individual track data is automatically parsed and available in the 
                    // class library in ReaderData.Track1Data, ReaderData.Track2Data and ReaderData.Track3Data.
                    // Optionally, the following (commented) code can also be used, if the user wants to manually 
                    // parse the track data using multiple sentinel sets.

                    //TrackParser trackParser = new TrackParser();
                    
                    ///****************************
                    //Track1 sentinels - ['%', '?']
                    //Track2 sentinels - [';', '?']
                    //Track3 sentinels - [';', '?']
                    //****************************/
                    //trackParser.SentinelSets.Add(new SentinelSet('%', '?', ';', '?', ';', '?'));

                    ///****************************
                    //Track1 sentinels - ['%', '?']
                    //Track2 sentinels - [';', '?']
                    //Track3 sentinels - ['%', '?']
                    //****************************/
                    //trackParser.SentinelSets.Add(new SentinelSet('%', '?', ';', '?', '%', '?'));
                    
                    //trackParser.ParseTrackData(TheReaderData);

                    //this.ReaderDataTextBox.Text += "Track 1: " + trackParser.Track1Data + "\r\n";
                    //this.ReaderDataTextBox.Text += "Track 2: " + trackParser.Track2Data + "\r\n";
                    //this.ReaderDataTextBox.Text += "Track 3: " + trackParser.Track3Data + "\r\n";

                }
            }
            catch
            {
                this.ReaderDataTextBox.Text += "  Please re-swipe card\r\n";
            }

            // Add a blank line
            this.ReaderDataTextBox.Text += "\r\n"; ;
		}

		/// <summary>
		/// Event notification
		/// </summary>
		private void MyReader_StatusNotify(object o,EventArgs e)
		{
			// Checks if the Invoke method is required because the StatusNotify delegate is called by a different thread
            if (this.InvokeRequired)
            {
				// Executes the StatusNotify delegate on the main thread
                this.Invoke(MyStatusNotifyHandler, new object[] { o, e });
            }
            else
            {
                Symbol.MagStripe.MagStripeStatus MyEvent = this.MyReader.GetNextStatus();

                this.EventTextBox.Items.Clear();

                if (MyEvent.State == Symbol.MagStripe.States.READY)
                {
                    this.EventTextBox.Items.Add("Ready.");
                }
                else
                {
                    this.EventTextBox.Items.Add("Please wait...");
                }
            }
		}

		private void ReaderForm_Activated(object sender, EventArgs e)
		{
			this.StartStatus();

			// If there are no reads pending on MyReader start a new read
			if ( MyReader.Info.PendingReads == 0 )
				this.StartRead();
		}

		private void ReaderForm_Deactivate(object sender, EventArgs e)
		{
			this.StopRead();
			this.StopStatus();
            if (this.MyReader != null)
            {
                this.MyReader.Actions.Flush();
            }
		}

        private void ReaderForm_Resize(object sender, EventArgs e)
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

        // The manual parsing code uses three additional classes.
        //  - TrackParser
        //  - SentinelSets
        //  - SentinelSet

        /// <summary>
        /// This is the main class used for parsing the track data.
        /// </summary>
        public class TrackParser
        {
            /// <summary>
            /// Provides the parsed track1 data
            /// </summary>
            public string Track1Data;

            /// <summary>
            /// Provides the parsed track2 data
            /// </summary>
            public string Track2Data;

            /// <summary>
            /// Provides the parsed track3 data
            /// </summary>
            public string Track3Data;

            /// <summary>
            /// Provides access to the sentinel sets.
            /// </summary>
            public SentinelSets SentinelSets;

            /// <summary>
            /// Constructor.
            /// </summary>
            public TrackParser()
            {
                SentinelSets = new SentinelSets();
            }

            /// <summary>
            /// This method is used for parsing the track data.
            /// </summary>
            /// <param name="TheReaderData">ReaderData object used to obtain the complete track data and track decode status</param>
            public void ParseTrackData(Symbol.MagStripe.CreditCardReaderData TheReaderData)
            {
                String strData = TheReaderData.Text;
                int nStartDelimeter = 0, nEndDelimeter = 0;
                bool track1Found = false, track2Found = false, track3Found = false;

                Track1Data = string.Empty;
                Track2Data = string.Empty;
                Track3Data = string.Empty;

                for (int i = 0; i < SentinelSets.Length; i++)
                {
                    nStartDelimeter = nEndDelimeter = 0;
                    track1Found = track2Found = track3Found = false;

                    //track 1
                    if (TheReaderData.Track1Decode == true)
                    {
                        nStartDelimeter = strData.IndexOf(SentinelSets[i].Track1StartSentinel);
                        if (nStartDelimeter != -1)
                        {
                            nEndDelimeter = strData.IndexOf(SentinelSets[i].Track1EndSentinel, nStartDelimeter);
                            if (nEndDelimeter == -1)
                            {
                                // only found a single start and no end, use Track 2 start as end for Track1
                                nEndDelimeter = strData.IndexOf(SentinelSets[i].Track2StartSentinel, nStartDelimeter);
                                if (nEndDelimeter == -1)
                                {
                                    // if still not found then just use end of string
                                    nEndDelimeter = strData.Length - 1;
                                }
                            }

                            // track 1 found
                            Track1Data = strData.Substring(nStartDelimeter + 1, nEndDelimeter - nStartDelimeter - 1);
                            track1Found = true;
                        }
                    }
                    else
                    {
                        track1Found = true;
                    }


                    //track 2
                    if (TheReaderData.Track2Decode == true)
                    {
                        nStartDelimeter = strData.IndexOf(SentinelSets[i].Track2StartSentinel, nEndDelimeter);
                        if (nStartDelimeter != -1)
                        {
                            nEndDelimeter = strData.IndexOf(SentinelSets[i].Track2EndSentinel, nStartDelimeter);
                            if (nEndDelimeter == -1)
                            {
                                // If EndDelimeter could not be found, use end of string
                                nEndDelimeter = strData.Length - 1;
                            }

                            // track 2 found
                            Track2Data = strData.Substring(nStartDelimeter + 1, nEndDelimeter - nStartDelimeter - 1);
                            track2Found = true;
                        }
                    }
                    else
                    {
                        track2Found = true;
                    }

                    //track 3
                    if (TheReaderData.Track3Decode == true)
                    {
                        nStartDelimeter = strData.IndexOf(SentinelSets[i].Track3StartSentinel, nEndDelimeter);
                        if (nStartDelimeter != -1)
                        {
                            nEndDelimeter = strData.IndexOf(SentinelSets[i].Track3EndSentinel, nStartDelimeter);
                            if (nEndDelimeter == -1)
                            {
                                // IF EndDelimeter could not be found, use end of string
                                nEndDelimeter = strData.Length - 1;
                            }

                            // track 3 found
                            Track3Data = strData.Substring(nStartDelimeter + 1, nEndDelimeter - nStartDelimeter - 1);
                            track3Found = true;
                        }
                    }
                    else
                    {
                        track3Found = true;
                    }

                    if (track1Found && track2Found && track3Found)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// This class provides access to the list of sentinel sets used for parsing.
        /// </summary>
        public class SentinelSets
        {
            private ArrayList m_SentinelSets;

            internal SentinelSets()
            {
                m_SentinelSets = new ArrayList();
            }

            /// <summary>
            /// Provides access to the list of sentinel sets.
            /// </summary>
            /// <param name="index">The indexer provided to enumerate SentinelSet objects in the list.</param>
            /// <returns></returns>
            public SentinelSet this[int index]
            {
                get
                {
                    return (SentinelSet)m_SentinelSets[index];
                }
            }

            /// <summary>
            /// Length of the sentinel sets list.
            /// </summary>
            public int Length
            {
                get
                {
                    return m_SentinelSets.Count;
                }
            }

            /// <summary>
            /// Adds a new sentinel set to the list.
            /// </summary>
            /// <param name="sentinels">SentinelSet to be added to the list.</param>
            public void Add(SentinelSet sentinels)
            {
                m_SentinelSets.Add(sentinels);
            }

            /// <summary>
            /// Deletes a sentinel set from the list.
            /// </summary>
            /// <param name="sentinels">SentinelSet to be deleted from the list.</param>
            public void Delete(SentinelSet sentinels)
            {
                m_SentinelSets.Remove(sentinels);
            }

            /// <summary>
            /// Deletes all the sentinel sets from the list
            /// </summary>
            public void DeleteAll()
            {
                m_SentinelSets.Clear();
            }
        }

        /// <summary>
        /// This class provides access to a sentinel set.
        /// A sentinel set represents the start and stop sentinels of all three tracks.
        /// </summary>
        public class SentinelSet
        {
            /// <summary>
            /// Start sentinel of track1.
            /// </summary>
            public char Track1StartSentinel;

            /// <summary>
            /// Start sentinel of track2.
            /// </summary>
            public char Track2StartSentinel;

            /// <summary>
            /// Start sentinel of track3.
            /// </summary>
            public char Track3StartSentinel;

            /// <summary>
            /// End sentinel of track1.
            /// </summary>
            public char Track1EndSentinel;

            /// <summary>
            /// End sentinel of track2.
            /// </summary>
            public char Track2EndSentinel;

            /// <summary>
            /// End sentinel of track3.
            /// </summary>
            public char Track3EndSentinel;

            /// <summary>
            /// Constructor
            /// </summary>
            public SentinelSet()
            {

            }

            /// <summary>
            /// Overloaded constructor accepting the start and stop sentinels of all three tracks.
            /// </summary>
            /// <param name="track1StartSentinel"></param>
            /// <param name="track1EndSentinel"></param>
            /// <param name="track2StartSentinel"></param>
            /// <param name="track2EndSentinel"></param>
            /// <param name="track3StartSentinel"></param>
            /// <param name="track3EndSentinel"></param>
            public SentinelSet(char track1StartSentinel, char track1EndSentinel, char track2StartSentinel, char track2EndSentinel, char track3StartSentinel, char track3EndSentinel)
            {
                Track1StartSentinel = track1StartSentinel;
                Track2StartSentinel = track2StartSentinel;
                Track3StartSentinel = track3StartSentinel;

                Track1EndSentinel = track1EndSentinel;
                Track2EndSentinel = track2EndSentinel;
                Track3EndSentinel = track3EndSentinel;
            }
        }
	}
}
