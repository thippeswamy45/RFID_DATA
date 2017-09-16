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
    public partial class PostFilterForm : Form
    {
        private AppForm m_AppForm;
        private bool m_IsLoaded;
        private PostFilter m_PostFilter = null;
        
        public PostFilterForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();
        }

        public Symbol.RFID3.PostFilter getFilter()
        {
            return m_PostFilter;
        }

        private void PostFilterForm_Load(object sender, EventArgs e)
        {
            if (!m_IsLoaded)
            {
                matchPattern_CB.SelectedIndex = 0;
                memBank_CB1.SelectedIndex = 1;
                memBank_CB2.SelectedIndex = 1;
                m_IsLoaded = true;
            }
        }

        private void PostFilterForm_Closing(object sender, EventArgs e)
        {
 
        }

        private void accessFilterButton_Click(object sender, EventArgs e)
        {
            string exceptionMsg = "TagPatternA BitOffset:";
            try
            {
                if (m_AppForm.m_ReaderAPI.IsConnected && userFilter_CB.Checked)
                {
                    if (null == m_PostFilter)
                    {
                        m_PostFilter = new Symbol.RFID3.PostFilter();
                    }
                    m_PostFilter.MatchPattern = (MATCH_PATTERN)matchPattern_CB.SelectedIndex;

                    /*
                     *  Tag Pattern A
                     */
                    m_PostFilter.TagPatternA.MemoryBank = (MEMORY_BANK)memBank_CB1.SelectedIndex;
                    m_PostFilter.TagPatternA.BitOffset = ushort.Parse(offset_TB1.Text);

                    exceptionMsg = "TagPatternA Mask:";
                    ushort maskLengthA = (ushort)(tagMask_TB1.Text.Length / 2);
                    byte[] filterMaskA = new byte[maskLengthA];
                    for (int index = 0; index < maskLengthA; index++)
                    {
                        filterMaskA[index] = byte.Parse(tagMask_TB1.Text.Substring(index * 2, 2),
                            System.Globalization.NumberStyles.HexNumber);
                    }
                    m_PostFilter.TagPatternA.TagMask = filterMaskA;
                    m_PostFilter.TagPatternA.TagMaskBitCount = (uint)maskLengthA * 8;

                    exceptionMsg = "TagPatternA Pattern:";
                    ushort dataLengthA = (ushort)(MembankData_TB1.Text.Length / 2);
                    byte[] memoryBankDataA = new byte[dataLengthA];
                    for (int index = 0; index < dataLengthA; index++)
                    {
                        memoryBankDataA[index] = byte.Parse(MembankData_TB1.Text.Substring(index * 2, 2),
                            System.Globalization.NumberStyles.HexNumber);
                    }
                    m_PostFilter.TagPatternA.TagPattern = memoryBankDataA;
                    m_PostFilter.TagPatternA.TagPatternBitCount = (uint)dataLengthA * 8;

                    if (m_PostFilter.MatchPattern != MATCH_PATTERN.A)
                    {
                        /*
                         *  Tag Pattern B
                         */
                        exceptionMsg = "TagPatternB BitOffset:";                    
                        m_PostFilter.TagPatternB.MemoryBank = (MEMORY_BANK)memBank_CB2.SelectedIndex;
                        m_PostFilter.TagPatternB.BitOffset = ushort.Parse(offset_TB2.Text);

                        ushort maskLengthB = (ushort)(tagMask_TB2.Text.Length / 2);
                        byte[] filterMaskB = new byte[maskLengthB];
                        exceptionMsg = "TagPatternB Mask:";       
                        for (int index = 0; index < maskLengthB; index++)
                        {
                            filterMaskB[index] = byte.Parse(tagMask_TB2.Text.Substring(index * 2, 2),
                                System.Globalization.NumberStyles.HexNumber);
                        }
                        m_PostFilter.TagPatternB.TagMask = filterMaskB;
                        m_PostFilter.TagPatternB.TagMaskBitCount = (uint)maskLengthB * 8;

                        ushort dataLengthB = (ushort)(MembankData_TB2.Text.Length / 2);
                        byte[] memoryBankDataB = new byte[dataLengthB];
                        exceptionMsg = "TagPatternB Pattern:";       
                        for (int index = 0; index < dataLengthB; index++)
                        {
                            memoryBankDataB[index] = byte.Parse(MembankData_TB2.Text.Substring(index * 2, 2),
                                System.Globalization.NumberStyles.HexNumber);
                        }
                        m_PostFilter.TagPatternB.TagPattern = memoryBankDataB;
                        m_PostFilter.TagPatternB.TagPatternBitCount = (uint)dataLengthB * 8;
                    }
                }
                else if (!userFilter_CB.Checked)
                {
                    m_PostFilter = null;
                }
                this.Close();                
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(exceptionMsg + ex.Message, "Access Filter");
            }
        }

        private void userFilter_CB_CheckStateChanged(object sender, EventArgs e)
        {
            tabControl1.Enabled = userFilter_CB.Checked;
        }
    }
}