//--------------------------------------------------------------------
// FILENAME: SmsRead.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION:		This file provides wrapper calls for receiving 
//					SMS calls. You must install file mapirule.dll
//					on the device to enable this feature.
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
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;

namespace WANSample
{
	public class SmsRead : IDisposable
	{
		#region IDisposable requirements

		/// <summary>
		/// Use C# destructor syntax for finalization code.
		/// This destructor will run only if the Dispose method does not get called.
		/// </summary>
		~SmsRead()
		{
			// Do not re-create Dispose clean-up code here.
			// Calling Dispose(false) is optimal in terms of
			// readability and maintainability.
			Dispose(false);
		}

		/// <summary>
		/// Dispose(bool disposing) executes in two distinct scenarios.
		///   If disposing equals true, the method has been called directly or indirectly 
		///     by user code. Managed and unmanaged resources can be disposed.
		///   If disposing equals false, the method has been called by the 
		///     runtime from inside the finalizer and you should not reference 
		///     other objects. Only unmanaged resources should be disposed.
		/// </summary>
		/// <param name="disposing">Called directly (true) or indirectly (false)</param>
		private void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!this.disposed)
			{
				// We don't really have managed resources to dispose, so just
				// remove the unmanaged resources, meaning getting rid
				// of the (managed) worker thread and freeing the used DLL.
				bDone = true;
                try
                {
                    TerminateSMSMessagePassing();
                }
                catch
                {
                }
			}
			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			// This object will be cleaned up by the Dispose method.
			// Therefore, you should call GC.SupressFinalize to
			// take this object off the finalization queue 
			// and prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}

		#endregion 		// IDisposable requirements

		#region SMSREAD enums, classes, structs

		public struct SMSREAD_CURRENT_DEVICE
		{
			public IntPtr curModuleHdl;
			public StringBuilder inMsg;
			public StringBuilder inPhone;
		}

		#endregion			// CONN enums, classes, structs

		#region Declarations

		private bool disposed = false;
		private bool bDone = false;
		public SMSREAD_CURRENT_DEVICE smsRdCurrentDevice;
		public event EventHandler smsMessageEvent;

		#endregion	// Declarations

		#region SMS READ low level calls

		/// <summary>
		/// Constructor does not do any operation. Instead OpenRead() initiates the read
		/// process.
		/// </summary>
		public SmsRead()
		{
			smsRdCurrentDevice.inMsg = new StringBuilder(255);
			smsRdCurrentDevice.inPhone = new StringBuilder(255);
		}

		/// <summary>
		/// OpenRead() loads the MapiRule.dll into the application's address space and 
		/// registers the dll server by calling into mapirule.dll.
		/// It creates the workhorse thread that receives the SMS messages sent from
		/// the unmanaged dll mapirule.dll
		/// </summary>
		public uint SMS_OpenRead()
		{
			uint uRet = 0;

			smsRdCurrentDevice.curModuleHdl = LoadLibrary("mapirule.dll");
			if (DllRegisterServer() != 0)
			{
				System.Windows.Forms.MessageBox.Show("Could not initialize the IMailRuleClient DLL.\n" + 
					"To receive SMS by this application, you have to first install MapiRule.dll.\n" + 
					"To install: Copy MapiRule.dll MapiRule.reg MapiRule.cpy from this folder\n" + 
					"to the platform folder and cold boot the terminal.");
			}
			else
			{
				bDone = false;
				CaptureSMSMessages();
				Thread workerThread = new Thread(new ThreadStart(CheckForData));
				workerThread.Start();
			}

			return uRet;
		}

		public void SMS_CloseRead()
		{
			DllUnregisterServer();
			FreeLibrary(smsRdCurrentDevice.curModuleHdl);
			smsRdCurrentDevice.curModuleHdl = IntPtr.Zero;
			Dispose();
		}

		/// <summary>
		/// This thread gets destroyed when the SmsRead object is disposed.
		/// The call SMSMessageAvailable in MapiRule.dll is executed in a while loop.
		/// The data received from the dll will be passed to the caller by raising an event.
		/// </summary>
		private void CheckForData()
		{
			// StringBuilder creates less overhead compared to the string class
			StringBuilder sbSms = new StringBuilder(255);
			StringBuilder sbPhoneNr = new StringBuilder(255);

			while (!bDone)
			{
				SMSMessageAvailable(smsRdCurrentDevice.inMsg, smsRdCurrentDevice.inPhone);

				if (smsRdCurrentDevice.inMsg.Length != 0)
					smsMessageEvent(this, null);
			}
		}

		#endregion		// SMS READ low level calls

		#region P/Invoke Calls
		[DllImport("mapirule.dll")]
		public static extern int SMSMessageAvailable(StringBuilder message, StringBuilder phoneNumber);

		[DllImport("mapirule.dll")]
		public static extern void CaptureSMSMessages();

		[DllImport("mapirule.dll")]
		public static extern void TerminateSMSMessagePassing();

		[DllImport("mapirule.dll")]
		public static extern int DllRegisterServer();

		[DllImport("mapirule.dll")]
		public static extern void DllUnregisterServer();

		[DllImport("coredll.dll")]
		public static extern IntPtr LoadLibrary(string libName);

		[DllImport("coredll.dll")]
		public static extern bool FreeLibrary(IntPtr hLibModule);

		#endregion  // P/Invoke Calls
	}
}
