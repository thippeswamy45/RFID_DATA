//--------------------------------------------------------------------
// FILENAME: Tapi.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION:		This file provides wrapper calls around native 
//					TAPI 2.0 calls
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
using System.Diagnostics;	// for debugging
using System.Threading;
using Symbol;

namespace WANSample
{
	public class Tapi : IDisposable
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
		~Tapi()
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

		#region TAPI enums, classes and structs

		/// <summary>
		/// Capabilities of a line device
		/// </summary>
		public class LINEDEVCAPS
		{
			public uint dwTotalSize;
			public uint dwNeededSize;
			public uint dwUsedSize;
			public uint dwProviderInfoSize;
			public uint dwProviderInfoOffset;
			public uint dwSwitchInfoSize;
			public uint dwSwitchInfoOffset;
			public uint dwPermanentLineID;
			public uint dwLineNameSize;
			public uint dwLineNameOffset;
			public uint dwStringFormat;
			public uint dwAddressModes;
			public uint dwNumAddresses;
			public uint dwBearerModes;
			public uint dwMaxRate;
			public uint dwMediaModes;
			public uint dwGenerateToneModes;
			public uint dwGenerateToneMaxNumFreq;
			public uint dwGenerateDigitModes;
			public uint dwMonitorToneMaxNumFreq;
			public uint dwMonitorToneMaxNumEntries;
			public uint dwMonitorDigitModes;
			public uint dwGatherDigitsMinTimeout;
			public uint dwGatherDigitsMaxTimeout;
			public uint dwMedCtlDigitMaxListSize;
			public uint dwMedCtlMediaMaxListSize;
			public uint dwMedCtlToneMaxListSize;
			public uint dwMedCtlCallStateMaxListSize;
			public uint dwDevCapFlags;
			public uint dwMaxNumActiveCalls;
			public uint dwAnswerMode;
			public uint dwRingModes;
			public uint dwLineStates;
			public uint dwUUIAcceptSize;
			public uint dwUUIAnswerSize;
			public uint dwUUIMakeCallSize;
			public uint dwUUIDropSize;
			public uint dwUUISendUserUserInfoSize;
			public uint dwUUICallInfoSize;
			public LINEDIALPARAMS MinDialParams;
			public LINEDIALPARAMS MaxDialParams;
			public LINEDIALPARAMS DefaultDialParams;
			public uint dwNumTerminals;
			public uint dwTerminalCapsSize;
			public uint dwTerminalCapsOffset;
			public uint dwTerminalTextEntrySize;
			public uint dwTerminalTextSize;
			public uint dwTerminalTextOffset;
			public uint dwDevSpecificSize;
			public uint dwDevSpecificOffset;
			public uint dwLineFeatures;
			public uint dwSettableDevStatus;
			public uint dwDeviceClassesSize;
			public uint dwDeviceClassesOffset;
		}

		/// <summary>
		/// Describes parameters supplied when making calls 
		/// using the lineMakeCall function
		/// </summary>
		public class LINECALLPARAMS
		{ 
			public uint dwTotalSize;
			public uint dwBearerMode;  
			public uint dwMinRate;
			public uint dwMaxRate;
			public uint dwMediaMode;
			public uint dwCallParamFlags;
			public uint dwAddressMode;   
			public uint dwAddressID;     
			public LINEDIALPARAMS DialParams;
			public uint dwOrigAddressSize;   
			public uint dwOrigAddressOffset;
			public uint dwDisplayableAddressSize; 
			public uint dwDisplayableAddressOffset;
			public uint dwCalledPartySize;     
			public uint dwCalledPartyOffset;
			public uint dwCommentSize;      
			public uint dwCommentOffset;
			public uint dwUserUserInfoSize;
			public uint dwUserUserInfoOffset;
			public uint dwHighLevelCompSize; 
			public uint dwHighLevelCompOffset;
			public uint dwLowLevelCompSize;   
			public uint dwLowLevelCompOffset;
			public uint dwDevSpecificSize;   
			public uint dwDevSpecificOffset;
			public uint dwPredictiveAutoTransferStates;
			public uint dwTargetAddressSize;    
			public uint dwTargetAddressOffset;  
			public uint dwSendingFlowspecSize;   
			public uint dwSendingFlowspecOffset; 
			public uint dwReceivingFlowspecSize;   
			public uint dwReceivingFlowspecOffset; 
			public uint dwDeviceClassSize;      
			public uint dwDeviceClassOffset;    
			public uint dwDeviceConfigSize;     
			public uint dwDeviceConfigOffset;   
			public uint dwCallDataSize;         
			public uint dwCallDataOffset;       
			public uint dwNoAnswerTimeout;      
			public uint dwCallingPartyIDSize;   
			public uint dwCallingPartyIDOffset;

			public IntPtr StructureToPtr()
			{
				IntPtr ptr = myCommon.AllocHGlobal((int)dwTotalSize);
				if (ptr == IntPtr.Zero) return ptr;

				Marshal.StructureToPtr(this, ptr, false);

				return ptr;
			}

			public void Dispose(IntPtr ptr)
			{
				myCommon.FreeHGlobal(ptr);
				ptr = IntPtr.Zero;
			}
		}

		/// <summary>
		/// Describes the current status of a line device
		/// </summary>
		public class LINEDEVSTATUS
		{
			public uint dwTotalSize;
			public uint dwNeededSize;
			public uint dwUsedSize;
			public uint dwNumOpens;
			public uint dwOpenMediaModes;
			public uint dwNumActiveCalls;
			public uint dwNumOnHoldCalls;
			public uint dwNumOnHoldPendCalls;
			public uint dwLineFeatures;
			public uint dwNumCallCompletions;
			public uint dwRingMode;
			public uint dwSignalLevel;
			public uint dwBatteryLevel;
			public uint dwRoamMode;
			public uint dwDevStatusFlags;
			public uint dwTerminalModesSize;
			public uint dwTerminalModesOffset;
			public uint dwDevSpecificSize;
			public uint dwDevSpecificOffset;
		}

		/// <summary>
		/// Holds miscellaneous information about the device and its software. 
		/// This structure is used with lineGetGeneralInfo calls.
		/// </summary>
		public class LINEGENERALINFO
		{
			internal int dwTotalSize = 0;
			internal int dwNeededSize = 0;
			internal int dwUsedSize = 0;
			internal int dwManufacturerSize = 0;
			internal int dwManufacturerOffset = 0;
			internal int dwModelSize = 0;
			internal int dwModelOffset = 0;
			internal int dwRevisionSize = 0;
			internal int dwRevisionOffset = 0;
			internal int dwSerialNumberSize = 0;
			internal int dwSerialNumberOffset = 0;
			internal int dwSubscriberNumberSize = 0;
			internal int dwSubscriberNumberOffset = 0;

			public String manufacturer = "";
			public String model = "";
			public String revision = "";
			public String IMEI = "";
            // iDen support
			public String IMSIorSIMId = "";
			public String SIMStatus = "";

			public IntPtr StructureToPtr()
			{
				IntPtr ptr = myCommon.AllocHGlobal((int)dwTotalSize);
				if (ptr == IntPtr.Zero) return ptr;

				//Marshal.StructureToPtr(this, ptr, false);
                int offset = 0;

                Marshal.WriteInt32(ptr, offset, this.dwTotalSize);
                Marshal.WriteInt32(ptr, (offset+=4), this.dwNeededSize);
                Marshal.WriteInt32(ptr, (offset += 4), this.dwUsedSize);

				return ptr;
			}

