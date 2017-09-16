//--------------------------------------------------------------------
// FILENAME: RCMForm.cs
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
using Symbol.ResourceCoordination;

namespace CS_ResCoordSample1
{
	/// <summary>
	/// CS_ResCoordSample1 Form class.
	/// </summary>
	public class RCMForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Button buttonAbout;
		private System.Windows.Forms.TabPage tabPageTrigger;
		private System.Windows.Forms.TabPage tabPageUUID;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPageConfig;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textBoxState;
		private System.Windows.Forms.TextBox textBoxUUID;
		private System.Windows.Forms.TextBox textBoxTrig;
		private System.Windows.Forms.TextBox textBoxTerm;
		private System.Windows.Forms.TextBox textBoxUART;
		private System.Windows.Forms.TextBox textBoxKB;
		private System.Windows.Forms.TextBox textBoxDisplay;
		private System.Windows.Forms.TextBox textBoxScanner;
		private System.Windows.Forms.ComboBox comboBoxStages;
		private System.Windows.Forms.ComboBox comboBoxTrigger;

		private Symbol.ResourceCoordination.Trigger trigger;
		private int triggerIndex = -1;
        private TextBox textBoxTriggerID;
        private Label label6;
		private Symbol.ResourceCoordination.TerminalInfo terminalInfo;

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

        private int resHeightReference = 217;  // The (cached) height of the form.
        // INITIALLY HAS TO BE SET TO THE HEIGHT OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private const double maxLength = 5.5;  // The maximum physical width/height of the sample (in inches).
        // The actual value on the device may slightly deviate from this
        // since the calculations based on the (received) DPI & resolution values 
        // would provide only an approximation, so not 100% accurate.


