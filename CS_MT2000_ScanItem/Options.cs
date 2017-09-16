//--------------------------------------------------------------------
// FILENAME: Options.cs
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
using Microsoft.Win32;

using Symbol.MT2000.ScannerServices;

namespace CS_MT2000_ScanItem
{
	/// <summary>
	/// defines the program options
	/// </summary>
	public static class Options
	{
		// public constants
		public const uint MaxQuantity = 99999;
		public const int MaxQuantityLength = 5;
		public const uint MaxInterTransmitDelay = 99;
		public const int MaxInterTransmitDelayLength = 2;
		public const uint MaxClearBarcodeDelay = 9;
		public const int MaxClearBarcodeDelayLength = 1;

		// public properties
		public static LabelType BarcodeType = LabelType.CODE128;
		public static TransmitFormat TransmitFormat = TransmitFormat.NBarcodes;
		public static TransmitDelimiter TransmitDelimiter = TransmitDelimiter.Comma;
		public static int InterTransmitDelay = 10;
		public static int ClearBarcodeDelay = 2;

		// registry key and value names
		private const string ScanItemKey = "Software\\Symbol\\ScanItem\\";
		private const string BarcodeTypeValue = "BarcodeType";
		private const string TransmitFormatValue = "TransmitFormat";
		private const string TransmitDelimiterValue = "TransmitDelimiter";
		private const string InterTransmitDelayValue = "InterTransmitDelay";
		private const string ClearBarcodeDelayValue = "ClearBarcodeDelay";

		/// <summary>
		/// load the options from the registry
		/// </summary>
		public static void Load()
		{
			// open the registry key for reading
			RegistryKey key = Registry.LocalMachine.OpenSubKey(ScanItemKey);
			if (key != null)
			{
				int value;

				// get the barcode type
				try
				{
					value = (int)key.GetValue(BarcodeTypeValue, LabelType.CODE128);
					BarcodeType = (LabelType)value;
				}
				catch
				{
					BarcodeType = LabelType.CODE128;
				}

				// get the transmit format
				try
				{
					value = (int)key.GetValue(TransmitFormatValue, TransmitFormat.NBarcodes);
					TransmitFormat = (TransmitFormat)value;
				}
				catch
				{
					TransmitFormat = TransmitFormat.NBarcodes;
				}

				// get the transmit delimiter
				try
				{
					value = (int)key.GetValue(TransmitDelimiterValue, TransmitDelimiter.Comma);
					TransmitDelimiter = (TransmitDelimiter)value;
				}
				catch
				{
					TransmitDelimiter = TransmitDelimiter.Comma;
				}

				// get the inter-transmit delay
				try
				{
					InterTransmitDelay = (int)key.GetValue(InterTransmitDelayValue, 10);
				}
				catch
				{
					InterTransmitDelay = 10;
				}

				// get the clear barcode delay
				try
				{
					ClearBarcodeDelay = (int)key.GetValue(ClearBarcodeDelayValue, 2);
				}
				catch
				{
					ClearBarcodeDelay = 2;
				}
				
				// close the registry key
				key.Close();
			}
			else
			{
				BarcodeType = LabelType.CODE128;
				TransmitFormat = TransmitFormat.NBarcodes;
				TransmitDelimiter = TransmitDelimiter.Comma;
				InterTransmitDelay = 10;
				ClearBarcodeDelay = 2;
			}
		}

		/// <summary>
		/// save the options to the registry
		/// </summary>
		/// <returns>true if successful, false if not</returns>
		public static bool Save()
		{
			// open the registry key for writing
			RegistryKey key = Registry.LocalMachine.CreateSubKey(ScanItemKey);
			if (key != null)
			{
				// write the registry values
				key.SetValue(BarcodeTypeValue, (int)BarcodeType, RegistryValueKind.DWord);
				key.SetValue(TransmitFormatValue, (int)TransmitFormat, RegistryValueKind.DWord);
				key.SetValue(TransmitDelimiterValue, (int)TransmitDelimiter, RegistryValueKind.DWord);
				key.SetValue(InterTransmitDelayValue, InterTransmitDelay, RegistryValueKind.DWord);
				key.SetValue(ClearBarcodeDelayValue, ClearBarcodeDelay, RegistryValueKind.DWord);

				// close the registry key
				key.Close();

				// indicate success
				return true;
			}

			// indicate failure
			return false;
		}
	}

	/// <summary>
	/// transmit modes
	/// </summary>
	public enum TransmitFormat
	{
		NBarcodes,
		QuantityDelimiterBarcode
	}

	/// <summary>
	/// transmit delimiters
	/// </summary>
	public enum TransmitDelimiter
	{
		Comma,
		Semicolon,
		Tab
	}
}
