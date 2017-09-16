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
    public partial class AccessFilterForm : Form
    {
        private AppForm m_AppForm;
        private bool m_IsLoaded;
        private AccessFilter m_AccessFilter = null;
        private Boolean m_UseAccessFilter = false;

        public AccessFilterForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();
        }

        public Symbol.RFID3.AccessFilter getFilter()
        {
           return  m_UseAccessFilter == true ? m_AccessFilter : m_AccessFilter;
        }

        private void AccessFilterForm_Load(object sender, EventArgs e)
        {
            if (!m_IsLoaded)
            {
                matchPattern_CB.SelectedIndex = 0;
                memBank_CB1.SelectedIndex = 1;
                memBank_CB2.SelectedIndex = 1;
                m_IsLoaded = true;
            }
        }

        private void AccessFilterForm_Closing(object sender, EventArgs e)
        {            
        }

        private void accessFilterButton_Click(object sender, EventArgs e)
        {
            string exceptionMsg = "TagPatternA BitOffset:";
            try
            {
                if (m_AppForm.m_ReaderAPI.IsConnected && useAccessFilter_CB.Checked)
                {
                    if (null == m_AccessFilter)
                    {
                        m_AccessFilter = new Symbol.RFID3.AccessFilter();
                    }
                    m_UseAccessFilter = useAccessFilter_CB.Checked;
                    m_AccessFilter.MatchPattern = (MATCH_PATTERN)matchPattern_CB.SelectedIndex;

                    /*
                     *  Tag Pattern A
                     */
                    m_AccessFilter.TagPatternA.MemoryBank = (MEMORY_BANK)memBank_CB1.SelectedIndex;
                    m_AccessFilter.TagPatternA.BitOffset = ushort.Parse(offset_TB1.Text);

                    int maskLengthA = (tagMask_TB1.Text.Length / 2);
                    byte[] filterMaskA = new byte[maskLengthA];
                    exceptionMsg = "TagPatternA Mask:";
                    for (int index = 0; index < maskLengthA; index++)
                    {
                        filterMaskA[index] = byte.Parse(tagMask_TB1.Text.Substring(index * 2, 2),
                            System.Globalization.NumberStyles.HexNumber);
                    }
                    m_AccessFilter.TagPatternA.TagMask = filterMaskA;
                    m_AccessFilter.TagPatternA.TagMaskBitCount = (uint)maskLengthA * 8;

                    int dataLengthA = (MembankData_TB1.Text.Length / 2);
                    byte[] memoryBankDataA = new byte[dataLengthA];
                    exceptionMsg = "TagPatternA Pattern:";
                    for (int index = 0; index < dataLengthA; index++)
                    {
                        memoryBankDataA[index] = byte.Parse(MembankData_TB1.Text.Substring(index * 2, 2),
                            System.Globalization.NumberStyles.HexNumber);
                    }
                    m_AccessFilter.TagPatternA.TagPattern = memoryBankDataA;
                    m_AccessFilter.TagPatternA.TagPatternBitCount = (uint)dataLengthA * 8;

                    if (m_AccessFilter.MatchPattern != MATCH_PATTERN.A)
                    {
                        /*
                         *  Tag Pattern B
                         */
                        exceptionMsg = "TagPatternB BitOffset:";
                        m_AccessFilter.TagPatternB.MemoryBank = (MEMORY_BANK)memBank_CB2.SelectedIndex;
                        m_AccessFilter.TagPatternB.BitOffset = ushort.Parse(offset_TB2.Text);

                        int maskLengthB = (tagMask_TB2.Text.Length / 2);
                        byte[] filterMaskB = new byte[maskLengthB];
                        exceptionMsg = "TagPatternB Mask:";
                        for (int index = 0; index < maskLengthB; index++)
                        {
                            filterMaskB[index] = byte.Parse(tagMask_TB2.Text.Substring(index * 2, 2),
                                System.Globalization.NumberStyles.HexNumber);
                        }
                        m_AccessFilter.TagPatternB.TagMask = filterMaskB;
                        m_AccessFilter.TagPatternB.TagMaskBitCount = (uint)maskLengthB * 8;

                        int dataLengthB = (MembankData_TB2.Text.Length / 2);
                        byte[] memoryBankDataB = new byte[dataLengthB];
                        exceptionMsg = "TagPatternB Pattern:";
                        for (int index = 0; index < dataLengthB; index++)
                        {
                            memoryBankDataB[index] = byte.Parse(MembankData_TB2.Text.Substring(index * 2, 2),
                                System.Globalization.NumberStyles.HexNumber);
                        }
                        m_AccessFilter.TagPatternB.TagPattern = memoryBankDataB;
                        m_AccessFilter.TagPatternB.TagPatternBitCount = (uint)dataLengthB * 8;
                    }
                }
                else if (!useAccessFilter_CB.Checked)
                {
                    m_AccessFilter = null;
                }
                this.Close();                
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(exceptionMsg + ex.Message, "Access Filter");
            }
        }

        private void useAccessFilter_CB_CheckStateChanged(object sender, EventArgs e)
        {
            tabControl1.Enabled = useAccessFilter_CB.Checked;
        }
    }
}