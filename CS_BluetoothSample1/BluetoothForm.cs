using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Symbol.WPAN.Bluetooth;

namespace CS_BluetoothSample1
{
    public partial class BluetoothForm : Form
    {

        private System.Windows.Forms.ColumnHeader NameColumn = null;
        private System.Windows.Forms.ColumnHeader NumberColumn = null;
        private System.Windows.Forms.ColumnHeader ItemColumn = null;
        private System.Windows.Forms.ColumnHeader ValueColumn = null;
        private System.Windows.Forms.ColumnHeader ComponentColumn = null;
        private System.Windows.Forms.ColumnHeader VersionColumn = null;
        private System.Windows.Forms.ColumnHeader DeviceColumn = null;
        private System.Windows.Forms.ColumnHeader ClassOfDeviceColumn = null;

        private System.Windows.Forms.ListView listViewBluetooth = null;
		private Resources MyResources = null;

		//The currently selected device
		private int selectedDeviceIndex = -1;

        private Timer timer1;
        private int selectedIndex = 0;
        System.EventHandler MyActivateHandler = null;
        //A formatting string used with listview items
        string itemNumberFormat = "";
        string currentListView = "";

        //the bluetooth reference
        Bluetooth m_Bluetooth = null;

        String m_ConnectedDeviceName = "";

		const string ROOT = "Main";
		const string ABOUT = "About";
		const string VERSION = "Version";
		const string BACK = "Back";
		const string EXITAPP = "ExitApp";
        const string DISCOVER = "Discover";
        private Panel panel1;
        const string DEVICES = "Devices";
        const string CANCEL = "Cancel";
        const string START = "Start";
        private StatusBar statusBar1;
        private bool bIsRefreshInProgress= false;

        public delegate void UpdateUIDelegate(RemoteDevice rd, RefreshStatus refreshStatus, BluetoothResults result);
        public UpdateUIDelegate UpdateUIHandler;

        public RemoteDevices.RefreshNotifyEventHandler RefreshNotifyHandler = null;

        // Used to adjust the row height of listViewBluetooth.
        // (This is a workaround in the absense of an exposed API to control the 
        // row height of ListView control)
        private System.Windows.Forms.ImageList imageList1;

        // The factor(n) which defines the row height of the ListView. 
        // The default row height would be multiplied by this factor (ROW_HEIGHT_FACTOR = n).
        // Currently set to 2. So the row height would be doubled in this sample.
        // The user can set it to any value (i.e. 1,2,3,..., etc.) based on the requirements.
        private const int ROW_HEIGHT_FACTOR = 2;