			public void PtrToStructure(IntPtr ptr)
			{
                //Marshal.PtrToStructure(ptr, this);

                int offset = 12;
                this.dwManufacturerSize = Marshal.ReadInt32(ptr, offset);
                this.dwManufacturerOffset = Marshal.ReadInt32(ptr, (offset += 4));

                this.dwModelSize = Marshal.ReadInt32(ptr, (offset += 4));
                this.dwModelOffset = Marshal.ReadInt32(ptr, (offset += 4));

                this.dwRevisionSize = Marshal.ReadInt32(ptr, (offset += 4));
                this.dwRevisionOffset = Marshal.ReadInt32(ptr, (offset += 4));

                this.dwSerialNumberSize = Marshal.ReadInt32(ptr, (offset += 4));
                this.dwSerialNumberOffset = Marshal.ReadInt32(ptr, (offset += 4));

                this.dwSubscriberNumberSize = Marshal.ReadInt32(ptr, (offset += 4));
                this.dwSubscriberNumberOffset = Marshal.ReadInt32(ptr, (offset += 4));

                byte[] totalbuffer = new byte[this.dwTotalSize];
				Marshal.Copy(ptr, totalbuffer, 0, this.dwTotalSize);

				this.manufacturer = Encoding.Unicode.GetString(totalbuffer,
					(int)this.dwManufacturerOffset,
					(int)this.dwManufacturerSize).Trim('\0');
				this.model = Encoding.Unicode.GetString(totalbuffer,
					(int)this.dwModelOffset,
					(int)this.dwModelSize).Trim('\0');
				this.revision = Encoding.Unicode.GetString(totalbuffer,
					(int)this.dwRevisionOffset,
					(int)this.dwRevisionSize).Trim('\0');
				this.IMEI = Encoding.Unicode.GetString(totalbuffer,
					(int)this.dwSerialNumberOffset,
					(int)this.dwSerialNumberSize).Trim('\0');
                this.IMSIorSIMId = Encoding.Unicode.GetString(totalbuffer,
					(int)this.dwSubscriberNumberOffset,
					(int)this.dwSubscriberNumberSize).Trim('\0');

                if (this.IMSIorSIMId != "") this.SIMStatus = "Ready";
				else this.SIMStatus = "Not available";
			}

			public void Dispose(IntPtr ptr)
			{
				myCommon.FreeHGlobal(ptr);
				ptr = IntPtr.Zero;
			}
		}

		/// <summary>
		/// Describes parameters supplied when making calls using lineInitializeEx
		/// </summary>
		public class LINEINITIALIZEEXPARAMS
		{
			public LINEINITIALIZEEXPARAMS()
			{
				dwTotalSize = Marshal.SizeOf(typeof(LINEINITIALIZEEXPARAMS));
				dwOptions = LINEINITIALIZEEXOPTION_USEEVENT;
			}
			public int dwTotalSize;
			public int dwNeededSize;
			public int dwUsedSize;
			public int dwOptions;
			public IntPtr hEvent;
			public int dwCompletionKey;

			const int LINEINITIALIZEEXOPTION_USEEVENT = 0x2;
		}

		/// <summary>
		/// Shows all the ways an operator can identify itself and is used in 
		/// the LINEOPERATORSTATUS structure and with lineGetOperatorStatus call.
		/// </summary>
		public struct LINEOPERATOR
		{
			// Maximum string lengths
			public const int MAX_LENGTH_OPERATOR_LONG = 32;
			public const int MAX_LENGTH_OPERATOR_SHORT = 16;
			public const int MAX_LENGTH_OPERATOR_NUMERIC = 16;

			public int dwIndex;
			public int dwValidFields;
			public int dwStatus;
			public String longName;
			public String shortName;
			public String numName;

			public IntPtr StructureToPtr()
			{
				longName = new String((char)0, MAX_LENGTH_OPERATOR_LONG);
				shortName = new String((char)0, MAX_LENGTH_OPERATOR_SHORT);
				numName = new String((char)0, MAX_LENGTH_OPERATOR_NUMERIC);
				//IntPtr ptr = myCommon.AllocHGlobal(Marshal.SizeOf(typeof(LINEOPERATOR)) + ((MAX_LENGTH_OPERATOR_LONG + MAX_LENGTH_OPERATOR_SHORT + MAX_LENGTH_OPERATOR_NUMERIC) * 2));
                IntPtr ptr = myCommon.AllocHGlobal(12 + ((MAX_LENGTH_OPERATOR_LONG + MAX_LENGTH_OPERATOR_SHORT + MAX_LENGTH_OPERATOR_NUMERIC) * 2));
                if (ptr == IntPtr.Zero) return ptr;

				Marshal.WriteInt32(ptr, 0, this.dwIndex);
				Marshal.WriteInt32(ptr, 4, this.dwValidFields);
				Marshal.WriteInt32(ptr, 8, this.dwStatus);

				WriteByteArray(ptr, MAX_LENGTH_OPERATOR_LONG, this.longName.ToCharArray());
				WriteByteArray(ptr, MAX_LENGTH_OPERATOR_SHORT, this.shortName.ToCharArray());
				WriteByteArray(ptr, MAX_LENGTH_OPERATOR_NUMERIC, this.numName.ToCharArray());
				return ptr;
			}

			public void PtrToStructure(IntPtr ptr)
			{
                int offset = 0;
				this.dwIndex = Marshal.ReadInt32(ptr, offset);

                offset += sizeof(int);
				this.dwValidFields = Marshal.ReadInt32(ptr, offset);

                offset += sizeof(int);
                this.dwStatus = Marshal.ReadInt32(ptr, offset);

                offset += sizeof(int);
                this.longName = Marshal.PtrToStringUni((IntPtr)((int)ptr + offset), MAX_LENGTH_OPERATOR_LONG);
				
                offset += MAX_LENGTH_OPERATOR_LONG;
				this.shortName = Marshal.PtrToStringUni((IntPtr)((int)ptr + offset), MAX_LENGTH_OPERATOR_SHORT);
				
                offset += MAX_LENGTH_OPERATOR_SHORT;
				this.numName = Marshal.PtrToStringUni((IntPtr)((int)ptr + offset), MAX_LENGTH_OPERATOR_NUMERIC);
			}

			public static void WriteByteArray(IntPtr ptr, int ofs, char[] val)
			{
				Marshal.Copy(val, 0, new IntPtr(ptr.ToInt32() + ofs), val.Length);
			}

			/// <summary>
			/// Disposes of the ConnectionInfo object.
			/// </summary>
			public void Dispose(IntPtr ptr)
			{
				if (ptr != IntPtr.Zero)
					myCommon.FreeHGlobal(ptr);
			}
		}

		/// <summary>
		/// Specifies a collection of dialing-related fields
		/// </summary>
		public struct LINEDIALPARAMS
		{
			public uint dwDialPause;
			public uint dwDialSpeed;
			public uint dwDigitDuration;
			public uint dwWaitForDialtone;
		}

