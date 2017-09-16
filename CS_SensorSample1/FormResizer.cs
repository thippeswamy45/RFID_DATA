//------------------------------------------------------------------------------------------------------------------
// FILENAME: ResizeControl.cs
//
// Copyright © 2012 - 2013 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION: The source file for the FormResizer.cs to resize the GUI based on the screen resolution and
// device orientation.
//
//------------------------------------------------------------------------------------------------------------------
//  
// This sample is designed to run on different devices with different resolutions. The FormResizer object is used to
// resize the form and its controls and the FormResizer class does not contain any Sensor class library related code.
//
// This sample is provided for demonstration purpose only and is not intended for use in the production environment.
// 
//------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CS_SensorSample1
{
    public class FormResizer
    {
        private static bool isPortrait = true;   // The default dispaly orientation 
        // has been set to Portrait.

        private bool isSkipMaxLen = false;    // The restriction on the maximum 
        // physical length is considered by default.

        private bool isInitialScale = true;   // The flag to track whether the 
        // scaling logic is applied for
        // the first time (from scatch) or not.
        // Based on that, the (outer) width/height values
        // of the form will be set or not.
        // Initially set to true.

        private int resWidthReference = 0;
        // In constructor, INITIALLY HAS TO BE SET TO THE WIDTH OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private int resHeightReference = 0;
        // In constructor, INITIALLY HAS TO BE SET TO THE HEIGHT OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private const double maxLength = 5.5;  // The maximum physical width/height of the sample (in inches).
        // The actual value on the device may slightly deviate from this
        // since the calculations based on the (received) DPI & resolution values 
        // would provide only an approximation, so not 100% accurate.

        private Form currentForm = null;

        private bool isFormClosed = false;

        public FormResizer(Form frm, int formWidth, int formHeight)
        {
            resWidthReference = formWidth;
            resHeightReference = formHeight;
            currentForm = frm;
            currentForm.Load   += new EventHandler(currentForm_Load);
            currentForm.Resize += new EventHandler(currentForm_Resize);
            currentForm.Closed += new EventHandler(currentForm_Close);
        }

        /// <summary>
        /// This function does the initialization parameters required for scaling of the form
        /// </summary>
        public void Initialize()
        {
            if (Screen.PrimaryScreen.Bounds.Width > Screen.PrimaryScreen.Bounds.Height)
            {
                isPortrait = false; // If the display orientation is not portrait (so it's landscape), set the flag to false.
            }

            if (currentForm.WindowState == FormWindowState.Maximized)    // If the form is maximized by default.
            {
                this.isSkipMaxLen = true; // we need to skip the max. length restriction
            }

            if ((Symbol.Win32.PlatformType.IndexOf("WinCE") != -1) || (Symbol.Win32.PlatformType.IndexOf("WindowsCE") != -1) || (Symbol.Win32.PlatformType.IndexOf("Windows CE") != -1)) // Only on Windows CE devices
            {
                this.resWidthReference = currentForm.Width;   // The width of the form at design time (in pixels) is obtained from the platorm.
                this.resHeightReference = currentForm.Height; // The height of the form at design time (in pixels) is obtained from the platform.
            }
        }

        /// <summary>
        /// This function scales the given Form & its child controls in order to
        /// make them completely viewable, based on the screen width & height.
        /// </summary>
        private void Scale(Form frm)
        {
            int PSWAW = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;    // The width of the working area (in pixels).
            int PSWAH = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;   // The height of the working area (in pixels).

            // The entire screen has been taken in to account below 
            // in order to decide the half (S)VGA settings etc.
            if (!((Screen.PrimaryScreen.Bounds.Width <= (1.5) * (Screen.PrimaryScreen.Bounds.Height))
            && (Screen.PrimaryScreen.Bounds.Height <= (1.5) * (Screen.PrimaryScreen.Bounds.Width))))
            {
                if ((Screen.PrimaryScreen.Bounds.Width) > (Screen.PrimaryScreen.Bounds.Height))
                {
                    // If the width/height ratio goes beyond 1.5, the (longer) effective width is made shorter.
                    PSWAW = (int)((1.33) * PSWAH); 
                }

            }

            System.Drawing.Graphics graphics = frm.CreateGraphics();

            float dpiX = graphics.DpiX; // Get the horizontal DPI value.

            if (isInitialScale == true) // If an initial scale (from scratch)
            {
                if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1) // If the platform is either Pocket PC or Windows Mobile
                {
                    // Set the form width. However this setting
                    // would be the ultimate one for Pocket PC (& Windows Mobile)devices.
                    // Just for the sake of consistency, it's explicitely specified here.
                    frm.Width = PSWAW; 
                }
                else
                {
                    frm.Width = (int)((frm.Width) * (PSWAW)) / (resWidthReference); // Set the form width for others (Windows CE devices).

                }
            }
            // The calculation of the width & left values for each control
            // without taking the maximum length restriction into consideration.
            if ((frm.Width <= maxLength * dpiX) || isSkipMaxLen == true) 
            {
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {
                    cntrl.Width = ((cntrl.Width) * (frm.Width)) / (resWidthReference);
                    cntrl.Left = ((cntrl.Left) * (frm.Width)) / (resWidthReference);

                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Width = (((cntrl2.Width) * (frm.Width)) / (resWidthReference));
                                cntrl2.Left = (((cntrl2.Left) * (frm.Width)) / (resWidthReference));
                            }
                        }
                    }
                }
            }
            else
            {   // The calculation of the width & left values for each control
                // with the maximum length restriction taken into consideration.
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {
                    cntrl.Width = (int)(((cntrl.Width) * (PSWAW) * (maxLength * dpiX)) / (resWidthReference * (frm.Width)));
                    cntrl.Left = (int)(((cntrl.Left) * (PSWAW) * (maxLength * dpiX)) / (resWidthReference * (frm.Width)));

                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Width = (int)(((cntrl2.Width) * (PSWAW) * (maxLength * dpiX)) / (resWidthReference * (frm.Width)));
                                cntrl2.Left = (int)(((cntrl2.Left) * (PSWAW) * (maxLength * dpiX)) / (resWidthReference * (frm.Width)));
                            }
                        }
                    }
                }
                frm.Width = (int)((frm.Width) * (maxLength * dpiX)) / (frm.Width);
            }

            resWidthReference = frm.Width; // Set the reference width to the new value.


            // A similar calculation is performed below for the height & top values for each control ...

            if (!((Screen.PrimaryScreen.Bounds.Width <= (1.5) * (Screen.PrimaryScreen.Bounds.Height))
            && (Screen.PrimaryScreen.Bounds.Height <= (1.5) * (Screen.PrimaryScreen.Bounds.Width))))
            {
                if ((Screen.PrimaryScreen.Bounds.Height) > (Screen.PrimaryScreen.Bounds.Width))
                {
                    PSWAH = (int)((1.33) * PSWAW);
                }
            }

            float dpiY = graphics.DpiY;

            if (isInitialScale == true)
            {
                if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
                {
                    frm.Height = PSWAH;
                }
                else
                {
                    frm.Height = (int)((frm.Height) * (PSWAH)) / (resHeightReference);
                }
            }

            if ((frm.Height <= maxLength * dpiY) || isSkipMaxLen == true)
            {
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {
                    cntrl.Height = ((cntrl.Height) * (frm.Height)) / (resHeightReference);
                    cntrl.Top = ((cntrl.Top) * (frm.Height)) / (resHeightReference);

                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Height = ((cntrl2.Height) * (frm.Height)) / (resHeightReference);
                                cntrl2.Top = ((cntrl2.Top) * (frm.Height)) / (resHeightReference);
                            }
                        }
                    }

                }

            }
            else
            {
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {

                    cntrl.Height = (int)(((cntrl.Height) * (PSWAH) * (maxLength * dpiY)) / (resHeightReference * (frm.Height)));
                    cntrl.Top = (int)(((cntrl.Top) * (PSWAH) * (maxLength * dpiY)) / (resHeightReference * (frm.Height)));


                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Height = (int)(((cntrl2.Height) * (PSWAH) * (maxLength * dpiY)) / (resHeightReference * (frm.Height)));
                                cntrl2.Top = (int)(((cntrl2.Top) * (PSWAH) * (maxLength * dpiY)) / (resHeightReference * (frm.Height)));
                            }
                        }
                    }
                }
                frm.Height = (int)((frm.Height) * (maxLength * dpiY)) / (frm.Height);
            }

            resHeightReference = frm.Height;

            if (isInitialScale == true)
            {
                isInitialScale = false; // If this was the initial scaling (from scratch), it's now complete.
            }
            if (isSkipMaxLen == true)
            {
                isSkipMaxLen = false; // No need to consider the maximum length restriction now.
            }
        }

        void currentForm_Resize(object sender, EventArgs e)
        {
            if (isInitialScale == true || isFormClosed)
            {
                return; // Return if the initial scaling (from scratch)is not complete.
            }

            if (Screen.PrimaryScreen.Bounds.Width > Screen.PrimaryScreen.Bounds.Height) // If landscape orientation
            {
                if (isPortrait != false) // If an orientation change has occured to landscape
                {
                    isPortrait = false; // Set the orientation flag accordingly.
                    isInitialScale = true; // An initial scaling is required due to orientation change.
                }
                else
                {   // No orientation change has occured
                    isSkipMaxLen = true; // Initial scaling is now complete, so skipping the max. length restriction is now possible.
                }
            }
            else
            {
                // Similarly for the portrait orientation...
                if (isPortrait != true)
                {
                    isPortrait = true;
                    isInitialScale = true;
                }
                else
                {
                    isSkipMaxLen = true;                   
                }
            } 
            Scale(currentForm);// Scale the GUI.
        }

        void currentForm_Load(object sender, EventArgs e)
        {
            if (!isFormClosed)
            {
                Initialize(); //Intialize the scale parameters
                Scale(currentForm); // Scale the GUI.
            }
        }

        void currentForm_Close(object sender, EventArgs e)
        {
            isFormClosed = true;
        }
    }
}