        public BluetoothForm()
		{
            
            Cursor savedCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            MyResources = new Resources();
            //
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this.ItemColumn.Text = Resources.GetString("Item");
            this.ValueColumn.Text = Resources.GetString("Value");
            this.ComponentColumn.Text = Resources.GetString("Component");
            this.VersionColumn.Text = Resources.GetString("VersionHeader");
            this.DeviceColumn.Text = Resources.GetString("DeviceName");
            this.ClassOfDeviceColumn.Text = Resources.GetString("ClassOfDevice");

            //set time interval for number input as 1 second
            this.timer1.Interval = 1000;

            //create bluetooth object
            m_Bluetooth = new Bluetooth();
            m_Bluetooth.Enable();
            RefreshNotifyHandler = new RemoteDevices.RefreshNotifyEventHandler(RemoteDevices_RefreshNotify);
            m_Bluetooth.RemoteDevices.RefreshNotify += RefreshNotifyHandler;
            UpdateUIHandler = new UpdateUIDelegate(UpdateUI);
            statusBar1.Visible = false;

            // Used to adjust the row height of listViewBluetooth.
            // (This is a workaround in the absense of an exposed API to control the 
            // row height of ListView control)
            imageList1 = new ImageList();

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
            this.listViewBluetooth = new System.Windows.Forms.ListView();
            this.NameColumn = new System.Windows.Forms.ColumnHeader();
            this.NumberColumn = new System.Windows.Forms.ColumnHeader();
            this.ItemColumn = new System.Windows.Forms.ColumnHeader();
            this.ValueColumn = new System.Windows.Forms.ColumnHeader();
            this.ComponentColumn = new System.Windows.Forms.ColumnHeader();
            this.VersionColumn = new System.Windows.Forms.ColumnHeader();
            this.DeviceColumn = new System.Windows.Forms.ColumnHeader();
            this.ClassOfDeviceColumn = new System.Windows.Forms.ColumnHeader();
            this.timer1 = new System.Windows.Forms.Timer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewBluetooth
            // 
            this.listViewBluetooth.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewBluetooth.Columns.Add(this.NameColumn);
            this.listViewBluetooth.Columns.Add(this.NumberColumn);
            this.listViewBluetooth.Columns.Add(this.ItemColumn);
            this.listViewBluetooth.Columns.Add(this.ValueColumn);
            this.listViewBluetooth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewBluetooth.FullRowSelect = true;
            this.listViewBluetooth.Location = new System.Drawing.Point(0, 0);
            this.listViewBluetooth.Name = "listViewBluetooth";
            this.listViewBluetooth.Size = new System.Drawing.Size(240, 272);
            this.listViewBluetooth.TabIndex = 0;
            this.listViewBluetooth.View = System.Windows.Forms.View.Details;
            this.listViewBluetooth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListViewBluetooth_KeyUp);
            this.listViewBluetooth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewBluetooth_KeyDown);
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
            this.ItemColumn.Width = 80;
            // 
            // ValueColumn
            // 
            this.ValueColumn.Text = "ColumnHeader";
            this.ValueColumn.Width = 125;
            // 
            // ComponentColumn
            // 
            this.ComponentColumn.Text = "ColumnHeader";
            this.ComponentColumn.Width = 125;
            // 
            // VersionColumn
            // 
            this.VersionColumn.Text = "ColumnHeader";
            this.VersionColumn.Width = 80;
            // 
            // DeviceColumn
            // 
            this.DeviceColumn.Text = "ColumnHeader";
            // 
            // ClassOfDeviceColumn
            // 
            this.ClassOfDeviceColumn.Text = "ColumnHeader";
            this.ClassOfDeviceColumn.Width = 80;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listViewBluetooth);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 272);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 248);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(240, 24);
            // 
            // BluetoothForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(240, 272);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "BluetoothForm";
            this.Text = "Bluetooth Sample";
            this.Load += new System.EventHandler(this.BluetoothForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.BluetoothForm_Closing);
            this.Resize += new System.EventHandler(this.BluetoothForm_Resize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
            BluetoothForm bluetoothForm = new BluetoothForm();
            Application.Run(bluetoothForm);

		}

		private void BluetoothForm_Load(object sender, System.EventArgs e)
		{

            MyActivateHandler = new System.EventHandler(this.listViewBluetooth_ItemActivate);
            this.listViewBluetooth.ItemActivate += MyActivateHandler;

            currentListView = ROOT;
            loadMainListViewItems();

            setGridLines(this.listViewBluetooth);
            setRowHeight(this.listViewBluetooth);

            // Ensure that the keyboard focus is set on a control.
            this.listViewBluetooth.Focus();

            // Add MainMenu if Pocket PC
            if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
            {
                this.Menu = new MainMenu();
            }
        }

		#region Main listView
		/// <summary>
		/// Add an item to listViewBluetooth
		/// </summary>
		/// <param name="number">the number to display in Number column</param>
		/// <param name="itemName">The string to displsy in Item column</param>
        private void addListViewItem(int number, string itemName)
		{
			string[] item;
            item = new string[] { itemName, number.ToString(itemNumberFormat), Resources.GetString(itemName) };
			ListViewItem li = new ListViewItem(item);
			listViewBluetooth.Items.Add(li);
		}
        /// <summary>
        /// Add an item to listViewBluetooth
        /// </summary>
        /// <param name="number">The number to display in Number column</param>
        /// <param name="itemName">The string to display in Item column</param>
        /// <param name="itemValue">The string to display in Value column</param>
        private void addListViewItem(int number, string itemName, string itemValue)
        {
            string[] item;
            item = new string[] { itemName, number.ToString(itemNumberFormat), Resources.GetString(itemName), itemValue };
            ListViewItem li = new ListViewItem(item);
            listViewBluetooth.Items.Add(li);
        }

        /// <summary>
        /// Add item to listViewBluetooth with extentions to item name
        /// </summary>
        /// <param name="number">The number to display in Number column</param>
        /// <param name="itemName">The string to display in Item column</param>
        /// <param name="itemNameExtentions">The string to display as Item name extension</param>
        /// <param name="itemValue">The string to display in Value column</param>
        private void addListViewItem(int number, string itemName, string itemNameExtentions, string itemValue)
        {
            string[] item;
            item = new string[] { itemName, number.ToString(itemNumberFormat), Resources.GetString(itemName) + itemNameExtentions, itemValue };
            ListViewItem li = new ListViewItem(item);
            listViewBluetooth.Items.Add(li);
        }

		/// <summary>
		/// Add items to the Start page of the Form
		/// </summary>
		private void loadMainListViewItems()
		{
            //Get the first adapter
            itemNumberFormat = "";

            int i = 0;
            addListViewItem(i++, EXITAPP, "");

            addListViewItem(i++, DISCOVER, m_ConnectedDeviceName);
            addListViewItem(i++, VERSION, "");
			addListViewItem(i++, ABOUT, "");


		}

		/// <summary>
		/// Refresh the main list view after unloading it
		/// </summary>
		private void loadMainListView()
		{
            currentListView = ROOT;
			//Add column headers
			this.listViewBluetooth.Columns.Add(this.NameColumn);
			this.listViewBluetooth.Columns.Add(this.NumberColumn);
            this.listViewBluetooth.Columns.Add(this.ItemColumn);
            this.listViewBluetooth.Columns.Add(this.ValueColumn);
			this.loadMainListViewItems();
		}
		/// <summary>
		/// Unload the start window
		/// </summary>
		private void unloadMainListView()
		{
			this.listViewBluetooth.Clear();
			//Remove column headers
			this.listViewBluetooth.Columns.Remove(this.NameColumn);
			this.listViewBluetooth.Columns.Remove(this.NumberColumn);
			this.listViewBluetooth.Columns.Remove(this.ItemColumn);
            this.listViewBluetooth.Columns.Remove(this.ValueColumn);
        }

 

		#endregion 

		#region Version listView
		/// <summary>
		/// Add items to Version listView
		/// </summary>
		private void loadVersionListViewItems()
		{
			int i = 0;
            //Format as a two digit number
            itemNumberFormat = "";
            addListViewItem(i++, BACK, "");
            if (m_Bluetooth.LoadedStack == BTH_STACK.SS_STACK)
            {
                addListViewItem(i++, "BTExplorerVersion", m_Bluetooth.Version.BTExplorerVersion);
            }
            else
            {
                addListViewItem(i++, "HCIVersion", m_Bluetooth.Version.HCI_Version);
                addListViewItem(i++, "HCIRevision", m_Bluetooth.Version.HCI_Revision);
                addListViewItem(i++, "LMPVersion", m_Bluetooth.Version.LMP_Version);
                addListViewItem(i++, "LMPSubVersion", m_Bluetooth.Version.LMP_SubVersion);
            }
		}

		/// <summary>
		/// load Version ListView
		/// </summary>
		private void loadVersionListView()
		{
            currentListView = VERSION;
			//Add column headers
			this.listViewBluetooth.Columns.Add(this.NameColumn);
			this.listViewBluetooth.Columns.Add(this.NumberColumn);
			this.listViewBluetooth.Columns.Add(this.ComponentColumn);
			this.listViewBluetooth.Columns.Add(this.VersionColumn);
			this.loadVersionListViewItems();
		}
		/// <summary>
		/// Unload Version listView
		/// </summary>
		private void unloadVersionListView()
		{
			//Remove all items
			this.listViewBluetooth.Clear();
			//Remove column headers
			this.listViewBluetooth.Columns.Remove(this.NameColumn);
			this.listViewBluetooth.Columns.Remove(this.NumberColumn);
			this.listViewBluetooth.Columns.Remove(this.ComponentColumn);
			this.listViewBluetooth.Columns.Remove(this.VersionColumn);
		}

		#endregion

		//Load unload a list of all Discovered devices
		#region Devices listView

		/// <summary>
		/// Add an item to listViewBluetooth
		/// </summary>
		/// <param name="number">the number to display in Number column</param>
		/// <param name="deviceName">The device Name to display in Item column</param>
		private void addDevicesListViewItem(int number, string deviceName, string deviceClassName)
		{
			string[] item;
			item = new string[] {Resources.GetString(DEVICES) , number.ToString(itemNumberFormat) , deviceName + "...", deviceClassName};
			ListViewItem li = new ListViewItem(item);
			listViewBluetooth.Items.Add(li);
		}

        private string SelectDeviceClassName(ClassOfDevice classOfDevice)
        {
            string DeviceClassName = string.Empty;
            switch (classOfDevice.MajorClass)
            {
                case MajorClassofDevice.COMPUTER:
                    if (classOfDevice.MinorClass == MinorClassOfDevice.COMPUTER_PDA ||
                        classOfDevice.MinorClass == MinorClassOfDevice.COMPUTER_PDACLAMSHELL)
                    {
                        DeviceClassName = Resources.GetString("ClassOfDevice_PDA");
                    }
                    else
                    {
                        DeviceClassName = Resources.GetString("ClassOfDevice_Computer");
                    }
                    break;
                case MajorClassofDevice.IMAGING:
                    if (classOfDevice.MinorClass == MinorClassOfDevice.IMAGING_PRINTER)
                    {
                        DeviceClassName = Resources.GetString("ClassOfDevice_Printer");
                    }
                    else
                    {
                        DeviceClassName = Resources.GetString("ClassOfDevice_Other");
                    }
                    break;
                case MajorClassofDevice.PHONE:
                    DeviceClassName = Resources.GetString("ClassOfDevice_Phone");
                    break;
                default:
                    DeviceClassName = Resources.GetString("ClassOfDevice_Other");
                    break;
            }

            return DeviceClassName;
        }

        /// <summary>
        /// Load Devices list view
        /// </summary>
        private void loadDevicesListView()
        {
            currentListView = DEVICES;

            //Add column headers
            this.listViewBluetooth.Columns.Add(this.NameColumn);
            this.listViewBluetooth.Columns.Add(this.NumberColumn);
            this.listViewBluetooth.Columns.Add(this.DeviceColumn);
            this.listViewBluetooth.Columns.Add(this.ClassOfDeviceColumn);

            itemNumberFormat = "00";
            addListViewItem(0, BACK);
            addListViewItem(1, CANCEL);
            
            statusBar1.Visible = true;
        }

        private void unloadDevicesListView()
        {
            //Remove all items
            this.listViewBluetooth.Clear();
            //Remove column headers
            this.listViewBluetooth.Columns.Remove(this.NameColumn);
            this.listViewBluetooth.Columns.Remove(this.NumberColumn);
            this.listViewBluetooth.Columns.Remove(this.DeviceColumn);
            this.listViewBluetooth.Columns.Remove(this.ClassOfDeviceColumn);
            statusBar1.Visible = false;
            statusBar1.Text = string.Empty;
        }

		#endregion

        /// <summary>
		/// This deligate is called when a listview item is double clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewBluetooth_ItemActivate(object sender, System.EventArgs e)
		{
            Cursor savedCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            // Used to reset the "[Cancel]" command in the listview to "[Start]" command.
            string[] item = new string[] { START, "01", Resources.GetString(START) };

            try
			{
				//Compare the zeroth element in the selected row in the listwiew
				switch (listViewBluetooth.Items[listViewBluetooth.SelectedIndices[0]].SubItems[0].Text)
				{
					case EXITAPP :
                        if (selectedDeviceIndex >= 0)
                        {
                            m_Bluetooth.RemoteDevices[selectedDeviceIndex].UnPair();
                        }
                        m_Bluetooth.Disable();
						this.Close();
						break;
					case VERSION : 
						// Version is only in main List View
						unloadMainListView();
						loadVersionListView();
						break;
					case ABOUT :  
						// About is only in main List View
                        AboutForm myAbout = new AboutForm();
                        Cursor.Current = savedCursor;
                        myAbout.ShowDialog();
                        Cursor.Current = Cursors.WaitCursor;
                        //Ensure that the keyboard focus is set on a control.
                        this.listViewBluetooth.Focus();
                        break;
					case BACK :
                        if (currentListView == DEVICES && bIsRefreshInProgress == true)
                        {
                            m_Bluetooth.RemoteDevices.RefreshCancel();
                            statusBar1.Text = Resources.GetString("DiscoveryCancelInProgress");
                        }
                        else
                        {
                            reloadLastListView();
                        }
						break;
					case DEVICES:
                        // When a device is selected, current refresh is cancelled.
                        // After the cancelled event is received, pairing is done with the selected device.
						selectedDeviceIndex = Int32.Parse(listViewBluetooth.Items[listViewBluetooth.SelectedIndices[0]].SubItems[1].Text) - 2;
                        if (bIsRefreshInProgress == true)
                        {
                            m_Bluetooth.RemoteDevices.RefreshCancel();
                            statusBar1.Text = Resources.GetString("DiscoveryCancelInProgress");
                        }
                        else
                        {
                            PairRemoteDevice();
                        }
                        break;
                    case DISCOVER:
                        if (selectedDeviceIndex >= 0)
                        {
                            m_Bluetooth.RemoteDevices[selectedDeviceIndex].UnPair();
                            m_ConnectedDeviceName = "";
                            selectedDeviceIndex = -1;
                        }
                        unloadMainListView();
                        loadDevicesListView();

                        bIsRefreshInProgress = true;
                        m_Bluetooth.RemoteDevices.Refresh();

                        break;
                    case CANCEL:
                        m_Bluetooth.RemoteDevices.RefreshCancel();
                        statusBar1.Text = Resources.GetString("DiscoveryCancelInProgress");
                        break;
                    case START:
                        listViewBluetooth.Items.Clear();
                        addListViewItem(0, BACK);
                        addListViewItem(1, CANCEL);
                        bIsRefreshInProgress = true;
                        m_Bluetooth.RemoteDevices.Refresh();
                        break;
					default: 
						break;
				}
   			}
			catch(ArgumentOutOfRangeException)
			{
				System.Windows.Forms.MessageBox.Show("Item not selected", "CS_BluetoothSample1");
                this.listViewBluetooth.Focus();
			}
            Cursor.Current = savedCursor;
		}

        /// <summary>
        /// Event handler for discovery notification 
        /// </summary>
        void RemoteDevices_RefreshNotify(object sender, RefreshNotifyEventArgs eRefreshNotifyEventArg)
        {
            this.Invoke(
                    this.UpdateUIHandler,
                    new object[] {
                        eRefreshNotifyEventArg.RemoteDevice,
                        eRefreshNotifyEventArg.Status,
                        eRefreshNotifyEventArg.Result}
                        );


        }

        /// <summary>
        /// Updates the UI with the discovered device information and the discvoery status.
        /// </summary>
        void UpdateUI(RemoteDevice rd, RefreshStatus refreshStatus, BluetoothResults result)
        {
            string[] item = new string[] { START, "01", Resources.GetString(START) };
            switch (refreshStatus)
            {
                case RefreshStatus.STARTED:
                    statusBar1.Text = Resources.GetString("DiscoveryStarted");
                    break;
                case RefreshStatus.INPROGRESS:
                    statusBar1.Text = Resources.GetString("DiscoveryInProgress");
                    addDevicesListViewItem(listViewBluetooth.Items.Count, rd.Name, SelectDeviceClassName(rd.ClassOfDevice));
                    listViewBluetooth.Items[listViewBluetooth.Items.Count - 1].Selected = true;
                    break;
                case RefreshStatus.CANCELLED:
                    if (currentListView == DEVICES)
                    {
                        // If the refresh is cancelled, by tapping "BACK" we reload the last view.
                        if (listViewBluetooth.Items[listViewBluetooth.SelectedIndices[0]].SubItems[0].Text == BACK)
                        {
                            reloadLastListView();
                            return;
                        }

                        statusBar1.Text = Resources.GetString("DiscoveryCancelled");
                        bIsRefreshInProgress = false;

                        // When a device is selected, current refresh is cancelled.
                        // After the cancelled event is received, pairing is done with the selected device.
                        if (selectedDeviceIndex >= 0)
                        {
                            PairRemoteDevice();
                        }
                        else
                        {
                            // Reset the "[Cancel]" command in the listview to "[Start]" command
                            listViewBluetooth.Items.RemoveAt(1);
                            listViewBluetooth.Items.Insert(1, new ListViewItem(item));
                        }
                    }
                    break;
                case RefreshStatus.COMPLETED:
                    statusBar1.Text = Resources.GetString("DiscoveryCompleted");
                    // Reset the "[Cancel]" command in the listview to "[Start]" command.
                    listViewBluetooth.Items.RemoveAt(1);
                    listViewBluetooth.Items.Insert(1, new ListViewItem(item));
                    bIsRefreshInProgress = false;
                    break;
                case RefreshStatus.ERROR:
                    statusBar1.Text = Resources.GetString("DiscoveryFailed");
                    MessageBox.Show(Resources.GetString("DiscoveryFailed") + " Error : " + result.ToString());
                    // Reset the "[Cancel]" command in the listview to "[Start]" command.
                    listViewBluetooth.Items.RemoveAt(1);
                    listViewBluetooth.Items.Insert(1, new ListViewItem(item));
                    break;
            }
        }

        void PairRemoteDevice()
        {
            //Pair with the selected device
            m_Bluetooth.RemoteDevices[selectedDeviceIndex].LocalComPort = m_Bluetooth.LocalComPorts[0];
            UserInputForm userInpBox = new UserInputForm();
            userInpBox.DoScale();
            string PINString = string.Empty;
            userInpBox.GetUserInput("Authentication PIN", "Enter PIN:", string.Empty, ref PINString);
            try
            {
                m_Bluetooth.RemoteDevices[selectedDeviceIndex].Pair(PINString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.Message);
                string[] item = new string[] { START, "01", Resources.GetString(START) };
                listViewBluetooth.Items.RemoveAt(1);
                listViewBluetooth.Items.Insert(1, new ListViewItem(item));
                selectedDeviceIndex = -1;
                return;
            }

            m_ConnectedDeviceName = "Paired: " + m_Bluetooth.RemoteDevices[selectedDeviceIndex].Name;
            reloadLastListView();
        }


		/// <summary>
		/// Backtrace to the previous listView
		/// </summary>
		private void reloadLastListView()
		{
			switch (currentListView) 
			{
				case VERSION :
					unloadVersionListView();
					break;
                case DEVICES:
                    unloadDevicesListView();
                    break;
                default:
                    return;
			}

			loadMainListView();
		}

		private void BluetoothForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            if (currentListView != ROOT)
            {
                //Cancel the closing event and reload the previous screen
                e.Cancel = true;

                //If an asynchronous refresh is in progress, cancel it and reload last view.
                if (currentListView == DEVICES)
                {
                    m_Bluetooth.RemoteDevices.RefreshCancel();
                }
                reloadLastListView();
                return;
            }

            m_Bluetooth.RemoteDevices.RefreshNotify -= RefreshNotifyHandler;
            m_Bluetooth.Dispose();
		}

        private void ListViewBluetooth_KeyUp(object sender, KeyEventArgs e)
        {
            this.listViewBluetooth.Focus();
        }

        /// <summary>
        /// Handle keyboard navigation of the listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewBluetooth_KeyDown(object sender, KeyEventArgs e)
        {
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
                    if (listViewBluetooth.Items.Count > tmpIndex)
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
                    if (listViewBluetooth.Items.Count > tmpIndex)
                    {
                        selectedIndex = tmpIndex;
                        if (listViewBluetooth.Items.Count < 10)
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
            if (selectedIndex <= listViewBluetooth.Items.Count)
            {
                //remove all previous selections
                for (int i = 0; i < listViewBluetooth.Items.Count; i++)
                {
                    listViewBluetooth.Items[i].Selected = false;
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
                listViewBluetooth.Items[selectedIndex].Selected = true;
                listViewBluetooth.Invoke(this.MyActivateHandler);       
            }
        }


        private void BluetoothForm_Resize(object sender, EventArgs e)
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
            listViewBluetooth.Width = this.Width;

            // Main list
            this.NumberColumn.Width = (13 * listViewBluetooth.Width) / 100;
            this.ItemColumn.Width = (36 * listViewBluetooth.Width) / 100;
            this.ValueColumn.Width = (50 * listViewBluetooth.Width) / 100;

            // Devices list
            this.NumberColumn.Width = (13 * listViewBluetooth.Width) / 100;
            this.DeviceColumn.Width = (48 * listViewBluetooth.Width) / 100;
            this.ClassOfDeviceColumn.Width = (33 * listViewBluetooth.Width) / 100;

            // Version list
            this.NumberColumn.Width = (13 * listViewBluetooth.Width) / 100;
            this.ComponentColumn.Width = (43 * listViewBluetooth.Width) / 100;
            this.VersionColumn.Width = (43 * listViewBluetooth.Width) / 100;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            SetListViewColumnWidth();
        }

        private const int LVM_GETITEMPOSITION = (0x1010);
        private const int LVM_GETEXTENDEDLISTVIEWSTYLE = 0x1037;
        private const int LVM_SETEXTENDEDLISTVIEWSTYLE = 0x1036;
        private const int LVS_EX_GRIDLINES = 0x1;

        public struct Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [DllImport("coredll.dll")]
        private static extern int SendMessageW(int hWnd, uint wMsg, int wParam, ref Point lParam);

        [DllImport("coredll.dll")]
        private static extern int SendMessageW(int hWnd, int wMsg, int wParam, int lParam);

        public void setRowHeight(System.Windows.Forms.ListView lvw)
        {
            Point positionItem1 = new Point(0, 0);
            Point positionItem2 = new Point(0, 0);

            SendMessageW((int)(lvw.Handle), LVM_GETITEMPOSITION, 0, ref positionItem1);

            SendMessageW((int)(lvw.Handle), LVM_GETITEMPOSITION, 1, ref positionItem2);

            int rowHeight = positionItem2.y - positionItem1.y;

            // Adjust the row height of listViewMain by multiplying the current factor by ROW_HEIGHT_FACTOR.
            //  The usage of this imageList is kind of a workaround in the absense of an exposed API to control the 
            //  row height of System.Windows.Forms.ListView.
            this.imageList1.ImageSize = new Size(1, (int)(rowHeight * ROW_HEIGHT_FACTOR));
            lvw.SmallImageList = this.imageList1;
        }

        public void setGridLines(System.Windows.Forms.ListView lvw)
        {
            lvw.Focus();
            int extendedStyle = SendMessageW((int)(lvw.Handle), LVM_GETEXTENDEDLISTVIEWSTYLE, 0, 0);
            extendedStyle |= LVS_EX_GRIDLINES;
            SendMessageW((int)(lvw.Handle), LVM_SETEXTENDEDLISTVIEWSTYLE, 0, extendedStyle);
        }
    }
}