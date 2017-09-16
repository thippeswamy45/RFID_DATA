//-----------------------------------------------------------------------------------
// FILENAME: SelectDevice.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION: Source file for the SelectDevice form.
//
// ----------------------------------------------------------------------------------
//  
//	This form lists the available scanners on the device
//  and allows the user to select one.
//	
// ----------------------------------------------------------------------------------
// 
//-----------------------------------------------------------------------------------


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using Symbol;

namespace CS_DocCapSample1
{
	/// <summary>
	/// The SelectDevice class provides a dialog for displaying and selecting a
	/// list of available Symbol.Barcode2.Device objects.
	/// </summary>
	/// <remarks>
    /// A SelectDevice dialog is displayed with the list of device choices 
    /// and the user selects one of them to be accessed.
	/// </remarks>
	public class SelectDevice : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.ListBox AvailableDevicesListBox;
		private System.Windows.Forms.Label AvailableDevicesLabel;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.Button OKButton;
		private int MySelection = -1;

		static private Symbol.Barcode2.Device[] OurAvailableDevices = null;
		static private string OurTitle;
		static private int DefaultIndex;

        private ResizeControls ResizeControls = null;

        
		/// <summary>
		/// The static Select method is the recommended way to create the SelectDevice
		/// dialog.
		/// </summary>
		/// <remarks>
		/// This method will display the SelectDevice dialog and block until a 
		/// selection has been made.
		/// </remarks>
		/// <param name="Title">A string that will be displayed as the title to the
		/// SelectDevice dialog.</param>
		/// <param name="AvailableDevices">An array of available Symbol.Device objects.
		/// </param>
		/// <returns>The selected device object.</returns>
		public static Symbol.Barcode2.Device Select(
            string Title, Symbol.Barcode2.Device[] AvailableDevices)
		{
			OurAvailableDevices = AvailableDevices;

			if ( OurAvailableDevices.Length == 0 )
			{
				return null;
			}

			if ( OurAvailableDevices.Length == 1 )
			{
				return OurAvailableDevices[0];
			}

			OurTitle = Title;
			DefaultIndex = 0;

			int nSelection = new SelectDevice().Selection;

			if ( nSelection < 0 )
			{
				return null;
			}

			return OurAvailableDevices[nSelection];
		}

		/// <summary>
		/// The static Select method is the recommended way to create the SelectDevice
		/// dialog.
		/// </summary>
		/// <remarks>
		/// This method will display the SelectDevice dialog and block until a 
		/// selection has been made.
		/// </remarks>
		/// <param name="Title">A string that will be displayed as the title to the
		/// SelectDevice dialog.</param>
		/// <param name="AvailableDevices">An array of available Symbol.Device objects.
		/// </param>
		/// <param name="SelectIndex">The index of the initially selected device
		/// object.</param>
		/// <returns>The selected device object.</returns>
        public static Symbol.Barcode2.Device Select(string Title,
            Symbol.Barcode2.Device[] AvailableDevices, int SelectIndex)
		{
			OurAvailableDevices = AvailableDevices;

			if ( OurAvailableDevices.Length == 0 )
			{
				return null;
			}

			if ( OurAvailableDevices.Length == 1 )
			{
				return OurAvailableDevices[0];
			}

			OurTitle = Title;
			DefaultIndex = SelectIndex;

			int nSelection = new SelectDevice().Selection;

			if ( nSelection < 0 )
			{
				return null;
			}

			return OurAvailableDevices[nSelection];
		}

