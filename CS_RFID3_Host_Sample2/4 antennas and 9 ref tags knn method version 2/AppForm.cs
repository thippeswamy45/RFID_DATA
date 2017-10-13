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
        StringBuilder sbee1 = new StringBuilder();
        public int counter = 4;
        double[] summ = new double[41];
        double[] countt = new double[41];
        double[] j = new double[41];
       public string tag1, tag15, tag3, tag4, tag5, tag6, tag11, tag8, tag13, tag7;
      
                           
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
            tag1 = "E280116060000205285A8BDE";
            tag3 = "E280116060000205285A63C0";
            tag4 = "E2002083950C01432820026A";
            tag5 = "987654328509025716906595";
            tag6 = "E280116060000205285A1D10";
            tag7 = "E280116060000205285BFA50";
            tag8 = "E2801160600002052859F172";
            tag11 = "222222222222222222222222";
            tag13 = "E2801160600002052859E8AF";
            tag15 = "E280116060000205285A9B9E";
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
                        //    if (!backgroundWorker1.IsBusy)
                        //    {
                       //         backgroundWorker1.RunWorkerAsync(_inputparameter);
                        //    }
                           // Console.WriteLine(m_TagTotalCount);
                     //       string time2;
                      //      DateTime localDate1 = DateTime.Now;
                     //       time2 = localDate1.ToString("h.mm.ss.fff");                            
                            switch (tag.AntennaID)
                            {
                                case 1:if (tagID == tag1)
                                    {

                                        summ[1] += tag.PeakRSSI;

                                        countt[1]++;
                                        

                                    }
                                 
                                    else if (tagID == tag3)
                                    {
                                       
                                        summ[2] += tag.PeakRSSI;
                                        countt[2]++;
                                       
                                    }
                                    else if (tagID == tag4)
                                    {
                                       
                                        summ[3] += tag.PeakRSSI;
                                        countt[3]++;
                                       
                                    }
                                    else if (tagID == tag5)
                                    {
                                        summ[4] += tag.PeakRSSI;
                                        countt[4]++;
                                    }
                                    else if (tagID == tag6)
                                    {
                                        summ[5] += tag.PeakRSSI;
                                        countt[5]++;
                                    }
                                    else if (tagID == tag7)
                                    {
                                        summ[6] += tag.PeakRSSI;
                                        countt[6]++;
                                    }
                                    else if (tagID == tag8)
                                    {
                                        summ[7] += tag.PeakRSSI;
                                        countt[7]++;
                                    }
                                    else if (tagID == tag11)
                                    {
                                        summ[8] += tag.PeakRSSI;

                                        countt[8]++;
                                    }
                                    else if (tagID == tag13)
                                    {
                                        summ[9] += tag.PeakRSSI;
                                        countt[9]++;
                                    }                                    
                                     else if (tagID == tag15)

                                    {
                                        summ[10] += tag.PeakRSSI;
                                        countt[10]++;
                                    }
                                    break;
                                case 2:
                                    if (tagID == tag1)
                                    {
                                       
                                        summ[11] += tag.PeakRSSI;
                                        countt[11]++;
                                       
                                    }
                                  
                                    else if (tagID == tag3)
                                    {
                                       
                                        summ[12] += tag.PeakRSSI;
                                        countt[12]++;
                                       
                                    }
                                    else if(tagID == tag4)
                                    {
                                       
                                        summ[13] += tag.PeakRSSI;
                                        countt[13]++;
                                       
                                    }
                                    else if (tagID == tag5)
                                    {
                                        summ[14] += tag.PeakRSSI;
                                        countt[14]++;
                                    }
                                    else if (tagID == tag6)
                                    {
                                        summ[15] += tag.PeakRSSI;
                                        countt[15]++;
                                    }
                                    else if (tagID == tag7)
                                    {
                                        summ[16] += tag.PeakRSSI;
                                        countt[16]++;
                                    }
                                    else if (tagID == tag8)
                                    {
                                        summ[17] += tag.PeakRSSI;
                                        countt[17]++;
                                    }
                                    else if (tagID == tag11)
                                    {
                                        summ[18] += tag.PeakRSSI;
                                        countt[18]++;
                                    }
                                    else if (tagID == tag13)
                                    {
                                        summ[19] += tag.PeakRSSI;
                                        countt[19]++;
                                    }

                                    else if (tagID == tag15)
                                    {
                                        summ[20] += tag.PeakRSSI;
                                        countt[20]++;
                                    }
                                    break;
                                case 3:
                                    if (tagID == tag1)
                                    {
                                       
                                        summ[21] += tag.PeakRSSI;
                                        countt[21]++;
                                      
                                    }
                                  
                                    else if (tagID == tag3)
                                    {
                                        
                                        summ[22] += tag.PeakRSSI;
                                        countt[22]++;
                                       
                                    }
                                    else if (tagID == tag4)
                                    {
                                        
                                        summ[23] += tag.PeakRSSI;
                                        countt[23]++;
                                      
                                    }
                                     else if (tagID == tag5)
                                    {
                                        summ[24] += tag.PeakRSSI;
                                        countt[24]++;
                                    }
                                    else if (tagID == tag6)
                                    {
                                        summ[25] += tag.PeakRSSI;
                                        countt[25]++;
                                    }
                                    else if (tagID == tag7)
                                    {
                                        summ[26] += tag.PeakRSSI;
                                        countt[26]++;
                                    }
                                    else if (tagID == tag8)
                                    {
                                        summ[27] += tag.PeakRSSI;
                                        countt[27]++;
                                    }
                                    else if (tagID == tag11)
                                    {
                                        summ[28] += tag.PeakRSSI;
                                        countt[28]++;
                                    }
                                    else if (tagID == tag13)
                                    {
                                        summ[29] += tag.PeakRSSI;
                                        countt[29]++;
                                    }
                                    else if (tagID == tag15)
                                    {
                                        summ[30] += tag.PeakRSSI;
                                        countt[30]++;
                                    }
                                    break;
                                case 4:
                                    if (tagID == tag1)
                                    {
                                       
                                        summ[31] += tag.PeakRSSI;
                                        countt[31]++;
                                       
                                    }
                                   
                                    else if (tagID == tag3)
                                    {
                                       
                                        summ[32] += tag.PeakRSSI;
                                        countt[32]++;
                                       
                                    }
                                    else if(tagID == tag4)
                                    {
                                       
                                        summ[33] += tag.PeakRSSI;
                                        countt[33]++;
                                       
                                    }
                                     else if (tagID == tag5)
                                    {
                                        summ[34] += tag.PeakRSSI;
                                        countt[34]++;
                                    }
                                    else if (tagID == tag6)
                                    {
                                        summ[35] += tag.PeakRSSI;
                                        countt[35]++;
                                    }
                                    else if (tagID == tag7)
                                    {
                                        summ[36] += tag.PeakRSSI;
                                        countt[36]++;
                                    }
                                    else if (tagID == tag8)
                                    {
                                        summ[37] += tag.PeakRSSI;
                                        countt[37]++;
                                    }
                                    else if (tagID == tag11)
                                    {
                                        summ[38] += tag.PeakRSSI;
                                        countt[38]++;
                                    }
                                    else if (tagID == tag13)
                                    {
                                        summ[39] += tag.PeakRSSI;
                                        countt[39]++;
                                    }
                                    else if (tagID == tag15)
                                    {
                                        summ[40] += tag.PeakRSSI;
                                        countt[40]++;
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
                            for(int i=0;i<summ.Length;i++)
                            {
                                summ[i] = 0.0;
                                countt[i] = 0.0;
                                j[i] = 0.0;
                            }
                          

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
                            counter = 4;                            
                             j[1 ]= (summ[1] / countt[1]) * -1;

                           // j[1] = Math.Round(j[1], 3, MidpointRounding.AwayFromZero);
                          
                             j[2] = (summ[2] / countt[2]) * -1;
                            //j[2] = Math.Round(j[2], 3, MidpointRounding.AwayFromZero);
                             j[3 ]= (summ[3] / countt[3]) * -1;
                          //  j[3] = Math.Round(j[3], 3, MidpointRounding.AwayFromZero);
                            j[4 ]= (summ[4] / countt[4]) * -1;
                          //  j[4 ]= Math.Round(j[4], 3, MidpointRounding.AwayFromZero);
                             j[5] = (summ[5] / countt[5]) * -1;
                         //   j[5] = Math.Round(j[5], 3, MidpointRounding.AwayFromZero);
                             j[6] = (summ[6] / countt[6]) * -1;
                          //  j[6] = Math.Round(j[6], 3, MidpointRounding.AwayFromZero);
                             j[7] = (summ[7] / countt[7]) * -1;
                          //  j[7] = Math.Round(j[7], 3, MidpointRounding.AwayFromZero);
                             j[8] = (summ[8] / countt[8]) * -1;
                          //  j[8] = Math.Round(j[8], 3, MidpointRounding.AwayFromZero);
                             j[9] = (summ[9] / countt[9]) * -1;
                          //  j[9] = Math.Round(j[9], 3, MidpointRounding.AwayFromZero);
                             j[10] = (summ[10] / countt[10]) * -1;
                         //   j[10] = Math.Round(j[10], 3, MidpointRounding.AwayFromZero);
                             j[11] = (summ[11] / countt[11]) * -1;
                          //  j[11] = Math.Round(j[11], 3, MidpointRounding.AwayFromZero);
                         
                             j[12] = (summ[12] / countt[12]) * -1;
                         //   j[12] = Math.Round(j[12], 3, MidpointRounding.AwayFromZero);
                             j[13] = (summ[13] / countt[13]) * -1;
                          //  j[13 ]= Math.Round(j[13], 3, MidpointRounding.AwayFromZero);
                             j[14] = (summ[14] / countt[14]) * -1;
                          //  j[14] = Math.Round(j[14], 3, MidpointRounding.AwayFromZero);
                             j[15] = (summ[15] / countt[15]) * -1;
                         //   j[15] = Math.Round(j[15], 3, MidpointRounding.AwayFromZero);
                             j[16] = (summ[16] / countt[16]) * -1;
                         //   j[16] = Math.Round(j[16], 3, MidpointRounding.AwayFromZero);
                             j[17] = (summ[17] / countt[17]) * -1;
                        //    j[17] = Math.Round(j[17], 3, MidpointRounding.AwayFromZero);
                             j[18] = (summ[18] / countt[18]) * -1;
                         //   j[18] = Math.Round(j[18], 3, MidpointRounding.AwayFromZero);
                             j[19] = (summ[19] / countt[19]) * -1;
                         //   j[19] = Math.Round(j[19], 3, MidpointRounding.AwayFromZero);
                             j[20] = (summ[20] / countt[20]) * -1;
                        //    j[20] = Math.Round(j[20], 3, MidpointRounding.AwayFromZero);
                             j[21] = (summ[21] / countt[21]) * -1;
                        //    j[21] = Math.Round(j[21], 3, MidpointRounding.AwayFromZero);
                            
                            j[22] = (summ[22] / countt[22]) * -1;
                        //    j[22] = Math.Round(j[22], 3, MidpointRounding.AwayFromZero);
                           j[23] = (summ[23] / countt[23]) * -1;
                         //   j[23] = Math.Round(j[23], 3, MidpointRounding.AwayFromZero);
                             j[24] = (summ[24] / countt[24]) * -1;
                         //   j[24] = Math.Round(j[24], 3, MidpointRounding.AwayFromZero);
                             j[25] = (summ[25] / countt[25]) * -1;
                        //    j[25] = Math.Round(j[25], 3, MidpointRounding.AwayFromZero);
                             j[26] = (summ[26] / countt[26]) * -1;
                          //  j[26] = Math.Round(j[26], 3, MidpointRounding.AwayFromZero);
                            j[27] = (summ[27] / countt[27]) * -1;
                         //   j[27] = Math.Round([j[27], 3, MidpointRounding.AwayFromZero);
                             j[28] = (summ[28] / countt[28]) * -1;
                         //   j[28] = Math.Round(j[28], 3, MidpointRounding.AwayFromZero);
                             j[29] = (summ[29] / countt[29]) * -1;
                         //   j[29] = Math.Round(j[29], 3, MidpointRounding.AwayFromZero);
                             j[30] = (summ[30 ]/ countt[30]) * -1;
                         //   j[30] = Math.Round(j[30], 3, MidpointRounding.AwayFromZero);
                             j[31] = (summ[31] / countt[31]) * -1;
                         //   j[31] = Math.Round(j[31], 3, MidpointRounding.AwayFromZero);
                           
                             j[32] = (summ[32] / countt[32]) * -1;
                          //  j[32] = Math.Round(j[32], 3, MidpointRounding.AwayFromZero);
                             j[33] = (summ[33] / countt[33]) * -1;
                         //   j[33] = Math.Round(j[33], 3, MidpointRounding.AwayFromZero);
                             j[34] = (summ[34] / countt[34]) * -1;
                          //  j[34] = Math.Round(j[34], 3, MidpointRounding.AwayFromZero);
                             j[35] = (summ[35] / countt[35]) * -1;
                         //   j[35] = Math.Round(j[35], 3, MidpointRounding.AwayFromZero);
                             j[36] = (summ[36] / countt[36]) * -1;
                        //   j[36] = Math.Round(j[36], 3, MidpointRounding.AwayFromZero);
                             j[37] = (summ[37] / countt[37]) * -1;
                         //   j[37] = Math.Round(j[37], 3, MidpointRounding.AwayFromZero);
                            j[38] = (summ[38] / countt[38]) * -1;
                        ///    j[38] = Math.Round(j[38], 3, MidpointRounding.AwayFromZero);
                             j[39] = (summ[39] / countt[39]) * -1;
                        //    j[39] = Math.Round(j[39], 3, MidpointRounding.AwayFromZero);
                             j[40] = (summ[40] / countt[40]) * -1;
                            //    j[40] = Math.Round(j[40], 3, MidpointRounding.AwayFromZero);



                                sbee1.AppendFormat("{0},", j[1]);//tag1
                                sbee1.AppendFormat("{0},", j[11]);
                                sbee1.AppendFormat("{0},", j[21]);
                                sbee1.AppendFormat("{0},", j[31]);
                                sbee1.AppendLine();
                            /*  
                               sbee1.AppendFormat("{0},", j[2]);//tag3
                                sbee1.AppendFormat("{0},", j[12]);
                                sbee1.AppendFormat("{0},", j[22]);
                                sbee1.AppendFormat("{0},", j[32]);
                                sbee1.AppendLine();
                              sbee1.AppendFormat("{0},", j[3]);//tag4
                              sbee1.AppendFormat("{0},", j[13]);
                              sbee1.AppendFormat("{0},", j[23]);
                              sbee1.AppendFormat("{0},", j[33]);
                              sbee1.AppendLine();*/
                            /* sbee1.AppendFormat("{0},", j[4]);//tag5
                                sbee1.AppendFormat("{0},", j[14]);
                                sbee1.AppendFormat("{0},", j[24]);
                                sbee1.AppendFormat("{0},", j[34]);
                                sbee1.AppendLine();
                                sbee1.AppendFormat("{0},", j[5]);//tag6
                                sbee1.AppendFormat("{0},", j[15]);
                                sbee1.AppendFormat("{0},", j[25]);
                                sbee1.AppendFormat("{0},", j[35]);
                                sbee1.AppendLine();

                                 sbee1.AppendFormat("{0},", j[6]);//tag7
                                 sbee1.AppendFormat("{0},", j[16]);
                                 sbee1.AppendFormat("{0},", j[26]);
                                 sbee1.AppendFormat("{0},", j[36]);
                                 sbee1.AppendLine();
                                  sbee1.AppendFormat("{0},", j[7]);//tag8
                                  sbee1.AppendFormat("{0},", j[17]);
                                  sbee1.AppendFormat("{0},", j[27]);
                                  sbee1.AppendFormat("{0},", j[37]);
                                  sbee1.AppendLine();
                                  sbee1.AppendFormat("{0},", j[8]);//tag11
                                  sbee1.AppendFormat("{0},", j[18]);
                                  sbee1.AppendFormat("{0},", j[28]);
                                  sbee1.AppendFormat("{0},", j[38]);
                                  sbee1.AppendLine();
                                 sbee1.AppendFormat("{0},", j[9]);//tag13
                                  sbee1.AppendFormat("{0},", j[19]);
                                  sbee1.AppendFormat("{0},", j[29]);
                                  sbee1.AppendFormat("{0},", j[39]);
                                  sbee1.AppendLine();
                                  sbee1.AppendFormat("{0},", j[10]);//tag15
                                  sbee1.AppendFormat("{0},", j[20]);
                                  sbee1.AppendFormat("{0},", j[30]);
                                  sbee1.AppendFormat("{0},", j[40]);
                                  sbee1.AppendLine();*/


                            /*  double R1, R2, R3, R4;

                                 R1 = (j[1] + j[2] + j[3] + j[4]) / 4;
                                 R2 = (j[11] + j[12] + j[13] + j[14]) / 4;
                                 R3 = (j[21] + j[22] + j[23] + j[24]) / 4;
                                 R4 = (j[31] + j[32] + j[33] + j[34]) / 4;*/
                            //      R1 = Math.Round(R1, 3, MidpointRounding.AwayFromZero);
                            //          Console.WriteLine("R1="+ R1);
                            //       R2 = Math.Round(R2, 3, MidpointRounding.AwayFromZero);
                            //           Console.WriteLine("R2=" + R2);
                            //     R3 = Math.Round(R3, 3, MidpointRounding.AwayFromZero);
                            //         Console.WriteLine("R3=" + R3);
                            //          R4 = Math.Round(R4, 3, MidpointRounding.AwayFromZero);
                            //           Console.WriteLine("R4=" + R4);
                            //        sbee1.AppendFormat("{0},", R1);
                            //        sbee1.AppendFormat("{0},", R2);
                            //        sbee1.AppendFormat("{0},", R3);
                            //        sbee1.AppendFormat("{0},", R4);
                            //        sbee1.AppendLine();



                            System.IO.File.WriteAllText("C:\\Users\\nem lab\\Desktop\\new1\\calibration.csv", sbee1.ToString());
                         
                            

                     

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
       // public void insertToXlFile(string tagId, string antId, uint count, string rssi)
       // {
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
       // }

       /* string MyConn = "datasource =localhost;port=3306;username=root;password=admin";

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
        }*/
       /* struct DataParameter
        {
            public string tagid;
            public string antennaID;
            public uint count;
            public string rssi;
        }*/
       // private DataParameter _inputparameter;

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           // try
          //  {
                //  uint count = ((DataParameter)e.Argument).count;
              //  string time2 = "none";
              //  DateTime localDate1 = DateTime.Now;
             //   time2 = localDate1.ToString("h.mm.ss.f tt "); // 7.00 AM // 12 hour clock
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
          //  }
         //   catch (Exception ex)
          //  {
          //      Console.WriteLine(ex.Message);
         //   }
            
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
