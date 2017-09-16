//--------------------------------------------------------------------
// FILENAME: Inventory.cs
//
// Copyright(c) 2009 Symbol Technologies Inc. All rights reserved.
//
// DESCRIPTION:
//      This module is part of the ScanInventory application.   It 
//      manages the internal storage of inventory data along with 
//      methods to save / load / export and transmit the data to the 
//      host PC. 
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
using System.Collections.Generic;

using Symbol.MT2000.Utils;
using Symbol.MT2000.ScannerServices;

namespace CS_MT2000_ScanInventory
{
	public static class Inventory
	{
		// local constants
		private const string InventoryFileName = "\\Application\\Inventory\\inventory.dat";

		// public variables
		public static InventoryLocations Locations = new InventoryLocations();
		public static bool Changed = false;
        public static int ItemsQueuedForSave = 0; 

		/// <summary>
		/// saves the inventory to a file
		/// </summary>
		public static bool Save()
		{
			StreamWriter writer = null;
			bool result = true;

			try
			{
				// create the directory if necessary
				string path = Path.GetDirectoryName(InventoryFileName);
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}

				// write the inventory to the file
				FileInfo fileInfo = new FileInfo(InventoryFileName);
				writer = fileInfo.CreateText();
				foreach (KeyValuePair<string, InventoryLocation> kvpLocations in Locations)
				{
					InventoryLocation location = kvpLocations.Value;
					writer.WriteLine(location.Items.Count.ToString() + " " + location.Location);
					foreach (KeyValuePair<string, InventoryItem> kvpItems in location.Items)
					{
						InventoryItem item = kvpItems.Value;
						writer.WriteLine(item.Quantity.ToString() + " " + item.Barcode);
					}
				}
			}
			catch
			{
				result = false;
			}
			finally
			{
				if (writer != null)
				{
					writer.Close();
				}
			}

			// clear the changed flag and return the result
			Changed = false;
            ItemsQueuedForSave = 0;
			return result;
		}

		/// <summary>
		/// loads the inventory from a file
		/// </summary>
		public static bool Load()
		{
			StreamReader reader = null;
			bool result = true;

			try
			{
				// clear the current inventory and open the file
				Locations.Clear();
				reader = File.OpenText(InventoryFileName);
				while (!reader.EndOfStream)
				{
					uint numLocations;
					string value;
					string line;

					// read the location
					line = reader.ReadLine();
					if (!ParseLine(line, out numLocations, out value, Options.MaxLocationLength))
					{
						result = false;
						break;
					}
					InventoryLocation location = new InventoryLocation(value);

					// read the location items
					for (uint j = 0; j < numLocations; j++)
					{
						uint quantity;

						if (reader.EndOfStream)
						{
							result = false;
							break;
						}
						line = reader.ReadLine();
						if (!ParseLine(line, out quantity, out value, 0))
						{
							result = false;
							break;
						}
						InventoryItem item = new InventoryItem(location, quantity, value);
						location.Items.Add(value, item);
					}
					if (!result)
					{
						break;
					}

					// add the location and items to the inventory
					Locations.Add(location.Location, location);
				}
			}
			catch
			{
				result = false;
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
				}
			}

