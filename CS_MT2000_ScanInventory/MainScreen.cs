//--------------------------------------------------------------------
// FILENAME: MainScreen.cs
//
// Copyright(c) 2009 Symbol Technologies Inc. All rights reserved.
//
// DESCRIPTION:
//      This module is part of the ScanInventory application.   It manages
//      the user interface Main Scanning Screen of the inventory application. 
//
// NOTES:
//      This software is provided as is as an example of how to use the 
//      MT2000 Scanner services assemblies. 
//      
// 
//--------------------------------------------------------------------
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

using Symbol.MT2000.Utils;
using Symbol.MT2000.UserInterface;
using Symbol.MT2000.ScannerServices;

namespace CS_MT2000_ScanInventory
{
	public class MainScreen : ListScreen
	{
		// local delegates
		private delegate void ReadLabelEventDelegate(object sender, ReadLabelEventArgs e);

		// local variables
		private ScrollableListItems items = null;
		private ScrollableListItem locationItem = null;
		private ScrollableListItem quantityItem = null;
		private ScrollableListItem barcodeItem = null;
		private MenuDataList menu = null;
		private LineTextBox textBox;
		private UnsignedIntegerValidator quantityValidator = null;
		private StringValidator locationValidator = null;
		private uint quantity = 1;
		private string quantityText = "1";
		private string lastBarcode = "";
		private int selectedIndex = 1;
		private ReadLabelEventDelegate readLabelEvent = null;
		private Timer clearBarcodeTimer;
		private string clearBarcode = "";

		/// <summary>
		/// initializes the member variables
		/// </summary>
		/// <param name="listForm">form to display the screen on</param>
		public MainScreen(ListForm listForm)
			: base(listForm)
		{
			// load the options and inventory
			Options.Load();
			Inventory.Load();

			// create the resource loader
			ResourceLoader resourceLoader = new ResourceLoader("CS_MT2000_ScanInventory.Resources.Images");

			// build the main menu
			menu = new MenuDataList();
			menu.ShowIcons = true;
			menu.Add(new MenuDataItem(Properties.Resources.StrViewInventory + "...", "view", null, resourceLoader.LoadBitmap("Inventory.bmp")));
			menu.Add(new MenuDataItem(Properties.Resources.StrSaveInventory, "save", null, Config.SaveBitmap));
			menu.Add(new MenuDataItem(Properties.Resources.StrOptions + "...", "options", null, Config.OptionsBitmap));
			menu.Add(new MenuDataItem(Properties.Resources.StrAbout + "...", "about", null, Config.AboutBitmap));
            menu.Add(new MenuDataItem(Properties.Resources.StrClose, "close", null, Config.CloseBitmap));

			// create the text box
			textBox = new LineTextBox();
			textBox.Name = "textBox";

			// create the validators
			quantityValidator = new UnsignedIntegerValidator(1, Options.MaxQuantity, Options.MaxQuantityLength,
										string.Format(Properties.Resources.StrErrorBadQuantity, Options.MaxQuantity));
			locationValidator = new StringValidator(Options.MaxLocationLength, false, Properties.Resources.StrErrorNoLocation);

			// create the list of options
			items = new ScrollableListItems();
			locationItem = new ScrollableListItem(Properties.Resources.StrLocation + ":\t" + Options.Location, null, null, "location");
			locationItem.EditControl = textBox;
			locationItem.Validator = locationValidator;
			items.Add(locationItem);
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

			// create the clear barcode timer
			clearBarcodeTimer = new Timer();
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
			listForm.TitleText = Properties.Resources.StrScanInventory;
			listForm.LeftSoftKeyText = Properties.Resources.StrMenu;
			listForm.RightSoftKeyText = Properties.Resources.StrClose;

			// clear the list of items
			listForm.List.ShowIcons = false;
			listForm.List.ShowShortcuts = false;
			listForm.List.Items = items;
			if (string.IsNullOrEmpty(Options.Location))
			{
				selectedIndex = 0;
			}
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

			// save the options
			if (Options.Changed || Inventory.Changed)
			{
				listForm.ShowSpinner = true;
				bool optionsOK = true;
				bool inventoryOK = true;
				if (Options.Changed)
				{
					optionsOK = Options.Save();
				}
				if (Inventory.Changed)
				{
					inventoryOK = Inventory.Save();
				}
				listForm.ShowSpinner = false;
				if (!optionsOK)
				{
					MsgBox.Error(listForm, Properties.Resources.StrErrorCouldntSaveOptions);
				}
				if (!inventoryOK)
				{
					MsgBox.Error(listForm, Properties.Resources.StrErrorCouldntSaveInventory);
				}
			}
		}
		
