//-----------------------------------------------------------------------------------
// FILENAME: DisplayForm.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION: The source file for the DisplayForm.
//
// ----------------------------------------------------------------------------------
//  
//	This sample demonstrates the usage of the EMDK for .NET API Symbol.Barcode2 
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

namespace CS_DocCapSample1
{
    public partial class DisplayForm : Form
    {
        public ResizeControls ResizeControls = null;

        public DisplayForm()
        {
            InitializeComponent();
            ResizeControls = new ResizeControls(this);
        }

        private void Back_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {
            // MainMenu is needed on PocketPC (Windows Mobile) devices, because it provides the OK/Minimize button 
            // and the Windows Start button at the bottom of the application. If not, the application goes to full screen 
            // and there is no way close/minimize the app, unless using specific controls in the app to do it
            if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
            {
                this.Menu = new MainMenu();
            }
        }
    }
}