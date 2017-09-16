//--------------------------------------------------------------------
// FILENAME: API.cs
//
// Copyright © 2011 - 2013 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION: Implements the code which communicates with the 
//				EMDK for .NET scanner API - Symbol.Barcode2.
//
// NOTES:
//
// 
//--------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Symbol.Barcode2;

/// <summary>
/// The namespace for CS_Barcode2Sample1.
/// </summary>
namespace CS_DocCapSample1
{

	/// <summary>
	/// The class which communicates with the EMDK for .NET scanner API 
	/// - Symbol.Barcode2. 
	/// </summary>
	class API
	{
		private Barcode2 myBarcode2 = null;

		/// <summary>
		/// Initialize the Barcode2 object.
		/// </summary>
		public bool InitBarcode()
		{
			// If the Barcode2 object is already initialized then fail the initialization.
            if (myBarcode2 != null)
			{
				return false;
			}
			else // Else initialize the reader.
			{
				try
				{
					// Get the device selected by the user.
					Device MyDevice =
						SelectDevice.Select(
						"Barcode",
						Symbol.Barcode2.Devices.SupportedDevices);

					if (MyDevice == null)
					{
						MessageBox.Show(Resources.GetString("NoDeviceSelected"), Resources.GetString("SelectDevice"));
						return false;
					}

					// Create the reader, based on selected device.
                    myBarcode2 = new Barcode2(MyDevice);

                    myBarcode2.Enable();

                    if (myBarcode2.Config.Decoders.DOCCAP.IsSupported == false)
                    {
                        MessageBox.Show(Resources.GetString("DocCapNotSupported"), Resources.GetString("Error"));
                        return false;
                    }

                    // If DocCap 2.0 is supported, swtching to the User Triger mode and Advanced Edge detection.
                    if (myBarcode2.DeviceInfo.DocCapVersion == DOCCAP_VERSION.DOCCAP_2)
                    {
                        // These two parameters available only in DocCap 2.0
                        myBarcode2.Config.Decoders.DOCCAP.TriggerMode = DOCCAP_TRIGGER_MODE.USER;
                        myBarcode2.Config.Decoders.DOCCAP.EdgeDetectionType = DOCCAP_EDGE_DETECTION_TYPE.ADVANCED;
                    }

                    // The size of the buffer was determined to be sufficient 
                    // for testing the DocCap label distributed with EMDK. 
                    // Depending on your requirement, you must increase or decrease the buffer size
                    myBarcode2.Config.ScanDataSize = (800 * 600 * 1) + 8192;

                    // The DOCCAP decoder must be enabled in order to use the document capture feature
                    myBarcode2.Config.Decoders.DOCCAP.Enabled = true;

                    myBarcode2.Config.Decoders.DOCCAP.Mode = DOC_CAPTURE_MODES.FREE_FORM;
                    myBarcode2.Config.Decoders.DOCCAP.ImgBrighten = IMAGE_BRIGHTEN_STATUS.ENABLED;
                    myBarcode2.Config.Decoders.DOCCAP.JPEGImageQuality = 100;
                    myBarcode2.Config.Decoders.DOCCAP.ImgDeskew = IMAGE_DESKEW_STATUS.ENABLED;

                    // In order for the barcode in the document to be decoded, 
                    // the corresponding decoder must be enabled and selected.
                    // The DocCap label distributed along with the sample uses Code128 symbology.
                    // In order for the sample to be used with documents having any of the supported 
                    // symbologies, we are enabling and selecting all supported decoders.
                    if (myBarcode2.Config.Decoders.CODE128.IsSupported)
                    {
                        myBarcode2.Config.Decoders.CODE128.Enabled = true;
                        // This will enable EAN128 variant also.
                        myBarcode2.Config.Decoders.CODE128.EAN128 = true;
                        myBarcode2.Config.Decoders.CODE128.Other128 = true;
                    }

                    if (myBarcode2.Config.Decoders.PDF417.IsSupported)
                    {
                        myBarcode2.Config.Decoders.PDF417.Enabled = true;
                    }

                    if (myBarcode2.Config.Decoders.CODE39.IsSupported)
                    {
                        myBarcode2.Config.Decoders.CODE39.Enabled = true;
                    }

                    if (myBarcode2.Config.Decoders.CODABAR.IsSupported)
                    {
                        myBarcode2.Config.Decoders.CODABAR.Enabled = true;
                    }

                    if (myBarcode2.Config.Decoders.I2OF5.IsSupported)
                    {
                        myBarcode2.Config.Decoders.I2OF5.Enabled = true;
                    }

                    if (myBarcode2.Config.Decoders.D2OF5.IsSupported)
                    {
                        myBarcode2.Config.Decoders.D2OF5.Enabled = true;
                    }

                    if (myBarcode2.Config.Decoders.DATAMATRIX.IsSupported)
                    {
                        myBarcode2.Config.Decoders.DATAMATRIX.Enabled = true;
                    }

                    // Select all the supported symbologies for DOCCAP.
                    myBarcode2.Config.Decoders.DOCCAP.SelectedSymbologies.SelectAll();

                    myBarcode2.Config.Decoders.Set();
				}

				catch (OperationFailureException ex)
				{
					MessageBox.Show(Resources.GetString("InitBarcode")+"\n" +
						Resources.GetString("OperationFailure") + "\n" + ex.Message +
						"\n" +
						Resources.GetString("Result") +" = " + (Results)((uint)ex.Result)
						);

					return false;
				}

				return true;
			}
		}

