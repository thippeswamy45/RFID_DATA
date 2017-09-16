using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Symbol.RFID3;
using MySql.Data.MySqlClient;
using Microsoft.Office.Interop.Excel;
namespace CS_RFID3_Host_Sample2
{
    public partial class AppForm : Form
    {
        internal RFIDReader m_ReaderAPI;
        internal bool m_IsConnected;
        internal ReaderManagement m_ReaderMgmt;
        internal READER_TYPE m_ReaderType;
        internal AccessFilterForm m_AccessFilterForm;
        internal AntennaInfoForm m_AntennaInfoForm;
        internal ConnectionForm m_ConnectionForm;
        internal LoginForm m_LoginForm;
        internal AntennaModeForm m_AntennaModeForm;
        internal ReadPointForm m_ReadPointForm;
        internal FirmwareUpdateForm m_FirmwareUpdateForm;
        internal ReaderInfoForm m_ReaderInfoForm;
        internal PreFilterForm m_PreFilterForm;
        internal PostFilterForm m_PostFilterForm;
        internal ReadForm m_ReadForm;
        internal WriteForm m_WriteForm;
        internal LockForm m_LockForm;
        internal KillForm m_KillForm;
        internal WriteForm m_BlockWriteForm;
        internal BlockEraseForm m_BlockEraseForm;
        internal LocateForm m_LocateForm;
        internal AccessOperationResult m_AccessOpResult;
        internal TriggersForm m_TriggerForm;
        internal TagStorageSettingsForm m_TagStorageSettingsForm;

        internal ArrayList m_GPIStateList;
        internal string m_SelectedTagID = "";

        private delegate void UpdateStatus(Events.StatusEventData eventData);
        private UpdateStatus m_UpdateStatusHandler = null;
        private delegate void UpdateRead(Events.ReadEventData eventData);
        private UpdateRead m_UpdateReadHandler = null;
        private TagData m_ReadTag = null;
        private Hashtable m_TagTable;
        private int m_SortColumn = -1;
        private uint m_TagTotalCount;
     //   static Microsoft.Office.Interop.Excel.Application xls = new Microsoft.Office.Interop.Excel.Application();// store data to xl file
     //   static Workbook wb = xls.Workbooks.Add(XlSheetType.xlWorksheet);// store data to xl file
    //    Worksheet ws = (Worksheet)xls.ActiveSheet;// store data to xl file
        StringBuilder sb1 = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        StringBuilder sb3 = new StringBuilder();
        StringBuilder sb4 = new StringBuilder();
        StringBuilder sb5 = new StringBuilder();
        StringBuilder sb6 = new StringBuilder();
        StringBuilder sb7 = new StringBuilder();
        StringBuilder sb8 = new StringBuilder();
        StringBuilder sb9 = new StringBuilder();
        StringBuilder sb10 = new StringBuilder();
        StringBuilder sb11 = new StringBuilder();
        StringBuilder sb12 = new StringBuilder();
        StringBuilder sbe1 = new StringBuilder();
        StringBuilder sbe2 = new StringBuilder();
        StringBuilder sbe3 = new StringBuilder();
        StringBuilder sbe4 = new StringBuilder();
        StringBuilder sbee1 = new StringBuilder();
        StringBuilder sbee2 = new StringBuilder();
        static int ccount = 1;
        public int counter = 5;
        static double summ1 = 0.0;
        static double summ2 = 0.0;
        static double summ3 = 0.0;
        static double summ4 = 0.0;
        static double summ5 = 0.0;
        static double summ6 = 0.0;
        static double summ7 = 0.0;
        static double summ8 = 0.0;
        static double summ9 = 0.0;
        static double summ10 = 0.0;
        static double summ11 = 0.0;
        static double summ12 = 0.0;
        static double summ13 = 0.0;
        static double summ14 = 0.0;
        static double summ15 = 0.0;
        static double summ16 = 0.0;
        static double summ17 = 0.0;
        static double summ18 = 0.0;
        static double summ19 = 0.0;
        static double summ20 = 0.0;
        static double summ21 = 0.0;
        static double summ22 = 0.0;
        static double summ23 = 0.0;
        static double summ24 = 0.0;
        static double summ25 = 0.0;
        static double summ26 = 0.0;
        static double summ27 = 0.0;
        static double summ28 = 0.0;
        static double summ29 = 0.0;
        static double summ30 = 0.0;
        static double summ31 = 0.0;
        static double summ32 = 0.0;
        static double summ33 = 0.0;
        static double summ34 = 0.0;
        static double summ35 = 0.0;
        static double summ36 = 0.0;
        static double summ37 = 0.0;
        static double summ38 = 0.0;
        static double summ39 = 0.0;
       static double summ40 = 0.0;
     
       
        static double min1 = 0.0;
        static double min2 = 0.0;
        static double min3 = 0.0;
        static double min4 = 0.0;
        static double min5 = 0.0;
        static double min6 = 0.0;
        static double min7 = 0.0;
        static double min8 = 0.0;
        static double min9 = 0.0;
        static double min10 = 0.0;
        static double min11 = 0.0;
        static double min12 = 0.0;
        static double mine1 = 0.0;
        static double mine2 = 0.0;
        static double mine3 = 0.0;
        static double mine4 = 0.0;
        public int count1 = 0;
        public int count2 = 0;
        public int count3 = 0;
        public int count4 = 0;
        public int count5 = 0;
        public int count6 = 0;
        public int count7 = 0;
        public int count8 = 0;
        public int count9 = 0;
        public int count10 = 0;
        public int count11 = 0;
        public int count12 = 0;
        public int count13 = 0;
        public int count14 = 0;
        public int count15 = 0;
        public int count16 = 0;
        public int count17 = 0;
        public int count18 = 0;
        public int count19 = 0;
        public int count20 = 0;
        public int count21 = 0;
        public int count22 = 0;
        public int count23 = 0;
        public int count24 = 0;
        public int count25 = 0;
        public int count26 = 0;
        public int count27 = 0;
        public int count28 = 0;
        public int count29 = 0;
        public int count30 = 0;
        public int count31 = 0;
        public int count32 = 0;
        public int count33 = 0;
        public int count34 = 0;
        public int count35 = 0;
        public int count36 = 0;
        public int count37 = 0;
        public int count38 = 0;
        public int count39 = 0;
        public int count40 = 0;
    
        internal class AccessOperationResult
        {
            public RFIDResults m_Result;
            public ACCESS_OPERATION_CODE m_OpCode;

            public AccessOperationResult()
            {
                m_Result = RFIDResults.RFID_NO_ACCESS_IN_PROGRESS;
                m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;
            }
        }

        internal class ListViewItemComparer : IComparer
        {
            private int m_Coloumn;
            private SortOrder m_Order;
            public ListViewItemComparer()
            {
                m_Coloumn = 0;
                m_Order = SortOrder.Ascending;
            }
            public ListViewItemComparer(int column, SortOrder order)
            {
                m_Coloumn = column;
                m_Order = order;
            }

            public int Compare(object x, object y)
            {
                int returnVal = -1;
                returnVal = String.Compare(((ListViewItem)x).SubItems[m_Coloumn].Text,
                    ((ListViewItem)y).SubItems[m_Coloumn].Text);
                if (m_Order == SortOrder.Descending)
                    returnVal *= -1;
                return returnVal;
            }
        }

