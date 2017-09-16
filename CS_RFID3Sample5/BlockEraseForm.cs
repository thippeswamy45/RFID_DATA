using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID3;

namespace CS_RFID3Sample5
{
    public partial class BlockEraseForm : Form
    {
        private AppForm m_AppForm;
        internal TagAccess.BlockEraseAccessParams m_BlockEraseParams;

        public BlockEraseForm(AppForm appForm)
        {
            InitializeComponent();
            m_AppForm = appForm;
            m_BlockEraseParams = new TagAccess.BlockEraseAccessParams();
            m_BlockEraseParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER;
            m_BlockEraseParams.AccessPassword = 0;
            m_BlockEraseParams.ByteOffset = 0;
            m_BlockEraseParams.ByteCount = 4;
        }

        private void BlockEraseForm_Load(object sender, EventArgs e)
        {
            if (m_AppForm.inventoryList.SelectedIndices.Count > 0)
            {
                int selectedIndex = m_AppForm.inventoryList.SelectedIndices[0];
                ListViewItem item = m_AppForm.inventoryList.Items[selectedIndex];
                tagID_TB.Text = item.Text;
            }
            this.memBank_CB.SelectedIndex = (int)m_BlockEraseParams.MemoryBank;
            this.Password_TB.Text = m_BlockEraseParams.AccessPassword.ToString("X");
            this.offset_TB.Text = m_BlockEraseParams.ByteOffset.ToString();
            this.length_TB.Text = m_BlockEraseParams.ByteCount.ToString();              
        }

        private void eraseButton_Click(object sender, EventArgs e)
        {
            if (null != m_AppForm.m_ReaderAPI && m_AppForm.m_IsConnected)
            {
                try
                {
                    int length = int.Parse(this.length_TB.Text);

                    if (tagID_TB.Text.Length == 0)
                    {
                        this.m_AppForm.notifyUser("No TagID is defined", "Block Erase Operation");
                        return;
                    }                    
                    else if (length % 2 != 0)
                    {
                        this.m_AppForm.notifyUser("Data length has to be a word size (2X)", "Block Erase Operation");
                        return;
                    }

                    m_BlockEraseParams.MemoryBank = (MEMORY_BANK)this.memBank_CB.SelectedIndex;
                    m_BlockEraseParams.AccessPassword = 0;
                    if (this.Password_TB.Text.Length > 0)
                    {
                        string accessPassword = this.Password_TB.Text;
                        if (accessPassword.StartsWith("0x"))
                        {
                            accessPassword = accessPassword.Substring(2);
                        }
                        m_BlockEraseParams.AccessPassword = uint.Parse(
                            accessPassword, System.Globalization.NumberStyles.HexNumber);
                    }
                    m_BlockEraseParams.ByteOffset = ushort.Parse(this.offset_TB.Text);
                    m_BlockEraseParams.ByteCount = ushort.Parse(this.length_TB.Text);
                    m_AppForm.m_SelectedTagID = this.tagID_TB.Text;

                    this.m_AppForm.RunAccessOps(ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE);
                    this.eraseButton.Enabled = false;
                }
                catch (Exception ex)
                {
                    this.m_AppForm.notifyUser(ex.Message, "Block Erase Operation");
                }
            }
            else
            {
                this.m_AppForm.notifyUser("Please connect to a reader", "Block Erase Operation");
            }
        }
    }
}