//--------------------------------------------------------------------
// FILENAME: OptionsScreen.cs
//
// Copyright(c) 2009 Symbol Technologies Inc. All rights reserved.
//
// DESCRIPTION:
//      This module is part of the ScanInventory application.   It manages 
//      the user interface and commands for the options screen. 
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

using Symbol.MT2000.Utils;
using Symbol.MT2000.UserInterface;

namespace CS_MT2000_ScanInventory
{
	public class OptionsScreen : ListScreen
	{
		// local variables
		private int separatorIndex;
		private ScrollableListItem separatorItem;
		private string exportFile;
		private bool exportXML;
		private ExportFormat exportFormat;
		private bool groupedByLocation;
		private uint clearBarcodeDelay;
		private LineTextBox textBox;
		private LineChooser formatChooser;
		private LineChooser separatorChooser;
		private LineChooser groupedChooser;
		private UnsignedIntegerValidator clearDelayValidator;
		private StringValidator exportFileValidator;

		/// <summary>
		/// initializes the member variables
		/// </summary>
		/// <param name="listForm">form to display the screen on</param>
		public OptionsScreen(ListForm listForm)
			: base(listForm)
		{
			// create the edit controls
			formatChooser = new LineChooser();
			formatChooser.Name = "formatChooser";
			formatChooser.IncValue += new EventHandler(formatChooser_IncDecValue);
			formatChooser.DecValue += new EventHandler(formatChooser_IncDecValue);
			separatorChooser = new LineChooser();
			separatorChooser.Name = "separatorChooser";
			separatorChooser.IncValue += new EventHandler(separatorChooser_IncValue);
			separatorChooser.DecValue += new EventHandler(separatorChooser_DecValue);
			groupedChooser = new LineChooser();
			groupedChooser.Name = "groupedChooser";
			groupedChooser.IncValue += new EventHandler(groupedChooser_IncDecValue);
			groupedChooser.DecValue += new EventHandler(groupedChooser_IncDecValue);
			textBox = new LineTextBox();
			textBox.Name = "textBox";

			// create the validators
			exportFileValidator = new StringValidator(0, false);
			clearDelayValidator = new UnsignedIntegerValidator(1, Options.MaxClearBarcodeDelay, Options.MaxClearBarcodeDelayLength);
		}

		/// <summary>
		/// displays this screen
		/// </summary>
		public override void Show()
		{
			// set the title and soft key text
			listForm.TitleText = Properties.Resources.StrOptions;
			listForm.LeftSoftKeyText = Properties.Resources.StrCancel;
			listForm.RightSoftKeyText = Properties.Resources.StrDone;

			// get the current option values
			exportFile = Options.ExportFile;
			exportFormat = Options.ExportFormat;
			exportXML = exportFormat == ExportFormat.XML;
			if (exportXML)
			{
				exportFormat = ExportFormat.CommaSeparated;
			}
			groupedByLocation = Options.GroupedByLocation;
			clearBarcodeDelay = Options.ClearBarcodeDelay;

			// create the list of options
			ScrollableListItems items = new ScrollableListItems();
			ScrollableListItem item = new ScrollableListItem(Properties.Resources.StrFile + ":\t" + Path.GetFileName(exportFile),
																null, null, "file");
			item.EditControl = textBox;
			item.Validator = exportFileValidator;
			items.Add(item);
			item = new ScrollableListItem(Properties.Resources.StrFormat + ":\t" + (exportXML ? Properties.Resources.StrXML :
											Properties.Resources.StrText), null, null, "format");
			item.EditControl = formatChooser;
			items.Add(item);
			separatorIndex = items.Count;
			separatorItem = new ScrollableListItem(Properties.Resources.StrSeparator + ":\t" + GetSeparatorString(exportFormat),
												null, null, "separator");
			separatorItem.EditControl = separatorChooser;
			if (!exportXML)
			{
				items.Add(separatorItem);
			}
			item = new ScrollableListItem(Properties.Resources.StrGrouped + ":\t" + (groupedByLocation ? Properties.Resources.StrYes :
											Properties.Resources.StrNo), null, null, "grouped");
			item.EditControl = groupedChooser;
			items.Add(item);
			item = new ScrollableListItem(Properties.Resources.StrClearDelay + ":\t" + clearBarcodeDelay +
											Properties.Resources.StrS, null, null, "delay");
			item.EditControl = textBox;
			item.Validator = clearDelayValidator;
			item.Suffix = Properties.Resources.StrS;
			items.Add(item);
			Graphics graphics = listForm.CreateGraphics();
			int descriptionWidth = (int)Math.Ceiling(graphics.MeasureString(Properties.Resources.StrClearDelay + ":",
																			listForm.List.Font).Width);
			graphics.Dispose();
			descriptionWidth += Config.LineSpaceWidth * 2;
			int valueWidth = Config.ScreenWidth - descriptionWidth - Config.LineSpaceWidth * 2;
			ScrollableListColumns columns = new ScrollableListColumns();
			columns.Add(new ScrollableListColumn(null, descriptionWidth, StringAlignment.Near));
			columns.Add(new ScrollableListColumn(null, valueWidth, StringAlignment.Near));
			items.Columns = columns;

			// display the options information
			listForm.List.ShowIcons = false;
			listForm.List.ShowShortcuts = false;
			listForm.List.Items = items;
		}

