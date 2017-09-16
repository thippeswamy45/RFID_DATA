//--------------------------------------------------------------------
// FILENAME: MainForm.cs
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
using System.Reflection;

using Symbol.Printing;

namespace CS_PrintSample2
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.MenuItem FileMenu;
		private System.Windows.Forms.MenuItem PrintMenu;
		private System.Windows.Forms.MenuItem ExitMenu;
		private System.Windows.Forms.MenuItem SettingsMenu;
		private System.Windows.Forms.MenuItem PrinterMenu;
		private System.Windows.Forms.MenuItem FontMenu;
        private System.Windows.Forms.MenuItem AboutMenu;
		private System.Windows.Forms.Label PrinterLabel;
		private System.Windows.Forms.Label WidthLabel;
		private System.Windows.Forms.Label HeightLabel;
		private System.Windows.Forms.Label TextLabel;
		private System.Windows.Forms.Label TitleLabel;
		private System.Windows.Forms.Label AddrLabel;

		private System.IO.Stream stream;
		private Assembly assembly;

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


		private PrinterSelectionDlg prnSelectDlg;
		private PrintDocument		prnDoc;
		private int					totalPages, pageNumber;
		private Bitmap				logo;
		private int					pageWidth, leftMargin, rightMargin;
		private SymbolFont			textFont, titleFont, addrFont;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.FileMenu = new System.Windows.Forms.MenuItem();
            this.PrintMenu = new System.Windows.Forms.MenuItem();
            this.ExitMenu = new System.Windows.Forms.MenuItem();
            this.SettingsMenu = new System.Windows.Forms.MenuItem();
            this.PrinterMenu = new System.Windows.Forms.MenuItem();
            this.FontMenu = new System.Windows.Forms.MenuItem();
            this.AboutMenu = new System.Windows.Forms.MenuItem();
            this.PrinterLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.WidthLabel = new System.Windows.Forms.Label();
            this.HeightLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TextLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.AddrLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.FileMenu);
            this.mainMenu1.MenuItems.Add(this.SettingsMenu);
            this.mainMenu1.MenuItems.Add(this.AboutMenu);
            // 
            // FileMenu
            // 
            this.FileMenu.MenuItems.Add(this.PrintMenu);
            this.FileMenu.MenuItems.Add(this.ExitMenu);
            this.FileMenu.Text = "File";
            // 
            // PrintMenu
            // 
            this.PrintMenu.Text = "Print";
            this.PrintMenu.Click += new System.EventHandler(this.MenuPrint_Click);
            // 
            // ExitMenu
            // 
            this.ExitMenu.Text = "Exit";
            this.ExitMenu.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // SettingsMenu
            // 
            this.SettingsMenu.MenuItems.Add(this.PrinterMenu);
            this.SettingsMenu.MenuItems.Add(this.FontMenu);
            this.SettingsMenu.Text = "Settings";
            // 
            // PrinterMenu
            // 
            this.PrinterMenu.Checked = true;
            this.PrinterMenu.Text = "Printer";
            this.PrinterMenu.Click += new System.EventHandler(this.MenuPrinter_Click);
            // 
            // FontMenu
            // 
            this.FontMenu.Text = "Font";
            this.FontMenu.Click += new System.EventHandler(this.MenuFont_Click);
            // 
            // AboutMenu
            // 
            this.AboutMenu.Text = "About";
            this.AboutMenu.Click += new System.EventHandler(this.MenuAbout_Click);
            // 
            // PrinterLabel
            // 
            this.PrinterLabel.Location = new System.Drawing.Point(112, 55);
            this.PrinterLabel.Name = "PrinterLabel";
            this.PrinterLabel.Size = new System.Drawing.Size(120, 24);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 24);
            this.label1.Text = "Page Size:";
            // 
            // WidthLabel
            // 
            this.WidthLabel.Location = new System.Drawing.Point(112, 88);
            this.WidthLabel.Name = "WidthLabel";
            this.WidthLabel.Size = new System.Drawing.Size(120, 24);
            this.WidthLabel.Text = "Width:";
            // 
            // HeightLabel
            // 
            this.HeightLabel.Location = new System.Drawing.Point(112, 121);
            this.HeightLabel.Name = "HeightLabel";
            this.HeightLabel.Size = new System.Drawing.Size(120, 24);
            this.HeightLabel.Text = "Height:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 24);
            this.label4.Text = "Selected Fonts:";
            // 
            // TextLabel
            // 
            this.TextLabel.Location = new System.Drawing.Point(112, 156);
            this.TextLabel.Name = "TextLabel";
            this.TextLabel.Size = new System.Drawing.Size(120, 24);
            this.TextLabel.Text = "Text:";
            // 
            // TitleLabel
            // 
            this.TitleLabel.Location = new System.Drawing.Point(112, 189);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(120, 24);
            this.TitleLabel.Text = "Title:";
            // 
            // AddrLabel
            // 
            this.AddrLabel.Location = new System.Drawing.Point(112, 223);
            this.AddrLabel.Name = "AddrLabel";
            this.AddrLabel.Size = new System.Drawing.Size(120, 24);
            this.AddrLabel.Text = "Addr:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 24);
            this.label2.Text = "Printer Name:";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 270);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AddrLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.TextLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.HeightLabel);
            this.Controls.Add(this.WidthLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PrinterLabel);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "CS_PrintSample2";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
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
        private static void Scale(MainForm frm)
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
            MainForm mf = new MainForm();
            mf.DoScale();
            Application.Run(mf);
        }

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			prnSelectDlg = new PrinterSelectionDlg();
            try
            {
			prnDoc = new PrintDocument();
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Printer driver not found", "CS_PrinterSample2");
                this.Close();
                return;
            }

			// Show printer selection dialog 
			if (prnSelectDlg.ShowDialog() != DialogResult.OK)
			{
				this.Close();
				return;
			}

			// Set PrintDocument to selected printer.
			if (prnDoc.PrinterSettings.PrinterName != prnSelectDlg.PrinterName)
				prnDoc.PrinterSettings.PrinterName = prnSelectDlg.PrinterName;
				
			textFont = prnDoc.PrinterSettings.DefaultFont;
			titleFont = prnDoc.PrinterSettings.DefaultFont;
			addrFont = prnDoc.PrinterSettings.DefaultFont;

			UpdateLabels();

			prnDoc.DocumentName = "CS_PrintSample2";
			prnDoc.PrintPage += new PrintPageEventHandler(OnPrintPage);

			// Get bitmap from resource
			assembly = Assembly.GetExecutingAssembly();
			stream = assembly.GetManifestResourceStream("CS_PrintSample2.CoffeeShop.bmp");
			logo = new Bitmap(stream);
		}

		private void OnPrintPage(object obj, PrintPageEventArgs ppea)
		{
			pageWidth = prnDoc.PrinterSettings.PrinterResolutions[0].X;
			if (pageWidth < 384)
				pageWidth = 384;

			leftMargin = 10;
			rightMargin = 15;

			if (pageNumber == 1)
				PrintSampleReceipt(ppea.SymbolGraphics);
			else
				PrintRebateForm(ppea.SymbolGraphics);
			
			ppea.HasMorePages = pageNumber < totalPages;
			pageNumber++;
		}

		private void PrintSampleReceipt(SymbolGraphics graphics)
		{
			SymbolPen	pen = new SymbolPen(Color.Black, 2);
			SolidBrush	brush = new SolidBrush(Color.Black);
			Rectangle	textRect = Rectangle.Empty;
			int		x, y;
			string[] items = {	"1 lb. Mocha Java", 
								"5 lb. Kona Special II",
								"4 lb. Frech Roast Decaf" };
			float[] prices = { 9.50f, 125.95f, 99.95f };
			int		numberOfItems = 3;
			string	text;
			string	receiptId = "09876543210";

			// Print bitmap
			y = 50;
			if (pageWidth >= logo.Width)
			{
				x = (pageWidth - logo.Width)/2 + 1;
				try
				{	// Try to use DrawBitmap because it's faster
					// Make sure that the Coffee.bmp is the same bitmap we have in the resource
					graphics.DrawBitmap("\\Application\\CoffeeShop.bmp", x, y, logo.Width, logo.Height);
				}
				catch 
				{	// Use DrawImage if DrawBitmap failed - it may not be supported on the target device.
					graphics.DrawImage(logo, x, y);
				}
				y += logo.Height + 20;
			}

			// Print the shop address using center alignment
			text = "The Symbol Plaza Coffee Shop";		// address line 1
			GetCenterRect(addrFont, text, y, ref textRect);
			graphics.DrawString(text, addrFont, brush, textRect);
			y += (int)addrFont.Height + 5;
			text = "One Symbol Plaza";					// address line 2
			GetCenterRect(addrFont, text, y, ref textRect);
			graphics.DrawString(text, addrFont, brush, textRect);
			y += (int)addrFont.Height + 5;
			text = "Holtsville, NY 11742";				// address line 3
			GetCenterRect(addrFont, text, y, ref textRect);
			graphics.DrawString(text, addrFont, brush, textRect);
			y += (int)addrFont.Height + 5;

			// Print Store id, date and time
			text = "Store #001";						// store ID
			y += (int)addrFont.Height + 5;
			GetLeftRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			text = DateTime.Now.ToShortDateString();	// date
			GetCenterRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			text = DateTime.Now.ToShortTimeString();	// time
			GetRightRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			y += (int)textFont.Height + 5;
			
			//
			// Print the item/price list
			//
			y += (int)textFont.Height + 5;
			int startY = y;
			leftMargin += 10;
			rightMargin += 10;
			
			text = "Item";						// column header
			GetLeftRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			text = "Price";
			GetRightRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			y += (int)textFont.Height + 5;
			graphics.DrawLine(pen, leftMargin-10, y, pageWidth-rightMargin+10, y);
			y += pen.Thickness + 5;

			float total = 0.0f;					// columns
			for (int i=0; i<numberOfItems; i++)
			{
				GetLeftRect(textFont, items[i], y, ref textRect);
				graphics.DrawString(items[i], textFont, brush, textRect);
				GetRightRect(textFont, prices[i].ToString("c"), y, ref textRect);
				graphics.DrawString(prices[i].ToString("c"), textFont, brush, textRect);
				y += (int)textFont.Height + 5;
				total += prices[i];
			}

			// Print rectangle and vertical line
			Point[] points = new Point[4];
			points[0].X = leftMargin - 10;				points[0].Y = startY - 5;
			points[1].X = pageWidth - rightMargin + 10;	points[1].Y = startY - 5;
			points[2].X = pageWidth - rightMargin + 10;	points[3].Y = y;
			points[3].X = leftMargin - 10;				points[2].Y = y;
			graphics.DrawPolygon(pen, points);
			int priceColWidth = 10 * (int)textFont.Width;
			graphics.DrawLine(pen, pageWidth - rightMargin - priceColWidth, startY,
									pageWidth - rightMargin - priceColWidth, y);
			y += pen.Thickness;
			
			// Simulate bold font
			y += (int)textFont.Height;
			text = "Total";						
			GetLeftRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			textRect.X += 2; textRect.Y -= 1;
			graphics.DrawString(text, textFont, brush, textRect);
			text = total.ToString("c");			
			GetRightRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			textRect.X += 2; textRect.Y -= 1;
			graphics.DrawString(text, textFont, brush, textRect);
			y += (int)textFont.Height + 5;
			leftMargin -= 10;
			rightMargin -= 10;

			// Print barcode
			y += 50;
			text = "ID# " + receiptId;				
			GetCenterRect(textFont, text, y, ref textRect);
			y += textRect.Height + 5;
			
			Rectangle barcodeRect = new Rectangle(1, y, 100, 50);
			BarCodeInfo bci = new BarCodeInfo();
			bci.NarrowBarWidth = 1;
			bci.RatioWideToNarrow = 2;
			bci.NumberOfColumns = 1;
		
			graphics.DrawBarCode(BarCodeTypes.CODE39, bci, receiptId, barcodeRect,
				text, textRect);

		}

		private void PrintRebateForm(SymbolGraphics graphics)
		{
			SymbolPen pen = new SymbolPen(Color.Black, 2);
			SolidBrush brush = new SolidBrush(Color.Black);
			Rectangle textRect = Rectangle.Empty;
			int x, y;
			string text;

			leftMargin = 20;
			rightMargin = 20;
			y = 50;

			text = "*** Rebate Request Form ***";		
			GetCenterRect(titleFont, text, y, ref textRect);
			graphics.DrawString(text, titleFont, brush, textRect);
			y += (int)titleFont.Height + 5;

			y += 50;
			text = "5 lb. Kona Special II";
			GetLeftRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			text = "$30.00";
			GetRightRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			y += (int)textFont.Height + 5;

			y += 50;
			text = "Name:";
			GetLeftRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			y += (int)textFont.Height + 5;
			graphics.DrawLine(pen, textRect.Right, y, pageWidth-rightMargin, y);
			y += pen.Thickness + 2;

			y += 50;
			text = "Address:";
			GetLeftRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			y += (int)textFont.Height + 5;
			x = textRect.Right;
			for (int i=0; i<3; i++)
			{
				graphics.DrawLine(pen, x, y, pageWidth-rightMargin, y);
				y += pen.Thickness + (int)textFont.Height + 10;
			}

			y += 50;
			text = "Send rebate Request to:";
			GetCenterRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			y += (int)textFont.Height + 5;

			y += 30;
			text = "The Symbol Plaza Coffee Shop";
			GetCenterRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			y += (int)textFont.Height + 5;
			text = "One Symbol Plaza";
			GetCenterRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			y += (int)textFont.Height + 5;
			text = "Holtsville, NY 11742";
			GetCenterRect(textFont, text, y, ref textRect);
			graphics.DrawString(text, textFont, brush, textRect);
			y += (int)textFont.Height + 5;

		}

		private void MenuPrint_Click(object sender, System.EventArgs e)
		{
			if (textFont == null || titleFont == null || addrFont == null)
			{
				MessageBox.Show("Please select proper fonts.", "Print Failed");
				return;
			}

            Cursor savedCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

			totalPages = 2;
			pageNumber = 1;		

			try 
			{
				if (prnDoc.PrinterSettings.PaperSizes.Count > 0)
					prnDoc.PaperSize = prnDoc.PrinterSettings.PaperSizes[0];
				else
					throw (new Exception("Unknown paper size!"));
	
				prnDoc.Print();
			}
			catch (Exception printException)
			{
				MessageBox.Show(printException.Message, "Print Failed");
			}

            Cursor.Current = savedCursor;
		}

		private void MenuExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void MenuFont_Click(object sender, System.EventArgs e)
		{
			try 
			{
                SymbolFont[] fonts = prnDoc.PrinterSettings.SupportedFonts;
                FontForm FontSetting = new FontForm(fonts, textFont, titleFont, addrFont); // Creates the form object

                FontSetting.DoScale();

				if (FontSetting.ShowDialog() == DialogResult.OK)
				{
					textFont = FontSetting.TextFont;
					titleFont = FontSetting.TitleFont;
					addrFont = FontSetting.AddressFont;
					UpdateLabels();
				}

				FontSetting.Dispose();
			}
			catch (Exception error)
			{
				MessageBox.Show(error.Message, "Exception");
			}
		}

		private void MenuAbout_Click(object sender, System.EventArgs e)
		{
            string sVerInfo = "CS_PrintSample2 \t- v1.1.1.4\r\n" +
                "Class Library Version \t- v" +
                prnDoc.PrinterSettings.Versions.AssemblyVersion + "\r\n" +
                "CAPI Version \t- v" +
                prnDoc.PrinterSettings.Versions.CAPIVersion + "\r\n" +
                "MDD Version \t- v" +
                prnDoc.PrinterSettings.Versions.MDDVersion + "\r\n" +
                "PDD Version \t- v" +
                prnDoc.PrinterSettings.Versions.PDDVersion;

			Symbol.StandardForms.About.Run(null, sVerInfo);
		}

		private void MenuPrinter_Click(object sender, System.EventArgs e)
		{
			// Show printer selection dialog box 
			prnSelectDlg.SelectPrinterName = prnDoc.PrinterSettings.PrinterName;
            
			if (prnSelectDlg.ShowDialog() != DialogResult.OK)
				return;

			// Set PrintDocument to selected printer.
			if (prnDoc.PrinterSettings.PrinterName != prnSelectDlg.PrinterName)
			{
				prnDoc.PrinterSettings.PrinterName = prnSelectDlg.PrinterName;
				textFont = prnDoc.PrinterSettings.DefaultFont;
				titleFont = prnDoc.PrinterSettings.DefaultFont;
				addrFont = prnDoc.PrinterSettings.DefaultFont;

				UpdateLabels();
			}
		}

		private void GetCenterRect(SymbolFont font, string text, int y, ref Rectangle rect)
		{
			int textWidth = (int)(text.Length*font.Width);
			if ( textWidth > pageWidth - leftMargin - rightMargin)
				throw (new Exception("Out of page range"));

			rect.X = (pageWidth - leftMargin - rightMargin - textWidth)/2 + leftMargin;
			rect.Y = y;
			rect.Width = textWidth;
			rect.Height = (int)font.Height;
		}

		private void GetLeftRect(SymbolFont font, string text, int y, ref Rectangle rect)
		{
			int textWidth = (int)(text.Length*font.Width);
			if ( textWidth > pageWidth - leftMargin - rightMargin)
				throw (new Exception("Out of page range"));

			rect.X = leftMargin;
			rect.Y = y;
			rect.Width = textWidth;
			rect.Height = (int)font.Height;
		}

		private void GetRightRect(SymbolFont font, string text, int y, ref Rectangle rect)
		{
			int textWidth = (int)(text.Length*font.Width);
			if ( textWidth > pageWidth - leftMargin - rightMargin)
				throw (new Exception("Out of page range"));

			rect.X = pageWidth - rightMargin - textWidth;
			rect.Y = y;
			rect.Width = textWidth;
			rect.Height = (int)font.Height;
		}

		private void UpdateLabels()
		{
			// Update labels on the main window

			PrinterLabel.Text = prnDoc.PrinterSettings.PrinterName;

			int height = 0, width = 0;
			if (prnDoc.PrinterSettings.PrinterResolutions != null 
					&& prnDoc.PrinterSettings.PrinterResolutions.Count > 0)
			{
				height = prnDoc.PrinterSettings.PrinterResolutions[0].Y;
				width = prnDoc.PrinterSettings.PrinterResolutions[0].X;
			}
			this.WidthLabel.Text = "Width: " + width.ToString() + " (pixels)";
			if (height == 0 && width != 0)
				this.HeightLabel.Text = "Height: continuous"; 
			else
				this.HeightLabel.Text = "Height: " + height.ToString() + " (pixels)";

			if (this.textFont != null)
				this.TextLabel.Text = "Text: " + this.textFont.Name;
			if (this.titleFont != null)
				this.TitleLabel.Text = "Title: " + this.titleFont.Name;
			if (this.addrFont != null)
				this.AddrLabel.Text = "Addr: " + this.addrFont.Name;

			this.Refresh();
		}

        private void MainForm_Resize(object sender, EventArgs e)
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
