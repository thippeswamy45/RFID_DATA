using System;
using System.Collections;
using Symbol.RFID2;
using System.Runtime.InteropServices;
using System.Text;

namespace CS_RFID2_Host_Sample
{
    internal delegate void AppReaderEventHandler(object sender, ReaderEventArgs args);
    
	/// <summary>
	/// Summary description for ClsWTReader.
	/// </summary>
	public class ClsReader : IDisposable
	{
        internal event AppReaderEventHandler appOnTagRead = null;
        internal event AppReaderEventHandler appOnPinStatusNotification = null;
        internal event AppReaderEventHandler appOnReaderManagement = null;
        internal event AppReaderEventHandler appOnProximitySensorEvent = null;
        internal event AppReaderEventHandler appOnMotionSensorEvent = null;
        internal event AppReaderEventHandler appOnRFIDStatusMonitor = null;
        private ReaderEventHandler onTagRead = null;

        private ReaderEventHandler onPinStatusNotification = null;

        private ReaderEventHandler onReaderManagement = null;

        private ReaderEventHandler onRfidStatusMonitor = null;
        private ReaderEventHandler onProximitySensorEvent = null;
        private ReaderEventHandler onMotionSensorEvent = null;

        private IRFIDReader  objReader = null;

        //public static SymTagMask getTagMask = new SymTagMask(new byte[] {0},0,-1);
        #region dummy antenna
        internal AntennaConfig[] dummyAntennae = new AntennaConfig[3];
        // dummy parameter for antennas
        internal AntennaConfig[] SetDummyAntenna()
        {
            
            ArrayList tagTypes = new ArrayList();
            tagTypes.Add(TagType.EPCClass1_GEN2);
            uint tx = 140;
            uint rx = 0;
            ArrayList tagTypes1 = new ArrayList();
            tagTypes1.Add(TagType.EPCClass0);
            tagTypes1.Add(TagType.EPCClass1);
            //AntennaConfig TestAntenna1 = new AntennaConfig("TestAntenna1", tagTypes, tx, rx, true);
            dummyAntennae[0] = new AntennaConfig("TestAntenna1", (TagType[])tagTypes1.ToArray(typeof(TagType)), tx, rx, true);
            tx = 240;
            //AntennaConfig TestAntenna2 = new AntennaConfig("TestAntenna2", tagTypes1, tx, rx, true);
            dummyAntennae[1] = new AntennaConfig("TestAntenna2", (TagType[])tagTypes1.ToArray(typeof(TagType)), tx, rx, true);

            ArrayList tagTypes3 = new ArrayList();
            tagTypes3.Add(TagType.EPCClass1_GEN2);
            tagTypes3.Add(TagType.EPCClass1);
            tx = 0;
            dummyAntennae[2] = new AntennaConfig("TestAntenna3", (TagType[])tagTypes3.ToArray(typeof(TagType)), tx, rx, false);
            return dummyAntennae;
        }


        internal string[] GetDummyAntennaNames()
        {
            if (dummyAntennae == null || dummyAntennae.Length == 0)
                throw new ReaderException("Antennas not intialized");

            int antCnt = dummyAntennae.Length;
            string[] antennaNames = new string[antCnt];

            for (int i = 0; i < antCnt; i++)
            {
                antennaNames[i] = dummyAntennae[i].AntennaName;

            }

            return antennaNames;

        }
        internal AntennaConfig GetDummyAntennaConfiguration(string antennaName)
        {
            if (dummyAntennae == null || dummyAntennae.Length == 0)
                throw new ReaderException("Antennas not intialized");

            foreach (AntennaConfig antenna in dummyAntennae)
            {
                if (antennaName == antenna.AntennaName)
                {
                    return antenna;
                }
            }

            throw new ReaderException
                ("Antenna with AntennaName = " + antennaName + "does not exist");
        }

        #endregion dummy antenna
        #region Parameters

        internal string reader_Name = "";
        internal ReaderModel model ;
        internal string reader_Desc = "";
        internal string ipAddress= "";
        internal string tcpPort = "";
        internal string httpPort = "";
        internal string notificationPort = "";
        internal string notificationAddress = "";
        internal string deviceSerialNo = "";
        internal string firmwareVersion = "";
        internal string sdkVersion = "";