		/// <summary>
		/// Describes an extension identifier. Extension identifiers are 
		/// used to identify service provider-specific extensions for line 
		/// devices.  Used by lineNegotiateAPIVersion
		/// </summary>
		public struct LINEEXTENSIONID
		{
			public uint dwExtensionID0;
			public uint dwExtensionID1;
			public uint dwExtensionID2;
			public uint dwExtensionID3;
		}

		/// <summary>
		/// Contains parameter values specifying a change in status of the line 
		/// the application currently has open. The lineGetMessage function 
		/// returns the LINEMESSAGE structure
		/// </summary>
		public struct LINEMESSAGE
		{
			public IntPtr hDevice;
			public uint dwMessageID;
			public uint dwCallbackInstance;
			public uint dwParam1;
			public uint dwParam2;
			public uint dwParam3;
		}

		/// <summary>
		/// These bit-flag constants describe the kinds of access rights or privileges 
		/// an application with a call handle may have to the corresponding call.
		/// </summary>
		public enum LINECALLPRIVILEGE : uint
		{
			LINECALLPRIVILEGE_NONE = (uint)0x00000001,
			LINECALLPRIVILEGE_MONITOR = (uint)0x00000002,
			LINECALLPRIVILEGE_OWNER = (uint)0x00000004
		}

		/// <summary>
		/// These constants describe media types (or modes) of a 
		/// communications session or call.
		/// </summary>
		public enum LINEMEDIAMODE : uint
		{
			LINEMEDIAMODE_UNKNOWN = (uint)0x00000002,
			LINEMEDIAMODE_INTERACTIVEVOICE = (uint)0x00000004,
			LINEMEDIAMODE_AUTOMATEDVOICE = (uint)0x00000008,
			LINEMEDIAMODE_DATAMODEM = (uint)0x00000010,
			LINEMEDIAMODE_G3FAX = (uint)0x00000020,
			LINEMEDIAMODE_TDD = (uint)0x00000040,
			LINEMEDIAMODE_G4FAX = (uint)0x00000080,
			LINEMEDIAMODE_DIGITALDATA = (uint)0x00000100,
			LINEMEDIAMODE_TELETEX = (uint)0x00000200,
			LINEMEDIAMODE_VIDEOTEX = (uint)0x00000400,
			LINEMEDIAMODE_TELEX = (uint)0x00000800,
			LINEMEDIAMODE_MIXED = (uint)0x00001000,
			LINEMEDIAMODE_ADSI = (uint)0x00002000,
			LINEMEDIAMODE_VOICEVIEW = (uint)0x00004000,
			LINEMEDIAMODE_VIDEO = (uint)0x00008000
		}

		/// <summary>
		/// These bit-flag constants describe various line status events.
		/// </summary>
		public enum LINEDEVSTATE : uint
		{
			LINEDEVSTATE_OTHER = (uint)0x00000001,
			LINEDEVSTATE_RINGING = (uint)0x00000002,
			LINEDEVSTATE_CONNECTED = (uint)0x00000004,
			LINEDEVSTATE_DISCONNECTED = (uint)0x00000008,
			LINEDEVSTATE_MSGWAITON = (uint)0x00000010,
			LINEDEVSTATE_MSGWAITOFF = (uint)0x00000020,
			LINEDEVSTATE_INSERVICE = (uint)0x00000040,
			LINEDEVSTATE_OUTOFSERVICE = (uint)0x00000080,
			LINEDEVSTATE_MAINTENANCE = (uint)0x00000100,
			LINEDEVSTATE_OPEN = (uint)0x00000200,
			LINEDEVSTATE_CLOSE = (uint)0x00000400,
			LINEDEVSTATE_NUMCALLS = (uint)0x00000800,
			LINEDEVSTATE_NUMCOMPLETIONS = (uint)0x00001000,
			LINEDEVSTATE_TERMINALS = (uint)0x00002000,
			LINEDEVSTATE_ROAMMODE = (uint)0x00004000,
			LINEDEVSTATE_BATTERY = (uint)0x00008000,
			LINEDEVSTATE_SIGNAL = (uint)0x00010000,
			LINEDEVSTATE_DEVSPECIFIC = (uint)0x00020000,
			LINEDEVSTATE_REINIT = (uint)0x00040000,
			LINEDEVSTATE_LOCK = (uint)0x00080000,
			LINEDEVSTATE_CAPSCHANGE = (uint)0x00100000,   
			LINEDEVSTATE_CONFIGCHANGE = (uint)0x00200000, 
			LINEDEVSTATE_TRANSLATECHANGE = (uint)0x00400000,
			LINEDEVSTATE_COMPLCANCEL = (uint)0x00800000,    
			LINEDEVSTATE_REMOVED = (uint)0x01000000         
		}

		/// <summary>
		/// These bit-flag constants describe different bearer modes of a call. 
		/// </summary>
		public enum LINEBEARERMODE : uint
		{
			LINEBEARERMODE_VOICE = (uint)0x00000001,
			LINEBEARERMODE_SPEECH = (uint)0x00000002,
			LINEBEARERMODE_MULTIUSE = (uint)0x00000004,
			LINEBEARERMODE_DATA = (uint)0x00000008,
			LINEBEARERMODE_ALTSPEECHDATA = (uint)0x00000010,
			LINEBEARERMODE_NONCALLSIGNALING = (uint)0x00000020,
			LINEBEARERMODE_PASSTHROUGH = (uint)0x00000040, 
			LINEBEARERMODE_RESTRICTEDDATA = (uint)0x00000080
		}

		/// <summary>
		/// These bit-flag constants describe various ways of identifying an 
		/// address on a line device. 
		/// </summary>
		public enum LINEADDRESSMODE : uint
		{
			LINEADDRESSMODE_ADDRESSID = (uint)0x00000001,
			LINEADDRESSMODE_DIALABLEADDR = (uint)0x00000002
		}

		/// <summary>
		/// This type is a callback function implemented by TAPI and supplied 
		/// to the service provider as a parameter to the TSPI_lineOpen function.
		/// </summary>
		public enum LINEEVENT
		{
			LINE_ADDRESSSTATE = 0,
			LINE_CALLINFO,
			LINE_CALLSTATE,
			LINE_CLOSE,
			LINE_DEVSPECIFIC,
			LINE_DEVSPECIFICFEATURE,
			LINE_GATHERDIGITS,
			LINE_GENERATE,
			LINE_LINEDEVSTATE,
			LINE_MONITORDIGITS,
			LINE_MONITORMEDIA,
			LINE_MONITORTONE,
			LINE_REPLY,
			LINE_REQUEST,
			PHONE_BUTTON,
			PHONE_CLOSE,
			PHONE_DEVSPECIFIC,
			PHONE_REPLY,
			PHONE_STATE,
			LINE_CREATE,
			PHONE_CREATE,
			LINE_AGENTSPECIFIC,
			LINE_AGENTSTATUS,
			LINE_APPNEWCALL,
			LINE_PROXYREQUEST,
			LINE_REMOVE,
			PHONE_REMOVE
		};

