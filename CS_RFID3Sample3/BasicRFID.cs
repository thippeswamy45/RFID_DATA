using System;
using System.Collections.Generic;
using System.Text;
using Symbol.RFID3;

namespace BasicRFID
{
    public delegate void TagArrivedCallBack(TagData tagData);
    public delegate void EventSignalledCallBack(Events.StatusEventArgs e);

    class BasicRFID
    {       
        private RFIDReader m_RfidReader;
        internal TagArrivedCallBack m_TagArrivedCallBack;
        internal EventSignalledCallBack m_EventSignalledCallBack;
        public Boolean m_IsConnected;
        public void ConnectReader(string hostname, TagArrivedCallBack tagCB, EventSignalledCallBack eventCB)
        {
            m_RfidReader = new RFIDReader(hostname, 0, 0);
            m_RfidReader.Connect();
            m_IsConnected = true;
            m_TagArrivedCallBack = tagCB;
            m_EventSignalledCallBack = eventCB;

            m_RfidReader.Events.NotifyInventoryStartEvent = true;
            m_RfidReader.Events.NotifyInventoryStopEvent = true;
            m_RfidReader.Events.NotifyAccessStartEvent = true;
            m_RfidReader.Events.NotifyAccessStopEvent = true;
            m_RfidReader.Events.NotifyAntennaEvent = true;
            m_RfidReader.Events.NotifyBufferFullEvent = true;
            m_RfidReader.Events.NotifyBufferFullWarningEvent = true;
            m_RfidReader.Events.NotifyReaderExceptionEvent = true;
            m_RfidReader.Events.NotifyReaderDisconnectEvent = true;
            m_RfidReader.Events.StatusNotify += new Events.StatusNotifyHandler(Events_StatusNotify);
            m_RfidReader.Events.ReadNotify += new Events.ReadNotifyHandler(Events_ReadNotify);
              
        }
        public void ResetFactoryDefaults()
        {
            m_RfidReader.Config.ResetFactoryDefaults();
        }
        public void DisconnectReader()
        {
            m_RfidReader.Disconnect();
            m_IsConnected = false;
        }
        private void Events_StatusNotify(object sender, Events.StatusEventArgs e)
        {
          m_IsConnected = m_RfidReader.IsConnected;
          m_EventSignalledCallBack(e);
          if (e.StatusEventData.StatusEventType == Events.STATUS_EVENT_TYPE.BUFFER_FULL_EVENT || e.StatusEventData.StatusEventType == Events.STATUS_EVENT_TYPE.BUFFER_FULL_WARNING_EVENT)
          {
              TagData[] tagData = m_RfidReader.Actions.GetReadTags(1000);
              if (tagData != null)
              {
                  for (uint nIndex = 0; nIndex < tagData.Length; nIndex++)
                      m_TagArrivedCallBack(tagData[nIndex]);
              }
          }
                
        }

        private void Events_ReadNotify(object sender, Events.ReadEventArgs e)
        {
            m_TagArrivedCallBack(e.ReadEventData.TagData);
        }

        public void StartInventory()
        {
            m_RfidReader.Actions.Inventory.Perform();
        }

        public void StopInventory()
        {
            m_RfidReader.Actions.Inventory.Stop();
        }
        public string ReadTag(string tagId, MEMORY_BANK mb, Int32 length, Int32 offset)
        {
            string memoryBankData;
            TagAccess.ReadAccessParams readParams = new TagAccess.ReadAccessParams();
            readParams.AccessPassword = 0;
            readParams.ByteCount = (uint)length;
            readParams.MemoryBank = mb;
            readParams.ByteOffset = (uint)offset;
            TagData tagData = m_RfidReader.Actions.TagAccess.ReadWait(tagId, readParams, null);
            memoryBankData = tagData.MemoryBankData;
            return memoryBankData;
        }

        public RFIDResults WriteTag(string tagId, string writeData, MEMORY_BANK mb, Int32 offset)
        {
            byte[] writeUserData = null;
            writeUserData = new byte[writeData.Length / 2];

            ConvertStringToByteArray(writeData, ref writeUserData);

            TagAccess.WriteAccessParams writeParams = new TagAccess.WriteAccessParams();
            writeParams.AccessPassword = 0;
            writeParams.WriteData = writeUserData;
            writeParams.WriteDataLength = (uint)writeUserData.Length;
            writeParams.MemoryBank = mb;
            writeParams.ByteOffset = (uint)offset;
            try
            {
                m_RfidReader.Actions.TagAccess.WriteWait(tagId, writeParams, null);
                return RFIDResults.RFID_API_SUCCESS;
            }
            catch (OperationFailureException e)
            {
                return e.Result;
            }
        }

        public RFIDResults LockTag(string tagId, MEMORY_BANK mb, LOCK_PRIVILEGE privilege, uint pwd)
        {
            TagAccess.LockAccessParams lockParams = new TagAccess.LockAccessParams();
            lockParams.AccessPassword = pwd;
            lockParams.LockPrivilege[(int)mb] = privilege;
            try
            {
                m_RfidReader.Actions.TagAccess.LockWait(tagId, lockParams, null);
                return RFIDResults.RFID_API_SUCCESS;
            }
            catch (OperationFailureException e)
            {
                return e.Result;
            }
        }
       
        private void ConvertStringToByteArray(string sourceString, ref  byte[] sourceArray)
        {
            for (int i = 0; i < sourceString.Length / 4; i++)
            {
                sourceArray[2 * i] = Convert.ToByte(sourceString.Substring(i * 4, 2), 16);
                sourceArray[2 * i + 1] = Convert.ToByte(sourceString.Substring((i * 4) + 2, 2), 16);
            }
        }


    }
}
