using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Symbol.Telemetry;

namespace CS_VBusSample1
{
    public partial class VBusForm : Form
    {
        private Timer timer1;
        private int selectedIndex = 0;
        private System.EventHandler myActivateHandler = null;
        private string currentListView = "";
        private string itemNumberFormat = "";

        private const string ROOT = "Main";
        private const string EXITAPP = "ExitApp";
        private const string ROAD_SPEED = "RoadSpeed";
        private const string TOTAL_DISTANCE = "TotalDistance";
        private const string ENGINE_RPM = "EngineRPM";
        private const string VERSIONS = "Versions";
        private const string ABOUT = "About";
        private const string BACK = "Back";

        private VBus.EnabledParameters.Value valueRead = null;

        private VBus m_VBus = null;
        private VBus.EnabledParameters.EParamInfo ePInfo_RS = null;
        private VBus.EnabledParameters.EParamInfo ePInfo_TD = null;
        private VBus.EnabledParameters.EParamInfo ePInfo_RPM = null;

        private VBus.EnabledParameters.ParameterReceivedNotifyHandler myHandler = null;
        
        private delegate void UpdateParameterReceivedNotify(Symbol.Telemetry.VBUS_TPARAM_ID paramID, String sValue);
        private UpdateParameterReceivedNotify myParamsUpdateUIHandler = null;

        private string capiVersion = null;
        private string driverVersion = null;

        private bool isLoaded = false;

        // The factor(n) which defines the row height of the ListView. 
        //  The default row height would be multiplied by this factor (ROW_HEIGHT_FACTOR = n).
        private const int ROW_HEIGHT_FACTOR = 2; // Currently set to 2. So the row height would be doubled in this sample.
                                                 // The user can set it to any value (i.e. 1,2,3,..., etc.) based on the requirements.

        public VBusForm()
        {
            //save the current cursor
            Cursor savedCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            InitializeComponent();

            this.timer1.Interval = 1000;

            Cursor.Current = savedCursor;
        }

        private void VBusForm_Load(object sender, EventArgs e)
        {
            try
            {
                myActivateHandler = new System.EventHandler(this.listViewVBus_ItemActivate);
                this.listViewVBus.ItemActivate += myActivateHandler;

                // Since the Aspen device is Windows Mobile/Pocket PC, add the MainMenu.
                this.Menu = new MainMenu();

                currentListView = ROOT;
                loadMainListViewItems();

                this.NumberColumn.Width = 30;
                this.ItemColumn.Width = (int)(((this.Width) - (this.NumberColumn.Width)) * (0.4));
                this.ValueColumn.Width = (int)(((this.Width) - (this.NumberColumn.Width)) * (0.6));

                InitVBus();

                setGridLines(this.listViewVBus);
                setRowHeight(this.listViewVBus);

                // Ensure that the keyboard focus is set on a control.
                listViewVBus.Focus();

                isLoaded = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("The following exception occurred : " + ex.Message);
                this.Close();
                return;
            }

        }

