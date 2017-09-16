using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Symbol;
using Symbol.RFID2;
using Symbol.ResourceCoordination;
using System.Threading;

namespace CS_RFID2_Sample
{

    public partial class MainFormMobil : Form
    {
        const ulong RFID_SUCCESS = 0x0;
        const ulong RFID_TAGLOST = 0x01;
        const ulong RFID_RESP_CRC = 0x02;
        const ulong RFID_TAG_ERR = 0x03;
        const ulong RFID_OTHER_ERR = 0x00;
        const ulong RFID_MEM_OVERRUN = 0x03;
        const ulong RFID_MEM_LOCKED = 0x04;
        const ulong RFID_POWER_LOW = 0x0B;
        const ulong RFID_GENERAL_ERR = 0x0F;

        class ErrorCodes
        {
            public ulong RFID_WRITE_NOT_VERIFIED = 0x94;
            public ulong RFID_WRITE_ERASE_ERROR = 0x95;
            public ulong RFID_WRITE_LOCK_ERROR = 0x96;
            public ulong RFID_TAG_ERR_MEM_OVERRUN = 0x98;
            public ulong RFID_TAG_ERR_MEM_LOCKED = 0x99;
            public ulong RFID_TAG_ERR_LACK_POWER = 0x9A;
            public ulong RFID_TAG_ERR_NON_SPECIFIC = 0x9B;
        }

        Utilities Utils = new Utilities ();

        object displayTextLock = new object ();
        public delegate void DisplayOutputHandler (string text, ReaderEventArgs args);
        public delegate void DisplayAutoOutputHandler (TagEventArgs args);
        IRFIDReader deviceReader = null;
        private const string m_ReaderName = "";
        private ReaderModel m_ReaderModel = Program.m_ReaderModel;
        private Hashtable htInParams;
        private DisplayOutputHandler displayHandler = null;
        private DisplayAutoOutputHandler displayAutoHandler = null;
        Symbol.ResourceCoordination.Trigger MyTrigger = null;
        Symbol.ResourceCoordination.Trigger.TriggerEventHandler MyTriggerHandler = null;
        Symbol.ResourceCoordination.TriggerDevice TriggerDev = null;
        Gen2Parameters sr = new Gen2Parameters (Gen2.InventorySelectFlag.Ignore_SL, Gen2.SessionFlag.S0, Gen2.TargetFlag.Bit_A, 4);

        //##########################
        FrmSelectRecord SelRecPage = null;
        FrmGen2Read Gen2ReadPage = null;
        FrmGen2Write Gen2WritePage = null;
        FrmGen2Lock LockTagPage = null;
        FrmGen2Kill KillTagPage = null;
        //##########################

        public MainFormMobil ()
        {
            InitializeComponent ();
            try {
                displayHandler = new DisplayOutputHandler (DisplayText);
                displayAutoHandler = new DisplayAutoOutputHandler (DisplayAutoTags);
                string strPath = Assembly.GetExecutingAssembly ().ManifestModule.FullyQualifiedName;
                strPath = strPath.Replace (Assembly.GetExecutingAssembly ().ManifestModule.Name, @"DeviceReader.Config");
                string configStreamStr = string.Empty;

                try {
                    if (!File.Exists (strPath)) {
                        CreateFile (strPath);
                    }

                    StreamReader readerStream = new StreamReader (strPath);
                    Stream configStream = readerStream.BaseStream;

                    byte[] configBytes = new byte[Convert.ToInt32 (configStream.Length)];

                    configStream.Read (configBytes, 0, configBytes.Length);
                    configStreamStr = System.Text.Encoding.UTF8.GetString (configBytes, 0, configBytes.Length);

                    deviceReader = ReaderFactory.CreateReader (m_ReaderModel, configStreamStr);


                    SetupTriggerResource ();

                    SetupEvents ();

                }
                catch {
                    if (m_ReaderModel == ReaderModel.MC9090) {
                        configStreamStr = @"<?xml version='1.0' ?>
                                        <ReaderConfig xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                                        <ComPortSettings>
                                        <COMPort>COM7</COMPort>
                                        <BaudRate>57600</BaudRate>
                                        </ComPortSettings>
                                        <ReaderInfo>
                                        <Model>MC9090</Model>
                                        <StartingQ>6</StartingQ>
                                        <StartingQWrite>6</StartingQWrite>
                                        </ReaderInfo>
                                        </ReaderConfig>";
                    } else {
                        configStreamStr = @"<?xml version='1.0' ?>
                                        <ReaderConfig xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                                        <ComPortSettings>
                                        <COMPort>COM3</COMPort>
                                        <BaudRate>57600</BaudRate>
                                        </ComPortSettings>
                                        <ReaderInfo>
                                        <Model>RD5000</Model>
                                        <StartingQ>6</StartingQ>
                                        <StartingQWrite>6</StartingQWrite>
                                        </ReaderInfo>
                                        </ReaderConfig>";
                    }

                    deviceReader = ReaderFactory.CreateReader (m_ReaderModel, configStreamStr);

                    SetupTriggerResource ();

                    SetupEvents ();
                }

            }
            catch (Exception ex) {
                MessageBox.Show (ex.Message, "CS_RFID2_Sample");
                Application.Exit ();
            }

        }