		/// <summary>
		/// commits the changes when the right soft key is pressed
		/// </summary>
		/// <returns>'this' to stay on the same screen or null to pop to the previous screen</returns>
		public override ListScreen RightSoftKeyPressed()
		{
			// make sure there is an export file name
			if (string.IsNullOrEmpty(exportFile))
			{
				MsgBox.Error(listForm, Properties.Resources.StrErrorNoExportFile);
				return this;
			}

			// save options to permanent storage
			Options.ExportFile = exportFile;
			Options.ExportFormat = exportXML ? ExportFormat.XML : exportFormat;
			Options.GroupedByLocation = groupedByLocation;
			Options.ClearBarcodeDelay = clearBarcodeDelay;
			if (Options.Changed)
			{
				listForm.ShowSpinner = true;
				if (!Options.Save())
				{
					listForm.ShowSpinner = false;
					MsgBox.Error(listForm, Properties.Resources.StrErrorCouldntSaveOptions);
					return this;
				}
				listForm.ShowSpinner = false;
			}

			// return to the previous screen
			return null;
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
				// set the (possibly) updated option value
				switch ((string)item.Tag)
				{
					case "file":
						exportFile = textBox.Text;
						break;
					case "delay":
						clearBarcodeDelay = clearDelayValidator.Value;
						break;
				}
			}

			// indicate that the selection can be changed
			return true;
		}

		/// <summary>
		/// increments/decrements the export format value
		/// </summary>
		void formatChooser_IncDecValue(object sender, EventArgs e)
		{
			exportXML = !exportXML;
			formatChooser.Text = exportXML ? Properties.Resources.StrXML : Properties.Resources.StrText;
			if (exportXML)
			{
				listForm.List.Items.Remove(separatorItem);
			}
			else
			{
				listForm.List.Items.Insert(separatorIndex, separatorItem);
			}
		}

		/// <summary>
		/// increments the export format separator value
		/// </summary>
		void separatorChooser_IncValue(object sender, EventArgs e)
		{
			switch (exportFormat)
			{
				case ExportFormat.CommaSeparated:
					exportFormat = ExportFormat.SemicolonSeparated;
					break;
				case ExportFormat.SemicolonSeparated:
					exportFormat = ExportFormat.TabSeparated;
					break;
				default:
					exportFormat = ExportFormat.CommaSeparated;
					break;
			}
			separatorChooser.Text = GetSeparatorString(exportFormat);
		}

		/// <summary>
		/// decrements the export format separator value
		/// </summary>
		void separatorChooser_DecValue(object sender, EventArgs e)
		{
			switch (exportFormat)
			{
				case ExportFormat.SemicolonSeparated:
					exportFormat = ExportFormat.CommaSeparated;
					break;
				case ExportFormat.TabSeparated:
					exportFormat = ExportFormat.SemicolonSeparated;
					break;
				default:
					exportFormat = ExportFormat.TabSeparated;
					break;
			}
			separatorChooser.Text = GetSeparatorString(exportFormat);
		}

		/// <summary>
		/// increments/decrements the groupedByLocation flag
		/// </summary>
		void groupedChooser_IncDecValue(object sender, EventArgs e)
		{
			groupedByLocation = !groupedByLocation;
			groupedChooser.Text = groupedByLocation ? Properties.Resources.StrYes : Properties.Resources.StrNo;
		}

		/// <summary>
		/// gets a string representing a ExportFormat separator
		/// </summary>
		/// <param name="option4">ExportFormat value to get a separator string for</param>
		/// <returns>string representing the ExportFormat separator value</returns>
		public static string GetSeparatorString(ExportFormat exportFormat)
		{
			switch (exportFormat)
			{
				case ExportFormat.CommaSeparated:
					return Properties.Resources.StrComma;
				case ExportFormat.SemicolonSeparated:
					return Properties.Resources.StrSemiColon;
				case ExportFormat.TabSeparated:
					return Properties.Resources.StrTab;
			}
			return "";
		}
	}
}