        public void InitVBus()
        {
            m_VBus = new VBus(VBUS_OPEN_SETTING.VBUS_OPEN_STREAM);

            ePInfo_RS = new VBus.EnabledParameters.EParamInfo();
            ePInfo_RS.ParamID = VBUS_TPARAM_ID.TPID_ROAD_SPEED;
            ePInfo_RS.NotifyReceipt = true;

            ePInfo_TD = new VBus.EnabledParameters.EParamInfo();
            ePInfo_TD.ParamID = VBUS_TPARAM_ID.TPID_TOTAL_DISTANCE;
            ePInfo_TD.NotifyReceipt = true;

            ePInfo_RPM = new VBus.EnabledParameters.EParamInfo();
            ePInfo_RPM.ParamID = VBUS_TPARAM_ID.TPID_ENGINE_RPM;
            ePInfo_RPM.NotifyReceipt = true;

            VBusResults result = m_VBus.Enable();

            if (VBusResults.VBUS_SUCCESS != result)
            {
                MessageBox.Show("Failure in enabling the VBus, returned : " + result);
            }

            result = m_VBus.EnabledParams.Add(ePInfo_RS);

            if (VBusResults.VBUS_SUCCESS != result)
            {
                MessageBox.Show("Failure in adding the Road Speed parameter, returned : " + result);
            }

            result = m_VBus.EnabledParams.Add(ePInfo_TD);

            if (VBusResults.VBUS_SUCCESS != result)
            {
                MessageBox.Show("Failure in adding the Total Distance parameter, returned : " + result);
            }

            result = m_VBus.EnabledParams.Add(ePInfo_RPM);

            if (VBusResults.VBUS_SUCCESS != result)
            {
                MessageBox.Show("Failure in adding the RPM parameter, returned : " + result);
            }

            capiVersion = m_VBus.VBusMgr.Version.CAPIVersion;
            driverVersion = m_VBus.VBusMgr.Version.DriverVersion;

            myHandler = new VBus.EnabledParameters.ParameterReceivedNotifyHandler(NotifyHandler);
            m_VBus.EnabledParams.ParameterReceivedNotify += myHandler;

            myParamsUpdateUIHandler = new UpdateParameterReceivedNotify(UpdateParamsUI);

        }

        private void NotifyHandler(object sender, VBus.EnabledParameters.ParameterReceivedEventArgs evntArg)
        {
            try
            {
                if (true == isLoaded)
                {
                    if (currentListView == ROOT)
                    {
                        valueRead = m_VBus.EnabledParams.ReadQuery(evntArg.eParamInfo);
                        
                        string stringValue = "";

                        stringValue = valueRead.StrValue + " "+ GetUOMString(valueRead.UOM);

                        listViewVBus.Invoke(this.myParamsUpdateUIHandler, new object[] { evntArg.eParamInfo.ParamID, stringValue });

                    }
                }
            }
            //catch
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("ex : " + ex.Message);

            }
             
        }

        private void UpdateParamsUI(Symbol.Telemetry.VBUS_TPARAM_ID paramID, String sValue)
        {
            if (ROOT == currentListView)
            {
                switch (paramID)
                {
                    case VBUS_TPARAM_ID.TPID_ROAD_SPEED:
                        listViewVBus.Items[1].SubItems[3].Text = sValue;
                        break;

                    case VBUS_TPARAM_ID.TPID_TOTAL_DISTANCE:
                        listViewVBus.Items[2].SubItems[3].Text = sValue;
                        break;

                    case VBUS_TPARAM_ID.TPID_ENGINE_RPM:
                        listViewVBus.Items[3].SubItems[3].Text = sValue;
                        break;

                }
            }

        }