        void deviceReader_TagEvent (object sender, ReaderEventArgs args)
        {
            try {
                if (args != null) {
                    listView1.Invoke (displayAutoHandler, (TagEventArgs)args);

                    if (Gen2ReadPage != null) {
                        //Gen2ReadPage.Invoke(
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show (ex.Message);
            }
        }

        public void SetupTriggerResource ()
        {
            try {                            //create a trigger object
                TriggerDev = new Symbol.ResourceCoordination.TriggerDevice (
                                 Symbol.ResourceCoordination.TriggerID.ALL_TRIGGERS,
                                (Symbol.ResourceCoordination.TriggerState[])null);

                MyTrigger = new Symbol.ResourceCoordination.Trigger (TriggerDev);

                //create an event handler and attach a handler method for trigger
                MyTriggerHandler = new Symbol.ResourceCoordination.Trigger.TriggerEventHandler (MyTriggerH);

                MyTrigger.Stage2Notify += MyTriggerHandler;
            }
            catch (Exception ex) {
                MessageBox.Show ("Failed to create Trigger: " + ex.Message, "Error");
                Shutdown ();
                this.Close ();
                return;
            }
        }
        private void SetupEvents ()
        {
            deviceReader.TagEvent += new ReaderEventHandler (deviceReader_TagEvent);
        }

        private void Shutdown ()
        {
            if (deviceReader != null) {
                deviceReader.ReadMode = ReadMode.ONDEMAND;
            }

            Thread.Sleep (50);

            if (MyTrigger != null) {
                MyTrigger.Dispose ();
            }

            if (deviceReader != null) {
                deviceReader.Disconnect ();
            }
        }



        /// <summary>
        /// DisplayAutoTags processes and displays tag data received from deviceReaderTagEvent handler
        /// </summary>

        private void DisplayAutoTagsOLD (IRFIDReader reader)
        {
            IEnumerable<IRFIDTag> Tags = reader.Tags;

            try {

                foreach (IRFIDTag itag in Tags) {
                    try 
                    {
                        if (itag == null || itag.TagID == null) {
                            return;
                        }



                        string strTagID = Utils.ProcessRtnStatus (itag);//null;

                        ListViewItem lvItem = new ListViewItem (new string[] { strTagID, 
                                                                      itag.TagType.ToString(), 
                                                                      itag.LastSeen.ToString("HH:mm:ss"),
                                                                      itag.AntennaName });
                        listView1.Items.Insert (0, lvItem).Selected = true;

                        if (listView1.Items.Count > 1024) {
                            listView1.Items.RemoveAt (1024);
                        }
                    }
                    catch (Exception ex) {
                        MessageBox.Show ("Error:" + ex.Message, "CS_RFID2_Sample");
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show ("DisplayText:" + ex.Message);
            }
        }



        private void Form1_Load (object sender, EventArgs e)
        {
            try {
                if (deviceReader.ReaderStatus != ReaderStatus.ONLINE)
                    deviceReader.Connect ();

                if (deviceReader.ReaderStatus == ReaderStatus.ONLINE)
                    ReaderConnected ();

                else
                    ReaderDisconnected ();

                InitializeParamHashtable ();


            }
            catch {
                MessageBox.Show ("Error: Reader not initialized", "CS_RFID2_Sample");
                ReaderDisconnected ();
                Application.Exit ();
            }
        }

        private void ReaderConnected ()
        {
            lblConnect.Text = "Connected";
            lblConnect.ForeColor = Color.Green;
            menuCommands.Enabled = true;
            menuReaderMode.Enabled = true;
            menuSetAntenna.Enabled = true;
            menuReaderInfo.Enabled = true;
            menuConnect.Enabled = false;
            menuDisconnect.Enabled = true;
            lblReaderMode.Text = "OnDemand";
            deviceReader.ReadMode = ReadMode.ONDEMAND;
            menuConnect.Checked = true;
            menuDisconnect.Checked = false;
            menuItem1.Enabled = true;
            menuCapability.Enabled = true;
            menuTriggerMode.Enabled = true;
            menuTriggerMode.Checked = false;
            menuItem2.Enabled = true;
        }

        private void ReaderDisconnected ()
        {
            lblConnect.Text = "Disconnected";
            lblReaderMode.Text = "";
            lblConnect.ForeColor = Color.Red;
            menuCommands.Enabled = false;
            menuReaderMode.Enabled = false;
            menuSetAntenna.Enabled = false;
            menuReaderInfo.Enabled = false;
            menuConnect.Enabled = true;
            menuDisconnect.Enabled = false;
            menuConnect.Checked = false;
            menuDisconnect.Checked = true;
            menuOnDemand.Checked = true;
            menuOnDemand.Enabled = false;
            menuAutoMode.Checked = false;
            menuAutoMode.Enabled = true;
            menuItem1.Enabled = false;
            menuCapability.Enabled = false;
            menuTriggerMode.Enabled = false;
            menuTriggerMode.Checked = false;
            menuItem2.Enabled = false;
            //            menuMotionSensor.Text = "Enable MotionSensor";
            //            menuProxSensor.Text = "Enable ProximitySensor";

        }

        private void InitializeParamHashtable ()
        {
            htInParams = new Hashtable ();
            htInParams.Add ("TagID", null);
            htInParams.Add ("TagType", null);
            htInParams.Add ("WriteData", null);

        }

        private void CreateFile (string path)
        {
            try {
                XmlTextWriter wrXmlConfig = new XmlTextWriter (@path, Encoding.UTF8);
                wrXmlConfig.WriteStartDocument ();

                wrXmlConfig.WriteStartElement ("ReaderConfig");
                wrXmlConfig.WriteAttributeString ("xmlns", "xsi", null, @"http://www.w3.org/2001/XMLSchema-instance");
                wrXmlConfig.WriteStartElement ("ComPortSettings");


                wrXmlConfig.WriteStartElement ("COMPort");

                if (m_ReaderModel == ReaderModel.MC9090) {
                    wrXmlConfig.WriteString ("COM7");
                }
                //else
                //{
                //   wrXmlConfig.WriteString("COM3");
                //}

                wrXmlConfig.WriteEndElement ();

                wrXmlConfig.WriteStartElement ("BaudRate");
                wrXmlConfig.WriteString ("57600");
                wrXmlConfig.WriteEndElement ();

                wrXmlConfig.WriteEndElement ();
                wrXmlConfig.WriteStartElement ("ReaderInfo");

                wrXmlConfig.WriteStartElement ("Model");
                if (m_ReaderModel == ReaderModel.MC9090) {
                    wrXmlConfig.WriteString ("MC9090");
                }
                //else
                //{ // not supported
                //wrXmlConfig.WriteString("MC9000");
                //}
                wrXmlConfig.WriteEndElement ();

                wrXmlConfig.WriteEndElement ();
                wrXmlConfig.WriteEndElement ();

                wrXmlConfig.WriteEndDocument ();
                wrXmlConfig.Close ();
            }
            catch (Exception ex) {
                MessageBox.Show ("Error:" + ex.Message, "CS_RFID2_Sample");
            }
        }

        private void UpdateListView (IRFIDTag Tag)
        {
            int i, len;
            try {
                string strTagID = string.Empty;
                byte[] tag_ID = Tag.TagID;

                if (tag_ID == null || tag_ID.Length == 0) {
                    return;
                }

                len = tag_ID.Length;

                for (i = 0; i < len; i++) {
                    strTagID += tag_ID[i].ToString ("X2") + " ";
                }

                ListViewItem lvItem = new ListViewItem (new string[] { strTagID, Tag.TagType.ToString (), Tag.LastSeen.ToString ("HH:mm:ss"), Tag.AntennaName });

                listView1.Items.Insert (0, lvItem);

                lvItem.Focused = true;

                lvItem.Selected = true;
            }
            catch (Exception ex) {
                MessageBox.Show ("Error:" + ex.Message, "CS_RFID2_Sample");
            }
        }

        private byte[] GetTagID ()
        {
            try {
                char[] chTagID = htInParams["TagID"].ToString ().ToCharArray ();
                ArrayList tagIdByteArr = new ArrayList ();

                for (int i = 0; i < chTagID.Length - 1; i += 2) {
                    string strTemp = new string (new char[] { chTagID[i], chTagID[i + 1] });
                    byte idByte = Convert.ToByte (strTemp, 16);
                    tagIdByteArr.Add (idByte);
                }
                return (byte[])tagIdByteArr.ToArray (typeof (byte));

            }
            catch (Exception ex) {
                MessageBox.Show ("Error:" + ex.Message, "CS_RFID2_Sample");
                return null;
            }
        }

        private void UpdateResponse (IEnumerable<IRFIDTag> tags)
        {
            try {
                byte[] tagSN = null;
                string tagIDStr = String.Empty;
                string tagDataStr = String.Empty;

                int count = 0;

                foreach (IRFIDTag newTag in tags) {

                    count++;

                    tagDataStr = String.Empty;
                    tagIDStr = String.Empty;
                    tagSN = newTag.TagID;

                    if (newTag.AccessStatus == RFID_SUCCESS) {
                        foreach (byte b in tagSN)
                            tagIDStr += b.ToString ("X2");
                    } else {
                        tagIDStr = Utils.ProcessRtnStatus (newTag);//null;
                    }

                    listView2.Items.Insert (0, new ListViewItem ());
                    listView2.Items.Insert (0, new ListViewItem (String.Format ("Antenna name:{0}", newTag.AntennaName)));
                    listView2.Items[0].ForeColor = Color.Green;
                    listView2.Items.Insert (0, new ListViewItem (String.Format ("Reader Name: {0}", newTag.ReaderName)));
                    listView2.Items[0].ForeColor = Color.Green;
                    listView2.Items.Insert (0, new ListViewItem ("Tag Type:" + newTag.TagType.ToString ()));
                    listView2.Items[0].ForeColor = Color.Green;
                    listView2.Items.Insert (0, new ListViewItem ("Tag ID:" + tagIDStr));
                    listView2.Items[0].ForeColor = Color.Green;

                    if (newTag.TagData != null) {
                        tagSN = newTag.TagData;
                        foreach (byte b in tagSN) {
                            tagDataStr += b.ToString ("X2");
                        }
                    }
                    listView2.Items.Insert (0, new ListViewItem ("Tag Data:" + tagDataStr));
                    listView2.Items[0].ForeColor = Color.Green;


                }

                if (count == 0) {
                    listView2.Items.Insert (0, new ListViewItem ());
                    listView2.Items.Insert (0, new ListViewItem ("No Tags Read"));
                    listView2.Items[0].ForeColor = Color.Red;
                    return;
                }

                listView2.Items.Insert (0, new ListViewItem ());

                if (count == 1)
                    listView2.Items.Insert (0, new ListViewItem (string.Format ("{0} Tag Found", count)));
                else
                    listView2.Items.Insert (0, new ListViewItem (string.Format ("{0} Tags Found", count)));
                listView2.Items[0].ForeColor = Color.Green;

            }
            catch (Exception ex) {
                MessageBox.Show ("Error:" + ex.Message, "CS_RFID2_Sample");
            }
        }

        private void menuGetTags_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            SendCommand ("GET TAGS");
        }

        private void SendCommand (string cmdName)
        {
            Color statusColor = Color.Green;
            //            byte[] tagID = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] tagID = new byte[128];
            IEnumerable<IRFIDTag> tagsRead = null;

            if (!VerifyOnDemand ()) {
                return;
            }

            string executionStatus = "Success";
            string errMsg = "";

            try {
                switch (cmdName) {
                    case "GET TAGS":

                        if (deviceReader.ReadMode == ReadMode.ONDEMAND) {
                            tagsRead = deviceReader.GetTags ();

                            if (tagsRead != null) {
                                executionStatus = "Success";
                            } else {
                                executionStatus = "Failed: No Tags Read!";
                            }
                        } else {
                            executionStatus = "Enter OnDemand First";
                        }
                        break;

                    //case "WRITE TAGS (CLASS1GEN2)":
                    //    deviceReader.WriteTag(genParams, lockOptions, accessPwd, wrData);
                    //    executionStatus = "Success";
                    //    break;

                    //case "WRITE TAGS (CLASS1)":
                    //    deviceReader.WriteTag(TagType.EPClass0_PLUS, lockOptions, wrData);
                    //    executionStatus = "Success";
                    //    break;

                    //case "WRITE TAGS (CLASS0+)":
                    //    deviceReader.WriteTag(TagType.EPCClass1, lockOptions, wrData);
                    //    executionStatus = "Success";
                    //    break;

                    case "GET ANTENNA NAMES":
                        deviceReader.GetAntennaNames ();
                        break;

                    case "ERASE TAGS (CLASS1GEN2)":
                        deviceReader.EraseTag (TagType.EPCClass1_GEN2);
                        executionStatus = "Success";
                        break;

                    case "ERASE TAGS (CLASS1)":
                        deviceReader.EraseTag (TagType.EPCClass1);
                        executionStatus = "Success";
                        break;

                    case "ERASE TAGS (CLASS0)":
                        deviceReader.EraseTag (TagType.EPCClass0);
                        executionStatus = "Success";
                        break;

                    case "ERASE TAGS (CLASS0+)":
                        deviceReader.EraseTag (TagType.EPClass0_PLUS);
                        executionStatus = "Success";
                        break;

                    case "KILL TAGS (CLASS0)":
                        tagID = GetTagID ();
                        deviceReader.KillTag (TagType.EPCClass0, tagID);
                        executionStatus = "Success";
                        break;

                    case "KILL TAGS (CLASS1)":
                        tagID = GetTagID ();
                        deviceReader.KillTag (TagType.EPCClass1, tagID);
                        executionStatus = "Success";
                        break;

                    case "KILL TAGS (CLASS0+)":
                        tagID = GetTagID ();
                        deviceReader.KillTag (TagType.EPClass0_PLUS, tagID);
                        executionStatus = "Success";
                        break;

                    case "KILL TAGS (CLASS1GEN2)":
                        deviceReader.KillTag (TagType.EPCClass1_GEN2, tagID);
                        executionStatus = "Success";
                        break;

                    case "LOCK TAGS (CLASS1GEN2)":
                        deviceReader.LockTag (htInParams["TagID"].ToString (), new AntennaConfig ());
                        executionStatus = "Success";
                        break;

                    case "WRITE TAG ID (CLASS1)":
                        tagID = GetTagID ();
                        deviceReader.WriteTagID (TagType.EPCClass1, tagID);
                        executionStatus = "Success";
                        break;

                    case "WRITE TAG ID (CLASS0)":
                        tagID = GetTagID ();
                        deviceReader.WriteTagID (TagType.EPCClass0, tagID);
                        executionStatus = "Success";
                        break;

                    case "WRITE TAG ID (CLASS1GEN2)":   //################################################
                        tagID = GetTagID ();
                        deviceReader.WriteTagID (TagType.EPCClass1_GEN2, tagID);
                        executionStatus = "Success";
                        break;

                    //case "ADD SELECT RECORD":

                    //  Gen2SelectRecordParameters parms = new Gen2SelectRecordParameters();

                    //  deviceReader.SelectRecordAdd(parms);

                    //  executionStatus = "Success";

                    //  break;

                    //case "REMOVE SELECT RECORD":

                    //  byte index=0;

                    //  deviceReader.SelectRecordRemove(index);

                    //  executionStatus = "Success";
                    //  break;

                    case "WRITE TAG ID (CLASS0+)":
                        tagID = GetTagID ();
                        deviceReader.WriteTagID (TagType.EPClass0_PLUS, tagID);
                        executionStatus = "Success";
                        break;

                    //case "DISABLE TRIGGER":
                    //    DisableTrigger();
                    //    executionStatus = "Success";
                    //        break;

                    //    case "ENABLE TRIGGER":
                    //        EnableTrigger();
                    //        executionStatus = "Success";
                    //        break;

                    //case "TRIGGERED MODE":
                    //        TriggerMode();
                    //        executionStatus = "Success";
                    //        break;


                    default:
                        executionStatus = "Failed";
                        statusColor = Color.Red;
                        break;
                }
            }
            catch (Exception ex) {
                executionStatus = "Failed";
                statusColor = Color.Red;
                errMsg = ex.Message;
            }

            finally {

                listView2.Items.Insert (0, new ListViewItem ());
                if (cmdName == "GET TAGS" && executionStatus == "Success")
                    UpdateResponse (tagsRead);

                UpdateCommandListView (errMsg, executionStatus, cmdName, statusColor);
            }
            tabPage2.BringToFront ();
            tabPage2.Focus ();
            tabControl1.SelectedIndex = 0;
        }

        private void menuAutoMode_Click (object sender, EventArgs e)
        {
            string executionStatus = "Success";
            string errMsg = "";
            Color statusColor = Color.Green;

            try {
                if (deviceReader.ReaderStatus == ReaderStatus.ONLINE) {
                    deviceReader.ReadMode = ReadMode.AUTONOMOUS;
                    //deviceReader.TagEvent += new ReaderEventHandler(deviceReader_TagEvent);
                    menuAutoMode.Enabled = false;
                    menuAutoMode.Checked = true;
                    menuOnDemand.Checked = false;
                    menuOnDemand.Enabled = true;
                    menuTriggerMode.Checked = false;
                    menuTriggerMode.Enabled = true;
                    lblReaderMode.Text = "Autonomous";
                }
                tabPage2.BringToFront ();
                tabPage2.Focus ();
                tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex) {
                executionStatus = "Failed";
                errMsg = ex.Message;
                tabPage1.BringToFront ();
                tabPage1.Focus ();
                tabControl1.SelectedIndex = 0;
            }
            finally {
                UpdateCommandListView (errMsg, executionStatus, "SET AUTONOMOUS MODE", statusColor);
            }
        }

        private void MyTriggerH (object sender, Symbol.ResourceCoordination.TriggerEventArgs evt)  //####
        {
            if (deviceReader.ReaderStatus == ReaderStatus.ONLINE) {
                string executionStatus = "Success";
                string errMsg = "";
                string Mode = "AUTONOMOUS MODE (via Trigger)";
                Color statusColor = Color.Green;

                try {
                    if (evt.NewState == Symbol.ResourceCoordination.TriggerState.STAGE2) {
                        deviceReader.ReadMode = Symbol.RFID2.ReadMode.AUTONOMOUS;
                        //deviceReader.ReadMode = ReadMode.TRIGGERED; Traditional Usage
                        //deviceReader.TagEvent += new ReaderEventHandler(deviceReader_TagEvent);
                        //deviceReader.TriggerPressedEvent += new ReaderEventHandler(deviceReader_TriggerPressedEvent);
                        menuAutoMode.Enabled = true;
                        menuAutoMode.Checked = false;
                        menuOnDemand.Checked = false;
                        menuOnDemand.Enabled = true;
                        menuTriggerMode.Checked = true;
                        menuTriggerMode.Enabled = false;
                        lblReaderMode.Text = "Triggering";
                        tabPage2.BringToFront ();
                        tabPage2.Focus ();
                        tabControl1.SelectedIndex = 1;
                    } else if (evt.NewState == Symbol.ResourceCoordination.TriggerState.RELEASED) {
                        Mode = "ONDEMAND";

                        deviceReader.ReadMode = Symbol.RFID2.ReadMode.ONDEMAND;

                        menuAutoMode.Enabled = true;
                        menuAutoMode.Checked = false;
                        menuOnDemand.Checked = true;
                        menuOnDemand.Enabled = false;
                        menuTriggerMode.Checked = false;
                        menuTriggerMode.Enabled = true;
                        lblReaderMode.Text = "OnDemand";
                    }
                }
                catch (Exception ex) {
                    executionStatus = "Failed";
                    errMsg = ex.Message;
                    tabPage1.BringToFront ();
                    tabPage1.Focus ();
                    tabControl1.SelectedIndex = 0;
                }
                finally {
                    UpdateCommandListView (errMsg, executionStatus, Mode.ToString (), statusColor);
                }
            }//if
        }

        private void menuOnDemand_Click (object sender, EventArgs e)
        {
            string executionStatus = "Success";
            string errMsg = "";
            Color statusColor = Color.Green;
            try {
                if (deviceReader.ReaderStatus == ReaderStatus.ONLINE) {
                    deviceReader.ReadMode = ReadMode.ONDEMAND;
                    //try Not Needed
                    //{
                    //    deviceReader.TagEvent -= new ReaderEventHandler(deviceReader_TagEvent);
                    //    deviceReader.TriggerPressedEvent -= new ReaderEventHandler(deviceReader_TriggerPressedEvent);
                    //}
                    //catch { }
                    menuAutoMode.Enabled = true;
                    menuAutoMode.Checked = false;
                    menuOnDemand.Checked = true;
                    menuOnDemand.Enabled = false;
                    menuTriggerMode.Checked = false;
                    menuTriggerMode.Enabled = true;
                    lblReaderMode.Text = "OnDemand";
                }
                tabPage1.BringToFront ();
                tabPage1.Focus ();
                tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex) {
                executionStatus = "Failed";
                errMsg = ex.Message;
            }
            finally {
                UpdateCommandListView (errMsg, executionStatus, "ON DEMAND MODE", statusColor);
            }
        }

        private void btnClear_Click (object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                listView2.Items.Clear ();
            else if (tabControl1.SelectedIndex == 1)
                listView1.Items.Clear ();
        }

        private void menuConnect_Click (object sender, EventArgs e)
        {
            try {
                deviceReader.Connect ();
                if (deviceReader.ReaderStatus == ReaderStatus.ONLINE) {
                    ReaderConnected ();
                }
            }
            catch (Exception ex) {
                MessageBox.Show ("Error:" + ex.Message, "CS_RFID2_Sample");
            }
        }

        private void menuExit_Click (object sender, EventArgs e)
        {
            try {
                deviceReader.Disconnect ();
                //pzhu deviceReader.Dispose();
                Application.Exit ();
            }
            catch (Exception ex) {
                MessageBox.Show ("Error:" + ex.Message, "CS_RFID2_Sample");
            }
        }

        private void menuDisconnect_Click (object sender, EventArgs e)
        {
            try {

                deviceReader.Disconnect ();
                if (deviceReader.ReaderStatus != ReaderStatus.ONLINE) {
                    ReaderDisconnected ();
                }
            }
            catch (Exception ex) {
                MessageBox.Show ("Error:" + ex.Message, "CS_RFID2_Sample");
            }
        }

        private void menuCapability_Click (object sender, EventArgs e)
        {
            FrmCapabilties readerCapabilies = new FrmCapabilties ();
            readerCapabilies.Capabilty = deviceReader.ReaderCapability;
            if (readerCapabilies.Capabilty != null)
                readerCapabilies.ShowDialog ();
            else
                MessageBox.Show ("Reader Capabilities not found");

        }

        private void menuReaderInfo_Click (object sender, EventArgs e)
        {
            try
            {
                FrmReadAntennaInfo antInfo = new FrmReadAntennaInfo(deviceReader.Antennas);
                antInfo.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CS_RFID2_Sample");
            }
        }


        private void menuProg_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            FrmSettings setTAGID = new FrmSettings ();
            setTAGID.currentCommand = FrmSettings.Commands.ProgramTag;

            setTAGID.ShowDialog ();
            if (setTAGID.htInParamSet != null) {
                htInParams["TagID"] = setTAGID.htInParamSet["TagID"].ToString ();
                htInParams["TagType"] = setTAGID.htInParamSet["TagType"].ToString ();

                SendCommand ("WRITE TAG ID (" + htInParams["TagType"].ToString () + ")");
            }

        }


        private void menuWrite_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            SendCommand ("WRITE TAGS (CLASS1GEN2)");
        }

        private void MainForm_Closing (object sender, CancelEventArgs e)
        {
            deviceReader.Disconnect ();
            Application.Exit ();
        }

        private void menuSetAntenna_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand()))
            {
                return;
            }

