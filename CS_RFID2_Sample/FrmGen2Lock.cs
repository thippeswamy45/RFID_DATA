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
    public partial class FrmGen2Lock : Form
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

        Gen2ParmLock LockParms = new Gen2ParmLock ();
        Gen2ParmRead rdParms = new Gen2ParmRead ();
        Gen2SelectRecordParameters sr = new Gen2SelectRecordParameters ();
        private object GeneralLock = new object ();
        IRFIDReader Reader;
        string epcId = null;
        public ulong gCnt = 0;
        Utilities Utils = new Utilities ();

        public struct DisplayData  //display data delegate parameter
        {
            public IEnumerable <IRFIDTag> tags;
        };


        public FrmGen2Lock (ref IRFIDReader Reader)
        {

            LockParms = new Gen2ParmLock (Gen2.InventorySelectFlag.Ignore_SL,
                                           Gen2.SessionFlag.S0,
                                           Gen2.TargetFlag.Bit_A,
                                           4,
                                           0,
                                           (ushort)Gen2ParmLockMask.EpcPwdRW,
                                           (ushort)Gen2ParmLockAction.EpcPwdRW
                                          );//fini

            this.Reader = Reader;

            InitializeComponent ();
        }



        private void FrmGen2Lock_Load (object sender, EventArgs e)
        {
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

            txtAccPwd.Text = "0x0000000";
            cmbRegion.SelectedIndex = 2;
            cmbLockAction.SelectedIndex = 0;
        }


        private void btn_Clr_Click (object sender, EventArgs e)
        {
            txtEpcid.Text = "";
            txtAccPwd.Text = "0";
            listView2.Items.Clear ();
        }

        private void btn_Quit_Click (object sender, EventArgs e)
        {
            this.Close ();
        }



        //########################## BUTTON LOCK ########################## 
        //########################## BUTTON LOCK ########################## 


        bool GetLockParms ()
        {

            try {
                int i = cmbSel.SelectedIndex;

                switch (i) {
                    case 0: LockParms.SelectFlag = Gen2.InventorySelectFlag.Ignore_SL; break;
                    case 1: LockParms.SelectFlag = Gen2.InventorySelectFlag.SL_Not_Set; break;
                    case 2: LockParms.SelectFlag = Gen2.InventorySelectFlag.SL_Set; break;
                }
            }
            catch {
                MessageBox.Show ("Invalid value for Sel flag");
                return (false);
            }


            try {
                int i = cmbSession.SelectedIndex;

                LockParms.SessionFlag = (Gen2.SessionFlag)byte.Parse (i.ToString ());
            }
            catch {
                MessageBox.Show ("Invalid value for Session");
                return (false);
            }

            try {
                int i = cmbTarget.SelectedIndex;

                LockParms.TargetFlag = (Gen2.TargetFlag)byte.Parse (i.ToString ());
            }
            catch {
                MessageBox.Show ("Invalid value for Target");
                return (false);
            }


            try {
                int i = cmbStartQ.SelectedIndex;

                LockParms.StartingQ = byte.Parse (i.ToString ());
            }
            catch {

                MessageBox.Show ("Invalid value for Start Q.");
                return (false);
            }


            try {
                LockParms.accPswd = Convert.ToUInt32 (txtAccPwd.Text.ToString (), 16);
            }
            catch {
                MessageBox.Show ("Invalid value for Access Password. Enter numeric value");
                return (false);
            }

            try {
                const int KILL = 0;
                const int ACCS = 1;
                const int EPC = 2;
                const int TID = 3;
                const int USER = 4;
                int i = cmbRegion.SelectedIndex;

                switch (i) {
                    case KILL: {
                            switch (cmbLockAction.SelectedIndex) {
                                case 0:
                                    LockParms.action = 0;
                                    break;
                                case 1:
                                    LockParms.action = (ushort)Gen2ParmLockAction.KilPrmLk;
                                    break;
                                case 2:
                                    LockParms.action = (ushort)Gen2ParmLockAction.KilPwdRW;
                                    break;

                                case 3:
                                    LockParms.action = (ushort)Gen2ParmLockAction.KilPwdRW + (ushort)Gen2ParmLockAction.KilPrmLk;
                                    break;
                            }

                            LockParms.mask = (ushort)Gen2ParmLockMask.KilPwdRW + (ushort)Gen2ParmLockMask.KilPrmLk;
                        }
                        break;

                    case ACCS: {
                            switch (cmbLockAction.SelectedIndex) {
                                case 0:
                                    LockParms.action = 0;
                                    break;
                                case 1:
                                    LockParms.action = (ushort)Gen2ParmLockAction.AccPrmLk;
                                    break;
                                case 2:
                                    LockParms.action = (ushort)Gen2ParmLockAction.AccPwdRW;
                                    break;

                                case 3:
                                    LockParms.action = (ushort)Gen2ParmLockAction.AccPwdRW + (ushort)Gen2ParmLockAction.AccPrmLk;
                                    break;
                            }

                            LockParms.mask = (ushort)Gen2ParmLockMask.AccPwdRW + (ushort)Gen2ParmLockMask.AccPrmLk;
                        }
                        break;


                    case EPC: {
                            switch (cmbLockAction.SelectedIndex) {
                                case 0:
                                    LockParms.action = 0;
                                    break;
                                case 1:
                                    LockParms.action = (ushort)Gen2ParmLockAction.EpcPrmLk;
                                    break;
                                case 2:
                                    LockParms.action = (ushort)Gen2ParmLockAction.EpcPwdRW;
                                    break;

                                case 3:
                                    LockParms.action = (ushort)Gen2ParmLockAction.EpcPwdRW + (ushort)Gen2ParmLockAction.EpcPrmLk;
                                    break;
                            }

                            LockParms.mask = (ushort)Gen2ParmLockMask.EpcPwdRW + (ushort)Gen2ParmLockMask.EpcPrmLk;
                        }
                        break;
                    case TID: {
                            switch (cmbLockAction.SelectedIndex) {
                                case 0:
                                    LockParms.action = 0;
                                    break;
                                case 1:
                                    LockParms.action = (ushort)Gen2ParmLockAction.TidPrmLk;
                                    break;
                                case 2:
                                    LockParms.action = (ushort)Gen2ParmLockAction.TidPwdRW;
                                    break;

                                case 3:
                                    LockParms.action = (ushort)Gen2ParmLockAction.TidPwdRW + (ushort)Gen2ParmLockAction.TidPrmLk;
                                    break;
                            }

                            LockParms.mask = (ushort)Gen2ParmLockMask.TidPwdRW + (ushort)Gen2ParmLockMask.TidPrmLk;
                        }
                        break;
                    case USER: {
                            switch (cmbLockAction.SelectedIndex) {
                                case 0:
                                    LockParms.action = 0;
                                    break;
                                case 1:
                                    LockParms.action = (ushort)Gen2ParmLockAction.UsrPrmLck;
                                    break;
                                case 2:
                                    LockParms.action = (ushort)Gen2ParmLockAction.UsrPwdRW;
                                    break;

                                case 3:
                                    LockParms.action = (ushort)Gen2ParmLockAction.UsrPwdRW + (ushort)Gen2ParmLockAction.UsrPrmLck;
                                    break;
                            }

                            LockParms.mask = (ushort)Gen2ParmLockMask.UsrPwdRW + (ushort)Gen2ParmLockMask.UsrPrmLck;
                        }
                        break;
                }//swtch
            }
            catch {
                MessageBox.Show ("Action/Mask Attempt Failed.");
                return (false);
            }

            try {
                string s = txtEpcid.Text;

                if (LockParms.SelectFlag == Gen2.InventorySelectFlag.Ignore_SL) {
                    sr.Target = (Gen2.SelectTarget)LockParms.SessionFlag;

                    if (LockParms.TargetFlag == Gen2.TargetFlag.Bit_A) {
                        sr.Action = Gen2.SelectAction.SET_A_ELSE_SET_B;     //use do_nothing on no match to keep max filters 4
                    } else {
                        sr.Action = Gen2.SelectAction.SET_B_ELSE_SET_A;
                    }
                } else {
                    sr.Target = Gen2.SelectTarget.TARGET_FLAG_SL;

                    if (LockParms.SelectFlag == Gen2.InventorySelectFlag.SL_Set) {
                        sr.Action = Gen2.SelectAction.SET_SL_ELSE_CLR_SL;
                    } else {
                        sr.Action = Gen2.SelectAction.CLR_SL_ELSE_SET_SL;
                    }
                }

                sr.BitPointer = 32;
                sr.BitLength = (byte)(s.Length * 4);
                sr.Mask = Utils.Hexify (s);
                sr.MemoryBank = Gen2.MemoryBank.EPC;
                sr.Truncate = 0;

            }
            catch {
                MessageBox.Show ("Invalid value for EPC ID data : Enter valid data");
                return (false);
            }

            return (true);
        }

        //########################## BUTTON CANCEL ########################## 
        //########################## BUTTON CANCEL ########################## 

        //private void btn_Cancel_Click(object sender, EventArgs e)
        //{
        //  this.Close();
        //}

        private void txtEpcid_GotFocus (object sender, EventArgs e)
        {
            txtEpcid.Text = "";
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



        private void txtAccPwd_GotFocus (object sender, EventArgs e)
        {
            txtAccPwd.Text = "";
        }




        private void btn_LOCK_Click (object sender, EventArgs e)
        {
            IEnumerable <IRFIDTag> tags;
            int rtn;
            bool status;
            int n = 0;

            status = GetLockParms ();

            if (status == true) {
                rtn = Reader.SelectRecordAdd (sr);

                if (rtn >= 0 && rtn <= 4) {

                    try
                    {
                        tags = Reader.LockTagID(LockParms);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Gen2 Lock: " + exp.Message, "Gen2Lock Excpt Event");
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
                    //tags = Reader.LockTagID (LockParms);

                    foreach (IRFIDTag itag in tags) {
                        ++n;

                        String msg = Utils.ProcessRtnStatus (itag);

                        MessageBox.Show (n.ToString () + " - " + msg.ToString (), "Lock Results...");
                    }
                   
                } else if (rtn == -1) {
                    MessageBox.Show ("Sel Full--Remove One", "Sel Rec:");
                } else {
                    MessageBox.Show ("Unknown Error", "Sel Rec:");
                }

            }
        }

        private void btn_Read_Click (object sender, EventArgs e)
        {
            DisplayData data = new DisplayData ();

            rdParms.SelectFlag = 0;
            rdParms.SessionFlag= 0;
            rdParms.TargetFlag  = 0;
            rdParms.StartingQ= 4;
            rdParms.accPswd = 0;
            rdParms.memBank = Gen2.MemoryBank.RESERVED;
            rdParms.wordPtr = 0;
            rdParms.wordCnt = 4;

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


        private void btn_Clear_Click (object sender, EventArgs e)
        {
            listView2.Items.Clear ();
            gCnt = 0;
        }


        public void DisplayTagList (DisplayData args)
        {
            int j, ln;
            IEnumerable <IRFIDTag> Tags = args.tags;
            byte[] epc = null;
            byte[] bnk = null;
            string cntStr = null;
            string strTagID = null;


            try {
                lock (GeneralLock) {

                    int count = 0;
                    foreach (IRFIDTag tag in Tags) {
                        count++;

                        epc = null;

                        if (tag.TagID == null) {
                            return;
                        }

                        if (tag.AccessStatus == RFID_SUCCESS) {
                            epc = tag.TagID;

                            ++gCnt;

                            cntStr = gCnt.ToString ();

                            strTagID = string.Empty;

                            ln = epc.Length;


                            for (j = 0; j < ln; j++) {
                                strTagID += epc[j].ToString ("X2");
                            }

                            if (tag.TagData != null) {
                                if (tag.TagData.Length > 0) {
                                    bnk = tag.TagData;    // memory bank data...if any

                                    ln = bnk.Length;

                                    strTagID += " : ";

                                    for (j = 0; j < ln; j++) {
                                        strTagID += bnk[j].ToString ("X2");
                                    }
                                }
                            }
                        } else {
                            strTagID = Utils.ProcessRtnStatus (tag);
                        }


                        ListViewItem lvItem = new ListViewItem (new string[] { count.ToString (), strTagID });

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
            ListView view = new ListView ();
            view = (ListView)sender;

            if (view.FocusedItem != null) {
                epcId = new string (view.FocusedItem.SubItems[1].Text.ToCharArray ());
                char[] c = { ':' };

                int i = epcId.IndexOf (':');

                if (i > 0) {
                    txtEpcid.Text = epcId.Substring (0, i - 1);
                } else {
                    txtEpcid.Text = epcId.ToString ();
                }
            }

        }


        //private String ProcessRtnStatus(IRFIDTag t)
        //{
        //  int n = 0;
        //  String nll = "Empty";
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
        //      case RFID_SUCCESS: epc = "OK, "; break;

        //      case RFID_TAGLOST: epc = "TAG LOST, "; break;

        //      case RFID_RESP_CRC: epc = "RESP CRC ERR, "; break;

        //      case RFID_TAG_ERR: epc = "Tag Error, Code: ";

        //        if (t.TagData != null)
        //        {
        //          epc += ReturnCodeStr(t);
        //        } break;

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
        //    foreach (char c in t.TagID)       // translate error string to ascii
        //    {
        //      arg.Append(System.Convert.ToChar(c));
        //    }
        //  }

        //  return (arg);

        //}


        //private void ProcessRtnStatus(IEnumerable <IRFIDTag> rtn)
        //{
        //  int n = 0;

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

        //      t.AccessStatus.ToString();

        //      switch (t.AccessStatus)
        //      {
        //        case RFID_SUCCESS: epc = "OK, "; break;

        //        case RFID_TAGLOST: epc = "TAG LOST, "; break;

        //        case RFID_RESP_CRC: epc = "RESP CRC ERR, "; break;

        //        case RFID_TAG_ERR: epc = "Tag Error, Code: ";

        //          if (t.TagData != null)
        //          {
        //            epc += ReturnCodeStr(t);
        //          } break;
        //        default: epc = "Unknown, no data available"; break;
        //      }

        //      ++n;

        //      MessageBox.Show(n.ToString() + " - "+ epc.ToString(), "Lock Results...");

        //    }//foreach
        //  }//try
        //  catch 
        //  {
        //  }
        //}//meth

        //public byte[] Hexify(String hexstr)// half byte capable
        //{
        //  int cnt;
        //  int i, x;
        //  byte[] bytes = null;

        //  if (hexstr != null)
        //  {
        //    cnt = (hexstr.Length / 2) * 2;

        //    x = hexstr.Length / 2 + hexstr.Length % 2;

        //    bytes = new byte[x];

        //    for (i = 0; i < cnt; i += 2)
        //    {
        //      bytes[i / 2] = Convert.ToByte(hexstr.Substring(i, 2), 16);
        //    }

        //    if (hexstr.Length % 2 > 0)
        //    {
        //      bytes[x - 1] = (byte)((Convert.ToInt16(hexstr.Substring(i, 1), 16)) << 4);
        //    }
        //  }
        //  return (bytes);
        //}

    }//cls
}
