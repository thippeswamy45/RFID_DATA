//--------------------------------------------------------------------
// FILENAME: FormMain.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION:		This "sample" application provides examples to use 
//					the TAPI, SMS and Connection Manager features
//					on Symbol PocketPC device.  Refer to the EULA.rtf
//					file found in this folder for important messages.
//
// NOTES:			Refer to the readme.txt file for a description of how 
//					to use these files to create a WAN application.
//
// 
//--------------------------------------------------------------------

//------------------------------------------------------------------------------------
//		I M P O R T A N T   D I S C L A I M E R
//
// This Software comes "as is", with no warranties. None whatsoever. This means no 
// express, implied or statutory warranty, including without limitation, warranties 
// of merchantability or fitness for a particular purpose or any warranty of title 
// or non-infringement. Also, you must pass this disclaimer on whenever you 
// distribute the Software or derivative works. 

// Neither Symbol nor any contributor to the Software will be liable for any of 
// those types of damages known as indirect, special, consequential, or incidental 
// related to the Software or this license, to the maximum extent the law permits, 
// no matter what legal theory it’s based on. Also, you must pass this limitation of 
// liability on whenever you distribute the Software or derivative works. 
//------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace WANSampleTest
{
	public partial class FormMain : Form
	{
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

        private int resWidthReference = 198;   // The (cached) width of the form. 
        // INITIALLY HAS TO BE SET TO THE WIDTH OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private int resHeightReference = 254;  // The (cached) height of the form.
        // INITIALLY HAS TO BE SET TO THE HEIGHT OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private const double maxLength = 5.5;  // The maximum physical width/height of the sample (in inches).
        // The actual value on the device may slightly deviate from this
        // since the calculations based on the (received) DPI & resolution values 
        // would provide only an approximation, so not 100% accurate.

		private static WANSample.Common myCommon;
		private static WANSample.Tapi myTapi;
		private static WANSample.Sms mySms;
		private static WANSample.Conn myConn;
		private static WANSample.SmsRead mySmsReader = null;

		public FormMain()
		{
			InitializeComponent();
			
			if (this.listViewSettings.Font.Size != 7)
            {
                this.listViewSettings.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            }

            this.buttonAbout.Focus();
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
        private static void Scale(FormMain frm)
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

		private void FormMain_Load(object sender, EventArgs e)
		{
            MessageBox.Show("This sample is provided for demonstration purpose only. You are welcome to modify the code to suit your requirement.\n Please refer to the MSDN help files for description of all the WAN related calls.", "WAN_Sample");

			myTapi = new WANSample.Tapi();
			mySms = new WANSample.Sms();
			myCommon = new WANSample.Common();
			myConn = new WANSample.Conn();

			myTapi.tapiMessageEvent += new EventHandler(onTapiMessageEvent);
			if (myTapi.TAPI_Open() == 0)
				buttonDial.Enabled = true;
			
			checkBoxEnableRadio.Checked = myTapi.TAPI_IsRadioEnabled();
			PopulateSettings(null, null);
			timer1.Enabled = true;

            try
            {

                mySmsReader = new WANSample.SmsRead();
                mySmsReader.SMS_OpenRead();
                mySmsReader.smsMessageEvent += new EventHandler(onSmsMessageEvent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

		}

		private void buttonExit_Click(object sender, EventArgs e)
		{
            timer1.Enabled = false;

			myTapi.tapiMessageEvent -= null;
			myTapi.TAPI_Close();

            try
            {

                if (mySmsReader != null)
                {
                    mySmsReader.smsMessageEvent -= null;
                    mySmsReader.SMS_CloseRead();
                }
            }
            catch { }

            this.Close();
		}

        private void buttonExit_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                buttonExit_Click(this, e);
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            this.buttonAbout.Focus();
        }

		private void onTapiMessageEvent(object sender, EventArgs e)
		{			
			this.Invoke(new EventHandler(UpdatePhoneStatus));
		}

		private void UpdatePhoneStatus(object sender, EventArgs e)
		{
			listBoxPhoneStatus.Items.Add(myTapi.lineCurrentDevice.lineClassState.ToString());

			// Incoming call
			if (myTapi.lineCurrentDevice.lineClassState == WANSample.Tapi.LINECALLSTATE.LINECALLSTATE_OFFERING)
			{
				listBoxPhoneStatus.Items.Clear();
                listBoxPhoneStatus.Items.Add(CS_WANSample.Resources.incomingPhoneCall);
                listBoxPhoneStatus.Items.Add(CS_WANSample.Resources.clickAnswerToRepsond);
				FlashStatusBar(CS_WANSample.Resources.incomingPhoneCall, 8);
				buttonAnswer.Enabled = true;
			}
			else
			if (myTapi.lineCurrentDevice.lineClassState == WANSample.Tapi.LINECALLSTATE.LINECALLSTATE_DISCONNECTED)
			{
				buttonHangup_Click(null, null);
				buttonAnswer.Enabled = false;
			}

			Application.DoEvents();
		}

		private void onSmsMessageEvent(object sender, EventArgs e)
		{
			this.Invoke(new EventHandler(UpdateSmsStatus));
		}

		private void UpdateSmsStatus(object sender, EventArgs e)
		{
			listBoxSmsStatus.Items.Clear();
			listBoxSmsStatus.Items.Add(CS_WANSample.Resources.youHaveMessage);
			listBoxSmsStatus.Items.Add(CS_WANSample.Resources.from + mySmsReader.smsRdCurrentDevice.inPhone);
			listBoxSmsStatus.Items.Add(CS_WANSample.Resources.msg + mySmsReader.smsRdCurrentDevice.inMsg);
			
			FlashStatusBar(CS_WANSample.Resources.youHaveSMSMessage, 8);
		}

		private void FlashStatusBar(String msg, int times)
		{
			statusBar1.Text = msg;
			for (int i = 0; i < times; i++)
			{
				if (statusBar1.Visible == true) statusBar1.Visible = false;
				else statusBar1.Visible = true;
				Application.DoEvents();
				System.Threading.Thread.Sleep(500);
			}

			statusBar1.Visible = true;
		}

		private void checkBoxConn_Click(object sender, EventArgs e)
		{
			uint uRet = 0;

			if (checkBoxConn.Checked == true)
			{
				listBoxDataStatus.Items.Clear();
				listBoxDataStatus.Items.Add(CS_WANSample.Resources.attempting);
				Application.DoEvents();

				uRet = myConn.CONN_Connect();

				if (uRet == (uint)WANSample.Conn.CONNECTIONSTATUS.CONNMGR_STATUS_CONNECTED)
					listBoxDataStatus.Items.Add(CS_WANSample.Resources.connected);
				else
				{ 
					listBoxDataStatus.Items.Add(CS_WANSample.Resources.failed + (WANSample.Conn.CONNECTIONSTATUS)uRet);
					checkBoxConn.Checked = false;
					Application.DoEvents();
				}
			}
			else
			{
				myConn.CONN_Disconnect();
				listBoxDataStatus.Items.Add(CS_WANSample.Resources.disconnected);
                MessageBox.Show(CS_WANSample.Resources.toPhysicallyDrop);
			}
		}

        private void checkBoxConn_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
            {
                checkBoxConn.Checked = !(checkBoxConn.Checked);
                checkBoxConn_Click(this, e);

            }

        }
        
		private void buttonDial_Click(object sender, EventArgs e)
		{
			uint uRet = 0;
			listBoxPhoneStatus.Items.Clear();

			if (myTapi.TAPI_IsRadioEnabled() == false)
				MessageBox.Show(CS_WANSample.Resources.radioMustBeEnabled);
			else
				if (textBoxPhNumber.Text.Length > 0)
				{
					listBoxPhoneStatus.Items.Add(CS_WANSample.Resources.dialing);
					Application.DoEvents();

					uRet = myTapi.TAPI_MakeCall(textBoxPhNumber.Text);
					if (uRet == (uint)WANSample.Tapi.LINECALLRETURN.LINEERR_OK)
					{
						buttonDial.Enabled = false;
						buttonHangup.Enabled = true;
					}
				}
		}
        private void buttonDial_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                buttonDial_Click(this, e);
        }

		private void buttonHangup_Click(object sender, EventArgs e)
		{
			myTapi.TAPI_DropCall();
			buttonDial.Enabled = true;
			buttonHangup.Enabled = false;
		}

        private void buttonHangup_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                buttonHangup_Click(this, e);
        }

		private void buttonSendSms_Click(object sender, EventArgs e)
		{
			listBoxSmsStatus.Items.Clear();
			listBoxSmsStatus.Items.Add(CS_WANSample.Resources.attempting);
			Application.DoEvents();

			mySms.SMS_Open();

			uint uRet = (uint)mySms.SMS_SendMessage(textBoxAddress.Text, textBoxMsg.Text);
			if (uRet == (uint)WANSample.Sms.SMSCALLRETURN.SMS_OK)
				listBoxSmsStatus.Items.Add(CS_WANSample.Resources.messageSent);
			else
				listBoxSmsStatus.Items.Add(CS_WANSample.Resources.failed + (WANSample.Sms.SMSCALLRETURN)uRet);

			mySms.SMS_Close();
		}
        private void buttonSendSms_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                buttonSendSms_Click(this, e);
        }

		private void checkBoxEnableRadio_Click(object sender, EventArgs e)
		{

            FormMsg fMsg = new FormMsg(CS_WANSample.Resources.pleaseWaitRadioSet);  // Creates the form object

            fMsg.DoScale();
           
			fMsg.Show(); 
			Application.DoEvents();

			uint ret = 0;
			if ((ret = myTapi.TAPI_SetRadioState(checkBoxEnableRadio.Checked)) !=
				(uint)WANSample.Tapi.LINECALLRETURN.LINEERR_OK)
			{
				checkBoxEnableRadio.Checked = !checkBoxEnableRadio.Checked;
				statusBar1.Text = CS_WANSample.Resources.failedToSetRadio;
			}

			PopulateSettings(null, null);
			fMsg.Close();
		}

        private void checkBoxEnableRadio_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
            {
                checkBoxEnableRadio.Checked = !(checkBoxEnableRadio.Checked);
                checkBoxEnableRadio_Click(this, e);

            }

        }

		private void PopulateSettings(object sender, EventArgs e)
		{
			listViewSettings.Items.Clear();

			ListViewItem lv1 = new ListViewItem("System Type");
			listViewSettings.Items.Add(lv1);

			// GSM or CDMA?
            uint lsType = myTapi.TAPI_GetSystemType();
            if (lsType == (uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_IDEN)// if (lsType == 0)
            {
                lv1.SubItems.Add("iDEN");
            }

            else if (((lsType & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_IS95A)) != 0)
                         || ((lsType & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_IS95B)) != 0)
                         || ((lsType & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_1XRTTPACKET)) != 0)
                         || ((lsType & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_1XEVDOPACKET)) != 0)
                         || ((lsType & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_1XEVDVPACKET)) != 0))
            {
                lv1.SubItems.Add("CDMA");
            }

            else if (((lsType & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_GSM)) != 0)
                        || ((lsType & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_GPRS)) != 0)
                        || ((lsType & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_EDGE)) != 0)
                        || ((lsType & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_UMTS)) != 0)
                        || ((lsType & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_HSDPA)) != 0))
            {
                lv1.SubItems.Add("GSM");
            }

            else
            {
                lv1.SubItems.Add("Unknown!");
            }



			// Signal Strength
			ListViewItem lv2 = new ListViewItem("Signal Strength");
			listViewSettings.Items.Add(lv2);
			lv2.SubItems.Add(myTapi.TAPI_GetSignalStrength().ToString() + " %");

			// Network Status
			ListViewItem lv3 = new ListViewItem("Network Status");
			listViewSettings.Items.Add(lv3);
			lv3.SubItems.Add(myTapi.TAPI_GetNetworkStatus().ToString());

			// Network Operator
			ListViewItem lv4 = new ListViewItem("Network Operator");
			listViewSettings.Items.Add(lv4);
			lv4.SubItems.Add(myTapi.TAPI_GetNetworkOperator());

			// Manufacturer
			myTapi.TAPI_GetGeneralInfo();
			ListViewItem lv5 = new ListViewItem("Radio Mfg");
			listViewSettings.Items.Add(lv5);
			lv5.SubItems.Add(myTapi.lineGeneralInfo.manufacturer);
			// Revision
			ListViewItem lv6 = new ListViewItem("Radio FW");
			listViewSettings.Items.Add(lv6);
			lv6.SubItems.Add(myTapi.lineGeneralInfo.revision);
			// Model
			ListViewItem lv7 = new ListViewItem("Radio Model");
			listViewSettings.Items.Add(lv7);
			lv7.SubItems.Add(myTapi.lineGeneralInfo.model);
			// IMEI
			ListViewItem lv8 = new ListViewItem("IMEI");
			listViewSettings.Items.Add(lv8);
			lv8.SubItems.Add(myTapi.lineGeneralInfo.IMEI);
			// IMSI or SIMId
            ListViewItem lv9;
            if (lsType == (uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_IDEN)
    			lv9 = new ListViewItem("SIM ID");
            else
       			lv9 = new ListViewItem("IMSI");
			listViewSettings.Items.Add(lv9);
			lv9.SubItems.Add(myTapi.lineGeneralInfo.IMSIorSIMId);
            // SIM Status
			ListViewItem lv10 = new ListViewItem("SIM Status");
			listViewSettings.Items.Add(lv10);
			lv10.SubItems.Add(myTapi.lineGeneralInfo.SIMStatus);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (tabControl1.SelectedIndex == 0)
			{
				timer1.Enabled = false;
				PopulateSettings(null, null);
				timer1.Enabled = true;
			}
		}

		private void buttonAbout_Click(object sender, EventArgs e)
		{
			string sVerInfo = "CS_WANSample - C# WANSample";

			Symbol.StandardForms.About.Run("CS_WANSample", sVerInfo);

            this.buttonAbout.Focus();
		}
        private void buttonAbout_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                buttonAbout_Click(this, e);
        }

        private void buttonAnswer_Click(object sender, EventArgs e)
        {

        }
        private void buttonAnswer_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                buttonAnswer_Click(this, e);
		}

        private void FormMain_Resize(object sender, EventArgs e)
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