        /// <summary>
        /// Gives the string representation of the enum VBUS_TPARAM_UOM
        /// </summary>
        private string GetUOMString(VBUS_TPARAM_UOM uom)
        {
            switch (uom)
            {
                // Not applicable unit of measure.
                // For example: raw message; vehicle ID; etc
                case VBUS_TPARAM_UOM.NOT_APPLICABLE_UOM:
                    return "";

                //----------------------//
                //	Unnamed				//
                //----------------------//

                // % In percents
                case VBUS_TPARAM_UOM.PERCENTS_UOM:
                    return "%";

                // The number of something. 
                // for example: Event occurances count
                case VBUS_TPARAM_UOM.COUNT_UOM:
                    return "";

                // The level number. 
                // For example: the gear level
                case VBUS_TPARAM_UOM.LEVEL_UOM:
                    return "";

                // Number of steps
                case VBUS_TPARAM_UOM.STEP_UOM:
                    return "";

                // Ratio value
                case VBUS_TPARAM_UOM.RATIO_UOM:
                    return "";

                //------------------------------//
                //		Length,width,height		//
                //------------------------------//

                // mm (milimetre)
                case VBUS_TPARAM_UOM.MILIMETRE_UOM:
                    return "mm";

                // in (inch) length (1/36 of yd)
                case VBUS_TPARAM_UOM.INCH_UOM:
                    return "in";

                // yd (yard) length (0.9144 m)
                case VBUS_TPARAM_UOM.YARD_UOM:
                    return "yd";

                // ft (foot) length [1 ft = 0.3048 m] [1 ft = 0.3333 yd;] [1 ft = 12 in] 
	            case VBUS_TPARAM_UOM.FOOT_UOM:
                    return "ft";

                // m (metre) length
                case VBUS_TPARAM_UOM.METRE_UOM:
                    return "m";

                /// km (kilometre) length
                case VBUS_TPARAM_UOM.KILOMETRE_UOM:
                    return "km";

                // mi (mile) length (1609.344 m)(1760 yd = 5280 ft)
                case VBUS_TPARAM_UOM.MILE_UOM:
                    return "miles";

                //----------------------//
                //		Area			//
                //----------------------//

                // mm^2 square milimetre
                case VBUS_TPARAM_UOM.SQR_MILIMETRE_UOM:
                    return "mm^2";

                // in^2 square inch
                case VBUS_TPARAM_UOM.SQR_INCH_UOM:
                    return "in^2";

                // m^2 square metre
                case VBUS_TPARAM_UOM.SQR_METRE_UOM:
                    return "m^2";

                //----------------------//
                //		Volume			//
                //----------------------//

                // L (Litre) - volume
                case VBUS_TPARAM_UOM.LITRE_UOM:
                    return "L";

                // PT (Pint) - volume
                case VBUS_TPARAM_UOM.PINT_UOM:
                    return "PT";

                // gal (gallon) - volume
                case VBUS_TPARAM_UOM.GALLON_UOM:
                    return "gal";

                // m^3 cube metre
                case VBUS_TPARAM_UOM.CUBE_METRE_UOM:
                    return "m^3";

                //----------------------//
                //		Angle			//
                //----------------------//

                // Angle
                case VBUS_TPARAM_UOM.DEGREE_UOM:
                    return "deg";

                //----------------------//
                //		Mass			//
                //----------------------//

                // kg (kilogram) - mass
                case VBUS_TPARAM_UOM.KILOGRAM_UOM:
                    return "kg";

                // lb (pound) - mass
                case VBUS_TPARAM_UOM.POUND_UOM:
                    return "lb";

                //----------------------//
                //		Date-Time		//
                //----------------------//

                // ms (miliseconds)
                case VBUS_TPARAM_UOM.MILISEC_UOM:
                    return "ms";

                // s (seconds)
                case VBUS_TPARAM_UOM.SECOND_UOM:
                    return "s";

                // min (minutes)
                case VBUS_TPARAM_UOM.MINUTE_UOM:
                    return "min";

                // h (hours)
                case VBUS_TPARAM_UOM.HOUR_UOM:
                    return "h";

                // day 
                case VBUS_TPARAM_UOM.DAY_UOM:
                    return "d";

                // week
                case VBUS_TPARAM_UOM.WEEK_UOM:
                    return "wk";

                // mon (month)
                case VBUS_TPARAM_UOM.MONTH_UOM:
                    return "mon";

                // y (year)
                case VBUS_TPARAM_UOM.YEAR_UOM:
                    return "y";

                //----------------------//
                //		Temperature		//
                //----------------------//

                // Fahrenheit
                case VBUS_TPARAM_UOM.FAHRENHEIT_UOM:
                    return "Fahrenheit";

                // Celsuis
                case VBUS_TPARAM_UOM.CELSIUIS_UOM:
                    return "Celsuis";

                // K (kelvin)
                case VBUS_TPARAM_UOM.KELVIN_UOM:
                    return "K";

                //----------------------//
                //		Speed			//
                //----------------------//

                // km/h (kilometres per hour)
                case VBUS_TPARAM_UOM.KMH_UOM:
                    return "km/h";

                // mph (miles per hour)
                case VBUS_TPARAM_UOM.MPH_UOM:
                    return "mph";

                // rpm (revolutions per minute)
                case VBUS_TPARAM_UOM.RPM_UOM:
                    return "rpm";

                //----------------------//
                //		Acceleration	//
                //----------------------//

                // g (gravity) -acceleration
                case VBUS_TPARAM_UOM.GRAVITY_UOM:
                    return "g";

                //----------------------//
                //		Force			//
                //----------------------//

                // lbf (pound-force) force
                case VBUS_TPARAM_UOM.POUND_FORCE_UOM:
                    return "lbf";

                // N (newton) force
                case VBUS_TPARAM_UOM.NEWTON_UOM:
                    return "N";

                //----------------------//
                //		Pressure		//
                //----------------------//

                // psi (pounds per square inch) - pressure
                case VBUS_TPARAM_UOM.POUND_SQR_INCH_UOM:
                    return "psi";

                // kPa (KiloPascal) - pressure
                case VBUS_TPARAM_UOM.KILOPASCAL_UOM:
                    return "kPa";

                // MPa (megapascal)- pressure
                case VBUS_TPARAM_UOM.MEGAPASCAL_UOM:
                    return "MPa";

                // Pound-force inch (pressure)
                case VBUS_TPARAM_UOM.POUND_FORCE_INCH_UOM:
                    return "Pound-force inch";

                //----------------------//
                //	Electric current	//
                //----------------------//

                // Current (A)
                case VBUS_TPARAM_UOM.AMPERE_UOM:
                    return "A";

                //----------------------//
                //		Momentum		//
                //----------------------//

                // Newton-metre (N.m) torque - momentum
                case VBUS_TPARAM_UOM.NEWTON_METRE_UOM:
                    return "Nm";

                // Pound-force - foot
                case VBUS_TPARAM_UOM.POUND_FORCE_FOOT_UOM:
                    return "Pound-force - foot";

                //----------------------//
                //		Power			//
                //----------------------//

                // W (Watt) - power
                case VBUS_TPARAM_UOM.WATT_UOM:
                    return "W";

                // hp - horse power 
                case VBUS_TPARAM_UOM.HORSEPOWER_UOM:
                    return "hp";

                // kW (KiloWatt) - power
                case VBUS_TPARAM_UOM.KILOWATT_UOM:
                    return "kW";

                //----------------------//
                //		Energy(work)	//
                //----------------------//

                // kW-h (kilowatt-hour) energy or work
                case VBUS_TPARAM_UOM.KWATT_HOUR_UOM:
                    return "kWh";

                // hp-h (horse power-hour) energy or work
                case VBUS_TPARAM_UOM.HORSEPOWER_HOUR_UOM:
                    return "hph";

                //----------------------//
                //	Electromotive Force	//
                //----------------------//

                // V (volt) electromotive force
                case VBUS_TPARAM_UOM.VOLT_UOM:
                    return "V";

                //----------------------//
                //	Energy per volume	//
                //----------------------//

                // kW-hour per litre (kW-h/L) - energy economy
                case VBUS_TPARAM_UOM.KWATTHOUR_LITRE_UOM:
                    return "kWh/l";

                // hp-hour / gal (horse-power per gallon) - energy economy
                case VBUS_TPARAM_UOM.HORSEPOWERHOUR_GALLON_UOM:
                    return "hph/gal";

                //----------------------//
                //	Mass flow per time	//
                //----------------------//

                // lb/min (pound per minute)
                case VBUS_TPARAM_UOM.POUND_MIN_UOM:
                    return "lb/min";

                // kg/min (kilogram per minute)
                case VBUS_TPARAM_UOM.KILOGRAM_MIN_UOM:
                    return "kg/min";

                // lb/h (pound per hour)
                case VBUS_TPARAM_UOM.POUND_HOUR_UOM:
                    return "lb/h";

                // kg/h (kilogram per hour)
                case VBUS_TPARAM_UOM.KILOGRAM_HOUR_UOM:
                    return "kg/h";

                //----------------------//
                //	Volume per time		//
                //----------------------//

                // L/S (litres per second)
                case VBUS_TPARAM_UOM.LITRE_SECOND_UOM:
                    return "l/s";

                // gal/s (gallons per second)
                case VBUS_TPARAM_UOM.GALLON_SECOND_UOM:
                    return "gal/s";

                // L/h (litres per hour)
                case VBUS_TPARAM_UOM.LITRE_HOUR_UOM:
                    return "l/h";

                // gal/h (gallons per hour)
                case VBUS_TPARAM_UOM.GALLON_HOUR_UOM:
                    return "gal/h";


                //----------------------//
                // Distance per mass	//
                //----------------------//

                // m/lb (metre per pound) - mass economy
                case VBUS_TPARAM_UOM.METRE_POUND_UOM:
                    return "m/lb";

                // km/kg (kilogram per kilometre) - mass economy
                case VBUS_TPARAM_UOM.KILOMETRE_KILOGRAM_UOM:
                    return "km/kg";

                //----------------------//
                // Distance per volume	//
                //----------------------//

                // km/L (kilometre per litre) (fuel economy)
                case VBUS_TPARAM_UOM.KILOMETRE_LITRE_UOM:
                    return "km/l";

                // mi/gallon
                case VBUS_TPARAM_UOM.MILE_GALLON_UOM:
                    return "miles/gal";

                //----------------------//
                // Electrical Resistence//
                //----------------------//

                // Ohm
                case VBUS_TPARAM_UOM.OHM_UOM:
                    return "Ohm";

                // Mega Ohm
                case VBUS_TPARAM_UOM.MEGAOHM_UOM:
                    return "Mega Ohm";

                //----------------------//
                // Pulses per distance	//
                //----------------------//

                // pls/km (pulses per kilometre)
                case VBUS_TPARAM_UOM.PULSE_KILOMETRE_UOM:
                    return "pls/km";

                // pls/mi (pulses per mile)
                case VBUS_TPARAM_UOM.PULSE_MILE_UOM:
                    return "pls/mile";

                // RESERVED
                // Denotes last available enumeration value
                case VBUS_TPARAM_UOM.VBUS_LAST_AVAILABLE_UOM:
                    return "";

                default:
                    return "";
            }


        }
        
