using System;
using System.Collections.Generic;

namespace CS_MT2000_ScanInventory
{
	/// <summary>
	/// an inventory location with a list of items at that location
	/// </summary>
	public class InventoryLocation
	{
		public string Location = "";
		public InventoryItems Items;

		/// <summary>
		/// initializes the inventory location
		/// </summary>
		/// <param name="quantity">location of the items</param>
		public InventoryLocation(string location)
		{
			Location = location;
			Items = new InventoryItems();
		}
	}

	/// <summary>
	/// a collection of inventory locations
	/// </summary>
	public class InventoryLocations : SortedList<string, InventoryLocation> { }
}
