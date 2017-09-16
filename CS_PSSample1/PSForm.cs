//--------------------------------------------------------------------
// FILENAME: PSForm.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION:
//
// NOTES:
//
// 
//--------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using Symbol.Exceptions;
using System.IO;

namespace CS_PSSample1
{
	/// <summary>
	/// Summary description for PSForm.
	/// </summary>
	public class PSForm : System.Windows.Forms.Form
	{

		private System.Windows.Forms.ColumnHeader NameColumn = null;
        private System.Windows.Forms.ColumnHeader NumberColumn = null;
        private System.Windows.Forms.ColumnHeader ItemColumn = null;
        private System.Windows.Forms.ColumnHeader DataColumn = null;

        private System.Windows.Forms.ListView listViewPS = null;
        private Resources MyResources = null;

		private Symbol.Barcode.Reader MyReader = null;
		private Symbol.Barcode.ReaderData MyReaderData = null;
		private System.EventHandler MyEventHandler = null;

		//Counter for scanned bar codes
		private int scannedItemCount = 0;
        private Timer timer1 = null;
        private int selectedIndex = 0;
		private int timeElapsedAfterUnlock = 0;
        System.EventHandler MyActivateHandler = null;
        //A formatting string used with listview items
        string itemNumberFormat = "";
        string currentListView = "";
		
		const string ROOT = "Main";
		const string ABOUT = "About";
		const string BACK = "Back";
		const string EXITAPP = "ExitApp";
		const string CRADLE_UNLOCK = "CradleUnlock";
		const string CRADLE_UNLOCKED = "CradleUnlocked";
		const string DEVICE_LED = "DeviceLEDOnOff";
		const string CRADLE_LED = "CradleLEDOnOff";
		const string SCAN = "Scan";
		const string SCAN_TITLE = "ScanTitle";
		const string MAIN_TITLE = "MainTitle";
        private System.Windows.Forms.Timer timer2 = null;

		//Time between unlock and lock cradle
		const int MAXIMUM_UNLOCK_TIME = 5000;
		//Time between count down
		const int COUNT_DOWN_INTERVAL = 1000;
		//The port to be opened to unlock the cradle
		const string CRADLE_UNLOCK_PORT = "CDL1:";

        //index of the device LED in notification device list
        const int PS_SAMP_INDEX_DEVICE_LED = 0;
        //index of the cradle LED in notification device list
        const int PS_SAMP_INDEX_CRADLE_LED = 3;
        
		FileStream cradleFileStream = null;
        private Panel panel1;
        private System.Windows.Forms.StatusBar statusBar1 = null;

