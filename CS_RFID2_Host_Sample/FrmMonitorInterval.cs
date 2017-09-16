using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CS_RFID2_Host_Sample
{
    public partial class FrmMonitorInterval : Form
    {
        public FrmMonitorInterval()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 10000000000)
            {
                MessageBox.Show("Enter interval between 5 to 10000000000 sec",
                                this.Text,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            
            //pzhu Symbol.RFID2.Reader.StatusMonitorIntervalSec = Convert.ToInt32(numericUpDown1.Value);
            this.Close();
        }

        private void FrmMonitorInterval_Load(object sender, EventArgs e)
        {
            //pzhu numericUpDown1.Value = Convert.ToDecimal(Symbol.RFID2.Reader.StatusMonitorIntervalSec);
        }
    }
}