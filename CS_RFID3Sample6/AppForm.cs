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
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace CS_RFID3Sample6
{
    public partial class AppForm : Form
    {
        internal RFIDReader m_ReaderAPI;
        internal ReaderManagement m_ReaderMgmt;
        internal bool m_IsConnected;
        internal string m_SelectedTagID;

        internal ConnectionForm m_ConnectionForm;
        internal CapabilitiesForm m_CapabilitiesForm;
        internal AntennaConfigForm m_AntennaConfigForm;
        internal RFModeForm m_RFModeForm;
        internal SingulationForm m_SingulationForm;
        internal TagStorageForm m_TagStorageForm;

        internal ReadForm m_ReadForm;
        internal AntennaInfoForm m_AntennaInfoForm;
        internal WriteForm m_WriteForm;
        internal LockForm m_LockForm;
        internal KillForm m_KillForm;
        internal WriteForm m_BlockWriteForm;
        internal BlockEraseForm m_BlockEraseForm;
        internal LocateForm m_LocateForm;

        internal PreFilterForm m_PreFilterForm;
        internal PostFilterForm m_PostFilterForm;
        internal AccessFilterForm m_AccessFilterForm;
        internal TriggerForm m_TriggerForm;
        internal LoginForm m_LoginForm;
        internal AntennaModeForm m_AntennaModeForm;
        internal FirmwareUpdateForm m_FirmwareUpdateForm;
        internal SystemInfoForm m_SystemInfoForm;

        internal AccessOperationResult m_AccessOpResult;
        internal READER_TYPE m_ReaderType;
        internal uint m_DurationTriggerTime;

        private TimeSpan m_StartTime;
        private Hashtable m_TagTable;
        private uint m_TagTotalCount;
        private TagData m_ReadTag;
        private delegate void UpdateStatusLabel(string text);
        private delegate void UpdateStatus(Events.StatusEventData eventData);
        private UpdateStatus m_UpdateStatusHandler = null;
        private delegate void UpdateRead(Events.ReadEventData eventData);
        private UpdateRead m_UpdateReadHandler = null;
        private bool m_ReaderInitiatedDisconnectionReceived;
        private bool m_isBeepingEnabled;
        private Boolean m_isReading = false;
        const uint MB_OK = 0x00000000;
        [DllImport("coredll")]
        private static extern bool SendMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);
        internal System.Drawing.Color m_DefaultButtonColor;
        private uint LVM_SETTEXTBKCOLOR = 0x1026;
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

        [DllImport("coredll.dll")]
        internal static extern bool MessageBeep([In] UInt32 beepType);

        public AppForm()
        {
            this.InitializeComponent();
            m_DefaultButtonColor = readButton.BackColor;
            m_UpdateReadHandler = new UpdateRead(myUpdateRead);
            m_UpdateStatusHandler = new UpdateStatus(myUpdateStatus);
            m_ReadTag = new Symbol.RFID3.TagData();

            m_ConnectionForm = new ConnectionForm(this);
            m_ReadForm = new ReadForm(this);
            m_AntennaInfoForm = new AntennaInfoForm(this);
            m_AntennaConfigForm = new AntennaConfigForm(this);
            m_PostFilterForm = new PostFilterForm(this);
            m_AccessFilterForm = new AccessFilterForm(this);
            m_TriggerForm = new TriggerForm(this);

            m_ReaderMgmt = new ReaderManagement();
            m_TagTable = new Hashtable(1023);
            m_AccessOpResult = new AccessOperationResult();
            m_IsConnected = false;
            m_TagTotalCount = 0;
            m_ReaderInitiatedDisconnectionReceived = false;

        }

        internal void configureMenuItemsUponConnectDisconnect()
        {
            this.capMenuItem.Enabled = this.m_IsConnected;
            this.configMenuItem.Enabled = this.m_IsConnected;
            this.antennaMenuItem.Enabled = this.m_IsConnected;
            this.rFModeMenuItem.Enabled = this.m_IsConnected;
            this.singulationMenuItem.Enabled = this.m_IsConnected;
            //this.gpioToolStripMenuItem.Enabled = this.m_IsConnected;
            this.ResetMenuItem.Enabled = this.m_IsConnected;
            this.tagStorageMenuItem.Enabled = this.m_IsConnected;
            this.filterMenuItem.Enabled = this.m_IsConnected;
            this.accessMenuItem.Enabled = this.m_IsConnected;
            this.triggerMenuItem.Enabled = this.m_IsConnected;
            if (this.m_ReaderAPI != null && this.m_IsConnected && this.m_ReaderAPI.ReaderCapabilities.IsRadioPowerControlSupported == true)
            {
                this.radioPowerMenuItem.Text = this.m_ReaderAPI.Config.RadioPowerState == RADIO_POWER_STATE.OFF ?
                    "Power On Radio" : "Power Off Radio";
            }
            else
            {
                this.radioPowerMenuItem.Enabled = false;
            }
            this.readButton.Enabled = this.m_IsConnected;
            this.memBank_CB.Enabled = this.m_IsConnected;
        }

        internal void configureMenuItemsUponLoginLogout()
        {
            /*The Handheld reader does not support any Reader-Management APIs as of now. So disabling the menu items for 
             MC Series*/
            this.softwareUpdateMenuItem.Enabled = this.m_ReaderMgmt.IsLoggedIn && (m_ReaderType != READER_TYPE.MC);
            this.readPointMenuItem.Enabled = this.m_ReaderMgmt.IsLoggedIn && (m_ReaderType != READER_TYPE.MC);
            this.rebootMenuItem.Enabled = this.m_ReaderMgmt.IsLoggedIn && (m_ReaderType != READER_TYPE.MC);
            this.antModeMenuItem.Enabled = this.m_ReaderMgmt.IsLoggedIn && (m_ReaderType != READER_TYPE.MC);
            this.systemInfoMenuItem.Enabled = this.m_ReaderMgmt.IsLoggedIn && (m_ReaderType != READER_TYPE.MC);
        }

        internal void configureMenuItemsBasedOnCapabilities()
        {
            this.autonomousMode_CB.Visible =
                 this.autonomousMode_CB.Enabled = this.m_ReaderAPI.IsConnected ? this.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported : false;
            this.radioPowerMenuItem.Enabled = this.m_ReaderAPI.IsConnected ? this.m_ReaderAPI.ReaderCapabilities.IsRadioPowerControlSupported : false;
            this.gpioMenuItem.Enabled = false;
            m_TriggerForm.Reset();
            //this.m_ReaderAPI.ReaderCapabilities.NumGPIPorts > 0 ? true : false |
            //     this.m_ReaderAPI.ReaderCapabilities.NumGPOPorts > 0 ? true : false;
            this.blockEraseContextMenuItem.Enabled = this.m_ReaderAPI.IsConnected ? this.m_ReaderAPI.ReaderCapabilities.IsBlockEraseSupported : false;
            this.blockWriteContextMenuItem.Enabled = this.m_ReaderAPI.IsConnected ? this.m_ReaderAPI.ReaderCapabilities.IsBlockWriteSupported : false;

            //this.m_TriggerForm.newTag_CB.Enabled =
            //this.m_TriggerForm.newTag_TB.Enabled =
            //this.m_TriggerForm.backTag_CB.Enabled =
            //this.m_TriggerForm.backTag_TB.Enabled =
            //this.m_TriggerForm.invisibleTag_CB.Enabled =
            //this.m_TriggerForm.invisibleTag_TB.Enabled = this.m_ReaderAPI.IsConnected ? this.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported : false;


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

        private void antennaMenuItem_Click(object sender, EventArgs e)
        {
            m_AntennaConfigForm.ShowDialog();
        }

        private void RFModeMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_RFModeForm)
            {
                m_RFModeForm = new RFModeForm(this);
            }
            m_RFModeForm.ShowDialog();
        }

        private void singulationMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_SingulationForm)
            {
                m_SingulationForm = new SingulationForm(this);
            }
            m_SingulationForm.ShowDialog();
        }

        private void tagStorageMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_TagStorageForm)
            {
                m_TagStorageForm = new TagStorageForm(this);
            }
            m_TagStorageForm.ShowDialog();
        }

        private void antInfoMenuItem_Click(object sender, EventArgs e)
        {
            m_AntennaInfoForm.ShowDialog();
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

        private void loginMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_LoginForm)
            {
                m_LoginForm = new LoginForm(this);
            }
            m_LoginForm.ShowDialog();
        }

        private void rebootMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                m_ReaderMgmt.Restart();

                this.rebootMenuItem.Enabled = false;
                this.antModeMenuItem.Enabled = false;
                this.radioPowerMenuItem.Enabled = false;
                this.softwareUpdateMenuItem.Enabled = false;

                if (m_LoginForm != null)
                {
                    m_LoginForm.loginButton.Text = "Login";
                }
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
                    if (m_TagStorageForm != null)
                        m_TagStorageForm.Reset();

                    /* Reset Loading default to reflect the changes */
                    if (null != m_AntennaConfigForm)
                        m_AntennaConfigForm.m_IsLoaded = false;
                    if (null != m_RFModeForm)
                        m_RFModeForm.m_IsLoaded = false;
                    if (null != m_SingulationForm)
                        m_SingulationForm.m_IsLoaded = false;
                }
            }
            catch (Exception ex)
            {
                notifyUser(ex.Message, "Reset");
            }
        }

        private void radioPowerMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.radioPowerMenuItem.Text == "Power Off Radio")
                {
                    m_ReaderAPI.Config.RadioPowerState = RADIO_POWER_STATE.OFF;
                }
                else
                {
                    m_ReaderAPI.Config.RadioPowerState = RADIO_POWER_STATE.ON;
                }
                this.radioPowerMenuItem.Text = this.m_ReaderAPI.Config.RadioPowerState == RADIO_POWER_STATE.OFF ?
                    "Power On Radio" : "Power Off Radio";

            }
            catch (InvalidUsageException iue)
            {
                this.notifyUser(iue.Info, "Radio Power Control");
            }
            catch (OperationFailureException ofe)
            {
                this.notifyUser(ofe.StatusDescription, "Radio Power Control");
            }
            catch (Exception ex)
            {
                this.notifyUser(ex.Message, "Radio Power Control");
            }
        }

        private void antModeMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_AntennaModeForm)
            {
                m_AntennaModeForm = new AntennaModeForm(this);
            }
            m_AntennaModeForm.ShowDialog();
        }

        private void softwareUpdateMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_FirmwareUpdateForm)
            {
                m_FirmwareUpdateForm = new FirmwareUpdateForm(this);
            }
            m_FirmwareUpdateForm.ShowDialog();
        }

        private void systemInfoMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_SystemInfoForm)
            {
                m_SystemInfoForm = new SystemInfoForm(this);
            }
            m_SystemInfoForm.ShowDialog();
        }

        private void triggerMenuItem_Click(object sender, EventArgs e)
        {
            m_TriggerForm.ShowDialog();
        }

        internal void notifyUser(string notificationMessage, string notificationSource)
        {
            MessageBox.Show(notificationMessage, notificationSource);
        }

        private void sampleAppResultItem_Click(object sender, EventArgs e)
        {
        }

        private void preFilterMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_PreFilterForm)
            {
                m_PreFilterForm = new PreFilterForm(this);
            }
            m_PreFilterForm.ShowDialog();
        }

        private void postFilterMenuItem_Click(object sender, EventArgs e)
        {
            m_PostFilterForm.ShowDialog();
        }

        private void accessFiltermenuItem_Click(object sender, EventArgs e)
        {
            m_AccessFilterForm.ShowDialog();
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
                    m_ReaderAPI.Events.ReadNotify -= Events_ReadNotify;
                    m_ReaderAPI.Events.StatusNotify -= Events_StatusNotify;
                    StopReading();
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
            if (m_isReading)
            {
                Symbol.RFID3.TagData[] tagDataArray = m_ReaderAPI.Actions.GetReadTags(100);
                if (tagDataArray != null)
                {
                    for (int nIndex = 0; nIndex < tagDataArray.Length && m_isReading; nIndex++)
                    {
                        if (tagDataArray[nIndex].ContainsLocationInfo)
                        {
                            m_LocateForm.Locate_PB.Value = tagDataArray[nIndex].LocationInfo.RelativeDistance;
                            m_LocateForm.lastLocatedTagTimeStamp = System.Environment.TickCount;
                        }
                        /* 
                         * Display all inventories tags or tags on which 
                         * Read access operation was successful
                         */
                        if (tagDataArray[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_NONE ||
                            (tagDataArray[nIndex].OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ &&
                            tagDataArray[nIndex].OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS))
                        {
                            Symbol.RFID3.TagData tag = tagDataArray[nIndex];
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
                                ListViewItem item = null;
                                lock (m_TagTable.SyncRoot)
                                {
                                    int itemLocation = (int)m_TagTable[tagID];
                                    item = inventoryList.Items[itemLocation];
                                }
                                try
                                {
                                    count = uint.Parse(item.SubItems[2].Text) + tagDataArray[nIndex].TagSeenCount;
                                    m_TagTotalCount += tagDataArray[nIndex].TagSeenCount;
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
                                            m_TagTable.Add(newID, item.Index);
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
                                    inventoryList.Update();
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

                                if (m_isBeepingEnabled)
                                    MessageBeep(MB_OK);

                                lock (m_TagTable.SyncRoot)
                                {
                                    m_TagTable.Add(tagID, item.Index);
                                }
                                Thread.Sleep(0);
                            }
                        }
                    }
                    /*
                     *  Update front panel Tag Count while changing inventory list
                     */
                    this.totalTagValueLabel.Text = m_TagTable.Count + "(" + m_TagTotalCount + ")";
                }
            }
        }

        private void myUpdateStatus(Events.StatusEventData eventData)
        {
            switch (eventData.StatusEventType)
            {
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_START_EVENT:
                    if (m_TriggerForm.getTriggerInfo().StartTrigger.Type == START_TRIGGER_TYPE.START_TRIGGER_TYPE_IMMEDIATE)
                    {
                        readButton.Text = "Stop Reading";
                    }
                    readButton.BackColor = System.Drawing.Color.Green;
                    m_isReading = true;
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_STOP_EVENT:
                    if (m_TriggerForm.getTriggerInfo().StartTrigger.Type == START_TRIGGER_TYPE.START_TRIGGER_TYPE_IMMEDIATE)
                    {
                        readButton.Text = "Start Reading";
                    }
                    readButton.BackColor = this.m_DefaultButtonColor;
                    m_isReading = false;
                    if (this.m_DurationTriggerTime > 0)
                    {
                        TimeSpan triggerDuration = TimeSpan.FromMilliseconds(m_DurationTriggerTime);
                        readTimeValueLabel.Text = triggerDuration.Seconds + "." + triggerDuration.Milliseconds / 100 + " Sec";
                    }
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.ACCESS_START_EVENT:
                    if (m_TriggerForm.getTriggerInfo().StartTrigger.Type == START_TRIGGER_TYPE.START_TRIGGER_TYPE_IMMEDIATE)
                    {
                        readButton.Text = "Stop Reading";
                    }
                    readButton.BackColor = System.Drawing.Color.Green;
                    m_isReading = true;
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.ACCESS_STOP_EVENT:
                    if (m_TriggerForm.getTriggerInfo().StartTrigger.Type == START_TRIGGER_TYPE.START_TRIGGER_TYPE_IMMEDIATE)
                    {
                        readButton.Text = "Start Reading";
                    }
                    m_isReading = false;
                    readButton.BackColor = this.m_DefaultButtonColor;
                    m_AccessOpResult.m_AccessCompleteEvent.Set();
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.BUFFER_FULL_WARNING_EVENT:
                    //notifyUser("Buffer Full Warning", "Tags");
                    //myUpdateRead(null);
                    m_ReaderAPI.Actions.PurgeTags();
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.BUFFER_FULL_EVENT:
                    //notifyUser("Buffer Full", "Tags");
                    //myUpdateRead(null);
                    m_ReaderAPI.Actions.PurgeTags();
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.DISCONNECTION_EVENT:
                    notifyUser(eventData.DisconnectionEventData.DisconnectEventInfo.ToString(), "Disconnection");
                    m_ReaderInitiatedDisconnectionReceived = true;
                    this.Connect("Disconnect");
                    Thread disconnectionThread = new Thread(Disconnect);
                    disconnectionThread.IsBackground = true;
                    disconnectionThread.Start();
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.READER_EXCEPTION_EVENT:
                    notifyUser(eventData.ReaderExceptionEventData.ReaderExceptionEventInfo.ToString(), "Reader Exception");
                    break;
                case Symbol.RFID3.Events.STATUS_EVENT_TYPE.HANDHELD_TRIGGER_EVENT:
                    TriggerInfo triggerInfo = m_TriggerForm.getTriggerInfo();
                    if (eventData.HandheldTriggerEventData.HandheldTriggerEvent == HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED &&
                        triggerInfo.StartTrigger.Type == START_TRIGGER_TYPE.START_TRIGGER_TYPE_IMMEDIATE)
                    {
                        // Lets start the inventory upon GPI event even if the StartTrigger is configured as immediate
                        processUIOrGPIEvent(eventData.HandheldTriggerEventData.HandheldTriggerEvent == HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED);
                    }
                    if (eventData.HandheldTriggerEventData.HandheldTriggerEvent == HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_RELEASED &&
                        triggerInfo.StopTrigger.Type == STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_IMMEDIATE)
                    {
                        processUIOrGPIEvent(eventData.HandheldTriggerEventData.HandheldTriggerEvent == HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED);
                    }
                    break;
                default:
                    break;
            }
        }

        private void Events_ReadNotify(object sender, Events.ReadEventArgs args)
        {
            this.Invoke(m_UpdateReadHandler, new object[] { args.ReadEventData.TagData });
        }

        public void Events_StatusNotify(object sender, Events.StatusEventArgs args)
        {
            this.Invoke(m_UpdateStatusHandler, new object[] { args.StatusEventData });
        }
        internal void Disconnect()
        {
            try
            {
                m_ReaderAPI.Disconnect();
                m_ReaderInitiatedDisconnectionReceived = false;
            }
            catch (OperationFailureException ofe)
            {
                notifyUser(ofe.VendorMessage, "DisConnect");
            }
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
                     * Label setup
                     */
                    this.Text = m_ConnectionForm.IpText;
                    this.m_ConnectionForm.connectionButton.Text = "Disconnect";
                    readButton.Text = "Start Reading";
                    m_isReading = false;
                    if (m_ConnectionForm.Visible)
                    {
                        this.m_ConnectionForm.Close();
                    }

                    try
                    {
                        // DiscardTagsOnInventoryStop enables faster processing of the stop operation
                        // by avoiding processing of tags that are still being received from the reader.
                        TagStorageSettings tagStorageSettings =  m_ReaderAPI.Config.GetTagStorageSettings();
                        tagStorageSettings.DiscardTagsOnInventoryStop = true;
                        m_ReaderAPI.Config.SetTagStorageSettings(tagStorageSettings);
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
                        m_ReaderAPI.Events.NotifyHandheldTriggerEvent = true;
                    }
                    catch (OperationFailureException ofe)
                    {
                        notifyUser(ofe.VendorMessage, "Connect Configuration");
                    }
                }
                catch (OperationFailureException ofe)
                {
                    notifyUser(ofe.StatusDescription, "Connect");
                }
            }
            else if (status == "Disconnect")
            {
                if (!m_ReaderInitiatedDisconnectionReceived)
                {
                    try
                    {
                        m_ReaderAPI.Events.ReadNotify -= Events_ReadNotify;
                        m_ReaderAPI.Events.StatusNotify -= Events_StatusNotify;
                        StopReading();
                        m_ReaderAPI.Disconnect();
                    }
                    catch (OperationFailureException ofe)
                    {
                        notifyUser(ofe.VendorMessage, "Connect");
                    }
                }

                this.Text = "CS_RFID3Sample6";
                this.m_ConnectionForm.connectionButton.Text = "Connect";
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
                            m_ReadForm.m_ReadParams, m_AntennaInfoForm.getInfo());
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.ReadEvent(m_ReadForm.m_ReadParams,
                            m_AccessFilterForm.getFilter(), m_AntennaInfoForm.getInfo());
                    }
                }
                else if ((ACCESS_OPERATION_CODE)opCommand == ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.WriteWait(m_SelectedTagID,
                            m_WriteForm.m_WriteParams, m_AntennaInfoForm.getInfo());
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.WriteEvent(m_WriteForm.m_WriteParams,
                            m_AccessFilterForm.getFilter(), m_AntennaInfoForm.getInfo());
                    }
                }
                else if ((ACCESS_OPERATION_CODE)opCommand == ACCESS_OPERATION_CODE.ACCESS_OPERATION_LOCK)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.LockWait(m_SelectedTagID,
                            m_LockForm.m_LockParams, m_AntennaInfoForm.getInfo());
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.LockEvent(m_LockForm.m_LockParams,
                            m_AccessFilterForm.getFilter(), m_AntennaInfoForm.getInfo());
                    }
                }
                else if ((ACCESS_OPERATION_CODE)opCommand == ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.KillWait(m_SelectedTagID,
                            m_KillForm.m_KillParams, m_AntennaInfoForm.getInfo());
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.KillEvent(m_KillForm.m_KillParams,
                            m_AccessFilterForm.getFilter(), m_AntennaInfoForm.getInfo());
                    }
                }
                else if ((ACCESS_OPERATION_CODE)opCommand == ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_WRITE)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.BlockWriteWait(m_SelectedTagID,
                           m_BlockWriteForm.m_WriteParams, m_AntennaInfoForm.getInfo());
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.BlockWriteEvent(m_BlockWriteForm.m_WriteParams,
                            m_AccessFilterForm.getFilter(), m_AntennaInfoForm.getInfo());
                    }
                }
                else if ((ACCESS_OPERATION_CODE)opCommand == ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        m_ReaderAPI.Actions.TagAccess.BlockEraseWait(m_SelectedTagID,
                            m_BlockEraseForm.m_BlockEraseParams, m_AntennaInfoForm.getInfo());
                    }
                    else
                    {
                        m_ReaderAPI.Actions.TagAccess.BlockEraseEvent(m_BlockEraseForm.m_BlockEraseParams,
                            m_AccessFilterForm.getFilter(), m_AntennaInfoForm.getInfo());
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
            string accessStats = null;
            if (result == "Access Succeeded")
            {
                if (this.m_SelectedTagID == string.Empty || this.m_SelectedTagID == null)
                {
                    uint successCount, failureCount;
                    successCount = failureCount = 0;
                    m_ReaderAPI.Actions.TagAccess.GetLastAccessResult(ref successCount, ref failureCount);
                    accessStats = "Succeeded on " + successCount.ToString() + "tags. Failed on " + failureCount.ToString();
                }
                if (m_AccessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ)
                {
                    if (m_SelectedTagID != String.Empty)
                    {
                        if (m_ReadTag.OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ &&
                            m_ReadTag.OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS)
                        {
                            string tagID = m_ReadTag.TagID + m_ReadTag.MemoryBank.ToString()
                                + m_ReadTag.MemoryBankDataOffset.ToString();
                            this.m_ReadForm.ReadData_TB.Text = m_ReadTag.MemoryBankData;
                            bool bAddNewEntry = false;
                            if (this.inventoryList.Items.Count > 0)
                            {
                                if (this.inventoryList.SelectedIndices.Count > 0)
                                {
                                    int selectedIndex = this.inventoryList.SelectedIndices[0];
                                    ListViewItem item = this.inventoryList.Items[selectedIndex];

                                    if (item.SubItems[0].Text == m_ReadTag.TagID)
                                    {
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
                                                //Add the new item to the list
                                                bAddNewEntry = true;
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
                                                m_TagTable.Add(tagID, item.Index);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                                //Add the new item to the list
                                bAddNewEntry = true;

                            if (bAddNewEntry)
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
                                    m_TagTable.Add(tagID, newItem.Index);
                                }
                            }
                        }
                        notifyUser("Read Succeed", "Access Operation");
                    }
                    else
                        notifyUser(accessStats, "Read Operation");
                }
                else if (m_AccessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE)
                {
                    m_WriteForm.writeButton.Enabled = true;
                    if (this.m_SelectedTagID == string.Empty || this.m_SelectedTagID == null)
                        notifyUser(accessStats, "Write Operation");
                    else
                        notifyUser("Write Succeed", "Access Operation");
                }
                else if (m_AccessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_LOCK)
                {
                    m_LockForm.lockButton.Enabled = true;
                    if (this.m_SelectedTagID == string.Empty || this.m_SelectedTagID == null)
                        notifyUser(accessStats, "Lock Operation");
                    else
                        notifyUser("Lock Succeed", "Access Operation");
                }
                else if (m_AccessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL)
                {
                    m_KillForm.killButton.Enabled = true;
                    if (this.m_SelectedTagID == string.Empty || this.m_SelectedTagID == null)
                        notifyUser(accessStats, "Kill Operation");
                    else
                        notifyUser("Kill Succeed", "Access Operation");
                }
                else if (m_AccessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_WRITE)
                {
                    m_BlockWriteForm.writeButton.Enabled = true;
                    if (this.m_SelectedTagID == string.Empty || this.m_SelectedTagID == null)
                        notifyUser(accessStats, "Block Write Operation");
                    else
                        notifyUser("Block Write Succeed", "Access Operation");
                }
                else if (m_AccessOpResult.m_OpCode == ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE)
                {
                    m_BlockEraseForm.eraseButton.Enabled = true;
                    if (this.m_SelectedTagID == string.Empty || this.m_SelectedTagID == null)
                        notifyUser(accessStats, "Write Operation");
                    else
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
            UpdateSettingsFromRegistry();
            this.memBank_CB.SelectedIndex = 0;
            this.Connect("Connect");
            SendMessage(inventoryList.Handle, LVM_SETTEXTBKCOLOR, IntPtr.Zero, unchecked((IntPtr)(int)0xFFFFFF));
        }

        private void AppForm_Closing(object sender, EventArgs e)
        {
            SaveSettingsToRegistry();
            CloseForm();
        }

        private void processUIOrGPIEvent(bool startRead)
        {
            try
            {
                if (m_IsConnected)
                {
                    if (startRead)
                    {
                        StartReading();
                    }
                    else
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

        private void readButton_Click(object sender, EventArgs e)
        {
            processUIOrGPIEvent(readButton.Text == "Start Reading");
        }

        private void StartReading()
        {
            if (!m_isReading)
            {
                try
                {
                    m_ReaderAPI.Actions.PurgeTags();
                    if (m_ReaderAPI.Actions.TagAccess.OperationSequence.Length > 0)
                    {
                        m_ReaderAPI.Actions.TagAccess.OperationSequence.PerformSequence(m_AccessFilterForm.getFilter(),
                                m_TriggerForm.getTriggerInfo(), m_AntennaInfoForm.getInfo());
                    }
                    else
                    {
                        m_ReaderAPI.Actions.Inventory.Perform(m_PostFilterForm.getFilter(), m_TriggerForm.getTriggerInfo(),
                            m_AntennaInfoForm.getInfo());
                    }
                    this.inventoryList.Items.Clear();
                    this.m_TagTable.Clear();
                    this.m_TagTotalCount = 0;

                    // If the start trigger is not START_TRIGGER_TYPE_IMMEDIATE, the Inventory Request 
                    // is processed, but it will start only when the start-criteria is satisfied.
                    // So we will change the button text to "Stop Reading" to indicate that the 
                    // Inventory Request is processed.
                    if (m_TriggerForm.getTriggerInfo().StartTrigger.Type != START_TRIGGER_TYPE.START_TRIGGER_TYPE_IMMEDIATE)
                        readButton.Text = "Stop Reading";

                    this.memBank_CB.Enabled = false;
                    this.autonomousMode_CB.Enabled = false;
                }
                catch (InvalidOperationException ioe)
                {
                    notifyUser("InvalidOperationException" + ioe.Message, "Read Operation");
                }
                catch (OperationFailureException ofe)
                {
                    notifyUser("OperationFailureException:" + ofe.StatusDescription, "Read Operation");
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
        }

        private void StopReading()
        {
            try
            {
                m_isReading = false;
                if (m_ReaderAPI.Actions.TagAccess.OperationSequence.Length > 0)
                {
                    m_ReaderAPI.Actions.TagAccess.OperationSequence.StopSequence();
                }
                else
                {
                    m_ReaderAPI.Actions.Inventory.Stop();
                }
                this.readButton.BackColor = m_DefaultButtonColor;
                this.readButton.Text = "Start Reading";
                m_ReaderAPI.Actions.PurgeTags();
                this.memBank_CB.Enabled = true;
                this.autonomousMode_CB.Enabled = true;
            }
            catch (InvalidOperationException ioe)
            {
                notifyUser("InvalidOperationException" + ioe.Message, "Stop Operation");
            }
            catch (OperationFailureException ofe)
            {
                notifyUser("OperationFailureException:" + ofe.StatusDescription, "Stop Read");
            }
            catch (InvalidUsageException iue)
            {
                notifyUser("InvalidUsageException:" + iue.Info, "Stop Read");
            }
            catch (Exception ex)
            {
                notifyUser("Exception:" + ex.Message, "Stop Read");
            }
            /*
             *  Update Read Time and Tags Read Count 
             */
            if (this.m_DurationTriggerTime == 0)
            {
                TimeSpan duration = TimeSpan.FromMilliseconds(Environment.TickCount).Subtract(m_StartTime).Duration();
                readTimeValueLabel.Text = duration.Hours + ":" + duration.Minutes + ":" + duration.Seconds + "." + duration.Milliseconds / 100 + " Sec";
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

        private void gpioMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void readPointMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void autonomousMode_CB_CheckStateChanged(object sender, EventArgs e)
        {
            if (m_IsConnected)
            {
                autonomousMode_CB.Checked = autonomousMode_CB.Checked &&
                (m_IsConnected &&
                    m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported);
            }

        }

        private void locateTagContextMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_LocateForm)
            {
                m_LocateForm = new LocateForm(this);
            }
            m_LocateForm.ShowDialog();

        }

        private void beepControlmenuItem_Click(object sender, EventArgs e)
        {
            if (this.beepOffMenuItem.Checked)
            {
                this.beepOffMenuItem.Checked = false;
            }
            else
            {
                this.beepOffMenuItem.Checked = true;
            }
            m_isBeepingEnabled = false == this.beepOffMenuItem.Checked;
        }


        private void SaveSettingsToRegistry()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey("Software\\Symbol\\RFID\\CS_RFID3Sample6", true);
            if (regKey == null)
            {
                regKey = Registry.LocalMachine.CreateSubKey("Software\\Symbol\\RFID\\CS_RFID3Sample6");
            }
            regKey.SetValue("DontBeepOnRead", false == m_isBeepingEnabled ? 1 : 0);
            regKey.Close();
        }

        private void UpdateSettingsFromRegistry()
        {
            int enabled;

            // These are the default values.
            m_isBeepingEnabled = true;

            RegistryKey regKey = Registry.LocalMachine.OpenSubKey("Software\\Symbol\\RFID\\CS_RFID3Sample6");
            if (regKey != null)
            {
                if (regKey.GetValue("DontBeepOnRead") != null)
                {
                    enabled = (int)regKey.GetValue("DontBeepOnRead");
                    m_isBeepingEnabled = enabled == 0;
                }
                this.beepOffMenuItem.Checked = false == m_isBeepingEnabled;
                regKey.Close();
            }
        }
    }
}