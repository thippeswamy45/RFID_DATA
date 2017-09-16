using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID2;

namespace CS_RFID2_Host_Sample
{
    public partial class frmSetAntenna : Form
    {
        private static ReaderModel model;
        public frmSetAntenna(string[] antennaNames,ReaderModel reader_Model)
        {
            InitializeComponent();
            InitializeAntennaNames(antennaNames);
            model = reader_Model;
        }
        bool bValidate = true;
        internal int m_txMin = 0;
        internal int m_txMax = 255;

        
        
        internal bool setAntennaConfig = false;
        private void InitializeAntennaNames(string[] antennaNames)
        {
            if (antennaNames.Length > 0)
            {
                foreach (string name in antennaNames)
                    m_cmbAntennaName.Items.Add(name);

            }
            else
                throw new Exception("No Antenna Found!");
        }

        private AntennaConfig m_antConfig ;
        private AntennaConfig[] m_allantConfig;
        
        public AntennaConfig SelectedAntenna
        {
            get
            {
                return m_antConfig;
            }
        }
       
        public AntennaConfig[] AllAntenna
        {
            set
            {
                m_allantConfig = value;
            }
            get
            {
                return m_allantConfig;
            }
        }
        
        private bool IsTxValid()
        {
            try
            {
                if (int.Parse(txtTxPower.Text.ToString()) > m_txMax || int.Parse(txtTxPower.Text.ToString()) < m_txMin)
                {
                    MessageBox.Show("Enter Tx Power between " + m_txMin + "-" + m_txMax);
                    return false;
                }
                else
                    return true;
                
            }
            catch
            {
                MessageBox.Show("Enter Tx Power between " + m_txMin + "-" + m_txMax);
                return false;
            }
        }

        private bool IsRxValid()
        {
            if (model == ReaderModel.RD5000)
                return true;
            try
            {
                if (int.Parse(txtRxPower.Text.ToString()) > m_txMax || int.Parse(txtRxPower.Text.ToString()) < m_txMin)
                {
                    MessageBox.Show("Enter Rx Power between " + m_txMin + "-" + m_txMax);
                    return false;
                }
                else
                    return true;

            }
            catch
            {
                MessageBox.Show("Enter Rx Power between " + m_txMin + "-" + m_txMax);
                return false;
            }
        }
       
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!IsTxValid() )
            {
                bValidate = false;
                return;
            }


            if (!IsRxValid())
            {
                bValidate = false;
                return;
            }
            try
            {
                ArrayList tagTypeList = new ArrayList();

                if (chkClass0.Checked)
                    tagTypeList.Add(TagType.EPCClass0);

                if (chkClass1.Checked)
                    tagTypeList.Add(TagType.EPCClass1);

                if (chkGen2.Checked)
                    tagTypeList.Add(TagType.EPCClass1_GEN2);

                /*
                m_antConfig = new AntennaConfig(m_cmbAntennaName.SelectedItem.ToString(),
                                (TagType[])tagTypeList.ToArray(typeof(TagType)),
                               Convert.ToUInt32(txtTxPower.Text.ToString()), 0,true,
                               chkConnected.Checked);
                */
                foreach (AntennaConfig config in m_allantConfig)
                {
                    if (config.AntennaName == m_cmbAntennaName.SelectedItem.ToString())
                    {
                        m_antConfig = config;
                        break;
                    }
                }
                
                switch (model)
                {
                    case ReaderModel.XR480:
                    case ReaderModel.XR450:
                    case ReaderModel.XR440:
                    case ReaderModel.XR400:
                        m_antConfig.TagTypesInUse = (TagType[])tagTypeList.ToArray(typeof(TagType));
                        m_antConfig.TxPower = Convert.ToUInt32(txtTxPower.Text);
                        m_antConfig.RxPower = Convert.ToUInt32(txtRxPower.Text);
                        m_antConfig.IsEnabled = chkConnected.Checked;
                
                        setAntennaConfig = true;
                        break;
                    case ReaderModel.RD5000:

                        m_antConfig = new AntennaConfig(m_cmbAntennaName.SelectedItem.ToString(),
                                               (TagType[])tagTypeList.ToArray(typeof(TagType)),
                                                Convert.ToUInt32(txtTxPower.Text), 0,
                                                chkConnected.Checked, chkConnected.Checked);
                        setAntennaConfig = true;
                        break;
                }

                bValidate = true;

                this.Close();
            }
            catch (Exception ex)
            {
                if (ex.InnerException!=null)
                    MessageBox.Show(ex.Message + " " + ex.InnerException.Message, "SymbolReader WinApp");
                else
                    MessageBox.Show(ex.Message, "SymbolReader WinApp");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bValidate = true;
            setAntennaConfig = false;
            this.Close();
        }

        private void m_cmbAntennaName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetConfigurationDetails();
        }

        private void GetConfigurationDetails()
        {
            try
            {
                foreach (AntennaConfig config in m_allantConfig)
                {
                    if (config.AntennaName == m_cmbAntennaName.SelectedItem.ToString())
                    {
                        txtTxPower.Text = config.TxPower.ToString();
                        if (model == ReaderModel.RD5000)
                            txtRxPower.Text = "0";
                        else
                            txtRxPower.Text = config.RxPower.ToString();

                        if (config.IsEnabled)
                            chkConnected.Checked = true;
                        else
                            chkConnected.Checked = false;

                        ArrayList tagTypesInUseList = new ArrayList(config.TagTypesInUse);

                        if (tagTypesInUseList.Contains(TagType.EPCClass0))
                            chkClass0.Checked = true;
                        else
                            chkClass0.Checked = false;

                        if (tagTypesInUseList.Contains(TagType.EPCClass1))
                            chkClass1.Checked = true;
                        else
                            chkClass1.Checked = false;

                        if (tagTypesInUseList.Contains(TagType.EPCClass1_GEN2))
                            chkGen2.Checked = true;
                        else
                            chkGen2.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException!=null)
                    MessageBox.Show(ex.Message + " . "+ "\n" + ex.InnerException.Message, "SymbolReader WinApp");
                else
                    MessageBox.Show(ex.Message, "SymbolReader WinApp");
            }
        }

        private void frmSetAntenna_Load(object sender, EventArgs e)
        {
            m_cmbAntennaName.SelectedIndex = 0;
            GetConfigurationDetails();
            switch (model)
            {
                case ReaderModel.XR480:
                case ReaderModel.XR450:
                case ReaderModel.XR400:
                case ReaderModel.XR440:
                    txtTxPower.Enabled = true;
                    txtRxPower.Enabled = true;
                    chkConnected.Enabled = true;
                    chkClass0.Enabled = false;
                    chkGen2.Enabled = false;
                    chkClass1.Enabled = false;
                    m_txMax = 30;
                    m_txMin = 16;
                    break;
                case ReaderModel.RD5000:
                    txtTxPower.Enabled = true;
                    chkConnected.Enabled = true;
                    txtRxPower.Enabled = false;
                    m_txMax = 30;
                    m_txMin = 18;
                    break;
            }
                

        }

        private void frmSetAntenna_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!bValidate)
                e.Cancel = true;
        }

 

    }
}