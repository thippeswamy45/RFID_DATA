using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID3;

namespace CS_RFID3Sample6
{
    public partial class AntennaInfoForm : Form
    {
        private AppForm m_AppForm;
        private ArrayList m_CheckBox = null;
        private Symbol.RFID3.AntennaInfo m_AntennaList = null;
        private bool m_IsLoaded = false;

        public AntennaInfoForm(AppForm appForm)
        {
            InitializeComponent();
            m_AppForm = appForm;
            
            m_CheckBox = new ArrayList();
        }

        public Symbol.RFID3.AntennaInfo getInfo()
        {
            return m_AntennaList;
        }

        private void AntennaInfoForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!m_IsLoaded)
                {
                    int xPos = selectAll_CB.Location.X + 80;
                    int yPos = selectAll_CB.Location.Y + 60;
                    int numAtenna = m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas.Length;

                    for (int index = 0; index < numAtenna; index++)
                    {
                        if (index > 0 && (index % 2 == 0))
                        {
                            xPos = 80;
                            yPos += 60;
                        }

                        CheckBox cb = new CheckBox();
                        int name = index + 1;
                        cb.Location = new System.Drawing.Point(xPos, yPos);
                        cb.Name = "checkBox " + name;
                        cb.Size = new System.Drawing.Size(80, 20);
                        cb.TabIndex = name;
                        cb.Text = name.ToString();
                        cb.Checked = true;

                        Controls.Add(cb);
                        m_CheckBox.Add(cb);

                        xPos += 120;
                    }
                    selectAll_CB.Checked = true;
                    m_IsLoaded = true;
                }
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Antenna Info");
            }
        }

        private void AntennaInfoForm_Closing(object sender, EventArgs e)
        {
        }

        private void antennaInfoButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_AppForm.m_ReaderAPI.IsConnected)
                {
                    ArrayList checkList = new ArrayList();
                    foreach (CheckBox cb in m_CheckBox)
                    {
                        if (cb.Checked)
                        {
                            checkList.Add(cb.Text);
                        }
                    }
                    if (checkList.Count == 0)
                    {
                        foreach (CheckBox cb in m_CheckBox)
                        {
                            cb.Checked = true;
                            selectAll_CB.Checked = true;
                            checkList.Add(cb.Text);
                        }
                    }
                    if (checkList.Count > 0)
                    {
                        ushort[] antList = new ushort[checkList.Count];
                        for (int index = 0; index < checkList.Count; index++)
                        {
                            antList[index] = ushort.Parse(checkList[index].ToString());
                        }

                        if (null == m_AntennaList)
                        {
                            m_AntennaList = new Symbol.RFID3.AntennaInfo(antList);
                        }
                        else
                        {
                            m_AntennaList.AntennaID = antList;
                        }
                    }
                }
                else
                {
                    this.m_AppForm.notifyUser("Please connect to a reader", "Write Operation");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Antenna Info");
            }

        }

        private void selectAll_CB_CheckStateChanged(object sender, EventArgs e)
        {
            foreach (CheckBox cb in m_CheckBox)
            {
                cb.Checked = selectAll_CB.Checked;
            }
        }
    }
}