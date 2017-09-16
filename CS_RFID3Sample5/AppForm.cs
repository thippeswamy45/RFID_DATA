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

namespace CS_RFID3Sample5
{
    public partial class AppForm : Form
    {
        internal RFIDReader m_ReaderAPI;
        internal ReaderManagement m_ReaderMgmt;
        internal bool m_IsConnected;
        internal string m_SelectedTagID;

        internal ConnectionForm m_ConnectionForm;
        internal CapabilitiesForm m_CapabilitiesForm;

        internal ReadForm m_ReadForm;
        internal WriteForm m_WriteForm;
        internal LockForm m_LockForm;
        internal KillForm m_KillForm;
        internal WriteForm m_BlockWriteForm;
        internal BlockEraseForm m_BlockEraseForm;

        internal AccessOperationResult m_AccessOpResult;
        internal uint m_DurationTriggerTime = 0;

        private TimeSpan m_StartTime;
        private Hashtable m_TagTable;
        private uint m_TagTotalCount;
        private TagData m_ReadTag;
        private delegate void UpdateStatusLabel(string text);
        private delegate void UpdateStatus(Events.StatusEventData eventData);
        private UpdateStatus m_UpdateStatusHandler = null;
        private delegate void UpdateRead(Events.ReadEventData eventData);
        private UpdateRead m_UpdateReadHandler = null;

        internal class AccessOperationResult
        {
            public string m_Result;
            public ACCESS_OPERATION_CODE m_OpCode;
            public AutoResetEvent m_AccessCompleteEvent;
            public AccessOperationResult()
            {
                m_Result = "";
                m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;
                m_AccessCompleteEvent = new AutoResetEvent(false);
            }
        }

        public AppForm()
        {
            this.InitializeComponent();
            m_UpdateReadHandler = new UpdateRead(myUpdateRead);
            m_UpdateStatusHandler = new UpdateStatus(myUpdateStatus);            
            m_ReadTag = new Symbol.RFID3.TagData();

            m_ConnectionForm = new ConnectionForm(this);
            m_ReadForm = new ReadForm(this);

            m_ReaderMgmt = new ReaderManagement();
            m_TagTable = new Hashtable(1023);
            m_AccessOpResult = new AccessOperationResult();
            m_IsConnected = false;
            m_TagTotalCount = 0;
        }

        internal void configureMenuItemsUponConnectDisconnect()
        {
            this.capMenuItem.Enabled = this.m_IsConnected;
            this.configMenuItem.Enabled = this.m_IsConnected;
            //this.gpioToolStripMenuItem.Enabled = this.m_IsConnected;
            this.ResetMenuItem.Enabled = this.m_IsConnected;
            this.accessMenuItem.Enabled = this.m_IsConnected;
            
            this.readButton.Enabled = this.m_IsConnected;
            this.memBank_CB.Enabled = this.m_IsConnected;
        }

        internal void configureMenuItemsUponLoginLogout()
        {
        }

        internal void configureMenuItemsBasedOnCapabilities()
        {
            //this.m_ReaderAPI.ReaderCapabilities.NumGPIPorts > 0 ? true : false |
            //     this.m_ReaderAPI.ReaderCapabilities.NumGPOPorts > 0 ? true : false;
            this.blockEraseContextMenuItem.Enabled = this.m_ReaderAPI.ReaderCapabilities.IsBlockEraseSupported;
            this.blockWriteContextMenuItem.Enabled = this.m_ReaderAPI.ReaderCapabilities.IsBlockWriteSupported;
        }

