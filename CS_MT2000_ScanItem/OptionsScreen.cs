//--------------------------------------------------------------------
// FILENAME: OptionsScreen.cs
//
// Copyright(c) 2009 Symbol Technologies Inc. All rights reserved.
//
// DESCRIPTION:
//      This module is part of the ScanItem application.   It manages 
//      the user interface and commands for the options screen. 
//
// NOTES:
//      This software is provided as is as an example of how to use the 
//      MT2000 Scanner services assemblies. 
//      
// 
//--------------------------------------------------------------------
using System;
using System.Drawing;

using Symbol.MT2000.Utils;
using Symbol.MT2000.UserInterface;
using Symbol.MT2000.ScannerServices;

namespace CS_MT2000_ScanItem
{
	public class OptionsScreen : ListScreen
	{
		// local variables
		private ScrollableListItem delayItem;
		private ScrollableListItem delimiterItem;
		private TransmitFormat transmitFormat;
		private TransmitDelimiter transmitDelimiter;
		private int interTransmitDelay;
		private int clearBarcodeDelay;
		private LabelType barcodeType;
		private LineTextBox textBox;
		private LineChooser formatChooser;
		private LineChooser delimiterChooser;
		private LineChooser typeChooser;
		private UnsignedIntegerValidator transmitDelayValidator;
		private UnsignedIntegerValidator clearDelayValidator;

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
			delimiterChooser = new LineChooser();
			delimiterChooser.Name = "delimiterChooser";
			delimiterChooser.IncValue += new EventHandler(delimiterChooser_IncValue);
			delimiterChooser.DecValue += new EventHandler(delimiterChooser_DecValue);
			typeChooser = new LineChooser();
			typeChooser.Name = "typeChooser";
			typeChooser.IncValue += new EventHandler(typeChooser_IncDecValue);
			typeChooser.DecValue += new EventHandler(typeChooser_IncDecValue);
			textBox = new LineTextBox();
			textBox.Name = "textBox";

			// create the validators
			transmitDelayValidator = new UnsignedIntegerValidator(0, Options.MaxInterTransmitDelay, Options.MaxInterTransmitDelayLength);
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

			// get the options
			transmitFormat = Options.TransmitFormat;
			transmitDelimiter = Options.TransmitDelimiter;
			interTransmitDelay = Options.InterTransmitDelay;
			clearBarcodeDelay = Options.ClearBarcodeDelay;
			barcodeType = Options.BarcodeType;

			// create the list of options
			ScrollableListItems items = new ScrollableListItems();
			ScrollableListItem item = new ScrollableListItem(Properties.Resources.StrTransmit + ":\t" + TransmitFormatString,
																null, null, "format");
			item.EditControl = formatChooser;
			items.Add(item);
			delimiterItem = new ScrollableListItem(Properties.Resources.StrDelimiter + ":\t" + TransmitDelimiterString, null, null,
											"delimiter");
			delimiterItem.EditControl = delimiterChooser;
			delayItem = new ScrollableListItem(Properties.Resources.StrDelay + ":\t" + interTransmitDelay + Properties.Resources.StrMS,
											null, null, "transmit_delay");
			delayItem.EditControl = textBox;
			delayItem.Validator = transmitDelayValidator;
			delayItem.Suffix = Properties.Resources.StrMS;
			items.Add((transmitFormat == TransmitFormat.NBarcodes) ? delayItem : delimiterItem);
			item = new ScrollableListItem(Properties.Resources.StrBarcode + ":\t" + BarcodeTypeString, null, null,
											"type");
			item.EditControl = typeChooser;
			items.Add(item);
			item = new ScrollableListItem(Properties.Resources.StrClearDelay + ":\t" + clearBarcodeDelay + Properties.Resources.StrS,
											null, null, "clear_delay");
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
		/// <returns>null to pop to the previous screen</returns>
		public override ListScreen RightSoftKeyPressed()
		{
			// update the options
			Options.TransmitFormat = transmitFormat;
			Options.TransmitDelimiter = transmitDelimiter;
			Options.InterTransmitDelay = interTransmitDelay;
			Options.BarcodeType = barcodeType;
			Options.ClearBarcodeDelay = clearBarcodeDelay;

			// save the options
			Options.Save();

			// pop this screen
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
					case "transmit_delay":
						interTransmitDelay = (int)transmitDelayValidator.Value;
						break;
					case "clear_delay":
						clearBarcodeDelay = (int)clearDelayValidator.Value;
						break;
				}
			}

