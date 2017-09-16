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

using Symbol.RFID2;


namespace CS_RFID2_Sample
{
    public partial class MainForm : Form
    {

        //##########################
        FrmSelectRecord SelRecPage = null;
        FrmGen2Read Gen2ReadPage = null;
        FrmGen2Write Gen2WritePage = null;
        FrmGen2Lock LockTagPage = null;
        FrmGen2Kill KillTagPage = null;
        //##########################

        IRFIDReader deviceReader = null;
        private const string m_ReaderName = "";
        private const ReaderModel m_ReaderModel = ReaderModel.RD5000;
        private Hashtable htInParams;
        public MainForm ()
        {
            InitializeComponent ();
            try {
                string strPath = Assembly.GetExecutingAssembly ().ManifestModule.FullyQualifiedName;
                //strPath = strPath.Replace(Assembly.GetExecutingAssembly().ManifestModule.Name, @"Data\ReaderConfig\SymbolReaderApex.Config");
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
                }
                catch {
                    configStreamStr = @"<?xml version='1.0' ?>
                                        <ReaderConfig xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                                            <ComPortSettings>
                                            <COMPort>COM7</COMPort>
                                            <BaudRate>57600</BaudRate>
                                            </ComPortSettings>
                                            </ReaderConfig>
                                            <ReaderInfo>
	                                    	<Model>RD5000</Model>
		                                    <StartingQ>6</StartingQ>
		                                    <StartingQWrite>6</StartingQWrite>  
	                                        </ReaderInfo>";

                    deviceReader = ReaderFactory.CreateReader (m_ReaderModel, configStreamStr);
                }

                deviceReader.TagEvent += new ReaderEventHandler (deviceReader_TagEvent);
            }
            catch (Exception ex) {
                MessageBox.Show (ex.Message, "CS_RFID2_Sample");
                Application.Exit ();
            }

        }