        #endregion Parameters

        public ClsReader(IRFIDReader paramObjReader)
        {
            objReader = paramObjReader;
            AddTagReadEventHandler();
            AddPinStatusEventHandler();
            AddManagementEventHandler();
            AddRFIDStatusMonitorEventHandler();

            InitializeParameters();
        }

        internal void InitializeParameters()
        {
            try
            {
                if (objReader != null)
                {
                    //Type objType = objReader.GetType();

                  if (objReader.Model == ReaderModel.XR450 ||
                      objReader.Model == ReaderModel.XR480 ||
                      objReader.Model == ReaderModel.XR400 ||
                      objReader.Model == ReaderModel.XR440
                     )
                  //if(objType == typeof(ReaderXR480))
                        {
                            try
                            {
                                reader_Name = objReader.ReaderName;
                                model = objReader.Model;
                                ipAddress = objReader.IpAddress;
                                tcpPort = objReader.TcpPort;
                                reader_Desc = objReader.ReaderDescription;
                                httpPort = objReader.HttpPort;
                                notificationPort = objReader.NotificationPort;
                                notificationAddress = objReader.NotificationAddress.ToString();
                                deviceSerialNo = objReader.ReaderInfo.DeviceSerialNumber;
                                firmwareVersion = objReader.ReaderInfo.FirmwareVersion;
                                sdkVersion = objReader.SDKVersionNumber;
                            }
                            catch { }
                           
                        }
                        else if (objReader.Model == ReaderModel.RD5000)
                   // if(objType == typeof(ReaderRD5000))
                        {
                            try
                            {
                                reader_Name = objReader.ReaderName;
                                model = objReader.Model;
                                ipAddress = objReader.IpAddress;
                                tcpPort = objReader.TcpPort;
                                 firmwareVersion = objReader.ReaderInfo.FirmwareVersion;
                                 if (firmwareVersion == null)
                                     firmwareVersion = string.Empty;

                                 deviceSerialNo = objReader.ReaderInfo.DeviceSerialNumber;
                                 if (deviceSerialNo == null )
                                     deviceSerialNo = string.Empty;
                                 
                                 
                            }
                            catch { }
                           
                        }
                }
            }
            
            catch { }
        }

        private void AddTagReadEventHandler()
		{
					
			if (onTagRead == null)
                onTagRead = new ReaderEventHandler(TagReadHandler);
            objReader.TagEvent += onTagRead;

		}

        private void RemoveTagReadEventHandler()
		{
            objReader.TagEvent -= onTagRead;
		}

        private void TagReadHandler(object sender, ReaderEventArgs args)
        {
            try
            {
                if (appOnTagRead != null)
                    appOnTagRead(sender, args);  //invoke appOnTagRead event

            }
            catch { }
        } 

        private void AddPinStatusEventHandler()
        {
            
            if (onPinStatusNotification == null)
                onPinStatusNotification = new ReaderEventHandler(PinStatusEventHandler);
            objReader.InputStatusNotifyEvent += onPinStatusNotification;
        }

        private void RemovePinStatusEventHandler()
        {
            objReader.InputStatusNotifyEvent -= onPinStatusNotification;
        }
        
        private void PinStatusEventHandler(object sender, ReaderEventArgs args)
        {
            try
            {
                if (appOnPinStatusNotification != null)
                    appOnPinStatusNotification(sender, args);
            }
            catch
            {
            }

        }

        internal void AddManagementEventHandler()
        {

            if (onReaderManagement == null)
                onReaderManagement = new ReaderEventHandler(ManagementEventHandler);

            objReader.ManagementEvent += onReaderManagement;

        }

        internal void AddRFIDStatusMonitorEventHandler()
        {

            if (onRfidStatusMonitor == null)
                onRfidStatusMonitor = new ReaderEventHandler(RFIDStatusMonitorEventHandler);

            objReader.RFIDStatusMonitorEvent += onRfidStatusMonitor;

        }

