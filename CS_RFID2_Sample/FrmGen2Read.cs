using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;
using Symbol.RFID2;


namespace CS_RFID2_Sample
{


    public partial class FrmGen2Read : Form
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

        public const int TAGPKTSMAX = 100;
        //public const int TAGINVMAX = 100;
        public const int AUTOREAD = 1;
        public const int STOPREAD = 0;
        public ulong gCnt = 0;
        public ulong readThreadExceptions;
        public ulong dispThreadExceptions;
        Gen2ParmRead rdParms = new Gen2ParmRead ();

        IRFIDReader Reader;
        Utilities Utils = new Utilities ();
        public struct DisplayData  //display data delegate parameter
        {
            public IEnumerable<IRFIDTag> tags;
        }

        public class TagInventory
        {
            public ulong readCnt;
            public IRFIDTag tag = null;
        }


        TagInventory[] tList = new TagInventory[TAGPKTSMAX];


        private Thread readThread = null;
        private object GeneralLock = new object ();

        public bool IsReading = false;
        public delegate void DisplayAutoHandler (DisplayData args);
        public delegate void ChangeLabel (int x);

        public DisplayAutoHandler DisplayAutoH = null;
        public ChangeLabel SetLabelH = null;
        public const int EPCBANK = 1;


        public FrmGen2Read (ref IRFIDReader Reader)
        {
            this.Reader = Reader;

            InitializeComponent ();
        }


        private void FrmGen2Read_Load (object sender, EventArgs e)
        {
            DisplayAutoH = new DisplayAutoHandler (DisplayAutoTagList);
            SetLabelH = new ChangeLabel (SetLabel);
            //cmbAntenna.SelectedIndex = 0;

            cmbSel.SelectedItem = 0;
            cmbSel.SelectedIndex = 0;

            cmbSession.SelectedItem = 0;
            cmbSession.SelectedIndex = 0;

            cmbTarget.SelectedItem = 0;
            cmbTarget.SelectedIndex = 0;

            cmbTarget.SelectedItem = 0;
            cmbTarget.SelectedIndex = 0;

            cmbStartQ.SelectedItem = 2;
            cmbStartQ.SelectedIndex = 2;

            txtAccPwd.Text = "00000000";
            cmbMemBank.SelectedIndex = 1;
            txtWrdPtr.Text = "2";
            txtWrdCnt.Text = "6";
            //txtRdData.Text = "IN COMING DATA";
        }


        //########################## BUTTON OKAY ########################## 
        //########################## BUTTON OKAY ########################## 


        public byte[] Hexify (String hexstr)
        {
            int cnt = hexstr.Length;

            byte[] bytes = new byte[cnt / 2];

            for (int i = 0; i < cnt; i += 2) {
                bytes[i / 2] = Convert.ToByte (hexstr.Substring (i, 2), 16);
            }

            return (bytes);
        }


        //########################## BUTTON CANCEL ########################## 
        //########################## BUTTON CANCEL ########################## 

        private void btn_Cancel_Click (object sender, EventArgs e)
        {
            this.Close ();
        }



        /// <summary>
        /// DisplayAutoTags processes and displays tag data received from deviceReaderTagEvent handler
        /// </summary>




        private bool GetReadParms ()
        {
            try {
                int i = cmbSel.SelectedIndex;

                switch (i) {
                    case 0: rdParms.SelectFlag = Gen2.InventorySelectFlag.Ignore_SL; break;
                    case 1: rdParms.SelectFlag = Gen2.InventorySelectFlag.SL_Not_Set; break;
                    case 2: rdParms.SelectFlag = Gen2.InventorySelectFlag.SL_Set; break;
                }
            }
            catch {
                MessageBox.Show ("Invalid value for SelectFlag flag");
                return (false);
            }


            try {
                int i = cmbSession.SelectedIndex;

                rdParms.SessionFlag = (Gen2.SessionFlag)byte.Parse (i.ToString ());
            }
            catch {

                MessageBox.Show ("Invalid value for Session");
                return (false);
            }

            try {
                int i = cmbTarget.SelectedIndex;

                rdParms.TargetFlag = (Gen2.TargetFlag)byte.Parse (i.ToString ());
            }
            catch {
                MessageBox.Show ("Invalid value for Target");
                return (false);
            }

            try {
                int i = cmbStartQ.SelectedIndex;

                rdParms.StartingQ = byte.Parse (i.ToString ());
            }
            catch {
                MessageBox.Show ("Invalid value for Start Q.");
                return (false);
            }


            try {
                rdParms.accPswd = Convert.ToUInt32 (txtAccPwd.Text.ToString (), 16); //uint.Parse(txtAccPwd.Text.ToString(), System.Globalization.NumberStyles.HexNumber);
            }
            catch {
                MessageBox.Show ("Invalid value for Access Password. Enter numeric value");
                return (false);
            }


            try {
                int i = cmbMemBank.SelectedIndex;

                rdParms.memBank = (Gen2.MemoryBank)byte.Parse (i.ToString ());
            }
            catch {
                MessageBox.Show ("Invalid value for memory bank .Enter value between 0 to 3");
                return (false);
            }


            try {
                rdParms.wordPtr = ushort.Parse (txtWrdPtr.Text);
            }
            catch {

                MessageBox.Show ("Invalid value for word pointer.Enter numeric value");
                return (false);
            }



            try {
                rdParms.wordCnt = byte.Parse (txtWrdCnt.Text);
            }
            catch {
                MessageBox.Show ("Invalid value for word count. Enter numeric value");
                return (false);
            }
            return (true);
        }