		/// <summary>
		/// performs the action associated with the selected list item
		/// </summary>
		/// <param name="item">selected list item</param>
		/// <returns>new screen to be displayed or 'this' to stay on the same screen</returns>
		public override ListScreen ExecuteListItem(ScrollableListItem item)
		{
			if (item != null && (string)item.Tag == "barcode")
			{
				// add some items to the inventory
				if (GetNewBarcode())
				{
					AddToInventory(lastBarcode);
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
				// close the program
				if (menuItem.Command == "close")
				{
					listForm.Close();
				}

				// save the inventory
				else if (menuItem.Command == "save")
				{
					listForm.ShowSpinner = true;
					bool ok = Inventory.Save();
					listForm.ShowSpinner = false;
					if (ok)
					{
						MsgBox.Show(listForm, Properties.Resources.StrSave, Properties.Resources.StrSaveOK);
					}
					else
					{
						MsgBox.Error(listForm, Properties.Resources.StrErrorCouldntSaveInventory);
					}
				}

				// go to one of the other screens
				else
				{
					ScrollableListItem item = listForm.List.SelectedItem;
					if (item == locationItem)
					{
						if (!GetNewLocation(true))
						{
							return this;
						}
					}
					else if (item == quantityItem)
					{
						if (!GetNewQuantity(true))
						{
							return this;
						}
					}
					else if (!GetNewBarcode())
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
					case "location":
						return GetNewLocation(false);
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
					case "location":
						textBox.MaxLength = Options.MaxLocationLength;
						break;
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
		/// gets and checks the new location value
		/// </summary>
		/// <returns>true if the new location is OK, false if not</returns>
		private bool GetNewLocation(bool validate)
		{
			// make sure there is a location
			string newLocation = textBox.Text.Trim();
			if (newLocation != Options.Location)
			{
				if (validate && !locationValidator.Validate(newLocation))
				{
					return false;
				}
				Options.Location = newLocation;
				locationItem.Text = Properties.Resources.StrLocation + ":\t" + newLocation;
			}

			// indicate success
			return true;
		}

		/// <summary>
		/// checks the new quantity value before leaving the line or screen
		/// </summary>
		/// <param name="validate">true to validate the quantity value, false to ignore it</param>
		/// <returns>true if the new quantity is OK, false if not</returns>
		private bool GetNewQuantity(bool validate)
		{
			// if the quantity has changed then check it
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
		/// gets and checks the new barcode value
		/// </summary>
		/// <returns>true if the new barcode is OK, false if not</returns>
		private bool GetNewBarcode()
		{
			// if the barcode has changed then update it
			string newBarcode = textBox.Text.Trim();
			if (newBarcode != lastBarcode)
			{
				lastBarcode = newBarcode;
				barcodeItem.Text = Properties.Resources.StrItem + ":\t" + lastBarcode;
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
				bool ok = true;
				ScrollableListItem item = listForm.List.SelectedItem;
				if (item == locationItem)
				{
					ok = GetNewLocation(true);
				}
				else if (item == quantityItem)
				{
					ok = GetNewQuantity(true);
				}
				if (ok)
				{
					AddToInventory(e.LabelData.Text);
				}
			}

			// start another read
			Program.ScannerServicesClient.BeginReadLabel();
		}

		/// <summary>
		/// adds a quantity of some barcode to the inventory
		/// </summary>
		/// <param name="barcode">barcode of the item to be added</param>
		private void AddToInventory(string barcode)
		{
			// display the barcode text
			lastBarcode = barcode;
			barcodeItem.Text = Properties.Resources.StrItem + ":\t" + lastBarcode;
            Inventory.AddItem(Options.Location, quantity, lastBarcode);


            // 2009 Jul 08 - 
            if (50 < barcode.Length)
            {
                lastBarcode = barcode.Substring(0, Math.Min(50, barcode.Length)) + "...";
            }
            else
            {
                lastBarcode = barcode;
            }


			if (listForm.List.SelectedItem == barcodeItem)
			{
				textBox.Text = lastBarcode;
			}
			clearBarcode = lastBarcode;
			clearBarcodeTimer.Enabled = true;

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
			}
		}
	}
}
