using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Symbol.Barcode;

namespace CS_ScanRSM
{
    public partial class RSMForm : Form
    {

        private System.Windows.Forms.ColumnHeader NameColumn = null;
        private System.Windows.Forms.ColumnHeader NumberColumn = null;
        private System.Windows.Forms.ColumnHeader ItemColumn = null;
        private System.Windows.Forms.ColumnHeader ValueColumn = null;
        private System.Windows.Forms.ColumnHeader AttributeNumColumn = null;

        private System.Windows.Forms.ListView listViewRSM = null;
		private Resources MyResources = null;

        private Timer timer1;
        private int selectedIndex = 0;
        System.EventHandler MyActivateHandler = null;
        //A formatting string used with listview items
        string itemNumberFormat = "";
        string currentListView = "";

        //Create the Reader reference
        Symbol.Barcode.Reader MyReader = null;

		const string ROOT = "Main";
		const string ABOUT = "About";
		const string BACK = "Back";
		const string EXITAPP = "ExitApp";
        const string RSMATTRIBUTES = "RSMAttributes";
        const string RSMATTRIBUTEVALUE = "RSMAttributeValue";
        const string RSMATTRIBUTEVALUESELECT = "RSMAttributeValueSelect";

        string selectedAttribNewVal = string.Empty;
        string selectedAttribCurrentVal = string.Empty;
        private Panel panel1;
        string selectedAttribName = string.Empty;

        // Used to adjust the row height of listViewRSM.
        // (This is a workaround in the absense of an exposed API to control the 
        // row height of ListView control)
        private System.Windows.Forms.ImageList imageList1;

        // The factor(n) which defines the row height of the ListView. 
        // The default row height would be multiplied by this factor (ROW_HEIGHT_FACTOR = n).
        // Currently set to 2. So the row height would be doubled in this sample.
        // The user can set it to any value (i.e. 1,2,3,..., etc.) based on the requirements.
        private const int ROW_HEIGHT_FACTOR = 2;

        public RSMForm()
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
            this.AttributeNumColumn.Text = Resources.GetString("AttribNum");
            //set time interval for number input as 1 second
            this.timer1.Interval = 1000;

