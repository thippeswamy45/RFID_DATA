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
    public partial class LoginForm : Form
    {
        private AppForm m_AppForm;
        private bool m_IsLoaded;

        public LoginForm(AppForm appForm)
        {
            m_AppForm = appForm;
            InitializeComponent();
        }

        public void clearPassword()
        {
            password_TB.Text = "";
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (!m_IsLoaded)
            {
                if (null != m_AppForm.m_ReaderAPI &&
                    m_AppForm.m_IsConnected)
                {
                    hostname_TB.Text = m_AppForm.m_ConnectionForm.IpText; 
                }
                readerType_CB.SelectedIndex = 2;
                m_IsLoaded = true;
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            bool closeForm = false;
            try
            {
                if (loginButton.Text == "Login")
                {
                    LoginInfo info = new LoginInfo();
                    info.HostName = hostname_TB.Text;
                    info.UserName = username_TB.Text;
                    info.Password = password_TB.Text;
                    info.SecureMode = SECURE_MODE.HTTP;
                    info.ForceLogin = forceLogin_CB.Checked;
                    m_AppForm.m_ReaderMgmt.Login(info, (READER_TYPE)readerType_CB.SelectedIndex);
                    if (null != this.m_AppForm.m_FirmwareUpdateForm)
                    {
                        this.m_AppForm.m_FirmwareUpdateForm.Reset();
                    }
                    m_AppForm.rebootMenuItem.Enabled = true;
                    m_AppForm.antModeMenuItem.Enabled = true;
                    m_AppForm.radioPowerMenuItem.Enabled = true;
                    m_AppForm.softwareUpdateMenuItem.Enabled = true;
                    m_AppForm.systemInfoMenuItem.Enabled = true;
                    m_AppForm.m_ReaderType = (READER_TYPE)readerType_CB.SelectedIndex;
                    loginButton.Text = "Logout";
                    closeForm = true;
                }
                else if (loginButton.Text == "Logout")
                {
                    closeForm = true;
                    m_AppForm.rebootMenuItem.Enabled = false;
                    m_AppForm.antModeMenuItem.Enabled = false;
                    m_AppForm.radioPowerMenuItem.Enabled = false;
                    m_AppForm.softwareUpdateMenuItem.Enabled = false;
                    m_AppForm.systemInfoMenuItem.Enabled = false;
                    loginButton.Text = "Login";
                    m_AppForm.m_ReaderMgmt.Logout();
                }
            }
            catch (OperationFailureException ofe)
            {
                this.m_AppForm.notifyUser(ofe.StatusDescription, loginButton.Text);
            }
            catch (Exception ex)
            {
                this.m_AppForm.notifyUser(ex.Message, "Reader Login");
            }
            hostname_TB.Enabled = username_TB.Enabled = password_TB.Enabled =
                readerType_CB.Enabled = (loginButton.Text == "Login");
            m_AppForm.configureMenuItemsUponLoginLogout();
            if (closeForm)
                this.Close();
        }

        private void readerType_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isMCType = ((READER_TYPE)readerType_CB.SelectedIndex == READER_TYPE.MC);
            username_TB.Enabled = !isMCType;
            password_TB.Enabled = !isMCType;
            forceLogin_CB.Enabled = ((READER_TYPE)readerType_CB.SelectedIndex == READER_TYPE.FX);
        }
    }
}