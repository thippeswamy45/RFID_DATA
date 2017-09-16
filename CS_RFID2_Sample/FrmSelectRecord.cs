using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Symbol.RFID2;
using System.Collections.ObjectModel;



namespace CS_RFID2_Sample
{
    public partial class FrmSelectRecord : Form
    {

        int index;

        int rtn;

        int srCnt = 0;

        const int MAX_SELECT_RECORDS = 4;

        IRFIDReader Reader;

        Utilities Utils = new Utilities ();

        //Gen2SelectRecordParameters[] srList = new Gen2SelectRecordParameters[4];
        ReadOnlyCollection<Gen2SelectRecordParameters> srList = null; 

        Gen2SelectRecordParameters sr = new Gen2SelectRecordParameters ();

        public FrmSelectRecord (ref IRFIDReader Reader)
        {
            this.Reader = Reader;

            InitializeComponent ();
        }

        private void FrmSelectRecord_Load (object sender, EventArgs e)
        {
            SetEmpty ();

            RefreshSR ();
        }

        private void btn_Cancel_Click (object sender, EventArgs e)
        {
            this.Close ();
        }

        public bool GetParms ()
        {
            sr = new Gen2SelectRecordParameters();

            try {
                int i = cmbTarget.SelectedIndex;

                sr.Target = (Gen2.SelectTarget)byte.Parse (i.ToString ());
            }
            catch {
                MessageBox.Show ("Invalid value for Target");
                return (false);
            }

            try {
                int i = cmbAction.SelectedIndex;

                sr.Action = (Gen2.SelectAction)byte.Parse (i.ToString ());
            }
            catch {
                MessageBox.Show ("Invalid Select Index");
                return (false);
            }


            try {
                int i = cmbMemBank.SelectedIndex + 1; //

                sr.MemoryBank = (Gen2.MemoryBank)byte.Parse (i.ToString ());
            }
            catch {
                MessageBox.Show ("Invalid value for memory bank .Enter value between 1 to 3");
                return (false);
            }


            try {
                sr.BitPointer = ushort.Parse (txtBitPtr.Text);
            }
            catch {
                MessageBox.Show ("Invalid value for bit pointer.Enter numeric value");
                return (false);
            }

            try {
                sr.BitLength = byte.Parse (txtLength.Text);
            }
            catch {
                MessageBox.Show ("Invalid value for mask length. Enter numeric value");
                return (false);
            }

            try {
                //ASCIIEncoding AE = new ASCIIEncoding();

                String s = txtMask.Text;

                if (s != null && s.Trim () != String.Empty) {
                    sr.Mask = Utils.Hexify (s);
                } else {
                    throw new Exception ("Invalid TagData : Cannot be Empty");
                }
            }
            catch {
                MessageBox.Show ("Invalid value for mask data.Enter a valid byte array.");
                return (false);
            }

            try {
                sr.Truncate = byte.Parse (txtTruncate.Text);
            }
            catch {
                MessageBox.Show ("Enter 0 or 1...Default = 0");
                return (false);
            }
            return (true);
        }

        private void btn_OK_Click (object sender, EventArgs e)
        {
            this.Close ();
        }

        private void FrmSelectRecord_Closing (object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        public void SetEmpty ()
        {
            //cmbIndex.SelectedIndex = 0;
            cmbTarget.SelectedIndex = 0;
            cmbAction.SelectedIndex = 5;
            cmbMemBank.SelectedIndex = 0;
            txtBitPtr.Text = "32";
            txtLength.Text = "0";
            txtMask.Text = "0";
            txtTruncate.Text = "0";
        }
        
        public void SetParms (int j)
        {
            cmbIndex.SelectedIndex = j;

            cmbTarget.SelectedIndex = (int)srList[j].Target;

            cmbAction.SelectedIndex = (int)srList[j].Action;

            cmbMemBank.SelectedIndex = (int)srList[j].MemoryBank - 1;

            txtBitPtr.Text = srList[j].BitPointer.ToString ();
                
            txtLength.Text = srList[j].BitLength.ToString ();

            txtMask.Text = Utils.Textify (srList[j].Mask);

            txtTruncate.Text = srList[j].Truncate.ToString ();
        }

        private void cmbIndex_SelectedIndexChanged (object sender, EventArgs e)
        {
            int index = cmbIndex.SelectedIndex;

            if (srCnt > index) {
                SetParms (index);
            } else {
                MessageBox.Show ("SR Index Not In Use");
            }
        }
        
        private void btn_Refresh_Click (object sender, EventArgs e)
        {
            RefreshSR ();
        }
        
        private void txtMask_GotFocus (object sender, EventArgs e)
        {
            txtMask.Text = "";
        }

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
        
        public void RefreshSR ()
        {
            srList = Reader.SelectRecords; 

            txtInUse.Text = srList.Count.ToString ();         

            srCnt = srList.Count;

            SetParms(0);

            cmbIndex.SelectedItem = 0;

            if (srList[0].Default)
            {
                txtInUse.Text = (srCnt - 1).ToString();   // show the default select record
            }
            else
            {
                txtInUse.Text = srCnt.ToString();
            }
             
        }
        
        private void btn_Add_Click (object sender, EventArgs e)
        {
            bool status;

            status = GetParms ();

            if (status == true) {
                rtn = Reader.SelectRecordAdd (sr);

                if (rtn == 0xff)  { //4 defines the maximum number of SR permissible
                    MessageBox.Show ("Other Error occurred");
                } else if (rtn == -1) {
                    MessageBox.Show ("Reached Maximum of 4 SRs");
                } else {
                    MessageBox.Show ("Successfully Added");

                    RefreshSR ();
                }
            }
        }
        
        private void btn_Remove_Click (object sender, EventArgs e)
        {
            index = cmbIndex.SelectedIndex;

            rtn = Reader.SelectRecordRemove ((byte)index);

            if (rtn == -1) {
                MessageBox.Show ("Index does not exist");
            } else if (rtn == -2) {
                MessageBox.Show ("Other Error occurred");
            } else {
                MessageBox.Show ("Successfully Removed");

                RefreshSR ();
            }
        }
    }
}

