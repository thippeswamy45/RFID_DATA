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
    public partial class KillForm : Form
    {
        private AppForm m_AppForm;
        internal TagAccess.KillAccessParams m_KillParams;

        public KillForm(AppForm appForm)
        {
            InitializeComponent();
            m_AppForm = appForm;
            m_KillParams = new TagAccess.KillAccessParams();
            m_KillParams.KillPassword = 0;
        }

        private void KillForm_Load(object sender, EventArgs e)
        {
            if (m_AppForm.inventoryList.SelectedIndices.Count > 0)
            {
                int selectedIndex = m_AppForm.inventoryList.SelectedIndices[0];
                ListViewItem item = m_AppForm.inventoryList.Items[selectedIndex];
                this.tagID_TB.Text = item.Text;
            }
            this.killPwd_TB.Text = m_KillParams.KillPassword.ToString("X");
        }


        private void killButton_Click(object sender, EventArgs e)
        {
            if (null != m_AppForm.m_ReaderAPI && m_AppForm.m_IsConnected)
            {
                try
                {
                    if (tagID_TB.Text.Length == 0)
                    {
                        this.m_AppForm.notifyUser("No TagID is defined", "Kill Operation");
                        return;
                    }
                    string killPassword = this.killPwd_TB.Text;
                    if (killPassword.StartsWith("0x"))
                    {
                        killPassword = killPassword.Substring(2);
                    }
                    m_KillParams.KillPassword = uint.Parse(killPassword, System.Globalization.NumberStyles.HexNumber);
                }
                catch (Exception ex)
                {
                    this.m_AppForm.notifyUser(ex.Message, "Kill Operation");
                }
                m_AppForm.m_SelectedTagID = this.tagID_TB.Text;

                killButton.Enabled = false;
                this.m_AppForm.RunAccessOps(ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL);
            }
            else
            {
                this.m_AppForm.notifyUser("Please connect to a reader", "Kill Operation");
            }
        }
    }
}