        private void RemoveManagementEventHandler()
        {
            objReader.ManagementEvent -= onReaderManagement;
        }

        private void RemoveRFIDStatusMonitorEventHandler()
        {
            objReader.RFIDStatusMonitorEvent -= onRfidStatusMonitor;
        }
        private void ManagementEventHandler(object sender, ReaderEventArgs args)
        {
            try
            {
                if (appOnReaderManagement != null)
                    appOnReaderManagement(sender, args);
            }
            catch
            {
            }
        }

        private void RFIDStatusMonitorEventHandler(object sender, ReaderEventArgs args)
        {
            try
            {
                if (appOnRFIDStatusMonitor != null)
                    appOnRFIDStatusMonitor(sender, args);
            }
            catch
            {
            }
        }
        internal void AddMotionSensorEventEventHandler()
        {

            if (onMotionSensorEvent == null)
                onMotionSensorEvent = new ReaderEventHandler(MotionSensorEventHandler);

            objReader.MotionSensorEvent += onMotionSensorEvent;
            
        }

        private void RemoveMotionSensorEventHandler()
        {
            objReader.MotionSensorEvent -= onMotionSensorEvent;
        }

        private void MotionSensorEventHandler(object sender, ReaderEventArgs args)
        {
            try
            {
                if (appOnMotionSensorEvent != null)
                    appOnMotionSensorEvent(sender, args);
            }
            catch
            {
            }
        }

        internal void AddProximitySensorEventHandler()
        {
            if (onProximitySensorEvent == null)
                onProximitySensorEvent = new ReaderEventHandler(ProximitySensorEventHandler);

            objReader.ProximitySensorEvent += onProximitySensorEvent;
        }

        private void ProximitySensorEventHandler(object sender, ReaderEventArgs args)
        {
            try
            {
                if (appOnProximitySensorEvent != null)
                    appOnProximitySensorEvent(sender, args);
            }
            catch
            {
            }

        }

        private void RemoveProximitySensorEventHandler()
        {
            objReader.ProximitySensorEvent -= onProximitySensorEvent; 
        }

        public void Dispose()
        {
            RemoveTagReadEventHandler();
            RemovePinStatusEventHandler();
            RemoveManagementEventHandler();
           
           
        }

        #region Commands
        
        internal void Connect()
        {
            objReader.Connect();
            
        }
        
        internal void DisConnect()
        {
            objReader.Disconnect();
            
            
        }
       

        internal IRFIDTag[] GetTags()
        {
            return  objReader.GetTags();
            
        }

        internal void StartAutonomousMode()
        {
            SetReadMode(ReadMode.AUTONOMOUS);
        }
        internal void StopAutonomousMode()
        {
            SetReadMode(ReadMode.ONDEMAND);
        }

        internal void WriteTagID(string hexTagID, AntennaConfig antennaCfg)
        {
                objReader.WriteTagID(hexTagID,antennaCfg);
        }

        internal void WriteTagID(string hexTagID)
        {

            hexTagID = hexTagID.Replace(" ", "");
            char[] chTagID = hexTagID.ToCharArray();
            ArrayList tagIdByteArr = new ArrayList();

            for (int i = 0; i < chTagID.Length - 1; i += 2)
            {
                string strTemp = new string(new char[] { chTagID[i], chTagID[i + 1] });
                byte idByte = Convert.ToByte(strTemp, 16);
                tagIdByteArr.Add(idByte);
            }

            byte[] tagId = (byte[])tagIdByteArr.ToArray(typeof(byte));

            objReader.WriteTagID(TagType.EPCClass1_GEN2, tagId);

        }

        //internal void KillTag(TagType tagType, byte[] tag, uint passcode)
        internal void KillTag(TagType tagType, byte[] tag)
        {
            // objReader.KillTag(tagType, tag, passcode);
            objReader.KillTag(tagType, tag);
        }
        //internal void KillTag(Gen2Parameters gen2Params, uint passcode)
        //{
        //    objReader.KillTag(gen2Params, passcode);
        //}


