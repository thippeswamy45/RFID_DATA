//--------------------------------------------------------------------
// FILENAME: Program.cs
//
// Copyright(c) 2009 Symbol Technologies Inc. All rights reserved.
//
// DESCRIPTION:
//      This module is part of the ScanInventory application.   It provides
//      the entry point for the application.  
//
// NOTES:
//      Prior to loading the MainForm, the entry point looks to see if 
//      it connect to MT2000 Scanner services and place it in the correct 
//      mode.  If this is completes without error, the main form is loaded. 
//
//      This software is provided as is as an example of how to use the 
//      MT2000 Scanner services assemblies. 
//      
// 
//--------------------------------------------------------------------
using System;
using System.Windows.Forms;
using Microsoft.Win32;

using Symbol.MT2000.Utils;
using Symbol.MT2000.UserInterface;
using Symbol.MT2000.ScannerServices;

namespace CS_MT2000_ScanInventory
{
	static class Program
	{
		// public variables
		public static ScannerServicesClient ScannerServicesClient = null;
        public static bool useSendRaw = true;
        public static byte previousHostMode = (byte)Symbol.MT2000.ScannerServices.ENUM_HOSTS.HOST_RS232;
        public static bool deleteAfterTransmit = false; 

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[MTAThread]
		static void Main()
		{
			// create the scanner services interface
			if (!ScannerServicesClient.IsServiceRunning)
			{
				MsgBox.Show(null, Properties.Resources.StrScanInventory, Properties.Resources.StrErrorScannerServicesNotRunning);
				return;
			}
			ScannerServicesClient = new ScannerServicesClient();

            // Test connection..
			if (!ScannerServicesClient.Connect(true))
			{
				MsgBox.Show(null, Properties.Resources.StrScanInventory, Properties.Resources.StrErrorCantStartScannerServices);
				return;
			}
			if (RESULTCODE.E_OK != ScannerServicesClient.SetMode(SCANNERSVC_MODE.SVC_MODE_DECODE))
			{
				MsgBox.Show(null, Properties.Resources.StrScanInventory, Properties.Resources.StrErrorCantSetScannerServicesMode);
				ScannerServicesClient.Disconnect();
				ScannerServicesClient.Dispose();
				return;
			}

            //// determine if we should send it raw or via "SendLabel". 
            RegistryKey key = Registry.LocalMachine.OpenSubKey("\\software\\symbol\\MT2000\\ScanInventory");
            if (key != null)
            {
                int value;

                // get the image format
                try
                {
                    value = (int)key.GetValue("DeleteAfterTransmit");
                }
                catch
                {
                    value = 0;
                }

                deleteAfterTransmit = System.Convert.ToBoolean(value);
            }

            if (RESULTCODE.E_OK != ScannerServicesClient.GetAttributeByte((ushort)ATTRIBUTE_NUMBER.ATT_MIA_HOSTNUM, out Program.previousHostMode))
            {
                MsgBox.Show(null, Properties.Resources.StrScanInventory, Properties.Resources.StrErrorCantSetRawMode);
                ScannerServicesClient.Disconnect();
                ScannerServicesClient.Dispose();
                return;
            }

            if (Program.previousHostMode != (byte)ENUM_HOSTS.HOST_RAW)
            {
                useSendRaw = false;
            }

            if (useSendRaw)
            {               
                if (RESULTCODE.E_OK != ScannerServicesClient.SetAttributeByte((ushort)ATTRIBUTE_NUMBER.ATT_MIA_HOSTNUM, (byte)ENUM_HOSTS.HOST_RAW))
                {
                    MsgBox.Show(null, Properties.Resources.StrScanInventory, Properties.Resources.StrErrorCantSetRawMode);
                    ScannerServicesClient.Disconnect();
                    ScannerServicesClient.Dispose();
                    return;
                }
            }

            ScannerServicesClient.Disconnect(); 

			// run the application
			SystemMonitor.Start();
			Application.Run(new MainForm());
			SystemMonitor.Stop();

            Inventory.Save();

			// shut down the scanner interface
			if (ScannerServicesClient != null)
			{
                ScannerServicesClient.Connect(false);
                if (useSendRaw)
                {
                    ScannerServicesClient.SetAttributeByte((ushort)ATTRIBUTE_NUMBER.ATT_MIA_HOSTNUM, (byte)Program.previousHostMode);
                }
				ScannerServicesClient.Disconnect();
				ScannerServicesClient.Dispose();
				ScannerServicesClient = null;
			}
		}
	}
}