        private void connectionMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_ConnectionForm)
            {
                m_ConnectionForm = new ConnectionForm(this);
            }
            m_ConnectionForm.ShowDialog();
        }

        private void capMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_CapabilitiesForm)
            {
                m_CapabilitiesForm = new CapabilitiesForm(this);
            }
            m_CapabilitiesForm.ShowDialog();
        }

        private void readMenuItem_Click(object sender, EventArgs e)
        {
            m_ReadForm.ShowDialog();
        }

        private void writeMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_WriteForm)
            {
                m_WriteForm = new WriteForm(this, false);
            }
            m_WriteForm.ShowDialog();
        }

        private void lockMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_LockForm)
            {
                m_LockForm = new LockForm(this);
            }
            m_LockForm.ShowDialog();
        }

        private void killMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_KillForm)
            {
                m_KillForm = new KillForm(this);
            }
            m_KillForm.ShowDialog();
        }

        private void blockWriteMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_BlockWriteForm)
            {
                m_BlockWriteForm = new WriteForm(this, true);
            }
            m_BlockWriteForm.ShowDialog();
        }

        private void blockEraseMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_BlockEraseForm)
            {
                m_BlockEraseForm = new BlockEraseForm(this);
            }
            m_BlockEraseForm.ShowDialog();
        }

        private void rebootMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                m_ReaderMgmt.Restart();
            }
            catch (OperationFailureException failureException)
            {
                notifyUser(failureException.VendorMessage, "Reboot");
            }
        }

        private void ResetMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_ReaderAPI.IsConnected)
                {
                    m_ReaderAPI.Config.ResetFactoryDefaults();
                }
            }
            catch (Exception ex)
            {
                notifyUser(ex.Message, "Reset");
            }
        }

        internal void notifyUser(string notificationMessage, string notificationSource)
        {
            DialogResult result = MessageBox.Show(notificationMessage, notificationSource);
        }

        private void sampleAppResultItem_Click(object sender, EventArgs e)
        {
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void memBank_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_ReaderAPI != null)
            {
                m_ReaderAPI.Actions.TagAccess.OperationSequence.DeleteAll();
                if (memBank_CB.SelectedIndex > 0)
                {
                    TagAccess.Sequence.Operation op = new TagAccess.Sequence.Operation();
                    op.AccessOperationCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ;
                    op.ReadAccessParams.MemoryBank = (MEMORY_BANK)memBank_CB.SelectedIndex - 1;
                    op.ReadAccessParams.ByteCount = 0;
                    op.ReadAccessParams.ByteOffset = 0;
                    op.ReadAccessParams.AccessPassword = 0;
                    m_ReaderAPI.Actions.TagAccess.OperationSequence.Add(op);
                }
            }
        }

        private void CloseForm()
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
                notifyUser(ex.Message, "Close");
            }
        }

        private void myUpdateRead(Events.ReadEventData eventData)
        {
            Symbol.RFID3.TagData[] tagData = m_ReaderAPI.Actions.GetReadTags(1000);
            if (tagData != null)
            {
                for (int nIndex = 0; nIndex < tagData.Length; nIndex++)
                {
                    /* 
                     * Display all inventories tags or tags on which 
                     * Read access operation was successful
                     */
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
                            if (!isFound)
                            {
                                tagID += tag.MemoryBank;
                                isFound = m_TagTable.ContainsKey(tagID);
                            }
                        }

                        if (isFound)
                        {
                            uint count = 0;
                            ListViewItem item = null ;
                            lock (m_TagTable.SyncRoot)
                            {
                               item = (ListViewItem)m_TagTable[tagID];
                            }
                            try
                            {
                                count = uint.Parse(item.SubItems[2].Text) + tagData[nIndex].TagSeenCount;
                                m_TagTotalCount += tagData[nIndex].TagSeenCount;
                            }
                            catch (FormatException fe)
                            {
                                notifyUser(fe.Message, "Tags");
                                break;
                            }
                            item.SubItems[1].Text = tag.AntennaID.ToString();
                            item.SubItems[2].Text = count.ToString();
                            item.SubItems[3].Text = tag.PeakRSSI.ToString();

                            string memoryBank = tag.MemoryBank.ToString();
                            int index = memoryBank.LastIndexOf('_');
                            if (index != -1)
                            {
                                memoryBank = memoryBank.Substring(index + 1);
                            }
                            if (tag.MemoryBankData.Length > 0 || !tag.MemoryBankData.Equals(item.SubItems[5].Text))
                            {
                                item.SubItems[5].Text = tag.MemoryBankData;
                                item.SubItems[6].Text = memoryBank;
                                item.SubItems[7].Text = tag.MemoryBankDataOffset.ToString();

                                lock (m_TagTable.SyncRoot)
                                {
                                    m_TagTable.Remove(tagID);
                                    string newID = tag.TagID + tag.MemoryBank.ToString();
                                    if (!m_TagTable.Contains(newID)) 
                                        m_TagTable.Add(newID, item);
                                }
                            }
                            if (tag.TagEvent != TAG_EVENT.NONE)
                            {
                                if (tag.TagEvent == TAG_EVENT.TAG_NOT_VISIBLE)
                                {
                                    item.BackColor = Color.LightGray;
                                }
                                else
                                {
                                    item.BackColor = Color.White;
                                }
                                inventoryList.Refresh();
                            }
                        }
                        else
                        {
                            ListViewItem item = new ListViewItem(tag.TagID);
                            ListViewItem.ListViewSubItem subItem;

                            subItem = new ListViewItem.ListViewSubItem();
                            subItem.Text = tag.AntennaID.ToString();
                            item.SubItems.Add(subItem);

                            subItem = new ListViewItem.ListViewSubItem();
                            subItem.Text = tag.TagSeenCount.ToString();
                            m_TagTotalCount += tag.TagSeenCount;
                            item.SubItems.Add(subItem);

                            subItem = new ListViewItem.ListViewSubItem();
                            subItem.Text = tag.PeakRSSI.ToString();
                            item.SubItems.Add(subItem);

                            subItem = new ListViewItem.ListViewSubItem();
                            subItem.Text = tag.PC.ToString("X");
                            item.SubItems.Add(subItem);

                            if (tag.MemoryBankData != string.Empty)
                            {
                                subItem = new ListViewItem.ListViewSubItem();
                                subItem.Text = tag.MemoryBankData;
                                item.SubItems.Add(subItem);

                                string memoryBank = tag.MemoryBank.ToString();
                                int index = memoryBank.LastIndexOf('_');
                                if (index != -1)
                                {
                                    memoryBank = memoryBank.Substring(index + 1);
                                }

                                subItem = new ListViewItem.ListViewSubItem();
                                subItem.Text = memoryBank;
                                item.SubItems.Add(subItem);

                                subItem = new ListViewItem.ListViewSubItem();
                                subItem.Text = tag.MemoryBankDataOffset.ToString();
                                item.SubItems.Add(subItem);
                            }
                            else
                            {
                                subItem = new ListViewItem.ListViewSubItem();
                                subItem.Text = "";
                                item.SubItems.Add(subItem);
                                subItem = new ListViewItem.ListViewSubItem();
                                subItem.Text = "";
                                item.SubItems.Add(subItem);
                                subItem = new ListViewItem.ListViewSubItem();
                                subItem.Text = "";
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
                /*
                 *  Update front panel Tag Count while changing inventory list
                 */
                this.totalTagValueLabel.Text = m_TagTable.Count + "(" + m_TagTotalCount + ")";
            }
        }

        private void myUpdateStatus(Events.StatusEventData eventData)
        {
            switch (eventData.StatusEventType)
            {
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_START_EVENT:
                    readButton.Text = "Stop Reading";
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_STOP_EVENT:
                    readButton.Text = "Start Reading";

                    if (this.m_DurationTriggerTime > 0)
                    {
                        TimeSpan triggerDuration = TimeSpan.FromMilliseconds(m_DurationTriggerTime);
                        readTimeValueLabel.Text = triggerDuration.Seconds + "." + triggerDuration.Milliseconds / 100 + " Sec";
                    }
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.ACCESS_START_EVENT:
                    readButton.Text = "Stop Reading";
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.ACCESS_STOP_EVENT:
                    readButton.Text = "Start Reading";
                     m_AccessOpResult.m_AccessCompleteEvent.Set();
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.BUFFER_FULL_WARNING_EVENT:
                    notifyUser("Buffer Full Warning", "Tags");
                    myUpdateRead(null);
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.BUFFER_FULL_EVENT:
                    notifyUser("Buffer Full", "Tags");
                    myUpdateRead(null);
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.DISCONNECTION_EVENT:
                    notifyUser(eventData.DisconnectionEventData.DisconnectEventInfo.ToString(),"Disconnection");
                    this.Connect("Disconnect");
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.READER_EXCEPTION_EVENT:
                    notifyUser(eventData.ReaderExceptionEventData.ReaderExceptionEventInfo, "Reader Exception");
                    break;
                default:
                    break;
            }
        }

        private void Events_ReadNotify(object sender, Events.ReadEventArgs args)
        {
            this.Invoke(m_UpdateReadHandler, new object[] { args.ReadEventData.TagData});
        }

        public void Events_StatusNotify(object sender, Events.StatusEventArgs args)
        {
            this.Invoke(m_UpdateStatusHandler, new object[] { args.StatusEventData });
        }

        internal void Connect(string status)
        {
            if (status == "Connect")
            {
                uint port = uint.Parse(m_ConnectionForm.PortText);
                m_ReaderAPI = new RFIDReader(m_ConnectionForm.IpText, port, 0);

                try
                {
                    m_ReaderAPI.Connect();
                                   
                    /*
                     *  Events Registration
                     */
                    m_ReaderAPI.Events.AttachTagDataWithReadEvent = false;
                    m_ReaderAPI.Events.ReadNotify += new Events.ReadNotifyHandler(Events_ReadNotify);
                    m_ReaderAPI.Events.StatusNotify += new Events.StatusNotifyHandler(Events_StatusNotify);
                    m_ReaderAPI.Events.NotifyBufferFullWarningEvent = true;
                    m_ReaderAPI.Events.NotifyBufferFullEvent = true;
                    m_ReaderAPI.Events.NotifyReaderDisconnectEvent = true;
                    m_ReaderAPI.Events.NotifyAccessStartEvent = true;
                    m_ReaderAPI.Events.NotifyAccessStopEvent = true;
                    m_ReaderAPI.Events.NotifyInventoryStartEvent = true;
                    m_ReaderAPI.Events.NotifyInventoryStopEvent = true;
                    m_ReaderAPI.Events.NotifyReaderExceptionEvent = true;

                    /*
                     * Label setup
                     */
                    this.Text = m_ConnectionForm.IpText;
                    this.m_ConnectionForm.connectionButton.Text = "Disconnect";
                    if (m_ConnectionForm.Visible)
                    {
                        this.m_ConnectionForm.Close();
                    }
                }
                catch (OperationFailureException ofe)
                {
                    notifyUser(ofe.VendorMessage, "Tags");
                }
            }
            else if (status == "Disconnect")
            {
                try
                {
                    m_ReaderAPI.Disconnect();
                }
                catch (OperationFailureException ofe)
                {
                    notifyUser(ofe.VendorMessage, "Connect");
                }

                this.Text = "CS_RFID3Sample5";
                this.m_ConnectionForm.connectionButton.Text = "Connect";
                if (m_ConnectionForm.Visible)
                {
                    this.m_ConnectionForm.Close();
                }
                this.readButton.Enabled = false;
                this.memBank_CB.Enabled = false;
            }
            m_IsConnected = m_ReaderAPI.IsConnected;
            configureMenuItemsUponConnectDisconnect();
            configureMenuItemsBasedOnCapabilities();
        }

        internal void RunAccessOps(ACCESS_OPERATION_CODE opCommand)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(startAccessCallback), opCommand);
        }

        private void startAccessCallback(object opCommand)
        {
            string result = RFIDResults.RFID_API_SUCCESS.ToString();
            try
            {
                m_AccessOpResult.m_OpCode = (ACCESS_OPERATION_CODE)opCommand;
                m_AccessOpResult.m_Result = "Access Succeeded";
                m_AccessOpResult.m_AccessCompleteEvent.Reset();
                if ((ACCESS_OPERATION_CODE)opCommand == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReadTag = m_ReaderAPI.Actions.TagAccess.ReadWait(m_SelectedTagID,
                            m_ReadForm.m_ReadParams, null);
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.ReadEvent(m_ReadForm.m_ReadParams,
                            null, null);
                    }
                }
                else if ((ACCESS_OPERATION_CODE)opCommand == ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.WriteWait(m_SelectedTagID,
                            m_WriteForm.m_WriteParams, null);
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.WriteEvent(m_WriteForm.m_WriteParams,
                            null, null);
                    }
                }
                else if ((ACCESS_OPERATION_CODE)opCommand == ACCESS_OPERATION_CODE.ACCESS_OPERATION_LOCK)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.LockWait(m_SelectedTagID,
                            m_LockForm.m_LockParams, null);
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.LockEvent(m_LockForm.m_LockParams,
                            null, null);
                    }
                }
                else if ((ACCESS_OPERATION_CODE)opCommand == ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.KillWait(m_SelectedTagID,
                            m_KillForm.m_KillParams, null);
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.KillEvent(m_KillForm.m_KillParams,
                            null, null);
                    }
                }
                else if ((ACCESS_OPERATION_CODE)opCommand == ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_WRITE)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.BlockWriteWait(m_SelectedTagID,
                           m_BlockWriteForm.m_WriteParams, null);
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.BlockWriteEvent(m_BlockWriteForm.m_WriteParams,
                            null, null);
                    }
                }
                else if ((ACCESS_OPERATION_CODE)opCommand == ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.BlockEraseWait(m_SelectedTagID,
                            m_BlockEraseForm.m_BlockEraseParams, null);
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.BlockEraseEvent(m_BlockEraseForm.m_BlockEraseParams,
                            null, null);
                    }
                }
                m_AccessOpResult.m_AccessCompleteEvent.WaitOne();
            }
            catch (InvalidUsageException iue)
            {
                m_AccessOpResult.m_Result = iue.Info;               
            }
            catch (OperationFailureException ofe)
            {
                m_AccessOpResult.m_Result = ofe.VendorMessage == "None" ? ofe.StatusDescription : ofe.VendorMessage;
            }
            this.Invoke(new UpdateStatusLabel(updateStatus),
               new object[] { m_AccessOpResult.m_Result });
        }

        private void updateStatus(string result)
        {
            if (result == "Access Succeeded")
            {
                if (m_AccessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        if (m_ReadTag.OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ &&
                            m_ReadTag.OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS)
                        {
                            string tagID = m_ReadTag.TagID + m_ReadTag.MemoryBank.ToString()
                                + m_ReadTag.MemoryBankDataOffset.ToString();
                            int selectedIndex = this.inventoryList.SelectedIndices[0];
                            ListViewItem item = this.inventoryList.Items[selectedIndex];

                            if (item.SubItems[5].Text.Length > 0)
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
                                    newItem.SubItems.Add(m_ReadTag.AntennaID.ToString());
                                    newItem.SubItems.Add(m_ReadTag.TagSeenCount.ToString());
                                    m_TagTotalCount += m_ReadTag.TagSeenCount;

                                    newItem.SubItems.Add(m_ReadTag.PeakRSSI.ToString());
                                    newItem.SubItems.Add(m_ReadTag.PC.ToString("X"));
                                    newItem.SubItems.Add(m_ReadTag.MemoryBankData);

                                    string memoryBank = m_ReadTag.MemoryBank.ToString();
                                    int index = memoryBank.LastIndexOf('_');
                                    if (index != -1)
                                    {
                                        memoryBank = memoryBank.Substring(index + 1);
                                    }
                                    newItem.SubItems.Add(memoryBank);
                                    newItem.SubItems.Add(m_ReadTag.MemoryBankDataOffset.ToString());

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
                                    item.SubItems[5].Text = m_ReadTag.MemoryBankData;
                                    item.SubItems[7].Text = m_ReadTag.MemoryBankDataOffset.ToString();
                                }
                            }
                            else
                            {
                                // Empty Memory Bank Slot
                                item.SubItems[5].Text = m_ReadTag.MemoryBankData;

                                string memoryBank = m_ReadForm.m_ReadParams.MemoryBank.ToString();
                                int index = memoryBank.LastIndexOf('_');
                                if (index != -1)
                                {
                                    memoryBank = memoryBank.Substring(index + 1);
                                }
                                item.SubItems[6].Text = memoryBank;
                                item.SubItems[7].Text = m_ReadTag.MemoryBankDataOffset.ToString();

                                lock (m_TagTable.SyncRoot)
                                {
                                    if (m_ReadTag.TagID != null)
                                    {
                                        m_TagTable.Remove(m_ReadTag.TagID);
                                    }
                                    m_TagTable.Add(tagID, item);
                                }
                            }
                            this.m_ReadForm.ReadData_TB.Text = m_ReadTag.MemoryBankData;
                        }
                        notifyUser("Read Succeed", "Access Operation");
                    }
                    notifyUser("Read Succeed", "Access Operation");
                }
                else if (m_AccessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE)
                {
                    m_WriteForm.writeButton.Enabled = true;
                    notifyUser("Write Succeed", "Access Operation");
                }
                else if (m_AccessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_LOCK)
                {
                    m_LockForm.lockButton.Enabled = true;
                    notifyUser("Lock Succeed", "Access Operation");
                }
                else if (m_AccessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL)
                {
                    m_KillForm.killButton.Enabled = true;
                    notifyUser("Kill Succeed", "Access Operation");
                }
                else if (m_AccessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_WRITE)
                {
                    m_BlockWriteForm.writeButton.Enabled = true;
                    notifyUser("Block Write Succeed", "Access Operation");
                }
                else if (m_AccessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE)
                {
                    m_BlockEraseForm.eraseButton.Enabled = true;
                    notifyUser("Block Erase Succeed", "Access Operation");
                }
            }
            else
            {
                notifyUser(m_AccessOpResult.m_Result, "Access Operation");
            }
            resetButtonState();
            this.readButton.Enabled = true;
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

        private void AppForm_Load(object sender, EventArgs e)
        {
            this.memBank_CB.SelectedIndex = 0;
            this.Connect("Connect");
        }

        private void AppForm_Closing(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void readButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_IsConnected)
                {
                    if (readButton.Text == "Start Reading")
                    {
                        StartReading();
                    }
                    else if (readButton.Text == "Stop Reading")
                    {
                        StopReading();
                    }
                }
                else
                {
                    notifyUser("Please connect to a reader", "Read Operation");
                }
            }
            catch (OperationFailureException ex)
            {
                notifyUser(ex.VendorMessage, "Read Operation");
            }
        }

        private void StartReading()
        {

            try
            {
                if (m_ReaderAPI.Actions.TagAccess.OperationSequence.Length > 0)
                {
                    m_ReaderAPI.Actions.TagAccess.OperationSequence.PerformSequence();
                }
                else
                {
                    this.inventoryList.Items.Clear();
                    this.m_TagTable.Clear();
                    this.m_TagTotalCount = 0;

                    m_ReaderAPI.Actions.Inventory.Perform();
                }
            }
            catch (InvalidOperationException ioe)
            {
                notifyUser("InvalidOperationException" + ioe.Message, "Read Operation");
            }
            catch (OperationFailureException ex)
            {
                notifyUser("OperationFailureException:" + ex.VendorMessage, "Read Operation");
            }
            catch (InvalidUsageException iue)
            {
                notifyUser("InvalidUsageException:" + iue.Info, "Read Operation");
            }
            catch (Exception ex)
            {
                notifyUser("Exception:" + ex.Message, "Read Operation");
            }
            m_StartTime = TimeSpan.FromMilliseconds(Environment.TickCount);
        }

        private void StopReading()
        {
            /*
             *  Update Read Time and Tags Read Count 
             */
            if (this.m_DurationTriggerTime == 0)
            {
                TimeSpan duration = TimeSpan.FromMilliseconds(Environment.TickCount).Subtract(m_StartTime).Duration();
                readTimeValueLabel.Text = duration.Seconds + "." + duration.Milliseconds / 100 + " Sec";
            }

            try
            {
                if (m_ReaderAPI.Actions.TagAccess.OperationSequence.Length > 0)
                {
                    m_ReaderAPI.Actions.TagAccess.OperationSequence.StopSequence();
                }
                else
                {
                    m_ReaderAPI.Actions.Inventory.Stop();
                }
            }
            catch (InvalidOperationException ioe)
            {
                notifyUser("InvalidOperationException" + ioe.Message, "Stop Operation");
            }
            catch (OperationFailureException ofe)
            {
                notifyUser("OperationFailureException:" + ofe.VendorMessage, "Stop Read");
            }
            catch (InvalidUsageException iue)
            {
                notifyUser("InvalidUsageException:" + iue.Info, "Stop Read");
            }
            catch (Exception ex)
            {
                notifyUser("Exception:" + ex.Message, "Stop Read");
            }
        }

        private void helpMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpDialog = new HelpForm(this);
            helpDialog.ShowDialog();
        }

        private void inventoryList_ItemActivate(object sender, EventArgs ea)
        {
            Point location = new Point();
            inventoryList.PointToScreen(location);
            dataContextMenu.Show(inventoryList, new Point(inventoryList.Width / 2, inventoryList.Height / 2));
        }

        private void tagDataMenuItem_Click(object sender, EventArgs e)
        {
            TagDataForm tagDataForm = new TagDataForm(this);
            tagDataForm.ShowDialog();
        }

        private void readContextMenuItem_Click(object sender, EventArgs e)
        {
            m_ReadForm.ShowDialog();
        }

        private void writeContextMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_WriteForm)
            {
                m_WriteForm = new WriteForm(this, false);
            }
            m_WriteForm.ShowDialog();
        }

        private void lockContextMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_LockForm)
            {
                m_LockForm = new LockForm(this);
            }
            m_LockForm.ShowDialog();
        }

        private void killContextMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_KillForm)
            {
                m_KillForm = new KillForm(this);
            }
            m_KillForm.ShowDialog();
        }

        private void blockEraseContextMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_BlockEraseForm)
            {
                m_BlockEraseForm = new BlockEraseForm(this);
            }
            m_BlockEraseForm.ShowDialog();
        }

        private void blockWriteContextMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_BlockWriteForm)
            {
                m_BlockWriteForm = new WriteForm(this, true);
            }
            m_BlockWriteForm.ShowDialog();
        }

        private void clearReportMenuItem_Click(object sender, EventArgs e)
        {
            this.totalTagValueLabel.Text = "";
            this.readTimeValueLabel.Text = "";
            this.inventoryList.Items.Clear();
            this.m_TagTable.Clear();
        }
    }
}