        internal IRFIDTag[] ReadData(byte memoryBank, ushort wordPointer, byte wordCount)
        {
            TagDataLoc dataLoc = new TagDataLoc(wordPointer, wordCount, (MemoryBank)memoryBank); 
            IRFIDTag[] tags = objReader.GetTags(dataLoc);
            return tags;

        }

        internal bool WriteTag(byte memoryBank, ushort wordPointer, byte[] tagData)
        {
            TagDataLoc loc = new TagDataLoc((ushort)wordPointer, Convert.ToByte(tagData.Length / 2), (MemoryBank)memoryBank);
            bool write = objReader.WriteTag(loc,tagData);
            return write;

        }

        
        internal void EraseTag(TagType tagType)
        {
            objReader.EraseTag(tagType);
        }
        internal void LockTag(string hexTagID, AntennaConfig antennaCfg)
        {
            objReader.LockTag(hexTagID, antennaCfg);
        }

        /*
        internal void EraseTag(Gen2Parameters gen2Params, TagDataSelector selection, uint accessPassword)
        {
            objReader.EraseTag(gen2Params, selection, accessPassword);
        }

       
        internal void WriteTag(TagType tagType, byte lockOptions, byte[] tagData)
        {
            objReader.WriteTag(tagType, lockOptions, tagData);
        }

        internal void WriteTag(Gen2Parameters gen2Params, byte lockOptions, uint accessPassword, byte[] tagData)
        {
            objReader.WriteTag(gen2Params, lockOptions, accessPassword, tagData);
        }
        */

        internal void GetPinlevels(out IOPinStatus[] inputPinsStatus, out IOPinStatus[] outputPinsStatus)
        {
           objReader.GetIOStatus(out inputPinsStatus,out outputPinsStatus);
        }

        internal void SetOutputPinlevels(IOPinStatus[] outputPinStatus)
        {
            objReader.SetOutputStatus(outputPinStatus);
        }

        internal void EnableInputStatusNotification(IOPins[] selectedPins, int interval)
        {
            objReader.EnableInputStatusNotification(selectedPins, interval);
            
        }

        internal void DisableInputStatusNotification()
        {
            objReader.DisableInputStatusNotification();
        }



        internal void EnableProximitySensor(int timeIntervalMS)
        {
            objReader.EnableProximitySensor(timeIntervalMS);
            AddProximitySensorEventHandler();
        }
        internal void DisableProximitySensor()
        {
            objReader.DisableProximitySensor();
            RemoveProximitySensorEventHandler();
        }

        internal void EnableMotionSensor(int timeIntervalMS)
        {
            objReader.EnableMotionSensor(timeIntervalMS);
            AddMotionSensorEventEventHandler();
            
        }
        internal void DisableMotionSensor()
        {
            objReader.DisableMotionSensor();
            RemoveMotionSensorEventHandler();
        }

        internal void EnableRFIDModule()
        {
            objReader.EnableRFIDModule();
            try
            {
                InitializeParameters();
            }
            catch { }
        }

        internal void DisableRFIDModule()
        {
            objReader.DisableRFIDModule();
        }

        internal bool GetRFIDModuleStatus()
        {
            return objReader.GetRFIDModuleStatus();
        }

        #endregion Commands


        internal ReaderCapability ReaderCapability
        {
            get { return objReader.ReaderCapability; }
        }

        internal bool IsMotionSensorOn
        {
            get { return objReader.IsMotionSensorOn; }
        }

        internal bool IsProximitySensorOn
        {
            get { return objReader.IsProximitySensorOn; }

        }

        internal int MotionTimeIntervalMS
        {
            get { return objReader.MotionTimeIntervalMS; }
        }

        internal int ProximityTimeIntervalMS
        {
            get { return objReader.ProximityTimeIntervalMS; }
        }

        internal string GetSDKVersionNumber()
        {
            return objReader.SDKVersionNumber;

        }

        internal string ReaderName()
        {
            return objReader.ReaderName;
         }

        internal string ReaderDesc()
        {
            return objReader.ReaderDescription;
        }

        internal string GetIPAddress()
        {
            return objReader.IpAddress;
        }

