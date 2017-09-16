//--------------------------------------------------------------------
// FILENAME: FusionForm.cs
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
using Symbol.Fusion;
using Symbol.Fusion.WLAN;
using Symbol.Exceptions;
using System.Runtime.InteropServices;

namespace FusionSample1
{
    /// <summary>
    /// Summary description for FusionForm.
    /// </summary>
    public class FusionForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.ColumnHeader NameColumn = null;
        private System.Windows.Forms.ColumnHeader NumberColumn = null;
        private System.Windows.Forms.ColumnHeader ItemColumn = null;
        private System.Windows.Forms.ColumnHeader ValueColumn = null;
        private System.Windows.Forms.ColumnHeader ProfileIDColumn = null;
        private System.Windows.Forms.ColumnHeader ComponentColumn = null;
        private System.Windows.Forms.ColumnHeader VersionColumn = null;

        // The ImageList reference which is used to control the row height of listViewMain.
        //  This is kind of a workaround in the absense of an exposed API to control the 
        //  row height of System.Windows.Forms.ListView.
        private ImageList imageList = null;

        private System.Windows.Forms.ListView listViewFusion = null;
        private Resources MyResources = null;

        //Config object for Version and Diagnostics
        private Config myConfig = null;
        //WLAN object for WLAN operations
        private WLAN myWlan = null;

        //A temporary Adhoc profile that is created by this sample for demonstration
        private Profile myAdhocProfile = null;
        //A temporary profile that is created by this sample for demonstration
        private Profile myInfrastructureProfile = null;

        //The flag to track whether the profile Sample-M-Adhoc is created by this sample or not. Initialized to false.
        private bool bAdhocCreated = false;
        //The flag to track whether the profile Sample-M-Infrastructure is created by this sample or not. Initialized to false.
        private bool bInfraCreated = false;

        //The currently selected profile
        private int selectedProfile = 0;
        //Store the index where SignalQuality appears in list view
        private int signalQualityLocation = 0;
        //Store the index where adapter status appears in list view
        private int adapterStatusLocation = 0;
        //The last received adapter power state.
        private string adapterPowerState = "";
        //the event handler for SignalQuality chages
        Adapter.SignalQualityHandler mySignalQualityHandler = null;
        //the event handler for status changes
        WLAN.StatusChangeHandler myStatusChangedHandler = null;
        private Timer timer1;
        private int selectedIndex = 0;
        System.EventHandler MyActivateHandler = null;
        //A formatting string used with listview items
        string itemNumberFormat = "";
        string currentListView = "";

        //Store the ESSID location for updating the content
        private int essidLocation = 0;

        //store profile name location for updating the content
        private int profileNameLocation = 0;

        const string ROOT = "Main";
        const string ABOUT = "About";
        const string VERSION = "Version";
        const string BACK = "Back";
        const string EXITAPP = "ExitApp";
        const string PROFILE = "Profile";
        const string PROFILES = "Profiles";
        private Panel panel1;
        const string ADAPTER_STATUS = "AdapterStatus";

        // The factor(n) which defines the row height of the ListView. 
        //  The default row height would be multiplied by this factor (ROW_HEIGHT_FACTOR = n).
        private const int ROW_HEIGHT_FACTOR = 2; // Currently set to 2. So the row height would be doubled in this sample.
        // The user can set it to any value (i.e. 1,2,3,..., etc.) based on the requirements.

