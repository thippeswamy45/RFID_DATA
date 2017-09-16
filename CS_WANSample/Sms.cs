//--------------------------------------------------------------------
// FILENAME: Sms.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION:		This file provides wrapper calls around native 
//					SMS calls. 
//
// NOTES:			Refer to the readme.txt file for a description 
//					of using this file to create a WAN application.
//					These calls can only be used for sending SMS 
//					messages. To receive SMS messages, you must use
//					the file SmsRead.cs.
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
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;

namespace WANSample
{
	public class Sms : IDisposable
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
		~Sms()
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

		#region SMS enums, classes, structs

		/// <summary>
		/// This structure represents an SMS source or destination address 
		/// used by SmsSendMessage
		/// </summary>
		public struct SMS_ADDRESS
		{
			public SMS_ADDRESS_TYPE smsatAddressType;
			public string address;

			const int SMS_MAX_ADDRESS_LENGTH = (256 + 1)*2;

			public IntPtr StructureToPtr()
			{
				int offset = 0;

				IntPtr ptr = myCommon.AllocHGlobal(Marshal.SizeOf(typeof(int)) + SMS_MAX_ADDRESS_LENGTH + 2);
				if (ptr == IntPtr.Zero) return ptr;

				Marshal.WriteInt32(ptr, (int)smsatAddressType);
				offset += Marshal.SizeOf(typeof(int));

				byte[] bAddress = System.Text.Encoding.Unicode.GetBytes(this.address);
				Marshal.Copy(bAddress, 0, new IntPtr(ptr.ToInt32() + offset), bAddress.Length);

				return ptr;
			}

			public void Dispose(IntPtr ptr)
			{
				myCommon.FreeHGlobal(ptr);
				ptr = IntPtr.Zero;
			}
		}

		/// <summary>The possible return values of all the SMS calls.</summary>
		public enum SMSCALLRETURN : uint
		{
			SMS_OK = 0,

			E_UNEXPECTED = 0x8000FFFF,
			E_NOTIMPL = 0x80004001,
			E_OUTOFMEMORY = 0x8007000E,
			E_INVALIDARG = 0x80070057,
			E_NOINTERFACE = 0x80004002,
			E_POINTER = 0x80004003,
			E_HANDLE = 0x80070006,
			E_ABORT = 0x80004004,
			E_FAIL = 0x80004005,
			E_ACCESSDENIED = 0x80070005,
			E_PENDING = 0x8000000A,

			// Specific registration errors (for SmsSetMessageNotification, SmsClearMessageNotification)
			SMS_E_INVALIDPROTOCOL = 0x0001,
			SMS_E_REGISTRATIONEXISTS = 0x0002,
			SMS_S_NOSUCHREGISTRATION = 0x0003,

			// Specific messaging errors (for SmsOpen, SmsSendMessage, SmsGetSMSC, etc.)
			SMS_E_TOOMUCHDATA  = 0x0100,
			SMS_E_INVALIDDATA = 0x0101,
			SMS_E_BUFFERTOOSMALL = 0x0102,
			SMS_E_PROVIDERSPECIFICBUFFERWRONGSIZE = 0x0103,
			SMS_E_TIMEUNAVAILABLE = 0x0104,
			SMS_E_RECEIVEHANDLEALREADYOPEN  = 0x0105,
			SMS_E_DESTINATIONOUTOFSVC = 0x0106,
			SMS_E_INVALIDADDRESS = 0x0107,
			SMS_E_MSGBARREDBYOPERATOR = 0x0108,
			SMS_E_MSGCALLBARRED = 0x0109,
			SMS_E_NOSCSUBSCRIPTION = 0x010a,
			SMS_E_SCBUSY = 0x010b,
			SMS_E_SVCNOTSUBSCRIBED = 0x010c,
			SMS_E_UNASSIGNEDNUMBER = 0x010d,
			SMS_E_UNKNOWNSCADDRESS = 0x010e,
			SMS_E_UNIDENTIFIEDSUBCRIBER = 0x010f,

			// General SMS messaging errors
			SMS_E_MISC = 0x0200,
			SMS_E_PASSWORD = 0x0201,
			SMS_E_SIM = 0x0202,
			SMS_E_NETWORKACCESS = 0x0203,
			SMS_E_NETWORK = 0x0204,
			SMS_E_MOBILE = 0x0205,
			SMS_E_NETWORKUNSUPPORTED = 0x0206,
			SMS_E_MOBILEUNSUPPORTED = 0x0207,
			SMS_E_BADPARAM = 0x0208,
			SMS_E_STORAGE = 0x0209,
			SMS_E_SMSC = 0x020a,
			SMS_E_DESTINATION = 0x020b,
			SMS_E_DESTINATIONUNSUPPORTED = 0x020c,
			SMS_E_RADIOUNAVAILABLE = 0x020d
		}

		/// <summary>
		/// This enumeration specifies the data encoding that is primarily used for outgoing 
		/// text message types in SmsSendMessage calls. 
		/// </summary>
		private enum SMS_DATA_ENCODING
		{
			SMSDE_OPTIMAL = 0,
			SMSDE_GSM,
			SMSDE_UCS2,
		}

