//--------------------------------------------------------------------
// FILENAME: InventoryScreen.cs
//
// Copyright(c) 2009 Symbol Technologies Inc. All rights reserved.
//
// DESCRIPTION:
//      This module is part of the ScanInventory application.   It manages
//      the user interface for the inventory view screen.  It also invokes 
//      inventor operations based upon commands from the user interface.  
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
using System.Collections.Generic;

using Symbol.MT2000.Utils;
using Symbol.MT2000.UserInterface;

namespace CS_MT2000_ScanInventory
{
	public class InventoryScreen : ListScreen
	{
		// local variables
		private MenuDataList menu = null;
		private ScrollableListItems items = null;
		private Bitmap transmit;

		/// <summary>
		/// initializes the member variables
		/// </summary>
		/// <param name="listForm">form to display the screen on</param>
		public InventoryScreen(ListForm listForm)
			: base(listForm)
		{
			// load the menu bitmaps
			ResourceLoader resourceLoader = new ResourceLoader("CS_MT2000_ScanInventory.Resources.Images");
			transmit = resourceLoader.LoadBitmap("Transmit.bmp");
		}

		/// <summary>
		/// displays this screen
		/// </summary>
		public override void Show()
		{
			// set the title and soft key text
			listForm.TitleText = Properties.Resources.StrViewInventory;
			listForm.LeftSoftKeyText = Properties.Resources.StrMenu;
			listForm.RightSoftKeyText = Properties.Resources.StrDone;

			// put the inventory into the list of items and calculate the column widths
			items = new ScrollableListItems();
			int locationWidth = 0;
			int quantityWidth = 0;
			Graphics graphics = listForm.CreateGraphics();
			//Font font = new Font(Config.LineFontName, 10, Config.LineFontSyle);
			Font font = listForm.List.Font;
			foreach (KeyValuePair<string, InventoryLocation> kvpLocations in Inventory.Locations)
			{
				InventoryLocation location = kvpLocations.Value;
				int width = (int)Math.Ceiling(graphics.MeasureString(location.Location, font).Width);
				if (width > locationWidth)
				{
					locationWidth = width;
				}
				foreach (KeyValuePair<string, InventoryItem> kvpItems in location.Items)
				{
					InventoryItem item = kvpItems.Value;
					string quantityText = item.Quantity.ToString();
					width = (int)Math.Ceiling(graphics.MeasureString(quantityText, font).Width);
					if (width > quantityWidth)
					{
						quantityWidth = width;
					}
					items.Add(new ScrollableListItem(location.Location + "\t" + quantityText + "\t" + item.Barcode, null, null, item));
				}
			}
			graphics.Dispose();
			locationWidth += Config.LineSpaceWidth * 2;
			quantityWidth += Config.LineSpaceWidth * 2;

			// create the list columns
			ScrollableListColumns columns = new ScrollableListColumns();
			columns.Add(new ScrollableListColumn(null, locationWidth, StringAlignment.Near));
			columns.Add(new ScrollableListColumn(null, quantityWidth, StringAlignment.Near));
			columns.Add(new ScrollableListColumn(null, Config.ScreenWidth - locationWidth - quantityWidth -
													Config.LineSpaceWidth * 2, StringAlignment.Near));
			items.Columns = columns;

			// set the list of items
			listForm.List.ShowIcons = false;
			listForm.List.ShowShortcuts = false;
			//listForm.List.Font = font;
			listForm.List.Items = items;
		}