            try
            {
                AntennaConfig antConfig;
                int antennaIndex;
                FrmSetAntenna f_setAntenna = new FrmSetAntenna(deviceReader.Antennas);

                f_setAntenna.m_txMax = deviceReader.MaxTxPower;
                f_setAntenna.m_txMin = deviceReader.MinTxPower;

                f_setAntenna.ShowDialog();
                if (f_setAntenna.setAntennaConfig)
                {
                    antConfig = f_setAntenna.SetAntenna;
                    antennaIndex = f_setAntenna.ConfigAntennaIndex;
                    deviceReader.SetAntennaConfiguration(antennaIndex, antConfig);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CS_RFID2_Sample");
            }
        }

        private void menuLock_Click (object sender, EventArgs e)
        {
            try {
                if ((!VerifyOnDemand ())) {
                    return;
                }

                FrmSettings setTAGID = new FrmSettings ();
                setTAGID.currentCommand = FrmSettings.Commands.LockTag;

                setTAGID.ShowDialog ();
                if (setTAGID.htInParamSet != null) {
                    htInParams["TagID"] = setTAGID.htInParamSet["TagID"].ToString ();
                    SendCommand ("LOCK TAGS (CLASS1GEN2)");
                }

            }
            catch (Exception ex) {
                MessageBox.Show (ex.Message, "CS_RFID2_Sample");
            }
        }