		/// <summary>
		/// This enumeration identifies the phone number type specified in the 
		/// SMS_ADDRESS structure when a message is sent by calling SmsSendMessage
		/// </summary>
		public enum SMS_ADDRESS_TYPE
		{
			/// <summary>Unknown phone number type.</summary>
			Unknown,
			/// <summary>International phone number.</summary>
			International,
			/// <summary>National phone number.</summary>
			National,
			/// <summary>Network-specific phone number.</summary>
			NetworkSpecific,
			/// <summary>Subscriber phone number.</summary>
			Subscriber,
			/// <summary>Alphanumeric phone number.</summary>
			Alphanumeric,
			/// <summary>Abbreviated phone number.</summary>
			Abbreviated
		}

		/// <summary>
		/// Holds the handles and other SMS related information 
		/// created by this module.  This is not a windows structure.
		/// </summary>
		public struct SMS_CURRENT_DEVICE
		{
			public IntPtr smsHandle;
			public String phoneNumber;
			public IntPtr smsRcvHandle;
		}

		#endregion	//SMS enums, classes, structs

		#region Declarations

		// Text message type
		private const String SMS_MSGTYPE_TEXT = "Microsoft Text SMS Protocol";
		private static Common myCommon = new Common();

		// dwMessageModes for SmsOpen
		private const uint SMS_MODE_RECEIVE = 0x00000001;
		private const uint SMS_MODE_SEND = 0x00000002;
		private const uint SMS_OPTION_DELIVERY_NONE = 0x00000000;
		private const uint SMS_OPTION_DELIVERY_NO_RETRY = 0x00000001;
		private const int PS_MESSAGE_OPTION_NONE = 0x00000000;

		SMS_CURRENT_DEVICE smsCurrentDevice;

		#endregion	// Declarations

		#region SMS low level calls

		/// <summary>
		/// Opens the SMS Messaging component for send access.
		/// </summary>
		/// <returns>Success returns SMSCALLRETURN.SMS_OK</returns>
		/// <returns>Error returns a constant defined in SMSCALLRETURN</returns>
		/// <remarks>Receiving is done by Mapirule.dll and SmsRead.cs</remarks>
		public uint SMS_Open()
		{
			uint uRet = SmsOpen(SMS_MSGTYPE_TEXT, SMS_MODE_SEND, 
				ref smsCurrentDevice.smsHandle, ref smsCurrentDevice.smsRcvHandle);
			
//			if ((IntPtr)uRet == IntPtr.Zero && smsCurrentDevice.smsHandle != IntPtr.Zero)
			if (uRet == 0 && smsCurrentDevice.smsHandle != IntPtr.Zero)
				return (uint)SMSCALLRETURN.SMS_OK;
			else smsCurrentDevice.smsHandle = IntPtr.Zero;

			return uRet;
		}

		/// <summary>Creates and sends an SMS message.</summary>
		/// <param name="sAddress">The destination of the message</param>
		/// <param name="sMsg">The message. This can be null</param>
		/// <returns>Success returns SMSCALLRETURN.SMS_OK</returns>
		/// <returns>Error returns a constant defined in SMSCALLRETURN</returns>
		public uint SMS_SendMessage(String sAddress, String sMsg)
		{
			uint uRet = (uint)SMSCALLRETURN.E_HANDLE;

			if (smsCurrentDevice.smsHandle == IntPtr.Zero)
				return uRet;

			SMS_ADDRESS smsAddress = new SMS_ADDRESS();

			smsAddress.smsatAddressType = SMS_ADDRESS_TYPE.Unknown;
			smsAddress.address = sAddress;

			// Allocate a buffer for unmanaged memory, but don't forget
			// to unallocate (Dispose) when done
			IntPtr pSmsAddress = smsAddress.StructureToPtr();

			byte[] bProviderSpecificData = new byte[12];

			uRet = SmsSendMessage(smsCurrentDevice.smsHandle, 
				IntPtr.Zero, pSmsAddress, IntPtr.Zero, sMsg, sMsg.Length*2,
				bProviderSpecificData, bProviderSpecificData.Length, 
				SMS_DATA_ENCODING.SMSDE_OPTIMAL, 
				(int)SMS_OPTION_DELIVERY_NONE, IntPtr.Zero);

			smsAddress.Dispose(pSmsAddress);

			return uRet;
		}

		/// <summary>Closes the SMS messaging handle.</summary>
		public void SMS_Close()
		{
			SmsClose(smsCurrentDevice.smsHandle);
		}

		#endregion			// SMS Low Level Calls

		#region P/Invoke API Calls

		[DllImport("sms.dll")]
		private static extern uint SmsOpen(String ptsMessageProtocol,
			uint dwMessageModes, ref IntPtr psmshHandle, 
			ref IntPtr phMessageAvailableEvent);

		[DllImport("sms.dll")]
		private static extern uint SmsClose(IntPtr psmshHandle);

		[DllImport("sms.dll")]
		private static extern uint SmsSendMessage(
			IntPtr smshHandle,
			IntPtr psmsaSMSCAddress,
			IntPtr psmsaDestinationAddress,
			IntPtr pstValidityPeriod,
			String pbData,
			int dwDataSize,
			byte[] pbProviderSpecificData,
			int dwProviderSpecificDataSize,
			SMS_DATA_ENCODING smsdeDataEncoding,
			int dwOptions,
			IntPtr psmsmidMessageID);

		#endregion	// P/Invoke API Calls

	}

}