        public AppForm()
        {
            
          //  Workbook wb1 = Xls.Workbooks.Open(@"C:\Users\nem lab\Desktop\mydata");
            InitializeComponent();
            m_GPIStateList = new ArrayList();
            m_UpdateStatusHandler = new UpdateStatus(myUpdateStatus);
            m_UpdateReadHandler = new UpdateRead(myUpdateRead);
            m_ReadTag = new Symbol.RFID3.TagData();
            m_ConnectionForm = new ConnectionForm(this);
            m_ReadForm = new ReadForm(this);
            m_AntennaInfoForm = new AntennaInfoForm(this);
            m_PostFilterForm = new PostFilterForm(this);
            m_AccessFilterForm = new AccessFilterForm(this);
            m_TriggerForm = new TriggersForm(this);
            m_ReaderMgmt = new ReaderManagement();
            m_TagTable = new Hashtable();
            m_AccessOpResult = new AccessOperationResult();
            m_IsConnected = false;
            m_TagTotalCount = 0;
            configureMenuItemsUponConnectDisconnect();
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void myUpdateStatus(Events.StatusEventData eventData)
        {
            switch (eventData.StatusEventType)
            {
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_START_EVENT:
                    functionCallStatusLabel.Text = "Inventory started";
                    this.readButton.Enabled = true;
                    this.readButton.Text = "Stop Reading";
                    memBank_CB.Enabled = false;
                    sbee1.Length = 0;
                    sbee1.AppendFormat("{0},", "A1");
                    sbee1.AppendFormat("{0},", "A2");
                    sbee1.AppendFormat("{0},", "A3");
                  //  sbee1.AppendFormat("{0},", "A4");
                    sbee1.AppendLine();
                    sb1.Length = 0;
                    sb1.AppendFormat("{0},", "tagID");
                    sb1.AppendFormat("{0},", "antenna ID");
                    sb1.AppendFormat("{0},", "RSSI");
                    sb1.AppendFormat("{0},", "time stamp");
                    sb1.AppendLine();
                    sb2.Length = 0;
                    sb2.AppendFormat("{0},", "tagID");
                    sb2.AppendFormat("{0},", "antenna ID");
                    sb2.AppendFormat("{0},", "RSSI");
                    sb2.AppendFormat("{0},", "time stamp");
                    sb2.AppendLine();
                    sb3.Length = 0;
                    sb3.AppendFormat("{0},", "tagID");
                    sb3.AppendFormat("{0},", "antenna ID");
                    sb3.AppendFormat("{0},", "RSSI");
                    sb3.AppendFormat("{0},", "time stamp");
                    sb3.AppendLine();
                    sb4.Length = 0;
                    sb4.AppendFormat("{0},", "tagID");
                    sb4.AppendFormat("{0},", "antenna ID");
                    sb4.AppendFormat("{0},", "RSSI");
                    sb4.AppendFormat("{0},", "time stamp");
                    sb4.AppendLine();
                    sb5.Length = 0;
                    sb5.AppendFormat("{0},", "tagID");
                    sb5.AppendFormat("{0},", "antenna ID");
                    sb5.AppendFormat("{0},", "RSSI");
                    sb5.AppendFormat("{0},", "time stamp");
                    sb5.AppendLine();
                    sb6.Length = 0;
                    sb6.AppendFormat("{0},", "tagID");
                    sb6.AppendFormat("{0},", "antenna ID");
                    sb6.AppendFormat("{0},", "RSSI");
                    sb6.AppendFormat("{0},", "time stamp");
                    sb6.AppendLine();
                    sb7.Length = 0;
                    sb7.AppendFormat("{0},", "tagID");
                    sb7.AppendFormat("{0},", "antenna ID");
                    sb7.AppendFormat("{0},", "RSSI");
                    sb7.AppendFormat("{0},", "time stamp");
                    sb7.AppendLine();
                    sb8.Length = 0;
                    sb8.AppendFormat("{0},", "tagID");
                    sb8.AppendFormat("{0},", "antenna ID");
                    sb8.AppendFormat("{0},", "RSSI");
                    sb8.AppendFormat("{0},", "time stamp");
                    sb8.AppendLine();
                    sb9.Length = 0;
                    sb9.AppendFormat("{0},", "tagID");
                    sb9.AppendFormat("{0},", "antenna ID");
                    sb9.AppendFormat("{0},", "RSSI");
                    sb9.AppendFormat("{0},", "time stamp");
                    sb9.AppendLine();
                    sb10.Length = 0;
                    sb10.AppendFormat("{0},", "tagID");
                    sb10.AppendFormat("{0},", "antenna ID");
                    sb10.AppendFormat("{0},", "RSSI");
                    sb10.AppendFormat("{0},", "time stamp");
                    sb10.AppendLine();
                    sb11.Length = 0;
                    sb11.AppendFormat("{0},", "tagID");
                    sb11.AppendFormat("{0},", "antenna ID");
                    sb11.AppendFormat("{0},", "RSSI");
                    sb11.AppendFormat("{0},", "time stamp");
                    sb11.AppendLine();
                    sb12.Length = 0;
                    sb12.AppendFormat("{0},", "tagID");
                    sb12.AppendFormat("{0},", "antenna ID");
                    sb12.AppendFormat("{0},", "RSSI");
                    sb12.AppendFormat("{0},", "time stamp");
                    sb12.AppendLine();
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_STOP_EVENT:
                    functionCallStatusLabel.Text = "Inventory stopped";
                    this.readButton.Enabled = true;
                    this.readButton.Text = "Start Reading";
                    memBank_CB.Enabled = true;

                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.ACCESS_START_EVENT:
                    functionCallStatusLabel.Text = "Access Operation started";
                    this.readButton.Enabled = true;
                    this.readButton.Text = "Stop Reading";
                    memBank_CB.Enabled = false; ;
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.ACCESS_STOP_EVENT:
                    functionCallStatusLabel.Text = "Access Operation stopped";

                    if (this.m_SelectedTagID == string.Empty)
                    {
                        uint successCount, failureCount;
                        successCount = failureCount = 0;
                        m_ReaderAPI.Actions.TagAccess.GetLastAccessResult(ref successCount, ref failureCount);
                        functionCallStatusLabel.Text = "Access completed - Success Count: " + successCount.ToString()
                            + ", Failure Count: " + failureCount.ToString();
                    }
                    resetButtonState();
                    this.readButton.Enabled = true;
                    this.readButton.Text = "Start Reading";
                    memBank_CB.Enabled = true;
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.GPI_EVENT:
                    this.updateGPIState();
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.ANTENNA_EVENT:
                    string status = (eventData.AntennaEventData.AntennaEvent == ANTENNA_EVENT_TYPE.ANTENNA_CONNECTED ? "connected" : "disconnected");
                    functionCallStatusLabel.Text = "Antenna " + eventData.AntennaEventData.AntennaID.ToString() + " has been " + status;
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.BUFFER_FULL_WARNING_EVENT:
                    functionCallStatusLabel.Text = " Buffer full warning";
                    myUpdateRead(null);
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.BUFFER_FULL_EVENT:
                    functionCallStatusLabel.Text = "Buffer full";
                    myUpdateRead(null);
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.DISCONNECTION_EVENT:
                    functionCallStatusLabel.Text = "Disconnection Event " + eventData.DisconnectionEventData.DisconnectEventInfo.ToString();
                    connectBackgroundWorker.RunWorkerAsync("Disconnect");
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.READER_EXCEPTION_EVENT:
                    functionCallStatusLabel.Text = "Reader ExceptionEvent " + eventData.ReaderExceptionEventData.ReaderExceptionEventInfo.ToString();
                    break;
                default:
                    break;
            }
        }
      //  int i = 1;
        private void myUpdateRead(Events.ReadEventData eventData)
        {
            Symbol.RFID3.TagData[] tagData = m_ReaderAPI.Actions.GetReadTags(1000);
            if (tagData != null)
            {
                for (int nIndex = 0; nIndex < tagData.Length; nIndex++)
                {
                    if (tagData[nIndex].ContainsLocationInfo)
                    {
                        m_LocateForm.Locate_PB.Value = tagData[nIndex].LocationInfo.RelativeDistance; 
                    }                  
                    if (tagData[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_NONE ||
                        (tagData[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ &&
                        tagData[nIndex].OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS))
                    {
                        Symbol.RFID3.TagData tag = tagData[nIndex];
                        string tagID = tag.TagID;
                        bool isFound = false;

                        lock (m_TagTable.SyncRoot)
                        {
                            isFound = m_TagTable.ContainsKey(tagID);
                            if (!isFound && this.memBank_CB.SelectedIndex >=1)
                            {
                                tagID = tag.TagID + tag.MemoryBank.ToString()
                                    + tag.MemoryBankDataOffset.ToString();
                                isFound = m_TagTable.ContainsKey(tagID);
                            }
                        }

                        if (isFound)
                        {
                           string tag1, tag15, tag3,tag4,tag5,tag6,tag11,tag8,tag13,tag7;
                           

                            tag1 = "E280116060000205285A8BDE";
                            tag15 = "E280116060000205285A9B9E";
                            tag3 = "E280116060000205285A63C0";
                            tag4 = "E2002083950C01432820026A";
                            tag5 = "987654328509025716906595";
                            tag6 = "E280116060000205285A1D10";
                            tag11 = "222222222222222222222222";
                            tag8 = "E2801160600002052859F172";
                            tag13 = "E2801160600002052859E8AF";
                            tag7 = "E280116060000205285BFA50";
                            uint count = 0;
                            ListViewItem item = (ListViewItem)m_TagTable[tagID];
                            try
                            {
                                count = uint.Parse(item.SubItems[3].Text) + tagData[nIndex].TagSeenCount;
                                m_TagTotalCount += tagData[nIndex].TagSeenCount;
                            }
                            catch (FormatException fe)
                            {
                                functionCallStatusLabel.Text = fe.Message;
                                break;
                            }
                            item.SubItems[2].Text = tag.AntennaID.ToString();
                            item.SubItems[3].Text = count.ToString();
                            item.SubItems[4].Text = tag.PeakRSSI.ToString();
                            //   if (tag.PeakRSSI > -50 && m_TagTotalCount >= 100 * i && tag.AntennaID.ToString()=="1") //beep sound if rssi magnitute is <40

                            //   insertToXlFile(tagID, tag.AntennaID.ToString(), m_TagTotalCount-1, tag.PeakRSSI.ToString());
                            //       _inputparameter.tagid = tagID;                             //asigning data to varible store into xl sheet  by stucture called dataparamter and object called inputparameter 
                            //      _inputparameter.antennaID = tag.AntennaID.ToString();       //asigning data to varible store into xl sheet  by stucture called dataparamter and object called inputparameter
                            //_inputparameter.count = m_TagTotalCount - 3;        //asigning data to varible store into xl sheet  by stucture called dataparamter and object called inputparameter
                            //_inputparameter.rssi = tag.PeakRSSI.ToString();        //asigning data to varible store into xl sheet  by stucture called dataparamter and object called inputparameter
                            if (!backgroundWorker1.IsBusy)
                            {
                       //         backgroundWorker1.RunWorkerAsync(_inputparameter);
                            }
                           // Console.WriteLine(m_TagTotalCount);
                            string time2;
                            DateTime localDate1 = DateTime.Now;
                            time2 = localDate1.ToString("h.mm.ss.fff");                            
                            switch (tag.AntennaID)
                            {
                                case 1:if (tagID == tag1)
                                    {

                                        sb1.AppendFormat("{0},", tagID);
                                        sb1.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sb1.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sb1.AppendFormat("{0},", time2);
                                        sb1.AppendLine();
                                        summ1 += tag.PeakRSSI;

                                        count1++;
                                        if (min1 > tag.PeakRSSI)
                                        {
                                            min1 = tag.PeakRSSI;
                                        }


                                    }
                                    else if (tagID == tag15)
                                    {
                                        sb2.AppendFormat("{0},", tagID);
                                        sb2.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sb2.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sb2.AppendFormat("{0},", time2);
                                        sb2.AppendLine();
                                        summ2 += tag.PeakRSSI;
                                        count2++;
                                        if (min2 > tag.PeakRSSI)
                                        {
                                            min2 = tag.PeakRSSI;
                                        }
                                    }
                                    else if (tagID == tag3)
                                    {
                                        sb3.AppendFormat("{0},", tagID);
                                        sb3.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sb3.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sb3.AppendFormat("{0},", time2);
                                        sb3.AppendLine();
                                        summ3 += tag.PeakRSSI;
                                        count3++;
                                        if (min3 > tag.PeakRSSI)
                                        {
                                            min3 = tag.PeakRSSI;
                                        }
                                    }
                                    else if (tagID == tag4)
                                    {
                                        sbe1.AppendFormat("{0},", tagID);
                                        sbe1.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sbe1.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sbe1.AppendFormat("{0},", time2);
                                        sbe1.AppendLine();
                                        summ4 += tag.PeakRSSI;
                                        count4++;
                                        if (mine1 > tag.PeakRSSI)
                                        {
                                            mine1 = tag.PeakRSSI;
                                        }
                                    }
                                    else if (tagID == tag5)
                                    {
                                        summ5 += tag.PeakRSSI;
                                        count5++;
                                    }
                                    else if (tagID == tag6)
                                    {
                                        summ6 += tag.PeakRSSI;
                                        count6++;
                                    }
                                    else if (tagID == tag11)
                                    {
                                        summ7 += tag.PeakRSSI;
                                        count7++;
                                    }
                                    else if (tagID == tag8)
                                    {
                                        summ8 += tag.PeakRSSI;
                                        count8++;
                                    }
                                    else if (tagID == tag13)
                                    {
                                        summ9 += tag.PeakRSSI;
                                        count9++;
                                    }
                                    else if (tagID == tag7)
                                    {
                                        summ10 += tag.PeakRSSI;
                                        count10++;
                                    }
                                    break;
                                case 2:
                                    if (tagID == tag1)
                                    {
                                        sb4.AppendFormat("{0},", tagID);
                                        sb4.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sb4.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sb4.AppendFormat("{0},", time2);
                                        sb4.AppendLine();
                                        summ11 += tag.PeakRSSI;
                                        count11++;
                                        if (min4 > tag.PeakRSSI)
                                        {
                                            min4 = tag.PeakRSSI;
                                        }
                                    }
                                    else if (tagID == tag15)
                                    {
                                        sb5.AppendFormat("{0},", tagID);
                                        sb5.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sb5.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sb5.AppendFormat("{0},", time2);
                                        sb5.AppendLine();
                                        summ12 += tag.PeakRSSI;
                                        count12++;
                                        if (min5 > tag.PeakRSSI)
                                        {
                                            min5 = tag.PeakRSSI;
                                        }
                                    }
                                    else if (tagID == tag3)
                                    {
                                        sb6.AppendFormat("{0},", tagID);
                                        sb6.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sb6.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sb6.AppendFormat("{0},", time2);
                                        sb6.AppendLine();
                                        summ13 += tag.PeakRSSI;
                                        count13++;
                                        if (min6 > tag.PeakRSSI)
                                        {
                                            min6 = tag.PeakRSSI;
                                        }
                                    }
                                    else if(tagID == tag4)
                                    {
                                        sbe2.AppendFormat("{0},", tagID);
                                        sbe2.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sbe2.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sbe2.AppendFormat("{0},", time2);
                                        sbe2.AppendLine();
                                        summ14 += tag.PeakRSSI;
                                        count14++;
                                        if (mine2 > tag.PeakRSSI)
                                        {
                                            mine2 = tag.PeakRSSI;
                                        }
                                    }
                                    else if (tagID == tag5)
                                    {
                                        summ15 += tag.PeakRSSI;
                                        count15++;
                                    }
                                    else if (tagID == tag6)
                                    {
                                        summ16 += tag.PeakRSSI;
                                        count16++;
                                    }
                                    else if (tagID == tag11)
                                    {
                                        summ17 += tag.PeakRSSI;
                                        count17++;
                                    }
                                    else if (tagID == tag8)
                                    {
                                        summ18 += tag.PeakRSSI;
                                        count18++;
                                    }
                                    else if (tagID == tag13)
                                    {
                                        summ19 += tag.PeakRSSI;
                                        count19++;
                                    }
                                    else if (tagID == tag7)
                                    {
                                        summ20 += tag.PeakRSSI;
                                        count20++;
                                    }

                                    break;
                                case 3:
                                    if (tagID == tag1)
                                    {
                                        sb7.AppendFormat("{0},", tagID);
                                        sb7.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sb7.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sb7.AppendFormat("{0},", time2);
                                        sb7.AppendLine();
                                        summ21 += tag.PeakRSSI;
                                        count21++;
                                        if (min7 > tag.PeakRSSI)
                                        {
                                            min7 = tag.PeakRSSI;
                                        }
                                    }
                                    else if (tagID == tag15)
                                    {
                                        sb8.AppendFormat("{0},", tagID);
                                        sb8.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sb8.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sb8.AppendFormat("{0},", time2);
                                        sb8.AppendLine();
                                        summ22 += tag.PeakRSSI;
                                        count22++;
                                        if (min8 > tag.PeakRSSI)
                                        {
                                            min8 = tag.PeakRSSI;
                                        }
                                    }
                                    else if (tagID == tag3)
                                    {
                                        sb9.AppendFormat("{0},", tagID);
                                        sb9.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sb9.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sb9.AppendFormat("{0},", time2);
                                        sb9.AppendLine();
                                        summ23 += tag.PeakRSSI;
                                        count23++;
                                        if (min9 > tag.PeakRSSI)
                                        {
                                            min9 = tag.PeakRSSI;
                                        }
                                    }
                                    else if (tagID == tag4)
                                    {
                                        sbe3.AppendFormat("{0},", tagID);
                                        sbe3.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sbe3.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sbe3.AppendFormat("{0},", time2);
                                        sbe3.AppendLine();
                                        summ24 += tag.PeakRSSI;
                                        count24++;
                                        if (mine3 > tag.PeakRSSI)
                                        {
                                          mine3 = tag.PeakRSSI;
                                        }
                                    }
                                     else if (tagID == tag5)
                                    {
                                        summ25 += tag.PeakRSSI;
                                        count25++;
                                    }
                                    else if (tagID == tag6)
                                    {
                                        summ26 += tag.PeakRSSI;
                                        count26++;
                                    }
                                    else if (tagID == tag11)
                                    {
                                        summ27 += tag.PeakRSSI;
                                        count27++;
                                    }
                                    else if (tagID == tag8)
                                    {
                                        summ28 += tag.PeakRSSI;
                                        count28++;
                                    }
                                    else if (tagID == tag13)
                                    {
                                        summ29 += tag.PeakRSSI;
                                        count29++;
                                    }
                                    else if (tagID == tag7)
                                    {
                                        summ30 += tag.PeakRSSI;
                                        count30++;
                                    }
                                    break;
                                case 4:
                                    if (tagID == tag1)
                                    {
                                        sb10.AppendFormat("{0},", tagID);
                                        sb10.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sb10.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sb10.AppendFormat("{0},", time2);
                                        sb10.AppendLine();
                                        summ31 += tag.PeakRSSI;
                                        count31++;
                                        if (min10 > tag.PeakRSSI)
                                        {
                                            min10 = tag.PeakRSSI;
                                        }
                                    }
                                    else if (tagID == tag15)
                                    {
                                        sb11.AppendFormat("{0},", tagID);
                                        sb11.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sb11.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sb11.AppendFormat("{0},", time2);
                                        sb11.AppendLine();
                                        summ32 += tag.PeakRSSI;
                                        count32++;
                                        if (min11 > tag.PeakRSSI)
                                        {
                                            min11 = tag.PeakRSSI;
                                        }
                                    }
                                    else if (tagID == tag3)
                                    {
                                        sb12.AppendFormat("{0},", tagID);
                                        sb12.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sb12.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sb12.AppendFormat("{0},", time2);
                                        sb12.AppendLine();
                                        summ33 += tag.PeakRSSI;
                                        count33++;
                                        if (min12 > tag.PeakRSSI)
                                        {
                                            min12 = tag.PeakRSSI;
                                        }
                                    }
                                    else if(tagID == tag4)
                                    {
                                        sbe4.AppendFormat("{0},", tagID);
                                        sbe4.AppendFormat("{0},", tag.AntennaID.ToString());
                                        sbe4.AppendFormat("{0},", tag.PeakRSSI.ToString());
                                        sbe4.AppendFormat("{0},", time2);
                                        sbe4.AppendLine();
                                        summ34 += tag.PeakRSSI;
                                        count34++;
                                        if (mine4 > tag.PeakRSSI)
                                        {
                                            mine4 = tag.PeakRSSI;
                                        }
                                    }
                                     else if (tagID == tag5)
                                    {
                                        summ35 += tag.PeakRSSI;
                                        count35++;
                                    }
                                    else if (tagID == tag6)
                                    {
                                        summ36 += tag.PeakRSSI;
                                        count36++;
                                    }
                                    else if (tagID == tag11)
                                    {
                                        summ37 += tag.PeakRSSI;
                                        count37++;
                                    }
                                    else if (tagID == tag8)
                                    {
                                        summ38 += tag.PeakRSSI;
                                        count38++;
                                    }
                                    else if (tagID == tag13)
                                    {
                                        summ39 += tag.PeakRSSI;
                                        count39++;
                                    }
                                    else if (tagID == tag7)
                                    {
                                        summ40 += tag.PeakRSSI;
                                        count40++;
                                    }
                                    break;



                            }
                            
                            //     Console.Beep(5000, 100);                        
                            //    insertDatabase(tagID, tag.AntennaID.ToString(), tag.PeakRSSI.ToString());
                            //
                            //Console.WriteLine(i);
                            //   i = i + 2;

                            /*  else if (tag.PeakRSSI < -60 && m_TagTotalCount >= 100 * i && tag.AntennaID.ToString() == "1") //beep sound if rssi magnitute is <40
                            
                            {


                                Console.Beep(5000, 100);
                                insertDatabase(tagID, tag.AntennaID.ToString(), tag.PeakRSSI.ToString());
                                Console.WriteLine(i);
                                i =i+2;
                            }*/
                            //  if (tag.PeakRSSI > -50 && m_TagTotalCount >= 100 * i && tag.AntennaID.ToString() == "2") //beep sound if rssi magnitute is <40
                            //    if ( tag.AntennaID.ToString() == "1")
                            //    {

                            //  Console.Beep(2000, 100);
                            // insertDatabase(tagID, tag.AntennaID.ToString(), tag.PeakRSSI.ToString());
                            //   Console.WriteLine(i);
                            //   i = i + 2;
                            //       }

                            /*  else if (tag.PeakRSSI < -60 && m_TagTotalCount >= 100 * i  && tag.AntennaID.ToString() == "2") //beep sound if rssi magnitute is <40
                                     {

                                         Console.Beep(2000, 100);
                                         insertDatabase(tagID, tag.AntennaID.ToString(), tag.PeakRSSI.ToString());
                                         Console.WriteLine(i);
                                         i = i + 2;
                                     }*/
                            string memoryBank = tag.MemoryBank.ToString();
                            int index = memoryBank.LastIndexOf('_');
                            if (index != -1)
                            {
                                memoryBank = memoryBank.Substring(index + 1);
                            }
                            if (tag.MemoryBankData.Length > 0 && !memoryBank.Equals(item.SubItems[5].Text))
                            {
                                item.SubItems[6].Text = tag.MemoryBankData;
                                item.SubItems[7].Text = memoryBank;
                                item.SubItems[8].Text = tag.MemoryBankDataOffset.ToString();

                                lock (m_TagTable.SyncRoot)
                                {
                                    m_TagTable.Remove(tagID);
                                    m_TagTable.Add(tag.TagID + tag.MemoryBank.ToString()
                                        + tag.MemoryBankDataOffset.ToString(), item);
                                }
                            }
                            item.SubItems[1].Text = getTagEvent(tag);

                        }
                        else
                        {
                            ListViewItem item = new ListViewItem(tag.TagID);
                            ListViewItem.ListViewSubItem subItem;

                            subItem = new ListViewItem.ListViewSubItem(item, getTagEvent(tag));
                            item.SubItems.Add(subItem);

                            subItem = new ListViewItem.ListViewSubItem(item, tag.AntennaID.ToString());
                            item.SubItems.Add(subItem);

                            subItem = new ListViewItem.ListViewSubItem(item, tag.TagSeenCount.ToString());
                            m_TagTotalCount += tag.TagSeenCount;
                            item.SubItems.Add(subItem);

                            subItem = new ListViewItem.ListViewSubItem(item, tag.PeakRSSI.ToString());
                            item.SubItems.Add(subItem);

                            subItem = new ListViewItem.ListViewSubItem(item, tag.PC.ToString("X"));
                            item.SubItems.Add(subItem);

                            if (memBank_CB.SelectedIndex >= 1)
                            {
                                subItem = new ListViewItem.ListViewSubItem(item, tag.MemoryBankData);
                                item.SubItems.Add(subItem);

                                string memoryBank = tag.MemoryBank.ToString();
                                int index = memoryBank.LastIndexOf('_');
                                if (index != -1)
                                {
                                    memoryBank = memoryBank.Substring(index + 1);
                                }

                                subItem = new ListViewItem.ListViewSubItem(item, memoryBank);
                                item.SubItems.Add(subItem);
                                subItem = new ListViewItem.ListViewSubItem(item, tag.MemoryBankDataOffset.ToString());
                                item.SubItems.Add(subItem);
                            }
                            else
                            {
                                subItem = new ListViewItem.ListViewSubItem(item, "");
                                item.SubItems.Add(subItem);
                                subItem = new ListViewItem.ListViewSubItem(item, "");
                                item.SubItems.Add(subItem);
                                subItem = new ListViewItem.ListViewSubItem(item, "");
                                item.SubItems.Add(subItem);
                            }
                            inventoryList.BeginUpdate();
                            inventoryList.Items.Add(item);
                            inventoryList.EndUpdate();

                            lock (m_TagTable.SyncRoot)
                            {
                                m_TagTable.Add(tagID, item);
                            }
                        }
                    }
                }
                totalTagValueLabel.Text = m_TagTable.Count + "(" + m_TagTotalCount + ")";
            }
        }

        private void Events_ReadNotify(object sender, Events.ReadEventArgs readEventArgs)
        {
            try
            {
                this.Invoke(m_UpdateReadHandler, new object[] { readEventArgs.ReadEventData.TagData});
            }
            catch (Exception)
            {
            }
        }

        public void Events_StatusNotify(object sender, Events.StatusEventArgs statusEventArgs)
        {
            try
            {
                this.Invoke(m_UpdateStatusHandler, new object[] { statusEventArgs.StatusEventData });
            }
            catch (Exception)
            {
            }
        }

        private void accessBackgroundWorker_DoWork(object sender, DoWorkEventArgs accessEvent)
        {
            try
            {
                m_AccessOpResult.m_OpCode = (ACCESS_OPERATION_CODE)accessEvent.Argument;
                m_AccessOpResult.m_Result = RFIDResults.RFID_API_SUCCESS;

                if ((ACCESS_OPERATION_CODE)accessEvent.Argument == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReadTag = m_ReaderAPI.Actions.TagAccess.ReadWait(
                        m_SelectedTagID, m_ReadForm.m_ReadParams, m_AntennaInfoForm.getInfo());
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.ReadEvent(m_ReadForm.m_ReadParams,
                            m_AccessFilterForm.getFilter(), m_AntennaInfoForm.getInfo());
                    }
                }
                else if ((ACCESS_OPERATION_CODE)accessEvent.Argument == ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.WriteWait(
                            m_SelectedTagID, m_WriteForm.m_WriteParams, m_AntennaInfoForm.getInfo());
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.WriteEvent(
                            m_WriteForm.m_WriteParams, m_AccessFilterForm.getFilter(), m_AntennaInfoForm.getInfo());
                    }
                }
                else if ((ACCESS_OPERATION_CODE)accessEvent.Argument == ACCESS_OPERATION_CODE.ACCESS_OPERATION_LOCK)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.LockWait(
                            m_SelectedTagID, m_LockForm.m_LockParams, m_AntennaInfoForm.getInfo());
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.LockEvent(
                            m_LockForm.m_LockParams, m_AccessFilterForm.getFilter(), m_AntennaInfoForm.getInfo());
                    }
                }
                else if ((ACCESS_OPERATION_CODE)accessEvent.Argument == ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.KillWait(
                            m_SelectedTagID, m_KillForm.m_KillParams, m_AntennaInfoForm.getInfo());
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.KillEvent(
                            m_KillForm.m_KillParams, m_AccessFilterForm.getFilter(), m_AntennaInfoForm.getInfo());
                    }
                }
                else if ((ACCESS_OPERATION_CODE)accessEvent.Argument == ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_WRITE)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.BlockWriteWait(
                            m_SelectedTagID, m_BlockWriteForm.m_WriteParams, m_AntennaInfoForm.getInfo());
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.BlockWriteEvent(
                            m_BlockWriteForm.m_WriteParams, m_AccessFilterForm.getFilter(), m_AntennaInfoForm.getInfo());
                    }
                }
                else if ((ACCESS_OPERATION_CODE)accessEvent.Argument == ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.BlockEraseWait(
                            m_SelectedTagID, m_BlockEraseForm.m_BlockEraseParams, m_AntennaInfoForm.getInfo());
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.BlockEraseEvent(
                            m_BlockEraseForm.m_BlockEraseParams, m_AccessFilterForm.getFilter(), m_AntennaInfoForm.getInfo());
                    }
                }
            }
            catch (OperationFailureException ofe)
            {
                m_AccessOpResult.m_Result = ofe.Result;
            }
            accessEvent.Result = m_AccessOpResult;
        }

        private void accessBackgroundWorker_ProgressChanged(object sender,
            ProgressChangedEventArgs pce)
        {

        }

        private void accessBackgroundWorker_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs accessEvents)
        {
            if (accessEvents.Error != null)
            {
                functionCallStatusLabel.Text = accessEvents.Error.Message;
            }
            else
            {
                // Handle AccessWait Operations              
                AccessOperationResult accessOpResult = (AccessOperationResult)accessEvents.Result;
                if (accessOpResult.m_Result == RFIDResults.RFID_API_SUCCESS)
                {
                    if (accessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ)
                    {
                        if (m_SelectedTagID != String.Empty)
                        {
                            if (m_ReadTag.OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ &&
                                m_ReadTag.OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS)
                            {
                                ListViewItem item = inventoryList.SelectedItems[0];
                                string tagID = m_ReadTag.TagID + m_ReadTag.MemoryBank.ToString()
                                    + m_ReadTag.MemoryBankDataOffset.ToString();

                                if (item.SubItems[6].Text.Length > 0)
                                {
                                    bool isFound = false;

                                    // Search or add new one
                                    lock (m_TagTable.SyncRoot)
                                    {
                                        isFound = m_TagTable.ContainsKey(tagID);
                                    }

                                    if (!isFound)
                                    {
                                        ListViewItem newItem = new ListViewItem(m_ReadTag.TagID);
                                        ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(newItem, getTagEvent(m_ReadTag));
                                        m_TagTotalCount += m_ReadTag.TagSeenCount;
                                        newItem.SubItems.Add(subItem);
                                        subItem = new ListViewItem.ListViewSubItem(item, m_ReadTag.TagSeenCount.ToString());
                                        newItem.SubItems.Add(subItem);
                                        subItem = new ListViewItem.ListViewSubItem(item, m_ReadTag.AntennaID.ToString());
                                        newItem.SubItems.Add(subItem);
                                        subItem = new ListViewItem.ListViewSubItem(item, m_ReadTag.PeakRSSI.ToString());
                                        newItem.SubItems.Add(subItem);
                                        subItem = new ListViewItem.ListViewSubItem(item, m_ReadTag.PC.ToString("X"));
                                        newItem.SubItems.Add(subItem);
                                        subItem = new ListViewItem.ListViewSubItem(item, m_ReadTag.MemoryBankData);
                                        newItem.SubItems.Add(subItem);

                                        string memoryBank = m_ReadTag.MemoryBank.ToString();
                                        int index = memoryBank.LastIndexOf('_');
                                        if (index != -1)
                                        {
                                            memoryBank = memoryBank.Substring(index + 1);
                                        }

                                        subItem = new ListViewItem.ListViewSubItem(item, memoryBank);
                                        newItem.SubItems.Add(subItem);
                                        subItem = new ListViewItem.ListViewSubItem(item, m_ReadTag.MemoryBankDataOffset.ToString());
                                        newItem.SubItems.Add(subItem);

                                        inventoryList.BeginUpdate();
                                        inventoryList.Items.Add(newItem);
                                        inventoryList.EndUpdate();

                                        lock (m_TagTable.SyncRoot)
                                        {
                                            m_TagTable.Add(tagID, newItem);
                                        }
                                    }
                                    else
                                    {
                                        item.SubItems[6].Text = m_ReadTag.MemoryBankData;
                                        item.SubItems[8].Text = m_ReadTag.MemoryBankDataOffset.ToString();
                                    }
                                }
                                else
                                {
                                    // Empty Memory Bank Slot
                                    item.SubItems[6].Text = m_ReadTag.MemoryBankData;

                                    string memoryBank = m_ReadForm.m_ReadParams.MemoryBank.ToString();
                                    int index = memoryBank.LastIndexOf('_');
                                    if (index != -1)
                                    {
                                        memoryBank = memoryBank.Substring(index + 1);
                                    }
                                    item.SubItems[7].Text = memoryBank;
                                    item.SubItems[8].Text = m_ReadTag.MemoryBankDataOffset.ToString();

                                    lock (m_TagTable.SyncRoot)
                                    {
                                        m_TagTable.Remove(m_ReadTag.TagID);
                                        m_TagTable.Add(tagID, item);
                                    }
                                }
                                this.m_ReadForm.ReadData_TB.Text = m_ReadTag.MemoryBankData;
                                functionCallStatusLabel.Text = "Read Succeed";
                            }
                        }
                    }
                    else if (accessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE)
                    {
                        functionCallStatusLabel.Text = "Write Succeed";
                    }
                    else if (accessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_LOCK)
                    {
                        functionCallStatusLabel.Text = "Lock Succeed";
                    }
                    else if (accessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL)
                    {
                        functionCallStatusLabel.Text = "Kill Succeed";
                    }
                    else if (accessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_WRITE)
                    {
                        functionCallStatusLabel.Text = "BlockWrite Succeed";
                    }
                    else if (accessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE)
                    {
                        functionCallStatusLabel.Text = "BlockErase Succeed";
                    }
                }
                else
                {
                    functionCallStatusLabel.Text = accessOpResult.m_Result.ToString();
                }
                resetButtonState();
                memBank_CB.Enabled = true;
            }
        }

        private void resetButtonState()
        {
            if (m_ReadForm != null)
                m_ReadForm.readButton.Enabled = true;
            if (m_WriteForm != null)
                m_WriteForm.writeButton.Enabled = true;
            if (m_LockForm != null)
                m_LockForm.lockButton.Enabled = true;
            if (m_KillForm != null)
                m_KillForm.killButton.Enabled = true;
            if (m_BlockWriteForm != null)
                m_BlockWriteForm.writeButton.Enabled = true;
            if (m_BlockEraseForm != null)
                m_BlockEraseForm.eraseButton.Enabled = true;
        }

        internal void configureMenuItemsUponConnectDisconnect()
        {
            this.autonomous_CB.Enabled = this.m_IsConnected;
            this.memBank_CB.Enabled = this.m_IsConnected;
            this.capabilitiesToolStripMenuItem.Enabled = this.m_IsConnected;
            this.antennaInfoToolStripMenuItem.Enabled = this.m_IsConnected;
            this.antennaToolStripMenuItem.Enabled = this.m_IsConnected;
            this.rFModesToolStripMenuItem.Enabled = this.m_IsConnected;
            this.singulationToolStripMenuItem.Enabled = this.m_IsConnected;
            this.gpioToolStripMenuItem.Enabled = this.m_IsConnected;
            this.resetToFactoryDefaultsToolStripMenuItem.Enabled = this.m_IsConnected;
            this.storageSettingsToolStripMenuItem.Enabled = this.m_IsConnected;
            this.filtersToolStripMenuItem.Enabled = this.m_IsConnected;
            this.accessToolStripMenuItem.Enabled = this.m_IsConnected;
            this.triggersToolStripMenuItem.Enabled = this.m_IsConnected;
            if (this.m_ReaderAPI != null && this.m_IsConnected && this.m_ReaderAPI.ReaderCapabilities.IsRadioPowerControlSupported == true)
            {
                this.radioPowerGetSetToolStripMenuItem.Text = this.m_ReaderAPI.Config.RadioPowerState == RADIO_POWER_STATE.OFF ?
                    "Power On Radio" : "Power Off Radio";
            }
            else
            {
                this.radioPowerGetSetToolStripMenuItem.Visible = false;
            }

        }
        internal void configureMenuItemsUponLoginLogout()
        {
           this.softwareFirmwareUpdateToolStripMenuItem.Enabled = this.m_ReaderMgmt.IsLoggedIn;

            if (this.m_ReaderType != READER_TYPE.MC)
            {
                this.antennaModeToolStripMenuItem.Enabled = this.m_ReaderMgmt.IsLoggedIn;
                this.readPointToolStripMenuItem.Enabled = this.m_ReaderMgmt.IsLoggedIn;
                this.rebootToolStripMenuItem.Enabled = this.m_ReaderMgmt.IsLoggedIn;
                this.softwareFirmwareUpdateToolStripMenuItem.Enabled = this.m_ReaderMgmt.IsLoggedIn;
                this.systemInfoToolStripMenuItem.Enabled = this.m_ReaderMgmt.IsLoggedIn;
            }
            else
            {
                this.antennaModeToolStripMenuItem.Enabled = false;
                this.readPointToolStripMenuItem.Enabled = false;
                this.rebootToolStripMenuItem.Enabled = false;
                this.systemInfoToolStripMenuItem.Enabled = false;
            }
            //this.systemInfoToolStripMenuItem.Visible = false; // Dlg Not implemented now
        }
        internal void configureMenuItemsBasedOnCapabilities()
        {            
            this.autonomousMode_CB.Visible = this.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported;
            m_TriggerForm.Reset();  
            this.radioPowerGetSetToolStripMenuItem.Visible = this.m_ReaderAPI.ReaderCapabilities.IsRadioPowerControlSupported;
            this.gpioToolStripMenuItem.Visible = this.m_ReaderAPI.ReaderCapabilities.NumGPIPorts > 0 ? true : false |
            this.m_ReaderAPI.ReaderCapabilities.NumGPOPorts > 0 ? true : false; 
            this.blockEraseDataContextMenuItem.Visible = this.m_ReaderAPI.ReaderCapabilities.IsBlockEraseSupported;
            this.blockWriteDataContextMenuItem.Visible = this.m_ReaderAPI.ReaderCapabilities.IsBlockWriteSupported;
          

            this.m_TriggerForm.newTag_CB.Enabled =
            this.m_TriggerForm.newTag_TB.Enabled =
            this.m_TriggerForm.backTag_CB.Enabled =
            this.m_TriggerForm.backTag_TB.Enabled =
            this.m_TriggerForm.invisibleTag_CB.Enabled =
            this.m_TriggerForm.invisibleTag_TB.Enabled = this.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported;

        }

        private void connectBackgroundWorker_DoWork(object sender, DoWorkEventArgs workEventArgs)
        {
            connectBackgroundWorker.ReportProgress(0, workEventArgs.Argument);

            if ((string)workEventArgs.Argument == "Connect")
            {
                m_ReaderAPI = new RFIDReader(m_ConnectionForm.IpText, uint.Parse(m_ConnectionForm.PortText), 0);

                try
                {
                    m_ReaderAPI.Connect();
                    m_IsConnected = true;
                    workEventArgs.Result = "Connect Succeed";

                }
                catch (OperationFailureException operationException)
                {
                    workEventArgs.Result = operationException.Result;
                }
                catch (Exception ex)
                {
                    workEventArgs.Result = ex.Message;
                }
            }
            else if ((string)workEventArgs.Argument == "Disconnect")
            {
                try
                {
                    m_ReaderAPI.Disconnect();
                    m_IsConnected = false;
                    workEventArgs.Result = "Disconnect Succeed";

                }
                catch (OperationFailureException ofe)
                {
                    workEventArgs.Result = ofe.Result;
                }
            }
        }

        private void connectBackgroundWorker_ProgressChanged(object sender,
            ProgressChangedEventArgs progressEventArgs)
        {
            m_ConnectionForm.connectionButton.Enabled = false;
        }

        private void connectBackgroundWorker_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs connectEventArgs)
        {
            if (m_ConnectionForm.connectionButton.Text == "Connect")
            {
                if (connectEventArgs.Result.ToString() == "Connect Succeed")
                {
                    /*
                     *  UI Updates
                     */
                    m_ConnectionForm.connectionButton.Text = "Disconnect";
                    m_ConnectionForm.hostname_TB.Enabled = false;
                    m_ConnectionForm.port_TB.Enabled = false;
                    m_ConnectionForm.Close();
                    this.readButton.Enabled = true;
                    this.readButton.Text = "Start Reading";
                    blockEraseToolStripMenuItem.Enabled = m_ReaderAPI.ReaderCapabilities.IsBlockEraseSupported;
                    blockWriteToolStripMenuItem.Enabled = m_ReaderAPI.ReaderCapabilities.IsBlockWriteSupported;                      

                    /*
                     *  Events Registration
                     */
                    m_ReaderAPI.Actions.PreFilters.DeleteAll();

                    m_ReaderAPI.Events.ReadNotify += new Events.ReadNotifyHandler(Events_ReadNotify);
                    m_ReaderAPI.Events.AttachTagDataWithReadEvent = false;
                    m_ReaderAPI.Events.StatusNotify += new Events.StatusNotifyHandler(Events_StatusNotify);
                    m_ReaderAPI.Events.NotifyGPIEvent = true;
                    m_ReaderAPI.Events.NotifyAntennaEvent = true;
                    m_ReaderAPI.Events.NotifyReaderDisconnectEvent = true;
                    m_ReaderAPI.Events.NotifyBufferFullEvent = true;
                    m_ReaderAPI.Events.NotifyBufferFullWarningEvent = true;
                    m_ReaderAPI.Events.NotifyAccessStartEvent = true;
                    m_ReaderAPI.Events.NotifyAccessStopEvent = true;
                    m_ReaderAPI.Events.NotifyInventoryStartEvent = true;
                    m_ReaderAPI.Events.NotifyInventoryStopEvent = true;
                    m_ReaderAPI.Events.NotifyReaderExceptionEvent = true;

                    this.Text = "Connected to " + m_ConnectionForm.IpText;
                    this.connectionStatus.BackgroundImage =
                        global::CS_RFID3_Host_Sample2.Properties.Resources.connected;
                    configureMenuItemsUponConnectDisconnect();
                    configureMenuItemsBasedOnCapabilities();
                }
            }
            else if (m_ConnectionForm.connectionButton.Text == "Disconnect")
            {
                if (connectEventArgs.Result.ToString() == "Disconnect Succeed")
                {
                }
                this.Text = "CS_RFID3_Host_Sample2";
                this.connectionStatus.BackgroundImage =
                    global::CS_RFID3_Host_Sample2.Properties.Resources.disconnected;

                m_ConnectionForm.connectionButton.Text = "Connect";
                m_ConnectionForm.hostname_TB.Enabled = true;
                m_ConnectionForm.port_TB.Enabled = true;
                this.readButton.Enabled = false;
                this.readButton.Text = "Start Reading";
                configureMenuItemsUponConnectDisconnect();
                m_IsConnected = false;

            }
            functionCallStatusLabel.Text = connectEventArgs.Result.ToString();
            m_ConnectionForm.connectionButton.Enabled = true;

            updateGPIState();
           // readButton.PerformClick();
        }

        private void AppForm_Load(object sender, EventArgs e)
        {
            // GPI States
            for (int index = 0; index < 8; index++)
            {
                int tabIndex = 1;
                System.Windows.Forms.Label gpiStateLabel = new System.Windows.Forms.Label();
                gpiStateLabel.AutoSize = true;
                gpiStateLabel.BackColor = System.Drawing.Color.Transparent;
                gpiStateLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                gpiStateLabel.Location = new System.Drawing.Point(86 + (index * 33), 16);
                gpiStateLabel.Name = "GPI States " + index;
                gpiStateLabel.Size = new System.Drawing.Size(15, 15);
                gpiStateLabel.TabIndex = tabIndex++;
                gpiStateLabel.Text = "  ";
                autonomous_CB.Controls.Add(gpiStateLabel);
                m_GPIStateList.Add(gpiStateLabel);

                int name = index + 1;
                System.Windows.Forms.Label gpiStateNumLabel = new System.Windows.Forms.Label();
                gpiStateNumLabel.AutoSize = true;
                gpiStateNumLabel.Font = new System.Drawing.Font(
                    "Microsoft Sans Serif",
                    8.25F,
                    System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point,
                    ((byte)(0)));
                gpiStateNumLabel.Location = new System.Drawing.Point(86 + (index * 33), 38);
                gpiStateNumLabel.Name = "label" + index;
                gpiStateNumLabel.Size = new System.Drawing.Size(241, 13);
                gpiStateNumLabel.TabIndex = tabIndex++;
                gpiStateNumLabel.Text = name.ToString();
                autonomous_CB.Controls.Add(gpiStateNumLabel);
            }
            configureMenuItemsUponConnectDisconnect();
            //  connectBackgroundWorker.RunWorkerAsync("Connect ");        
        }

        private void AppForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (m_IsConnected)
                {
                    m_ReaderAPI.Disconnect();
                }
                m_ReaderMgmt.Dispose();
                this.Dispose();
            }
            catch (Exception ex)
            {
                functionCallStatusLabel.Text = ex.Message;
            }
        }

        private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_ConnectionForm.ShowDialog(this);
        }

        private void capabilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CapabilitiesForm capabilitiesForm = new CapabilitiesForm(this);
            capabilitiesForm.ShowDialog(this);
        }

        private void antennaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AntennaConfigForm antennaConfigForm = new AntennaConfigForm(this);
            antennaConfigForm.ShowDialog(this);
        }

        private void rFModesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RFModeForm RFModeForm = new RFModeForm(this);
            RFModeForm.ShowDialog(this);
        }

        private void singulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SingulationForm singulationForm = new SingulationForm(this);
            singulationForm.ShowDialog(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ReaderAPI != null && m_IsConnected)
            {
                m_ReaderAPI.Disconnect();
            }
            if (this.m_ReaderMgmt.IsLoggedIn)
            {
                m_ReaderMgmt.Logout();
            }
            this.Dispose();
        }

        private void storageSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_TagStorageSettingsForm)
            {
                m_TagStorageSettingsForm = new TagStorageSettingsForm(this);
            }
            m_TagStorageSettingsForm.ShowDialog(this);
        }

        private void gpioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GPIOForm gpio = new GPIOForm(this);
            gpio.ShowDialog(this);
        }

        private void preFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_PreFilterForm)
            {
                m_PreFilterForm = new PreFilterForm(this);
            }
            m_PreFilterForm.ShowDialog(this);
        }

        private void postfilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_PostFilterForm.ShowDialog(this);
        }

        private void accessfilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_AccessFilterForm.ShowDialog(this);
        }

        private void triggersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_TriggerForm)
            {
                m_TriggerForm = new TriggersForm(this);
            }
            m_TriggerForm.ShowDialog(this);
        }

        private void readToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                m_ReadForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                functionCallStatusLabel.Text = "Read Form:" + ex.Message;
            }
        }

        private void writeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_WriteForm)
            {
                m_WriteForm = new WriteForm(this, false);
            }
            m_WriteForm.ShowDialog(this);
        }

        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_LockForm)
            {
                m_LockForm = new LockForm(this);
            }
            m_LockForm.ShowDialog(this);
        }

        private void killToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_KillForm)
            {
                m_KillForm = new KillForm(this);
            }
            m_KillForm.ShowDialog(this);
        }

        private void blockWriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_BlockWriteForm)
            {
                m_BlockWriteForm = new WriteForm(this, true);
            }
            m_BlockWriteForm.ShowDialog(this);
        }

        private void blockEraseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_BlockEraseForm)
            {
                m_BlockEraseForm = new BlockEraseForm(this);
            }
            m_BlockEraseForm.ShowDialog(this);
        }

        private void softwareFirmwareUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_FirmwareUpdateForm)
            {
                m_FirmwareUpdateForm = new FirmwareUpdateForm(this);
            }
            m_FirmwareUpdateForm.ShowDialog(this);
        }

        private void antennaModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_AntennaModeForm)
            {
                m_AntennaModeForm = new AntennaModeForm(this);
            }
            m_AntennaModeForm.ShowDialog(this);
        }

        private void readPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_ReadPointForm)
            {
                m_ReadPointForm = new ReadPointForm(this);
            }
            m_ReadPointForm.ShowDialog(this);
        }

        private void radioPowerGetSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.radioPowerGetSetToolStripMenuItem.Text == "Power On Radio")
                {
                    m_ReaderAPI.Config.RadioPowerState = RADIO_POWER_STATE.OFF;
                }
                else
                {
                    m_ReaderAPI.Config.RadioPowerState = RADIO_POWER_STATE.ON;
                }
                this.radioPowerGetSetToolStripMenuItem.Text = this.m_ReaderAPI.Config.RadioPowerState == RADIO_POWER_STATE.OFF ?
                    "Power On Radio" : "Power Off Radio";
            }
            catch (OperationFailureException ex)
            {
                functionCallStatusLabel.Text = ex.Result.ToString();
            }
        }

        private void rebootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                m_ReaderMgmt.Restart();

                this.antennaModeToolStripMenuItem.Enabled = false;
                this.rebootToolStripMenuItem.Enabled = false;
                this.radioPowerGetSetToolStripMenuItem.Enabled = false;
                this.readPointToolStripMenuItem.Enabled = false;
                this.softwareFirmwareUpdateToolStripMenuItem.Enabled = false;
                this.m_IsConnected = false;

                m_LoginForm.loginButton.Text = "Login";
                functionCallStatusLabel.Text = "Reboot Successfully";
            }
            catch (OperationFailureException failureException)
            {
                functionCallStatusLabel.Text = failureException.Result.ToString();
            }           
        }

        private void loginLogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_LoginForm == null)
            {
                m_LoginForm = new LoginForm(this);
            }
            m_LoginForm.clearPassword();
            m_LoginForm.ShowDialog(this);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpDialog = new HelpForm(this);
            if (helpDialog.ShowDialog(this) == DialogResult.Yes)
            {

            }
            helpDialog.Dispose();
        }

        private void readButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_IsConnected)
                {
                    if (readButton.Text == "Start Reading")
                    {
                        if (m_ReaderAPI.Actions.TagAccess.OperationSequence.Length > 0)
                        {
                            m_ReaderAPI.Actions.TagAccess.OperationSequence.PerformSequence(m_AccessFilterForm.getFilter(),
                                m_TriggerForm.getTriggerInfo(), m_AntennaInfoForm.getInfo());
                        }
                        else
                        {
                            timer1 = new System.Windows.Forms.Timer();
                            timer1.Tick += new EventHandler(timer1_Tick);
                            timer1.Interval = 1000; // 1 second
                            timer1.Start();
                            label1.Text = counter.ToString();
                            inventoryList.Items.Clear();
                            m_TagTable.Clear();
                            m_TagTotalCount = 0;

                            m_ReaderAPI.Actions.Inventory.Perform(
                                m_PostFilterForm.getFilter(),
                                m_TriggerForm.getTriggerInfo(),
                                m_AntennaInfoForm.getInfo());
                        }

                        readButton.Text = "Stop Reading";
                        memBank_CB.Enabled = false;

                    }
                    else if (readButton.Text == "Stop Reading")
                    {
                        if (m_ReaderAPI.Actions.TagAccess.OperationSequence.Length > 0)
                        {
                            m_ReaderAPI.Actions.TagAccess.OperationSequence.StopSequence();
                        }
                        else
                        {
                            m_ReaderAPI.Actions.Inventory.Stop();
                            counter = 5;                            
                            double j1 = (summ1 / count1) * -1;
                            j1 = Math.Round(j1, 0, MidpointRounding.AwayFromZero);
                          //  Console.WriteLine("t1 A1="+j1);
                            double j2 = (summ2 / count2) * -1;
                            j2 = Math.Round(j2, 0, MidpointRounding.AwayFromZero);
                            double j3 = (summ3 / count3) * -1;
                            j3 = Math.Round(j3, 0, MidpointRounding.AwayFromZero);
                            double j4 = (summ4 / count4) * -1;
                            j4 = Math.Round(j4, 0, MidpointRounding.AwayFromZero);
                            double j5 = (summ5 / count5) * -1;
                            j5 = Math.Round(j5, 0, MidpointRounding.AwayFromZero);
                            double j6 = (summ6 / count6) * -1;
                            j6 = Math.Round(j6, 0, MidpointRounding.AwayFromZero);
                            double j7 = (summ7 / count7) * -1;
                            j7 = Math.Round(j7, 0, MidpointRounding.AwayFromZero);
                            double j8 = (summ8 / count8) * -1;
                            j8 = Math.Round(j8, 0, MidpointRounding.AwayFromZero);
                            double j9 = (summ9 / count9) * -1;
                            j9 = Math.Round(j9, 0, MidpointRounding.AwayFromZero);
                            double j10 = (summ10 / count10) * -1;
                            j10 = Math.Round(j10, 0, MidpointRounding.AwayFromZero);
                            double j11 = (summ11 / count11) * -1;
                            j11 = Math.Round(j11, 0, MidpointRounding.AwayFromZero);
                         
                            double j12 = (summ12 / count12) * -1;
                            j12 = Math.Round(j12, 0, MidpointRounding.AwayFromZero);
                            double j13 = (summ13 / count13) * -1;
                            j13 = Math.Round(j13, 0, MidpointRounding.AwayFromZero);
                            double j14 = (summ14 / count14) * -1;
                            j14 = Math.Round(j14, 0, MidpointRounding.AwayFromZero);
                            double j15 = (summ15 / count15) * -1;
                            j15 = Math.Round(j15, 0, MidpointRounding.AwayFromZero);
                            double j16 = (summ16 / count16) * -1;
                            j16 = Math.Round(j16, 0, MidpointRounding.AwayFromZero);
                            double j17 = (summ17 / count17) * -1;
                            j17 = Math.Round(j17, 0, MidpointRounding.AwayFromZero);
                            double j18 = (summ18 / count18) * -1;
                            j18 = Math.Round(j18, 0, MidpointRounding.AwayFromZero);
                            double j19 = (summ19 / count19) * -1;
                            j19 = Math.Round(j19, 0, MidpointRounding.AwayFromZero);
                            double j20 = (summ20 / count20) * -1;
                            j20 = Math.Round(j20, 0, MidpointRounding.AwayFromZero);
                            double j21 = (summ21 / count21) * -1;
                            j21 = Math.Round(j21, 0, MidpointRounding.AwayFromZero);
                            
                            double j22 = (summ22 / count22) * -1;
                            j22 = Math.Round(j22, 0, MidpointRounding.AwayFromZero);
                            double j23 = (summ23 / count23) * -1;
                            j23 = Math.Round(j23, 0, MidpointRounding.AwayFromZero);
                            double j24 = (summ24 / count24) * -1;
                            j24 = Math.Round(j24, 0, MidpointRounding.AwayFromZero);
                            double j25 = (summ25 / count25) * -1;
                            j25 = Math.Round(j25, 0, MidpointRounding.AwayFromZero);
                            double j26 = (summ26 / count26) * -1;
                            j26 = Math.Round(j26, 0, MidpointRounding.AwayFromZero);
                            double j27 = (summ27 / count27) * -1;
                            j27 = Math.Round(j27, 0, MidpointRounding.AwayFromZero);
                            double j28 = (summ28 / count28) * -1;
                            j28 = Math.Round(j28, 0, MidpointRounding.AwayFromZero);
                            double j29 = (summ29 / count29) * -1;
                            j29 = Math.Round(j29, 0, MidpointRounding.AwayFromZero);
                            double j30 = (summ30 / count30) * -1;
                            j30 = Math.Round(j30, 0, MidpointRounding.AwayFromZero);
                            double j31 = (summ31 / count31) * -1;
                            j31 = Math.Round(j31, 0, MidpointRounding.AwayFromZero);
                           
                            double j32 = (summ32 / count32) * -1;
                            j32 = Math.Round(j32, 0, MidpointRounding.AwayFromZero);
                            double j33 = (summ33 / count33) * -1;
                            j33 = Math.Round(j33, 0, MidpointRounding.AwayFromZero);
                            double j34 = (summ34 / count34) * -1;
                            j34 = Math.Round(j34, 0, MidpointRounding.AwayFromZero);
                            double j35 = (summ35 / count35) * -1;
                            j35 = Math.Round(j35, 0, MidpointRounding.AwayFromZero);
                            double j36 = (summ36 / count36) * -1;
                            j36 = Math.Round(j36, 0, MidpointRounding.AwayFromZero);
                            double j37 = (summ37 / count37) * -1;
                            j37 = Math.Round(j37, 0, MidpointRounding.AwayFromZero);
                            double j38 = (summ38 / count38) * -1;
                            j38 = Math.Round(j38, 0, MidpointRounding.AwayFromZero);
                            double j39 = (summ39 / count39) * -1;
                            j39 = Math.Round(j39, 0, MidpointRounding.AwayFromZero);
                            double j40 = (summ40 / count40) * -1;
                            j40 = Math.Round(j40, 0, MidpointRounding.AwayFromZero);

                            sbee1.AppendFormat("{0},", j1);
                            sbee1.AppendFormat("{0},", j11);
                            sbee1.AppendFormat("{0},", j21);
                            sbee1.AppendFormat("{0},", j31);
                            sbee1.AppendLine();
                            sbee1.AppendFormat("{0},", j2);
                            sbee1.AppendFormat("{0},", j12);
                            sbee1.AppendFormat("{0},", j22);
                         //   sbee1.AppendFormat("{0},", j32);
                            sbee1.AppendLine();
                            sbee1.AppendFormat("{0},", j3);
                            sbee1.AppendFormat("{0},", j13);
                            sbee1.AppendFormat("{0},", j23);
                         //   sbee1.AppendFormat("{0},", j33);
                            sbee1.AppendLine();
                            sbee1.AppendFormat("{0},", j4);
                            sbee1.AppendFormat("{0},", j14);
                            sbee1.AppendFormat("{0},", j24);
                        //    sbee1.AppendFormat("{0},", j34);
                            sbee1.AppendLine();
                            sbee1.AppendFormat("{0},", j5);
                            sbee1.AppendFormat("{0},", j15);
                            sbee1.AppendFormat("{0},", j25);
                         //   sbee1.AppendFormat("{0},", j35);
                            sbee1.AppendLine();
                            sbee1.AppendFormat("{0},", j6);
                            sbee1.AppendFormat("{0},", j16);
                            sbee1.AppendFormat("{0},", j26);
                         //   sbee1.AppendFormat("{0},", j36);
                            sbee1.AppendLine();
                            sbee1.AppendFormat("{0},", j7);
                            sbee1.AppendFormat("{0},", j17);
                            sbee1.AppendFormat("{0},", j27);
                        //    sbee1.AppendFormat("{0},", j37);
                            sbee1.AppendLine();
                            sbee1.AppendFormat("{0},", j8);
                            sbee1.AppendFormat("{0},", j18);
                            sbee1.AppendFormat("{0},", j28);
                          //  sbee1.AppendFormat("{0},", j38);
                            sbee1.AppendLine();
                            sbee1.AppendFormat("{0},", j9);
                            sbee1.AppendFormat("{0},", j19);
                            sbee1.AppendFormat("{0},", j29);
                       //     sbee1.AppendFormat("{0},", j39);
                            sbee1.AppendLine();
                            sbee1.AppendFormat("{0},", j10);
                            sbee1.AppendFormat("{0},", j20);
                            sbee1.AppendFormat("{0},", j30);
                      //      sbee1.AppendFormat("{0},", j40);
                            sbee1.AppendLine();
                           
                            

                            /*    double R1, R2, R3, R4;
                                double mina1, mina2, mina3;

                                mina1 = ((-min1) + (-min2) + (-min3) + (-mine1))/4;
                                mina2 = ((-min4) + (-min5) + (-min6) + (-mine2))/4;
                                mina3 = ((-min7) + (-min8) + (-min9) + (-mine3))/4;
                                mina1 = Math.Round(mina1, 0, MidpointRounding.AwayFromZero);
                                Console.WriteLine("min1=" + mina1);
                                mina2 = Math.Round(mina2, 0, MidpointRounding.AwayFromZero);
                                Console.WriteLine("min2=" + mina2);
                                mina3 = Math.Round(mina3, 0, MidpointRounding.AwayFromZero);
                                Console.WriteLine("min3=" + mina3);
                              R1 = (j1 + j5 + j9 + j13) / 4;
                                R2 = (j2 + j6 + j10 + j14) / 4;
                                R3 = (j3 + j7 + j11 + j15) / 4;
                                R4 = (j4 + j8 + j12 + j16) / 4;
                                R1 = Math.Round(R1, 0, MidpointRounding.AwayFromZero);
                                Console.WriteLine("R1="+ R1);
                                R2 = Math.Round(R2, 0, MidpointRounding.AwayFromZero);
                                Console.WriteLine("R2=" + R2);
                                R3 = Math.Round(R3, 0, MidpointRounding.AwayFromZero);
                                Console.WriteLine("R3=" + R3);
                                R4 = Math.Round(R4, 0, MidpointRounding.AwayFromZero);
                                Console.WriteLine("R4=" + R4);*/
                            //   sbee1.AppendFormat("{0},", R1);
                            //  sbee1.AppendFormat("{0},", R2);
                            //  sbee1.AppendFormat("{0},", R3);
                            // sbee2.AppendFormat("{0},", mina1);
                            //  sbee2.AppendFormat("{0},", mina2);
                            //  sbee2.AppendFormat("{0},", mina3);
                            //   sbee1.AppendFormat("{0},", R4);

                            //  sbee2.AppendLine();
                            System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new1\\calibration.csv", sbee1.ToString());
                            sbee1.Remove(0, sbee1.Length);
                            //   System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new1\\min.csv", sbee2.ToString());

                            System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new\\tag1\\antenna1.csv", sb1.ToString());
                            sb1.Remove(0,sb1.Length);
                            System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new\\tag1\\antenna2.csv", sb4.ToString());
                            sb4.Remove(0,sb4.Length);
                            System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new\\tag1\\antenna3.csv", sb7.ToString());
                            sb7.Remove(0,sb7.Length);
                            System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new\\tag1\\antenna4.csv", sb10.ToString());
                            sb10.Remove(0, sb10.Length);
                            System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new\\tag3\\antenna1.csv", sb2.ToString());
                            sb2.Remove(0, sb2.Length);
                            System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new\\tag3\\antenna2.csv", sb5.ToString());
                            sb5.Remove(0,sb5.Length);
                            System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new\\tag3\\antenna3.csv", sb8.ToString());
                            sb8.Remove(0, sb8.Length);
                            System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new\\tag3\\antenna4.csv", sb11.ToString());
                            sb11.Remove(0, sb11.Length);
                            System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new\\tag8\\antenna1.csv", sb3.ToString());
                            sb3.Remove(0, sb3.Length);
                            System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new\\tag8\\antenna2.csv", sb6.ToString());
                            sb6.Remove(0, sb6.Length);
                            System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new\\tag8\\antenna3.csv", sb9.ToString());
                            sb9.Remove(0, sb9.Length);
                            System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new\\tag8\\antenna4.csv", sb12.ToString());
                            sb12.Remove(0, sb12.Length);

                            //     wb.Close();
                            //    xls.Quit();
                            //    System.Runtime.InteropServices.Marshal.ReleaseComObject(xls);
                            //   backgroundWorker1.CancelAsync();
                        }

                        readButton.Text = "Start Reading";
                        memBank_CB.Enabled = true;
                    }
                }
                else
                {
                    functionCallStatusLabel.Text = "Please connect to a reader first";
                }
            }
            catch (InvalidOperationException ioe)
            {
                functionCallStatusLabel.Text = ioe.Message;
            }
            catch (InvalidUsageException iue)
            {
                functionCallStatusLabel.Text = iue.Info;
            }
            catch (OperationFailureException ofe)
            {
                functionCallStatusLabel.Text = ofe.Result + ":" + ofe.StatusDescription;
            }
            catch (Exception ex)
            {
                functionCallStatusLabel.Text = ex.Message;
            }
        }

        void inventoryList_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataContextMenuStrip.Show(inventoryList, new System.Drawing.Point(e.X, e.Y));
            }
        }

        void inventoryList_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            if (e.Column != this.m_SortColumn)
            {
                m_SortColumn = e.Column;
                inventoryList.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (inventoryList.Sorting == SortOrder.Ascending)
                    inventoryList.Sorting = SortOrder.Descending;
                else
                    inventoryList.Sorting = SortOrder.Ascending;
            }
            this.inventoryList.Sort();
            this.inventoryList.ListViewItemSorter = new ListViewItemComparer(e.Column, inventoryList.Sorting);
        }

        private void tagDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TagDataForm tagDataForm = new TagDataForm(this);
            tagDataForm.ShowDialog(this);
        }

        private void readDataContextMenuItem_Click(object sender, EventArgs e)
        {
            m_ReadForm.ShowDialog(this);
        }

        private void writeDataContextMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_WriteForm)
            {
                m_WriteForm = new WriteForm(this, false);
            }
            m_WriteForm.ShowDialog(this);
        }

        private void lockDataContextMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_LockForm)
            {
                m_LockForm = new LockForm(this);
            }
            m_LockForm.ShowDialog(this);
        }

        private void killDataContextMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_KillForm)
            {
                m_KillForm = new KillForm(this);
            }
            m_KillForm.ShowDialog(this);
        }

        private void blockWriteDataContextMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_BlockWriteForm)
            {
                m_BlockWriteForm = new WriteForm(this, true);
            }
            m_BlockWriteForm.ShowDialog(this);
        }

        private void blockEraseDataContextMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_BlockEraseForm)
            {
                m_BlockEraseForm = new BlockEraseForm(this);
            }
            m_BlockEraseForm.ShowDialog(this);
        }

        private void updateGPIState()
        {
            try
            {
                if (m_IsConnected)
                {
                    for (int index = 0; index < 8; index++)
                    {
                        if (index < m_ReaderAPI.ReaderCapabilities.NumGPIPorts)
                        {
                            System.Windows.Forms.Label gpiLabel = (System.Windows.Forms.Label)m_GPIStateList[index];
                            GPIs.GPI_PORT_STATE portState = m_ReaderAPI.Config.GPI[index + 1].PortState;

                            if (portState == GPIs.GPI_PORT_STATE.GPI_PORT_STATE_HIGH)
                            {
                                gpiLabel.BackColor = System.Drawing.Color.GreenYellow;
                            }
                            else if (portState == GPIs.GPI_PORT_STATE.GPI_PORT_STATE_LOW)
                            {
                                gpiLabel.BackColor = System.Drawing.Color.Red;
                            }
                            else if (portState == GPIs.GPI_PORT_STATE.GPI_PORT_STATE_UNKNOWN)
                            {

                            }
                        }
                    }
                }
                else
                {
                    for (int index = 0; index < 8; index++)
                    {
                        ((System.Windows.Forms.Label)m_GPIStateList[index]).BackColor = System.Drawing.Color.Transparent;
                    }
                }
            }
            catch (Exception ex)
            {
                functionCallStatusLabel.Text = ex.Message;
            }
        }

        private void resetToFactoryDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_ReaderAPI.IsConnected)
                {
                    m_ReaderAPI.Config.ResetFactoryDefaults();
                    if(m_TagStorageSettingsForm != null)
                        m_TagStorageSettingsForm.Reset();
                    functionCallStatusLabel.Text = "Reset Factory Defaults Successfully";
                }
            }
            catch (Exception ex)
            {
                functionCallStatusLabel.Text = ex.Message;
            }
        }

        private void clearReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.inventoryList.Items.Clear();
            this.m_TagTable.Clear();
        }

        private void antennaInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_AntennaInfoForm.ShowDialog(this);
        }

        private void memBank_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_IsConnected)
            {
                m_ReaderAPI.Actions.TagAccess.OperationSequence.DeleteAll();
                if (memBank_CB.SelectedIndex >= 1)
                {
                    TagAccess.Sequence.Operation op = new TagAccess.Sequence.Operation();
                    op.AccessOperationCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;
                    op.ReadAccessParams.MemoryBank = (MEMORY_BANK)memBank_CB.SelectedIndex - 1;
                    op.ReadAccessParams.ByteCount = 0;
                    op.ReadAccessParams.ByteOffset = m_ReadForm.m_ReadParams.ByteOffset;
                    op.ReadAccessParams.AccessPassword = m_ReadForm.m_ReadParams.AccessPassword;
                    m_ReaderAPI.Actions.TagAccess.OperationSequence.Add(op);
                }
            }
        }

        private void systemInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_ReaderInfoForm)
            {
                m_ReaderInfoForm = new ReaderInfoForm(this);
            }
            m_ReaderInfoForm.ShowDialog(this);
        }

        private void autonomousCB_CheckedChanged(object sender, EventArgs e)
        {
            if (m_IsConnected)
            {
                autonomousMode_CB.Checked = autonomousMode_CB.Checked &&
                (m_IsConnected &&
                    m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported);
            }
        }

        private void clearReports_CB_CheckedChanged(object sender, EventArgs e)
        {
            this.totalTagValueLabel.Text = "0(0)";
            this.inventoryList.Items.Clear();
            this.m_TagTable.Clear();          
            clearReports_CB.Checked = false;      
        }

        private void AppForm_ClientSizeChanged(object sender, EventArgs e)
        {
            functionCallStatusLabel.Width = this.Width - 77;
        }

        private string getTagEvent(TagData tag)
        {
            string eventString = "None";
            if (tag.TagEvent != TAG_EVENT.NONE)
            {
                switch (tag.TagEvent)
                {
                    case TAG_EVENT.NEW_TAG_VISIBLE:
                        eventString = "New";
                        break;
                    case TAG_EVENT.TAG_BACK_TO_VISIBILITY:
                        eventString = "Back";
                        break;
                    case TAG_EVENT.TAG_NOT_VISIBLE:
                        eventString = "Gone";
                        break;
                    default:
                        eventString = "None";
                        break;

                }
                
            }
            return eventString;
        }

        private void locateTagDataContextMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_LocateForm)
            {
                m_LocateForm = new LocateForm(this);
            }
            m_LocateForm.ShowDialog(this);
        }
        public void insertToXlFile(string tagId, string antId, uint count, string rssi)
        {
          /*  string time2 = "none";
            DateTime localDate1 = DateTime.Now;
            
          //  time2 = localDate1.ToString();
            
            string time = localDate1.ToString("hh.mm.ss.f tt");
            xls.Visible = false;
            ws.Cells[1, 1] = "tagId";
            ws.Cells[1, 2] = "antId";
            ws.Cells[1, 3] = "rssi";
            ws.Cells[count, 1] = tagId;
            ws.Cells[count, 2] = antId;
            ws.Cells[count, 3] = rssi;
            ws.Cells[1, 4] = "timestamp";
            ws.Cells[count, 4] = time;
            wb.Save();
            */
        }

        string MyConn = "datasource =localhost;port=3306;username=root;password=admin";

