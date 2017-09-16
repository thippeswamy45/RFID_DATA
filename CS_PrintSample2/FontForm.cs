//--------------------------------------------------------------------
// FILENAME: FontForm.cs
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
using System.ComponentModel;
using System.Windows.Forms;

using Symbol.Printing;

namespace CS_PrintSample2
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class FontForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;	
		private System.Windows.Forms.ListBox FontsListBox;
		private System.Windows.Forms.ComboBox TextFontComboBox;
		private System.Windows.Forms.ComboBox AddrFontComboBox;
		private System.Windows.Forms.ComboBox TitleFontComboBox;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Button CancelButton;

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

        private int resHeightReference = 317;  // The (cached) height of the form.
        // INITIALLY HAS TO BE SET TO THE HEIGHT OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private const double maxLength = 5.5;  // The maximum physical width/height of the sample (in inches).
        // The actual value on the device may slightly deviate from this
        // since the calculations based on the (received) DPI & resolution values 
        // would provide only an approximation, so not 100% accurate.


		private SymbolFont[] availableFonts;
		private int indexTextFont;
		private int indexTitleFont;
		private int indexAddrFont;
		
		public SymbolFont TextFont	{ get { return availableFonts[indexTextFont]; } }

		public SymbolFont TitleFont { get { return availableFonts[indexTitleFont]; } }

		public SymbolFont AddressFont { get { return availableFonts[indexAddrFont]; } }
	
		public FontForm(SymbolFont[] fonts, SymbolFont textFont, SymbolFont titleFont,
							SymbolFont addrFont)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			availableFonts = fonts;
			if (fonts != null)
			{
				indexTextFont = Array.IndexOf(fonts, textFont, 0, fonts.Length);
				indexTitleFont = Array.IndexOf(fonts, titleFont, 0, fonts.Length);
				indexAddrFont = Array.IndexOf(fonts, addrFont, 0, fonts.Length);
			}
            this.OKButton.Focus();
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
            this.FontsListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TextFontComboBox = new System.Windows.Forms.ComboBox();
            this.AddrFontComboBox = new System.Windows.Forms.ComboBox();
            this.TitleFontComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // FontsListBox
            // 
            this.FontsListBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular);
            this.FontsListBox.Location = new System.Drawing.Point(16, 66);
            this.FontsListBox.Name = "FontsListBox";
            this.FontsListBox.Size = new System.Drawing.Size(208, 72);
            this.FontsListBox.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 24);
            this.label1.Text = "Available Fonts:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 24);
            this.label2.Text = "Text Font:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 24);
            this.label3.Text = "Address Font:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 24);
            this.label4.Text = "Title Font:";
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(32, 246);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(64, 25);
            this.OKButton.TabIndex = 7;
            this.OKButton.Text = "OK";
            this.OKButton.Click += new System.EventHandler(this.ButtonOK_Click);
            this.OKButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonOK_KeyDown);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(144, 246);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(64, 25);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.Click += new System.EventHandler(this.ButtonCancel_Click);
            this.CancelButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ButtonCancel_KeyDown);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 24);
            this.label5.Text = "Name";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(96, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 24);
            this.label6.Text = "Height";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(152, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 24);
            this.label7.Text = "Width";
            // 
            // TextFontComboBox
            // 
            this.TextFontComboBox.Location = new System.Drawing.Point(112, 149);
            this.TextFontComboBox.Name = "TextFontComboBox";
            this.TextFontComboBox.Size = new System.Drawing.Size(96, 23);
            this.TextFontComboBox.TabIndex = 2;
            // 
            // AddrFontComboBox
            // 
            this.AddrFontComboBox.Location = new System.Drawing.Point(112, 182);
            this.AddrFontComboBox.Name = "AddrFontComboBox";
            this.AddrFontComboBox.Size = new System.Drawing.Size(96, 23);
            this.AddrFontComboBox.TabIndex = 1;
            // 
            // TitleFontComboBox
            // 
            this.TitleFontComboBox.Location = new System.Drawing.Point(112, 213);
            this.TitleFontComboBox.Name = "TitleFontComboBox";
            this.TitleFontComboBox.Size = new System.Drawing.Size(96, 23);
            this.TitleFontComboBox.TabIndex = 0;
            // 
            // FontForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 292);
            this.Controls.Add(this.TitleFontComboBox);
            this.Controls.Add(this.AddrFontComboBox);
            this.Controls.Add(this.TextFontComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FontsListBox);
            this.Name = "FontForm";
            this.Text = "Font Setting";
            this.Load += new System.EventHandler(this.FontForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FontForm_KeyUp);
            this.Resize += new System.EventHandler(this.FontForm_Resize);
            this.ResumeLayout(false);

		}
		#endregion

		private void FontForm_Load(object sender, System.EventArgs e)
		{
			if (availableFonts == null)
				return;

			foreach (SymbolFont font in availableFonts)
			{
				string fontInfo = font.Name.PadRight(10) +  
									font.Height.ToString().PadLeft(4);
				if (font.Width == 0)
					fontInfo += " Proportional";
				else
					fontInfo += font.Width.ToString().PadLeft(8);

				this.FontsListBox.Items.Add(fontInfo);
				this.TextFontComboBox.Items.Add(font.Name);
				this.AddrFontComboBox.Items.Add(font.Name);
				this.TitleFontComboBox.Items.Add(font.Name);
			}

			TextFontComboBox.SelectedIndex = indexTextFont;
			TitleFontComboBox.SelectedIndex = indexTitleFont;
			AddrFontComboBox.SelectedIndex = indexAddrFont;
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
        private static void Scale(FontForm frm)
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
		private void ButtonOK_Click(object sender, System.EventArgs e)
		{
            try
            {
                if (TextFontComboBox.SelectedIndex == -1
                    || TitleFontComboBox.SelectedIndex == -1
                    || AddrFontComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select fonts for Text, Address and Title", "CS_PrintSample2",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button1);
                    return;
                }


                if (availableFonts[TextFontComboBox.SelectedIndex].Width == 0
                    || availableFonts[TitleFontComboBox.SelectedIndex].Width == 0
                    || availableFonts[AddrFontComboBox.SelectedIndex].Width == 0)
                {
                    MessageBox.Show("Please select non-proportional fonts", "CS_PrintSample2",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button1);
                    return;
                }
                indexTextFont = TextFontComboBox.SelectedIndex;
                indexTitleFont = TitleFontComboBox.SelectedIndex;
                indexAddrFont = AddrFontComboBox.SelectedIndex;

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
                DialogResult = DialogResult.Cancel;
            }
			
			this.Close();
		}

		private void ButtonCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;

			this.Close();
		}
        private void ButtonOK_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                ButtonOK_Click(this, e);
        }

        private void ButtonCancel_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                ButtonCancel_Click(this, e);
        }

        private void FontForm_KeyUp(object sender, KeyEventArgs e)
        {
            this.OKButton.Focus();
        }

        private void FontForm_Resize(object sender, EventArgs e)
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
