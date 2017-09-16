using System;
using System.Collections.Generic;
using System.Text;
using Symbol.RFID3;

namespace BasicRFID
{
    class Program
    {
        static public void DisplayTag(TagData tagData)
        {
            Console.WriteLine("{0} Tag id: {1} Antenna ID: {2}", DateTime.Now, tagData.TagID, tagData.AntennaID.ToString());

        }
        static public void DisplayEvent(Events.StatusEventArgs e)
        {
           Console.WriteLine("Event Signalled: " + e.StatusEventData.StatusEventType.ToString());
           if (e.StatusEventData.StatusEventType == Events.STATUS_EVENT_TYPE.DISCONNECTION_EVENT)
               Console.WriteLine("Please Exit and Restart the Application");
           if (e.StatusEventData.StatusEventType == Events.STATUS_EVENT_TYPE.READER_EXCEPTION_EVENT)
               Console.WriteLine(e.StatusEventData.ReaderExceptionEventData.ReaderExceptionEventInfo);
        }

        static void Main(string[] args)
        {
            string hostName;
         
            BasicRFID BasicRFIDReader = new BasicRFID();

            TagArrivedCallBack tagSignalledFnCB = new TagArrivedCallBack(DisplayTag);
            EventSignalledCallBack eventSignalledFnCB = new EventSignalledCallBack(DisplayEvent);

            // No arguments
            if (args.Length == 0)
            {
                hostName = "localhost";
            }
            // IP address or Host name given
            else if (args.Length == 1)
            {
                hostName = args[0].ToString();
            }
        
            // other Invalid parameter
            else
            {
                Console.WriteLine("Please enter valid arguments...");
                return;
            }

            BasicRFIDReader.ConnectReader(hostName, tagSignalledFnCB, eventSignalledFnCB);

            bool keepWorking = true;
            bool inventoryInProgress = false;
            while (keepWorking)
            {
                string TagID, mb, length, offset, privilege, pwd;
                RFIDResults result;
                Console.WriteLine("..................................................");
                Console.WriteLine("Welcome to RFID API3 .NET Basic Sample Application");
                Console.WriteLine("..................................................");
                Console.WriteLine("a. Start Inventory");
                Console.WriteLine("b. Stop Inventory");
                Console.WriteLine("c. Read Tag");
                Console.WriteLine("d. Write Tag");
                Console.WriteLine("e. Lock Tag");
                Console.WriteLine("f. Exit");
                string readKey = Console.ReadLine();
             
                try
                {
                    switch (readKey)
                    {
                        case "a":
                            BasicRFIDReader.StartInventory();
                            inventoryInProgress = true;
                            break;
                        case "b":
                            BasicRFIDReader.StopInventory();
                            inventoryInProgress = false;
                            break;
                        case "c":
                            if (inventoryInProgress)
                                BasicRFIDReader.StopInventory();
                            Console.WriteLine("Enter TagID to read from");
                            TagID = Console.ReadLine();
                            Console.WriteLine("Enter MemoryBank to read from [0-RSVD, 1-EPC, 2-TID, 3-User]");
                            mb = Console.ReadLine();
                            Console.WriteLine("Enter Offset to read from");
                            offset = Console.ReadLine();
                            Console.WriteLine("Enter length to read");
                            length = Console.ReadLine();
                            Console.WriteLine(BasicRFIDReader.ReadTag(TagID, (MEMORY_BANK)Int32.Parse(mb), Int32.Parse(length), Int32.Parse(offset)));
                            break;
                        case "d":
                            if (inventoryInProgress)
                            {
                                BasicRFIDReader.StopInventory();
                                inventoryInProgress = false;
                            }
                            Console.WriteLine("Enter TagID to write to");
                            TagID = Console.ReadLine();
                            Console.WriteLine("Enter MemoryBank to write to [0-RSVD, 1-EPC, 2-TID, 3-User]");
                            mb = Console.ReadLine();
                            Console.WriteLine("Enter Offset to write from");
                            offset = Console.ReadLine();
                            Console.WriteLine("Enter Memory Bank Data to write to");
                            string writeData = Console.ReadLine();
                            result = BasicRFIDReader.WriteTag(TagID, writeData, (MEMORY_BANK)Int32.Parse(mb), Int32.Parse(offset));
                            if (result == RFIDResults.RFID_API_SUCCESS)
                                Console.WriteLine("Write Successfully");
                            else
                                Console.WriteLine("Failed to Write: " + result);
                            break;
                        case "e":
                            if (inventoryInProgress)
                            {
                                BasicRFIDReader.StopInventory();
                                inventoryInProgress = false;
                            }
                            Console.WriteLine("Enter TagID to Lock");
                            TagID = Console.ReadLine();
                            Console.WriteLine("Enter MemoryBank to lock [0-RSVD, 1-EPC, 2-TID, 3-User]");
                            mb = Console.ReadLine();                            
                            Console.WriteLine("Enter Access Password:");
                            pwd = Console.ReadLine();
                            Console.WriteLine("Choose your privilege:");
                            Console.WriteLine("1. Read-Write Lock");//LOCK_PRIVILEGE_READ_WRITE
                            Console.WriteLine("2. Permanent Lock");//LOCK_PRIVILEGE_PERMA_LOCK
                            Console.WriteLine("3. Permanent Unlock");//LOCK_PRIVILEGE_PERMA_UNLOCK
                            Console.WriteLine("4. Unlock");//LOCK_PRIVILEGE_UNLOCK
                            privilege = Console.ReadLine();
                            result = BasicRFIDReader.LockTag(TagID, (MEMORY_BANK)Int32.Parse(mb), (LOCK_PRIVILEGE)Int32.Parse(privilege), uint.Parse(pwd));
                            if (result == RFIDResults.RFID_API_SUCCESS)
                                Console.WriteLine("Lock Successfully");
                            else
                                Console.WriteLine("Failed to Lock: " + result); break;
                        case "f":
                            if (BasicRFIDReader.m_IsConnected == true && inventoryInProgress)
                            {
                                BasicRFIDReader.StopInventory();
                                inventoryInProgress = false;
                            }
                            keepWorking = false;
                            break;
                    }
                }
                catch (OperationFailureException ex)
                {
                    Console.WriteLine("Error: " + ex.StatusDescription + " : " + ex.VendorMessage);
                }
            }
            BasicRFIDReader.DisconnectReader();
        }

    }
}
