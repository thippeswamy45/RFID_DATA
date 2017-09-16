using System;
using System.Collections.Generic;
using System.Text;
using Symbol.RFID3;
using System.Threading;

namespace CS_RFIDSample4
{
    class CS_RFIDSample4
    {
        // Connection Parameters HostName, ReaderPort
        string hostName;
        uint readerPort;

        // RFID Reader object 
        RFIDReader rfidReader;

        // Pre-Filters
        PreFilters.PreFilter filter;

        // Tag ID
        string tagID;

        // Access filter
        AccessFilter AccessFilter;

        // Event
        AutoResetEvent AccessComplete;
        AutoResetEvent InventoryComplete;

        public CS_RFIDSample4()
        {

        }

        public void ConnectToReader()
        {
            rfidReader = new RFIDReader(hostName, readerPort, 5084);
            try
            {

                rfidReader.Connect();

                AccessComplete = new AutoResetEvent(false);
                
                rfidReader.Events.NotifyInventoryStartEvent = true;
                rfidReader.Events.NotifyAccessStartEvent = true;
                rfidReader.Events.NotifyAccessStopEvent = true;
                rfidReader.Events.NotifyInventoryStopEvent = true;
                rfidReader.Events.NotifyAntennaEvent = true;
                rfidReader.Events.NotifyBufferFullWarningEvent = true;
                rfidReader.Events.NotifyBufferFullEvent = true;
                rfidReader.Events.NotifyGPIEvent = true;
                rfidReader.Events.NotifyReaderDisconnectEvent = true;
                rfidReader.Events.NotifyReaderExceptionEvent = true;
                rfidReader.Events.AttachTagDataWithReadEvent = false;
                rfidReader.Events.StatusNotify += new Events.StatusNotifyHandler(Events_StatusNotify);


            }

            catch (OperationFailureException operationFailureException)
            {
                Console.WriteLine("Operation Failed " + operationFailureException.StatusDescription + operationFailureException.VendorMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect" + ex.Message);
            }
        }

        private void Createmenu()
        {
            int option = 0;
            bool keepWorking = true;
            while (keepWorking)
            {
                Console.WriteLine("..................................................");
                Console.WriteLine("Welcome to RFID API3 .NET Standard Sample Application");
                Console.WriteLine("..................................................\n\n");

                Console.WriteLine("----Command Menu----");
                Console.WriteLine("1. Capability");
                Console.WriteLine("2. Configuration");
                Console.WriteLine("3. Inventory");
                Console.WriteLine("4. Access");
                Console.WriteLine("5. Exit");

                try
                {
                    option = Convert.ToInt32(Console.ReadLine());

                    switch (option)
                    {
                        // Capability
                        case 1:
                            DisplayCapability();
                            break;
                        // Configuration
                        case 2:
                            ConfigurationMenu();
                            break;
                        // Inventory operation
                        case 3:
                            InventoryMenu();
                            break;
                        // Access operation
                        case 4:
                            AccessMenu();
                            break;
                        // Application Exit
                        case 5:
                            rfidReader.Disconnect();
                            keepWorking = false;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void DisplayCapability()
        {
            Console.WriteLine("Reader Capabilities\n\n");
            Console.WriteLine("FirwareVersion={0}", rfidReader.ReaderCapabilities.FirwareVersion);
            Console.WriteLine("ModelName={0}", rfidReader.ReaderCapabilities.ModelName);
            Console.WriteLine("NumAntennaSupported={0}", rfidReader.ReaderCapabilities.NumAntennaSupported);
            Console.WriteLine("NumGPIPorts={0}", rfidReader.ReaderCapabilities.NumGPIPorts);
            Console.WriteLine("NumGPOPorts={0}", rfidReader.ReaderCapabilities.NumGPOPorts);
            Console.WriteLine("IsUTCClockSupported= {0}", rfidReader.ReaderCapabilities.IsUTCClockSupported);
            Console.WriteLine("IsBlockEraseSupported={0}", rfidReader.ReaderCapabilities.IsBlockEraseSupported);
            Console.WriteLine("IsBlockWriteSupported={0}", rfidReader.ReaderCapabilities.IsBlockWriteSupported);
            Console.WriteLine("IsTagInventoryStateAwareSingulationSupported={0}", rfidReader.ReaderCapabilities.IsTagInventoryStateAwareSingulationSupported);
            Console.WriteLine("MaxNumOperationsInAccessSequence={0}", rfidReader.ReaderCapabilities.MaxNumOperationsInAccessSequence);
            Console.WriteLine("MaxNumPreFilters={0}", rfidReader.ReaderCapabilities.MaxNumPreFilters);
            Console.WriteLine("CommunicationStandard={0}", rfidReader.ReaderCapabilities.CommunicationStandard);
            Console.WriteLine("CountryCode={0}", rfidReader.ReaderCapabilities.CountryCode);
            Console.WriteLine("IsHoppingEnabled={0}", rfidReader.ReaderCapabilities.IsHoppingEnabled);

        }

        private void GetSingulationControl()
        {
            int antennaID = 0;
            Antennas.SingulationControl g1_singulationControl;
            try
            {
                Console.WriteLine("Enter AntennaID");
                antennaID = Convert.ToInt32(Console.ReadLine());
                g1_singulationControl = rfidReader.Config.Antennas[antennaID].GetSingulationControl();


                Console.WriteLine("Session={0}", g1_singulationControl.Session);
                Console.WriteLine("TagPopulation={0}", g1_singulationControl.TagPopulation);
                Console.WriteLine("TagTransitTime={0}", g1_singulationControl.TagTransitTime);
                Console.WriteLine("inventoryState={0}", g1_singulationControl.Action.InventoryState);
                Console.WriteLine("SLFlag={0}", g1_singulationControl.Action.SLFlag);
                Console.WriteLine("PerformStateAwareSingulationAction={0}", g1_singulationControl.Action.PerformStateAwareSingulationAction);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        private void ConfigureGPOState()
        {
            Int32 portNumber;
            Byte GPOState = 0;
            Int16 option = 0;
            bool keepworking = true;
            while(keepworking)
            {
                Console.WriteLine("----Command Menu----");
                Console.WriteLine("1. SetGPOState");
                Console.WriteLine("2. GetGPOState");
                Console.WriteLine("");
                Console.WriteLine("3. Go back");
                try
                {
                    option = Convert.ToInt16(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("Enter portNumber");
                            portNumber = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter GPOState 0 to disable,1 to enable");
                            GPOState = Convert.ToByte(Console.ReadLine());
                            if (GPOState != 0)
                                rfidReader.Config.GPO[portNumber].PortState = GPOs.GPO_PORT_STATE.TRUE;
                            else
                                rfidReader.Config.GPO[portNumber].PortState = GPOs.GPO_PORT_STATE.FALSE;
                            Console.WriteLine("Set GPO Successfully");
                            break;
                        case 2:
                            Console.WriteLine("Enter portNumber");
                            portNumber = Convert.ToInt32(Console.ReadLine());
                            if (rfidReader.Config.GPO[portNumber].PortState == GPOs.GPO_PORT_STATE.TRUE)
                                Console.WriteLine("GPOEnable= TRUE");
                            else
                                Console.WriteLine("GPOEnable= FALSE");
                            break;
                        default:
                            keepworking = false;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private void ConfigureGPIState()
        {
            Int32 portNumber;

            try
            {
                Console.WriteLine("Enter portNumber");
                portNumber = Convert.ToInt32(Console.ReadLine());
                if (rfidReader.Config.GPI[portNumber].IsEnabled)
                    Console.WriteLine("GPIEnable= TRUE");
                else
                    Console.WriteLine("GPIEnable= TRUE");

                Console.WriteLine("portstate={0}", rfidReader.Config.GPI[portNumber].PortState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void ConfigureAntenna()
        {

            Int16 antennaID;
            bool keepworking = true;
            Antennas.Config config;

            Int16 option = 0;
            
                Console.WriteLine("Enter AntennaID");
                antennaID = Convert.ToInt16(Console.ReadLine());
                while (keepworking)
                {

                    Console.WriteLine("----Command Menu----");
                    Console.WriteLine("1. SetAntennaConfig");
                    Console.WriteLine("2. GetAntennaConfig");
                    Console.WriteLine("");
                    Console.WriteLine("3. Go back");
                    try
                    {
                        option = Convert.ToInt16(Console.ReadLine());

                        switch (option)
                        {
                            case 1:
                                {
                                    config = new Antennas.Config();
                                    Int32 length = 0;
                                    rfidReader.ReaderCapabilities.ReceiveSensitivityValues.GetLength(length);
                                    Console.WriteLine("Enter ReceiveSensitivityIndex  value ");

                                    config.ReceiveSensitivityIndex = Convert.ToUInt16(Console.ReadLine());
                                    Console.WriteLine("Enter TransmitPowerIndex  value ");
                                    config.TransmitPowerIndex = Convert.ToUInt16(Console.ReadLine());

                                    Console.WriteLine("Enter TransmitFrequencyIndex value ");
                                    config.TransmitFrequencyIndex = Convert.ToUInt16(Console.ReadLine());

                                    try
                                    {
                                        rfidReader.Config.Antennas[antennaID].SetConfig(config);
                                        Console.WriteLine("Set Antenna Configuration Successfully");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Failed to configure" + ex.Message);
                                    }
                                }
                                break;
                            case 2:
                                config = null;

                                config = rfidReader.Config.Antennas[antennaID].GetConfig();

                                Console.WriteLine("ReceiveSensitivityIndex={0}", config.ReceiveSensitivityIndex);
                                Console.WriteLine("TransmitPowerIndex={0}", config.TransmitPowerIndex);
                                Console.WriteLine("TransmitFrequencyIndex={0}", config.TransmitFrequencyIndex);

                                break;
                            default:
                                keepworking = false;
                                break;
                        }
                    }
                    catch (OperationFailureException opEx)
                    {
                        Console.WriteLine(opEx.VendorMessage);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
             
              }
              
            }
        

        private void ConfigureRFMode()
        {
            Int16 antennaID;
            Antennas.RFMode rfMode;

            Int16 option = 0;
            bool keepworking = true;
            Console.WriteLine("Enter AntennaID");
            antennaID = Convert.ToInt16(Console.ReadLine());

            while(keepworking)
            {
                          
                Console.WriteLine("----Command Menu----");
                Console.WriteLine("1. SetRFMode");
                Console.WriteLine("2. GetRFMode");
                Console.WriteLine("");
                Console.WriteLine("3. Go back");
                try
                {
                    option = Convert.ToInt16(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            {
                                rfMode = new Antennas.RFMode();
                                Console.WriteLine("Enter RfModeTable Index  value ");
                                rfMode.TableIndex = Convert.ToUInt16(Console.ReadLine());
                                Console.WriteLine("Enter Tari value ");
                                rfMode.Tari = Convert.ToUInt16(Console.ReadLine());

                                try
                                {
                                    rfidReader.Config.Antennas[antennaID].SetRFMode(rfMode);
                                    Console.WriteLine("Set RF Mode Successfully");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Failed to configure RF Mode: " + ex.Message);
                                }
                            }
                            break;
                        case 2:
                            {
                                rfMode = null;
                                try
                                {
                                    rfMode = rfidReader.Config.Antennas[antennaID].GetRFMode();
                                    Console.WriteLine("RfModeTableIndex={0}", rfMode.TableIndex);
                                    Console.WriteLine("Tari={0}", rfMode.Tari);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Failed to configure" + ex.Message);
                                }


                            }
                            break;
                        default:
                            keepworking = false;
                            break;

                    }
                }
                catch (OperationFailureException opEx)
                {
                    Console.WriteLine(opEx.VendorMessage);
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                
             }
        }

        private void SimpleInventory()
        {
            Console.WriteLine("simple inventory started");
            InventoryComplete = new AutoResetEvent(false);
            InventoryComplete.Reset();
            TriggerInfo triggerInfo = new TriggerInfo();

            TagData reportedTag = new TagData();
            rfidReader.Actions.Inventory.Perform();
            Thread.Sleep(1000);
            rfidReader.Actions.Inventory.Stop();
            GetReadTags();
      
        }
        private void GetReadTags()
        {

            try
            {
                /* Get the tags */
                
                TagData[] tagData = rfidReader.Actions.GetReadTags(1000);
                if (tagData != null)
                    for (int nIndex = 0; nIndex < tagData.Length; nIndex++)
                     Console.WriteLine("GetReadTags(): Tag available in Queue: " + tagData[nIndex].TagID);
                    
            }            
            catch (OperationFailureException e)
            {
                Console.WriteLine(e.VendorMessage);
            }
        }


        private void PeriodicInventory()
        {
            Console.WriteLine("Periodic inventory started");
            InventoryComplete = new AutoResetEvent(false);
            InventoryComplete.Reset();

            TriggerInfo triggerInfo = new TriggerInfo();
            triggerInfo.StartTrigger.Type = START_TRIGGER_TYPE.START_TRIGGER_TYPE_PERIODIC;
            triggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_DURATION;
            DateTime currentTime = DateTime.UtcNow;
            triggerInfo.StartTrigger.Periodic.Period = 2000; // 1 sec
            triggerInfo.StartTrigger.Periodic.StartTime = currentTime.AddSeconds(03);
            triggerInfo.StopTrigger.Duration = 1000; // 400ms
            triggerInfo.TagReportTrigger = 1;

            rfidReader.Actions.Inventory.Perform(null, triggerInfo, null);
            Thread.Sleep(6000);
            rfidReader.Actions.Inventory.Stop();
            GetReadTags();
           
        }
        private void AddPreFilter()
        {
            string tagMask;
            Int16 memoryBank;
            Int16 action;
            UInt32 byteCount;
            string temp;
            filter = new PreFilters.PreFilter();

            Console.WriteLine("----Command Menu----");
            Console.WriteLine("Enter AntennaID");
            filter.AntennaID = Convert.ToUInt16(Console.ReadLine());
            Console.WriteLine("Enter Memorybank ");
            Console.WriteLine(" 1 for EPC  ");
            Console.WriteLine(" 2 for TID  ");
            Console.WriteLine(" 3 for USER  ");
            memoryBank = Convert.ToInt16(Console.ReadLine());
            if (memoryBank == 1)
                filter.MemoryBank = MEMORY_BANK.MEMORY_BANK_EPC;
            if (memoryBank == 2)
                filter.MemoryBank = MEMORY_BANK.MEMORY_BANK_TID;
            if (memoryBank == 3)
                filter.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER;

            Console.WriteLine(" Enter TagPattern Count in Bits ");
            filter.TagPatternBitCount = Convert.ToUInt32(Console.ReadLine());
            byteCount = filter.TagPatternBitCount / 8;
            filter.TagPattern = new byte[byteCount];
            Console.WriteLine(" Enter Tag Pattern ");
            tagMask = Console.ReadLine();

            for (int i = 0; i < byteCount; i++)
            {
                temp = tagMask.Substring(i * 2, 2);
                filter.TagPattern[i] = Convert.ToByte(temp, 16);
            }
            Console.WriteLine(" Enter Bit OffSet ");
             
            filter.BitOffset = Convert.ToUInt32(Console.ReadLine());

            filter.FilterAction = FILTER_ACTION.FILTER_ACTION_DEFAULT;

            Console.WriteLine("Enter stateUnawareAction");
            Console.WriteLine("0 for STATE_UNAWARE_ACTION_SELECT_NOT_UNSELECT ");
            Console.WriteLine("1 for STATE_UNAWARE_ACTION_SELECT ");
            Console.WriteLine("2 for STATE_UNAWARE_ACTION_NOT_UNSELECT ");
            Console.WriteLine("3 for STATE_UNAWARE_ACTION_UNSELECT ");
            Console.WriteLine("4 for STATE_UNAWARE_ACTION_UNSELECT_NOT_SELECT ");
            Console.WriteLine("5 for STATE_UNAWARE_ACTION_NOT_SELECT ");
            action = Convert.ToInt16(Console.ReadLine());

            if (action == 0)
                filter.StateUnawareAction.Action = STATE_UNAWARE_ACTION.STATE_UNAWARE_ACTION_SELECT_NOT_UNSELECT;
            if (action == 1)
                filter.StateUnawareAction.Action = STATE_UNAWARE_ACTION.STATE_UNAWARE_ACTION_SELECT;
            if (action == 2)
                filter.StateUnawareAction.Action = STATE_UNAWARE_ACTION.STATE_UNAWARE_ACTION_NOT_UNSELECT;
            if (action == 3)
                filter.StateUnawareAction.Action = STATE_UNAWARE_ACTION.STATE_UNAWARE_ACTION_UNSELECT;
            if (action == 4)
                filter.StateUnawareAction.Action = STATE_UNAWARE_ACTION.STATE_UNAWARE_ACTION_UNSELECT_NOT_SELECT;
            if (action == 5)
                filter.StateUnawareAction.Action = STATE_UNAWARE_ACTION.STATE_UNAWARE_ACTION_NOT_SELECT;

            try
            {
                rfidReader.Actions.PreFilters.DeleteAll();
                rfidReader.Actions.PreFilters.Add(filter);
                Console.WriteLine("Add PreFilter Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to add filter" + ex.Message);
            }
        }

        private void RemovePrefilter()
        {
            try
            {
                rfidReader.Actions.PreFilters.Delete(filter);
                Console.WriteLine("Remove PreFilter Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to delete filter" + ex.Message);
            }

        }

        private void ReadTagsWithEPCID()
        {
            Int16 memoryBank;
            TagData readAccessTag;
            TagAccess.ReadAccessParams readParams = new TagAccess.ReadAccessParams();
            Console.WriteLine("Enter accessPassword");
            readParams.AccessPassword = uint.Parse(Console.ReadLine(), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("Enter memoryBank ");
            Console.WriteLine("0 for RESERVED ");
            Console.WriteLine("1 for EPC ");
            Console.WriteLine("2 for TID ");
            Console.WriteLine("3 for USER ");

            memoryBank = Convert.ToInt16(Console.ReadLine());
            if (memoryBank == 0)
                readParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_RESERVED;
            if (memoryBank == 1)
                readParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_EPC;
            if (memoryBank == 2)
                readParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_TID;
            if (memoryBank == 3)
                readParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER;
            Console.WriteLine("Enter byte offset ");
            readParams.ByteOffset = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Enter byte length ");
            readParams.ByteCount = Convert.ToUInt32(Console.ReadLine());
            try
            {
                /*Test case to Read Single Tag Based on TagID*/
                readAccessTag = rfidReader.Actions.TagAccess.ReadWait(tagID, readParams, null);
                Console.WriteLine("Read-Data  : " + readAccessTag.MemoryBankData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to read tags " + ex.Message);
            }


        }

        private void WriteTagsWithEPCID()
        {
            Int16 memoryBank;
            string writeData;
            TagAccess.WriteAccessParams WriteParams = new TagAccess.WriteAccessParams();
            Console.WriteLine("Enter accessPassword");
            WriteParams.AccessPassword = uint.Parse(Console.ReadLine(), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("Enter memoryBank ");
            Console.WriteLine("0 for RESERVED ");
            Console.WriteLine("1 for EPC ");
            Console.WriteLine("2 for TID ");
            Console.WriteLine("3 for USER ");

            memoryBank = Convert.ToInt16(Console.ReadLine());

            if (memoryBank == 0)
                WriteParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_RESERVED;
            if (memoryBank == 1)
                WriteParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_EPC;
            if (memoryBank == 2)
                WriteParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_TID;
            if (memoryBank == 3)
                WriteParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER;
            Console.WriteLine("Enter byte offset ");
            WriteParams.ByteOffset = Convert.ToUInt32(Console.ReadLine());

            Console.WriteLine("Enter data to be written ");

            writeData = Console.ReadLine();
            byte[] writeUserData = null;
            writeUserData = new byte[writeData.Length / 2];

            ConvertStringToByteArray(writeData, ref writeUserData);
            WriteParams.WriteData = writeUserData;
            WriteParams.WriteDataLength = (uint)writeUserData.Length;

            try
            {
                rfidReader.Actions.TagAccess.WriteWait(tagID, WriteParams, null);
                Console.WriteLine("Data witten on tag successfully");

            }
            catch (OperationFailureException ex)
            {
                Console.WriteLine("Failed to write tags " + ex.VendorMessage);
            }
            catch (InvalidUsageException e)
            {
                Console.WriteLine("Failed to write tags " + e.Message);
            }
        }


        private void LockTagsWithEPCID()
        {
            Int16 temp;
            Int32 memoryBank = 0;
            Int16 priviledge;
            TagAccess.LockAccessParams lockAccessParams = new TagAccess.LockAccessParams();
            Console.WriteLine("Enter accessPassword");
            lockAccessParams.AccessPassword = uint.Parse(Console.ReadLine(), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("Enter memory to be locked..");
            Console.WriteLine("0 for Kill Password ");
            Console.WriteLine("1 for Access Password ");
            Console.WriteLine("2 for EPC Memory ");
            Console.WriteLine("3 for TID Memory ");
            Console.WriteLine("4 for User  Memory ");


            temp = Convert.ToInt16(Console.ReadLine());
            if (temp == 0)
                memoryBank = lockAccessParams.KillPasswordMemory;
            if (temp == 1)
                memoryBank = lockAccessParams.AccessPasswordMemory;
            if (temp == 2)
                memoryBank = lockAccessParams.EPCMemory;
            if (temp == 3)
                memoryBank = lockAccessParams.TIDMemory;
            if (temp == 4)
                memoryBank = lockAccessParams.UserMemory;



            Console.WriteLine("Enter the locking priviledge ");
            Console.WriteLine("0 for PRIVILEGE_NONE ");
            Console.WriteLine("1 for PRIVILEGE_READ_WRITE");
            Console.WriteLine("2 for PRIVILEGE_PERMA_LOCK ");
            Console.WriteLine("3 for PRIVILEGE_PERMA_UNLOCK ");
            Console.WriteLine("4 for PRIVILEGE_UNLOCK ");
            priviledge = Convert.ToInt16(Console.ReadLine());

            if (priviledge == 0)
                lockAccessParams.LockPrivilege[memoryBank] = LOCK_PRIVILEGE.LOCK_PRIVILEGE_NONE;
            if (priviledge == 1)
                lockAccessParams.LockPrivilege[memoryBank] = LOCK_PRIVILEGE.LOCK_PRIVILEGE_READ_WRITE;
            if (priviledge == 2)
                lockAccessParams.LockPrivilege[memoryBank] = LOCK_PRIVILEGE.LOCK_PRIVILEGE_PERMA_LOCK;
            if (priviledge == 3)
                lockAccessParams.LockPrivilege[memoryBank] = LOCK_PRIVILEGE.LOCK_PRIVILEGE_PERMA_UNLOCK;
            if (priviledge == 4)
                lockAccessParams.LockPrivilege[memoryBank] = LOCK_PRIVILEGE.LOCK_PRIVILEGE_UNLOCK;


            try
            {
                rfidReader.Actions.TagAccess.LockWait(tagID, lockAccessParams, null);
                Console.WriteLine("Tag locked successfully");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to read tags " + ex.Message);
            }
        }

        private void KillTagsWithEPCID()
        {
            TagAccess.KillAccessParams killAccessParams = new TagAccess.KillAccessParams();
            Console.WriteLine("Enter KillPassword");
            killAccessParams.KillPassword = uint.Parse(Console.ReadLine(), System.Globalization.NumberStyles.HexNumber);
            try
            {
                rfidReader.Actions.TagAccess.KillWait(tagID, killAccessParams, null);
                Console.WriteLine("Tag killed successfully");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to kill tags " + ex.Message);
            }

        }

        private bool AddAccessFilter()
        {
            Int16 memoryBank;
            string memoryBankData,tagMask;
            uint tempByteLength = 0;

            string temp;
            AccessFilter = new AccessFilter();
            AccessFilter.TagPatternA = new TagPatternBase();
            Console.WriteLine("---Access Filter Info---");
            Console.WriteLine("Enter memoryBank");
            Console.WriteLine("0 for RESERVED ");
            Console.WriteLine("1 for EPC ");
            Console.WriteLine("2 for TID ");
            Console.WriteLine("3 for USER ");

            try
            {
                memoryBank = Convert.ToInt16(Console.ReadLine());

                if (memoryBank == 0)
                    AccessFilter.TagPatternA.MemoryBank = MEMORY_BANK.MEMORY_BANK_RESERVED;
                if (memoryBank == 1)
                    AccessFilter.TagPatternA.MemoryBank = MEMORY_BANK.MEMORY_BANK_EPC;
                if (memoryBank == 2)
                    AccessFilter.TagPatternA.MemoryBank = MEMORY_BANK.MEMORY_BANK_TID;
                if (memoryBank == 3)
                    AccessFilter.TagPatternA.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER;

                Console.WriteLine("Enter offset in Bits ");
                AccessFilter.TagPatternA.BitOffset = Convert.ToUInt32(Console.ReadLine());


                Console.WriteLine("Enter Tag Pattern Length in Bits ");
                AccessFilter.TagPatternA.TagPatternBitCount = Convert.ToUInt32(Console.ReadLine());
                tempByteLength = (AccessFilter.TagPatternA.TagPatternBitCount + 7) / 8;
                AccessFilter.TagPatternA.TagPattern = new byte[tempByteLength];
                Console.WriteLine("Enter Tag Pattern ");
                memoryBankData = Console.ReadLine();
                for (int j = 0; j < tempByteLength; j++)
                {
                    temp = memoryBankData.Substring(j * 2, 2);
                    AccessFilter.TagPatternA.TagPattern[j] = Convert.ToByte(temp, 16);

                };
                
                Console.WriteLine("Enter tagMaskLength in Bits");
                AccessFilter.TagPatternA.TagMaskBitCount = Convert.ToUInt32(Console.ReadLine());
                tempByteLength = (AccessFilter.TagPatternA.TagMaskBitCount + 7) / 8;
                AccessFilter.TagPatternA.TagMask = new byte[tempByteLength];

                Console.WriteLine("Enter tagMask ");
                tagMask = Console.ReadLine();
                for (int j = 0; j < tempByteLength; j++)
                {
                    temp = tagMask.Substring(j * 2, 2);
                    AccessFilter.TagPatternA.TagMask[j] = Convert.ToByte(temp, 16);
                }
                
                AccessFilter.MatchPattern = MATCH_PATTERN.A;
                Console.WriteLine("Access Filter Set");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Access Filter not set");
                return false;
            }
        }
        private void ReadTagsWithAccessFilter()
        {
            Int16 memoryBank;
            uint successCount = 0, failureCount = 0;

            TagAccess.ReadAccessParams readParams = new TagAccess.ReadAccessParams();
            Console.WriteLine("Enter accessPassword");
            readParams.AccessPassword = uint.Parse(Console.ReadLine(), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("Enter memoryBank ");
            Console.WriteLine("0 for RESERVED ");
            Console.WriteLine("1 for EPC ");
            Console.WriteLine("2 for TID ");
            Console.WriteLine("3 for USER ");

            memoryBank = Convert.ToInt16(Console.ReadLine());
            if (memoryBank == 0)
                readParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_RESERVED;
            if (memoryBank == 1)
                readParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_EPC;
            if (memoryBank == 2)
                readParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_TID;
            if (memoryBank == 3)
                readParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER;
            Console.WriteLine("Enter byte offset ");
            readParams.ByteOffset = (uint)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter byte length ");
            readParams.ByteCount = Convert.ToUInt32(Console.ReadLine());

            try
            {
                AccessComplete.Reset();
                rfidReader.Actions.TagAccess.ReadEvent(readParams, AccessFilter, null);
                AccessComplete.WaitOne();
                rfidReader.Actions.TagAccess.GetLastAccessResult(ref successCount, ref failureCount);
                if (successCount > 0)
                {
                    TagData[] tagData = rfidReader.Actions.GetReadTags(1000);
                    if (tagData != null)
                        for (int y = 0; y < tagData.Length; y++)
                        {
                            Console.WriteLine("FilterRead   : " + tagData[y].MemoryBankData);
                        }
                }
            }
            catch (OperationFailureException e)
            {
                Console.WriteLine(e.Result.ToString());

            }

        }

        private void WriteTagsWithAccessFilter()
        {
            Int16 memoryBank;
            string writeData;
            string temp;
            uint successCount = 0, failureCount = 0;

            TagAccess.WriteAccessParams WriteParams = new TagAccess.WriteAccessParams();
            Console.WriteLine("Enter accessPassword");
            WriteParams.AccessPassword = uint.Parse(Console.ReadLine(), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("Enter memoryBank ");
            Console.WriteLine("0 for RESERVED ");
            Console.WriteLine("1 for EPC ");
            Console.WriteLine("2 for TID ");
            Console.WriteLine("3 for USER ");
            memoryBank = Convert.ToInt16(Console.ReadLine());
            if (memoryBank == 0)
                WriteParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_RESERVED;
            if (memoryBank == 1)
                WriteParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_EPC;
            if (memoryBank == 2)
                WriteParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_TID;
            if (memoryBank == 3)
                WriteParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER;
            Console.WriteLine("Enter byte offset ");
            WriteParams.ByteOffset = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Enter byte length ");
            WriteParams.WriteDataLength = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Enter data to be written ");

            writeData = Console.ReadLine();
            WriteParams.WriteData = new byte[WriteParams.WriteDataLength];

            for (int i = 0; i < WriteParams.WriteDataLength; i++)
            {

                temp = writeData.Substring(i * 2, 2);
                WriteParams.WriteData[i] = Convert.ToByte(temp, 16);
            }

            try
            {
                AccessComplete.Reset();
                rfidReader.Actions.TagAccess.WriteEvent(WriteParams, AccessFilter, null);
                AccessComplete.WaitOne();
                successCount = failureCount = 0;
                rfidReader.Actions.TagAccess.GetLastAccessResult(ref successCount, ref failureCount);
                if (successCount > 0)
                    Console.WriteLine("WriteEvent Success on : {0} tags  ",successCount);
                if (failureCount > 0)
                    Console.WriteLine("WriteEvent Failed on : {0} tags  ", failureCount);
            }
            catch (OperationFailureException e)
            {
                Console.WriteLine("Write operation  Failed   " + e.Result.ToString());
            }

        }

        private void LockTagsWithAccessFilter()
        {
            Int16 temp;
            Int32 memoryBank = 0;
            Int16 priviledge;
            uint successCount = 0, failureCount = 0;


            TagAccess.LockAccessParams lockAccessParams = new TagAccess.LockAccessParams();
            Console.WriteLine("Enter accessPassword");
            lockAccessParams.AccessPassword = uint.Parse(Console.ReadLine(), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("Enter memory to be locked..");
            Console.WriteLine("0 for Kill Password ");
            Console.WriteLine("1 for Access Password ");
            Console.WriteLine("2 for EPC Memory ");
            Console.WriteLine("3 for TID Memory ");
            Console.WriteLine("4 for User  Memory ");


            temp = Convert.ToInt16(Console.ReadLine());
            if (temp == 0)
                memoryBank = lockAccessParams.KillPasswordMemory;
            if (temp == 1)
                memoryBank = lockAccessParams.AccessPasswordMemory;
            if (temp == 2)
                memoryBank = lockAccessParams.EPCMemory;
            if (temp == 3)
                memoryBank = lockAccessParams.TIDMemory;
            if (temp == 4)
                memoryBank = lockAccessParams.UserMemory;



            Console.WriteLine("Enter the locking priviledge ");
            Console.WriteLine("0 for PRIVILEGE_NONE ");
            Console.WriteLine("1 for PRIVILEGE_READ_WRITE");
            Console.WriteLine("2 for PRIVILEGE_PERMA_LOCK ");
            Console.WriteLine("3 for PRIVILEGE_PERMA_UNLOCK ");
            Console.WriteLine("4 for PRIVILEGE_UNLOCK ");
            priviledge = Convert.ToInt16(Console.ReadLine());

            if (priviledge == 0)
                lockAccessParams.LockPrivilege[memoryBank] = LOCK_PRIVILEGE.LOCK_PRIVILEGE_NONE;
            if (priviledge == 1)
                lockAccessParams.LockPrivilege[memoryBank] = LOCK_PRIVILEGE.LOCK_PRIVILEGE_READ_WRITE;
            if (priviledge == 2)
                lockAccessParams.LockPrivilege[memoryBank] = LOCK_PRIVILEGE.LOCK_PRIVILEGE_PERMA_LOCK;
            if (priviledge == 3)
                lockAccessParams.LockPrivilege[memoryBank] = LOCK_PRIVILEGE.LOCK_PRIVILEGE_PERMA_UNLOCK;
            if (priviledge == 4)
                lockAccessParams.LockPrivilege[memoryBank] = LOCK_PRIVILEGE.LOCK_PRIVILEGE_UNLOCK;

            try
            {
                AccessComplete.Reset();
                rfidReader.Actions.TagAccess.LockEvent(lockAccessParams, AccessFilter, null);
                AccessComplete.WaitOne();
                successCount = failureCount = 0;

                rfidReader.Actions.TagAccess.GetLastAccessResult(ref successCount, ref failureCount);
                if (successCount > 0)
                    Console.WriteLine("LockEvent Success on : {0} tags  " ,successCount);
                if (failureCount > 0)
                    Console.WriteLine("LockEvent Failed on : {0} tags  " , failureCount);

            }
            catch (OperationFailureException e)
            {
                Console.WriteLine("Lock operation  Failed   " + e.Result.ToString());
            }
        }

        private void KillTagsWithAccessFilter()
        {
            uint successCount = 0, failureCount = 0;

            TagAccess.KillAccessParams killAccessParams = new TagAccess.KillAccessParams();
            Console.WriteLine("Enter KillPassword");
            killAccessParams.KillPassword = uint.Parse(Console.ReadLine(), System.Globalization.NumberStyles.HexNumber);
            try
            {
                AccessComplete.Reset();
                rfidReader.Actions.TagAccess.KillEvent(killAccessParams, AccessFilter, null);
                AccessComplete.WaitOne(10000, false);
                successCount = failureCount = 0;

                rfidReader.Actions.TagAccess.GetLastAccessResult(ref successCount, ref failureCount);
                if (successCount > 0)
                    Console.WriteLine("KillEvent Success on : {0} tags  " , successCount);
                if (failureCount > 0)
                    Console.WriteLine("KillEvent Failed on : {0} tags  " , failureCount);

            }
            catch (OperationFailureException e)
            {
                Console.WriteLine("Kill operation  Failed   " + e.Result.ToString());
            }



        }
        private void AccessOperationWithAccessFilter()
        {
            Int16 option;
            bool keepworking = true;
           
            keepworking = AddAccessFilter();
            while(keepworking)
            {
                Console.WriteLine("");
                Console.WriteLine("----Command Menu----");
                Console.WriteLine("1.Read Tag");
                Console.WriteLine("2.Write Tag");
                Console.WriteLine("3.Lock Tag");
                Console.WriteLine("4.Kill Tag");
                Console.WriteLine("5.Go back to Access Menu");
                option = Convert.ToInt16(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        ReadTagsWithAccessFilter();
                        break;
                    case 2:
                        WriteTagsWithAccessFilter();
                        break;
                    case 3:
                        LockTagsWithAccessFilter();
                        break;
                    case 4:
                        KillTagsWithAccessFilter();
                        break;
                    default:
                        keepworking = false;
                        break;
                }
            }

        }
        private void AccessOperationWithEPCID()
        {
            Int16 option;
            bool keepworking = true;
            Console.WriteLine("Enter TagID ");
            tagID = Console.ReadLine();
            while (keepworking)
            {
                Console.WriteLine("");
                Console.WriteLine("----Command Menu----");
                Console.WriteLine("1. Read Tag");
                Console.WriteLine("2. Write Tag");
                Console.WriteLine("3. Lock Tag");
                Console.WriteLine("4. Kill Tag");
                Console.WriteLine("5. Go back to Access Menu");

                option = Convert.ToInt16(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        ReadTagsWithEPCID();
                        break;
                    case 2:
                        WriteTagsWithEPCID();
                        break;
                    case 3:
                        LockTagsWithEPCID();
                        break;
                    case 4:
                        KillTagsWithEPCID();
                        break;
                    default:
                        keepworking = false;
                        break;
                }
            }
        }
  
        private void InventoryFilterOption()
        {
            int option = 0;
            bool keepworking = true;
            while (keepworking)
            {

                Console.WriteLine("");
                Console.WriteLine("----Command Menu----");
                Console.WriteLine("1. Add Pre-Filter [only 1 filter is allowed]");
                Console.WriteLine("2. Remove PreFilter");
                Console.WriteLine("3. Back to Inventory Menu");
                option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        AddPreFilter();
                        break;
                    case 2:
                        RemovePrefilter();
                        break;
                    case 3:
                        keepworking = false;
                        break;
                }
            }
        }


        private void ConfigurationMenu()
        {

            int option = 0;
            bool keepworking = true;
            while (keepworking)
            {
                Console.WriteLine("");
                Console.WriteLine("----Configuration Sub Menu---");
                Console.WriteLine("1. Get Singulation Control");
                Console.WriteLine("2. GPO");
                Console.WriteLine("3. GPI");
                Console.WriteLine("4. Antenna Config");
                Console.WriteLine("5. RF Mode ");
                Console.WriteLine("6. Back to Main Menu");
                option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        GetSingulationControl();
                        break;
                    case 2:
                        ConfigureGPOState();
                        break;
                    case 3:
                        ConfigureGPIState();
                        break;
                    case 4:
                        ConfigureAntenna();
                        break;
                    case 5:
                        ConfigureRFMode();
                        break;
                    default:
                        keepworking = false;
                        break;
                }
            }

        }

        private void InventoryMenu()
        {
            Int16 option = 0;
            bool keepworking = true;
            while (keepworking)
            {
                Console.WriteLine("");
                Console.WriteLine("----Inventory Sub Menu----");
                Console.WriteLine("1. Simple");
                Console.WriteLine("2. Periodic Inventory");
                Console.WriteLine("3. Pre-Filter");
                Console.WriteLine("4. Back to Main Menu");

            option = Convert.ToInt16(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        SimpleInventory();
                        break;
                    case 2:
                        PeriodicInventory();
                        break;
                    case 3:
                        InventoryFilterOption();
                        break;
                    default:
                        keepworking = false;
                        break;
                }
            }
        }

        public void AccessMenu()
        {
            Int16 option = 0;
            bool keepworking = true;
            while (keepworking)
            {
                Console.WriteLine("");
                Console.WriteLine("----Access Sub Menu----");
                Console.WriteLine("1. Access Operation with Specific EPC-ID ");
                Console.WriteLine("2. Access Operation with Access-Filters");
                Console.WriteLine("3. Back to Main Menu");
                option = Convert.ToInt16(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AccessOperationWithEPCID();
                        break;
                    case 2:
                        AccessOperationWithAccessFilter();
                        break;
                    default:
                        keepworking = false;
                        break;
                }
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

        public void Events_StatusNotify(object sender, Events.StatusEventArgs e)
        {
            switch (e.StatusEventData.StatusEventType)
            {
                case Events.STATUS_EVENT_TYPE.INVENTORY_START_EVENT:
                    InventoryComplete.Reset();
                    Console.WriteLine("SIGNALLED: INVENTORY_START_EVENT");
                    break;
                case Events.STATUS_EVENT_TYPE.INVENTORY_STOP_EVENT:
                    InventoryComplete.Set();
                    Console.WriteLine("SIGNALLED: INVENTORY_STOP_EVENT");
                    break;
                case Events.STATUS_EVENT_TYPE.DISCONNECTION_EVENT:
                    Console.WriteLine("SIGNALLED: DISCONNECTION_EVENT");
                    Console.WriteLine("Please Exit and Restart the Application");
                    break;
                case Events.STATUS_EVENT_TYPE.ACCESS_START_EVENT:
                    AccessComplete.Reset();
                    Console.WriteLine("SIGNALLED: ACCESS_START_EVENT");
                    break;
                case Events.STATUS_EVENT_TYPE.ACCESS_STOP_EVENT:
                    AccessComplete.Set();
                    Console.WriteLine("SIGNALLED: ACCESS_STOP_EVENT");
                    break;
                case Events.STATUS_EVENT_TYPE.ANTENNA_EVENT:
                    Console.WriteLine("SIGNALLED: ANTENNA_EVENT");
                    break;
                case Events.STATUS_EVENT_TYPE.BUFFER_FULL_WARNING_EVENT:
                    Console.WriteLine("SIGNALLED: BUFFER_FULL_WARNING_EVENT");
                    GetReadTags();
                    break;
                case Events.STATUS_EVENT_TYPE.BUFFER_FULL_EVENT:
                    Console.WriteLine("SIGNALLED: BUFFER_FULL_EVENT");
                    GetReadTags();
                    break;
                case Events.STATUS_EVENT_TYPE.GPI_EVENT:
                    Console.WriteLine("SIGNALLED: GPI_EVENT");
                    break;
                case Events.STATUS_EVENT_TYPE.READER_EXCEPTION_EVENT:
                    Console.WriteLine("SIGNALLED:READER_EXCEPTION_EVENT");
                    Console.WriteLine(e.StatusEventData.ReaderExceptionEventData.ReaderExceptionEventInfo);
                    break;
            }
        }


        static void Main(string[] args)
        {
            CS_RFIDSample4 rfidSample = new CS_RFIDSample4();

            // No arguments
            if (args.Length == 0)
            {
                rfidSample.hostName = "localhost";
                rfidSample.readerPort = 0;
            }
            // IP address or Host name given
            else if (args.Length == 1)
            {
                rfidSample.hostName = args[0].ToString();
            }
            // IP address and Port number
            else if (args.Length == 2)
            {
                rfidSample.hostName = args[0].ToString();
                rfidSample.readerPort = uint.Parse(args[1]);
            }
            // other Invalid parameter
            else
            {
                Console.WriteLine("Please enter valid arguments...");
                return;
            }

            // Connect to the reader
            rfidSample.ConnectToReader();

            // Shows the main menu 
            rfidSample.Createmenu();
        }
    }
}
