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
    public partial class TagDataForm : Form
    {
        private AppForm m_AppForm;
        private bool m_IsLoaded;

        public TagDataForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();
        }

        private void TagDataForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (m_AppForm.m_IsConnected && !m_IsLoaded)
                {
                    if (m_AppForm.inventoryList.SelectedIndices.Count > 0)
                    {
                        int index = m_AppForm.inventoryList.SelectedIndices[0];
                        ListViewItem item = m_AppForm.inventoryList.Items[index];

                        tagDataView.Items[0].SubItems.Add(item.SubItems[0].Text);
                        if (item.SubItems.Count > 7)
                        {
                            tagDataView.Items[1].SubItems.Add(item.SubItems[1].Text);
                            tagDataView.Items[2].SubItems.Add(item.SubItems[3].Text);
                            tagDataView.Items[3].SubItems.Add(item.SubItems[4].Text);//PC Bits
                            tagDataView.Items[4].SubItems.Add(item.SubItems[6].Text);
                            tagDataView.Items[5].SubItems.Add(item.SubItems[5].Text);
                            tagDataView.Items[6].SubItems.Add(item.SubItems[7].Text);
                            int length = item.SubItems[5].Text.Length / 2;
                            tagDataView.Items[7].SubItems.Add(length.ToString());
                        }
                        m_IsLoaded = true;
                    }
                    else
                    {
                        this.m_AppForm.notifyUser("No item is selected", "Tag Data");
                    }
                }
                else
                {
                    this.m_AppForm.notifyUser("Please connect to a reader", "Tag Data");
                }
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Tag Data");
            }
        }
    }
}