public void insertDatabase(string tagID, string antennaID, string rssi)
        {

            MySqlConnection myconn3 = new MySqlConnection(MyConn);
            string time2 = "none";
            DateTime localDate1 = DateTime.Now;
            time2 = localDate1.ToString();
            MySqlDataReader myReader3;
            myconn3.Open();

            try
            {
                string Query1 = "insert into process_id_001.rfid_data(tag_id,ant_id,assembly_checkin_datetime,rssi) values('" + tagID + "','" + antennaID + "','" + time2 + "','" + rssi + "')";
                MySqlCommand MyCommand = new MySqlCommand(Query1, myconn3);
                myReader3 = MyCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            myconn3.Close();
            return;
        }
        struct DataParameter
        {
            public string tagid;
            public string antennaID;
            public uint count;
            public string rssi;
        }
       // private DataParameter _inputparameter;

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //  uint count = ((DataParameter)e.Argument).count;
                string time2 = "none";
                DateTime localDate1 = DateTime.Now;
                time2 = localDate1.ToString("h.mm.ss.f tt "); // 7.00 AM // 12 hour clock
                                                               // string sheetname = "Sheet1";
                                                               //  Workbook wb = xls.Workbooks.Open(@"C:\Users\Thippeswamy\Desktop\mydata.xlsx");
                                                               //  Worksheet ws = String.IsNullOrEmpty(sheetname) ? (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet : (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[sheetname];

                //  Workbook wb = xls.Workbooks.Add(XlSheetType.xlWorksheet);
                /*Worksheet ws = (Worksheet)xls.ActiveSheet;
                xls.Visible = false;
                ws.Cells[1, 1] = "tagId";
                ws.Cells[1, 2] = "antId";
                ws.Cells[1, 3] = "rssi";
                ws.Cells[1, 4] = "timestamp";
                ws.Cells[ccount, 1] = ((DataParameter)e.Argument).tagid;
                ws.Cells[ccount, 2] = ((DataParameter)e.Argument).antennaID;
                ws.Cells[ccount, 3] = ((DataParameter)e.Argument).rssi;
                ws.Cells[ccount, 4] = time2;
                wb.Save();*/
                //   string timestamp = time2 + ".xlsx";
                //   wb.SaveAs(@"C:\Users\Thippeswamy\Desktop\" + timestamp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ccount++;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter--;
            label1.Text = counter.ToString();
            if (counter == 0)
            {
                timer1.Stop();

                readButton.PerformClick();
            }
        }
    }
     
    }
