//-----------------------------------------------------------------------------------
// FILENAME: MainForm.cs
//
// Copyright © 2011 - 2013 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION: The source file for the MainForm.
//
// ----------------------------------------------------------------------------------
//  
//	 This sample demonstrates the usage of the EMDK for .NET API Symbol.Barcode2 
//   in order to access the functionality of document capture. Please note the 
//   fact that this sample covers only the most basic operations associated with 
//   the barcode scanner, so illustrates the usage of only a subset of the complete 
//   API available.
//	
// ----------------------------------------------------------------------------------
// 
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.Barcode2;

namespace CS_DocCapSample1
{
    public partial class MainForm : Form
    {
        private ResizeControls ResizeControls = null;
        private API myScanAPI = null;
        private Barcode2.OnScanHandler myScanNotifyHandler = null;
        private Barcode2.OnStatusHandler myStatusNotifyHandler = null;
        private DisplayForm displayForm;

        public MainForm()
        {
            InitializeComponent();
            // This sample is designed to run on different devices with different resolutions.
            // The ResizeControls object is used to resize the form and its controls.
            // The ResizeControls class does not contain any barcode scanning or document capture related code
            ResizeControls = new ResizeControls(this);
        }

        private void Exit_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // MainMenu is needed on PocketPC (Windows Mobile) devices, because it provides the OK/Minimize button 
            // and the Windows Start button at the bottom of the application. If not, the application goes to full screen 
            // and there is no way close/minimize the app, unless using specific controls in the app to do it
            if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
            {
                this.Menu = new MainMenu();
            }

            myScanAPI = new API();

            if (false == myScanAPI.InitBarcode())// If the Barcode object has not been initialized
            {
                // Display a message & exit the application.
                MessageBox.Show(Resources.GetString("AppExitMsg"));
                myScanAPI.TermBarcode();
                Application.Exit();
                return;
            }

            // Attach a scan notification handler.
            this.myScanNotifyHandler = new Barcode2.OnScanHandler(myBarcode2_ScanNotify);
            myScanAPI.AttachScanNotify(myScanNotifyHandler);

            // Attach a status notification handler.
            this.myStatusNotifyHandler = new Barcode2.OnStatusHandler(myBarcode2_StatusNotify);
            myScanAPI.AttachStatusNotify(myStatusNotifyHandler);

            // By default, the Freeform mode is chosen.
            Mode_FreeForm_RB.Checked = true;

            myScanAPI.StartScan();
        }

        /// <summary>
        /// Read notification handler.
        /// </summary>
        private void myBarcode2_ScanNotify(ScanDataCollection scanDataCollection)
        {
            // Get ScanData
            ScanData scanData = scanDataCollection.GetFirst;

            switch (scanData.Result)
            {
                case Results.SUCCESS:
                    // Handle the data from this read & submit the next read.
                    HandleData(scanData);
                    break;

                case Results.CANCELED:
                    break;

                default:

                    string sMsg = Resources.GetString("ScanFailedErrorMsg")+
                        (scanData.Result).ToString();

                    if (scanData.Result == Results.E_SCN_READINCOMPATIBLE)
                    {
                        // If the failure is E_SCN_READINCOMPATIBLE, exit the application.
                        MessageBox.Show(Resources.GetString("AppExitMsg"), Resources.GetString("Failure"));

                        this.Close();
                        return;
                    }

                    MessageBox.Show(sMsg);

                    break;
            }

            this.myScanAPI.StartScan(); // Start Scan after an error also.
        }

        /// <summary>
        /// Status notification handler.
        /// </summary>
        private void myBarcode2_StatusNotify(StatusData statusData)
        {
            // Check if doccap status available, if so gives prority. If not shows the high level status.
            if (statusData.DocCapState != DOCCAP_STATE.NOT_AVAILABLE)
            {
                switch (statusData.DocCapState)
                {
                    case DOCCAP_STATE.WAITING:
                        statusBar1.Text = Resources.GetString("DocCapWaitingMsg");
                        break;
                    case DOCCAP_STATE.PREVIEWING:
                        statusBar1.Text = Resources.GetString("DocCapPreviewingMsg");
                        break;
                    case DOCCAP_STATE.CAPTURING:
                        statusBar1.Text = Resources.GetString("DocCapCapturingMsg");
                        break;
                    case DOCCAP_STATE.RESULT_PENDING:
                        statusBar1.Text = Resources.GetString("DocCapResultPendingMsg");
                        break;
                    case DOCCAP_STATE.PROCESSING:
                        statusBar1.Text = Resources.GetString("DocCapProcessingMsg");
                        break;
                    case DOCCAP_STATE.IMAGE_REJECTED:
                        statusBar1.Text = Resources.GetString("DocCapImageRejectedMsg");
                        break;
                    case DOCCAP_STATE.IMAGE_AVAILABLE:
                        statusBar1.Text = Resources.GetString("DocCapImageAvailableMsg");
                        break;
                    case DOCCAP_STATE.ERROR:
                        statusBar1.Text = Resources.GetString("DocCapErrorMsg");
                        break;
                    default:
                        statusBar1.Text = string.Empty;
                        break;
                }
            }
            else
            {
                switch (statusData.State)
                {
                    case States.WAITING:
                        statusBar1.Text = Resources.GetString("TriggerMsg");
                        break;

                    case States.READY:
                        statusBar1.Text = Resources.GetString("AimMsg");
                        break;

                    default:
                        statusBar1.Text = statusData.Text;
                        break;
                }
            }
        }

