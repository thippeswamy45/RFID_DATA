//--------------------------------------------------------------------
// FILENAME: Options.cs
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
using System.Xml;
using System.Text;

using Symbol.MT2000.Utils;

namespace CS_MT2000_ScanInventory
{
	/// <summary>
	/// defines the program options
	/// </summary>
	public static class Options
	{
		// public constants
		public const uint MaxQuantity = 99999;
		public const int MaxQuantityLength = 5;
		public const int MaxLocationLength = 8;
		public const uint MaxClearBarcodeDelay = 9;
		public const int MaxClearBarcodeDelayLength = 1;
		public const string RootPath = "\\Application\\Inventory";
        public static int AutoSaveCount = 10;       // 0 indicates that auto save is disabled. 

		// local constants
		private const string OptionsFileName = RootPath + "\\options.xml";

		// local variables
		private static string location = "";
		private static string exportFile = RootPath + "\\export";
		private static ExportFormat exportFormat = ExportFormat.CommaSeparated;
		private static bool groupedByLocation = false;
		private static uint clearBarcodeDelay = 2;
		private static bool changed = false;

		/// <summary>
		/// gets and sets the location
		/// </summary>
		public static string Location
		{
			get { return location; }
			set
			{
				if (value != location)
				{
					location = value;
					changed = true;
				}
			}
		}

		/// <summary>
		/// gets and sets the export file
		/// </summary>
		public static string ExportFile
		{
			get { return exportFile; }
			set
			{
				if (value != exportFile)
				{
					exportFile = value;
					changed = true;
				}
			}
		}

		/// <summary>
		/// gets and sets the export format
		/// </summary>
		public static ExportFormat ExportFormat
		{
			get { return exportFormat; }
			set
			{
				if (value != exportFormat)
				{
					exportFormat = value;
					changed = true;
				}
			}
		}

		/// <summary>
		/// gets and sets the "grouped by location" flag
		/// </summary>
		public static bool GroupedByLocation
		{
			get { return groupedByLocation; }
			set
			{
				if (value != groupedByLocation)
				{
					groupedByLocation = value;
					changed = true;
				}
			}
		}

		/// <summary>
		/// gets and sets the clear barcode delay
		/// </summary>
		public static uint ClearBarcodeDelay
		{
			get { return clearBarcodeDelay; }
			set
			{
				if (value != clearBarcodeDelay)
				{
					clearBarcodeDelay = value;
					changed = true;
				}
			}
		}

		/// <summary>
		/// gets the changed flag
		/// </summary>
		public static bool Changed
		{
			get { return changed; }
		}

		/// <summary>
		/// saves the profile to an XML file
		/// </summary>
		/// <returns>true if successful, false if not</returns>
		public static bool Save()
		{
			XmlTextWriter textWriter = null;

			try
			{
				// create the directory if necessary
				string path = Path.GetDirectoryName(OptionsFileName);
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}

				// open the writer
				textWriter = new XmlTextWriter(OptionsFileName, Encoding.UTF8);

				// start the document
				textWriter.Formatting = Formatting.Indented;
				textWriter.IndentChar = '\t';
				textWriter.Indentation = 1;
				textWriter.WriteStartDocument();

				// start the "options" element
				textWriter.WriteStartElement("options");

				// write the options
				Xml.WriteElement(textWriter, "location", location);
				Xml.WriteElement(textWriter, "export_file", exportFile);
				Xml.WriteElement(textWriter, "export_format", GetExportFormatString(exportFormat));
				Xml.WriteElement(textWriter, "grouped_by_location", groupedByLocation ? "yes" : "no");
				Xml.WriteElement(textWriter, "clear_barcode_delay", clearBarcodeDelay.ToString());

				// end the "options" element
				textWriter.WriteEndElement();

				// end the document
				textWriter.WriteEndDocument();

				// flush and close the writer
				textWriter.Flush();
				textWriter.Close();
				textWriter = null;

				// indicate success
				changed = false;
				return true;
			}
			catch
			{
				// indicate failure
				return false;
			}
			finally
			{
				if (textWriter != null)
				{
					textWriter.Close();
				}
			}
		}

		/// <summary>
		/// reads an Adhoc power level element
		/// </summary>
		/// <param name="node">node containing the element</param>
		/// <param name="name">name of the element</param>
		/// <param name="defaultValue">default value if it doesn't exist</param>
		/// <returns>the value read</returns>
		private static ExportFormat ReadExportFormatElement(XmlNode node, string name, ExportFormat defaultValue)
		{
			string strValue = Xml.ReadElement(node, name, GetExportFormatString(defaultValue)).ToLower();
			switch (strValue)
			{
				case "comma separated":
					return ExportFormat.CommaSeparated;
				case "semi-colon separated":
					return ExportFormat.SemicolonSeparated;
				case "tab separated":
					return ExportFormat.TabSeparated;
				default:
					return ExportFormat.XML;
			}
		}

		/// <summary>
		/// loads the options from an XML file
		/// </summary>
		/// <returns>true if successful, false if not</returns>
		public static bool Load()
		{
			XmlDocument doc;
			XmlTextReader reader = null;

			try
			{
				// create the XML reader
				reader = new System.Xml.XmlTextReader(OptionsFileName);

				// load the settings XML file
				doc = new XmlDocument();
				doc.Load(reader);

				// get the root node
				XmlNode rootNode = doc.SelectSingleNode("options");
				if (rootNode == null)
				{
					return false;
				}

				// read the options
				location = Xml.ReadElement(rootNode, "location", "");
				exportFile = Xml.ReadElement(rootNode, "export_file", RootPath + "\\export");
				exportFormat = ReadExportFormatElement(rootNode, "export_format", ExportFormat.CommaSeparated);
				groupedByLocation = Xml.ReadBooleanElement(rootNode, "grouped_by_location", false);
				clearBarcodeDelay = Xml.ReadUnsignedIntegerElement(rootNode, "clear_barcode_delay", 2);

				// indicate success
				return true;
			}
			catch
			{
				// indicate failure
                location = "default"; 
				return false;
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
				}
			}
		}

		/// <summary>
		/// gets a string representing an ExportFormat value
		/// </summary>
		/// <param name="option4">ExportFormat value to get a string for</param>
		/// <returns>string representing the ExportFormat value</returns>
		private static string GetExportFormatString(ExportFormat exportFormat)
		{
			switch (exportFormat)
			{
				case ExportFormat.CommaSeparated:
					return "Comma Separated";
				case ExportFormat.SemicolonSeparated:
					return "Semi-colon Separated";
				case ExportFormat.TabSeparated:
					return "Tab Separated";
				default:
					return "XML";
			}
		}
	}

	/// <summary>
	/// export file formats
	/// </summary>
	public enum ExportFormat
	{
		CommaSeparated,
		SemicolonSeparated,
		TabSeparated,
		XML
	}
}