		/// <summary>
		/// These bit-flag constants describe the call states a call can be in.
		/// </summary>
		public enum LINECALLSTATE : uint
		{
			LINECALLSTATE_IDLE = (uint)0x00000001,
			LINECALLSTATE_OFFERING = (uint)0x00000002,
			LINECALLSTATE_ACCEPTED = (uint)0x00000004,
			LINECALLSTATE_DIALTONE = (uint)0x00000008,
			LINECALLSTATE_DIALING = (uint)0x00000010,
			LINECALLSTATE_RINGBACK = (uint)0x00000020,
			LINECALLSTATE_BUSY = (uint)0x00000040,
			LINECALLSTATE_SPECIALINFO = (uint)0x00000080,
			LINECALLSTATE_CONNECTED = (uint)0x00000100,
			LINECALLSTATE_PROCEEDING = (uint)0x00000200,
			LINECALLSTATE_ONHOLD = (uint)0x00000400,
			LINECALLSTATE_CONFERENCED = (uint)0x00000800,
			LINECALLSTATE_ONHOLDPENDCONF = (uint)0x00001000,
			LINECALLSTATE_ONHOLDPENDTRANSFER = (uint)0x00002000,
			LINECALLSTATE_DISCONNECTED = (uint)0x00004000,
			LINECALLSTATE_UNKNOWN = (uint)0x00008000
		}

		/// <summary>
		/// Line equipment state.  
		/// </summary>
		public enum LINEEQUIPSTATE : uint
		{
			LINEEQUIPSTATE_MINIMUM = 0x00000001,
			LINEEQUIPSTATE_RXONLY = 0x00000002,
			LINEEQUIPSTATE_TXONLY = 0x00000003,
			LINEEQUIPSTATE_NOTXRX = 0x00000004,
			LINEEQUIPSTATE_FULL = 0x00000005,
		}

		/// <summary>
		/// Contains information about the Line System Types; CDMA or GSM
		/// </summary>
		public enum LINESYSTEMTYPE
		{
			LINESYSTEMTYPE_IDEN = 0x00000000,			// IDEN Nextel
			LINESYSTEMTYPE_IS95A = 0x00000001,			// CDMA IS95A air interface standard
			LINESYSTEMTYPE_IS95B = 0x00000002,			// CDMA IS95B air interface standard
			LINESYSTEMTYPE_1XRTTPACKET = 0x00000004,	// CDMA 1XRTTPACKET one-carrier radio-transmission technology
			LINESYSTEMTYPE_GSM = 0x00000008,			// GSM  Global System for Mobile Communications (GSM)
			LINESYSTEMTYPE_GPRS = 0x00000010,			// GSM  General Packet Radio Service (GPRS)
            LINESYSTEMTYPE_EDGE = 0x00000020,
            LINESYSTEMTYPE_1XEVDOPACKET = 0x00000040,
            LINESYSTEMTYPE_1XEVDVPACKET = 0x00000080,
            LINESYSTEMTYPE_UMTS = 0x00000100,
            LINESYSTEMTYPE_HSDPA = 0x00000200
		}

		/// <summary>
		/// The current registration status of the line.
		/// </summary>
		public enum LINEREGSTATUS
		{
			/// <summary>Registration is unknown</summary>
			UNKNOWN = 0x00000001,
			/// <summary>Registration denied</summary>
			DENIED = 0x00000002,						// Registration denied
			/// <summary>Not registered</summary>
			UNREGISTERED = 0x00000003,
			/// <summary>Attempting to register</summary>
			ATTEMPTING = 0x00000004,
			/// <summary>Registered at home</summary>
			HOME = 0x00000005,
			/// <summary>Registered for roam</summary>
			ROAM = 0x00000006,
			/// <summary>Registered for digital service</summary>
			DIGITAL = 0x00000007,
			/// <summary>Registered for analog service</summary>
			ANALOG = 0x00000008,
		}

		/// <summary>
		/// The possible return values of all the line calls.
		/// </summary>
		public enum LINECALLRETURN : uint
		{
			LINEERR_OK = (uint)0x00000000,
			LINEERR_ALLOCATED = (uint)0x80000001,
			LINEERR_BADDEVICEID = (uint)0x80000002,
			LINEERR_BEARERMODEUNAVAIL = (uint)0x80000003,
			LINEERR_CALLUNAVAIL = (uint)0x80000005,
			LINEERR_COMPLETIONOVERRUN = (uint)0x80000006,
			LINEERR_CONFERENCEFULL = (uint)0x80000007,
			LINEERR_DIALBILLING = (uint)0x80000008,
			LINEERR_DIALDIALTONE = (uint)0x80000009,
			LINEERR_DIALPROMPT = (uint)0x8000000A,
			LINEERR_DIALQUIET = (uint)0x8000000B,
			LINEERR_INCOMPATIBLEAPIVERSION = (uint)0x8000000C,
			LINEERR_INCOMPATIBLEEXTVERSION = (uint)0x8000000D,
			LINEERR_INIFILECORRUPT = (uint)0x8000000E,
			LINEERR_INUSE = (uint)0x8000000F,
			LINEERR_INVALADDRESS = (uint)0x80000010,
			LINEERR_INVALADDRESSID = (uint)0x80000011,
			LINEERR_INVALADDRESSMODE = (uint)0x80000012,
			LINEERR_INVALADDRESSSTATE = (uint)0x80000013,
			LINEERR_INVALAPPHANDLE = (uint)0x80000014,
			LINEERR_INVALAPPNAME = (uint)0x80000015,
			LINEERR_INVALBEARERMODE = (uint)0x80000016,
			LINEERR_INVALCALLCOMPLMODE = (uint)0x80000017,
			LINEERR_INVALCALLHANDLE = (uint)0x80000018,
			LINEERR_INVALCALLPARAMS = (uint)0x80000019,
			LINEERR_INVALCALLPRIVILEGE = (uint)0x8000001A,
			LINEERR_INVALCALLSELECT = (uint)0x8000001B,
			LINEERR_INVALCALLSTATE = (uint)0x8000001C,
			LINEERR_INVALCALLSTATELIST = (uint)0x8000001D,
			LINEERR_INVALCARD = (uint)0x8000001E,
			LINEERR_INVALCOMPLETIONID = (uint)0x8000001F,
			LINEERR_INVALCONFCALLHANDLE = (uint)0x80000020,
			LINEERR_INVALCONSULTCALLHANDLE = (uint)0x80000021,
			LINEERR_INVALCOUNTRYCODE = (uint)0x80000022,
			LINEERR_INVALDEVICECLASS = (uint)0x80000023,
			LINEERR_INVALDEVICEHANDLE = (uint)0x80000024,
			LINEERR_INVALDIALPARAMS = (uint)0x80000025,
			LINEERR_INVALDIGITLIST = (uint)0x80000026,
			LINEERR_INVALDIGITMODE = (uint)0x80000027,
			LINEERR_INVALDIGITS = (uint)0x80000028,
			LINEERR_INVALEXTVERSION = (uint)0x80000029,
			LINEERR_INVALGROUPID = (uint)0x8000002A,
			LINEERR_INVALLINEHANDLE = (uint)0x8000002B,
			LINEERR_INVALLINESTATE = (uint)0x8000002C,
			LINEERR_INVALLOCATION = (uint)0x8000002D,
			LINEERR_INVALMEDIALIST = (uint)0x8000002E,
			LINEERR_INVALMEDIAMODE = (uint)0x8000002F,
			LINEERR_INVALMESSAGEID = (uint)0x80000030,
			LINEERR_INVALPARAM = (uint)0x80000032,
			LINEERR_INVALPARKID = (uint)0x80000033,
			LINEERR_INVALPARKMODE = (uint)0x80000034,
			LINEERR_INVALPOINTER = (uint)0x80000035,
			LINEERR_INVALPRIVSELECT = (uint)0x80000036,
			LINEERR_INVALRATE = (uint)0x80000037,
			LINEERR_INVALREQUESTMODE = (uint)0x80000038,
			LINEERR_INVALTERMINALID = (uint)0x80000039,
			LINEERR_INVALTERMINALMODE = (uint)0x8000003A,
			LINEERR_INVALTIMEOUT = (uint)0x8000003B,
			LINEERR_INVALTONE = (uint)0x8000003C,
			LINEERR_INVALTONELIST = (uint)0x8000003D,
			LINEERR_INVALTONEMODE = (uint)0x8000003E,
			LINEERR_INVALTRANSFERMODE = (uint)0x8000003F,
			LINEERR_LINEMAPPERFAILED = (uint)0x80000040,
			LINEERR_NOCONFERENCE = (uint)0x80000041,
			LINEERR_NODEVICE = (uint)0x80000042,
			LINEERR_NODRIVER = (uint)0x80000043,
			LINEERR_NOMEM = (uint)0x80000044,
			LINEERR_NOREQUEST = (uint)0x80000045,
			LINEERR_NOTOWNER = (uint)0x80000046,
			LINEERR_NOTREGISTERED = (uint)0x80000047,
			LINEERR_OPERATIONFAILED = (uint)0x80000048,
			LINEERR_OPERATIONUNAVAIL = (uint)0x80000049,
			LINEERR_RATEUNAVAIL = (uint)0x8000004A,
			LINEERR_RESOURCEUNAVAIL = (uint)0x8000004B,
			LINEERR_REQUESTOVERRUN = (uint)0x8000004C,
			LINEERR_STRUCTURETOOSMALL = (uint)0x8000004D,
			LINEERR_TARGETNOTFOUND = (uint)0x8000004E,
			LINEERR_TARGETSELF = (uint)0x8000004F,
			LINEERR_UNINITIALIZED = (uint)0x80000050,
			LINEERR_USERUSERINFOTOOBIG = (uint)0x80000051,
			LINEERR_REINIT = (uint)0x80000052,
			LINEERR_ADDRESSBLOCKED = (uint)0x80000053,
			LINEERR_BILLINGREJECTED = (uint)0x80000054,
			LINEERR_INVALFEATURE = (uint)0x80000055,
			LINEERR_NOMULTIPLEINSTANCE = (uint)0x80000056,
			LINEERR_INVALAGENTID = (uint)0x80000057,
			LINEERR_INVALAGENTGROUP = (uint)0x80000058,
			LINEERR_INVALPASSWORD = (uint)0x80000059,
			LINEERR_INVALAGENTSTATE = (uint)0x8000005A,
			LINEERR_INVALAGENTACTIVITY = (uint)0x8000005B,
			LINEERR_DIALVOICEDETECT = (uint)0x8000005C
		};