        private void HandleData(ScanData scanData)
        {
            displayForm = new DisplayForm();
            displayForm.BarcodeData_Lb.Text = scanData.Text;

            if (myScanAPI.Barcode2.Config.Decoders.DOCCAP.Mode == DOC_CAPTURE_MODES.BARCODE_ANCHORED)
            {
                displayForm.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            // During document capture, the captured image data will be available as auxiliary data
            if (scanData.AuxDataFormat == AUX_FORMATS.IMAGE_DATA)
            {
                displayForm.pictureBox1.Image = (Image)scanData.AuxData.GetBitmap().Clone();
            }
            displayForm.ShowDialog();
            displayForm.Dispose();
        }

        private void Mode_FreeForm_RB_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                myScanAPI.StopScan();
                myScanAPI.Barcode2.Config.Decoders.DOCCAP.Mode = DOC_CAPTURE_MODES.FREE_FORM;
                if (myScanAPI.Barcode2.Config.Decoders.CODE128.IsSupported)
                {
                    // In the free-form mode, inclusion of the barcode within the form is optional
                    // and it will be decoded only if its symbology is enabled.
                    // The DocCap sample form distibuted with the sample contains Code128 barcode.
                    // For demo purpose, the Code128 symoblogy is disabled here, so only the image
                    // of the form will be captured.
                    myScanAPI.Barcode2.Config.Decoders.CODE128.Enabled = false;
                }
                myScanAPI.Barcode2.Config.Decoders.Set();
                myScanAPI.StartScan();
            }
        }

        private void Mode_BarcodeLinked_RB_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                myScanAPI.StopScan();
                myScanAPI.Barcode2.Config.Decoders.DOCCAP.Mode = DOC_CAPTURE_MODES.BARCODE_LINKED;
                if (myScanAPI.Barcode2.Config.Decoders.CODE128.IsSupported)
                {
                    // In Barcode-linked mode the barcode is mandatory on the form.
                    // The DocCap sample form distibuted with the sample contains Code128 barcode.
                    // So Code128 is enabled and selected here
                    myScanAPI.Barcode2.Config.Decoders.CODE128.Enabled = true;
                    //myScanAPI.Barcode2.Config.Decoders.DOCCAP.SelectedSymbologies.CODE128 = true;
                }
                myScanAPI.Barcode2.Config.Decoders.Set();
                myScanAPI.StartScan();
            }
        }

        private void Mode_Barcode_Anchored_RB_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                myScanAPI.StopScan();
                myScanAPI.Barcode2.Config.Decoders.DOCCAP.Mode = DOC_CAPTURE_MODES.BARCODE_ANCHORED;
                // The location and size of the capture region illustrated here is set according to the 
                // DocCap label distributed with the sample. This can be changed by 
                // the user according to region of the document being captured.
                myScanAPI.Barcode2.Config.Decoders.DOCCAP.ImageOffsetX = -286;
                myScanAPI.Barcode2.Config.Decoders.DOCCAP.ImageOffsetY = 70;
                myScanAPI.Barcode2.Config.Decoders.DOCCAP.ImageWidth = 174;
                myScanAPI.Barcode2.Config.Decoders.DOCCAP.ImageHeight = 38;
                if (myScanAPI.Barcode2.Config.Decoders.CODE128.IsSupported)
                {
                    // In Barcode-anchored mode the barcode is mandatory on the form.
                    // The DocCap sample form distibuted with the sample contains Code128 barcode.
                    // So Code128 is enabled and selected here
                    myScanAPI.Barcode2.Config.Decoders.CODE128.Enabled = true;
                    //myScanAPI.Barcode2.Config.Decoders.DOCCAP.SelectedSymbologies.CODE128 = true;
                }
                myScanAPI.Barcode2.Config.Decoders.Set();
                myScanAPI.StartScan();
            }
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            myScanAPI.DetachScanNotify(myScanNotifyHandler);
            myScanAPI.DetachStatusNotify(myStatusNotifyHandler);
            myScanAPI.TermBarcode();
        }
    }
}