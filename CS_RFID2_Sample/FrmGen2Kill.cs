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
    public partial class FrmGen2Kill : Form
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


        Gen2ParmKill KillParm = new Gen2ParmKill ();
        IRFIDReader Reader;
        Utilities Utils = new Utilities ();
        Gen2SelectRecordParameters sr = new Gen2SelectRecordParameters ();

        public FrmGen2Kill (ref IRFIDReader Reader)
        {
            KillParm = new Gen2ParmKill (Gen2.InventorySelectFlag.Ignore_SL,
                                           Gen2.SessionFlag.S0,
                                           Gen2.TargetFlag.Bit_A,
                                           4,
                                           0
                                        );//fini

            this.Reader = Reader;

            InitializeComponent ();
        }


        private void FrmGen2Kill_Load (object sender, EventArgs e)
        {
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

            txtKillPwd.Text = "0000 0000";
            txtEpcid.Text = "Needed for Filtered Lock";
        }



        //########################## BUTTON OKAY ########################## 
        //########################## BUTTON OKAY ########################## 

        private void txtKillPwd_GotFocus (object sender, EventArgs e)
        {
            txtKillPwd.Text = "";
        }

        private void btn_Cancel_Click (object sender, EventArgs e)
        {
            this.Close ();
        }


        bool GetKillParms ()
        {
            try {
                int i = cmbSel.SelectedIndex;

                switch (i) {
                    case 0: KillParm.SelectFlag = Gen2.InventorySelectFlag.Ignore_SL; break;
                    case 1: KillParm.SelectFlag = Gen2.InventorySelectFlag.SL_Not_Set; break;
                    case 2: KillParm.SelectFlag = Gen2.InventorySelectFlag.SL_Set; break;
                }

            }
            catch {
                MessageBox.Show ("Invalid value for Sel flag");
                return (false);
            }


            try {
                int i = cmbSession.SelectedIndex;

                KillParm.SessionFlag= (Gen2.SessionFlag)byte.Parse (i.ToString ());
            }
            catch {
                MessageBox.Show ("Invalid value for Session");
                return (false);
            }

            try {
                int i = cmbTarget.SelectedIndex;

                KillParm.TargetFlag  = (Gen2.TargetFlag)byte.Parse (i.ToString ());
            }
            catch {
                MessageBox.Show ("Invalid value for Target");
                return (false);
            }


            try {
                int i = cmbStartQ.SelectedIndex;

                KillParm.StartingQ= byte.Parse (i.ToString ());
            }
            catch {
                MessageBox.Show ("Invalid value for Start Q.");
                return (false);
            }


            try {
                KillParm.KillPassword = Convert.ToUInt32 (txtKillPwd.Text.ToString (), 16);
            }
            catch {
                MessageBox.Show ("Invalid value for Kill Password. Enter numeric value");
                return (false);
            }



            try {
                string s = txtEpcid.Text;

                if (KillParm.SelectFlag == Gen2.InventorySelectFlag.Ignore_SL) {
                    sr.Target = (Gen2.SelectTarget)KillParm.SessionFlag;

                    if (KillParm.TargetFlag  == Gen2.TargetFlag.Bit_A) {
                        sr.Action = Gen2.SelectAction.SET_A_ELSE_SET_B;     //use do_nothing on no match to keep max filters 4
                    } else {
                        sr.Action = Gen2.SelectAction.SET_B_ELSE_SET_A;
                    }
                } else {
                    sr.Target = Gen2.SelectTarget.TARGET_FLAG_SL;

                    if (KillParm.SelectFlag == Gen2.InventorySelectFlag.SL_Set) {
                        sr.Action = Gen2.SelectAction.SET_SL_ELSE_CLR_SL;
                    } else {
                        sr.Action = Gen2.SelectAction.CLR_SL_ELSE_SET_SL;
                    }
                }


                sr.Action = Gen2.SelectAction.SET_A_ELSE_SET_B;
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






        private void btn_KILL_Click (object sender, EventArgs e)
        {
            IEnumerable <IRFIDTag> tags;
            bool status;
            int rtn;
            int n = 0;

            status = GetKillParms ();

            if (status == true) {

                rtn = Reader.SelectRecordAdd (sr);

                if (rtn >= 0 && rtn <= 4) {

                    try
                    {
                        tags = Reader.KillTagID(KillParm);   //attempt to kill tag
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Gen2 Kill: " + exp.Message, "Gen2Kill Excpt Event");
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

                        String msg = Utils.ProcessRtnStatus (itag);

                        MessageBox.Show (n.ToString () + " - " + msg.ToString (), "Kill Results...");
                    }

                    
                } else if (rtn == -1) {
                    MessageBox.Show ("Sel Full--Remove One", "Sel Rec:");
                } else {
                    MessageBox.Show ("Unknown Error", "Sel Rec:");
                }

            }

        }


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
        //        case RFID_SUCCESS: epc  = "OK, ";             break;

        //        case RFID_TAGLOST: epc  = "TAG LOST, ";       break;

        //        case RFID_RESP_CRC: epc = "RESP CRC ERR, ";   break;

        //        case RFID_TAG_ERR: epc  = "Tag Error, Code: ";

        //                                  if (t.TagData != null)
        //                                  {
        //                                    epc += ReturnCodeStr(t);
        //                                  }                   break;
        //        default: epc = "Unknown, no data available";  break;
        //      }

        //      ++n;

        //      MessageBox.Show(n.ToString() + epc.ToString(), "Kill Results...");

        //    }//foreach
        //  }//try
        //  catch
        //  {
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