		/// <summary>
		/// Holds the handles and other TAPI line related information 
		/// created by this module.  This is not a windows structure.
		/// </summary>
		public struct LINECURRENTDEVICE
		{
			public IntPtr appHandle;
			public IntPtr lineHandle;
			public IntPtr inLineHandle;		// Incoming call handle
			public IntPtr lineHcallHandle;
			public String lineDeviceName;
			public uint lineDeviceIndex;
			public uint lineDeviceAPIVersion;
			public LINECALLSTATE lineClassState;
		}

		#endregion		//TAPI enums, classes, structs

		#region Declarations

		//TAPI versions used in lineNegotiateAPIVersion
		public const uint MOST_RECENT_VERSION = 0x00020000;
		public const uint LEAST_RECENT_VERSION = 0x00010004;
		
		// Line device name
		public const String LINE_DEVICE_NAME = "Cellular Line";

		// LINE_DEVSPECIFIC message types
		public const uint LINE_EQUIPSTATECHANGE = 0x00000100;

		public LINECURRENTDEVICE lineCurrentDevice;
		public LINEGENERALINFO lineGeneralInfo;
		public event EventHandler tapiMessageEvent;

		private static WANSample.Common myCommon = new WANSample.Common();

		private bool stopThread = false;

		#endregion	// Declarations

		#region TAPI Low Level Calls

		/// <summary>
		/// Initializes and opens the TAPI line.
		/// </summary>
		/// <returns>Success returns LINECALLRETURN.LINEERR_OK</returns>
		/// <returns>Error returns a constant defined in LINECALLRETURN</returns>
		/// <remarks>Radio must be enabled before opening a TAPI line</remarks>
		public uint TAPI_Open()
		{
			uint uRet = TAPI_Init();
			if (uRet == (uint)LINECALLRETURN.LINEERR_OK)
				uRet = TAPI_OpenLine();

			return uRet;
		}

		/// <summary>
		/// Closes and shuts down the TAPI line.
		/// </summary>
		public void TAPI_Close()
		{
			TAPI_CloseLine();
			TAPI_Shutdown();
		}

		/// <summary>
		/// Initializes the TAPI line. You may use TAPI_Open() to initialize and open the line.
		/// </summary>
		/// <returns>Success returns LINECALLRETURN.LINEERR_OK</returns>
		/// <returns>Error returns a constant defined in LINECALLRETURN</returns>
		private uint TAPI_Init()
		{
			IntPtr hLineApp = IntPtr.Zero;
			uint uDevCount = 0, uTapiVer = 0;
			IntPtr pLineDevCaps = IntPtr.Zero;

			uint uRet = (uint)LINECALLRETURN.LINEERR_UNINITIALIZED;
			LINEEXTENSIONID lineExtensionId;

			// Supported API version up to 2.0
			uint apiVersion = MOST_RECENT_VERSION;
			LINEINITIALIZEEXPARAMS initParams = new LINEINITIALIZEEXPARAMS();
			
			// Initializes the application's use of TAPI for subsequent use of the line abstraction
			uRet = lineInitializeEx(out hLineApp, IntPtr.Zero, IntPtr.Zero, "WANSample.Tapi", out uDevCount, ref apiVersion, initParams);
			if (uRet != (uint)LINECALLRETURN.LINEERR_OK) return uRet;

			lineCurrentDevice.appHandle = hLineApp;

			// LINEDEVCAPS is variable sized structure.
			LINEDEVCAPS lineDevCaps = new LINEDEVCAPS();
			// This size may differ, we need to use needsize param to figure this out 
			// during runtime and adjust the size accordingly
			int initSize = Marshal.SizeOf(lineDevCaps) * 2;

			pLineDevCaps = myCommon.AllocHGlobal(initSize);
			if (pLineDevCaps == IntPtr.Zero) return (uint)Marshal.GetLastWin32Error();

			lineDevCaps.dwTotalSize = (uint)(initSize);

			for (int i = 0; i < uDevCount; i++)
			{
				// This function is used to negotiate the TAPI version number to use. 
				// It also retrieves the extension identifier supported by the line device.
				uRet = lineNegotiateAPIVersion(hLineApp, (uint)i, LEAST_RECENT_VERSION, MOST_RECENT_VERSION,
							out uTapiVer, out lineExtensionId);
				if (uRet != (uint)LINECALLRETURN.LINEERR_OK) break;

				Marshal.StructureToPtr(lineDevCaps, pLineDevCaps, false);

				// This function queries a specified line device to determine its telephony capabilities. 
				// The name of the specified device will be copied to LINEDEVCAPS
				uRet = lineGetDevCaps(hLineApp, (uint)i, uTapiVer, 0, pLineDevCaps);
				if (uRet != (uint)LINECALLRETURN.LINEERR_OK) break;

				Marshal.PtrToStructure(pLineDevCaps, lineDevCaps);

				byte[] bBuffer = new byte[(int)lineDevCaps.dwTotalSize];
				Marshal.Copy(pLineDevCaps, bBuffer, 0, (int)lineDevCaps.dwTotalSize);
				string sDevName = System.Text.Encoding.Unicode.GetString(bBuffer, (int)lineDevCaps.dwLineNameOffset, (int)lineDevCaps.dwLineNameSize).Trim('\0');

				// Save the cellular line info for later use
				if (sDevName == LINE_DEVICE_NAME)
				{
					lineCurrentDevice.lineDeviceName = sDevName;
					lineCurrentDevice.lineDeviceIndex = (uint)i;
					lineCurrentDevice.lineDeviceAPIVersion = uTapiVer;

					// Start a thread to receive TAPI messages
					new Thread(new ThreadStart(lineThreadProc)).Start();

					break;
				}
			}
			myCommon.FreeHGlobal(pLineDevCaps);

			return uRet;
		}

