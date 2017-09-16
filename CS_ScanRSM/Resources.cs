//--------------------------------------------------------------------
// FILENAME: Resources.cs
//
// Copyright � 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION:
//
// NOTES:
//
// 
//--------------------------------------------------------------------

using System.Globalization;
using System.Resources;


namespace CS_ScanRSM
{
	internal class Resources
	{
		static System.Resources.ResourceManager m_rmNameValues;

		static Resources()
		{
			m_rmNameValues= new System.Resources.ResourceManager(
				"CS_ScanRSM.Resources", typeof(Resources).Assembly);
		}

		public static string GetString(string name)
		{
			return m_rmNameValues.GetString(name);
		}
	}
}