		/// <summary>
		/// performs the action associated with the selected list item
		/// </summary>
		/// <param name="item">selected list item</param>
		/// <returns>new screen to be displayed or 'this' to stay on the same screen</returns>
		public override ListScreen ExecuteListItem(ScrollableListItem item)
		{
			if (item != null)
			{
				EditItem(item);
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
			bool ok;
			string fullFileName;

			// build the menu
			menu = new MenuDataList();
			menu.ShowIcons = true;
			if (listForm.List.Items.Count != 0)
			{
				menu.Add(new MenuDataItem(Properties.Resources.StrEdit + "...", "edit", null, Config.EditBitmap));
				menu.Add(new MenuDataItem(Properties.Resources.StrDeleteItem + "...", "delete_item", null, Symbol.MT2000.UserInterface.Config.DeleteBitmap));
				menu.Add(new MenuDataItem(Properties.Resources.StrDeleteAll + "...", "delete_all", null, Symbol.MT2000.UserInterface.Config.DeleteBitmap));
				menu.Add(new MenuDataItem(Properties.Resources.StrTransmit, "transmit", null, transmit));
				menu.Add(new MenuDataItem(Properties.Resources.StrExport, "export", null, Symbol.MT2000.UserInterface.Config.SaveBitmap));
			}
			menu.Add(new MenuDataItem(Properties.Resources.StrSave, "save", null, Symbol.MT2000.UserInterface.Config.SaveBitmap));
			menu.Add(new MenuDataItem(Properties.Resources.StrOptions + "...", "options", null, Symbol.MT2000.UserInterface.Config.OptionsBitmap));
			menu.Add(new MenuDataItem(Properties.Resources.StrAbout + "...", "about", null, Symbol.MT2000.UserInterface.Config.AboutBitmap));
			menu.Add(new MenuDataItem(Properties.Resources.StrClose, "close", null, Symbol.MT2000.UserInterface.Config.CloseBitmap));

			// display the menu
			listForm.HighlightLeftSoftKey = true;
			MenuDataItem menuItem = PopupMenu.Show(listForm, menu);
			listForm.HighlightLeftSoftKey = false;
			if (menuItem != null)
			{
				switch (menuItem.Command)
				{
					// edit the selected inventory item
					case "edit":
						EditItem(listForm.List.SelectedItem);
						break;

					// delete the selected inventory item
					case "delete_item":
						ScrollableListItem item = listForm.List.SelectedItem;
						if (item != null)
						{
							bool delete = QuestionForm.ShowYesNo(listForm, Properties.Resources.StrDeleteItem, Properties.Resources.StrOKToDeleteItem);
							if (delete)
							{
								listForm.List.Items.Remove(item);
								Inventory.DeleteItem((InventoryItem)item.Tag);
							}
						}
						break;

					// delete all the inventory items
					case "delete_all":
						bool deleteAll = QuestionForm.ShowYesNo(listForm, Properties.Resources.StrDeleteAll, Properties.Resources.StrOKToDeleteAll);
						if (deleteAll)
						{
							listForm.List.Items.Clear();
							Inventory.DeleteAllItems();
						}
						break;

					// transmit all the inventory items and delete them
					case "transmit":
						listForm.ShowSpinner = true;
						ok = Inventory.Export("\\Temp\\inventory", Options.ExportFormat, Options.GroupedByLocation, out fullFileName);
						if (!ok)
						{
							listForm.ShowSpinner = false;
							MsgBox.Error(listForm, Properties.Resources.StrErrorCouldntExportInventoryToTempFile);
							break;
						}
						ok = Inventory.Transmit(fullFileName);
						if ((ok) && (Program.deleteAfterTransmit == true))
						{
							Inventory.DeleteAllItems();
							listForm.List.Items.Clear();
						}
						listForm.ShowSpinner = false;
						if (!ok)
						{
							MsgBox.Error(listForm, Properties.Resources.StrErrorCouldntTransmitInventory);
						}
						break;

					// export all the inventory items to a text or XML file
					case "export":
						listForm.ShowSpinner = true;
						ok = Inventory.Export(Options.ExportFile, Options.ExportFormat, Options.GroupedByLocation, out fullFileName);
						listForm.ShowSpinner = false;
						if (ok)
						{
							MsgBox.Show(listForm, Properties.Resources.StrExport, string.Format(Properties.Resources.StrExportOK, fullFileName));
						}
						else
						{
							MsgBox.Error(listForm, string.Format(Properties.Resources.StrErrorCouldntExportInventory, fullFileName));
						}
						break;

					// save all the inventory items to the data file
					case "save":
						listForm.ShowSpinner = true;
						ok = Inventory.Save();
						listForm.ShowSpinner = false;
						if (ok)
						{
							MsgBox.Show(listForm, Properties.Resources.StrSave, Properties.Resources.StrSaveOK);
						}
						else
						{
							MsgBox.Error(listForm, Properties.Resources.StrErrorCouldntSaveInventory);
						}
						break;

					// display the options screen
					case "options":
						return listForm.Screens["options"];

					// display the about screen
					case "about":
						return listForm.Screens["about"];

					// close the program
					case "close":
						listForm.Close();
						break;
				}
			}
         Program.ScannerServicesClient.CancelReadLabel();
         return this;
		}

		/// <summary>
		/// resets the column widths after editing
		/// </summary>
		private void ResetColumns()
		{
			int locationWidth = 0;
			int quantityWidth = 0;
			Graphics graphics = listForm.CreateGraphics();
			Font font = listForm.List.Font;
			foreach (KeyValuePair<string, InventoryLocation> kvpLocations in Inventory.Locations)
			{
				InventoryLocation location = kvpLocations.Value;
				int width = (int)Math.Ceiling(graphics.MeasureString(location.Location, font).Width);
				if (width > locationWidth)
				{
					locationWidth = width;
				}
				foreach (KeyValuePair<string, InventoryItem> kvpItems in location.Items)
				{
					InventoryItem item = kvpItems.Value;
					string quantityText = item.Quantity.ToString();
					width = (int)Math.Ceiling(graphics.MeasureString(quantityText, font).Width);
					if (width > quantityWidth)
					{
						quantityWidth = width;
					}
				}
			}
			graphics.Dispose();
			locationWidth += Config.LineSpaceWidth * 2;
			quantityWidth += Config.LineSpaceWidth * 2;

			// create the list columns
			ScrollableListColumns columns = new ScrollableListColumns();
			columns.Add(new ScrollableListColumn(null, locationWidth, StringAlignment.Near));
			columns.Add(new ScrollableListColumn(null, quantityWidth, StringAlignment.Near));
			columns.Add(new ScrollableListColumn(null, Config.ScreenWidth - locationWidth - quantityWidth -
													Config.LineSpaceWidth * 2, StringAlignment.Near));
			listForm.List.Items.Columns = columns;
		}

		/// <summary>
		/// displays the EditForm to edit an inventory item
		/// </summary>
		/// <param name="item">inventory item to be edited</param>
		private void EditItem(ScrollableListItem item)
		{
			// displat the edit form
			InventoryItem inventoryItem = (InventoryItem)item.Tag;
			EditForm editForm = new EditForm(inventoryItem);
			editForm.Show(listForm);

			// if changes were made
			if (editForm.ItemLocation != "" && (editForm.ItemLocation != inventoryItem.Location.Location ||
				editForm.ItemQuantity != inventoryItem.Quantity || editForm.ItemBarcode != inventoryItem.Barcode))
			{
				// if the list position would change then delete the existing item and add a new one
				if (editForm.ItemLocation != inventoryItem.Location.Location || editForm.ItemBarcode != inventoryItem.Barcode)
				{
					// delete the existing inventory item and add a new one
					Inventory.DeleteItem(inventoryItem);
					inventoryItem = Inventory.AddItem(editForm.ItemLocation, editForm.ItemQuantity, editForm.ItemBarcode);

					// delete the existing list item and create a new one
					listForm.List.Items.Remove(item);
					item = new ScrollableListItem(inventoryItem.Location.Location + "\t" + inventoryItem.Quantity.ToString() + "\t" +
													inventoryItem.Barcode, null, null, inventoryItem);

					// find the position of the inventory item
					int index = 0;
					foreach (KeyValuePair<string, InventoryLocation> kvpLocations in Inventory.Locations)
					{
						InventoryLocation searchLocation = kvpLocations.Value;
						if (searchLocation.Location == inventoryItem.Location.Location)
						{
							foreach (KeyValuePair<string, InventoryItem> kvpItems in searchLocation.Items)
							{
								InventoryItem searchItem = kvpItems.Value;
								if (searchItem.Barcode == inventoryItem.Barcode)
								{
									break;
								}
								index++;
							}
							break;
						}
						else
						{
							index += searchLocation.Items.Count;
						}
					}

					// add the new list item
					if (index < listForm.List.Items.Count)
					{
						listForm.List.Items.Insert(index, item);
					}
					else
					{
						listForm.List.Items.Add(item);
					}
					listForm.List.SelectedIndex = index;
				}

				// otherwise update the quantity in place
				else
				{
					inventoryItem.Quantity = editForm.ItemQuantity;
					item.Text = inventoryItem.Location.Location + "\t" + inventoryItem.Quantity.ToString() + "\t" + inventoryItem.Barcode;
				}

				// calculate the new column widths
				ResetColumns();
			}
		}
	}
}
