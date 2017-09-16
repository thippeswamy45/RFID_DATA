//--------------------------------------------------------------------
// FILENAME: Conn.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION:		This file provides wrapper calls for using.
//					connection manager.
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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace WANSample
{
	public class Conn : IDisposable
	{
		#region IDisposable requirements
		/// <summary>
		/// Boolean used to indicate if the object has been disposed
		/// </summary>
		private bool bDisposed = false;

		// Use C# destructor syntax for finalization code.
		// This destructor will run only if the Dispose method 
		// does not get called.
		// It gives your base class the opportunity to finalize.
		// Do not provide destructors in types derived from this class.
		~Conn()
		{
			// Do not re-create Dispose clean-up code here.
			// Calling Dispose() is optimal in terms of
			// readability and maintainability.
			Dispose();
		}

		/// <summary>
		/// Releases the resources used by the object.
		/// </summary>
		public void Dispose()
		{
			if (!bDisposed)
			{
				bDisposed = true;

				// SupressFinalize to take this object off the finalization queue 
				// and prevent finalization code for this object from executing a second time.
				GC.SuppressFinalize(this);
			}
		}
		#endregion	// IDisposable requirements

		#region CONN enums, classes, structs

		public enum CONNECTIONPRIORITY : int
		{
			Voice = 0x20000,
			UserInteractive = 0x08000,
			UserBackground = 0x02000,
			UserIdle = 0x0800,
			HighPriorityBackground = 0x0200,
			IdleBackground = 0x0080,
			ExternalInteractive = 0x0020,
			LowBackground = 0x0008,
			Cached = 0x0002,
		}

		public enum CONNECTIONSTATUS : uint
		{
			CONNMGR_STATUS_UNKNOWN = 0x00,				// Unknown status
			CONNMGR_STATUS_CONNECTED = 0x10,			// Connection is up
			CONNMGR_STATUS_DISCONNECTED = 0x20,			// Connection is disconnected
			CONNMGR_STATUS_CONNECTIONFAILED = 0x21,		// Connection failed and cannot not be reestablished
			CONNMGR_STATUS_CONNECTIONCANCELED = 0x22,	// User aborted connection
			CONNMGR_STATUS_CONNECTIONDISABLED = 0x23,	// Connection is ready to connect but disabled
			CONNMGR_STATUS_NOPATHTODESTINATION = 0x24,  // No path could be found to destination
			CONNMGR_STATUS_WAITINGFORPATH = 0x25,		// Waiting for a path to the destination
			CONNMGR_STATUS_WAITINGFORPHONE = 0x26,		// Voice call is in progress
			CONNMGR_STATUS_WAITINGCONNECTION = 0x40,	// Attempting to connect
			CONNMGR_STATUS_WAITINGFORRESOURCE = 0x41,	// Resource is in use by another connection
			CONNMGR_STATUS_WAITINGFORNETWORK = 0x42,	// No path could be found to destination
			CONNMGR_STATUS_WAITINGDISCONNECTION = 0x80, // Connection is being brought down
			CONNMGR_STATUS_WAITINGCONNECTIONABORT = 0x81// Aborting connection attempt
		}

		internal struct CONNMGR_CONNECTIONINFO
		{
			public int cbSize;
			public int dwParams;
			public int dwFlags;
			public int dwPriority;
			public int bExclusive;
			public int bDisabled;
			public Guid guidDestNet;
			public IntPtr hWnd;
			public int uMsg;
			public int lParam;
			int ulMaxCost;
			int ulMinRcvBw;
			int ulMaxConnLatency;

			/// <summary>
			/// Writes the ConnectionInfo data to unmanaged memory.
			/// </summary>
			/// <returns>A pointer to the unmanaged memory block storing the ConnectionInfo data</returns>		
			public IntPtr StructureToPtr()
			{
				ulMaxCost = 0;
				ulMinRcvBw = 0;
				ulMaxConnLatency = 0;
				int offset = 0;

				IntPtr ptr = myCommon.AllocHGlobal(Marshal.SizeOf(typeof(CONNMGR_CONNECTIONINFO)));
				if (ptr == IntPtr.Zero) return ptr;

				Marshal.WriteInt32(ptr, offset, this.cbSize);
				offset += Marshal.SizeOf(typeof(int));
				Marshal.WriteInt32(ptr, offset, this.dwParams);
				offset += Marshal.SizeOf(typeof(int));
				Marshal.WriteInt32(ptr, offset, this.dwFlags);
				offset += Marshal.SizeOf(typeof(int));
				Marshal.WriteInt32(ptr, offset, this.dwPriority);
				offset += Marshal.SizeOf(typeof(int));

                Marshal.WriteInt32(ptr, offset, this.bExclusive);
				// Do not use the sizeof a .Net boolean type in place BOOL even though they appear same.  
				// You must use the sizeof a signed int (32 bit) which matches Win32 BOOL type
				offset += Marshal.SizeOf(typeof(int));
                Marshal.WriteInt32(ptr, offset, this.bDisabled);
				// Do not use the sizeof a .Net boolean type in place BOOL even though they appear same.  
				// You must use the sizeof a signed int (32 bit) which matches Win32 BOOL type
				offset += Marshal.SizeOf(typeof(int));

				WriteByteArray(ptr, offset, this.guidDestNet.ToByteArray());
				offset += Marshal.SizeOf(typeof(Guid));

				// IntPtr is treated as int
				Marshal.WriteInt32(ptr, offset, this.hWnd.ToInt32());
				offset += Marshal.SizeOf(typeof(int));
				Marshal.WriteInt32(ptr, offset, this.uMsg);
				offset += Marshal.SizeOf(typeof(int));
				Marshal.WriteInt32(ptr, offset, this.lParam);
				offset += Marshal.SizeOf(typeof(int));
				Marshal.WriteInt32(ptr, offset, this.ulMaxCost);
				offset += Marshal.SizeOf(typeof(int));
				Marshal.WriteInt32(ptr, offset, this.ulMinRcvBw);
				offset += Marshal.SizeOf(typeof(int));
				Marshal.WriteInt32(ptr, offset, this.ulMaxConnLatency);

				return ptr;
			}

			public static void WriteBool(IntPtr ptr, int offset, bool val)
			{
				byte[] data = BitConverter.GetBytes(val);
				Marshal.Copy(data, 0, new IntPtr(ptr.ToInt32() + offset), data.Length);
			}

			public static void WriteByteArray(IntPtr ptr, int offset, byte[] val)
			{
				Marshal.Copy(val, 0, new IntPtr(ptr.ToInt32() + offset), val.Length);
			}

			/// <summary>
			/// Disposes of the ConnectionInfo object.
			/// </summary>
			public void Dispose(IntPtr ptr)
			{
				myCommon.FreeHGlobal(ptr);
				ptr = IntPtr.Zero;
			}
		};

		public struct CONNCURRENTDEVICE
		{
			public IntPtr connHandle;
		}


		#endregion	//CONN enums, classes, structs

		#region Declarations

		private CONNCURRENTDEVICE conCurrentDevice;
		private static WANSample.Common myCommon;

		#endregion	// Declarations

		#region CONN low level calls

		public uint CONN_IsConnected()
		{
			uint connectionStatus = (uint)CONNECTIONSTATUS.CONNMGR_STATUS_UNKNOWN;

			ConnMgrConnectionStatus(conCurrentDevice.connHandle, out connectionStatus);

			return connectionStatus;
		}

		public uint CONN_Connect()
		{
			const int CONNMGR_PARAM_GUIDDESTNET = (0x1);
			const int WM_APP_CONNMGR = 0x400 + 0;
            const uint CONNMGR_CONNECTION_TIMEOUT_MSECS = 60000;
			uint uConnectionStatus = (uint)CONNECTIONSTATUS.CONNMGR_STATUS_UNKNOWN;

			CONNMGR_CONNECTIONINFO connectionInfo = new CONNMGR_CONNECTIONINFO();
			connectionInfo.cbSize = Marshal.SizeOf(connectionInfo);
			connectionInfo.dwParams = CONNMGR_PARAM_GUIDDESTNET;
			connectionInfo.dwPriority = (int)CONNECTIONPRIORITY.UserBackground;
			connectionInfo.dwFlags = 0;
			connectionInfo.bExclusive = 1;//true
			connectionInfo.bDisabled = 0;//false

			Guid IID_DestNetInternet = new Guid("436ef144-b4fb-4863-a041-8f905a62c572");
			connectionInfo.guidDestNet = IID_DestNetInternet;
			connectionInfo.hWnd = IntPtr.Zero;
			connectionInfo.uMsg = WM_APP_CONNMGR;
			connectionInfo.lParam = 0;

			myCommon = new WANSample.Common();

			IntPtr pConnectionInfo = connectionInfo.StructureToPtr();

			ConnMgrEstablishConnectionSync(pConnectionInfo, out conCurrentDevice.connHandle,
                CONNMGR_CONNECTION_TIMEOUT_MSECS, out uConnectionStatus);

			connectionInfo.Dispose(pConnectionInfo);

            return uConnectionStatus;
		}

		public void CONN_Disconnect()
		{
			ConnMgrReleaseConnection(conCurrentDevice.connHandle, Convert.ToInt32(false));
			conCurrentDevice.connHandle = IntPtr.Zero;
		}

		#endregion			// CONN Low Level Calls

		#region P/Invoke API Calls

		[DllImport("cellcore.dll")]
		internal extern static uint ConnMgrConnectionStatus(
			IntPtr hConnection,
			out uint pdwStatus);

		[DllImport("cellcore.dll")]
		internal extern static uint ConnMgrEstablishConnectionSync(
			IntPtr pConnInfo,
			out IntPtr phConnection,
			uint dwTimeout,
			out uint dwStatus);

		[DllImport("cellcore.dll")]
		internal static extern void ConnMgrReleaseConnection(IntPtr hConnection, int bCache);

		#endregion	// P/Invoke API Calls

	}
}