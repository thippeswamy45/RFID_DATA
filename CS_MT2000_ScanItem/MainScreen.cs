//--------------------------------------------------------------------
// FILENAME: MainScreen.cs
//
// Copyright(c) 2009 Symbol Technologies Inc. All rights reserved.
//
// DESCRIPTION:
//      This module is part of the ScanItem application.   It manages 
//      the user interface and command for the main scanning screen.
//
// NOTES:
//      This software is provided as is as an example of how to use the 
//      MT2000 Scanner services assemblies. 
//      
// 
//--------------------------------------------------------------------
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using Symbol.MT2000.Utils;
using Symbol.MT2000.UserInterface;
using Symbol.MT2000.ScannerServices;

namespace CS_MT2000_ScanItem
{
	public class MainScreen : ListScreen
	{
		// local delegates
		private delegate void ReadLabelEventDelegate(object sender, Symbol.MT2000.ScannerServices.ReadLabelEventArgs e);

		// local variables
		private ScrollableListItems items = null;
		private ScrollableListItem quantityItem = null;
		private ScrollableListItem barcodeItem = null;
		private MenuDataList menu = null;
		private LineTextBox textBox;
		private UnsignedIntegerValidator quantityValidator = null;
		private uint quantity = 1;
		private string quantityText = "1";
		private string lastBarcode = "";
		private int selectedIndex = 1;
		private ReadLabelEventDelegate readLabelEvent = null;
		private System.Windows.Forms.Timer clearBarcodeTimer;
		private string clearBarcode = "";

		/// <summary>
		/// initializes the member variables
		/// </summary>
		/// <param name="listForm">form to display the screen on</param>
		public MainScreen(MainForm listForm)
			: base(listForm)
		{
			// load the options
			Options.Load();

			// create the text box
			textBox = new LineTextBox();
			textBox.Name = "textBox";

			// create the quantity validator
			quantityValidator = new UnsignedIntegerValidator(1, Options.MaxQuantity, Options.MaxQuantityLength,
										string.Format(Properties.Resources.StrErrorBadQuantity, Options.MaxQuantity));

			// create the list of items
			items = new ScrollableListItems();
			quantityItem = new ScrollableListItem(Properties.Resources.StrQuantity + ":\t" + quantityText, null, null, "quantity");
			quantityItem.EditControl = textBox;
			quantityItem.Validator = quantityValidator;
			items.Add(quantityItem);
			barcodeItem = new ScrollableListItem(Properties.Resources.StrItem + ":\t", null, null, "barcode");
			barcodeItem.EditControl = textBox;
			items.Add(barcodeItem);
			Graphics graphics = listForm.CreateGraphics();
			int descriptionWidth = (int)Math.Ceiling(graphics.MeasureString(Properties.Resources.StrQuantity + ":", listForm.List.Font).Width);
			graphics.Dispose();
			descriptionWidth += Config.LineSpaceWidth * 2;
			int valueWidth = Config.ScreenWidth - descriptionWidth - Config.LineSpaceWidth * 2;
			ScrollableListColumns columns = new ScrollableListColumns();
			columns.Add(new ScrollableListColumn(null, descriptionWidth, StringAlignment.Near));
			columns.Add(new ScrollableListColumn(null, valueWidth, StringAlignment.Near));
			items.Columns = columns;

			// create the left soft key menu
			menu = new MenuDataList();
			menu.ShowIcons = true;
			menu.Add(new MenuDataItem(Properties.Resources.StrOptions + "...", "options", null, Config.OptionsBitmap));
			menu.Add(new MenuDataItem(Properties.Resources.StrAbout + "...", "about", null, Config.AboutBitmap));
			menu.Add(new MenuDataItem(Properties.Resources.StrClose, "close", null, Config.CloseBitmap));

			// create the clear barcode timer
			clearBarcodeTimer = new System.Windows.Forms.Timer();
			clearBarcodeTimer.Tick += new EventHandler(clearBarcodeTimer_Tick);

			// create the ReadLabelEvent delegate
			readLabelEvent = new ReadLabelEventDelegate(ReadLabelEvent);

			// connect to the scanner read label events
			Program.ScannerServicesClient.ReadLabelEvent += new ScannerServicesClient.ReadLabelHandler(ReadLabelEvent);
		}

		/// <summary>
		/// displays this screen
		/// </summary>
		public override void Show()
		{
			// set the title and soft key text
			listForm.TitleText = Properties.Resources.StrScanItem;
			listForm.LeftSoftKeyText = Properties.Resources.StrMenu;
			listForm.RightSoftKeyText = Properties.Resources.StrClose;

			// display the item and quantity
			listForm.List.ShowIcons = false;
			listForm.List.ShowShortcuts = false;
			listForm.List.Items = items;
			listForm.List.SelectedIndex = selectedIndex;

			// set the clear barcode delay
			clearBarcodeTimer.Interval = (int)Options.ClearBarcodeDelay * 1000;

			// start an asynchronous scanner read
			Program.ScannerServicesClient.BeginReadLabel();
		}