		/// <summary>
		/// Thread to receive line events. Only a selected number of events are processed
		/// in this sample. You may choose to process additonal events if required.
		/// </summary>
		private void lineThreadProc()
		{
			while (!stopThread)
			{
				LINEMESSAGE lineMessage = new LINEMESSAGE();

				if (lineGetMessage(lineCurrentDevice.appHandle, out lineMessage, -1) == 0)
				{
					uint thisState = lineMessage.dwParam1;

					switch (lineMessage.dwMessageID)
					{
						case (uint)LINEEVENT.LINE_CALLSTATE:
						{
							switch (thisState)
							{
								//case (uint)LINECALLSTATE.LINECALLSTATE_IDLE:
									//goto case (uint)LINECALLSTATE.LINECALLSTATE_DISCONNECTED;
								case (uint)LINECALLSTATE.LINECALLSTATE_BUSY:
									goto case (uint)LINECALLSTATE.LINECALLSTATE_DISCONNECTED;
								case (uint)LINECALLSTATE.LINECALLSTATE_DIALTONE:
									goto case (uint)LINECALLSTATE.LINECALLSTATE_DISCONNECTED;
								//when Data or Voice is connected
								case (uint)LINECALLSTATE.LINECALLSTATE_CONNECTED:
									goto case (uint)LINECALLSTATE.LINECALLSTATE_DISCONNECTED;
								//incoming call
								case (uint)LINECALLSTATE.LINECALLSTATE_OFFERING:
									lineCurrentDevice.inLineHandle = lineMessage.hDevice;
									goto case (uint)LINECALLSTATE.LINECALLSTATE_DISCONNECTED;
								case (uint)LINECALLSTATE.LINECALLSTATE_DISCONNECTED:
									lineCurrentDevice.lineClassState = (LINECALLSTATE)thisState;
									tapiMessageEvent(this, null);
									break;
							}
							break;
						}
						case (uint)LINEEVENT.LINE_DEVSPECIFIC:
							//Radio power on/off
							//if ((lineMessage.dwParam1 == LINE_EQUIPSTATECHANGE) && ((lineMessage.dwParam2 == (uint)LINEEQUIPSTATE.LINEEQUIPSTATE_MINIMUM)
							//    || (lineMessage.dwParam2 == (uint)LINEEQUIPSTATE.LINEEQUIPSTATE_FULL)))
							break;
						case (uint)LINEEVENT.LINE_REPLY:
							//This message is sent to report the results of function calls that completed asynchronously.
							break;
					}
				}
			}
		}

		/// <summary>
		/// Opens the TAPI line. You may use TAPI_Open() to initialize and open the line.
		/// </summary>
		/// <returns>Success returns LINECALLRETURN.LINEERR_OK</returns>
		/// <returns>Error returns a constant defined in LINECALLRETURN</returns>
		private uint TAPI_OpenLine()
		{
			uint uRet = lineOpen(lineCurrentDevice.appHandle,
				lineCurrentDevice.lineDeviceIndex,
				out lineCurrentDevice.lineHandle,
				lineCurrentDevice.lineDeviceAPIVersion,
				0,
				0,
				LINECALLPRIVILEGE.LINECALLPRIVILEGE_MONITOR | LINECALLPRIVILEGE.LINECALLPRIVILEGE_OWNER,
				LINEMEDIAMODE.LINEMEDIAMODE_INTERACTIVEVOICE | LINEMEDIAMODE.LINEMEDIAMODE_DATAMODEM,
				0);

			if (uRet == (uint)LINECALLRETURN.LINEERR_OK)
			{
				uRet = lineSetStatusMessages(lineCurrentDevice.lineHandle,
					LINEDEVSTATE.LINEDEVSTATE_DEVSPECIFIC | LINEDEVSTATE.LINEDEVSTATE_CONFIGCHANGE,
					0);
			}

			return uRet;
		}

		/// <summary>
		/// Makes a call on an opened line. This is an asynchronous call.
		/// </summary>
		/// <param name="String sPhoneNum">The phone number (address) of the destination</param>
		/// <returns>Success returns LINECALLRETURN.LINEERR_OK</returns>
		/// <returns>Error returns a constant defined in LINECALLRETURN</returns>
		public uint TAPI_MakeCall(String sPhoneNum)
		{
			IntPtr pLineCallParams = IntPtr.Zero;

			// This is a variable sized structure
			LINECALLPARAMS lineCallParams = new LINECALLPARAMS();

			// This size must be big enough to hold all the fixed and variably sized 
			// portions of this data structure
			lineCallParams.dwTotalSize = (uint)Marshal.SizeOf(lineCallParams) * 2;
			lineCallParams.dwBearerMode = (uint)LINEBEARERMODE.LINEBEARERMODE_VOICE;
			lineCallParams.dwMediaMode = (uint)LINEMEDIAMODE.LINEMEDIAMODE_INTERACTIVEVOICE;
			lineCallParams.dwAddressMode = (uint)LINEADDRESSMODE.LINEADDRESSMODE_ADDRESSID;
			lineCallParams.dwAddressID = 0;

			pLineCallParams = lineCallParams.StructureToPtr();

			// This function places a call on the specified line to the specified destination address.
			uint uRequestId = lineMakeCall(lineCurrentDevice.lineHandle, out lineCurrentDevice.lineHcallHandle,
				sPhoneNum, 0, pLineCallParams);

			lineCallParams.Dispose(pLineCallParams);

			if (uRequestId < 0) return uRequestId;
			else return (uint)LINECALLRETURN.LINEERR_OK;
		}

