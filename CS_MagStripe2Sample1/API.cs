//--------------------------------------------------------------------
// FILENAME: API.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION: Implements the code which communicates with the 
//					EMDK for .NET MagStripe API - Symbol.MagStripe2.
//
// NOTES:
//
// 
//--------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Symbol.MagStripe2;

/// <summary>
/// The namespace for CS_MagStripe2Sample1.
/// </summary>
namespace CS_MagStripe2Sample1
{

	/// <summary>
	/// The class which communicates with the EMDK for .NET scanner API 
	/// - Symbol.Barcode2. 
	/// </summary>
	class API
	{
		private MagStripe2 myMagStripe2 = null;

        /// <summary>
        /// Initialize the MagStripe2 object.
        /// </summary>
        public bool InitMagStripe()
        {
            // If reader is already present then fail initialize
            if (this.myMagStripe2 != null)
            {
                return false;
            }

            try
            {
                if (Devices.SupportedDevices.Length > 1)
                {
                    SelectDevForm devFrm = new SelectDevForm();
                    devFrm.DoScale();
                    devFrm.ShowDialog();
                    string devName = devFrm.GetDeviceName();
                    if ((devName != null) && (devName != ""))
                    {
                        myMagStripe2 = new MagStripe2(devName);
                    }
                    else
                    {
                        MessageBox.Show("No device selected.");
                        return false;
                    }

                }
                else
                {
                    // Create new reader, first available reader will be used.
                    myMagStripe2 = new MagStripe2();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

		/// <summary>
		/// Stop reading and disable/close the MagStripe2 object.
		/// </summary>
        public void TermMagStripe()
		{
			// If we have a reader
            if (myMagStripe2 != null)
            {
                // stop all the swipes.
                StopSwipe();

                // Free it up.
                myMagStripe2.Dispose();

                // Make the reference null.
                myMagStripe2 = null;
            }
		}

        /// <summary>
        /// Start a swipe on the reader
        /// </summary>
        public void StartSwipe()
        {
            // If we have both a reader
            if ((this.myMagStripe2 != null))
            {
                // Submit a swipe
                this.myMagStripe2.Swipe();
            }
        }

        /// <summary>
        /// Stop all swipes on the reader
        /// </summary>
        public void StopSwipe()
        {
            // If we have a reader
            if (this.myMagStripe2 != null)
            {
                // Cancel all pending reads
                myMagStripe2.SwipeCancel();
            }
        }

		/// <summary>
		/// Provides the access to the MagStripe2 reference.
		/// The user can use this reference for his additional magstripe - related operations.
		/// </summary>
        public MagStripe2 MagStripe2 
		{
			get
			{
				return myMagStripe2;
			}
		}

		/// <summary>
        /// Attach a SwipeNotify handler.
		/// </summary>
        public void AttachSwipeNotify(MagStripe2.OnSwipeHandler SwipeNotifyHandler)
		{
			// If we have a reader
			if( myMagStripe2 != null)
			{
				// Attach the swipe notification handler.
                myMagStripe2.OnSwipe += SwipeNotifyHandler;
			}

		}

		/// <summary>
        /// Detach the SwipeNotify handler.
		/// </summary>
        public void DetachSwipeNotify(MagStripe2.OnSwipeHandler SwipeNotifyHandler)
		{
            if (myMagStripe2 != null)
			{
                // Detach the swipe notification handler.
                myMagStripe2.OnSwipe -= SwipeNotifyHandler;
			}
		}
	}

}
 