        public void DisplayTagList (DisplayData args)
        {
            IEnumerable<IRFIDTag> Tags = args.tags;
            string strTagID = null;

            try {
                lock (GeneralLock) {
                    foreach (IRFIDTag tag in Tags) {

                        strTagID = Utils.ProcessRtnStatus (tag);

                        ++gCnt;

                        ListViewItem lvItem = new ListViewItem (new string[] { gCnt.ToString (), strTagID });

                        listView2.Items.Insert (0, lvItem);//.Selected = true;


                        if (listView2.Items.Count > 175) {
                            listView2.Items.Clear ();
                        }
                    }//for
                }//lock
            }//try
            catch (Exception ex) {
                MessageBox.Show ("DisplayTag:" + ex.Message, "Display Except Event");
            }
        }



        private void StartReadThread ()
        {
            GetReadParms ();

            IsReading = true;

            readThread = new Thread(new ThreadStart(ThreadStart));

            readThread.Start ();

            btn_Auto.BeginInvoke (SetLabelH, AUTOREAD);
        }




        public void StopReadThread ()
        {
            lock (GeneralLock) {
                IsReading = false;

                readThread.Join (100);

                readThread = null;

                btn_Auto.BeginInvoke (SetLabelH, STOPREAD);
            }
        }


        private void btn_Clear_Click (object sender, EventArgs e)
        {
            listView2.Items.Clear ();
            gCnt = 0;
        }

        private void btn_Auto_Click (object sender, EventArgs e)
        {
            if (IsReading) {
                StopReadThread ();

                IsReading = false;
            } else {
                StartReadThread ();

                IsReading = true;
            }
        }

        private void SetLabel (int x)
        {
            if (x == AUTOREAD) {
                btn_Auto.Text = "STOP";
            } else {
                btn_Auto.Text = "AUTO";
            }
        }

        //public String ProcessRtnStatus(IRFIDTag t)
        //{
        //  int n = 0;
        //  String nll = "Empty";
        //  StringBuilder arg = new StringBuilder();

        //  try
        //  {
        //    if (t == null)
        //    {
        //      return (nll);
        //    }

        //    string epc = string.Empty;
        //    //string s = string.Empty;

        //    t.AccessStatus.ToString();

        //    switch (t.AccessStatus)
        //    {
        //      case RFID_SUCCESS:
        //                          epc = arg.Append(Textify(t.TagID)).ToString();    //contains the tag ID used in the command

        //                          if (t.TagData != null && t.TagData.Length > 0)
        //                          {
        //                            epc += ":" + Textify(t.TagData);
        //                          }

        //                          break;

        //      case RFID_TAGLOST:
        //                          epc = "TAG LOST, ";

        //                          epc += arg.Append(Textify(t.TagID));    //contains the tag ID used in the command

        //                          if (t.TagData != null && t.TagData.Length > 0)
        //                          {
        //                            epc += ":" + Textify(t.TagData);
        //                          }

        //                          break;

        //      case RFID_RESP_CRC:
        //                          epc = "RESP CRC ERR, ";

        //                          epc += arg.Append(Textify(t.TagID));    //contains the tag ID used in the command

        //                          if (t.TagData != null && t.TagData.Length > 0)
        //                          {
        //                            epc += ":" + Textify(t.TagData);
        //                          }