		/// <summary>
		/// cleans up when the screen is being closed
		/// </summary>
		public override void Done()
		{
			// disconnect from the scanner read label events
			Program.ScannerServicesClient.ReadLabelEvent -= new ScannerServicesClient.ReadLabelHandler(ReadLabelEvent);
		}
		
		/// <summary>
		/// performs the action associated with the selected list item
		/// </summary>
		/// <param name="item">selected list item</param>
		/// <returns>new screen to be displayed, 'this' to stay on the same screen</returns>
		public override ListScreen ExecuteListItem(ScrollableListItem item)
		{
			if (item != null && (string)item.Tag == "barcode")
			{
				// send the barcode
				string barcode = textBox.Text;
				if (barcode != "")
				{
					SendBarcode(barcode);
				}
			}

			// stay on this screen
			return this;
		}

		/// <summary>
		/// performs the action associated with the left soft key
		/// </summary>
		/// <returns>new screen to be displayed or 'this' to stay on the same screen</returns>
		public override ListScreen LeftSoftKeyPressed()
		{
			menu.SelectedItemIndex = 0;
			listForm.HighlightLeftSoftKey = true;
			MenuDataItem menuItem = PopupMenu.Show(listForm, menu);
			listForm.HighlightLeftSoftKey = false;
			if (menuItem != null)
			{
				if (menuItem.Command == "close")
				{
					listForm.Close();
				}
				else
				{
					ScrollableListItem item = listForm.List.SelectedItem;
					if (item == barcodeItem)
					{
						GetNewBarcode();
					}
					else if (!GetNewQuantity(true))
					{
						return this;
					}
					Program.ScannerServicesClient.CancelReadLabel();
					selectedIndex = listForm.List.SelectedIndex;
					return listForm.Screens[menuItem.Command];
				}
			}
			return this;
		}

		/// <summary>
		/// called when the list selection is about to change
		/// </summary>
		/// <param name="item">currently selected list item, null if there's no selection</param>
		/// <returns>true if the selection can be changed, false if not</returns>
		public override bool SelectionChanging(ScrollableListItem item)
		{
			if (item != null)
			{
				// set the text box value and select the text
				switch ((string)item.Tag)
				{
					case "quantity":
						return GetNewQuantity(false);
					case "barcode":
						GetNewBarcode();
						break;
				}
			}
			return true;
		}

		/// <summary>
		/// called when the list selection changes
		/// </summary>
		/// <param name="item">newly selected list item, null if there's no selection</param>
		public override void SelectionChanged(ScrollableListItem item)
		{
			if (item != null)
			{
				// set the text box value and select the text
				switch ((string)item.Tag)
				{
					case "quantity":
						textBox.MaxLength = Options.MaxQuantityLength;
						break;
					case "barcode":
						textBox.MaxLength = 0;
						break;
				}
			}
		}

		/// <summary>
		/// updates the barcode if it has changed
		/// </summary>
		private void GetNewBarcode()
		{
			if (textBox.Text != lastBarcode)
			{
				lastBarcode = textBox.Text;
				barcodeItem.Text = Properties.Resources.StrItem + ":\t" + lastBarcode;
			}
		}

		/// <summary>
		/// checks the new quantity value before leaving the line or screen
		/// </summary>
		/// <param name="validate">true to validate the quantity value, false to ignore it</param>
		/// <returns>true if the new quantity is OK, false if not</returns>
		private bool GetNewQuantity(bool validate)
		{
			// if the quantity has changed
			string qt = textBox.Text.Trim();
			if (qt != quantityText)
			{
				if (validate && !quantityValidator.Validate(qt))
				{
					return false;
				}
				quantity = quantityValidator.Value;
				quantityText = qt;
				quantityItem.Text = Properties.Resources.StrQuantity + ":\t" + quantityText;
			}

			// indicate success
			return true;
		}

		/// <summary>
		/// receives and sends the scanner data
		/// </summary>
		private void ReadLabelEvent(object sender, ReadLabelEventArgs e)
		{
			// invoke this method if required
			if (listForm.List.InvokeRequired)
			{
				listForm.List.Invoke(readLabelEvent, sender, e);
				return;
			}

			if (e.Result == RESULTCODE.E_OK && !listForm.ShowSpinner)
			{
				SendBarcode(e.LabelData, true);
			}

			// start another read
			Program.ScannerServicesClient.BeginReadLabel();
		}