        /// <summary>
        /// Add an item to listViewVBus
        /// </summary>
        /// <param name="number">the number to display in Number column</param>
        /// <param name="itemName">The string to displsy in Item column</param>
        private void addListViewItem(int number, string itemName)
        {
            string[] item;
            item = new string[] { itemName, number.ToString(itemNumberFormat), Resources.GetString(itemName) };
            ListViewItem li = new ListViewItem(item);
            listViewVBus.Items.Add(li);
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
            listViewVBus.Items.Add(li);
        }
        #region Main listView

        /// <summary>
        /// Add items to the Start page of the Form
        /// </summary>
        private void loadMainListViewItems()
        {
            itemNumberFormat = "";

            int i = 0;
            addListViewItem(i++, EXITAPP);
            addListViewItem(i++, ROAD_SPEED,"");
            addListViewItem(i++, TOTAL_DISTANCE,"");
            addListViewItem(i++, ENGINE_RPM,"");
            addListViewItem(i++, VERSIONS);
            addListViewItem(i++, ABOUT);

        }

        /// <summary>
        /// Refresh the main list view after unloading it
        /// </summary>
        private void loadMainListView()
        {
            currentListView = ROOT;
            //Add column headers
            this.listViewVBus.Columns.Add(this.NameColumn);
            this.listViewVBus.Columns.Add(this.NumberColumn);
            this.listViewVBus.Columns.Add(this.ItemColumn);
            this.listViewVBus.Columns.Add(this.ValueColumn);
            this.loadMainListViewItems();
        }
        /// <summary>
        /// Unload the start window
        /// </summary>
        private void unloadMainListView()
        {
            //Remove all items
            this.listViewVBus.Clear();
            //Remove column headers
            this.listViewVBus.Columns.Remove(this.NameColumn);
            this.listViewVBus.Columns.Remove(this.NumberColumn);
            this.listViewVBus.Columns.Remove(this.ItemColumn);
            this.listViewVBus.Columns.Remove(this.ValueColumn);
        }

