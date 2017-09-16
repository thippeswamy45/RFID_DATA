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
    public partial class FrmReadData : Form
    {
        public FrmReadData(bool writeData)
        {
            InitializeComponent();
            if (writeData)
            {
                txtData.Visible = true;
                txtWordCount.Visible = false;
                label1.Visible = false;
                label3.Visible = true;
                lblDescCount.Visible = false;
                this.Text = "Write Data";
            }
            else 
            {
                txtData.Visible = false;
                txtWordCount.Visible = true;
                label1.Visible = true;
                label3.Visible = false;
                lblDescCount.Visible = true;
                this.Text = "Read Data";
            }
            cmbMemBank.SelectedIndex = 0;
        }

        public byte memBank = 0;
        public ushort wordPointer = 0;
        public byte wordCount = 0;
        public string writeData = string.Empty;
        public Gen2Parameters gen2Params = new Gen2Parameters(Gen2.InventorySelectFlag.Ignore_SL, Gen2.SessionFlag.S0, Gen2.TargetFlag.Bit_A, 6);
        public uint  accessPassword = 0;
        private bool IsValid = false;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int mem = cmbMemBank.SelectedIndex + 1; 
                memBank = byte.Parse(mem.ToString());
            }
            catch
            {
                IsValid = false;
                MessageBox.Show("Invalid value for memory bank .Enter value between 1 to 3");
            return;
            }

            try
            {
                wordPointer = ushort.Parse(txtPointer.Text);
            }
            catch
            {
                IsValid = false;
                MessageBox.Show("Invalid value for word pointer.Enter numeric value");
            return;
            }

            try
            {
                if (txtWordCount.Visible)
                    wordCount = byte.Parse(txtWordCount.Text);
            }
            catch
            {
                IsValid = false;
                MessageBox.Show("Invalid value for word pointer.Enter numeric value");
                return;
            }
            try
            {
                if (txtData.Visible)
                {
                    writeData = txtData.Text;
                    string patternAlphaNum = @"[a-fA-F\d]*";
                    if (writeData == null || writeData.Trim() == String.Empty)
                        throw new Exception("Invalid TagData : Cannot be Empty");

                    if (!(new Regex(patternAlphaNum).Match(writeData).Success))
                        throw new Exception("Invalid tag data");
                }
            }
            catch
            {
                IsValid = false;
                MessageBox.Show("Invalid value for tag data.Enter a valid byte array.");
                return;
            }

            try
            {
                gen2Params = new Gen2Parameters((Gen2.InventorySelectFlag)(cmbSel.SelectedIndex),
                    (Gen2.SessionFlag)cmbSession.SelectedIndex,
                    (Gen2.TargetFlag)cmbTarget.SelectedIndex, byte.Parse(numStartQ.Value.ToString()));
            }
            catch
            {
                IsValid = false;
                MessageBox.Show("Invalid value for Starting Q parameter");
                return;
            }


            try
            {
               accessPassword = uint.Parse(txtpassword.Text);
            }
            catch
            {
                IsValid = false;
                MessageBox.Show("Access password should have numeric value. Default = 0");
                return;
            }
            IsValid = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IsValid = true;
            this.Close();
        }

        private void FrmReadData_Closing(object sender, CancelEventArgs e)
        {
            if(!IsValid)
                e.Cancel = true;
        }

        private void FrmReadData_Load(object sender, EventArgs e)
        {
            cmbSel.SelectedIndex = 0;
            cmbSession.SelectedIndex = 0;
            cmbTarget.SelectedIndex = 0;
            numStartQ.Value = 6;
            txtpassword.Text = "0";
        }

      private void cmbSel_SelectedIndexChanged(object sender, EventArgs e)
      {

      }
     
    }
}