		/// <summary>
		/// Answers an incoming call. When a new call arrives, LINE_CALLSTATE message is 
		/// sent with the new call handle. This handle is retrieved in lineThreadProc()
		/// and used to answer the call.
		/// </summary>
		/// <returns>Success returns LINECALLRETURN.LINEERR_OK</returns>
		/// <returns>Error returns a constant defined in LINECALLRETURN</returns>
		public uint TAPI_AnswerCall()
		{
			uint uRet = (uint)LINECALLRETURN.LINEERR_UNINITIALIZED;

			if (lineCurrentDevice.inLineHandle != IntPtr.Zero)
				uRet = lineAnswer(lineCurrentDevice.inLineHandle, IntPtr.Zero, 0);

			return uRet;
		}

		/// <summary>
		/// Drops or disconnects a call. 
		/// </summary>
		/// <returns>Success returns LINECALLRETURN.LINEERR_OK</returns>
		/// <returns>Error returns a constant defined in LINECALLRETURN</returns>
		public uint TAPI_DropCall()
		{
			uint uRequestId = 0;

			if (lineCurrentDevice.lineHcallHandle != IntPtr.Zero)
			{
				uRequestId = lineDrop(lineCurrentDevice.lineHcallHandle, "", 0);
			}

			if (uRequestId < 0) return uRequestId;
			else return (uint)LINECALLRETURN.LINEERR_OK;
		}

		/// <summary>
		/// Closes an opened line. You may choose to use TAPI_Close() to close and
		/// shutdown the line instead.
		/// </summary>
		/// <returns>Success returns LINECALLRETURN.LINEERR_OK</returns>
		/// <returns>Error returns a constant defined in LINECALLRETURN</returns>
		private uint TAPI_CloseLine()
		{
			uint uRet = lineClose(lineCurrentDevice.lineHandle);

			if (uRet == (uint)LINECALLRETURN.LINEERR_OK)
				lineCurrentDevice.lineHandle = IntPtr.Zero;

			return uRet;
		}

		/// <summary>
		/// Shuts down an initialized line. You may choose to use TAPI_Close() to close and
		/// shutdown the line instead. Before we shutdown, close the thread as well
		/// </summary>
		/// <returns>Success returns LINECALLRETURN.LINEERR_OK</returns>
		/// <returns>Error returns a constant defined in LINECALLRETURN</returns>
		private uint TAPI_Shutdown()
		{
			stopThread = true;
			return lineShutdown(lineCurrentDevice.appHandle);
		}

		/// <summary>
		/// Enables the radio transmitter and receiver.
		/// </summary>
		/// <returns>Success returns LINECALLRETURN.LINEERR_OK</returns>
		/// <returns>Error returns a constant defined in LINECALLRETURN</returns>
		public uint TAPI_EnableRadio()
		{
			return TAPI_SetRadioState(true);
		}

		/// <summary>
		/// Disables the radio transmitter and receiver.
		/// </summary>
		/// <returns>Success returns LINECALLRETURN.LINEERR_OK</returns>
		/// <returns>Error returns a constant defined in LINECALLRETURN</returns>
		public uint TAPI_DisableRadio()
		{
			return TAPI_SetRadioState(false);
		}

		/// <summary>
		/// Determines the state of the radio transmitter and receiver
		/// </summary>
		/// <returns>Returns true or false</returns>
		public bool TAPI_IsRadioEnabled()
		{
			uint dwState = 0, dwRadioSupport = 0;

			uint uRet = lineGetEquipmentState(lineCurrentDevice.lineHandle, out dwState, out dwRadioSupport);

			if (uRet == (uint)LINECALLRETURN.LINEERR_OK && dwState == (uint)LINEEQUIPSTATE.LINEEQUIPSTATE_FULL)
				return true;

			return false;
		}

		/// <summary>
		/// Enables or disables the radio transmitter and receiver.
		/// </summary>
		/// <param name="enable">set this value to true to enable and false to disable</param>
		/// <returns>Success returns LINECALLRETURN.LINEERR_OK</returns>
		/// <returns>Error returns a constant defined in LINECALLRETURN</returns>
		public uint TAPI_SetRadioState(bool enable)
		{
			uint dwState = (uint)LINEEQUIPSTATE.LINEEQUIPSTATE_MINIMUM;

			if (enable) dwState = (uint)LINEEQUIPSTATE.LINEEQUIPSTATE_FULL;

			uint uRequestId = lineSetEquipmentState(lineCurrentDevice.lineHandle, dwState);
			if (uRequestId > 0) return (uint)LINECALLRETURN.LINEERR_OK;
			else return uRequestId;
		}

		/// <summary>
		/// Retrieves the current line system type.
		/// </summary>
		/// <returns>Returns constants declared in LINESYSTEMTYPE
        /// 0 for iDEN
        /// 1, 2 or 4 for CDMA
		/// 8 or 10 for GSM
		/// Any other value indicates an error
		/// </returns>
		public uint TAPI_GetSystemType()
		{
			LINESYSTEMTYPE lineSystemType;

			uint uRet = lineGetCurrentSystemType(lineCurrentDevice.lineHandle, out lineSystemType);
			if (uRet != 0) return uRet;
			else return (uint)lineSystemType;
		}

		/// <summary>Retrieves the signal strength.</summary>
		/// <returns>Returns the signal strength from 0% to 100%</returns>
		public uint TAPI_GetSignalStrength()
		{
			IntPtr pDevStatus = IntPtr.Zero;
			uint uRet = 0;

			if (lineCurrentDevice.lineHandle == IntPtr.Zero)
				return uRet;

			LINEDEVSTATUS lineDevStatus = new LINEDEVSTATUS();

			// This size may differ, need to use needsize param to figure this out 
			// during runtime and adjust the size accordingly
			int lineDevStatusSize = Marshal.SizeOf(lineDevStatus) * 2;
			lineDevStatus.dwTotalSize = (uint)lineDevStatusSize;

			pDevStatus = myCommon.AllocHGlobal(lineDevStatusSize);
			if (pDevStatus == IntPtr.Zero) return (uint)Marshal.GetLastWin32Error();

			Marshal.StructureToPtr(lineDevStatus, pDevStatus, false);

			uRet = lineGetLineDevStatus(lineCurrentDevice.lineHandle, pDevStatus);
			if (uRet == (uint)LINECALLRETURN.LINEERR_OK)
			{
				Marshal.PtrToStructure(pDevStatus, lineDevStatus);

				// Siganl Strength, the value ranges from 0x00000000 to 0x0000FFFF
				uRet = (uint)(LINECALLRETURN)(100 * (int)lineDevStatus.dwSignalLevel / 0x0000FFFF);
			}

			myCommon.FreeHGlobal(pDevStatus);

			return uRet;
		}

		/// <summary>Retrieves the network status information.</summary>
		/// <returns>Returns current registration status as defined in LINEREGSTATUS.</returns>
		public LINEREGSTATUS TAPI_GetNetworkStatus()
		{
			LINEREGSTATUS lineRegStatus;
			lineGetRegisterStatus(lineCurrentDevice.lineHandle, out lineRegStatus);

			return lineRegStatus;		
		}