		/// <summary>
		/// sends some barcode text to the host
		/// </summary>
		/// <param name="barcode">barcode text to be sent to the host</param>
		private void SendBarcode(string barcode)
		{
            
			// display the barcode text
            // 20090214 - RJP had to do this to fix delay in displaying barcode. 
            if (50 < barcode.Length)
            {
                lastBarcode = barcode.Substring(0, Math.Min(50, barcode.Length)) + "...";
            }
            else
            {
                lastBarcode = barcode; 
            }
			ScrollableListItem item = listForm.List.SelectedItem;
			if (item == barcodeItem)
			{
                textBox.Text = lastBarcode;
			}
			else if (!GetNewQuantity(true))
			{
				return;
			}
			barcodeItem.Text = Properties.Resources.StrItem + ":\t" + lastBarcode;
			clearBarcode = lastBarcode;
			clearBarcodeTimer.Enabled = true;

			// send the barcode as quantity + delimiter + barcode
			if (Options.TransmitFormat == TransmitFormat.QuantityDelimiterBarcode)
			{
				string labelText = quantityText;
				switch (Options.TransmitDelimiter)
				{
					case TransmitDelimiter.Tab:
						labelText += "\t";
						break;
					case TransmitDelimiter.Semicolon:
						labelText += ";";
						break;
					default:
						labelText += ",";
						break;
				}
				labelText += barcode;
				SendBarcode(new LabelData(labelText, Options.BarcodeType));
			}

			// send the barcode as barcode + barcode + ... + barcode
			else
			{
				LabelData label = new LabelData(barcode, Options.BarcodeType);
				int delay = Options.InterTransmitDelay;
				for (uint i = 0; i < quantity && Program.ScannerServicesClient != null; i++)
				{
					if (i > 0)
					{
						Thread.Sleep((int)delay);
					}
					if (!SendBarcode(label))
					{
						break;
					}
				}
			}
		}

		/// <summary>
		/// sends a barcode label to the host
		/// </summary>
		/// <param name="label">barcode label to be sent to the host</param>
		private bool SendBarcode(LabelData label)
		{
			RESULTCODE result = RESULTCODE.E_OK;
			try
			{
				result = Program.ScannerServicesClient.SendLabel(label, 10000);
			}
			catch
			{
				result = RESULTCODE.E_HOST_NOT_READY;
			}
			if (result != RESULTCODE.E_OK)
			{
				MsgBox.Error(listForm, Properties.Resources.StrErrorCouldntSendBarcode);
			}
			return result == RESULTCODE.E_OK;
		}
        /// <summary>
        /// Sends barcode data that was received from Scanner services, in this case, we do not 
        /// want to alter the code types...
        /// </summary>
        private void SendBarcode(LabelData label, bool fromServices)
        {
            // display the barcode text
            // 20090214 - RJP had to do this to fix delay in displaying barcode. 
            if (50 < label.Text.Length)
            {
                lastBarcode = label.Text.Substring(0, Math.Min(50, label.Text.Length)) + "...";
            }
            else
            {
                lastBarcode = label.Text;
            }
            ScrollableListItem item = listForm.List.SelectedItem;
            if (item == barcodeItem)
            {
                textBox.Text = lastBarcode;
            }
            else if (!GetNewQuantity(true))
            {
                return;
            }
            barcodeItem.Text = Properties.Resources.StrItem + ":\t" + lastBarcode;
            clearBarcode = lastBarcode;
            clearBarcodeTimer.Enabled = true;

            // send the barcode as quantity + delimiter + barcode
            if (Options.TransmitFormat == TransmitFormat.QuantityDelimiterBarcode)
            {
                string labelText = quantityText;
                switch (Options.TransmitDelimiter)
                {
                    case TransmitDelimiter.Tab:
                        labelText += "\t";
                        break;
                    case TransmitDelimiter.Semicolon:
                        labelText += ";";
                        break;
                    default:
                        labelText += ",";
                        break;
                }
                labelText += label.Text;
                SendBarcode(new LabelData(labelText, Options.BarcodeType));
            }

            // send the barcode as barcode + barcode + ... + barcode
            else
            {
                //LabelData label = new LabelData(barcode, Options.BarcodeType);
                int delay = Options.InterTransmitDelay;
                for (uint i = 0; i < quantity && Program.ScannerServicesClient != null; i++)
                {
                    if (i > 0)
                    {
                        Thread.Sleep((int)delay);
                    }
                    if (!SendBarcode(label))
                    {
                        break;
                    }
                }
            }
        }

		/// <summary>
		/// clears the barcode
		/// </summary>
		void clearBarcodeTimer_Tick(object sender, EventArgs e)
		{
			clearBarcodeTimer.Enabled = false;
			if (!string.IsNullOrEmpty(lastBarcode) && clearBarcode == lastBarcode)
			{
				barcodeItem.Text = Properties.Resources.StrItem + ":\t";
				if (listForm.List.SelectedItem == barcodeItem && textBox.Text == lastBarcode)
				{
					textBox.Text = "";
				}
				lastBarcode = "";

                // Clear quantity to 1 after barcode send. 
                quantityText = "1";
                quantity = 1;
                quantityItem.Text = Properties.Resources.StrQuantity + ":\t" + quantity.ToString() ;
			}
		}
	}
}