        #endregion 

        #region Version listView
        /// <summary>
        /// Add items to Version listView
        /// </summary>
        private void loadVersionListViewItems()
        {
            int i = 0;
            itemNumberFormat = "0";

            addListViewItem(i++, BACK, "");
            addListViewItem(i++, "CAPIVersion", capiVersion);
            addListViewItem(i++, "DriverVersion", driverVersion);

        }

        /// <summary>
        /// load Version ListView
        /// </summary>
        private void loadVersionListView()
        {
            currentListView = VERSIONS;
            //Add column headers
            this.listViewVBus.Columns.Add(this.NameColumn);
            this.listViewVBus.Columns.Add(this.NumberColumn);
            this.listViewVBus.Columns.Add(this.ItemColumn);
            this.listViewVBus.Columns.Add(this.ValueColumn);

            this.loadVersionListViewItems();
        }
        /// <summary>
        /// Unload Version listView
        /// </summary>
        private void unloadVersionListView()
        {
            //Remove all items
            this.listViewVBus.Clear();
            //Remove column headers
            this.listViewVBus.Columns.Remove(this.NameColumn);
            this.listViewVBus.Columns.Remove(this.NumberColumn);
            this.listViewVBus.Columns.Remove(this.ItemColumn);
            this.listViewVBus.Columns.Remove(this.ValueColumn);
        }
        #endregion

