using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID3;

namespace CS_RFID3Sample6
{
    public partial class ReadForm : Form
    {
        private AppForm m_AppForm;
        
        internal TagAccess.ReadAccessParams m_ReadParams = null;

        public ReadForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();

            m_ReadParams = new TagAccess.ReadAccessParams();
            m_ReadParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_EPC;
            m_ReadParams.AccessPassword = 0;
            m_ReadParams.ByteOffset = 0;
            m_ReadParams.ByteCount = 12;
        }

        private void ReadForm_Load(object sender, EventArgs e)
        {
            if (m_AppForm.inventoryList.SelectedIndices.Count > 0)
            {
                int selectedIndex = m_AppForm.inventoryList.SelectedIndices[0];
                ListViewItem item = m_AppForm.inventoryList.Items[selectedIndex];
                tagID_TB.Text = item.Text;
            }
            else
            {
                tagID_TB.Text = "";
            }
            ReadData_TB.Text = "";
            memBank_CB.SelectedIndex = (int)m_ReadParams.MemoryBank;
            Password_TB.Text = m_ReadParams.AccessPassword.ToString("X");
            offset_TB.Text = m_ReadParams.ByteOffset.ToString();
            length_TB.Text = m_ReadParams.ByteCount.ToString();
        }

        private void readButton_Click(object sender, EventArgs e)
        {
            if (null != m_AppForm.m_ReaderAPI && m_AppForm.m_IsConnected)
            {
                try
                {
                    if (tagID_TB.Text.Length == 0 && m_AppForm.m_AccessFilterForm.getFilter() == null)
                    {
                        this.m_AppForm.notifyUser("No TagID or Access Filter is defined", "Read Operation");
                        return;
                    }
                    string accessPassword = this.Password_TB.Text;
                    m_ReadParams.AccessPassword = 0;
                    if (accessPassword.Length > 0)
                    {
                        if (accessPassword.StartsWith("0x"))
                        {
                            accessPassword = accessPassword.Substring(2, accessPassword.Length - 2);
                        }
                        m_ReadParams.AccessPassword = uint.Parse(
                            accessPassword, System.Globalization.NumberStyles.HexNumber);
                    }

                    m_ReadParams.MemoryBank = (MEMORY_BANK)this.memBank_CB.SelectedIndex;
                    m_ReadParams.ByteOffset = ushort.Parse(this.offset_TB.Text);
                    m_ReadParams.ByteCount = ushort.Parse(this.length_TB.Text);

                    ReadData_TB.Text = "";
                    m_AppForm.m_SelectedTagID = this.tagID_TB.Text;
                    m_AppForm.RunAccessOps(ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ);
                    readButton.Enabled = false;
                }
                catch (Exception ex)
                {
                    this.m_AppForm.notifyUser(ex.Message, "Read Operation");
                }
            }
            else
            {
                this.m_AppForm.notifyUser("Please connect to a reader", "Read Operation");
            }
        }

        private void accessFilterButton_Click(object sender, EventArgs e)
        {
            m_AppForm.m_AccessFilterForm.ShowDialog();
        }

        private void tagMask_TB_TextChanged(object sender, EventArgs e)
        {
            if (tagID_TB.Text.Length == 0 && !accessFilterButton.Enabled)
            {
                accessFilterButton.Enabled = true;
            }
            else if (tagID_TB.Text.Length > 0 && accessFilterButton.Enabled)
            {
                accessFilterButton.Enabled = false;
            }
        }
    }
}