		public RCMForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// ensure that the keyboard focus is set to the about button.
			this.buttonAbout.Focus();
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
            this.tabPageTrigger = new System.Windows.Forms.TabPage();
            this.textBoxTriggerID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxTrigger = new System.Windows.Forms.ComboBox();
            this.comboBoxStages = new System.Windows.Forms.ComboBox();
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageUUID = new System.Windows.Forms.TabPage();
            this.textBoxUUID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.textBoxTrig = new System.Windows.Forms.TextBox();
            this.textBoxTerm = new System.Windows.Forms.TextBox();
            this.textBoxUART = new System.Windows.Forms.TextBox();
            this.textBoxKB = new System.Windows.Forms.TextBox();
            this.textBoxDisplay = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxScanner = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageTrigger.SuspendLayout();
            this.tabPageUUID.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageTrigger);
            this.tabControl1.Controls.Add(this.tabPageUUID);
            this.tabControl1.Controls.Add(this.tabPageConfig);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 152);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageTrigger
            // 
            this.tabPageTrigger.Controls.Add(this.textBoxTriggerID);
            this.tabPageTrigger.Controls.Add(this.label6);
            this.tabPageTrigger.Controls.Add(this.comboBoxTrigger);
            this.tabPageTrigger.Controls.Add(this.comboBoxStages);
            this.tabPageTrigger.Controls.Add(this.textBoxState);
            this.tabPageTrigger.Controls.Add(this.label3);
            this.tabPageTrigger.Controls.Add(this.label2);
            this.tabPageTrigger.Controls.Add(this.label1);
            this.tabPageTrigger.Location = new System.Drawing.Point(4, 25);
            this.tabPageTrigger.Name = "tabPageTrigger";
            this.tabPageTrigger.Size = new System.Drawing.Size(232, 123);
            this.tabPageTrigger.Text = "Trigger";
            // 
            // textBoxTriggerID
            // 
            this.textBoxTriggerID.Enabled = false;
            this.textBoxTriggerID.Location = new System.Drawing.Point(136, 90);
            this.textBoxTriggerID.Name = "textBoxTriggerID";
            this.textBoxTriggerID.ReadOnly = true;
            this.textBoxTriggerID.Size = new System.Drawing.Size(94, 23);
            this.textBoxTriggerID.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 24);
            this.label6.Text = "Active Trigger:";
            // 
            // comboBoxTrigger
            // 
            this.comboBoxTrigger.Location = new System.Drawing.Point(136, 8);
            this.comboBoxTrigger.Name = "comboBoxTrigger";
            this.comboBoxTrigger.Size = new System.Drawing.Size(96, 23);
            this.comboBoxTrigger.TabIndex = 0;
            this.comboBoxTrigger.SelectedIndexChanged += new System.EventHandler(this.comboBoxTrigger_SelectedIndexChanged);
            // 
            // comboBoxStages
            // 
            this.comboBoxStages.Location = new System.Drawing.Point(136, 37);
            this.comboBoxStages.Name = "comboBoxStages";
            this.comboBoxStages.Size = new System.Drawing.Size(96, 23);
            this.comboBoxStages.TabIndex = 1;
            // 
            // textBoxState
            // 
            this.textBoxState.Enabled = false;
            this.textBoxState.Location = new System.Drawing.Point(136, 66);
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.ReadOnly = true;
            this.textBoxState.Size = new System.Drawing.Size(94, 23);
            this.textBoxState.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 24);
            this.label3.Text = "Current State:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 24);
            this.label2.Text = "Available Stages:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.Text = "Select a Trigger:";
            // 
            // tabPageUUID
            // 
            this.tabPageUUID.Controls.Add(this.textBoxUUID);
            this.tabPageUUID.Controls.Add(this.label4);
            this.tabPageUUID.Location = new System.Drawing.Point(4, 25);
            this.tabPageUUID.Name = "tabPageUUID";
            this.tabPageUUID.Size = new System.Drawing.Size(232, 123);
            this.tabPageUUID.Text = "UUID";
            // 
            // textBoxUUID
            // 
            this.textBoxUUID.Location = new System.Drawing.Point(48, 48);
            this.textBoxUUID.Multiline = true;
            this.textBoxUUID.Name = "textBoxUUID";
            this.textBoxUUID.ReadOnly = true;
            this.textBoxUUID.Size = new System.Drawing.Size(144, 48);
            this.textBoxUUID.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(208, 16);
            this.label4.Text = "The Unique Unit ID of this device is:";
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.textBoxTrig);
            this.tabPageConfig.Controls.Add(this.textBoxTerm);
            this.tabPageConfig.Controls.Add(this.textBoxUART);
            this.tabPageConfig.Controls.Add(this.textBoxKB);
            this.tabPageConfig.Controls.Add(this.textBoxDisplay);
            this.tabPageConfig.Controls.Add(this.label12);
            this.tabPageConfig.Controls.Add(this.label11);
            this.tabPageConfig.Controls.Add(this.label10);
            this.tabPageConfig.Controls.Add(this.label9);
            this.tabPageConfig.Controls.Add(this.label8);
            this.tabPageConfig.Controls.Add(this.textBoxScanner);
            this.tabPageConfig.Controls.Add(this.label7);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 25);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Size = new System.Drawing.Size(232, 123);
            this.tabPageConfig.Text = "Config Data";
            // 
            // textBoxTrig
            // 
            this.textBoxTrig.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxTrig.Location = new System.Drawing.Point(176, 95);
            this.textBoxTrig.Name = "textBoxTrig";
            this.textBoxTrig.ReadOnly = true;
            this.textBoxTrig.Size = new System.Drawing.Size(48, 19);
            this.textBoxTrig.TabIndex = 5;
            // 
            // textBoxTerm
            // 
            this.textBoxTerm.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.textBoxTerm.Location = new System.Drawing.Point(56, 3);
            this.textBoxTerm.Name = "textBoxTerm";
            this.textBoxTerm.ReadOnly = true;
            this.textBoxTerm.Size = new System.Drawing.Size(168, 18);
            this.textBoxTerm.TabIndex = 0;
            // 
            // textBoxUART
            // 
            this.textBoxUART.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxUART.Location = new System.Drawing.Point(56, 95);
            this.textBoxUART.Name = "textBoxUART";
            this.textBoxUART.ReadOnly = true;
            this.textBoxUART.Size = new System.Drawing.Size(48, 19);
            this.textBoxUART.TabIndex = 4;
            // 
            // textBoxKB
            // 
            this.textBoxKB.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxKB.Location = new System.Drawing.Point(56, 72);
            this.textBoxKB.Name = "textBoxKB";
            this.textBoxKB.ReadOnly = true;
            this.textBoxKB.Size = new System.Drawing.Size(168, 19);
            this.textBoxKB.TabIndex = 3;
            // 
            // textBoxDisplay
            // 
            this.textBoxDisplay.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxDisplay.Location = new System.Drawing.Point(56, 49);
            this.textBoxDisplay.Name = "textBoxDisplay";
            this.textBoxDisplay.ReadOnly = true;
            this.textBoxDisplay.Size = new System.Drawing.Size(168, 19);
            this.textBoxDisplay.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(121, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 18);
            this.label12.Text = "Trigger";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(0, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 16);
            this.label11.Text = "Terminal";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(0, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 16);
            this.label10.Text = "UART";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(0, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 16);
            this.label9.Text = "Keyboard";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(0, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.Text = "Display";
            // 
            // textBoxScanner
            // 
            this.textBoxScanner.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxScanner.Location = new System.Drawing.Point(56, 26);
            this.textBoxScanner.Name = "textBoxScanner";
            this.textBoxScanner.ReadOnly = true;
            this.textBoxScanner.Size = new System.Drawing.Size(168, 19);
            this.textBoxScanner.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.Text = "Scanner";
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(144, 160);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(72, 24);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "Exit";
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            this.buttonExit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.buttonExit_KeyDown);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Location = new System.Drawing.Point(32, 160);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(72, 24);
            this.buttonAbout.TabIndex = 0;
            this.buttonAbout.Text = "About";
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            this.buttonAbout.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonAbout_KeyDown);
            // 
            // RCMForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 192);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.tabControl1);
            this.Name = "RCMForm";
            this.Text = "CS_ResCoordSample1";
            this.Load += new System.EventHandler(this.RCMForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RCMForm_KeyUp);
            this.Resize += new System.EventHandler(this.RCMForm_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPageTrigger.ResumeLayout(false);
            this.tabPageUUID.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
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
        private static void Scale(RCMForm frm)
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
            RCMForm rcmf = new RCMForm();
            rcmf.DoScale();
            Application.Run(rcmf);
		}

		/// <summary>
		/// Occurs before the form is displayed for the first time.
		/// </summary>
		private void RCMForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				terminalInfo = new Symbol.ResourceCoordination.TerminalInfo();

				// Initialize trigger tab page 
				foreach (Symbol.ResourceCoordination.TriggerDevice device
							 in Symbol.ResourceCoordination.TriggerDevice.AvailableTriggers)
					this.comboBoxTrigger.Items.Add(device.ID.ToString());
                this.comboBoxTrigger.Items.Add(Symbol.ResourceCoordination.TriggerID.ALL_TRIGGERS);

				if (this.comboBoxTrigger.Items.Count > 0)
				{
					this.comboBoxTrigger.SelectedIndex = 0;
				}
			
				// Initialize UUID tab page
				string uuid = "0x";
				
				if(terminalInfo.UniqueUnitID != null)
				{
					foreach (byte b in terminalInfo.UniqueUnitID)
						uuid += b.ToString("X2");
				}
				else
				{
					uuid = "UUID not set";
				}

				this.textBoxUUID.Text = uuid;

				if(terminalInfo.ConfigData != null)
				{
					// Initialize config data tab page
                    if (Enum.IsDefined(typeof(ScannerTypes), terminalInfo.ConfigData.SCANNER))
                    {
                        this.textBoxScanner.Text = ((ScannerTypes)terminalInfo.ConfigData.SCANNER).ToString();
                    }
                    else
                    {
                        this.textBoxScanner.Text = "Undefined: 0x" + terminalInfo.ConfigData.SCANNER.ToString("X");
                    }

                    if (Enum.IsDefined(typeof(DisplayTypes), terminalInfo.ConfigData.DISPLAY))
                    {
                        this.textBoxDisplay.Text = ((DisplayTypes)terminalInfo.ConfigData.DISPLAY).ToString();
                    }
                    else
                    {
                        this.textBoxDisplay.Text = "Undefined: 0x" + terminalInfo.ConfigData.DISPLAY.ToString("X");
                    }

                    if (Enum.IsDefined(typeof(KeyboardTypes), terminalInfo.ConfigData.KEYBOARD))
                    {
                        this.textBoxKB.Text = ((KeyboardTypes)terminalInfo.ConfigData.KEYBOARD).ToString();
                    }
                    else
                    {
                        this.textBoxKB.Text = "Undefined: 0x" + terminalInfo.ConfigData.KEYBOARD.ToString("X");
                    }

                    if (Enum.IsDefined(typeof(TerminalTypes), terminalInfo.ConfigData.TERMINAL))
                    {
                        this.textBoxTerm.Text = ((TerminalTypes)terminalInfo.ConfigData.TERMINAL).ToString();
                    }
                    else
                    {
                        this.textBoxTerm.Text = "Undefined: 0x" + terminalInfo.ConfigData.TERMINAL.ToString("X");
                    }

                    //this.textBoxScanner.Text = "0x" +
                    //    terminalInfo.ConfigData.SCANNER.ToString("X");
                    //this.textBoxDisplay.Text = "0x" +
                    //    terminalInfo.ConfigData.DISPLAY.ToString("X");
                    //this.textBoxKB.Text = "0x" +
                    //    terminalInfo.ConfigData.KEYBOARD.ToString("X");
					this.textBoxUART.Text = "0x" +
						terminalInfo.ConfigData.UART.ToString("X");
                    //this.textBoxTerm.Text = "0x" +
                    //    terminalInfo.ConfigData.TERMINAL.ToString("X");
					this.textBoxTrig.Text = "0x" +
						terminalInfo.ConfigData.TRIGGER.ToString("X");
				}
				else
				{
					//this.textBoxTrig.Text = "ConfigData not set";
                    this.textBoxTerm.Text = "ConfigData not available!";
				}
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message, this.Text);
				this.Close();
				return;
			}

			if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
			{
				this.Menu = new MainMenu();
			}
		}

		/// <summary>
		/// Display the About box when About button is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAbout_Click(object sender, System.EventArgs e)
		{
			string sVerInfo = "CS_ResCoordSample1	\t - v1.0.0\r\n";
			Symbol.ResourceCoordination.Version version
				= terminalInfo.ResCoordVersion;

			if(version != null)
			{
				sVerInfo += "Assembly Version	\t - v" + 
					version.AssemblyVersion + "\r\n" +
					"CAPI Version		\t - v" + 
					version.CAPIVersion + "\r\n" +
					"ResCoord Version	\t - v" +
					version.ResCoordVersion + "\r\n" +
					"UUID Version		\t - v" +
					version.UUIDVersion + "\r\n" + "\r\n";
			}
			else
			{
				sVerInfo += "Version not set";
			}
			
			MessageBox.Show(sVerInfo, "About");

			// After returning from the About box then reset the keyboard focus to the about button.
			this.buttonAbout.Focus();

		}

		// Handles when the user selects the about button from the keyboard.
		private void buttonAbout_KeyDown(object sender, KeyEventArgs e)
		{
			// Checks to see if the enter key (character code 13) was the key pressed.
			if (e.KeyValue == (char)13)
				buttonAbout_Click(this, e);
		}

		/// <summary>
		/// Release resources and close the window.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			if (trigger != null)
				trigger.Dispose();
			this.Close();
		}

		// Handles when the user selects the exit button from the keyboard.
		private void buttonExit_KeyDown(object sender, KeyEventArgs e)
		{
			// Checks to see if the enter key (character code 13) was the key pressed.
			if (e.KeyValue == (char)13)
				buttonExit_Click(this, e);
		}

        private void RCMForm_KeyUp(object sender, KeyEventArgs e)
        {
            this.buttonAbout.Focus();
        }

		/// <summary>
		/// Hanlde Stage1Notify event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void trigger_Stage1Notify(object sender, Symbol.ResourceCoordination.TriggerEventArgs e)
		{
			this.textBoxState.Text = e.NewState.ToString();
            this.textBoxTriggerID.Text = e.TriggerID.ToString();
		}

		/// <summary>
		/// Handle Stage2Notify event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void trigger_Stage2Notify(object sender, Symbol.ResourceCoordination.TriggerEventArgs e)
		{
			this.textBoxState.Text = e.NewState.ToString();
            this.textBoxTriggerID.Text = e.TriggerID.ToString();
		}

		/// <summary>
		/// Switch Trigger if necessary
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxTrigger_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (triggerIndex == this.comboBoxTrigger.SelectedIndex)
                return;

            triggerIndex = this.comboBoxTrigger.SelectedIndex;

            // Dispose the original trigger and create a new one.
            if (trigger != null)
                trigger.Dispose();

            if (triggerIndex == comboBoxTrigger.Items.Count - 1) //last item 
            { //Selected ALL_TRIGGERS
                trigger = new Symbol.ResourceCoordination.Trigger(
                    new Symbol.ResourceCoordination.TriggerDevice(
                    Symbol.ResourceCoordination.TriggerID.ALL_TRIGGERS, new ArrayList()));
                InitializeAllTriggers();
            }
            else
            {
                trigger = new Symbol.ResourceCoordination.Trigger(
                    Symbol.ResourceCoordination.TriggerDevice.AvailableTriggers[triggerIndex]);
                InitializeTrigger();
            }
		}

		/// <summary>
		/// Initialize UI control and event handlers of trigger.
		/// </summary>
		private void InitializeTrigger()
		{
			this.textBoxState.Text = trigger.State.ToString();

			this.comboBoxStages.Items.Clear();

			foreach (Symbol.ResourceCoordination.TriggerState stage
						 in trigger.AvailableStages)
				this.comboBoxStages.Items.Add(stage.ToString());
			this.comboBoxStages.SelectedIndex = 0;

			if (trigger.IsStage1Supported)
			{
				if (trigger.IsStage1InUse)
					MessageBox.Show("STAGE1 is exclusively registered by another application.", "Error");
				else
					trigger.Stage1Notify +=
						new Symbol.ResourceCoordination.Trigger.TriggerEventHandler(trigger_Stage1Notify);
			}
			if (trigger.IsStage2Supported)
			{
				if (trigger.IsStage2InUse)
					MessageBox.Show("STAGE2 is exclusively registered by another application.", "Error");
				else
					trigger.Stage2Notify +=
						new Symbol.ResourceCoordination.Trigger.TriggerEventHandler(trigger_Stage2Notify);
			}
		}

        /// <summary>
        /// Initialize UI control and event handlers for ALL_TRIGGERS.
        /// </summary>
        private void InitializeAllTriggers()
        {
            this.textBoxState.Text = trigger.State.ToString();
            this.comboBoxStages.Items.Clear();

            trigger.Stage1Notify +=
                new Symbol.ResourceCoordination.Trigger.TriggerEventHandler(trigger_Stage1Notify);
            trigger.Stage2Notify +=
                new Symbol.ResourceCoordination.Trigger.TriggerEventHandler(trigger_Stage2Notify);
        }

        private void RCMForm_Resize(object sender, EventArgs e)
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