        /// <summary>
        /// Backtrace to the previous listView
        /// </summary>
        private void reloadLastListView()
        {
            switch (currentListView)
            {
                case VERSIONS:
                    unloadVersionListView();
                    break;
                default:
                    return;
            }

            loadMainListView();
        }

        private void listViewVBus_KeyUp(object sender, KeyEventArgs e)
        {
            this.listViewVBus.Focus();
        }

        private void listViewVBus_KeyDown(object sender, KeyEventArgs e)
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
                    if (listViewVBus.Items.Count > tmpIndex)
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
                    if (listViewVBus.Items.Count > tmpIndex)
                    {
                        selectedIndex = tmpIndex;
                        if (listViewVBus.Items.Count <= 10)
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
            if (selectedIndex <= listViewVBus.Items.Count)
            {
                //remove all previous selections
                for (int i = 0; i < listViewVBus.Items.Count; i++)
                {
                    listViewVBus.Items[i].Selected = false;
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
                listViewVBus.Items[selectedIndex].Selected = true;
                listViewVBus.Invoke(this.myActivateHandler);
            }
        }

        /// <summary>
        /// This deligate is called when a listview item is double clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewVBus_ItemActivate(object sender, System.EventArgs e)
        {
            Cursor savedCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                switch (listViewVBus.Items[listViewVBus.SelectedIndices[0]].SubItems[0].Text)
                {
                    case EXITAPP:
                        this.Close();
                        break;
                    case ROAD_SPEED:
                        break;
                    case TOTAL_DISTANCE:
                        break;
                    case ENGINE_RPM:
                        break;
                    case VERSIONS:
                        unloadMainListView();
                        loadVersionListView();
                        break;
                    case ABOUT:
                        AboutForm myAbout = new AboutForm();
                        Cursor.Current = savedCursor;
                        myAbout.ShowDialog();
                        Cursor.Current = Cursors.WaitCursor;
                        // Ensure that the keyboard focus is set on a control.
                        this.listViewVBus.Focus();
                        break;
                    case BACK:
                        reloadLastListView();
                        break;

                    default:
                        break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Windows.Forms.MessageBox.Show(Resources.GetString("ItemNotSelected"), "CS_VBusSample1");
                this.listViewVBus.Focus();
            }
            Cursor.Current = savedCursor;
        }

        private void VBusForm_Closing(object sender, CancelEventArgs e)
        {
            m_VBus.EnabledParams.ParameterReceivedNotify -= myHandler;
            m_VBus.Disable();
            m_VBus.Dispose();

        }

        private void VBusForm_Resize(object sender, EventArgs e)
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
            listViewVBus.Width = this.Width;

            // Main list
            this.NumberColumn.Width = (13 * listViewVBus.Width) / 100;
            this.ItemColumn.Width = (36 * listViewVBus.Width) / 100;
            this.ValueColumn.Width = (50 * listViewVBus.Width) / 100;
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