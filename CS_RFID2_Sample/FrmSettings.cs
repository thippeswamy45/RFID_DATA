using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace CS_RFID2_Sample
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
        }
        public Hashtable htInParamSet = new Hashtable();
        public Hashtable htParams
        {
            get { return htInParamSet; }
            set { htInParamSet = value; }
        }
        bool bValidate = false;

        public enum Commands
        {
            ProgramTag,
            WriteTag,
            LockTag,
            KillTag,
            EraseTag

        }

        public Commands currentCommand;

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox1.Focus();
            //htInParams["TagType"] = comboBox1.SelectedValue.ToString();
            switch (currentCommand)
            {
                case Commands.ProgramTag:
                    txtTagID.Visible = true;
                    lblTagID.Visible = true;
                    comboBox1.Items.Remove("CLASS0");
                    break;

                case Commands.EraseTag:
                    txtTagID.Visible = false;
                    lblTagID.Visible = false;
                    break;

                case Commands.WriteTag:
                    break;

                case Commands.LockTag:
                    txtTagID.Visible = true;
                    lblTagID.Visible = true;
                    comboBox1.Enabled = false;
                    break;

                case Commands.KillTag:
                    //txtTagID.Visible = true;
                    //lblTagID.Visible = true;
                    break;


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Validate();
            if(!bValidate)
                return;
            
            try
            {
                htInParamSet["TagType"] = comboBox1.SelectedItem.ToString();
                switch (currentCommand)
                {
                    case Commands.ProgramTag:
                        htInParamSet["TagID"] = txtTagID.Text.Replace(" ", string.Empty);
                        break;

                    case Commands.EraseTag:
                        htInParamSet["TagID"] = txtTagID.Text.Replace(" ", string.Empty);
                        break;

                    case Commands.WriteTag:
                        break;

                    case Commands.LockTag:
                        htInParamSet["TagID"] = txtTagID.Text.Replace(" ", string.Empty);
                        break;

                    case Commands.KillTag:
                        htInParamSet["TagID"] = txtTagID.Text.Replace(" ", string.Empty);
                        break;
                }
                                 
                  this.Close();
            }            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CS_RFID2_Sample");
            }
        }

        private void Validate()
        {
            try
            {
                if (txtTagID.Visible)
                {
                    //if (txtTagID.Text.Length != 16 && txtTagID.Text.Length != 24)
                    //    throw new Exception("Tag ID should be 8 byte or 12 byte ");

                    char[] chTagID = txtTagID.Text.ToCharArray();

                    for (int i = 0; i < chTagID.Length - 1; i += 2)
                    {
                        string strTemp = new string(new char[] { chTagID[i], chTagID[i + 1] });
                        byte idByte = Convert.ToByte(strTemp, 16);
                    }
                }
                bValidate = true;
            }
            catch (FormatException )
            {
                MessageBox.Show("Enter a valid Tag ID", "CS_RFID2_Sample");
                bValidate = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CS_RFID2_Sample");
                bValidate = false;
            }    
        }

        private void FrmSettings_Closing(object sender, CancelEventArgs e)
        {
            if (!bValidate)
                e.Cancel = true; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            bValidate = true;
            htInParamSet = null;
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentCommand == Commands.KillTag)
            {
                if (comboBox1.SelectedItem.ToString() != "CLASS1")
                {
                    txtTagID.Visible = false;
                    lblTagID.Visible = false;
                }
                else
                {
                    txtTagID.Visible = true;
                    lblTagID.Visible = true;
                }
            }
        }

       

           
    }
}