		/// <summary>
		/// Default SelectDevice constructor.
		/// </summary>
		internal SelectDevice()
		{
			InitializeComponent();
            ResizeControls = new ResizeControls(this);
			this.OKButton.Focus();
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.CancelButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.AvailableDevicesListBox = new System.Windows.Forms.ListBox();
			this.AvailableDevicesLabel = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			// 
			// CancelButton
			// 
			this.CancelButton.Location = new System.Drawing.Point(8, 232);
			this.CancelButton.Size = new System.Drawing.Size(64, 25);
			this.CancelButton.Text = "Cancel";
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			this.CancelButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CancelButton_KeyDown);
			// 
			// OKButton
			// 
			this.OKButton.Location = new System.Drawing.Point(168, 232);
			this.OKButton.Size = new System.Drawing.Size(64, 25);
			this.OKButton.Text = "OK";
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			this.OKButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OKButton_KeyDown);
			// 
			// AvailableDevicesListBox
			// 
			this.AvailableDevicesListBox.Location = new System.Drawing.Point(8, 40);
			this.AvailableDevicesListBox.Size = new System.Drawing.Size(224, 170);
			// 
			// AvailableDevicesLabel
			// 
			this.AvailableDevicesLabel.Location = new System.Drawing.Point(8, 8);
			this.AvailableDevicesLabel.Size = new System.Drawing.Size(224, 24);
			this.AvailableDevicesLabel.Text = "Available Devices:";
			// 
			// SelectDevice
			// 
			this.ClientSize = new System.Drawing.Size(242, 264);
			this.Controls.Add(this.AvailableDevicesLabel);
			this.Controls.Add(this.AvailableDevicesListBox);
			this.Controls.Add(this.CancelButton);
			this.Controls.Add(this.OKButton);
			this.Menu = this.mainMenu1;
			this.Text = "Select";
			this.Load += new System.EventHandler(this.SelectDevice_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SelectDevice_KeyUp);

		}
		#endregion


		// Private: SelectDevice_Load
		private void SelectDevice_Load(object sender, System.EventArgs e)
		{
            //// Set wait cursor.
            //bool SaveWaitCursor = Symbol.Win32.WaitCursor;
            //Symbol.Win32.WaitCursor = true;

			try
			{

				this.AvailableDevicesListBox.Hide();

                foreach (Symbol.Barcode2.Device d in OurAvailableDevices)
                {

                    if (d.DeviceType != Symbol.Barcode2.DEVICETYPES.UNKNOWN)
                    {
                        this.AvailableDevicesListBox.Items.Add(d.DeviceType + " - " + d.FriendlyName);
                    }
                    else
                    {
                        this.AvailableDevicesListBox.Items.Add(d.FriendlyName);
                    }
                }

				this.AvailableDevicesListBox.SelectedIndex = 0;
				this.AvailableDevicesListBox.Show();
				this.KeyDown += new KeyEventHandler(SelectDevice_KeyDown);

				// Add MainMenu if Pocket PC
				if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
				{
					this.Menu = new MainMenu();
				}

			}
			finally
			{
				// restore wait cursor
				//Symbol.Win32.WaitCursor = SaveWaitCursor;
			}
		}

		// Private: Selection
		private int Selection
		{
			get
			{
				this.AvailableDevicesLabel.Text = "Select " + OurTitle + ":";
				this.ShowDialog();
				return MySelection;
			}
		}

		// Private: OKButton_Click
		private void OKButton_Click(object sender, System.EventArgs e)
		{
			MySelection = AvailableDevicesListBox.SelectedIndex;
			
			this.Close();
		}

		// Private: CancelButton_Click
		private void CancelButton_Click(object sender, System.EventArgs e)
		{
			MySelection = -1;
			this.Close();
		}

		private void OKButton_KeyDown(object sender, KeyEventArgs e)
		{
			// Checks if the key pressed was an enter button (character code 13)
			if (e.KeyValue == (char)13)
				OKButton_Click(this, e);
		}

		private void CancelButton_KeyDown(object sender, KeyEventArgs e)
		{
			// Checks if the key pressed was an enter button (character code 13)
			if (e.KeyValue == (char)13)
				CancelButton_Click(this, e);
		}

		private void SelectDevice_KeyUp(object sender, KeyEventArgs e)
		{
			this.OKButton.Focus();
		}

		// Private: SelectDevice_KeyDown
		private void SelectDevice_KeyDown(object sender,KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Enter:
					this.OKButton_Click(this,EventArgs.Empty);
					return;
				case Keys.Up:
					if ( this.AvailableDevicesListBox.SelectedIndex > 0 )
					{
						this.AvailableDevicesListBox.SelectedIndex--;
					}
					return;
				case Keys.Down:
					if ( this.AvailableDevicesListBox.SelectedIndex < this.AvailableDevicesListBox.Items.Count-1 )
					{
						this.AvailableDevicesListBox.SelectedIndex++;
					}
					return;
			}
		}
	}
}