        public FusionForm()
        {

            //enable status monitoring
            WLAN.Monitor.AdapterPower = true;
            WLAN.Monitor.AccessPoint = true;

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
            this.ProfileIDColumn.Text = Resources.GetString("ActiveProfile"); ;
            //set time interval for number input as 1 second
            this.timer1.Interval = 1000;

            try
            {
                //create a reference to Config
                myConfig = new Config(FusionAccessType.STATISTICS_MODE);
                //create a reference to WLAN
                myWlan = new WLAN(FusionAccessType.STATISTICS_MODE);
            }
            catch (OperationFailureException)
            {
                Symbol.ResourceCoordination.TerminalInfo terminalInfo = new Symbol.ResourceCoordination.TerminalInfo();

                if (((Symbol.ResourceCoordination.WLANTypes)(terminalInfo.ConfigData.WLAN)) == Symbol.ResourceCoordination.WLANTypes.NONE)
                {
                    MessageBox.Show("The configuration of the device doesn't support Fusion. The application will now exit.");
                }
                else
                {
                    MessageBox.Show("Unable to open Fusion. The application will now exit.");
                }

                this.Close();
                return;
            }

            //The variable to hold the management state. 
            //Initialize it to Fusion.
            Symbol.Fusion.WLAN.WLAN_MANAGEMENT_STATE managementState = WLAN_MANAGEMENT_STATE.WLAN_MANAGEMENT_FUSION_STATE;

            try
            {
                //Get the current state form the device.
                managementState = myWlan.Adapters[0].WLANManagement;
            }
            catch
            {
                //In case of any failure, just continue.
            }

            //Only if the current management state is WZC, skip performing any profile - related operations.
            if (!((managementState == WLAN_MANAGEMENT_STATE.WLAN_MANAGEMENT_WZC_STATE) || (managementState == WLAN_MANAGEMENT_STATE.WLAN_MANAGEMENT_WZC_STATE_REBOOT_IN_FUSION_STATE)))
            {
                createSampleProfiles();

                try
                {
                    //refresh profile list so that newly created profiles will be visible
                    myWlan.Profiles.Refresh();
                }
                catch (OperationFailureException)
                {
                    // This is just to allow the application to continue even if the profile enumeration fails fails with OperationFailureException.
                }
            }

            Cursor.Current = savedCursor;

        }
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewFusion = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList();
            this.NameColumn = new System.Windows.Forms.ColumnHeader();
            this.NumberColumn = new System.Windows.Forms.ColumnHeader();
            this.ItemColumn = new System.Windows.Forms.ColumnHeader();
            this.ValueColumn = new System.Windows.Forms.ColumnHeader();
            this.ProfileIDColumn = new System.Windows.Forms.ColumnHeader();
            this.ComponentColumn = new System.Windows.Forms.ColumnHeader();
            this.VersionColumn = new System.Windows.Forms.ColumnHeader();
            this.timer1 = new System.Windows.Forms.Timer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewFusion
            // 
            this.listViewFusion.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewFusion.Columns.Add(this.NameColumn);
            this.listViewFusion.Columns.Add(this.NumberColumn);
            this.listViewFusion.Columns.Add(this.ItemColumn);
            this.listViewFusion.Columns.Add(this.ValueColumn);
            this.listViewFusion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFusion.FullRowSelect = true;
            this.listViewFusion.Location = new System.Drawing.Point(0, 0);
            this.listViewFusion.Name = "listViewFusion";
            this.listViewFusion.Size = new System.Drawing.Size(240, 272);
            this.listViewFusion.TabIndex = 0;
            this.listViewFusion.View = System.Windows.Forms.View.Details;
            this.listViewFusion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListViewFusion_KeyUp);
            this.listViewFusion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewFusion_KeyDown);
            // 
            // imageList
            // 
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
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
            // ProfileIDColumn
            // 
            this.ProfileIDColumn.Text = "ColumnHeader";
            this.ProfileIDColumn.Width = 205;
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
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listViewFusion);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 272);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // FusionForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(240, 272);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "FusionForm";
            this.Text = "Fusion Sample";
            this.Load += new System.EventHandler(this.FusionForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FusionForm_Closing);
            this.Resize += new System.EventHandler(this.FusionForm_Resize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static void Main()
        {
            FusionForm ff = new FusionForm();
            Application.Run(ff);

        }

        private void FusionForm_Load(object sender, System.EventArgs e)
        {
            mySignalQualityHandler = new Adapter.SignalQualityHandler(myAdapter_SignalQualityChanged);

            myStatusChangedHandler = new WLAN.StatusChangeHandler(myWlan_StatusChanged);

            MyActivateHandler = new System.EventHandler(this.listViewFusion_ItemActivate);
            this.listViewFusion.ItemActivate += MyActivateHandler;

            // Add MainMenu if Pocket PC
            if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
            {
                this.Menu = new MainMenu();
            }

            currentListView = ROOT;
            loadMainListViewItems();

            setGridLines(this.listViewFusion);
            setRowHeight(this.listViewFusion);

            // Ensure that the keyboard focus is set on a control.
            this.listViewFusion.Focus();
        }

        #region Main listView
        /// <summary>
        /// Add an item to listViewFusion
        /// </summary>
        /// <param name="number">the number to display in Number column</param>
        /// <param name="itemName">The string to displsy in Item column</param>
        private void addListViewItem(int number, string itemName)
        {
            string[] item;
            item = new string[] { itemName, number.ToString(itemNumberFormat), Resources.GetString(itemName) };
            ListViewItem li = new ListViewItem(item);
            listViewFusion.Items.Add(li);
        }
        /// <summary>
        /// Add an item to listViewFusion
        /// </summary>
        /// <param name="number">the number to display in Number column</param>
        /// <param name="itemName">The string to display in Item column</param>
        /// <param name="itemValue">The string to display in Value column</param>
        private void addListViewItem(int number, string itemName, string itemValue)
        {
            string[] item;
            item = new string[] { itemName, number.ToString(itemNumberFormat), Resources.GetString(itemName), itemValue };
            ListViewItem li = new ListViewItem(item);
            listViewFusion.Items.Add(li);
        }

        /// <summary>
        /// Add item to listViewFusion with extentions to item name
        /// </summary>
        /// <param name="number"></param>
        /// <param name="itemName"></param>
        /// <param name="itemNameExtentions"></param>
        /// <param name="itemValue"></param>
        private void addListViewItem(int number, string itemName, string itemNameExtentions, string itemValue)
        {
            string[] item;
            item = new string[] { itemName, number.ToString(itemNumberFormat), Resources.GetString(itemName) + itemNameExtentions, itemValue };
            ListViewItem li = new ListViewItem(item);
            listViewFusion.Items.Add(li);
        }

        /// <summary>
        /// Add items to the Start page of the Form
        /// </summary>
        private void loadMainListViewItems()
        {
            //Get the first adapter
            Adapter myAdapter = myWlan.Adapters[0];
            itemNumberFormat = "";

            string activeProfileName = "";

            int i = 0;
            addListViewItem(i++, EXITAPP, "");

            //save adapter status location for later references
            adapterStatusLocation = i;

            if (myWlan.Adapters[0] != null)
            {
                adapterPowerState = myWlan.Adapters[0].PowerState.ToString();

                if ((myWlan.Adapters[0].NDIS != null) && (myWlan.Adapters[0].NDIS.BSSID != null))
                {
                    if (((myWlan.Adapters[0].NDIS.BSSID.BSSID == "") || (myWlan.Adapters[0].NDIS.BSSID.BSSID == null)))
                    {
                        addListViewItem(i, ADAPTER_STATUS, adapterPowerState + ", Not associated");
                    }
                    else
                    {
                        addListViewItem(i, ADAPTER_STATUS, adapterPowerState + ", " + myWlan.Adapters[0].NDIS.BSSID.BSSID);
                    }
                }
                else
                {
                    addListViewItem(i, ADAPTER_STATUS, adapterPowerState);
                }
            }
            else
            {
                addListViewItem(i, ADAPTER_STATUS, "");
            }

            i++;

            string signalQualityString = "";
            try
            {
                signalQualityString = myAdapter.SignalStrength.ToString() + " dBm, " + myAdapter.SignalQuality.ToString();
            }
            catch (Exception)
            {
                //no action is taken
            }
            //save signal quality location for later references
            signalQualityLocation = i;
            //Display signal strength and signal quality
            addListViewItem(i++, "Signal", signalQualityString);
            essidLocation = i;
            //Display SSID
            try
            {
                addListViewItem(i, "SSID", myAdapter.ESSID);
            }
            catch
            {
                addListViewItem(i, "SSID", "");
            }

            i++;

            profileNameLocation = i;
            //Display active profile
            try
            {
                activeProfileName = "";
                activeProfileName = myAdapter.ActiveProfile.Name;
            }
            catch (Exception)
            {

            }
            addListViewItem(i++, PROFILES, activeProfileName);
            addListViewItem(i++, VERSION, "");
            addListViewItem(i++, ABOUT, "");

            // Add SignalQualityChanged event handler
            myAdapter.SignalQualityChanged += mySignalQualityHandler;

            //add StatusChanged event handler
            myWlan.StatusChanged += myStatusChangedHandler;

        }

        /// <summary>
        /// Refresh the main list view after unloading it
        /// </summary>
        private void loadMainListView()
        {
            currentListView = ROOT;
            //Add column headers
            this.listViewFusion.Columns.Add(this.NameColumn);
            this.listViewFusion.Columns.Add(this.NumberColumn);
            this.listViewFusion.Columns.Add(this.ItemColumn);
            this.listViewFusion.Columns.Add(this.ValueColumn);
            this.loadMainListViewItems();
        }
        /// <summary>
        /// Unload the start window
        /// </summary>
        private void unloadMainListView()
        {
            //Remove event handlers
            myWlan.Adapters[0].SignalQualityChanged -= mySignalQualityHandler;
            //Remove all items
            this.listViewFusion.Clear();
            //Remove column headers
            this.listViewFusion.Columns.Remove(this.NameColumn);
            this.listViewFusion.Columns.Remove(this.NumberColumn);
            this.listViewFusion.Columns.Remove(this.ItemColumn);
            this.listViewFusion.Columns.Remove(this.ValueColumn);
        }

        /// <summary>
        /// Update Signal Strength and Quality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void myAdapter_SignalQualityChanged(object sender, StatusChangeArgs e)
        {
            //populate signal quality and strength in the list
            listViewFusion.Items[signalQualityLocation].SubItems[3].Text = e.SignalStrength.ToString() + " dBm, " + e.SignalQuality.ToString();
            
            try
            {
                //populate ESSID in the list
                listViewFusion.Items[essidLocation].SubItems[3].Text = "";
                listViewFusion.Items[essidLocation].SubItems[3].Text = e.Adapter.ESSID;
            }
            catch
            {
            }
            try
            {
                //populate profile name in the list
                listViewFusion.Items[profileNameLocation].SubItems[3].Text = "";
                listViewFusion.Items[profileNameLocation].SubItems[3].Text = e.Adapter.ActiveProfile.Name;
            }
            catch
            {
            }
        }

        /// <summary>
        /// Status notification function. This updates adapter power state and the MAC address of the accesspoint with which the adapter is associated (BSSID).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void myWlan_StatusChanged(object sender, StatusChangeEventArgs e)
        {
            if (e.StatusChange == Symbol.Fusion.WLAN.StatusChangeEventArgs.StatusChanges.AdapterPowerON)
            {
                //populate adapter power state in the list
                adapterPowerState = "ON";
                listViewFusion.Items[adapterStatusLocation].SubItems[3].Text = adapterPowerState;

            }
            else if (e.StatusChange == Symbol.Fusion.WLAN.StatusChangeEventArgs.StatusChanges.AdapterPowerOFF)
            {
                adapterPowerState = "OFF";
                //populate adapter power state in the list
                listViewFusion.Items[adapterStatusLocation].SubItems[3].Text = adapterPowerState;

            }
            else if (e.StatusChange == Symbol.Fusion.WLAN.StatusChangeEventArgs.StatusChanges.AccesspointChanged)
            {
                //populate accesspoint MAC address (BSSID) in the list
                Symbol.Fusion.WLAN.StatusChangeEventArgs.APChangedEventData APData = (Symbol.Fusion.WLAN.StatusChangeEventArgs.APChangedEventData)(e.StatusChangeData);

                if (((APData.BSSID == "") || (APData.BSSID == null)))
                {
                    listViewFusion.Items[adapterStatusLocation].SubItems[3].Text = adapterPowerState + ", Not associated";
                }
                else
                {
                    listViewFusion.Items[adapterStatusLocation].SubItems[3].Text = adapterPowerState + ", " + APData.BSSID;
                }

                try
                {
                    //populate ESSID in the list
                    listViewFusion.Items[essidLocation].SubItems[3].Text = "";
                    listViewFusion.Items[essidLocation].SubItems[3].Text = APData.Adapter.ESSID;
                }
                catch
                {
                }
                try
                {
                    //populate profile name in the list
                    listViewFusion.Items[profileNameLocation].SubItems[3].Text = "";
                    listViewFusion.Items[profileNameLocation].SubItems[3].Text = APData.Adapter.ActiveProfile.Name;
                }
                catch
                {
                }

            }

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
            itemNumberFormat = "00";
            Symbol.Fusion.Version version = myConfig.Version;

            int radioDriverDelimitterIndex = version.RadioDriver.IndexOf("-");
            string radioDriverName = "";
            string radioDriverVersion = "";

            if (radioDriverDelimitterIndex >= 1)
            {
                radioDriverName = version.RadioDriver.Substring(0, radioDriverDelimitterIndex - 1);
                radioDriverVersion = version.RadioDriver.Substring(radioDriverDelimitterIndex + 2);
            }

            addListViewItem(i++, BACK, "");
            addListViewItem(i++, "Fusion", version.Fusion);
            addListViewItem(i++, "ConfigurationEditor", version.ConfigurationEditor);
            addListViewItem(i++, "LoginService", version.LoginService);
            addListViewItem(i++, "RadioDriver", "(" + radioDriverName + ")", radioDriverVersion);
            addListViewItem(i++, "WCDiag", version.WCDiag);
            addListViewItem(i++, "WCLaunch", version.WCLaunch);
            addListViewItem(i++, "WCSAPI", version.WCSAPI);
            addListViewItem(i++, "WCSRV", version.WCSRV);
            addListViewItem(i++, "WCStatus", version.WCStatus);
            addListViewItem(i++, "FusionPublicAPI", version.FusionPublicAPI);
        }

        /// <summary>
        /// load Version ListView
        /// </summary>
        private void loadVersionListView()
        {
            currentListView = VERSION;
            //Add column headers
            this.listViewFusion.Columns.Add(this.NameColumn);
            this.listViewFusion.Columns.Add(this.NumberColumn);
            this.listViewFusion.Columns.Add(this.ComponentColumn);
            this.listViewFusion.Columns.Add(this.VersionColumn);
            this.loadVersionListViewItems();
        }
        /// <summary>
        /// Unload Version listView
        /// </summary>
        private void unloadVersionListView()
        {
            //Remove all items
            this.listViewFusion.Clear();
            //Remove column headers
            this.listViewFusion.Columns.Remove(this.NameColumn);
            this.listViewFusion.Columns.Remove(this.NumberColumn);
            this.listViewFusion.Columns.Remove(this.ComponentColumn);
            this.listViewFusion.Columns.Remove(this.VersionColumn);
        }
        #endregion

        //Load unload a list of all WLAN Profiles
        #region Profiles listView

        /// <summary>
        /// Add an item to listViewFusion
        /// </summary>
        /// <param name="number">the number to display in Number column</param>
        /// <param name="profileName">The profile Name to display in Item column</param>
        private void addProfilesListViewItem(int number, string profileName)
        {
            string[] item;
            item = new string[] { Resources.GetString(PROFILE), number.ToString(itemNumberFormat), profileName + "..." };
            ListViewItem li = new ListViewItem(item);
            listViewFusion.Items.Add(li);
        }
        /// <summary>
        /// Add items to the Profiles page of the Form
        /// </summary>
        private void loadProfilesListViewItems()
        {
            Profiles myProfiles = myWlan.Profiles;
            if (myProfiles.Length > 9)
            {
                itemNumberFormat = "00";
            }
            else
            {
                itemNumberFormat = "";
            }

            int i = 0;
            addListViewItem(i++, BACK);

            //Display Profile IDs of all Profiles
            for (int profileIndex = 0; profileIndex < myProfiles.Length; profileIndex++)
            {
                Profile myProfile = myProfiles[profileIndex];
                //display the Profile ID
                addProfilesListViewItem(i++, myProfile.Name);
            }
        }
        /// <summary>
        /// Refresh the main list view after unloading it
        /// </summary>
        private void loadProfilesListView()
        {
            currentListView = PROFILES;

            //Add column headers
            this.listViewFusion.Columns.Add(this.NameColumn);
            this.listViewFusion.Columns.Add(this.NumberColumn);
            this.listViewFusion.Columns.Add(this.ProfileIDColumn);
            this.loadProfilesListViewItems();
        }
        /// <summary>
        /// Unload the start window
        /// </summary>
        private void unloadProfilesListView()
        {
            //Remove all items
            this.listViewFusion.Clear();
            //Remove column headers
            this.listViewFusion.Columns.Remove(this.NameColumn);
            this.listViewFusion.Columns.Remove(this.NumberColumn);
            this.listViewFusion.Columns.Remove(this.ProfileIDColumn);
        }

        #endregion

        /// <summary>
        /// This deligate is called when a listview item is double clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewFusion_ItemActivate(object sender, System.EventArgs e)
        {
            Cursor savedCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //Compare the zeroth element in the selected row in the listwiew
                switch (listViewFusion.Items[listViewFusion.SelectedIndices[0]].SubItems[0].Text)
                {
                    case EXITAPP:
                        this.Close();
                        break;
                    case PROFILES:
                        // Profiles list is only in main List View
                        unloadMainListView();
                        loadProfilesListView();
                        break;
                    case VERSION:
                        // Version is only in main List View
                        unloadMainListView();
                        loadVersionListView();
                        break;
                    case ABOUT:
                        // About is only in main List View
                        AboutForm myAbout = new AboutForm();
                        Cursor.Current = savedCursor;
                        myAbout.ShowDialog();
                        Cursor.Current = Cursors.WaitCursor;
                        //Ensure that the keyboard focus is set on a control.
                        this.listViewFusion.Focus();
                        break;
                    case BACK:
                        reloadLastListView();
                        break;
                    case PROFILE:
                        //Execution falls here when any row in the Profiles list is selected
                        selectedProfile = Int32.Parse(listViewFusion.Items[listViewFusion.SelectedIndices[0]].SubItems[1].Text) - 1;
                        //connect to the selected profile
                        connectToProfile(myWlan.Profiles[selectedProfile].ProfileID, true);
                        reloadLastListView();
                        break;
                    default:
                        break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Windows.Forms.MessageBox.Show("Item not selected", "CS_FusionSample1");
                this.listViewFusion.Focus();
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
                case PROFILES:
                    unloadProfilesListView();
                    break;
                case VERSION:
                    unloadVersionListView();
                    break;
                default:
                    return;
            }

            loadMainListView();
        }

        private void FusionForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (currentListView != ROOT)
            {
                //Cancell the closing event and reload the previous screen
                e.Cancel = true;
                reloadLastListView();
                return;
            }

            //The variable to hold the management state. 
            //Initialize it to Fusion.
            Symbol.Fusion.WLAN.WLAN_MANAGEMENT_STATE managementState = WLAN_MANAGEMENT_STATE.WLAN_MANAGEMENT_FUSION_STATE;

            try
            {
                //Get the current state form the device.
                managementState = myWlan.Adapters[0].WLANManagement;
            }
            catch
            {
                //In case of any failure, just continue.
            }

            //Only if the current management state is WZC, skip performing any profile - related operations.
            if (!((managementState == WLAN_MANAGEMENT_STATE.WLAN_MANAGEMENT_WZC_STATE) || (managementState == WLAN_MANAGEMENT_STATE.WLAN_MANAGEMENT_WZC_STATE_REBOOT_IN_FUSION_STATE)))
            {
                //Delete the profiles created in this sample
                deleteSampleProfiles();
            }

            myWlan.Dispose();
            myConfig.Dispose();
        }

        private void ListViewFusion_KeyUp(object sender, KeyEventArgs e)
        {
            this.listViewFusion.Focus();
        }

        /// <summary>
        /// Handle keyboard navigation of the listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewFusion_KeyDown(object sender, KeyEventArgs e)
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
                    if (listViewFusion.Items.Count > tmpIndex)
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
                    if (listViewFusion.Items.Count > tmpIndex)
                    {
                        selectedIndex = tmpIndex;
                        if (listViewFusion.Items.Count < 10)
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
            if (selectedIndex <= listViewFusion.Items.Count)
            {
                //remove all previous selections
                for (int i = 0; i < listViewFusion.Items.Count; i++)
                {
                    listViewFusion.Items[i].Selected = false;
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
                listViewFusion.Items[selectedIndex].Selected = true;
                listViewFusion.Invoke(this.MyActivateHandler);
            }
        }


        /// <summary>
        /// Create two sample profiles
        /// <summary>
        private void createSampleProfiles()
        {
            //Create a WLAN object in COMMAND_MODE
            WLAN myCommandModeWlan = null;
            try
            {
                myCommandModeWlan = new WLAN(FusionAccessType.COMMAND_MODE);
            }
            catch (OperationFailureException)
            {
                System.Windows.Forms.MessageBox.Show("Command mode is in use", "CS_FusionSample1");
                this.listViewFusion.Focus();
                return;
            }

            //if (!findProfile("Sample-M-Adhoc"))
            if (getProfileByName("Sample-M-Adhoc", myCommandModeWlan) == null)
            {
                //Create a simple adhoc profile
                AdhocProfileData myAdhocProfileData = new AdhocProfileData("Sample-M-Adhoc", "Sample-Adhoc");
                try
                {
                    myAdhocProfile = myCommandModeWlan.Profiles.CreateAdhocProfile(myAdhocProfileData);
                    bAdhocCreated = true;
                }
                catch
                {
                    // This is just to allow the application to continue even if the profile creation fails ...
                }

            }
            //if (!findProfile("Sample-M-Infrastructure"))
            if (getProfileByName("Sample-M-Infrastructure", myCommandModeWlan) == null)
            {
                //Create a simple infrastructure profile
                InfrastructureProfileData myInfrastructureProfileData = new InfrastructureProfileData("Sample-M-Infrastructure", "Sample-Infrastructure");
                try
                {
                    myInfrastructureProfile = myCommandModeWlan.Profiles.CreateInfrastructureProfile(myInfrastructureProfileData);
                    bInfraCreated = true;
                }
                catch
                {
                    // This is just to allow the application to continue even if the profile creation fails ...
                }
            }

            //Dispose COMMAND_MODE WLAN object
            myCommandModeWlan.Dispose();
            myCommandModeWlan = null;

        }

        /// <summary>
        /// Find and return Profile with the given name
        /// </summary>
        /// <param name="profileName">Name of the interested Profile</param>
        /// <param name="wlan">The WLAN instance for searching</param>
        /// <returns>Profile instance if found. Otherwise returns null</returns>
        private Profile getProfileByName(string profileName, WLAN myNewWlan)
        {
            Symbol.Fusion.WLAN.Profiles myProfiles = myNewWlan.Profiles;
            //traverse all Profiles
            for (int profileIndex = 0; profileIndex < myProfiles.Length; profileIndex++)
            {
                Profile myProfile = myProfiles[profileIndex];
                if (profileName == myProfile.Name)
                {
                    return myProfile;
                }
            }
            return null;
        }

        /// <summary>
        /// Delete all the profiles created by createSampleProfiles()
        /// </summary>
        private void deleteSampleProfiles()
        {
            //Create a WLAN object in COMMAND_MODE
            WLAN myCommandModeWlan = null;
            try
            {
                myCommandModeWlan = new WLAN(FusionAccessType.COMMAND_MODE);
            }
            catch (OperationFailureException)
            {
                System.Windows.Forms.MessageBox.Show("Command mode is in use", "CS_FusionSample1");
                this.listViewFusion.Focus();
                return;
            }

            //Delete the profile Sample-M-Adhoc only if it has been created by this sample itself. 
            if (bAdhocCreated)
            {
                //get a reference to the Sample-M-Adhoc profile
                myAdhocProfile = getProfileByName("Sample-M-Adhoc", myCommandModeWlan);
                if (myAdhocProfile != null)
                {
                    myCommandModeWlan.Profiles.DeleteProfile(myAdhocProfile);
                    bAdhocCreated = false;
                }
            }

            //Delete the profile Sample-M-Infrastructure only if it has been created by this sample itself. 
            if (bInfraCreated)
            {
                //get a reference to the Sample-M-Infrastructure profile
                myInfrastructureProfile = getProfileByName("Sample-M-Infrastructure", myCommandModeWlan);
                if (myInfrastructureProfile != null)
                {
                    myCommandModeWlan.Profiles.DeleteProfile(myInfrastructureProfile);
                    bInfraCreated = false;
                }
            }

            //dispose the WLAN object
            myCommandModeWlan.Dispose();
            myCommandModeWlan = null;

        }

        /// <summary>
        /// Connect to a profile with given ID
        /// </summary>
        /// <param name="profileID"></param>
        /// <param name="persistance"></param>
        private void connectToProfile(string profileID, bool persistance)
        {
            //Create a WLAN object in COMMAND_MODE
            WLAN myCommandModeWlan = null;
            try
            {
                myCommandModeWlan = new WLAN(FusionAccessType.COMMAND_MODE);
            }
            catch (OperationFailureException)
            {
                System.Windows.Forms.MessageBox.Show("Command mode is in use", "CS_FusionSample1");
                this.listViewFusion.Focus();
                return;
            }

            //search the profile and connect if found
            Profile myProfile = getProfileByID(profileID, myCommandModeWlan);
            if (myProfile != null)
            {
                Symbol.Fusion.FusionResults result = myProfile.Connect(persistance);

                if (result != Symbol.Fusion.FusionResults.SUCCESS)
                {
                    MessageBox.Show("Failure in connecting to the specified profile. Result = " + result);
                }
            }

            //dispose the created WLAN object
            myCommandModeWlan.Dispose();

        }

        /// <summary>
        /// Search the profile by ID and return it
        /// </summary>
        /// <param name="profileID"></param>
        /// <param name="myNewWlan"></param>
        /// <returns></returns>
        private Profile getProfileByID(string profileID, WLAN myNewWlan)
        {
            Symbol.Fusion.WLAN.Profiles myProfiles = myNewWlan.Profiles;
            //traverse all Profiles
            for (int profileIndex = 0; profileIndex < myProfiles.Length; profileIndex++)
            {
                Profile myProfile = myProfiles[profileIndex];
                if (profileID == myProfile.ProfileID)
                {
                    return myProfile;
                }
            }
            return null;
        }

        private void FusionForm_Resize(object sender, EventArgs e)
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
            listViewFusion.Width = this.Width;

            // Main list
            this.NumberColumn.Width = (13 * listViewFusion.Width) / 100;
            this.ItemColumn.Width = (36 * listViewFusion.Width) / 100; ;
            this.ValueColumn.Width = (50 * listViewFusion.Width) / 100;

            // Version list
            this.NumberColumn.Width = (13 * listViewFusion.Width) / 100;
            this.ComponentColumn.Width = (43 * listViewFusion.Width) / 100;
            this.VersionColumn.Width = (43 * listViewFusion.Width) / 100;

            // Profile list
            this.NumberColumn.Width = (13 * listViewFusion.Width) / 100;
            this.ProfileIDColumn.Width = (86 * listViewFusion.Width) / 100;
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
            this.imageList.ImageSize = new Size(1, (int)(rowHeight * ROW_HEIGHT_FACTOR));
            lvw.SmallImageList = this.imageList;
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