        void deviceReader_TagEvent (object sender, ReaderEventArgs args)
        {
            if (InvokeRequired)
                Invoke (new ReaderEventHandler (deviceReader_TagEvent), new object[] { this, args });
            else {
                TagEventArgs tagArgs = (TagEventArgs)args;
                IEnumerable<IRFIDTag> Tags = tagArgs.Reader.Tags;
                //####UpdateListView(Tags[0]);
                UpdateListView (Tags);
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
            menuItem2.Enabled = false;
            foreach (Control ctrl in tabPage3.Controls) {
                if (ctrl.GetType () == typeof (TextBox)) {
                    ctrl.Enabled = false;
                    ((TextBox)ctrl).Text = string.Empty;
                }
                if (ctrl.GetType () == typeof (ProgressBar)) {
                    ctrl.Enabled = false;
                    ((ProgressBar)ctrl).Value = 0;
                }
            }
            menuMotionSensor.Text = "Enable MotionSensor";
            menuProxSensor.Text = "Enable ProximitySensor";

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
                wrXmlConfig.WriteString ("COM7");
                wrXmlConfig.WriteEndElement ();

                wrXmlConfig.WriteStartElement ("BaudRate");
                wrXmlConfig.WriteString ("57600");
                wrXmlConfig.WriteEndElement ();


                wrXmlConfig.WriteEndElement ();
                wrXmlConfig.WriteStartElement ("ReaderInfo");

                wrXmlConfig.WriteStartElement ("Model");
                wrXmlConfig.WriteString ("RD5000");
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

        private void UpdateListView (IEnumerable<IRFIDTag> Tag)
        {
            try {
                IRFIDTag aTag;
                string strTagID = string.Empty;

                int count = 0;

                foreach (IRFIDTag tag in Tag)//####
              {
                  if (++count % 50 == 0)
                  {
                      System.Windows.Forms.Application.DoEvents();  // give gui thread a chance to response user operation

                  }
                    aTag = tag;

                    byte[] tag_ID = aTag.TagID;
                    strTagID = string.Empty;

                    foreach (byte b in tag_ID) {
                        strTagID += b.ToString ("X2") + " ";
                    }

                    ListViewItem lvItem = new ListViewItem (new string[] { strTagID, aTag.TagType.ToString (), aTag.LastSeen.ToString ("HH:mm:ss"), aTag.AntennaName });
                    listView1.Items.Insert (0, lvItem);
                    lvItem.Focused = true;
                    lvItem.Selected = true;
                }
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

                    foreach (byte b in tagSN)
                        tagIDStr += b.ToString("X2");
                   

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
                        foreach (byte b in tagSN)
                            tagDataStr += b.ToString ("X2");

                        listView2.Items.Insert (0, new ListViewItem ("Tag Data:" + tagDataStr));
                        listView2.Items[0].ForeColor = Color.Green;
                    }

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
            if ((!VerifyOnDemand()))
            {
                return;
            }

            SendCommand ("GET TAGS");
        }

        private void SendCommand (string cmdName)
        {
            Color statusColor = Color.Green;
            byte[] tagID = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            IEnumerable<IRFIDTag> tagsRead = null;

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
                        //deviceReader.KillTag(TagType.EPClass0_PLUS, tagID, passCode);
                        deviceReader.KillTag (TagType.EPClass0_PLUS, tagID);
                        executionStatus = "Success";
                        break;

                    case "KILL TAGS (CLASS1GEN2)":
                        //deviceReader.KillTag(genParams, 1);// passCode);
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

                    case "WRITE TAG ID (CLASS1GEN2)":
                        tagID = GetTagID ();
                        deviceReader.WriteTagID (TagType.EPCClass1_GEN2, tagID);
                        executionStatus = "Success";
                        break;


                    case "WRITE TAG ID (CLASS0+)":
                        tagID = GetTagID ();
                        deviceReader.WriteTagID (TagType.EPClass0_PLUS, tagID);
                        executionStatus = "Success";
                        break;

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
            if (deviceReader.ReaderStatus == ReaderStatus.ONLINE) {
                menuAutoMode.Enabled = false;
                menuAutoMode.Checked = true;
                menuOnDemand.Checked = false;
                menuOnDemand.Enabled = true;
                lblReaderMode.Text = "Autonomous";
                deviceReader.ReadMode = ReadMode.AUTONOMOUS;
            }
            tabPage1.BringToFront ();
            tabPage1.Focus ();
            tabControl1.SelectedIndex = 1;
        }

        private void menuOnDemand_Click (object sender, EventArgs e)
        {
            if (deviceReader.ReaderStatus == ReaderStatus.ONLINE) {
                menuAutoMode.Enabled = true;
                menuAutoMode.Checked = false;
                menuOnDemand.Checked = true;
                menuOnDemand.Enabled = false;
                lblReaderMode.Text = "OnDemand";
                deviceReader.ReadMode = ReadMode.ONDEMAND;
            }


            tabPage2.BringToFront ();
            tabPage2.Focus ();
            tabControl1.SelectedIndex = 0;
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
                try {
                    deviceReader.MotionSensorEvent -= new ReaderEventHandler
                                                        (deviceReader_MotionSensorEvent);
                }
                catch { }
                try {
                    deviceReader.ProximitySensorEvent -= new ReaderEventHandler
                                                        (deviceReader_ProximitySensorEvent);
                }
                catch { }
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
            readerCapabilies.ShowDialog ();
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

        //private void menuWrite_Click(object sender, EventArgs e)
        //{
        //    SendCommand("WRITE TAGS (CLASS1GEN2)");
        //}

        private void menuProg_Click (object sender, EventArgs e)
        {
            FrmSettings setTAGID = new FrmSettings ();
            setTAGID.currentCommand = FrmSettings.Commands.ProgramTag;

            setTAGID.ShowDialog ();
            if (setTAGID.htInParamSet != null) {
                htInParams["TagID"] = setTAGID.htInParamSet["TagID"].ToString ();
                htInParams["TagType"] = setTAGID.htInParamSet["TagType"].ToString ();

                SendCommand ("WRITE TAG ID (" + htInParams["TagType"].ToString () + ")");
            }
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

        private void menuMotionSensor_Click (object sender, EventArgs e)
        {
            string executionStatus = "Success";
            string errMsg = "";
            string cmdName = "";
            Color statusColor = Color.Green;
            try {
                if (menuMotionSensor.Text == "Enable MotionSensor") {
                    deviceReader.MotionSensorEvent += new ReaderEventHandler (deviceReader_MotionSensorEvent);

                    deviceReader.EnableMotionSensor (500);
                    cmdName = menuMotionSensor.Text;
                    menuMotionSensor.Text = "Disable MotionSensor";

                    xmotionprogBar.Enabled = true;
                    ymotionProgBar.Enabled = true;
                    zMotionProgBar.Enabled = true;
                    txtXMotion.Enabled = true;
                    txtYMotion.Enabled = true;
                    txtZMotion.Enabled = true;

                } else if (menuMotionSensor.Text == "Disable MotionSensor") {
                    deviceReader.MotionSensorEvent -= new ReaderEventHandler (deviceReader_MotionSensorEvent);
                    deviceReader.DisableMotionSensor ();

                    cmdName = menuMotionSensor.Text;
                    menuMotionSensor.Text = "Enable MotionSensor";
                    xmotionprogBar.Value = 0;
                    ymotionProgBar.Value = 0;
                    zMotionProgBar.Value = 0;
                    txtXMotion.Text = string.Empty;
                    txtYMotion.Text = string.Empty;
                    txtZMotion.Text = string.Empty;
                    xmotionprogBar.Enabled = false;
                    ymotionProgBar.Enabled = false;
                    zMotionProgBar.Enabled = false;
                    txtXMotion.Enabled = false;
                    txtYMotion.Enabled = false;
                    txtZMotion.Enabled = false;

                }

                tabPage3.BringToFront ();
                tabPage3.Focus ();
                tabControl1.SelectedIndex = 2;

            }
            catch (Exception ex) {
                MessageBox.Show (ex.Message);
                statusColor = Color.Red;
                errMsg = ex.Message;
                executionStatus = "Failed";
            }
            finally {
                UpdateCommandListView (errMsg, executionStatus, cmdName, statusColor);
            }
        }

        void deviceReader_MotionSensorEvent (object sender, ReaderEventArgs args)
        {
            if (InvokeRequired)
                Invoke (new ReaderEventHandler (deviceReader_MotionSensorEvent), new object[] { this, args });

            MotionEventArgs motionArgs = (MotionEventArgs)args;
            txtXMotion.Text = motionArgs.XMotion.ToString ();
            txtYMotion.Text = motionArgs.YMotion.ToString ();
            txtZMotion.Text = motionArgs.ZMotion.ToString ();
            try {
                xmotionprogBar.Value = (int)motionArgs.XMotion;
                ymotionProgBar.Value = (int)motionArgs.YMotion;
                zMotionProgBar.Value = (int)motionArgs.ZMotion;
            }
            catch {

            }
        }

        private void menuProxSensor_Click (object sender, EventArgs e)
        {
            string executionStatus = "Success";
            string errMsg = "";
            string cmdName = "";
            Color statusColor = Color.Green;

            try {

                if (menuProxSensor.Text == "Enable ProximitySensor") {
                    deviceReader.ProximitySensorEvent += new ReaderEventHandler (deviceReader_ProximitySensorEvent);

                    deviceReader.EnableProximitySensor (500);
                    cmdName = menuProxSensor.Text;
                    menuProxSensor.Text = "Disable ProximitySensor";
                    proximityProgbar.Enabled = true;
                    txtProximity.Enabled = true;
                } else if (menuProxSensor.Text == "Disable ProximitySensor") {
                    deviceReader.DisableProximitySensor ();
                    cmdName = menuProxSensor.Text;
                    menuProxSensor.Text = "Enable ProximitySensor";
                    proximityProgbar.Value = 0;
                    txtProximity.Text = string.Empty;
                    proximityProgbar.Enabled = false;
                    txtProximity.Enabled = false;
                }

                proximityProgbar.Refresh ();
                tabPage3.BringToFront ();
                tabPage3.Focus ();

                tabControl1.SelectedIndex = 2;
            }
            catch (Exception ex) {
                MessageBox.Show (ex.Message);
                errMsg = ex.Message;
                executionStatus = "Failed";
                statusColor = Color.Red;
            }
            finally {
                UpdateCommandListView (errMsg, executionStatus, cmdName, statusColor);
            }

        }

        void deviceReader_ProximitySensorEvent (object sender, ReaderEventArgs args)
        {

            if (InvokeRequired)
                Invoke (new ReaderEventHandler (deviceReader_ProximitySensorEvent), new object[] { this, args });

            ProximityEventArgs proxArgs = (ProximityEventArgs)args;
            txtProximity.Text = proxArgs.Proximity.ToString ();
            proximityProgbar.Value = (int)proxArgs.Proximity;
        }

        private void UpdateCommandListView (string errMsg, string executionStatus, string command, Color statusColor)
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

        private void menuItem2_Click (object sender, EventArgs e)
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



        //#########################################################
        //#########################################################

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


        //######################## GEN2 READ ########################
        //######################## GEN2 READ ########################

        private void menuGen2Read_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            Gen2ReadPage = new FrmGen2Read (ref deviceReader);

            if (Gen2ReadPage.ShowDialog () == DialogResult.OK) {
            }

        }

        //######################## GEN2 WRITE ########################
        //######################## GEN2 WRITE ########################

        private void menuGen2Write_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            Gen2WritePage = new FrmGen2Write (ref deviceReader);

            if (Gen2WritePage.ShowDialog () == DialogResult.OK) {
            }
        }


        //######################## SELECT RECORD ########################
        //######################## SELECT RECORD ########################

        private void menuGen2SelRec_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            SelRecPage = new FrmSelectRecord (ref deviceReader);

            if (SelRecPage.ShowDialog () == DialogResult.OK) {
            }

        }


        //######################## GEN2 LOCK ########################
        //######################## GEN2 LOCK ########################

        private void menuGen2Lock_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            LockTagPage = new FrmGen2Lock (ref deviceReader);

            if (LockTagPage.ShowDialog () == DialogResult.OK) {
            }
        }


        //######################## GEN2 KILL ########################
        //######################## GEN2 KILL ########################

        private void menuGen2Kill_Click (object sender, EventArgs e)
        {
            if ((!VerifyOnDemand ())) {
                return;
            }

            KillTagPage = new FrmGen2Kill (ref deviceReader);

            if (KillTagPage.ShowDialog () == DialogResult.OK) {
            }
        }

    }
}