        //                          break;

        //      case RFID_TAG_ERR:
        //                          epc = "Tag Error, " +  ReturnCodeStr(t);

        //                          break;

        //                        default: epc = "Unknown, no data available"; break;
        //    }

        //    ++n;

        //    String msg = n.ToString() + ", " + epc.ToString();

        //    return (msg);
        //  }//try
        //  catch
        //  {
        //    return (nll);
        //  }
        //}//meth

        //public StringBuilder ReturnCodeStr(IRFIDTag t)
        //{
        //  StringBuilder arg = new StringBuilder();

        //  if (t.TagData != null && t.TagData.Length == 1)
        //  {
        //    switch ((ulong)t.TagData[0])
        //    {
        //      case RFID_OTHER_ERR:
        //        arg = arg.Append("Other_error, ");
        //        break;

        //      case RFID_MEM_OVERRUN:
        //        arg = arg.Append("Memory_Overrun, ");
        //        break;

        //      case RFID_MEM_LOCKED:
        //        arg = arg.Append("Memory_Locked, ");
        //        break;
        //      case RFID_POWER_LOW:
        //        arg = arg.Append("Power Too Low, ");
        //        break;

        //      case RFID_GENERAL_ERR:
        //        arg = arg.Append("General Error, ");
        //        break;

        //      default:
        //        arg = arg.Append("Unkown Error, ");
        //        break;
        //    }

        //    if (t.TagID != null)
        //    {
        //      arg.Append(Textify(t.TagID));    //contains the tag ID used in the command
        //    }
        //  }
        //  else
        //  {
        //    if (t.TagID != null)
        //    {
        //      foreach (char c in t.TagID)
        //      {
        //        arg.Append(System.Convert.ToChar(c));
        //      }
        //    }
        //  }

        //  return (arg);

        //}

        //public String Textify(byte[] bytes)
        //{
        //  int i, len;
        //  String str = new String(' ', 0);

        //  if (bytes == null)
        //  {
        //    bytes = new byte[1];
        //  }

        //  len = bytes.Length;

        //  for (i = 0; i < len; i++)
        //  {
        //    str += bytes[i].ToString("X2");
        //  }

        //  return (str);
        //}


        private void btn_Read_Click (object sender, EventArgs e)
        {
            bool status;

            DisplayData data = new DisplayData ();

            status = GetReadParms ();

            if (status == true) {

                try
                {
                    data.tags = Reader.ReadTagID(rdParms);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Gen2 Read: " + exp.Message, "Gen2Read Excpt Event");
                    return;

                }

                DisplayTagList (data);
            }

        }

        private void ThreadStart ()   // check performance using one data object
        {
            DisplayData[] data = new DisplayData[TAGPKTSMAX];
            int j = 0;

            try {
                while (IsReading) {
                    data[j].tags = Reader.ReadTagID (rdParms);

                    if (data[j].tags != null) {
                        listView2.BeginInvoke (DisplayAutoH, data[j]);   //asynchronous queuing it whl

                        Thread.Sleep (0);                              // pause to let obj get copied into new thread
                    }

                    if (++j == TAGPKTSMAX) {
                        j = 0;
                    }

                }//whl

            }//try

            catch (Exception ex) {
                MessageBox.Show ("ReadThread:" + ex.Message, "Display Excpt Event");
            }
        }



        public void DisplayAutoTagList (DisplayData args)
        {
            int  j,  ln;
            IEnumerable<IRFIDTag> Tags = args.tags;
            string epc = null;

            try {


                foreach (IRFIDTag tag in Tags) {
                    if (tag.AccessStatus != RFID_SUCCESS || tag.TagID == null) {
                        continue;
                    }

                    epc = null;

                    ln = tag.TagID.Length;

                    for (j = 0; j < ln; j++) {
                        epc += tag.TagID[j].ToString ("X2");
                    }

                    ++gCnt;

                    ListViewItem lvItem = new ListViewItem (new string[] { gCnt.ToString (), epc });

                    listView2.Items.Insert (0, lvItem);//.Selected = true;


                    if (listView2.Items.Count > 175) {
                        listView2.Items.Clear ();
                    }
                }//for
                //}//lock
            }//try
            catch (Exception ex) {
                MessageBox.Show ("DisplayTag:" + ex.Message, "Display Excpt Event");
            }
        }

    }
}