		/// <summary>
		/// Stop reading and disable/close the Barcode2 object.
		/// </summary>
		public void TermBarcode()
		{
			// If we have a reader
			if (myBarcode2 != null)
			{
				try
				{
					// stop all the notifications.
					StopScan();

					// Free it up.
					myBarcode2.Dispose();

					// Make the reference null.
                    myBarcode2 = null;
				}

				catch (OperationFailureException ex)
				{
					MessageBox.Show(Resources.GetString("TermBarcode") + "\n" +
                        Resources.GetString("OperationFailure") + "\n" + ex.Message +
						"\n" +
                        Resources.GetString("Result") + " = " + (Results)((uint)ex.Result)
						);
				}
			}
		}

		/// <summary>
		/// Start a scan.
		/// </summary>
		public void StartScan()
		{
            if (myBarcode2 != null)
            {
                try
                {
                    // Submit a scan.
                    if (myBarcode2.IsScanPending == false)
                    {
                        myBarcode2.Scan();
                    }
                }

                catch (Symbol.Exceptions.OperationFailureException ex)
                {
                    MessageBox.Show(Resources.GetString("StartScan") + "\n" +
                        Resources.GetString("OperationFailure") + "\n" + ex.Message +
                        "\n" +
                        Resources.GetString("Result") + " = " + (Symbol.Results)((uint)ex.Result));
                }
            }
		}

		/// <summary>
		/// Stop all reads on the reader.
		/// </summary>
		public void StopScan()
		{
			//If we have a reader
            if (myBarcode2 != null)
			{
				try
				{
					// Flush (Cancel all pending reads).
                    myBarcode2.ScanCancel();
				}

				catch (OperationFailureException ex)
				{
					MessageBox.Show(Resources.GetString("StopScan") + "\n" +
                        Resources.GetString("OperationFailure") + "\n" + ex.Message +
						"\n" +
                        Resources.GetString("Result") + " = " + (Results)((uint)ex.Result)
						);
				}
			}
		}

		/// <summary>
		/// Provides the access to the Symbol.Barcode.Reader reference.
		/// The user can use this reference for his additional Reader - related operations.
		/// </summary>
		public Barcode2 Barcode2 
		{
			get
			{
				return myBarcode2;
			}
		}

		/// <summary>
		/// Attach a ScanNotify handler.
		/// </summary>
		public void AttachScanNotify(Barcode2.OnScanHandler ScanNotifyHandler)
		{
			// If we have a reader
			if( myBarcode2 != null)
			{
				// Attach the read notification handler.
                myBarcode2.OnScan += ScanNotifyHandler;
			}

		}

		/// <summary>
		/// Detach the ScanNotify handler.
		/// </summary>
        public void DetachScanNotify(Barcode2.OnScanHandler ScanNotifyHandler)
		{
            if (myBarcode2 != null)
			{
				// Detach the read notification handler.
                myBarcode2.OnScan -= ScanNotifyHandler;
			}
		}

		/// <summary>
		/// Attach a StatusNotify handler.
		/// </summary>
		public void AttachStatusNotify(Barcode2.OnStatusHandler StatusNotifyHandler)
		{
			// If we have a reader
			if( myBarcode2 != null)
			{
				// Attach status notification handler.
				myBarcode2.OnStatus += StatusNotifyHandler;
			}
		}

		/// <summary>
		/// Detach a StatusNotify handler.
		/// </summary>
        public void DetachStatusNotify(Barcode2.OnStatusHandler StatusNotifyHandler)
		{
			// If we have a reader registered for receiving the status notifications
            if (myBarcode2 != null)
			{
				// Detach the status notification handler.
                myBarcode2.OnStatus -= StatusNotifyHandler;
			}
		}

	}

}
 