			// clear the changed flag and return the result
			Changed = false;
			return result;
		}

		/// <summary>
		/// parses a line of the format "count value"
		/// </summary>
		/// <param name="line">line to be parsed</param>
		/// <param name="count">output count as parsed from the line</param>
		/// <param name="value">output value as parsed from the line</param>
		/// <param name="maxValueLength">maximum value length (0 if there is no maximum)</param>
		/// <returns>true if successful, false if not</returns>
		private static bool ParseLine(string line, out uint count, out string value, int maxValueLength)
		{
			count = 0;
			value = null;
			try
			{
				int i = line.IndexOf(' ');
				if (i == -1)
				{
					return false;
				}
				count = uint.Parse(line.Substring(0, i));
				value = line.Substring(i + 1);
				if (string.IsNullOrEmpty(value))
				{
					return false;
				}
				if (maxValueLength != 0 && value.Length > maxValueLength)
				{
					return false;
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// adds a new item or updates the count of an existing one
		/// </summary>
		/// <param name="location">location of the item to be added/incremented</param>
		/// <param name="quantity">quantity of the item to be added/incremented</param>
		/// <param name="barcode">barcode of the item to be added/incremented</param>
		public static InventoryItem AddItem(string location, uint quantity, string barcode)
		{
			// get an existing location or add a new one
			InventoryLocation loc;
			if (Locations.ContainsKey(location))
			{
				loc = Inventory.Locations[location];
			}
			else
			{
				loc = new InventoryLocation(location);
				Inventory.Locations.Add(location, loc);
			}

			// increment an existing item count or add a new item
			InventoryItem item;
			if (loc.Items.ContainsKey(barcode))
			{
				item = loc.Items[barcode];
				item.Quantity += quantity;
			}
			else
			{
				item = new InventoryItem(loc, quantity, barcode);
				loc.Items.Add(barcode, item);
			}

			// mark the inventory as changed
			Changed = true;

            if ((Options.AutoSaveCount > 0) && (Options.AutoSaveCount < ItemsQueuedForSave++)) {
                Inventory.Save();
            }

			// return the new or updated inventory item
			return item;
		}

		/// <summary>
		/// deletes a single item from the inventory
		/// </summary>
		/// <param name="item">item to be deleted</param>
		public static void DeleteItem(InventoryItem item)
		{
			item.Location.Items.Remove(item.Barcode);
			if (item.Location.Items.Count == 0)
			{
				Locations.Remove(item.Location.Location);
			}
			Changed = true;
		}

		/// <summary>
		/// deletes all the inventory items
		/// </summary>
		public static void DeleteAllItems()
		{
			Locations.Clear();
			Changed = true;
		}

		/// <summary>
		/// exports the inventory to a text or XML file
		/// </summary>
		/// <param name="fileName">name of the files to export to</param>
		/// <param name="exportFormat">format of the export file</param>
		/// <param name="groupByLocation">true to group the items by location</param>
		/// <param name="fullFileName">full path and name of the file that the inventory was written to</param>
		/// <returns>true if successful, false if not</returns>
		public static bool Export(string fileName, ExportFormat exportFormat, bool groupByLocation, out string fullFileName)
		{
			// create the directory if necessary
			string path = Path.GetDirectoryName(fileName);
			if (string.IsNullOrEmpty(path))
			{
				path = Options.RootPath;
				fileName = path + Path.DirectorySeparatorChar + fileName;
			}
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			// get the file name without the extension
			if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
			{
				path += Path.DirectorySeparatorChar;
			}
			fullFileName = path + Path.GetFileNameWithoutExtension(fileName);

			// export the inventory to an XML file
			if (exportFormat == ExportFormat.XML)
			{
				fullFileName += ".xml";
				return ExportXML(fullFileName, groupByLocation);
			}

			// export the inventory to a text file
			else
			{
				fullFileName += ".txt";
				return ExportText(fullFileName, exportFormat, groupByLocation);
			}
		}

		/// <summary>
		/// exports the inventory to an XML file
		/// </summary>
		/// <param name="fileName">name of the files to export to</param>
		/// <param name="exportFormat">format of the export file</param>
		/// <param name="groupByLocation">true to group the items by location</param>
		/// <returns>true if successful, false if not</returns>
		private static bool ExportText(string fileName, ExportFormat exportFormat, bool groupByLocation)
		{
			StreamWriter writer = null;
			char separator = ',';
			bool result = true;

			// get the separator character
			switch (exportFormat)
			{
				case ExportFormat.CommaSeparated:
					separator = ',';
					break;
				case ExportFormat.SemicolonSeparated:
					separator = ';';
					break;
				case ExportFormat.TabSeparated:
					separator = '\t';
					break;
			}

			// write the inventory to the text file
			try
			{
				FileInfo fileInfo = new FileInfo(fileName);
				writer = fileInfo.CreateText();
				foreach (KeyValuePair<string, InventoryLocation> kvpLocations in Locations)
				{
					InventoryLocation loc = kvpLocations.Value;
					if (groupByLocation)
					{
						writer.WriteLine(loc.Location);
					}
					foreach (KeyValuePair<string, InventoryItem> kvpItems in loc.Items)
					{
						InventoryItem item = kvpItems.Value;
						writer.WriteLine((groupByLocation ? "\t" : (loc.Location + separator)) +
                                             item.Quantity.ToString() + separator + item.Barcode);
					}
				}
			}
			catch
			{
				result = false;
			}
			finally
			{
				if (writer != null)
				{
					writer.Close();
				}
			}
			return result;
		}

		/// <summary>
		/// exports the inventory to an XML file
		/// </summary>
		/// <param name="fileName">name of the file to export to</param>
		/// <param name="groupByLocation">true to group the items by location</param>
		/// <returns>true if successful, false if not</returns>
		private static bool ExportXML(string fileName, bool groupByLocation)
		{
			XmlTextWriter textWriter = null;

			try
			{
				// open the writer
				textWriter = new XmlTextWriter(fileName, Encoding.UTF8);

				// start the document
				textWriter.Formatting = Formatting.Indented;
				textWriter.IndentChar = '\t';
				textWriter.Indentation = 1;
				textWriter.WriteStartDocument();

				// start the "inventory" element
				textWriter.WriteStartElement("inventory");

				// write the items
				foreach (KeyValuePair<string, InventoryLocation> kvpLocations in Locations)
				{
					InventoryLocation loc = kvpLocations.Value;
					if (groupByLocation)
					{
						textWriter.WriteStartElement("location");
						Xml.WriteElement(textWriter, "location", loc.Location);
					}
					foreach (KeyValuePair<string, InventoryItem> kvpItems in loc.Items)
					{
						InventoryItem item = kvpItems.Value;
						textWriter.WriteStartElement("item");
						if (!groupByLocation)
						{
							Xml.WriteElement(textWriter, "location", loc.Location);
						}
						Xml.WriteElement(textWriter, "barcode", item.Barcode);
						Xml.WriteElement(textWriter, "quantity", item.Quantity.ToString());
						textWriter.WriteEndElement();
					}
					if (groupByLocation)
					{
						textWriter.WriteEndElement();
					}
				}

				// end the "inventory" element
				textWriter.WriteEndElement();

				// end the document
				textWriter.WriteEndDocument();

				// flush and close the writer
				textWriter.Flush();
				textWriter.Close();
				textWriter = null;

				// indicate success
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
		/// transmits the inventory from a file to the host computer
		/// </summary>
		/// <param name="fileName">name of the files to export to</param>
		/// <returns>true if successful, false if not</returns>
		public static bool Transmit(string fileName)
		{
			try
			{
                if (Program.useSendRaw)
                {
                    byte[] data = Misc.ReadAllBytes(fileName);
                    Symbol.MT2000.ScannerServices.RawData rawData = new Symbol.MT2000.ScannerServices.RawData(data, data.Length, 1);
                    RESULTCODE result = Program.ScannerServicesClient.SendRawData(rawData, 30000);
                    return result == RESULTCODE.E_OK;
                }
                else
                {
                    char separator = ',';

			        // get the separator character
                    switch (Options.ExportFormat)
			        {
				        case ExportFormat.CommaSeparated:
					        separator = ',';
					        break;
				        case ExportFormat.SemicolonSeparated:
					        separator = ';';
					        break;
				        case ExportFormat.TabSeparated:
					        separator = '\t';
					        break;
			        }

                    foreach (KeyValuePair<string, InventoryLocation> kvpLocations in Locations)
                    {
                        InventoryLocation loc = kvpLocations.Value;
                        string Entry; 
                        
                        foreach (KeyValuePair<string, InventoryItem> kvpItems in loc.Items)
                        {
                            InventoryItem item = kvpItems.Value;
                            Entry = loc.Location + separator + item.Quantity.ToString() + separator + item.Barcode +'\n';

                            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

                            Symbol.MT2000.ScannerServices.LabelData labelData = new Symbol.MT2000.ScannerServices.LabelData(encoding.GetBytes(Entry), encoding.GetByteCount(Entry), Symbol.MT2000.ScannerServices.LabelType.CODE128);
                            RESULTCODE result = Program.ScannerServicesClient.SendLabel(labelData, 30000);
                            if (result != RESULTCODE.E_OK)
                            {
                                return false; 
                            }                            
                        }
                    }
                }
			}
			catch
			{
				return false;
			}
            return true; 
		}
	}
}