        internal string GetPort()
        {
            return objReader.TcpPort;
        }

        internal string GetHttpPort()
        {
            return objReader.HttpPort;
        }
        
        internal string GetNotificationPort()
        {
            return objReader.NotificationPort;
           
        }

        internal ReaderModel GetModel()
        {
            return objReader.Model;
        }

        #region ReaderInfo

        internal ReaderInfo GetReaderInfo()
        {
            return objReader.ReaderInfo;
           
        }

        #endregion ReaderInfo

        #region Antenna
        internal int GetNoOfAntennas()
        {
            return objReader.NoOfAntenna;

        }

        internal AntennaConfig[] GetAntennas()
        {
            return objReader.Antennas;
        }

        internal string[] GetAntennaNames()
        {
            return objReader.GetAntennaNames();
        }

        internal AntennaConfig GetAntennaConfiguration(string antennaName)
        {
            return objReader.GetAntennaConfiguration(antennaName);
        }

        internal void SetAntennaConfiguration(AntennaConfig antennaConfig)
        {
            objReader.SetAntennaConfiguration( antennaConfig);
        }
        #endregion Antenna
        internal ReadMode GetReadMode()
        {
              return objReader.ReadMode;
        }
        private void SetReadMode(ReadMode value)
        {
            objReader.ReadMode= value;
        }
        internal  ReaderStatus GetReaderStatus()
        {
            return objReader.ReaderStatus;

        }

        internal static string SDKVersion
        {
            get { return ReaderFactory.SDKVersion; }
        }



        #region  Run Single Instance of Application
       
        [DllImport("user32.Dll")]
        private static extern int EnumWindows(EnumWinCallBack callBackFunc, int lParam);

        [DllImport("User32.Dll")]
        private static extern void GetWindowText(int hWnd, StringBuilder str, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);

        /// <summary>
        /// EnumWindowCallBack
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private static bool EnumWindowCallBack(int hwnd, int lParam)
        {
            windowHandle = (IntPtr)hwnd;

            StringBuilder sbuilder = new StringBuilder(256);
            GetWindowText((int)windowHandle, sbuilder, sbuilder.Capacity);
            string strTitle = sbuilder.ToString();

            if (strTitle == sTitle)
            {
                ShowWindow(windowHandle, SW_RESTORE);
                SetForegroundWindow(windowHandle);
                return false;
            }
            return true;
        }//EnumWindowCallBack

        /// <summary>
        /// Execute a form base application if another instance already running on
        /// the system activate previous one
        /// </summary>
        /// <param name="frmMain">main form</param>
        /// <returns>true if no previous instance is running</returns>
        public static bool RunSingleInstance(System.Windows.Forms.Form frmMain)
        {
            if (IsAlreadyRunning())
            {
                sTitle = frmMain.Text;
                //set focus on previously running app

                EnumWindows(new EnumWinCallBack(EnumWindowCallBack), 0);
                return false;
            }

            System.Windows.Forms.Application.Run(frmMain);
            return true;
        }
               

        /// <summary>
        /// for console base application
        /// </summary>
        /// <returns></returns>
        public static bool RunSingleInstance()
        {
            if (IsAlreadyRunning())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// check if given exe alread running or not
        /// </summary>
        /// <returns>returns true if already running</returns>
        private static bool IsAlreadyRunning()
        {
            // Use the EntryAssembly to allow multiple applications 
            // to take advantage of the same routine. 
            // It will create one Mutex per EntryAssembly 
            string strLoc = System.Reflection.Assembly.GetEntryAssembly().FullName;

            System.IO.FileSystemInfo fileInfo = new System.IO.FileInfo(strLoc);
            string sExeName = fileInfo.Name;
            mutex = new System.Threading.Mutex(true, sExeName);

            if (mutex.WaitOne(0, false))
            {
                return false;
            }
            return true;
        }

        static System.Threading.Mutex mutex;
        const int SW_RESTORE = 9;
        static string sTitle;
        static IntPtr windowHandle;
        delegate bool EnumWinCallBack(int hwnd, int lParam);

        #endregion  Run Single Instance of Application 

    }
}

			