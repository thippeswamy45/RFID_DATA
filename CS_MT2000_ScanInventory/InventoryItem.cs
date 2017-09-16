//--------------------------------------------------------------------
// FILENAME: InventoryItem.cs
//
// Copyright(c) 2009 Symbol Technologies Inc. All rights reserved.
//
// DESCRIPTION:
//      This module is part of the ScanInventory application.   It 
//      defines the internal structure of the saved Inventory data 
//      element.  
//
// NOTES:
//      This software is provided as is as an example of how to use the 
//      MT2000 Scanner services assemblies. 
//      
// 
//--------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace CS_MT2000_ScanInventory
{
	/// <summary>
	/// an inventory item
	/// </summary>
	public class InventoryItem
	{
		public InventoryLocation Location;
		public uint Quantity = 0;
		public string Barcode = "";

		/// <summary>
		/// initializes the inventory item
		/// </summary>
		/// <param name="quantity">location of the item</param>
		/// <param name="quantity">quantity of the item</param>
		/// <param name="barcode">barcode of the item</param>
		public InventoryItem(InventoryLocation location, uint quantity, string barcode)
		{
			Location = location;
			Quantity = quantity;
			Barcode = barcode;
		}
	}

	/// <summary>
	/// a collection of inventory items
	/// </summary>
	public class InventoryItems : SortedList<string, InventoryItem> { }
}
