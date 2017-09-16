using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Symbol.RFID2;


namespace CS_RFID2_Sample
{
    public partial class FrmGen2Write : Form
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

        Gen2ParmWrite wrtParms = new Gen2ParmWrite ();
        Gen2SelectRecordParameters sr = new Gen2SelectRecordParameters ();

        IRFIDReader Reader;
        Utilities Utils = new Utilities ();
        public delegate void DisplayAutoHandler (DisplayData args);
        public DisplayAutoHandler DisplayAutoH = null;
        private object GeneralLock = new object ();
        public ulong dispThreadExceptions;
        string epcId = null;
        public ulong gCnt = 0;
        public const int EPCBANK = 1;
        DisplayData data = new DisplayData ();
        Gen2ParmRead rdParms = new Gen2ParmRead ();




        public struct DisplayData  //display data delegate parameter
        {
            public IEnumerable <IRFIDTag> tags;
        };

        public FrmGen2Write (ref IRFIDReader Reader)
        {
            this.Reader = Reader;

            InitializeComponent ();
        }


        private void FrmGen2Write_Load (object sender, EventArgs e)
        {
            DisplayAutoH = new DisplayAutoHandler (DisplayTagList);
            listView2.FullRowSelect = true;
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

            cmbOptions.SelectedItem = 0;
            cmbOptions.SelectedIndex = 0;

            cmbLockOptions.SelectedItem = 0;
            cmbLockOptions.SelectedIndex = 0;

            cmbMemBank.SelectedItem = 1;
            cmbMemBank.SelectedIndex = 1;

            txtWrdPtr.Text = "2";
            txtWrdCnt.Text = "6";
            txtWrite.Text = "max 32 hex bytes";
            txtMask.Text = "max 32 hex bytes";
            epcId = System.String.Empty;
        }



        //########################## BUTTON OKAY ########################## 
        //########################## BUTTON OKAY ########################## 

        private void btn_OK_Click (object sender, EventArgs e)
        {

            this.Close ();

        }


        public byte[] Hexify (String hexstr)// half byte capable
        {
            int cnt;
            int i, x;
            byte[] bytes = null;

            if (hexstr != null) {
                cnt = (hexstr.Length / 2) * 2;

                x = hexstr.Length / 2 + hexstr.Length % 2;

                bytes = new byte[x];

                for (i = 0; i < cnt; i += 2) {
                    bytes[i / 2] = Convert.ToByte (hexstr.Substring (i, 2), 16);
                }

                if (hexstr.Length % 2 > 0) {
                    bytes[x - 1] = (byte)((Convert.ToInt16 (hexstr.Substring (i, 1), 16)) << 4);
                }
            }
            return (bytes);
        }


        private void btn_Quit_Click (object sender, EventArgs e)
        {
            this.Close ();
        }


        private void btn_Read_Click (object sender, EventArgs e)
        {
            bool status;
            IEnumerable <IRFIDTag> rtn;

            status = GetWriteParms ();

            SetReadParms (ref rdParms);

            try
            {
                rtn = Reader.ReadTagID(rdParms);
            }
            catch (Exception exp)
            {
                MessageBox.Show("Gen2 Read: " + exp.Message, "Gen2Read Excpt Event");
                return;

            }
            
            data.tags = rtn;

            DisplayTagList (data);

        }

        //##########################   ########################## 
        //##########################   ########################## 