            // Used to adjust the row height of listViewRSM.
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
            this.listViewRSM = new System.Windows.Forms.ListView();
            this.NameColumn = new System.Windows.Forms.ColumnHeader();
            this.NumberColumn = new System.Windows.Forms.ColumnHeader();
            this.ItemColumn = new System.Windows.Forms.ColumnHeader();
            this.AttributeNumColumn = new System.Windows.Forms.ColumnHeader();
            this.ValueColumn = new System.Windows.Forms.ColumnHeader();
            this.timer1 = new System.Windows.Forms.Timer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewRSM
            // 
            this.listViewRSM.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewRSM.Columns.Add(this.NameColumn);
            this.listViewRSM.Columns.Add(this.NumberColumn);
            this.listViewRSM.Columns.Add(this.ItemColumn);
            this.listViewRSM.Columns.Add(this.AttributeNumColumn);
            this.listViewRSM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewRSM.FullRowSelect = true;
            this.listViewRSM.Location = new System.Drawing.Point(0, 0);
            this.listViewRSM.Name = "listViewRSM";
            this.listViewRSM.Size = new System.Drawing.Size(240, 272);
            this.listViewRSM.TabIndex = 0;
            this.listViewRSM.View = System.Windows.Forms.View.Details;
            this.listViewRSM.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListViewRSM_KeyUp);
            this.listViewRSM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewRSM_KeyDown);
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
            this.ItemColumn.Width = 250;
            // 
            // AttributeNumColumn
            // 
            this.AttributeNumColumn.Text = "Attribute";
            this.AttributeNumColumn.Width = 150;
            // 
            // ValueColumn
            // 
            this.ValueColumn.Text = "ColumnHeader";
            this.ValueColumn.Width = 150;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listViewRSM);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 272);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // RSMForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(240, 272);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "RSMForm";
            this.Text = "ScanRSM";
            this.Load += new System.EventHandler(this.RSMForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.RSMForm_Closing);
            this.Resize += new System.EventHandler(this.RSMForm_Resize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
            RSMForm rsmForm = new RSMForm();
            Application.Run(rsmForm);

		}

		private void RSMForm_Load(object sender, System.EventArgs e)
		{
            currentListView = ROOT;

            // Initialize the Reader
            if (false == this.InitReader())
            {
                MessageBox.Show("Unable to initialize device", "Error");

                // If not, close this form
                this.Close();

                return;
            }

            MyActivateHandler = new System.EventHandler(this.listViewRSM_ItemActivate);
            this.listViewRSM.ItemActivate += MyActivateHandler;
			
            // Add MainMenu if Pocket PC
			if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
			{
				this.Menu = new MainMenu();
			}
            
            loadMainListView();

            setGridLines(this.listViewRSM);
            setRowHeight(this.listViewRSM);

            // Ensure that the keyboard focus is set on a control.
            this.listViewRSM.Focus();
        }

        /// <summary>
        /// Initialize the reader.
        /// </summary>
        private bool InitReader()
        {
            // If reader is already present then fail initialize
            if (this.MyReader != null)
            {
                return false;
            }

            // Get selected device from user
            Symbol.Generic.Device MyDevice =
                Symbol.StandardForms.SelectDevice.Select(
                Symbol.Barcode.Device.Title,
                Symbol.Barcode.Device.AvailableDevices);

            if (MyDevice == null)
            {
                MessageBox.Show("No Device Selected", "SelectDevice");

                return false;
            }

            try
            {
                // Create new reader.
                MyReader = new Symbol.Barcode.Reader(MyDevice);

                // Enable reader
                this.MyReader.Actions.Enable();
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Disable/close reader
        /// </summary>
        private void TermReader()
        {
            // If we have a reader
            if (this.MyReader != null)
            {
                // Disable the reader
                this.MyReader.Actions.Disable();

                this.MyReader.Dispose();

                this.MyReader = null;
            }
        }

		#region Main listView
		/// <summary>
		/// Add an item to listViewRSM
		/// </summary>
		/// <param name="number">the number to display in Number column</param>
		/// <param name="itemName">The string to displsy in Item column</param>
        private void addListViewItem(int number, string itemName)
		{
			string[] item;
            item = new string[] { itemName, number.ToString(itemNumberFormat), Resources.GetString(itemName) };
			ListViewItem li = new ListViewItem(item);
			listViewRSM.Items.Add(li);
		}
        /// <summary>
        /// Add an item to listViewRSM
        /// </summary>
        /// <param name="number">The number to display in Number column</param>
        /// <param name="itemName">The string to display in Item column</param>
        /// <param name="itemValue">The string to display in Value column</param>
        private void addListViewItem(int number, string itemName, string itemValue)
        {
            string[] item;
            item = new string[] { itemName, number.ToString(itemNumberFormat), Resources.GetString(itemName), itemValue };
            ListViewItem li = new ListViewItem(item);
            listViewRSM.Items.Add(li);
        }

        /// <summary>
        /// Add item to listViewRSM with extentions to item name
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
            listViewRSM.Items.Add(li);
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
            addListViewItem(i++, RSMATTRIBUTES, "");
			addListViewItem(i++, ABOUT, "");


		}

		/// <summary>
		/// Refresh the main list view after unloading it
		/// </summary>
		private void loadMainListView()
		{
            currentListView = ROOT;
            this.listViewRSM.Columns.Clear();
			//Add column headers
			this.listViewRSM.Columns.Add(this.NameColumn);
			this.listViewRSM.Columns.Add(this.NumberColumn);
            this.listViewRSM.Columns.Add(this.ItemColumn);
			this.loadMainListViewItems();
		}
		/// <summary>
		/// Unload the start window
		/// </summary>
		private void unloadMainListView()
		{
			this.listViewRSM.Clear();
			//Remove column headers
			this.listViewRSM.Columns.Remove(this.NameColumn);
			this.listViewRSM.Columns.Remove(this.NumberColumn);
			this.listViewRSM.Columns.Remove(this.ItemColumn);
        }

		#endregion 


        //Load unload a list of all RSM attributes
        #region RSMAttributes listView

        /// <summary>
        /// Add an item to listViewRSM
        /// </summary>
        /// <param name="number">the number to display in Number column</param>
        /// <param name="attribName">The RSM attribute Name to display in Item column</param>
        /// <param name="attribValue">The RSM attribute value to display in Item column</param>
        private void addRSMAttributesListViewItem(int number, string attribName, string attribValue)
        {
            string[] item;
            item = new string[] { RSMATTRIBUTEVALUE, number.ToString(itemNumberFormat), attribName, attribValue};
            ListViewItem li = new ListViewItem(item);
            listViewRSM.Items.Add(li);
        }

        /// <summary>
        /// Add items to the Attributes page of the Form
        /// </summary>
        private void loadRSMAttributesListViewItems()
        {
            itemNumberFormat = "00";

            int i = 0;
            addListViewItem(i++, BACK);

            string attribValue = string.Empty;
            string attribNum = string.Empty;

            try
            {
                //Display all supported RSM attributes and the corresponding value.
                for (int attributesIndex = 0; attributesIndex < MyReader.RSM.SupportedAttribs.Length; attributesIndex++)
                {
                    try
                    {
                        ATTRIB_NUMBER iAttribNum = MyReader.RSM.SupportedAttribs[attributesIndex].AttribNumber;

                        attribNum = iAttribNum.ToString();

                        switch (iAttribNum)
                        {
                            case ATTRIB_NUMBER.MODEL_NUMBER:
                                attribValue = MyReader.RSM.ModelNumber.CurrentValue;
                                break;
                            case ATTRIB_NUMBER.SERIAL_NUMBER:
                                attribValue = MyReader.RSM.SerialNumber.CurrentValue;
                                break;
                            case ATTRIB_NUMBER.DATE_OF_MANUFACTURE:
                                attribValue = MyReader.RSM.DateofManufacture.CurrentValue;
                                break;
                            case ATTRIB_NUMBER.DATE_OF_SERVICE:
                                attribValue = MyReader.RSM.DateofService.CurrentValue;
                                break;
                            case ATTRIB_NUMBER.BT_ADDR:
                                attribValue = MyReader.RSM.BluetoothAddress.CurrentValue;
                                break;
                            case ATTRIB_NUMBER.BT_AUTHENTICATION:
                                attribValue = MyReader.RSM.BluetoothAuthentication.CurrentValue.ToString();
                                attribNum = attribNum + "...";
                                break;
                            case ATTRIB_NUMBER.BT_ENCRYPTION:
                                attribValue = MyReader.RSM.BluetoothEncryption.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.BT_PINCODE:
                                attribValue = MyReader.RSM.BluetoothPINCode.CurrentValue;
                                attribNum = attribNum + "...";
                                break;
                            case ATTRIB_NUMBER.RECONNECT_ATTEMPTS:
                                attribValue = MyReader.RSM.ReconnectAttempts.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.BEEP_ON_RECON_ATTEMPT:
                                attribValue = MyReader.RSM.BeeponReconnectAttempt.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.HID_AUTO_RECON:
                                attribValue = MyReader.RSM.HIDAutoReconnect.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.BT_FRIENDLY_NAME:
                                attribValue = MyReader.RSM.BluetoothFriendlyName.CurrentValue;
                                break;
                            case ATTRIB_NUMBER.PIN_CODE_TYPE:
                                attribValue = MyReader.RSM.PINCodeType.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.BT_INQUIRY_MODE:
                                attribValue = MyReader.RSM.BluetoothInquiryMode.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.MEMS_ENABLE:
                                attribValue = MyReader.RSM.MemsEnable.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.PROXIMITY_ENABLE:
                                attribValue = MyReader.RSM.ProximityEnable.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.PROXIMITY_DISTANCE:
                                attribValue = MyReader.RSM.ProximityDistance.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.PAGING_ENABLE:
                                attribValue = MyReader.RSM.PagingEnable.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.PAGING_BEEP_SEQ:
                                attribValue = MyReader.RSM.PagingBeepSequence.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.LOW_BATTERY_IND_EN:
                                attribValue = MyReader.RSM.LowBatteryIndicationEnable.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.SCAN_TRIG_WAKEUP_EN:
                                attribValue = MyReader.RSM.ScantriggerWakeupEnable.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.BLUETOOTH_AUTO_RECON:
                                attribValue = MyReader.RSM.BluetoothAutoReconnect.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.LOW_BATTERY_IND_CYCLE:
                                attribValue = MyReader.RSM.LowBatteryIndicationCycle.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.PREFERRED_WIRELESSHOST:
                                attribValue = MyReader.RSM.PreferredWirelessHost.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.FIRM_VERSION:
                                attribValue = MyReader.RSM.FirmwareVersion.CurrentValue;
                                break;
                            case ATTRIB_NUMBER.DEVICE_CLASS:
                                attribValue = MyReader.RSM.DeviceClass.CurrentValue;
                                break;
                            case ATTRIB_NUMBER.BATTERY_STATUS:
                                attribValue = MyReader.RSM.BatteryStatus.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.BATTERY_CAPACITY:
                                attribValue = MyReader.RSM.BatteryCapacity.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.BATTERY_ID:
                                attribValue = MyReader.RSM.BatteryID.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.HARDWARE_VERSION:
                                attribValue = MyReader.RSM.HardwareVersion.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.SCANLINE_WIDTH:
                                attribValue = MyReader.RSM.ScanlineWidth.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.PROXIMITY_CONTINUOUS_EN:
                                attribValue = MyReader.RSM.ProximityContinuousEnable.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.GOOD_SCANS_DELAY:
                                attribValue = MyReader.RSM.GoodScansDelay.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.EXCLUSIVE_CODE128_EN:
                                attribValue = MyReader.RSM.ExclusiveCode128Enable.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.DISABLE_GOOD_DECODE_LED_BEEP:
                                attribValue = MyReader.RSM.DisableGoodDecodeLEDBeep.CurrentValue.ToString();
                                break;
                            case ATTRIB_NUMBER.FORCE_PAIRING_SAVE:
                                attribValue = MyReader.RSM.ForcePairingSave.CurrentValue.ToString();
                                break;
                            default:
                                attribValue = "N/A";
                                break;
                        }

                        //display the RSM attribute number and its value
                        addRSMAttributesListViewItem(i++, attribNum, attribValue);

                    }
                    catch (NullReferenceException ex)
                    {
                        // NullReferenceException can occur when there is an attribute on the device which is
                        // not supported on the class library. In that case, the exception can be ignored and it will not cause any harm.
                    }
                    catch (Symbol.Exceptions.OperationFailureException opEx)
                    {
                        MessageBox.Show("Exception - " + opEx.Message);
                        return;
                    }
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Failed to get supported attributes");
                reloadLastListView();
            }

        }

        /// <summary>
        /// Load RSM attributes list view
        /// </summary>
        private void loadRSMAttributesListView()
        {
            currentListView = RSMATTRIBUTES;

            //Add column headers
            this.listViewRSM.Columns.Add(this.NameColumn);
            this.listViewRSM.Columns.Add(this.NumberColumn);
            this.listViewRSM.Columns.Add(this.AttributeNumColumn);
            this.listViewRSM.Columns.Add(this.ValueColumn);
            this.loadRSMAttributesListViewItems();
        }

        private void unloadRSMAttributesListView()
        {
            //Remove all items
            this.listViewRSM.Clear();
            //Remove column headers
            this.listViewRSM.Columns.Remove(this.NameColumn);
            this.listViewRSM.Columns.Remove(this.NumberColumn);
            this.listViewRSM.Columns.Remove(this.AttributeNumColumn);
            this.listViewRSM.Columns.Remove(this.ValueColumn);
        }
        #endregion

        #region RSMAttributeValueSelectView

        private void loadRSMAttributeValueSelectListView()
        {
            currentListView = RSMATTRIBUTEVALUESELECT;

            //Add column headers
            this.listViewRSM.Columns.Add(this.NameColumn);
            this.listViewRSM.Columns.Add(this.NumberColumn);
            this.listViewRSM.Columns.Add(this.ValueColumn);
            this.loadRSMAttributeValueSelectListViewItems();
        }

        private void unloadRSMAttributeValueSelectListView()
        {
            //Remove all items
            this.listViewRSM.Clear();
            //Remove column headers
            this.listViewRSM.Columns.Remove(this.NameColumn);
            this.listViewRSM.Columns.Remove(this.NumberColumn);
            this.listViewRSM.Columns.Remove(this.ValueColumn);
        }

        private void loadRSMAttributeValueSelectListViewItems()
        {
            itemNumberFormat = "";

            int i = 0;
            addListViewItem(i++, BACK);

            if (selectedAttribName == "BT_AUTHENTICATION")
            {
                addRSMAttributeValueSelectListViewItem(i++, "TRUE");
                addRSMAttributeValueSelectListViewItem(i++, "FALSE");

                if (selectedAttribCurrentVal.ToLower() == "true")
                {
                    listViewRSM.Items[1].Selected = true;
                }
                else
                {
                    listViewRSM.Items[2].Selected = true;
                }
            }
        }

        /// <summary>
        /// Add an item to listViewRSM
        /// </summary>
        /// <param name="number">the number to display in Number column</param>
        /// <param name="attribValue">The RSM attribute value to display in Item column</param>
        private void addRSMAttributeValueSelectListViewItem(int number, string attribValue)
        {
            string[] item;
            item = new string[] { RSMATTRIBUTEVALUESELECT, number.ToString(itemNumberFormat), attribValue };
            ListViewItem li = new ListViewItem(item);
            listViewRSM.Items.Add(li);
        }

        #endregion

        /// <summary>
		/// This deligate is called when a listview item is double clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewRSM_ItemActivate(object sender, System.EventArgs e)
		{
            Cursor savedCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            

            try
			{
				//Compare the zeroth element in the selected row in the listwiew
				switch (listViewRSM.Items[listViewRSM.SelectedIndices[0]].SubItems[0].Text)
				{
					case EXITAPP :
						this.Close();
						break;
					case ABOUT :  
						// About is only in main List View
                        AboutForm myAbout = new AboutForm();
                        Cursor.Current = savedCursor;
                        myAbout.ShowDialog();
                        Cursor.Current = Cursors.WaitCursor;
                        //Ensure that the keyboard focus is set on a control.
                        this.listViewRSM.Focus();
                        break;
                    case RSMATTRIBUTES:
                        unloadMainListView();
                        loadRSMAttributesListView();
                        break;
                    case RSMATTRIBUTEVALUE:
                        if (true == listViewRSM.Items[listViewRSM.SelectedIndices[0]].SubItems[2].Text.EndsWith("..."))
                        {
                            selectedAttribName = listViewRSM.Items[listViewRSM.SelectedIndices[0]].SubItems[2].Text.TrimEnd('.');
                            selectedAttribCurrentVal = listViewRSM.Items[listViewRSM.SelectedIndices[0]].SubItems[3].Text;

                            if (selectedAttribName == "BT_PINCODE")
                            {
                                UserInputForm userInpBox = new UserInputForm();
                                userInpBox.DoScale();

                                if (true == userInpBox.GetUserInput(selectedAttribName, "Enter new value:", selectedAttribCurrentVal, ref selectedAttribNewVal))
                                {
                                    if (true == SetAttributeValue(selectedAttribName, selectedAttribNewVal))
                                    {
                                        unloadRSMAttributesListView();
                                        loadRSMAttributesListView();
                                    }
                                }
                                userInpBox.Dispose();
                            }

                            if (selectedAttribName == "BT_AUTHENTICATION")
                            {
                                unloadRSMAttributesListView();
                                this.ValueColumn.Text = selectedAttribName;
                                loadRSMAttributeValueSelectListView();
                            }
                        }
                        break;
                    case RSMATTRIBUTEVALUESELECT:
                        selectedAttribNewVal = listViewRSM.Items[listViewRSM.SelectedIndices[0]].SubItems[2].Text;
                        SetAttributeValue(selectedAttribName, selectedAttribNewVal);
                        reloadLastListView();
                        break;
					case BACK:  
						reloadLastListView();
						break;
					default: 
						break;
				}
   			}
			catch(ArgumentOutOfRangeException)
			{
				System.Windows.Forms.MessageBox.Show("Item not selected", "CS_ScanRSM");
                this.listViewRSM.Focus();
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
                case RSMATTRIBUTES:
                    unloadRSMAttributesListView();
                    break;
                case RSMATTRIBUTEVALUESELECT:
                    unloadRSMAttributeValueSelectListView();
                    this.ValueColumn.Text = Resources.GetString("Value");
                    loadRSMAttributesListView();
                    return;
                default:
                    return;
            }


			loadMainListView();
		}

		private void RSMForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            if (currentListView != ROOT)
            {
                //Cancell the closing event and reload the previous screen
                e.Cancel = true;
                reloadLastListView();
                return;
            }

            // Terminate reader
            this.TermReader();
		}

        private void ListViewRSM_KeyUp(object sender, KeyEventArgs e)
        {
            this.listViewRSM.Focus();
        }

        /// <summary>
        /// Handle keyboard navigation of the listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewRSM_KeyDown(object sender, KeyEventArgs e)
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
                    if (listViewRSM.Items.Count > tmpIndex)
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
                    if (listViewRSM.Items.Count > tmpIndex)
                    {
                        selectedIndex = tmpIndex;
                        if (listViewRSM.Items.Count <= 10)
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
            if (selectedIndex <= listViewRSM.Items.Count)
            {
                //remove all previous selections
                for (int i = 0; i < listViewRSM.Items.Count; i++)
                {
                    listViewRSM.Items[i].Selected = false;
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
                listViewRSM.Items[selectedIndex].Selected = true;
                listViewRSM.Invoke(this.MyActivateHandler);       
            }
        }

        private void RSMForm_Resize(object sender, EventArgs e)
        {
            // If it is CE
            if (Symbol.Win32.PlatformType.IndexOf("PocketPC", 0) <= 0)
            {
                this.Width = (Screen.PrimaryScreen.WorkingArea.Width > 320 ? 320 : Screen.PrimaryScreen.WorkingArea.Width);
                this.Height = (Screen.PrimaryScreen.WorkingArea.Height > 320 ? 320 : Screen.PrimaryScreen.WorkingArea.Height);
            }
        }

        private bool SetAttributeValue(string attributeName, string newAttributeValue)
        {
            switch (attributeName)
            {
                case "BT_PINCODE":
                    MyReader.RSM.BluetoothPINCode.CurrentValue = newAttributeValue;
                    return true;
                case "BT_AUTHENTICATION":
                    if (newAttributeValue.ToLower() == "true" || newAttributeValue.ToLower() == "false")
                    {
                        MyReader.RSM.BluetoothAuthentication.CurrentValue = Convert.ToBoolean(newAttributeValue);
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }

        private void SetListViewColumnWidth()
        {
            listViewRSM.Width = this.Width;

            // Main list
            this.NumberColumn.Width = (13 * listViewRSM.Width) / 100;
            this.ItemColumn.Width = (50 * listViewRSM.Width) / 100;
            
            // Attribute list
            this.NumberColumn.Width = (13 * listViewRSM.Width) / 100;
            this.AttributeNumColumn.Width = (40 * listViewRSM.Width) / 100;
            this.ValueColumn.Width = (50 * listViewRSM.Width) / 100;

            // Attribute value select list
            this.NumberColumn.Width = (13 * listViewRSM.Width) / 100;
            this.ValueColumn.Width = (50 * listViewRSM.Width) / 100;
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