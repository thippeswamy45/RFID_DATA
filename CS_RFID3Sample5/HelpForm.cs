using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID3;
using System.Reflection;

namespace CS_RFID3Sample5
{
    public partial class HelpForm : Form
    {
        private AppForm m_AppForm;

        public HelpForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();
        }
        private void HelpForm_Load(object sender, EventArgs e)
        {
            if (null != m_AppForm.m_ReaderAPI)
            {
                this.dllVersionLabel.Text += ("C-Dll: " + m_AppForm.m_ReaderAPI.VersionInfo.Version);
                this.dllVersionLabel.Text += (", .NET-Dll: " + GetSymbolDotNetDllVersion());
            }
        }

        private string GetSymbolDotNetDllVersion()
        {
            //Retrieve the assembly from the dll name.
            Assembly myAssembly = Assembly.LoadFrom("Symbol.RFID3.Device.dll");
            if (myAssembly != null)
            {
                return myAssembly.GetName().Version.ToString();
            }
            return null;
        }
    }
}