        private void menuKillTags_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            FrmSettings setTAGID = new FrmSettings ();
            setTAGID.currentCommand = FrmSettings.Commands.KillTag;
            setTAGID.ShowDialog ();
            if (setTAGID.htInParamSet != null) {
                htInParams["TagID"] = setTAGID.htInParamSet["TagID"].ToString ();
                htInParams["TagType"] = setTAGID.htInParamSet["TagType"].ToString ();

                SendCommand ("KILL TAGS (" + htInParams["TagType"].ToString () + ")");
            }

        }


        private void menuEraseTag_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            FrmSettings setTAGID = new FrmSettings ();
            setTAGID.currentCommand = FrmSettings.Commands.EraseTag;
            setTAGID.ShowDialog ();
            if (setTAGID.htInParamSet != null) {
                htInParams["TagType"] = setTAGID.htInParamSet["TagType"].ToString ();

                SendCommand ("ERASE TAGS (" + htInParams["TagType"].ToString () + ")");
            }
        }

        private void menuItem1_Click (object sender, EventArgs e)
        {
            Hashtable htReaderInfo = new Hashtable ();
            htReaderInfo.Add ("DeviceSerialNumber", deviceReader.ReaderInfo.DeviceSerialNumber);
            htReaderInfo.Add ("DeviceModelNumber", deviceReader.ReaderInfo.DeviceModelNumber);
            htReaderInfo.Add ("ManufacturerName", deviceReader.ReaderInfo.ManufacturerName);
            htReaderInfo.Add ("ManufactureDate", deviceReader.ReaderInfo.ManufactureDate);
            htReaderInfo.Add ("HardwareVersion", deviceReader.ReaderInfo.HardwareVersion);
            htReaderInfo.Add ("BootLoaderVersion", deviceReader.ReaderInfo.BootLoaderVersion);
            htReaderInfo.Add ("FirmwareVersion", deviceReader.ReaderInfo.FirmwareVersion);
            htReaderInfo.Add ("SymbolSDKVersion", deviceReader.SDKVersionNumber);
            htReaderInfo.Add ("Model", deviceReader.Model.ToString ());

            FrmReaderInfo reader = new FrmReaderInfo (htReaderInfo);
            reader.ShowDialog ();
        }

        void deviceReader_TriggerPressedEvent (object sender, ReaderEventArgs args)
        {
            try {
                lblTrig.Invoke (displayHandler, new object[] { "TriggerState", args });
            }
            catch (Exception ex) {
                MessageBox.Show ("Error in Trigger event: " + ex.Message, "CS_RFID2_Sample");
            }
        }

        private void menuItem2_Click (object sender, EventArgs e)
        {
            string executionStatus = "Success";
            string errMsg = "";
            Color statusColor = Color.Green;

            try {
                if (deviceReader.ReaderStatus == ReaderStatus.ONLINE) {
                    //deviceReader.ReadMode = ReadMode.TRIGGERED;
                    //deviceReader.TagEvent += new ReaderEventHandler(deviceReader_TagEvent);
                    //deviceReader.TriggerPressedEvent += new ReaderEventHandler(deviceReader_TriggerPressedEvent);
                    menuAutoMode.Enabled = true;
                    menuAutoMode.Checked = false;
                    menuOnDemand.Checked = false;
                    menuOnDemand.Enabled = true;
                    menuTriggerMode.Checked = true;
                    menuTriggerMode.Enabled = false;
                    lblReaderMode.Text = "Use Trigger";
                    if (deviceReader.ReadMode == ReadMode.AUTONOMOUS) {
                        deviceReader.ReadMode = ReadMode.ONDEMAND;
                    }
                }
                tabPage2.BringToFront ();
                tabPage2.Focus ();
                tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex) {
                executionStatus = "Failed";
                errMsg = ex.Message;

                tabPage1.BringToFront ();
                tabPage1.Focus ();
                tabControl1.SelectedIndex = 0;
            }
            finally {
                UpdateCommandListView (errMsg, executionStatus, "ONDEMAND MODE", statusColor);
            }
        }

        private void UpdateCommandListView (string errMsg, string executionStatus,string command, Color statusColor)
        {
            listView2.Items.Insert (0, new ListViewItem ());
            listView2.Items.Insert (0, new ListViewItem (errMsg));
            listView2.Items[0].ForeColor = statusColor;
            listView2.Items.Insert (0, new ListViewItem ("Execution Status:" + executionStatus));
            listView2.Items[0].ForeColor = statusColor;
            listView2.Items.Insert (0, new ListViewItem ("Response:"));
            listView2.Items[0].ForeColor = statusColor;
            listView2.Items.Insert (0, new ListViewItem ("Command Sent:" + command));
            listView2.Items[0].ForeColor = Color.Blue;

        }
        //private void menuItem2_Click_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        SendCommand("ENABLE TRIGGER"); 
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error in Enable Trigger" + ex.Message, "CS_RFID2_Sample");
        //    }
        //}
        //private void EnableTrigger()
        //{
        //    deviceReader.EnableTrigger();
        //    deviceReader.TriggerPressedEvent += new ReaderEventHandler(deviceReader_TriggerPressedEvent);
        //    tabPage3.BringToFront();
        //    tabPage3.Focus();
        //    tabControl1.SelectedIndex = 2;
        //}

        //private void menuItem3_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        SendCommand("DISABLE TRIGGER"); 
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error in Disable Trigger" + ex.Message, "CS_RFID2_Sample");
        //    }
        //}

        //private void DisableTrigger()
        //{
        //    deviceReader.DisableTrigger();
        //    deviceReader.TriggerPressedEvent -= new ReaderEventHandler(deviceReader_TriggerPressedEvent);
        //    tabPage3.BringToFront();
        //    tabPage3.Focus();
        //    tabControl1.SelectedIndex = 2;
        //}

        private void DisplayText (string text, ReaderEventArgs args)
        {
            try {

                lock (displayTextLock) 
                {
                    if (text == "Tags Read") {
                        if (args == null)
                            return;
                        TagEventArgs tagArgs = (TagEventArgs)args;
                        IEnumerable<IRFIDTag> tags = tagArgs.Reader.Tags;

                        foreach (IRFIDTag tag in tags) {
                            UpdateListView (tag);
                        }

                    }
                    if (text == "TriggerState") {
                        Symbol.RFID2.TriggerEventArgs trigArgs = (Symbol.RFID2.TriggerEventArgs)args;
                        lblTrig.Text = trigArgs.TriggerState.ToString ();

                    }

                }
            }
            catch (Exception ex) {
                MessageBox.Show ("DisplayText:" + ex.Message);
            }
        }

        private void menuItem2_Click_1 (object sender, EventArgs e)
        {
            ReaderSettings readerSettings = new ReaderSettings();
            //pzhu readerSettings.StartingQ = deviceReader.StartinqQ;
            readerSettings.StartingQ = deviceReader.Gen2Settings[0].StartingQ;

            if (readerSettings.ShowDialog() == DialogResult.OK)
            {

                foreach (Gen2Parameters tempParameters in deviceReader.Gen2Settings)
                {
                    tempParameters.StartingQ = readerSettings.StartingQ;
                }
                //pzhu deviceReader.StartinqQ = readerSettings.StartingQ;
            }

        }


        private void menuReadData_Click (object sender, EventArgs e)
        {
            FrmReadData read = new FrmReadData (false);
            string errMsg = "";
            string executionStatus = "Success";
            Color statusColor = Color.Green;
            IEnumerable<IRFIDTag> tagsRead = null;
            if (read.ShowDialog () == DialogResult.OK) {
                try {
                    TagDataLoc dataLoc = new TagDataLoc (read.wordPointer,
                                            read.wordCount, (Gen2.MemoryBank)read.memBank);
                    tagsRead = deviceReader.GetTags (dataLoc, read.gen2Params, read.accessPassword);

                }
                catch (Exception ex) {
                    errMsg = ex.Message;
                    statusColor = Color.Red;
                    executionStatus = "Failed";
                }
                finally {
                    UpdateResponse (tagsRead);
                    UpdateCommandListView (errMsg, executionStatus, "Read Data", statusColor);
                }
            }
        }

        private void menuWriteTag_Click (object sender, EventArgs e)
        {
            FrmReadData read = new FrmReadData (true);
            string errMsg = "";
            string executionStatus = "Success";
            Color statusColor = Color.Green;

            if ((!VerifyOnDemand ())) {
                return;
            }

            if (read.ShowDialog () == DialogResult.OK) {
                try {
                    char[] chTagID = read.writeData.ToCharArray ();

                    ArrayList tagIdByteArr = new ArrayList ();

                    for (int i = 0; i < chTagID.Length - 1; i += 2) {
                        string strTemp = new string (new char[] { chTagID[i], chTagID[i + 1] });
                        byte idByte = Convert.ToByte (strTemp, 16);
                        tagIdByteArr.Add (idByte);
                    }

                    byte[] wrData = (byte[])tagIdByteArr.ToArray (typeof (byte));

                    TagDataLoc dataLoc = new TagDataLoc (read.wordPointer,
                                                    (byte)(wrData.Length / 2), (Gen2.MemoryBank)read.memBank);

                    bool tagsWritten = deviceReader.WriteTag (read.gen2Params, dataLoc, read.accessPassword, wrData);

                    if (!tagsWritten) {
                        errMsg = "Could not write tags ";
                        statusColor = Color.Red;
                        executionStatus = "Failed";
                    }
                }
                catch (Exception ex) {
                    errMsg = ex.Message;
                    statusColor = Color.Red;
                    executionStatus = "Failed";
                }
                finally {
                    UpdateCommandListView (errMsg, executionStatus, "Write Data", statusColor);
                }
            }
        }


        //######################## GEN2 READ ########################
        //######################## GEN2 READ ########################

        private void menuGen2Read_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            MyTrigger.Stage2Notify -= MyTriggerHandler;

            Gen2ReadPage = new FrmGen2Read (ref deviceReader);

            if (Gen2ReadPage.ShowDialog () == DialogResult.OK) {
            }

            SetupTriggerResource ();
        }



        //######################## GEN2 WRITE ########################
        //######################## GEN2 WRITE ########################

        private void menuGen2Write_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            MyTrigger.Stage2Notify -= MyTriggerHandler;

            Gen2WritePage = new FrmGen2Write (ref deviceReader);

            if (Gen2WritePage.ShowDialog () == DialogResult.OK) {
            }
            SetupTriggerResource ();
        }


        //######################## SELECT RECORD ########################
        //######################## SELECT RECORD ########################

        private void menuGen2SelRec_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            MyTrigger.Stage2Notify -= MyTriggerHandler;

            SelRecPage = new FrmSelectRecord (ref deviceReader);

            if (SelRecPage.ShowDialog () == DialogResult.OK) {
            }
            SetupTriggerResource ();
        }


        //######################## GEN2 LOCK ########################
        //######################## GEN2 LOCK ########################

        private void menuGen2Lock_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            MyTrigger.Stage2Notify -= MyTriggerHandler;

            LockTagPage = new FrmGen2Lock (ref deviceReader);

            if (LockTagPage.ShowDialog () == DialogResult.OK) {
            }
            SetupTriggerResource ();
        }


        //######################## GEN2 KILL ########################
        //######################## GEN2 KILL ########################

        private void menuGen2Kill_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            MyTrigger.Stage2Notify -= MyTriggerHandler;

            KillTagPage = new FrmGen2Kill (ref deviceReader);

            if (KillTagPage.ShowDialog () == DialogResult.OK) {
            }
            SetupTriggerResource ();
        }



        //menuGen2SelRec



        bool VerifyOnDemand ()
        {
            if (deviceReader.ReaderStatus == ReaderStatus.ONLINE) {
                if (deviceReader.ReadMode != ReadMode.ONDEMAND) {
                    MessageBox.Show ("Exit Autnonmous Mode");
                    return (false);
                } else {
                    return (true);
                }
            }
            return (false);
        }



        /// <summary>
        /// DisplayAutoTags processes and displays tag data received from deviceReaderTagEvent handler
        /// </summary>

        private void DisplayAutoTags (TagEventArgs args)
        {
            int j, ln;
            TagEventArgs TagArgs = null;
            IEnumerable<IRFIDTag> Tags = null;
            byte[] tag_ID = null;

            int count = 0;

            TagArgs = args;

            Tags = TagArgs.Reader.Tags;

            try {


                foreach  (IRFIDTag tag in Tags) {

                    if (++count % 50 == 0)
                    {
                        System.Windows.Forms.Application.DoEvents();  // give gui thread a chance to response user operation

                    }

                    tag_ID = null;

                    tag_ID = tag.TagID;

                    try {
                        if (tag_ID == null || tag_ID.Length == 0) {
                            return;
                        }

                        string strTagID = null;

                        ln = tag_ID.Length;

                        for (j = 0; j < ln; j++) {
                            strTagID += tag_ID[j].ToString ("X2") + " ";
                        }

                        ListViewItem lvItem = new ListViewItem (new string[] { strTagID, 
                                                                    tag.TagType.ToString(), 
                                                                    tag.LastSeen.ToString("HH:mm:ss"),
                                                                    tag.AntennaName });
                        listView1.Items.Insert (0, lvItem).Selected = true;

                        if (listView1.Items.Count > 1024) {
                            listView1.Items.RemoveAt (1024);
                        }
                    }
                    catch (Exception ex) {
                        MessageBox.Show ("Error:" + ex.Message, "CS_RFID2_Sample");
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show ("DisplayText:" + ex.Message);
            }
        }

    }//cls


    //########################################### unify these utilities into one package ################################
    //########################################### unify these utilities into one package ################################

    public class Utilities
    {
        const ulong RFID_SUCCESS = 0x0;
        const ulong RFID_TAGLOST = 0x01;
        const ulong RFID_RESP_CRC = 0x02;
        const ulong RFID_TAG_ERR = 0x03;
        const ulong RFID_OTHER_ERR = 0x00;
        const ulong RFID_MEM_OVERRUN = 0x03;
        const ulong RFID_MEM_LOCKED = 0x04;
        const ulong RFID_POWER_LOW = 0x0B;
        const ulong RFID_GENERAL_ERR = 0x0F;
        const ulong RFID_NOT_ATTEMPTED = 0xFF;  // used by Inventory command ( added by Ray )


        public String ProcessRtnStatus (IRFIDTag t)
        {
            int n = 0;
            String nll = "Empty";
            StringBuilder arg = new StringBuilder ();

            try {
                if (t == null) {
                    return (nll);
                }

                string epc = string.Empty;
                //string s = string.Empty;

                t.AccessStatus.ToString ();

                switch (t.AccessStatus) {
                    case RFID_SUCCESS:
                    case RFID_NOT_ATTEMPTED:  // for Gen2 Inventory, it always returns tag's AccessStatus with this value  // added by Ray 
                        epc = arg.Append (Textify (t.TagID)).ToString ();    //contains the tag ID used in the command

                        if (t.TagData != null && t.TagData.Length > 0) {
                            epc += ":" + Textify (t.TagData);
                        }

                        break;

                    case RFID_TAGLOST:
                        epc = "TAG LOST, ";

                        epc += arg.Append (Textify (t.TagID));    //contains the tag ID used in the command

                        if (t.TagData != null && t.TagData.Length > 0) {
                            epc += ":" + Textify (t.TagData);
                        }

                        break;

                    case RFID_RESP_CRC:
                        epc = "RESP CRC ERR, ";

                        epc += arg.Append (Textify (t.TagID));    //contains the tag ID used in the command

                        if (t.TagData != null && t.TagData.Length > 0) {
                            epc += ":" + Textify (t.TagData);
                        }

                        break;

                    case RFID_TAG_ERR:

                        epc = "Tag Error, " + ReturnCodeStr (t);

                        break;

                    default: epc = "Unknown, no data available"; break;
                }

                ++n;

                // String msg = n.ToString () + ", " + epc.ToString ();
                //return (msg);

                return epc.ToString();

            }//try
            catch {
                return (nll);
            }
        }//meth

        public StringBuilder ReturnCodeStr (IRFIDTag t)
        {
            StringBuilder arg = new StringBuilder ();
            if (t.TagData != null && t.TagData.Length == 1) {
                switch ((ulong)t.TagData[0]) {
                    case RFID_OTHER_ERR:
                        arg = arg.Append ("Other_error, ");
                        break;

                    case RFID_MEM_OVERRUN:
                        arg = arg.Append ("Memory_Overrun, ");
                        break;

                    case RFID_MEM_LOCKED:
                        arg = arg.Append ("Memory_Locked, ");
                        break;
                    case RFID_POWER_LOW:
                        arg = arg.Append ("Power Too Low, ");
                        break;

                    case RFID_GENERAL_ERR:
                        arg = arg.Append ("General Error, ");
                        break;

                    default:
                        arg = arg.Append ("Unkown Error, ");
                        break;
                }

                if (t.TagID != null) {
                    arg.Append (Textify (t.TagID));    //contains the tag ID used in the command
                }
            } else {
                foreach (char c in t.TagID) {
                    arg.Append (System.Convert.ToChar (c));
                }

            }

            return (arg);

        }


        public byte[] Hexify (String hexstr)// half byte capable
        {
            int cnt;
            int i, x;
            byte[] bytes = null;

            if (hexstr != null) {
                cnt = (hexstr.Length / 2) * 2;

                x = hexstr.Length / 2 + hexstr.Length % 2;

                bytes = new byte[x];

                for (i = 0; i < cnt; i += 2) {
                    bytes[i / 2] = Convert.ToByte (hexstr.Substring (i, 2), 16);
                }

                if (hexstr.Length % 2 > 0) {
                    bytes[x - 1] = (byte)((Convert.ToInt16 (hexstr.Substring (i, 1), 16)) << 4);
                }
            }
            return (bytes);
        }



        public String Textify (byte[] bytes)
        {
            int i, len;
            String str = new String (' ', 0);

            if (bytes == null) {
                bytes = new byte[1];
            }

            len = bytes.Length;

            for (i = 0; i < len; i++) {
                str += bytes[i].ToString ("X2");
            }

            return (str);
        }


        //#############################################################################################################33


    }

}
