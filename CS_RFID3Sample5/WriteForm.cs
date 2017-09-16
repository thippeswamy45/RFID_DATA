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
    public partial class WriteForm : Form
    {
        private AppForm m_AppForm;
        
        private bool m_IsBlockWrite;
        internal TagAccess.WriteAccessParams m_WriteParams;

        public WriteForm(AppForm appForm, bool isBlockWrite)
        {
            m_AppForm = appForm;
            InitializeComponent();
            
            m_IsBlockWrite = isBlockWrite;
            if (m_IsBlockWrite)
                this.Text = "Block Write Tags";
            else
                this.Text = "Write Tags";
            m_WriteParams = new TagAccess.WriteAccessParams();
            m_WriteParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER;
            m_WriteParams.AccessPassword = 0;
            m_WriteParams.ByteOffset = 0;
            m_WriteParams.WriteDataLength = 4;
        }

        private void WriteForm_Load(object sender, EventArgs e)
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
            this.memBank_CB.SelectedIndex = (int)m_WriteParams.MemoryBank;
            this.Password_TB.Text = m_WriteParams.AccessPassword.ToString("X");
            this.offset_TB.Text = m_WriteParams.ByteOffset.ToString();
            this.length_TB.Text = m_WriteParams.WriteDataLength.ToString();
        }

        private void writeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (null != m_AppForm.m_ReaderAPI && m_AppForm.m_IsConnected)
                {
                    ushort length = ushort.Parse(this.length_TB.Text);
                    if (tagID_TB.Text.Length == 0)
                    {
                        this.m_AppForm.notifyUser("No TagID is defined", "Write/BlockWrite Operation");
                        return;
                    }
                    else if (length * 2 > data_TB.Text.Length)
                    {
                        this.m_AppForm.notifyUser("Data length has to be a word size (2X)", "Write/BlockWrite Operation");
                        return;
                    }
                    else if (length % 2 != 0)
                    {
                        this.m_AppForm.notifyUser("Data length has to be a even number", "Write/BlockWrite Operation");
                        return;
                    }

                    string accessPassword = this.Password_TB.Text;
                    m_WriteParams.AccessPassword = 0;
                    if (accessPassword.Length > 0)
                    {
                        if (accessPassword.StartsWith("0x"))
                        {
                            accessPassword = accessPassword.Substring(2, accessPassword.Length - 2);
                        }
                        m_WriteParams.AccessPassword = uint.Parse(
                            accessPassword, System.Globalization.NumberStyles.HexNumber);
                    }

                    m_WriteParams.MemoryBank = (MEMORY_BANK)this.memBank_CB.SelectedIndex;
                    m_WriteParams.ByteOffset = ushort.Parse(this.offset_TB.Text);
                    m_WriteParams.WriteDataLength = length;

                    byte[] writeData = new byte[m_WriteParams.WriteDataLength];
                    for (int index = 0; index < m_WriteParams.WriteDataLength; index += 2)
                    {
                        writeData[index] = byte.Parse(data_TB.Text.Substring(index * 2, 2),
                            System.Globalization.NumberStyles.HexNumber);
                        writeData[index + 1] = byte.Parse(data_TB.Text.Substring((index + 1) * 2, 2),
                            System.Globalization.NumberStyles.HexNumber);
                    }
                    m_WriteParams.WriteData = writeData;
                    m_AppForm.m_SelectedTagID = this.tagID_TB.Text;

                    if (m_IsBlockWrite)
                    {
                        this.m_AppForm.RunAccessOps(ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_WRITE);
                    }
                    else
                    {
                        this.m_AppForm.RunAccessOps(ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE);
                    }
                    writeButton.Enabled = false;
                }
                else
                {
                    this.m_AppForm.notifyUser("Please connect to a reader", "Write Operation");
                }
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Write/BlockWrite Operation");                        
            }
        }

    }
}