		public PSForm()
		{

			//save the current cursor
			Cursor savedCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor; 

			MyResources = new Resources();
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.ItemColumn.Text = Resources.GetString("Item");
			this.DataColumn.Text = Resources.GetString("Data");
			this.timer1.Interval = 1000;

			Cursor.Current = savedCursor;
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.listViewPS = new System.Windows.Forms.ListView();
            this.NameColumn = new System.Windows.Forms.ColumnHeader();
            this.NumberColumn = new System.Windows.Forms.ColumnHeader();
            this.ItemColumn = new System.Windows.Forms.ColumnHeader();
            this.DataColumn = new System.Windows.Forms.ColumnHeader();
            this.timer1 = new System.Windows.Forms.Timer();
            this.timer2 = new System.Windows.Forms.Timer();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewPS
            // 
            this.listViewPS.Columns.Add(this.NameColumn);
            this.listViewPS.Columns.Add(this.NumberColumn);
            this.listViewPS.Columns.Add(this.ItemColumn);
            this.listViewPS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPS.FullRowSelect = true;
            this.listViewPS.Location = new System.Drawing.Point(0, 0);
            this.listViewPS.Name = "listViewPS";
            this.listViewPS.Size = new System.Drawing.Size(318, 268);
            this.listViewPS.TabIndex = 1;
            this.listViewPS.View = System.Windows.Forms.View.Details;
            this.listViewPS.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListViewPS_KeyUp);
            this.listViewPS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewPS_KeyDown);
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "";
            this.NameColumn.Width = 0;
            // 
            // NumberColumn
            // 
            this.NumberColumn.Text = "#";
            this.NumberColumn.Width = 30;
            // 
            // ItemColumn
            // 
            this.ItemColumn.Text = "ColumnHeader";
            this.ItemColumn.Width = 208;
            // 
            // DataColumn
            // 
            this.DataColumn.Text = "ColumnHeader";
            this.DataColumn.Width = 208;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 268);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(318, 24);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listViewPS);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 268);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // PSForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(318, 292);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusBar1);
            this.MinimizeBox = false;
            this.Name = "PSForm";
            this.Text = "CS_PSSample1";
            this.Load += new System.EventHandler(this.PSForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.PSForm_Closing);
            this.Resize += new System.EventHandler(this.PSForm_Resize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
            PSForm psForm = new PSForm();
            Application.Run(psForm);

		}

		private void PSForm_Load(object sender, System.EventArgs e)
		{
			MyActivateHandler = new System.EventHandler(this.listViewPS_ItemActivate);
			this.listViewPS.ItemActivate += MyActivateHandler;

			// Add MainMenu if Pocket PC
			if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
			{
				this.Menu = new MainMenu();
			}
			currentListView = ROOT;
			loadMainListViewItems();

			//resize the table column width
			listViewPS.Columns[2].Width =this.Width-32;

			// Ensure that the keyboard focus is set on a control.
			this.listViewPS.Focus();
		}

		#region Main listView
		/// <summary>
		/// Add an item to listViewPS
		/// </summary>
		/// <param name="number">the number to display in Number column</param>
		/// <param name="itemName">The string to displsy in Item column</param>
        private void addListViewItem(int number, string itemName)
		{
			string[] item;
            item = new string[] { itemName, number.ToString(itemNumberFormat), Resources.GetString(itemName) };
			ListViewItem li = new ListViewItem(item);
			listViewPS.Items.Add(li);
		}


		/// <summary>
		/// Add items to the Start page of the Form
		/// </summary>
		private void loadMainListViewItems()
		{
			statusBar1.Text = Resources.GetString(MAIN_TITLE);
            itemNumberFormat = "";

            int i = 0;
            addListViewItem(i++, EXITAPP);
            addListViewItem(i++, CRADLE_UNLOCK);
            addListViewItem(i++, DEVICE_LED);
			addListViewItem(i++, CRADLE_LED);
			addListViewItem(i++, SCAN);
			addListViewItem(i++, ABOUT);

		}

		/// <summary>
		/// Refresh the main list view after unloading it
		/// </summary>
		private void loadMainListView()
		{
            currentListView = ROOT;
			//Add column headers
			this.listViewPS.Columns.Add(this.NameColumn);
			this.listViewPS.Columns.Add(this.NumberColumn);
            this.listViewPS.Columns.Add(this.ItemColumn);
			this.loadMainListViewItems();
		}
		/// <summary>
		/// Unload the start window
		/// </summary>
		private void unloadMainListView()
		{
			//Remove all items
			this.listViewPS.Clear();
			//Remove column headers
			this.listViewPS.Columns.Remove(this.NameColumn);
			this.listViewPS.Columns.Remove(this.NumberColumn);
			this.listViewPS.Columns.Remove(this.ItemColumn);
        }

		#endregion 

		#region Scan listView

		/// <summary>
		/// Add scanned data to the list view
		/// </summary>
		/// <param name="number"></param>
		/// <param name="scanData"></param>
		private void addListViewItemScanData(int number, string scanData)
		{
			string[] item;
			item = new string[] { "Scan Data", number.ToString(itemNumberFormat), scanData };
			ListViewItem li = new ListViewItem(item);
			listViewPS.Items.Add(li);
		}

		/// <summary>
		/// Add items to the Scan page of the Form
		/// </summary>
		private void loadScanListViewItems()
		{
			statusBar1.Text = Resources.GetString(SCAN_TITLE);
            addListViewItem(0, BACK);

			//Initialize and start barcode scanner
			try
			{
			InitReader();
			StartRead();
		}
			catch(Exception)
			{
				System.Windows.Forms.MessageBox.Show(Resources.GetString("ScanInitializeFailed"),"CS_PSSample1");
				listViewPS.Focus();
				reloadLastListView();
			}
		}
		/// <summary>
		/// Refresh the main list view after unloading it
		/// </summary>
		private void loadScanListView()
		{
            currentListView = SCAN;

			itemNumberFormat = "00";

			//Add column headers
			this.listViewPS.Columns.Add(this.NameColumn);
			this.listViewPS.Columns.Add(this.NumberColumn);
			this.listViewPS.Columns.Add(this.DataColumn);
			this.loadScanListViewItems();
		}
		/// <summary>
		/// Unload the start window
		/// </summary>
		private void unloadScanListView()
		{
			//Remove all items
			this.listViewPS.Clear();
			//Remove column headers
			this.listViewPS.Columns.Remove(this.NameColumn);
			this.listViewPS.Columns.Remove(this.NumberColumn);
			this.listViewPS.Columns.Remove(this.DataColumn);

			//Un-initialize and terminate barcode scanner
			StopRead();
			TermReader();
		}

		#endregion

        /// <summary>
		/// This deligate is called when a listview item is double clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewPS_ItemActivate(object sender, System.EventArgs e)
		{
			Cursor savedCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
            try
			{
				//Compare the zeroth element in the selected row in the listwiew
				switch (listViewPS.Items[listViewPS.SelectedIndices[0]].SubItems[0].Text)
				{
					case EXITAPP :
						this.Close();
						break;
					case SCAN :  
						// Profiles list is only in main List View
						unloadMainListView();
						loadScanListView();
						break;
					case ABOUT :  
						// About is only in main List View
                        AboutForm myAbout = new AboutForm();
						Cursor.Current = savedCursor;
                        myAbout.ShowDialog();
						Cursor.Current = Cursors.WaitCursor;
						// Ensure that the keyboard focus is set on a control.
						this.listViewPS.Focus();
                        break;
					case BACK :  
						reloadLastListView();
						break;
					case CRADLE_UNLOCK:
						unlockCradle();
						break;
					case DEVICE_LED:
						toggleDeviceLed();
						break;
					case CRADLE_LED:
						toggleCradleLed();
						break;
					default: 
						break;
				}
   			}
			catch(ArgumentOutOfRangeException)
			{
				System.Windows.Forms.MessageBox.Show(Resources.GetString("ItemNotSelected"), "CS_PSSample1");
				this.listViewPS.Focus();
			}
			Cursor.Current = savedCursor;
		}

		/// <summary>
		/// Backtrace to the previous listView
		/// </summary>
		private void reloadLastListView()
		{
			switch (currentListView) 
			{
				case SCAN :
					unloadScanListView();
					break;
                default:
                    return;
			}

			loadMainListView();
		}

		private void PSForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            if (currentListView != ROOT)
            {
                //Cancell the closing event and reload the previous screen
                e.Cancel = true;
                reloadLastListView();
                return;
            }
            else
            {
                try
                {
                    //switch off any LEDs before exit
                    Symbol.Notification.LED deviceLed = getNotificationLed(PS_SAMP_INDEX_DEVICE_LED);
                    if (deviceLed.State == Symbol.Notification.NotifyState.ON)
                    {
                        deviceLed.State = Symbol.Notification.NotifyState.OFF;
                    }

                    Symbol.Notification.LED cradleLed = getNotificationLed(PS_SAMP_INDEX_CRADLE_LED);
                    if (cradleLed.State == Symbol.Notification.NotifyState.ON)
                    {
                        cradleLed.State = Symbol.Notification.NotifyState.OFF;
                    }
                }
                catch (Exception)
                {
                    //ignore any exceptions at this stage
                }
            }
		}

        private void ListViewPS_KeyUp(object sender, KeyEventArgs e)
        {
            this.listViewPS.Focus();
        }

        /// <summary>
        /// Handle keyboard navigation of the listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewPS_KeyDown(object sender, KeyEventArgs e)
        {
			//process softkeys first of all
			if (currentListView == ROOT)
			{
				switch(e.KeyCode)
				{
					case Keys.Right:
						this.Close();
						return;
					case Keys.Left:
						unloadMainListView();
						loadScanListView();
						return;
				}

			}
			else if(currentListView == SCAN)
			{
				switch(e.KeyCode)
				{
					case Keys.Right:
						reloadLastListView();
						return;
				}
			}
			
			//Process rest of the keys
            char c = System.Convert.ToChar(e.KeyValue);
            int tmpIndex;

            if ((c >= '0') && (c <= '9'))
            {
                //A number is pressed
                if (timer1.Enabled)
                {//This is the second number being pressed as a pair
                    //stop the timer after the second digit
                    timer1.Enabled = false;
                    tmpIndex = selectedIndex * 10 + (int)(c - '0');
                    if (listViewPS.Items.Count > tmpIndex)
                    {
                        selectedIndex = tmpIndex;
                        gotoListItem();
                    }
                    else
                    {
                        selectedIndex = 0;
                    }
                }
                else
                {//The first number being pressed
                    tmpIndex = (int)(c - '0');
                    if (listViewPS.Items.Count > tmpIndex)
                    {
                        selectedIndex = tmpIndex;
						if (listViewPS.Items[0].SubItems[1].Text.Length < 2 )
                        {
                            //list view is one digit so process here
                            gotoListItem();
                            //reset selected index for the next cycle
                            selectedIndex = 0;

                        }
                        else
                        {
                            //list view has more than 10 items so wait for the next cycle or tick
                             //start timer
                             timer1.Enabled = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Stop the timer and reset index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //stop timer after one tic
            timer1.Enabled = false;
            //reset selected index for the next cycle
            selectedIndex = 0;
        }

        /// <summary>
        /// Go to the selected item and expand it if possible
        /// </summary>
        private void gotoListItem()
        {
            if (selectedIndex <= listViewPS.Items.Count)
            {
                //remove all previous selections
                for (int i = 0; i < listViewPS.Items.Count; i++)
                {
                    listViewPS.Items[i].Selected = false;
                }

				//process keyboard input for exit first of all
				if (currentListView == ROOT)
				{
					if (selectedIndex == 0)
					{
						this.Close();
						return;
					}
				}

                //select the desired item
                listViewPS.Items[selectedIndex].Selected = true;
                listViewPS.Invoke(this.MyActivateHandler);       
            }
        }

		/// <summary>
		/// Read complete or failure notification
		/// </summary>
		private void MyReader_ReadNotify(object sender, EventArgs e)
		{
			Symbol.Barcode.ReaderData TheReaderData = this.MyReader.GetNextReaderData();

			itemNumberFormat = "00";

			// If it is a successful read (as opposed to a failed one)
			if ( TheReaderData.Result == Symbol.Results.SUCCESS )
			{
				// Handle the data from this read
				addListViewItemScanData(1+(scannedItemCount++), TheReaderData.Text.Trim());
				// Start the next read
				this.StartRead();
			}
		}

		/// <summary>
		/// Start a read on the reader
		/// </summary>
		private void StartRead()
		{
			// If we have both a reader and a reader data
			if ( ( this.MyReader != null ) &&
				( this.MyReaderData != null ) )
			{
				// Submit a read
				this.MyReader.ReadNotify += this.MyEventHandler;
				this.MyReader.Actions.Read(this.MyReaderData);
			}
		}

		/// <summary>
		/// Stop all reads on the reader
		/// </summary>
		private void StopRead()
		{
			// If we have a reader
			if ( this.MyReader != null )
			{
				// Flush (Cancel all pending reads)
				this.MyReader.ReadNotify -= this.MyEventHandler;
				this.MyReader.Actions.Flush();
			}
		}

		/// <summary>
		/// Initialize the reader.
		/// </summary>
		private bool InitReader()
		{
			// If reader is already present then fail initialize
			if ( this.MyReader != null )
			{
				return false;
			}

			// Create new reader, first available reader will be used.
			this.MyReader = new Symbol.Barcode.Reader();

			// Create reader data
			this.MyReaderData = new Symbol.Barcode.ReaderData(
				Symbol.Barcode.ReaderDataTypes.Text,
				Symbol.Barcode.ReaderDataLengths.MaximumLabel);

			// Create event handler delegate
			this.MyEventHandler = new EventHandler(MyReader_ReadNotify);

			// Enable reader, with wait cursor
			this.MyReader.Actions.Enable();

			scannedItemCount = 0;

			return true;
		}

		/// <summary>
		/// Stop reading and disable/close reader
		/// </summary>
		private void TermReader()
		{
			// If we have a reader
			if ( this.MyReader != null )
			{
				// Disable the reader
				this.MyReader.Actions.Disable();

				// Free it up
				this.MyReader.Dispose();

				// Indicate we no longer have one
				this.MyReader = null;
			}

			// If we have a reader data
			if ( this.MyReaderData != null )
			{
				// Free it up
				this.MyReaderData.Dispose();

				// Indicate we no longer have one
				this.MyReaderData = null;
			}
		}

		private void timer2_Tick(object sender, System.EventArgs e)
		{
			timeElapsedAfterUnlock+=COUNT_DOWN_INTERVAL;
			if(timeElapsedAfterUnlock>=MAXIMUM_UNLOCK_TIME)
			{
				timer2.Enabled = false;
			cradleFileStream.Close();
		}
			updateCountDownDisplay(timeElapsedAfterUnlock);
		}

		/// <summary>
		/// Unlock device from cradle
		/// </summary>
		private void unlockCradle()
		{
			try
			{
				cradleFileStream = new FileStream(CRADLE_UNLOCK_PORT, FileMode.Open);
			}
			catch(Exception)
			{
				System.Windows.Forms.MessageBox.Show(Resources.GetString("UnlockFailed"),"CS_PSSample1");
				listViewPS.Focus();
				return;
			}
			
			//no time is elapsed when starting hence initialized to '0'
			timeElapsedAfterUnlock = 0;
			//update list with unlock status
			updateCountDownDisplay(0);

			timer2.Interval = COUNT_DOWN_INTERVAL;
			timer2.Enabled = true;
		}
		
		/// <summary>
		/// Toggle device LED on/off state
		/// </summary>
		private void toggleDeviceLed()
		{
				try
				{
                Symbol.Notification.LED notificationLed = getNotificationLed(PS_SAMP_INDEX_DEVICE_LED);
                if (notificationLed == null)
				{
                    System.Windows.Forms.MessageBox.Show(Resources.GetString("LedFailed"), "CS_PSSample1");
                    listViewPS.Focus();
                    return;
				}
                if (notificationLed.State == Symbol.Notification.NotifyState.ON)
				{
                    notificationLed.State = Symbol.Notification.NotifyState.OFF;
				}
                else if (notificationLed.State == Symbol.Notification.NotifyState.OFF)
                {
                    notificationLed.State = Symbol.Notification.NotifyState.ON;
                }
                
				}
				catch(Exception)
				{
					System.Windows.Forms.MessageBox.Show(Resources.GetString("LedFailed"),"CS_PSSample1");
					listViewPS.Focus();
				}
							
			
		}

		/// <summary>
		/// Toggle cradle LED on/off state
		/// </summary>
		private void toggleCradleLed()
		{
				try
				{
                Symbol.Notification.LED notificationLed = getNotificationLed(PS_SAMP_INDEX_CRADLE_LED);
                if (notificationLed == null)
				{
                    System.Windows.Forms.MessageBox.Show(Resources.GetString("LedFailed"), "CS_PSSample1");
                    listViewPS.Focus();
                    return;
				}

                if (notificationLed.State == Symbol.Notification.NotifyState.ON)
			    {
                    notificationLed.State = Symbol.Notification.NotifyState.OFF;
			    }
                else if (notificationLed.State == Symbol.Notification.NotifyState.OFF)
				{
                    notificationLed.State = Symbol.Notification.NotifyState.ON;
				}							
			}
				catch(Exception)
				{
					System.Windows.Forms.MessageBox.Show(Resources.GetString("LedFailed"),"CS_PSSample1");
					listViewPS.Focus();
				}
			
		}
		
		/// <summary>
		/// Update the count down timer after unlock
		/// </summary>
		/// <param name="timeElasped"></param>
		private void updateCountDownDisplay(int timeElasped)
		{
			String itemData = "";
			if(currentListView==ROOT)
			{
				if(timeElasped<MAXIMUM_UNLOCK_TIME)
				{
					itemData = Resources.GetString(CRADLE_UNLOCKED)+ " " + ((MAXIMUM_UNLOCK_TIME - timeElasped)/1000).ToString();
				}
				else
				{
					itemData = Resources.GetString(CRADLE_UNLOCK);

				}
				listViewPS.Items[1].SubItems[2].Text = itemData;
			}

		}

        /// <summary>
        /// This function returns the notification LED specified by notoficationDeviceIndex
        /// </summary>
        /// <param name="notoficationDeviceIndex"></param>
        /// <returns></returns>
        Symbol.Notification.LED getNotificationLed(int notoficationDeviceIndex)
        {
            Symbol.Notification.Device[] notificationDevices = Symbol.Notification.Device.AvailableDevices;

            //get references for required notification devices
            return new Symbol.Notification.LED(notificationDevices[notoficationDeviceIndex]);

        }

        private void PSForm_Resize(object sender, EventArgs e)
        {
            // If it is CE
            if (Symbol.Win32.PlatformType.IndexOf("PocketPC", 0) <= 0)
            {
                this.Width = (Screen.PrimaryScreen.WorkingArea.Width > 320 ? 320 : Screen.PrimaryScreen.WorkingArea.Width);
                this.Height = (Screen.PrimaryScreen.WorkingArea.Height > 320 ? 320 : Screen.PrimaryScreen.WorkingArea.Height);
            }
        }

        private void SetListViewColumnWidth()
        {
            listViewPS.Width = this.Width;

            // Main list
            this.NumberColumn.Width = (13 * listViewPS.Width) / 100;
            this.ItemColumn.Width = (50 * listViewPS.Width) / 100;

            // Scan list
            this.DataColumn.Width = (70 * listViewPS.Width) / 100;

        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            SetListViewColumnWidth();
        }

	}
}