		/// <summary>Retrieves the network operator on which the device is currently registered.</summary>
		/// <returns>Returns the name of the network operator.</returns>
        public String TAPI_GetNetworkOperator()
        {
            String sNetOperator = "UNINITIALIZED";
            uint uRet = (uint)LINECALLRETURN.LINEERR_UNINITIALIZED;

            uint st = TAPI_GetSystemType();

            if (((st & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_IDEN)) != 0) || ((st & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_GSM)) != 0) || ((st & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_GPRS)) != 0)
                || ((st & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_EDGE)) != 0) || ((st & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_UMTS)) != 0) || ((st & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_HSDPA)) != 0))
            {
                // For GSM only
                LINEOPERATOR lineOperator = new LINEOPERATOR();
                IntPtr pLineOperator = lineOperator.StructureToPtr();

                uRet = lineGetCurrentOperator(lineCurrentDevice.lineHandle, pLineOperator);
                if (uRet == (uint)LINECALLRETURN.LINEERR_OK)
                {
                    lineOperator.PtrToStructure(pLineOperator);

                    if ((lineOperator.dwValidFields & 0x00000002) != 0)
                        sNetOperator = lineOperator.longName;
                    else
                        if ((lineOperator.dwValidFields & 0x00000001) != 0)
                            sNetOperator = lineOperator.shortName;
                        else
                            if ((lineOperator.dwValidFields & 0x00000004) != 0)
                                sNetOperator = lineOperator.numName;
                }

                lineOperator.Dispose(pLineOperator);
            }
            else
                if (((st & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_1XRTTPACKET)) != 0) || ((st & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_IS95A)) != 0) || ((st & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_IS95B)) != 0)
                || ((st & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_1XEVDVPACKET)) != 0) || ((st & ((uint)WANSample.Tapi.LINESYSTEMTYPE.LINESYSTEMTYPE_1XEVDOPACKET)) != 0))
                {
                    // For CDMA only. Get this value from registry

                    IntPtr hKey = IntPtr.Zero;
                    const String CDMA_KEY = "SOFTWARE\\Sierra Wireless\\EM3400";
                    const String CARRIER_VALUE = "CarrierSuffix";

                    int hRes = Win32.W32RegOpenKey((IntPtr)Win32.RegKeyHandles.HKEY_LOCAL_MACHINE,
                        CDMA_KEY, 0, 0, out hKey);
                    if (hRes == 0)
                    {
                        Win32.W32RegQueryString(hKey, CARRIER_VALUE, ref sNetOperator);
                    }
                }

            return sNetOperator;
        }

		/// <summary>Retrieves general device information about the radio hardware, 
		/// the radio software, the serial number, subscriber identity, and other 
		/// information.</summary>
		/// <returns>Success returns LINECALLRETURN.LINEERR_OK</returns>
		/// <returns>Error returns a constant defined in LINECALLRETURN</returns>
		/// <remarks>If this call is successful, the device information will be
		/// copied to the public class lineGeneralInfo.</remarks>
		public uint TAPI_GetGeneralInfo()
		{
			uint uRet = (uint)LINECALLRETURN.LINEERR_UNINITIALIZED;
			lineGeneralInfo = new LINEGENERALINFO();

            //lineGeneralInfo.dwTotalSize = Marshal.SizeOf(typeof(LINEGENERALINFO)) * 4;
            lineGeneralInfo.dwTotalSize = 13 * 4 * 4;

			IntPtr pLineGeneralInfo = lineGeneralInfo.StructureToPtr();

			uRet = lineGetGeneralInfo(lineCurrentDevice.lineHandle, pLineGeneralInfo);
			if (uRet == (uint)LINECALLRETURN.LINEERR_OK)
				lineGeneralInfo.PtrToStructure(pLineGeneralInfo);

			lineGeneralInfo.Dispose(pLineGeneralInfo);

			return uRet;
		}

		#endregion // TAPI Low Level Calls

		#region P/Invoke API Calls

		[DllImport("coredll.dll")]
		public static extern uint lineInitializeEx(
			out IntPtr hLineApp,
			IntPtr hAppHandle,
			IntPtr lCalllBack,
			string lpszFriendlyAppName,
			out uint uDevCount,
			ref uint APIVersion,
			LINEINITIALIZEEXPARAMS lpLineInitializeExParams);

		[DllImport("coredll")]
		public static extern int lineGetMessage(IntPtr m_hLineApp, out LINEMESSAGE lpMessage, int dwTimeout);

		[DllImport("coredll.dll")]
		internal static extern uint lineNegotiateAPIVersion(
			IntPtr hLineApp,
			uint dwDeviceID,
			uint dwAPILowVersion,
			uint dwAPIHighVersion,
			out uint lpdwAPIVersion,
			out LINEEXTENSIONID lineExtensionId);

		[DllImport("coredll.dll")]
		internal static extern uint lineGetDevCaps(
			IntPtr hLineApp,
			uint dwDeviceID,
			uint dwAPIVersion,
			uint dwExtVersion,
			IntPtr pLineDevCaps);

		[DllImport("coredll.dll")]
		internal static extern uint lineOpen(
			IntPtr hLineApp,
			uint dwDeviceID,
			out IntPtr hLine,
			uint dwAPIVersion,
			uint dwExtVersion,
			uint dwCallbackInstance,
			LINECALLPRIVILEGE dwPrivileges,
			LINEMEDIAMODE dwMediaModes,
			uint lpCallParams);

		[DllImport("coredll.dll")]
		internal static extern uint lineSetStatusMessages(
			IntPtr hLine,
			LINEDEVSTATE dwLineStates,
			uint dwAddressStates);

		[DllImport("coredll.dll")]
		internal static extern uint lineMakeCall(
			IntPtr hLine,
			out IntPtr hCall,
			string DestAddress,
			uint CountryCode,
			IntPtr lpCallParams);

		[DllImport("coredll.dll")]
		internal static extern uint lineAnswer(
			IntPtr hLine,
			IntPtr userInfo,
			uint dwSize);

		[DllImport("coredll.dll")]
		internal static extern uint lineDrop(
			IntPtr hCall,
			string UserInfo,
			uint dwSize);

		[DllImport("coredll.dll")]
		private static extern uint lineGetLineDevStatus(
			IntPtr hLine,
			IntPtr lplinegetdevstatus);

		[DllImport("coredll.dll")]
		internal static extern uint lineClose(IntPtr hLine);

		[DllImport("coredll.dll")]
		internal static extern uint lineShutdown(System.IntPtr hLineApp);

		[DllImport("cellcore")]
		public static extern uint lineSetEquipmentState(
			IntPtr hLine,
			uint dwState);

		[DllImport("cellcore")]
		public static extern uint lineGetEquipmentState(
			IntPtr hLine,
			out uint dwState,
			out uint dwRadioSupport);

		[DllImport("cellcore")]
		public static extern uint lineGetCurrentSystemType(
			IntPtr hLine,
			out LINESYSTEMTYPE lpdwCurrentSystemType);

		[DllImport("cellcore")]
		public static extern uint lineGetRegisterStatus(
			IntPtr hLine,
			out LINEREGSTATUS lpdwRegisterStatus);

		[DllImport("cellcore")]
		public static extern uint lineGetCurrentOperator(
			IntPtr hLine,
			IntPtr lpLineOperator);

		[DllImport("cellcore")]
		public static extern uint lineGetGeneralInfo(
			IntPtr hLine,
			IntPtr lpLineGeneralInfo);

		#endregion P/Invoke API Calls
	}
}
