//--------------------------------------------------------------------
// FILENAME: Common.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION:		This file contains common calls.
//
// NOTES:			Refer to the readme.txt file for a description 
//					of using this file to create a WAN application.
//--------------------------------------------------------------------

//------------------------------------------------------------------------------------
//		I M P O R T A N T   D I S C L A I M E R
//
// This Software comes "as is", with no warranties. None whatsoever. This means no 
// express, implied or statutory warranty, including without limitation, warranties 
// of merchantability or fitness for a particular purpose or any warranty of title 
// or non-infringement. Also, you must pass this disclaimer on whenever you 
// distribute the Software or derivative works. 

// Neither Symbol nor any contributor to the Software will be liable for any of 
// those types of damages known as indirect, special, consequential, or incidental 
// related to the Software or this license, to the maximum extent the law permits, 
// no matter what legal theory it’s based on. Also, you must pass this limitation of 
// liability on whenever you distribute the Software or derivative works. 
//------------------------------------------------------------------------------------
using System;
using System.Text;
using System.Runtime.InteropServices;

namespace WANSample
{
	public class Common
	{
		#region COMMON enums, classes, structs

		public enum StringFormat : uint
		{
			STRINGFORMAT_ASCII = (uint)0x00000001,
			STRINGFORMAT_DBCS = (uint)0x00000002,
			STRINGFORMAT_UNICODE = (uint)0x00000003,
			STRINGFORMAT_BINARY = (uint)0x00000004
		}
		#endregion	// SMS enums, classes, structs

		#region Declarations

		const uint LMEM_FIXED = 0x0000;
		const uint LMEM_ZEROINIT = 0x0040;
		const uint LPTR = (LMEM_FIXED | LMEM_ZEROINIT);

		#endregion	// Declarations

		#region COMMON workhorse calls
		//Create wrappers for the memory API's similar to 
		//Marshal.AllocHGlobal and Marshal.FreeHGlobal 
		public IntPtr AllocHGlobal(int cb)
		{
			IntPtr hMemory = new IntPtr();
			hMemory = LocalAlloc(LPTR, (uint)cb);
			return hMemory;
		}

		public IntPtr FreeHGlobal(IntPtr hMemory)
		{
			IntPtr pRet = (IntPtr)1;

			if (hMemory != IntPtr.Zero)
				pRet = LocalFree(hMemory);

			return pRet;
		}

		public void PlaySound(string lpszName, IntPtr hModule)
		{
			PlaySoundW(lpszName, hModule, 0);
		}
		
		#endregion		// COMMON workhorse calls

		#region P/Invoke API Calls

		[DllImport("coredll.dll")]
		internal static extern IntPtr LocalAlloc(
			uint uFlags,
			uint uBytes);

		[DllImport("coredll.dll")]
		internal static extern IntPtr LocalFree(IntPtr hMem);


		[DllImport("coredll")]
		public static extern bool PlaySoundW(String lpszName, IntPtr hModule, uint
		dwFlags);

		#endregion P/Invoke API Calls

	}

}