        //public void DisplayTagList()
        public void DisplayTagList (DisplayData args)
        {
            IEnumerable <IRFIDTag> Tags = args.tags;
            string strTagID = null;



            try {
                lock (GeneralLock) {
 
                    foreach (IRFIDTag tag in Tags) {
                        if (tag.TagID == null) {
                            return;
                        }

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
                MessageBox.Show ("DisplayTag:" + ex.Message, "Display Excpt Event");
            }
        }



        private void listView2_SelectedIndexChanged (object sender, EventArgs e)
        {
            /*
            ListView view = new ListView ();
            view = (ListView)sender;
            
            if (view.FocusedItem != null) {
                epcId = new string (view.FocusedItem.SubItems[1].Text.ToCharArray ());

                //char[] c = { ':' };

                int i = epcId.IndexOf (',');

                epcId = epcId.Substring (i + 2);

                i = epcId.IndexOf (':');

                if (i > 0) {
                    txtMask.Text = epcId.Substring (0, i);
                    txtWrite.Text = epcId.Substring (0, i);
                } else {
                    txtMask.Text = epcId.ToString ();
                    txtWrite.Text = epcId.ToString ();
                }

            }
             * */



            ListView view = new ListView();
            view = (ListView)sender;

            if (view.FocusedItem != null)
            {
                epcId = new string(view.FocusedItem.SubItems[1].Text.ToCharArray());
                char[] c = { ':' };

                int i = epcId.IndexOf(':');

                if (i > 0)
                {
                    txtMask.Text = epcId.Substring(0, i);
                    txtWrite.Text = epcId.Substring(0, i);
                }
                else
                {
                    txtMask.Text = epcId.ToString();
                    txtWrite.Text = epcId.ToString();
                }
            }

        }


        private void btn_Write_Click (object sender, EventArgs e)
        {
            bool status;
            int rtn;
            int n = 0;
            IEnumerable <IRFIDTag> tags;

            status = GetWriteParms ();

            if (status == true) {
                
                rtn = Reader.SelectRecordAdd (sr);
               
                if (rtn >= 0 && rtn <= 4) {
                    try
                    {
                        tags = Reader.WriteTagID(wrtParms);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Gen2 Write: " + exp.Message, "Gen2Write Excpt Event");
                        return;

                    }
                    finally
                    {
                        rtn = Reader.SelectRecordRemove((byte)rtn);

                        if (rtn != -1 & rtn != -2)
                        {
                            //the removal was successful
                        }

                    }

                    foreach (IRFIDTag itag in tags) {
                        ++n;

                        String epc = Utils.ProcessRtnStatus (itag);

                        ListViewItem lvItem = new ListViewItem (new String[] { n.ToString (), epc.ToString () });

                        listView2.Items.Insert (0, lvItem);//.Selected = true;
                    }                    
                    
                } else if (rtn == -1) {
                    MessageBox.Show ("Sel Full--Remove One", "Sel Rec:");
                } else {
                    MessageBox.Show ("Unknown Error", "Sel Rec:");
                }
            }
        }


        private bool GetWriteParms ()
        {
            try {
                int i = cmbSel.SelectedIndex;

                switch (i) {
                    case 0: wrtParms.SelectFlag = Gen2.InventorySelectFlag.Ignore_SL; break;
                    case 1: wrtParms.SelectFlag = Gen2.InventorySelectFlag.SL_Not_Set; break;
                    case 2: wrtParms.SelectFlag = Gen2.InventorySelectFlag.SL_Set; break;
                }
            }
            catch {

                MessageBox.Show ("Invalid value for Sel flag");
                return (false);
            }


            try {
                int i = cmbSession.SelectedIndex;

                wrtParms.SessionFlag = (Gen2.SessionFlag)byte.Parse (i.ToString ());
            }
            catch {
                MessageBox.Show ("Invalid value for Session");
                return (false);
            }

            try {
                int i = cmbTarget.SelectedIndex;

                wrtParms.TargetFlag = (Gen2.TargetFlag)byte.Parse (i.ToString ());
            }
            catch {

                MessageBox.Show ("Invalid value for Target");
                return (false);
            }

            try {
                int i = cmbStartQ.SelectedIndex;

                wrtParms.StartingQ= byte.Parse (i.ToString ());
            }
            catch {
                MessageBox.Show ("Invalid value for Start Q.");
                return (false);
            }


            try {
                wrtParms.accPswd = Convert.ToUInt32 (txtAccPwd.Text.ToString (), 16);
            }
            catch {
                MessageBox.Show ("Invalid value for Access Password. Enter numeric value");
                return (false);
            }


            try {
                wrtParms.lockOpt = 0;// not supported, use Gen2LockTag
            }
            catch {
                MessageBox.Show ("Invalid value for Lock Option .Enter value between 0 to 3");
                return (false);
            }


            try {
                wrtParms.wrtOpts = 0;// not supported, use Gen2LockTag
            }
            catch {
                MessageBox.Show ("Invalid value for Write Options.  Enter value between 0 to 3");
                return (false);
            }


            try {
                int i = cmbMemBank.SelectedIndex;

                wrtParms.memBank = (Gen2.MemoryBank)byte.Parse (i.ToString ());
            }
            catch {
                MessageBox.Show ("Invalid value for memory bank .Enter value between 0 to 3");
                return (false);
            }


            try {
                wrtParms.wordPtr = ushort.Parse (txtWrdPtr.Text);
            }
            catch {
                MessageBox.Show ("Invalid value for word pointer.Enter numeric value");
                return (false);
            }



            try {
                wrtParms.wordCnt = byte.Parse (txtWrdCnt.Text);
            }
            catch {
                MessageBox.Show ("Invalid value for word count. Enter numeric value");
                return (false);
            }



            try {
                String s = txtMask.Text;

                if (wrtParms.SelectFlag == Gen2.InventorySelectFlag.Ignore_SL) {
                    sr.Target = (Gen2.SelectTarget)wrtParms.SessionFlag;

                    if (wrtParms.TargetFlag  == Gen2.TargetFlag.Bit_A) {
                        sr.Action = Gen2.SelectAction.SET_A_ELSE_SET_B;     //use do_nothing on no match to keep max filters 4
                    } else {
                        sr.Action = Gen2.SelectAction.SET_B_ELSE_SET_A;
                    }
                } else {
                    sr.Target = Gen2.SelectTarget.TARGET_FLAG_SL;

                    if (wrtParms.SelectFlag == Gen2.InventorySelectFlag.SL_Set) {
                        sr.Action = Gen2.SelectAction.SET_SL_ELSE_CLR_SL;
                    } else {
                        sr.Action = Gen2.SelectAction.CLR_SL_ELSE_SET_SL;
                    }
                }

                sr.BitPointer = 32;             // skip 32 bits of EPC block (CRC bits-1st 16 bits + 2nd 16 bits-PC bits)
                sr.BitLength = (byte)(s.Length * 4);  // need number of bits
                sr.Mask = Hexify (s);
                sr.MemoryBank = Gen2.MemoryBank.EPC;
                sr.Truncate = 0;
                String w = txtWrite.Text;

                wrtParms.newData = Hexify (w);

            }
            catch {
                MessageBox.Show ("Invalid value for New Write Data. Enter a valid byte array.");
                return (false);
            }


            return (true);
        }

        private void SetReadParms (ref Gen2ParmRead rdParm)
        {
            rdParm.SelectFlag = wrtParms.SelectFlag;
            rdParm.SessionFlag= wrtParms.SessionFlag;
            rdParm.TargetFlag  = wrtParms.TargetFlag ;
            rdParm.StartingQ= wrtParms.StartingQ;
            rdParm.accPswd = wrtParms.accPswd;
            rdParm.memBank = wrtParms.memBank;
            rdParm.wordPtr = wrtParms.wordPtr;
            rdParm.wordCnt = wrtParms.wordCnt;
        }

        private void btn_Clr_Click (object sender, EventArgs e)
        {
            txtMask.Text = "";
            txtWrite.Text = "";
            listView2.Items.Clear ();
        }

        public String Textify (byte[] bytes)
        {
            int i, len;
            String str = new String (' ', 0);

            if (bytes == null) {
                bytes = new byte[1];
            }

            len = bytes.Length;

            for (i = 0; i < len; i++) {
                str += bytes[i].ToString ("X2");
            }

            return (str);
        }



        //private void ProcessRtnStatus(IEnumerable <IRFIDTag> rtn)
        //{
        //  int n = 0;
        //  StringBuilder arg = new StringBuilder();

        //  try
        //  {
        //    if (rtn == null || rtn.Length <= 0)
        //    {
        //      return;
        //    }

        //    foreach (IRFIDTag t in rtn)
        //    {
        //      string epc = string.Empty;
        //      string s = string.Empty;

        //      switch (t.AccessStatus)
        //      {
        //        case RFID_SUCCESS:  
        //                            epc  = "OK, ";

        //                            epc += arg.Append(Textify(t.TagID)).ToString();    //contains the tag ID used in the command

        //                            break;

        //        case RFID_TAGLOST:  
        //                            epc = "TAG LOST, ";

        //                            epc += arg.Append(Textify(t.TagID));    //contains the tag ID used in the command

        //                            if (t.TagData != null && t.TagData.Length > 0)
        //                            {
        //                              epc += ":" + Textify(t.TagData);
        //                            }

        //                            break;

        //        case RFID_RESP_CRC: 
        //                            epc = "RESP CRC ERR, ";  

        //                            epc += arg.Append(Textify(t.TagID));

        //                            if (t.TagData != null && t.TagData.Length > 0)
        //                            {
        //                              epc += ":" + Textify(t.TagData);
        //                            }

        //                            break;                                   //contains the tag ID used in the command

        //        case RFID_TAG_ERR: 
        //                            epc = "Tag Error, ";

        //                            if (t.TagData != null)
        //                            {
        //                              epc += ReturnCodeStr(t);
        //                            }  

        //                            break;

        //        default:  epc = "Unknown, no data available";break;
        //      }

        //      ++n;

        //      ListViewItem lvItem = new ListViewItem(new String[]{ n.ToString(), epc.ToString()});

        //      listView2.Items.Insert(0, lvItem);//.Selected = true;

        //    }//foreach
        //  }//try
        //  catch
        //  {
        //  }
        //}//meth



        //private String ProcessRtnStatus(IRFIDTag t)
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
        //    string s = string.Empty;

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
        //                          epc = "Tag Error, ";

        //                          if (t.TagData != null)
        //                          {
        //                            epc += ReturnCodeStr(t);
        //                          } 

        //                          break;

        //      default: epc = "Unknown, no data available"; break;
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

        //  if (t.TagData.Length == 1)
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
        //    foreach (char c in t.TagID)
        //    {
        //      arg.Append(System.Convert.ToChar(c));
        //    }
        //  }

        //  return (arg);

        //}


    }
}