			// indicate that the selection can be changed
			return true;
		}

		/// <summary>
		/// increments and decrements the format value
		/// </summary>
		void formatChooser_IncDecValue(object sender, EventArgs e)
		{
			switch (transmitFormat)
			{
				case TransmitFormat.QuantityDelimiterBarcode:
					transmitFormat = TransmitFormat.NBarcodes;
					listForm.List.Items.Replace(listForm.List.Items.IndexOf(delimiterItem), delayItem);
					break;
				case TransmitFormat.NBarcodes:
					transmitFormat = TransmitFormat.QuantityDelimiterBarcode;
					listForm.List.Items.Replace(listForm.List.Items.IndexOf(delayItem), delimiterItem);
					break;
			}
			formatChooser.Text = TransmitFormatString;
		}

		/// <summary>
		/// increments the delimiter value
		/// </summary>
		void delimiterChooser_IncValue(object sender, EventArgs e)
		{
			switch (transmitDelimiter)
			{
				case TransmitDelimiter.Comma:
					transmitDelimiter = TransmitDelimiter.Semicolon;
					break;
				case TransmitDelimiter.Semicolon:
					transmitDelimiter = TransmitDelimiter.Tab;
					break;
				case TransmitDelimiter.Tab:
					transmitDelimiter = TransmitDelimiter.Comma;
					break;
			}
			delimiterChooser.Text = TransmitDelimiterString;
		}

		/// <summary>
		/// decrements the delimiter value
		/// </summary>
		void delimiterChooser_DecValue(object sender, EventArgs e)
		{
			switch (transmitDelimiter)
			{
				case TransmitDelimiter.Comma:
					transmitDelimiter = TransmitDelimiter.Tab;
					break;
				case TransmitDelimiter.Tab:
					transmitDelimiter = TransmitDelimiter.Semicolon;
					break;
				case TransmitDelimiter.Semicolon:
					transmitDelimiter = TransmitDelimiter.Comma;
					break;
			}
			delimiterChooser.Text = TransmitDelimiterString;
		}

		/// <summary>
		/// increments and decrements the barcode type value
		/// </summary>
		void typeChooser_IncDecValue(object sender, EventArgs e)
		{
			switch (barcodeType)
			{
				case LabelType.CODE128:
					barcodeType = LabelType.CODE39;
					break;
				case LabelType.CODE39:
					barcodeType = LabelType.CODE128;
					break;
			}
			typeChooser.Text = BarcodeTypeString;
		}

		/// <summary>
		/// gets a string representing the current transmit format
		/// </summary>
		private string TransmitFormatString
		{
			get
			{
				switch (transmitFormat)
				{
					case TransmitFormat.NBarcodes:
						return Properties.Resources.StrNBarcodes;
					default:
						return Properties.Resources.StrQuantityDelimiterBarcode;
				}
			}
		}

		/// <summary>
		/// gets a string representing the current transmit delimiter
		/// </summary>
		private string TransmitDelimiterString
		{
			get
			{
				switch (transmitDelimiter)
				{
					case TransmitDelimiter.Semicolon:
						return Properties.Resources.StrSemicolon;
					case TransmitDelimiter.Tab:
						return Properties.Resources.StrTab;
					default:
						return Properties.Resources.StrComma;
				}
			}
		}

		/// <summary>
		/// gets a string representing the current barcode type
		/// </summary>
		private string BarcodeTypeString
		{
			get
			{
				switch (barcodeType)
				{
					case LabelType.CODE39:
						return Properties.Resources.StrCode39;
					default:
						return Properties.Resources.StrCode128;
				}
			}
		}
	}
}
