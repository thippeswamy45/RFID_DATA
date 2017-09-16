using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Symbol.RFID2;


namespace CS_RFID2_Host_Sample
{
    /// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class frmTest : System.Windows.Forms.Form
	{   
        private IRFIDReader m_ObjReader = null;
		#region		Private Constant
		    private const string m_StartAutoMode =                    "01. Set Autonomous Mode";
		    private const string m_OnDemandMode =                     "02. Set OnDemand Mode";
            private const string m_GetTags =                          "03. Get Tags";
            private const string m_WriteTagID =                       "04. Write Tag ID";
            private const string m_GetIOStatus =                      "05. Get IO Status";
            private const string m_SetOutputStatus =                  "06. Set Output Status";
            private const string m_EnableInputStatusNotification =    "07. Enable InputStatus Notification";
            private const string m_DisableInputStatusNotification =   "08. Disable InputStatus Notification";
            private const string m_EnableProximitySensor =            "09. Enable Proximity Sensor";
            private const string m_DisableProximitySensor =           "10. Disable Proximity Sensor";
            private const string m_EnableMotionSensor =               "11. Enable Motion Sensor";
            private const string m_DisableMotionSensor =              "12. Disable Motion Sensor";
            private const string m_EnableRFIDModule =                 "13. Enable RFID Module";
            private const string m_DisableRFIDModule =                "14. Disable RFID Module";
            private const string m_GetRFIDModuleStatus =              "15. Get RFID Module Status"; 
        //not in use
            private const string m_KillTags = "5.  Kill Tags";
            private const string m_EraseTags = "6.  Erase Tags";
            private const string m_LockTags = "16.  Lock Tags";
            
            private const string m_WriteTags = "17.  Write Tags";
            private const string m_ReadTagData = "18.  Read Tags"; 
		
		#endregion		Private Constant

        private string timeStampFormat = "MMM dd, HH:mm:ss";
        #region Private Members



        private System.ComponentModel.Container components = null;

        //private int m_timeOut = 5000;
        private ClsReader m_ClsReader = null;
		
		private Cursor m_cursor=null;
        //private ThreadStart m_threadFunction = null;
        //private Thread m_threadExecute = null;
        //private bool m_CancelCmdExec = false;
	
		
        private string m_strReaderInfo = null;
		private object m_Object = new object();
		//private bool m_IsVisible = true;
        private string reader_Model = "";
        


		#region Filter Variable
        private GroupBox m_gbxCommand;
        private Label m_lblAutoPoll;
        private Label m_lblReaderInfo;
        private Button m_btnClearResp;
        private GroupBox m_gpBxParameters;
        private Button m_btnCancel;
        private ComboBox m_cboCmdList;
        private Button m_btnSend;
        private GroupBox groupBox3;
        private Button m_btnDisconnect;
        private Button m_btnConnect;
        private Label m_lblStatus;
        private Label m_lblActive;
        private Label m_lblInActive;
        private TabControl m_tabCtlOutput;
        private TabPage m_tabPgTableView;
   
        private TabPage m_tabPgCmdOutput;
        private ListView listView2;
        private ColumnHeader TagID;
        private ColumnHeader TagType;
        private ListView listView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader CmdOutputColumn;
        private Button m_btnAntennaSetting;
        private Button m_btnReaderInfo;
        private ColumnHeader columnHeader3;
        private TextBox m_txtTagID;
        private Label m_lblTagID;
        private Label m_lblAntenna;
        private ComboBox m_cmbAntenna;
        private ColumnHeader columnHeader4;
        private CheckedListBox m_chkbxlstoutpins;
        private Label m_lblNotification;
        private TextBox m_tbxNotification;
        private GroupBox grpBxInpLineStatus;
        private RadioButton m_rbtnInpPin5;
        private RadioButton m_rbtnInpPin4;
        private RadioButton m_rbtnInpPin6;
        private RadioButton m_rbtnInpPin3;
        private RadioButton m_rbtnInpPin2;
        private RadioButton m_rbtnInpPin1;
        private Label m_lblPin6;
        private Label m_lblPin5;
        private Label m_lblPin4;
        private Label m_lblPin3;
        private Label m_lblPin2;
        private Label m_lblPin1;
        private GroupBox groupBox2;
        private GroupBox grpBxAntennaStatus;
        private Label m_lblAnt4;
        private Label m_lblAnt3;
        private Label m_lblAnt2;
        private Label m_lblAnt1;
        private RadioButton m_rdiobtnantena4;
        private RadioButton m_rdiobtnantena3;
        private RadioButton m_rdiobtnantena2;
        private RadioButton m_rdiobtnantena1;
        private GroupBox grpbxSensors;
        private Label m_lblMotionZ;
        private Label m_lblMotionY;
        private Label m_lblMotionX;
        private Label m_lblProximity;
        private ProgressBar proximityProgBar;
        private ProgressBar motionZProgBar;
        private ProgressBar motionYProgBar;
        private ProgressBar motionXProgBar;
        private Button m_btnreaderCapability;
        private TextBox m_tbxtimeintervalMS;
        private Label m_lbltimeintervalMS;
        private Label m_lblRFIDStatus;
        private Label lblCount;
        private Label lblWriteData;
        private TextBox txtCount;
        private Label lblPointer;
        private TextBox txtMemBank;
        private Label lblMemoryBank;
        private TextBox txtWriteData;
        private TextBox txtPointer;
        private Label label1;
        private Label m_lblAnt5;
        private RadioButton m_rdiobtnantena8;
        private RadioButton m_rdiobtnantena7;
        private RadioButton m_rdiobtnantena6;
        private RadioButton m_rdiobtnantena5;
        private Label m_lblAnt8;
        private Label m_lblAnt7;
        private Label m_lblAnt6;
        private Panel panel1;
		#endregion Filter Variable 
		#endregion Private Members
		/// <summary>
		/// Required designer variable.
		/// </summary>
		
		#region Constructor
        public frmTest(IRFIDReader paramObjReader)
		{
			try
			{
				//
				// Required for Windows Form Designer support
				//
				m_ObjReader = paramObjReader;
                               
                m_ClsReader = new ClsReader(paramObjReader);
				InitializeComponent();

                reader_Model = m_ClsReader.GetModel().ToString() ;
                m_strReaderInfo = m_ClsReader.ReaderName();// +" (" + m_NotifiParam1 + ":" + m_ClsReader.GetNotificationPort() + ")";
                m_lblReaderInfo.Text = m_strReaderInfo + ": " + "Model:" + reader_Model + " (" +m_ClsReader.GetIPAddress() + ":" +  m_ClsReader.GetPort()+")";
				m_btnConnect.Enabled = false;
				m_btnDisconnect.Enabled = true;


                m_cboCmdList.Items.Add(m_StartAutoMode);
                m_cboCmdList.Items.Add(m_OnDemandMode);
                m_cboCmdList.Items.Add(m_GetTags);
                m_cboCmdList.Items.Add(m_WriteTagID);
                m_cboCmdList.Items.Add(m_GetIOStatus);
                m_cboCmdList.Items.Add(m_SetOutputStatus);
                m_cboCmdList.Items.Add(m_EnableInputStatusNotification);
                m_cboCmdList.Items.Add(m_DisableInputStatusNotification);
                m_cboCmdList.Items.Add(m_EnableProximitySensor);
                m_cboCmdList.Items.Add(m_DisableProximitySensor);
                m_cboCmdList.Items.Add(m_EnableMotionSensor);
                m_cboCmdList.Items.Add(m_DisableMotionSensor);
                m_cboCmdList.Items.Add(m_EnableRFIDModule);
                m_cboCmdList.Items.Add(m_DisableRFIDModule);
                m_cboCmdList.Items.Add(m_GetRFIDModuleStatus);
                //m_cboCmdList.Items.Add(m_EraseTags);
                //m_cboCmdList.Items.Add(m_KillTags);
                m_cboCmdList.Items.Add(m_LockTags);
                // VM: enable it when ready
                //m_cboCmdList.Items.Add(m_WriteTags);
                //m_cboCmdList.Items.Add(m_ReadTagData);
                //m_cboCmdList.Items.Add(m_WriteTags);
                m_cboCmdList.SelectedIndex = 0;
                //m_cboRepeatCount.SelectedIndex = 0; 
                m_ClsReader.appOnTagRead += new AppReaderEventHandler(InvokeUpdateListView);
                m_ClsReader.appOnPinStatusNotification += new AppReaderEventHandler(InvokeUpdateInputStatus);
			    m_ClsReader.appOnReaderManagement += new AppReaderEventHandler(InvokeUpdateStatusMonitor);
                m_ClsReader.appOnMotionSensorEvent += new AppReaderEventHandler(InvokeMotionSensorEvent);
                m_ClsReader.appOnProximitySensorEvent += new AppReaderEventHandler(InvokeProximitySensorEvent);
                m_ClsReader.appOnRFIDStatusMonitor += new AppReaderEventHandler(m_ClsReader_appOnRFIDStatusMonitor);
                
                try
                {
                    ReaderModel selectedReaderModel = (ReaderModel)Enum.Parse(typeof(ReaderModel), reader_Model);
                    if (selectedReaderModel == ReaderModel.XR480 || selectedReaderModel == ReaderModel.XR450 
                        || selectedReaderModel == ReaderModel.XR440 || selectedReaderModel == ReaderModel.XR400)
                    {
                        IOPinStatus[] inputPinsStatus = null;
                        IOPinStatus[] outputPinsStatus = null;

                        grpBxAntennaStatus.Visible = true;

                        if (selectedReaderModel == ReaderModel.XR480)
                        {
                            m_rdiobtnantena5.Visible = true;
                            m_lblAnt5.Visible = true;
                            m_rdiobtnantena6.Visible = true;
                            m_lblAnt6.Visible = true;
                            m_rdiobtnantena7.Visible = true;
                            m_lblAnt7.Visible = true;
                            m_rdiobtnantena8.Visible = true;
                            m_lblAnt8.Visible = true;
                        }

                        grpBxInpLineStatus.Visible = true;
                        grpbxSensors.Visible = false;
                        
                        m_ClsReader.GetPinlevels(out inputPinsStatus, out outputPinsStatus);
                        SetInputPinsDisplay(inputPinsStatus);
                    }

                    if ((ReaderModel)Enum.Parse(typeof(ReaderModel), reader_Model) == ReaderModel.RD5000)
                    {

                        grpBxAntennaStatus.Visible = false;
                        grpBxInpLineStatus.Visible = false;
                        grpbxSensors.Visible = true;
                       
                        
                    }   
                }
                catch { }

				
			}
			catch(Exception ex)
			{
               if(ex.InnerException!=null)
				    MessageBox.Show(ex.Message + " . \n " + ex.InnerException.Message  ,this.MdiParent.Text,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                else
                    MessageBox.Show(ex.Message, this.MdiParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			finally
			{
			}
		
		}

   		#endregion Constructor

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// 

		#region Dispose
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
                //m_ClsReader.Dispose();
                //m_ClsReader.DisConnect();
                
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion Dispose

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTest));
            this.m_gbxCommand = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_lblRFIDStatus = new System.Windows.Forms.Label();
            this.m_btnreaderCapability = new System.Windows.Forms.Button();
            this.m_btnReaderInfo = new System.Windows.Forms.Button();
            this.m_btnAntennaSetting = new System.Windows.Forms.Button();
            this.m_lblAutoPoll = new System.Windows.Forms.Label();
            this.m_lblReaderInfo = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grpbxSensors = new System.Windows.Forms.GroupBox();
            this.motionZProgBar = new System.Windows.Forms.ProgressBar();
            this.motionYProgBar = new System.Windows.Forms.ProgressBar();
            this.motionXProgBar = new System.Windows.Forms.ProgressBar();
            this.proximityProgBar = new System.Windows.Forms.ProgressBar();
            this.m_lblMotionZ = new System.Windows.Forms.Label();
            this.m_lblMotionY = new System.Windows.Forms.Label();
            this.m_lblMotionX = new System.Windows.Forms.Label();
            this.m_lblProximity = new System.Windows.Forms.Label();
            this.grpBxAntennaStatus = new System.Windows.Forms.GroupBox();
            this.m_lblAnt8 = new System.Windows.Forms.Label();
            this.m_lblAnt7 = new System.Windows.Forms.Label();
            this.m_lblAnt6 = new System.Windows.Forms.Label();
            this.m_lblAnt5 = new System.Windows.Forms.Label();
            this.m_rdiobtnantena8 = new System.Windows.Forms.RadioButton();
            this.m_rdiobtnantena7 = new System.Windows.Forms.RadioButton();
            this.m_rdiobtnantena6 = new System.Windows.Forms.RadioButton();
            this.m_rdiobtnantena5 = new System.Windows.Forms.RadioButton();
            this.m_lblAnt4 = new System.Windows.Forms.Label();
            this.m_lblAnt3 = new System.Windows.Forms.Label();
            this.m_lblAnt2 = new System.Windows.Forms.Label();
            this.m_lblAnt1 = new System.Windows.Forms.Label();
            this.m_rdiobtnantena4 = new System.Windows.Forms.RadioButton();
            this.m_rdiobtnantena3 = new System.Windows.Forms.RadioButton();
            this.m_rdiobtnantena2 = new System.Windows.Forms.RadioButton();
            this.m_rdiobtnantena1 = new System.Windows.Forms.RadioButton();
            this.grpBxInpLineStatus = new System.Windows.Forms.GroupBox();
            this.m_lblPin6 = new System.Windows.Forms.Label();
            this.m_lblPin5 = new System.Windows.Forms.Label();
            this.m_lblPin4 = new System.Windows.Forms.Label();
            this.m_lblPin3 = new System.Windows.Forms.Label();
            this.m_lblPin2 = new System.Windows.Forms.Label();
            this.m_lblPin1 = new System.Windows.Forms.Label();
            this.m_rbtnInpPin5 = new System.Windows.Forms.RadioButton();
            this.m_rbtnInpPin4 = new System.Windows.Forms.RadioButton();
            this.m_rbtnInpPin6 = new System.Windows.Forms.RadioButton();
            this.m_rbtnInpPin3 = new System.Windows.Forms.RadioButton();
            this.m_rbtnInpPin2 = new System.Windows.Forms.RadioButton();
            this.m_rbtnInpPin1 = new System.Windows.Forms.RadioButton();
            this.m_btnDisconnect = new System.Windows.Forms.Button();
            this.m_btnConnect = new System.Windows.Forms.Button();
            this.m_lblActive = new System.Windows.Forms.Label();
            this.m_lblStatus = new System.Windows.Forms.Label();
            this.m_lblInActive = new System.Windows.Forms.Label();
            this.m_gpBxParameters = new System.Windows.Forms.GroupBox();
            this.txtPointer = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWriteData = new System.Windows.Forms.Label();
            this.m_lbltimeintervalMS = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.m_btnClearResp = new System.Windows.Forms.Button();
            this.lblPointer = new System.Windows.Forms.Label();
            this.txtMemBank = new System.Windows.Forms.TextBox();
            this.m_lblNotification = new System.Windows.Forms.Label();
            this.lblMemoryBank = new System.Windows.Forms.Label();
            this.m_tbxNotification = new System.Windows.Forms.TextBox();
            this.txtWriteData = new System.Windows.Forms.TextBox();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_tbxtimeintervalMS = new System.Windows.Forms.TextBox();
            this.m_chkbxlstoutpins = new System.Windows.Forms.CheckedListBox();
            this.m_txtTagID = new System.Windows.Forms.TextBox();
            this.m_lblTagID = new System.Windows.Forms.Label();
            this.m_lblAntenna = new System.Windows.Forms.Label();
            this.m_cmbAntenna = new System.Windows.Forms.ComboBox();
            this.m_cboCmdList = new System.Windows.Forms.ComboBox();
            this.m_btnSend = new System.Windows.Forms.Button();
            this.m_tabCtlOutput = new System.Windows.Forms.TabControl();
            this.m_tabPgTableView = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.m_tabPgCmdOutput = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.CmdOutputColumn = new System.Windows.Forms.ColumnHeader();
            this.TagID = new System.Windows.Forms.ColumnHeader();
            this.TagType = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_gbxCommand.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpbxSensors.SuspendLayout();
            this.grpBxAntennaStatus.SuspendLayout();
            this.grpBxInpLineStatus.SuspendLayout();
            this.m_gpBxParameters.SuspendLayout();
            this.m_tabCtlOutput.SuspendLayout();
            this.m_tabPgTableView.SuspendLayout();
            this.m_tabPgCmdOutput.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_gbxCommand
            // 
            this.m_gbxCommand.BackColor = System.Drawing.SystemColors.Control;
            this.m_gbxCommand.Controls.Add(this.groupBox2);
            this.m_gbxCommand.Controls.Add(this.m_lblReaderInfo);
            this.m_gbxCommand.Controls.Add(this.groupBox3);
            this.m_gbxCommand.Controls.Add(this.m_gpBxParameters);
            this.m_gbxCommand.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_gbxCommand.Location = new System.Drawing.Point(0, 0);
            this.m_gbxCommand.Name = "m_gbxCommand";
            this.m_gbxCommand.Size = new System.Drawing.Size(859, 196);
            this.m_gbxCommand.TabIndex = 4;
            this.m_gbxCommand.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.m_lblRFIDStatus);
            this.groupBox2.Controls.Add(this.m_btnreaderCapability);
            this.groupBox2.Controls.Add(this.m_btnReaderInfo);
            this.groupBox2.Controls.Add(this.m_btnAntennaSetting);
            this.groupBox2.Controls.Add(this.m_lblAutoPoll);
            this.groupBox2.Location = new System.Drawing.Point(8, 146);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox2.Size = new System.Drawing.Size(618, 40);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // m_lblRFIDStatus
            // 
            this.m_lblRFIDStatus.BackColor = System.Drawing.Color.Tan;
            this.m_lblRFIDStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblRFIDStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblRFIDStatus.Location = new System.Drawing.Point(351, 10);
            this.m_lblRFIDStatus.Margin = new System.Windows.Forms.Padding(0);
            this.m_lblRFIDStatus.Name = "m_lblRFIDStatus";
            this.m_lblRFIDStatus.Size = new System.Drawing.Size(129, 25);
            this.m_lblRFIDStatus.TabIndex = 13;
            this.m_lblRFIDStatus.Text = "RFID Module : ACTIVE";
            this.m_lblRFIDStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_lblRFIDStatus.Click += new System.EventHandler(this.m_lblRFIDStatus_Click);
            // 
            // m_btnreaderCapability
            // 
            this.m_btnreaderCapability.BackColor = System.Drawing.Color.White;
            this.m_btnreaderCapability.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnreaderCapability.Location = new System.Drawing.Point(228, 10);
            this.m_btnreaderCapability.Margin = new System.Windows.Forms.Padding(0);
            this.m_btnreaderCapability.Name = "m_btnreaderCapability";
            this.m_btnreaderCapability.Size = new System.Drawing.Size(116, 25);
            this.m_btnreaderCapability.TabIndex = 12;
            this.m_btnreaderCapability.Text = "Reader Capabilities";
            this.m_btnreaderCapability.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_btnreaderCapability.UseVisualStyleBackColor = false;
            this.m_btnreaderCapability.Click += new System.EventHandler(this.m_btnreaderCapability_Click);
            // 
            // m_btnReaderInfo
            // 
            this.m_btnReaderInfo.BackColor = System.Drawing.Color.White;
            this.m_btnReaderInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnReaderInfo.Location = new System.Drawing.Point(5, 10);
            this.m_btnReaderInfo.Margin = new System.Windows.Forms.Padding(0);
            this.m_btnReaderInfo.Name = "m_btnReaderInfo";
            this.m_btnReaderInfo.Size = new System.Drawing.Size(107, 25);
            this.m_btnReaderInfo.TabIndex = 11;
            this.m_btnReaderInfo.Text = "Reader Info";
            this.m_btnReaderInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_btnReaderInfo.UseVisualStyleBackColor = false;
            this.m_btnReaderInfo.Click += new System.EventHandler(this.m_btnReaderInfo_Click);
            // 
            // m_btnAntennaSetting
            // 
            this.m_btnAntennaSetting.BackColor = System.Drawing.Color.White;
            this.m_btnAntennaSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnAntennaSetting.Location = new System.Drawing.Point(118, 10);
            this.m_btnAntennaSetting.Margin = new System.Windows.Forms.Padding(0);
            this.m_btnAntennaSetting.Name = "m_btnAntennaSetting";
            this.m_btnAntennaSetting.Size = new System.Drawing.Size(107, 25);
            this.m_btnAntennaSetting.TabIndex = 10;
            this.m_btnAntennaSetting.Text = "Antenna Settings";
            this.m_btnAntennaSetting.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.m_btnAntennaSetting.UseVisualStyleBackColor = false;
            this.m_btnAntennaSetting.Click += new System.EventHandler(this.m_btnAntennaSetting_Click);
            // 
            // m_lblAutoPoll
            // 
            this.m_lblAutoPoll.BackColor = System.Drawing.Color.Tan;
            this.m_lblAutoPoll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblAutoPoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblAutoPoll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_lblAutoPoll.Location = new System.Drawing.Point(484, 10);
            this.m_lblAutoPoll.Margin = new System.Windows.Forms.Padding(0);
            this.m_lblAutoPoll.Name = "m_lblAutoPoll";
            this.m_lblAutoPoll.Size = new System.Drawing.Size(128, 25);
            this.m_lblAutoPoll.TabIndex = 5;
            this.m_lblAutoPoll.Text = "Autonomous Mode : OFF";
            this.m_lblAutoPoll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lblReaderInfo
            // 
            this.m_lblReaderInfo.AutoSize = true;
            this.m_lblReaderInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_lblReaderInfo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblReaderInfo.Location = new System.Drawing.Point(3, 16);
            this.m_lblReaderInfo.Name = "m_lblReaderInfo";
            this.m_lblReaderInfo.Size = new System.Drawing.Size(103, 19);
            this.m_lblReaderInfo.TabIndex = 7;
            this.m_lblReaderInfo.Text = "Reader Name";
            this.m_lblReaderInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.grpBxAntennaStatus);
            this.groupBox3.Controls.Add(this.grpbxSensors);
            this.groupBox3.Controls.Add(this.grpBxInpLineStatus);
            this.groupBox3.Controls.Add(this.m_btnDisconnect);
            this.groupBox3.Controls.Add(this.m_btnConnect);
            this.groupBox3.Controls.Add(this.m_lblActive);
            this.groupBox3.Controls.Add(this.m_lblStatus);
            this.groupBox3.Controls.Add(this.m_lblInActive);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(626, 38);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox3.Size = new System.Drawing.Size(230, 148);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            // 
            // grpbxSensors
            // 
            this.grpbxSensors.BackColor = System.Drawing.SystemColors.Control;
            this.grpbxSensors.Controls.Add(this.motionZProgBar);
            this.grpbxSensors.Controls.Add(this.motionYProgBar);
            this.grpbxSensors.Controls.Add(this.motionXProgBar);
            this.grpbxSensors.Controls.Add(this.proximityProgBar);
            this.grpbxSensors.Controls.Add(this.m_lblMotionZ);
            this.grpbxSensors.Controls.Add(this.m_lblMotionY);
            this.grpbxSensors.Controls.Add(this.m_lblMotionX);
            this.grpbxSensors.Controls.Add(this.m_lblProximity);
            this.grpbxSensors.Location = new System.Drawing.Point(3, 61);
            this.grpbxSensors.Margin = new System.Windows.Forms.Padding(0);
            this.grpbxSensors.Name = "grpbxSensors";
            this.grpbxSensors.Padding = new System.Windows.Forms.Padding(0);
            this.grpbxSensors.Size = new System.Drawing.Size(221, 83);
            this.grpbxSensors.TabIndex = 13;
            this.grpbxSensors.TabStop = false;
            this.grpbxSensors.Text = "Sensors";
            // 
            // motionZProgBar
            // 
            this.motionZProgBar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.motionZProgBar.ForeColor = System.Drawing.Color.Indigo;
            this.motionZProgBar.Location = new System.Drawing.Point(108, 65);
            this.motionZProgBar.Margin = new System.Windows.Forms.Padding(0);
            this.motionZProgBar.Maximum = 5000;
            this.motionZProgBar.Name = "motionZProgBar";
            this.motionZProgBar.Size = new System.Drawing.Size(105, 10);
            this.motionZProgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.motionZProgBar.TabIndex = 7;
            // 
            // motionYProgBar
            // 
            this.motionYProgBar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.motionYProgBar.ForeColor = System.Drawing.Color.Indigo;
            this.motionYProgBar.Location = new System.Drawing.Point(108, 50);
            this.motionYProgBar.Margin = new System.Windows.Forms.Padding(0);
            this.motionYProgBar.Maximum = 5000;
            this.motionYProgBar.Name = "motionYProgBar";
            this.motionYProgBar.Size = new System.Drawing.Size(105, 10);
            this.motionYProgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.motionYProgBar.TabIndex = 6;
            // 
            // motionXProgBar
            // 
            this.motionXProgBar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.motionXProgBar.ForeColor = System.Drawing.Color.Indigo;
            this.motionXProgBar.Location = new System.Drawing.Point(108, 32);
            this.motionXProgBar.Margin = new System.Windows.Forms.Padding(0);
            this.motionXProgBar.Maximum = 5000;
            this.motionXProgBar.Name = "motionXProgBar";
            this.motionXProgBar.Size = new System.Drawing.Size(105, 10);
            this.motionXProgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.motionXProgBar.TabIndex = 5;
            // 
            // proximityProgBar
            // 
            this.proximityProgBar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.proximityProgBar.ForeColor = System.Drawing.Color.Navy;
            this.proximityProgBar.Location = new System.Drawing.Point(108, 16);
            this.proximityProgBar.Margin = new System.Windows.Forms.Padding(0);
            this.proximityProgBar.Maximum = 255;
            this.proximityProgBar.Name = "proximityProgBar";
            this.proximityProgBar.Size = new System.Drawing.Size(105, 10);
            this.proximityProgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.proximityProgBar.TabIndex = 4;
            // 
            // m_lblMotionZ
            // 
            this.m_lblMotionZ.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblMotionZ.Enabled = false;
            this.m_lblMotionZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblMotionZ.ForeColor = System.Drawing.Color.Black;
            this.m_lblMotionZ.Location = new System.Drawing.Point(11, 65);
            this.m_lblMotionZ.Margin = new System.Windows.Forms.Padding(0);
            this.m_lblMotionZ.Name = "m_lblMotionZ";
            this.m_lblMotionZ.Size = new System.Drawing.Size(91, 12);
            this.m_lblMotionZ.TabIndex = 3;
            this.m_lblMotionZ.Text = "Motion Z  : ";
            // 
            // m_lblMotionY
            // 
            this.m_lblMotionY.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblMotionY.Enabled = false;
            this.m_lblMotionY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblMotionY.ForeColor = System.Drawing.Color.Black;
            this.m_lblMotionY.Location = new System.Drawing.Point(11, 48);
            this.m_lblMotionY.Margin = new System.Windows.Forms.Padding(0);
            this.m_lblMotionY.Name = "m_lblMotionY";
            this.m_lblMotionY.Size = new System.Drawing.Size(91, 12);
            this.m_lblMotionY.TabIndex = 2;
            this.m_lblMotionY.Text = "Motion Y  : ";
            // 
            // m_lblMotionX
            // 
            this.m_lblMotionX.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblMotionX.Enabled = false;
            this.m_lblMotionX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblMotionX.ForeColor = System.Drawing.Color.Black;
            this.m_lblMotionX.Location = new System.Drawing.Point(11, 31);
            this.m_lblMotionX.Margin = new System.Windows.Forms.Padding(0);
            this.m_lblMotionX.Name = "m_lblMotionX";
            this.m_lblMotionX.Size = new System.Drawing.Size(91, 12);
            this.m_lblMotionX.TabIndex = 1;
            this.m_lblMotionX.Text = "Motion X  : ";
            // 
            // m_lblProximity
            // 
            this.m_lblProximity.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblProximity.Enabled = false;
            this.m_lblProximity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblProximity.ForeColor = System.Drawing.Color.Black;
            this.m_lblProximity.Location = new System.Drawing.Point(12, 14);
            this.m_lblProximity.Margin = new System.Windows.Forms.Padding(0);
            this.m_lblProximity.Name = "m_lblProximity";
            this.m_lblProximity.Size = new System.Drawing.Size(91, 12);
            this.m_lblProximity.TabIndex = 0;
            this.m_lblProximity.Text = "Proximity  :  ";
            // 
            // grpBxAntennaStatus
            // 
            this.grpBxAntennaStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grpBxAntennaStatus.Controls.Add(this.m_lblAnt8);
            this.grpBxAntennaStatus.Controls.Add(this.m_lblAnt7);
            this.grpBxAntennaStatus.Controls.Add(this.m_lblAnt6);
            this.grpBxAntennaStatus.Controls.Add(this.m_lblAnt5);
            this.grpBxAntennaStatus.Controls.Add(this.m_rdiobtnantena8);
            this.grpBxAntennaStatus.Controls.Add(this.m_rdiobtnantena7);
            this.grpBxAntennaStatus.Controls.Add(this.m_rdiobtnantena6);
            this.grpBxAntennaStatus.Controls.Add(this.m_rdiobtnantena5);
            this.grpBxAntennaStatus.Controls.Add(this.m_lblAnt4);
            this.grpBxAntennaStatus.Controls.Add(this.m_lblAnt3);
            this.grpBxAntennaStatus.Controls.Add(this.m_lblAnt2);
            this.grpBxAntennaStatus.Controls.Add(this.m_lblAnt1);
            this.grpBxAntennaStatus.Controls.Add(this.m_rdiobtnantena4);
            this.grpBxAntennaStatus.Controls.Add(this.m_rdiobtnantena3);
            this.grpBxAntennaStatus.Controls.Add(this.m_rdiobtnantena2);
            this.grpBxAntennaStatus.Controls.Add(this.m_rdiobtnantena1);
            this.grpBxAntennaStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBxAntennaStatus.Location = new System.Drawing.Point(15, 63);
            this.grpBxAntennaStatus.Margin = new System.Windows.Forms.Padding(0);
            this.grpBxAntennaStatus.Name = "grpBxAntennaStatus";
            this.grpBxAntennaStatus.Padding = new System.Windows.Forms.Padding(0);
            this.grpBxAntennaStatus.Size = new System.Drawing.Size(207, 40);
            this.grpBxAntennaStatus.TabIndex = 10;
            this.grpBxAntennaStatus.TabStop = false;
            this.grpBxAntennaStatus.Text = "Antenna Status";
            // 
            // m_lblAnt8
            // 
            this.m_lblAnt8.AutoSize = true;
            this.m_lblAnt8.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblAnt8.Location = new System.Drawing.Point(163, 22);
            this.m_lblAnt8.Name = "m_lblAnt8";
            this.m_lblAnt8.Size = new System.Drawing.Size(17, 12);
            this.m_lblAnt8.TabIndex = 25;
            this.m_lblAnt8.Text = "A8";
            this.m_lblAnt8.Visible = false;
            // 
            // m_lblAnt7
            // 
            this.m_lblAnt7.AutoSize = true;
            this.m_lblAnt7.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblAnt7.Location = new System.Drawing.Point(142, 22);
            this.m_lblAnt7.Name = "m_lblAnt7";
            this.m_lblAnt7.Size = new System.Drawing.Size(17, 12);
            this.m_lblAnt7.TabIndex = 24;
            this.m_lblAnt7.Text = "A7";
            this.m_lblAnt7.Visible = false;
            // 
            // m_lblAnt6
            // 
            this.m_lblAnt6.AutoSize = true;
            this.m_lblAnt6.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblAnt6.Location = new System.Drawing.Point(119, 22);
            this.m_lblAnt6.Name = "m_lblAnt6";
            this.m_lblAnt6.Size = new System.Drawing.Size(17, 12);
            this.m_lblAnt6.TabIndex = 23;
            this.m_lblAnt6.Text = "A6";
            this.m_lblAnt6.Visible = false;
            // 
            // m_lblAnt5
            // 
            this.m_lblAnt5.AutoSize = true;
            this.m_lblAnt5.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblAnt5.Location = new System.Drawing.Point(97, 22);
            this.m_lblAnt5.Name = "m_lblAnt5";
            this.m_lblAnt5.Size = new System.Drawing.Size(17, 12);
            this.m_lblAnt5.TabIndex = 22;
            this.m_lblAnt5.Text = "A5";
            this.m_lblAnt5.Visible = false;
            // 
            // m_rdiobtnantena8
            // 
            this.m_rdiobtnantena8.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rdiobtnantena8.AutoSize = true;
            this.m_rdiobtnantena8.BackColor = System.Drawing.Color.Red;
            this.m_rdiobtnantena8.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_rdiobtnantena8.Location = new System.Drawing.Point(168, 15);
            this.m_rdiobtnantena8.Name = "m_rdiobtnantena8";
            this.m_rdiobtnantena8.Size = new System.Drawing.Size(6, 6);
            this.m_rdiobtnantena8.TabIndex = 21;
            this.m_rdiobtnantena8.UseVisualStyleBackColor = false;
            this.m_rdiobtnantena8.Visible = false;
            // 
            // m_rdiobtnantena7
            // 
            this.m_rdiobtnantena7.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rdiobtnantena7.AutoSize = true;
            this.m_rdiobtnantena7.BackColor = System.Drawing.Color.Red;
            this.m_rdiobtnantena7.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_rdiobtnantena7.Location = new System.Drawing.Point(146, 15);
            this.m_rdiobtnantena7.Name = "m_rdiobtnantena7";
            this.m_rdiobtnantena7.Size = new System.Drawing.Size(6, 6);
            this.m_rdiobtnantena7.TabIndex = 20;
            this.m_rdiobtnantena7.UseVisualStyleBackColor = false;
            this.m_rdiobtnantena7.Visible = false;
            // 
            // m_rdiobtnantena6
            // 
            this.m_rdiobtnantena6.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rdiobtnantena6.AutoSize = true;
            this.m_rdiobtnantena6.BackColor = System.Drawing.Color.Red;
            this.m_rdiobtnantena6.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_rdiobtnantena6.Location = new System.Drawing.Point(124, 15);
            this.m_rdiobtnantena6.Name = "m_rdiobtnantena6";
            this.m_rdiobtnantena6.Size = new System.Drawing.Size(6, 6);
            this.m_rdiobtnantena6.TabIndex = 19;
            this.m_rdiobtnantena6.UseVisualStyleBackColor = false;
            this.m_rdiobtnantena6.Visible = false;
            // 
            // m_rdiobtnantena5
            // 
            this.m_rdiobtnantena5.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rdiobtnantena5.AutoSize = true;
            this.m_rdiobtnantena5.BackColor = System.Drawing.Color.Red;
            this.m_rdiobtnantena5.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_rdiobtnantena5.Location = new System.Drawing.Point(102, 15);
            this.m_rdiobtnantena5.Name = "m_rdiobtnantena5";
            this.m_rdiobtnantena5.Size = new System.Drawing.Size(6, 6);
            this.m_rdiobtnantena5.TabIndex = 18;
            this.m_rdiobtnantena5.UseVisualStyleBackColor = false;
            this.m_rdiobtnantena5.Visible = false;
            // 
            // m_lblAnt4
            // 
            this.m_lblAnt4.AutoSize = true;
            this.m_lblAnt4.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblAnt4.Location = new System.Drawing.Point(75, 22);
            this.m_lblAnt4.Name = "m_lblAnt4";
            this.m_lblAnt4.Size = new System.Drawing.Size(17, 12);
            this.m_lblAnt4.TabIndex = 17;
            this.m_lblAnt4.Text = "A4";
            // 
            // m_lblAnt3
            // 
            this.m_lblAnt3.AutoSize = true;
            this.m_lblAnt3.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblAnt3.Location = new System.Drawing.Point(53, 22);
            this.m_lblAnt3.Name = "m_lblAnt3";
            this.m_lblAnt3.Size = new System.Drawing.Size(17, 12);
            this.m_lblAnt3.TabIndex = 16;
            this.m_lblAnt3.Text = "A3";
            // 
            // m_lblAnt2
            // 
            this.m_lblAnt2.AutoSize = true;
            this.m_lblAnt2.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblAnt2.Location = new System.Drawing.Point(31, 22);
            this.m_lblAnt2.Name = "m_lblAnt2";
            this.m_lblAnt2.Size = new System.Drawing.Size(17, 12);
            this.m_lblAnt2.TabIndex = 15;
            this.m_lblAnt2.Text = "A2";
            // 
            // m_lblAnt1
            // 
            this.m_lblAnt1.AutoSize = true;
            this.m_lblAnt1.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblAnt1.Location = new System.Drawing.Point(9, 22);
            this.m_lblAnt1.Margin = new System.Windows.Forms.Padding(0);
            this.m_lblAnt1.Name = "m_lblAnt1";
            this.m_lblAnt1.Size = new System.Drawing.Size(17, 12);
            this.m_lblAnt1.TabIndex = 14;
            this.m_lblAnt1.Text = "A1";
            // 
            // m_rdiobtnantena4
            // 
            this.m_rdiobtnantena4.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rdiobtnantena4.AutoSize = true;
            this.m_rdiobtnantena4.BackColor = System.Drawing.Color.Red;
            this.m_rdiobtnantena4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_rdiobtnantena4.Location = new System.Drawing.Point(80, 15);
            this.m_rdiobtnantena4.Name = "m_rdiobtnantena4";
            this.m_rdiobtnantena4.Size = new System.Drawing.Size(6, 6);
            this.m_rdiobtnantena4.TabIndex = 13;
            this.m_rdiobtnantena4.UseVisualStyleBackColor = false;
            // 
            // m_rdiobtnantena3
            // 
            this.m_rdiobtnantena3.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rdiobtnantena3.AutoSize = true;
            this.m_rdiobtnantena3.BackColor = System.Drawing.Color.Red;
            this.m_rdiobtnantena3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_rdiobtnantena3.Location = new System.Drawing.Point(58, 15);
            this.m_rdiobtnantena3.Name = "m_rdiobtnantena3";
            this.m_rdiobtnantena3.Size = new System.Drawing.Size(6, 6);
            this.m_rdiobtnantena3.TabIndex = 12;
            this.m_rdiobtnantena3.UseVisualStyleBackColor = false;
            // 
            // m_rdiobtnantena2
            // 
            this.m_rdiobtnantena2.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rdiobtnantena2.AutoSize = true;
            this.m_rdiobtnantena2.BackColor = System.Drawing.Color.Red;
            this.m_rdiobtnantena2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_rdiobtnantena2.Location = new System.Drawing.Point(36, 15);
            this.m_rdiobtnantena2.Name = "m_rdiobtnantena2";
            this.m_rdiobtnantena2.Size = new System.Drawing.Size(6, 6);
            this.m_rdiobtnantena2.TabIndex = 11;
            this.m_rdiobtnantena2.UseVisualStyleBackColor = false;
            // 
            // m_rdiobtnantena1
            // 
            this.m_rdiobtnantena1.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rdiobtnantena1.AutoSize = true;
            this.m_rdiobtnantena1.BackColor = System.Drawing.Color.Red;
            this.m_rdiobtnantena1.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_rdiobtnantena1.Location = new System.Drawing.Point(14, 15);
            this.m_rdiobtnantena1.Name = "m_rdiobtnantena1";
            this.m_rdiobtnantena1.Size = new System.Drawing.Size(6, 6);
            this.m_rdiobtnantena1.TabIndex = 10;
            this.m_rdiobtnantena1.UseVisualStyleBackColor = false;
            // 
            // grpBxInpLineStatus
            // 
            this.grpBxInpLineStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grpBxInpLineStatus.Controls.Add(this.m_lblPin6);
            this.grpBxInpLineStatus.Controls.Add(this.m_lblPin5);
            this.grpBxInpLineStatus.Controls.Add(this.m_lblPin4);
            this.grpBxInpLineStatus.Controls.Add(this.m_lblPin3);
            this.grpBxInpLineStatus.Controls.Add(this.m_lblPin2);
            this.grpBxInpLineStatus.Controls.Add(this.m_lblPin1);
            this.grpBxInpLineStatus.Controls.Add(this.m_rbtnInpPin5);
            this.grpBxInpLineStatus.Controls.Add(this.m_rbtnInpPin4);
            this.grpBxInpLineStatus.Controls.Add(this.m_rbtnInpPin6);
            this.grpBxInpLineStatus.Controls.Add(this.m_rbtnInpPin3);
            this.grpBxInpLineStatus.Controls.Add(this.m_rbtnInpPin2);
            this.grpBxInpLineStatus.Controls.Add(this.m_rbtnInpPin1);
            this.grpBxInpLineStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpBxInpLineStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBxInpLineStatus.Location = new System.Drawing.Point(15, 105);
            this.grpBxInpLineStatus.Margin = new System.Windows.Forms.Padding(0);
            this.grpBxInpLineStatus.Name = "grpBxInpLineStatus";
            this.grpBxInpLineStatus.Padding = new System.Windows.Forms.Padding(0);
            this.grpBxInpLineStatus.Size = new System.Drawing.Size(207, 40);
            this.grpBxInpLineStatus.TabIndex = 9;
            this.grpBxInpLineStatus.TabStop = false;
            this.grpBxInpLineStatus.Text = "Input Line Status";
            // 
            // m_lblPin6
            // 
            this.m_lblPin6.AutoSize = true;
            this.m_lblPin6.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblPin6.Location = new System.Drawing.Point(182, 22);
            this.m_lblPin6.Margin = new System.Windows.Forms.Padding(1);
            this.m_lblPin6.Name = "m_lblPin6";
            this.m_lblPin6.Size = new System.Drawing.Size(16, 12);
            this.m_lblPin6.TabIndex = 11;
            this.m_lblPin6.Text = "I 5";
            // 
            // m_lblPin5
            // 
            this.m_lblPin5.AutoSize = true;
            this.m_lblPin5.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblPin5.Location = new System.Drawing.Point(147, 22);
            this.m_lblPin5.Margin = new System.Windows.Forms.Padding(1);
            this.m_lblPin5.Name = "m_lblPin5";
            this.m_lblPin5.Size = new System.Drawing.Size(16, 12);
            this.m_lblPin5.TabIndex = 10;
            this.m_lblPin5.Text = "I 4";
            // 
            // m_lblPin4
            // 
            this.m_lblPin4.AutoSize = true;
            this.m_lblPin4.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblPin4.Location = new System.Drawing.Point(113, 22);
            this.m_lblPin4.Margin = new System.Windows.Forms.Padding(1);
            this.m_lblPin4.Name = "m_lblPin4";
            this.m_lblPin4.Size = new System.Drawing.Size(16, 12);
            this.m_lblPin4.TabIndex = 9;
            this.m_lblPin4.Text = "I 3";
            // 
            // m_lblPin3
            // 
            this.m_lblPin3.AutoSize = true;
            this.m_lblPin3.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblPin3.Location = new System.Drawing.Point(78, 22);
            this.m_lblPin3.Margin = new System.Windows.Forms.Padding(1);
            this.m_lblPin3.Name = "m_lblPin3";
            this.m_lblPin3.Size = new System.Drawing.Size(16, 12);
            this.m_lblPin3.TabIndex = 8;
            this.m_lblPin3.Text = "I 2";
            // 
            // m_lblPin2
            // 
            this.m_lblPin2.AutoSize = true;
            this.m_lblPin2.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblPin2.Location = new System.Drawing.Point(42, 22);
            this.m_lblPin2.Margin = new System.Windows.Forms.Padding(1);
            this.m_lblPin2.Name = "m_lblPin2";
            this.m_lblPin2.Size = new System.Drawing.Size(16, 12);
            this.m_lblPin2.TabIndex = 7;
            this.m_lblPin2.Text = "I 1";
            // 
            // m_lblPin1
            // 
            this.m_lblPin1.AutoSize = true;
            this.m_lblPin1.Font = new System.Drawing.Font("Times New Roman", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblPin1.Location = new System.Drawing.Point(9, 22);
            this.m_lblPin1.Margin = new System.Windows.Forms.Padding(1);
            this.m_lblPin1.Name = "m_lblPin1";
            this.m_lblPin1.Size = new System.Drawing.Size(16, 12);
            this.m_lblPin1.TabIndex = 6;
            this.m_lblPin1.Text = "I 0";
            // 
            // m_rbtnInpPin5
            // 
            this.m_rbtnInpPin5.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rbtnInpPin5.AutoSize = true;
            this.m_rbtnInpPin5.BackColor = System.Drawing.Color.Red;
            this.m_rbtnInpPin5.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_rbtnInpPin5.Location = new System.Drawing.Point(151, 15);
            this.m_rbtnInpPin5.Name = "m_rbtnInpPin5";
            this.m_rbtnInpPin5.Size = new System.Drawing.Size(6, 6);
            this.m_rbtnInpPin5.TabIndex = 5;
            this.m_rbtnInpPin5.UseVisualStyleBackColor = false;
            // 
            // m_rbtnInpPin4
            // 
            this.m_rbtnInpPin4.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rbtnInpPin4.AutoSize = true;
            this.m_rbtnInpPin4.BackColor = System.Drawing.Color.Red;
            this.m_rbtnInpPin4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_rbtnInpPin4.Location = new System.Drawing.Point(116, 15);
            this.m_rbtnInpPin4.Name = "m_rbtnInpPin4";
            this.m_rbtnInpPin4.Size = new System.Drawing.Size(6, 6);
            this.m_rbtnInpPin4.TabIndex = 4;
            this.m_rbtnInpPin4.UseVisualStyleBackColor = false;
            // 
            // m_rbtnInpPin6
            // 
            this.m_rbtnInpPin6.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rbtnInpPin6.AutoSize = true;
            this.m_rbtnInpPin6.BackColor = System.Drawing.Color.Red;
            this.m_rbtnInpPin6.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_rbtnInpPin6.Location = new System.Drawing.Point(186, 15);
            this.m_rbtnInpPin6.Name = "m_rbtnInpPin6";
            this.m_rbtnInpPin6.Size = new System.Drawing.Size(6, 6);
            this.m_rbtnInpPin6.TabIndex = 3;
            this.m_rbtnInpPin6.UseVisualStyleBackColor = false;
            // 
            // m_rbtnInpPin3
            // 
            this.m_rbtnInpPin3.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rbtnInpPin3.AutoSize = true;
            this.m_rbtnInpPin3.BackColor = System.Drawing.Color.Red;
            this.m_rbtnInpPin3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_rbtnInpPin3.Location = new System.Drawing.Point(81, 15);
            this.m_rbtnInpPin3.Name = "m_rbtnInpPin3";
            this.m_rbtnInpPin3.Size = new System.Drawing.Size(6, 6);
            this.m_rbtnInpPin3.TabIndex = 2;
            this.m_rbtnInpPin3.UseVisualStyleBackColor = false;
            // 
            // m_rbtnInpPin2
            // 
            this.m_rbtnInpPin2.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rbtnInpPin2.AutoSize = true;
            this.m_rbtnInpPin2.BackColor = System.Drawing.Color.Red;
            this.m_rbtnInpPin2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_rbtnInpPin2.Location = new System.Drawing.Point(46, 15);
            this.m_rbtnInpPin2.Name = "m_rbtnInpPin2";
            this.m_rbtnInpPin2.Size = new System.Drawing.Size(6, 6);
            this.m_rbtnInpPin2.TabIndex = 1;
            this.m_rbtnInpPin2.UseVisualStyleBackColor = false;
            // 
            // m_rbtnInpPin1
            // 
            this.m_rbtnInpPin1.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_rbtnInpPin1.AutoSize = true;
            this.m_rbtnInpPin1.BackColor = System.Drawing.Color.Red;
            this.m_rbtnInpPin1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_rbtnInpPin1.Location = new System.Drawing.Point(11, 15);
            this.m_rbtnInpPin1.Name = "m_rbtnInpPin1";
            this.m_rbtnInpPin1.Size = new System.Drawing.Size(6, 6);
            this.m_rbtnInpPin1.TabIndex = 0;
            this.m_rbtnInpPin1.UseVisualStyleBackColor = false;
            // 
            // m_btnDisconnect
            // 
            this.m_btnDisconnect.BackColor = System.Drawing.Color.White;
            this.m_btnDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("m_btnDisconnect.Image")));
            this.m_btnDisconnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnDisconnect.Location = new System.Drawing.Point(120, 35);
            this.m_btnDisconnect.Name = "m_btnDisconnect";
            this.m_btnDisconnect.Size = new System.Drawing.Size(102, 23);
            this.m_btnDisconnect.TabIndex = 8;
            this.m_btnDisconnect.Text = "Disconnect";
            this.m_btnDisconnect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnDisconnect.UseVisualStyleBackColor = true;
            this.m_btnDisconnect.Click += new System.EventHandler(this.m_btnDisconnect_Click);
            // 
            // m_btnConnect
            // 
            this.m_btnConnect.BackColor = System.Drawing.Color.White;
            this.m_btnConnect.Image = ((System.Drawing.Image)(resources.GetObject("m_btnConnect.Image")));
            this.m_btnConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnConnect.Location = new System.Drawing.Point(15, 35);
            this.m_btnConnect.Name = "m_btnConnect";
            this.m_btnConnect.Size = new System.Drawing.Size(102, 23);
            this.m_btnConnect.TabIndex = 7;
            this.m_btnConnect.Text = "Connect";
            this.m_btnConnect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnConnect.UseVisualStyleBackColor = false;
            this.m_btnConnect.Click += new System.EventHandler(this.m_btnConnect_Click);
            // 
            // m_lblActive
            // 
            this.m_lblActive.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.m_lblActive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblActive.Location = new System.Drawing.Point(84, 10);
            this.m_lblActive.Name = "m_lblActive";
            this.m_lblActive.Size = new System.Drawing.Size(138, 22);
            this.m_lblActive.TabIndex = 0;
            this.m_lblActive.Text = "ACTIVE";
            this.m_lblActive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lblStatus
            // 
            this.m_lblStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.m_lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblStatus.Location = new System.Drawing.Point(15, 10);
            this.m_lblStatus.Name = "m_lblStatus";
            this.m_lblStatus.Size = new System.Drawing.Size(66, 22);
            this.m_lblStatus.TabIndex = 1;
            this.m_lblStatus.Text = "Status";
            this.m_lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lblInActive
            // 
            this.m_lblInActive.BackColor = System.Drawing.Color.Red;
            this.m_lblInActive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblInActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblInActive.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_lblInActive.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lblInActive.Location = new System.Drawing.Point(84, 10);
            this.m_lblInActive.Name = "m_lblInActive";
            this.m_lblInActive.Size = new System.Drawing.Size(138, 22);
            this.m_lblInActive.TabIndex = 2;
            this.m_lblInActive.Text = "INACTIVE";
            this.m_lblInActive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_gpBxParameters
            // 
            this.m_gpBxParameters.BackColor = System.Drawing.SystemColors.Control;
            this.m_gpBxParameters.Controls.Add(this.txtPointer);
            this.m_gpBxParameters.Controls.Add(this.lblCount);
            this.m_gpBxParameters.Controls.Add(this.label1);
            this.m_gpBxParameters.Controls.Add(this.lblWriteData);
            this.m_gpBxParameters.Controls.Add(this.m_lbltimeintervalMS);
            this.m_gpBxParameters.Controls.Add(this.txtCount);
            this.m_gpBxParameters.Controls.Add(this.m_btnClearResp);
            this.m_gpBxParameters.Controls.Add(this.lblPointer);
            this.m_gpBxParameters.Controls.Add(this.txtMemBank);
            this.m_gpBxParameters.Controls.Add(this.m_lblNotification);
            this.m_gpBxParameters.Controls.Add(this.lblMemoryBank);
            this.m_gpBxParameters.Controls.Add(this.m_tbxNotification);
            this.m_gpBxParameters.Controls.Add(this.txtWriteData);
            this.m_gpBxParameters.Controls.Add(this.m_btnCancel);
            this.m_gpBxParameters.Controls.Add(this.m_tbxtimeintervalMS);
            this.m_gpBxParameters.Controls.Add(this.m_chkbxlstoutpins);
            this.m_gpBxParameters.Controls.Add(this.m_txtTagID);
            this.m_gpBxParameters.Controls.Add(this.m_lblTagID);
            this.m_gpBxParameters.Controls.Add(this.m_lblAntenna);
            this.m_gpBxParameters.Controls.Add(this.m_cmbAntenna);
            this.m_gpBxParameters.Controls.Add(this.m_cboCmdList);
            this.m_gpBxParameters.Controls.Add(this.m_btnSend);
            this.m_gpBxParameters.Location = new System.Drawing.Point(8, 38);
            this.m_gpBxParameters.Margin = new System.Windows.Forms.Padding(0);
            this.m_gpBxParameters.Name = "m_gpBxParameters";
            this.m_gpBxParameters.Padding = new System.Windows.Forms.Padding(1);
            this.m_gpBxParameters.Size = new System.Drawing.Size(618, 109);
            this.m_gpBxParameters.TabIndex = 0;
            this.m_gpBxParameters.TabStop = false;
            this.m_gpBxParameters.Text = "Command ";
            // 
            // txtPointer
            // 
            this.txtPointer.Location = new System.Drawing.Point(419, 42);
            this.txtPointer.Name = "txtPointer";
            this.txtPointer.Size = new System.Drawing.Size(47, 20);
            this.txtPointer.TabIndex = 8;
            this.txtPointer.Visible = false;
            // 
            // lblCount
            // 
            this.lblCount.Location = new System.Drawing.Point(322, 67);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(86, 21);
            this.lblCount.TabIndex = 8;
            this.lblCount.Text = "Word Count";
            this.lblCount.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(322, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "Word Pointer";
            this.label1.Visible = false;
            // 
            // lblWriteData
            // 
            this.lblWriteData.Location = new System.Drawing.Point(322, 70);
            this.lblWriteData.Name = "lblWriteData";
            this.lblWriteData.Size = new System.Drawing.Size(72, 21);
            this.lblWriteData.TabIndex = 9;
            this.lblWriteData.Text = "Write Data";
            this.lblWriteData.Visible = false;
            // 
            // m_lbltimeintervalMS
            // 
            this.m_lbltimeintervalMS.Location = new System.Drawing.Point(304, 40);
            this.m_lbltimeintervalMS.Margin = new System.Windows.Forms.Padding(0);
            this.m_lbltimeintervalMS.Name = "m_lbltimeintervalMS";
            this.m_lbltimeintervalMS.Size = new System.Drawing.Size(131, 18);
            this.m_lbltimeintervalMS.TabIndex = 13;
            this.m_lbltimeintervalMS.Text = "Time Interval(millisecs)";
            this.m_lbltimeintervalMS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_lbltimeintervalMS.Visible = false;
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(419, 70);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(47, 20);
            this.txtCount.TabIndex = 14;
            this.txtCount.Visible = false;
            // 
            // m_btnClearResp
            // 
            this.m_btnClearResp.BackColor = System.Drawing.Color.White;
            this.m_btnClearResp.Image = ((System.Drawing.Image)(resources.GetObject("m_btnClearResp.Image")));
            this.m_btnClearResp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnClearResp.Location = new System.Drawing.Point(147, 68);
            this.m_btnClearResp.Margin = new System.Windows.Forms.Padding(0);
            this.m_btnClearResp.Name = "m_btnClearResp";
            this.m_btnClearResp.Size = new System.Drawing.Size(125, 25);
            this.m_btnClearResp.TabIndex = 9;
            this.m_btnClearResp.Text = "Clear Response";
            this.m_btnClearResp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnClearResp.UseVisualStyleBackColor = false;
            this.m_btnClearResp.Click += new System.EventHandler(this.m_btnClearResp_Click);
            // 
            // lblPointer
            // 
            this.lblPointer.Location = new System.Drawing.Point(322, 39);
            this.lblPointer.Name = "lblPointer";
            this.lblPointer.Size = new System.Drawing.Size(86, 21);
            this.lblPointer.TabIndex = 13;
            this.lblPointer.Text = "Word Pointer";
            this.lblPointer.Visible = false;
            // 
            // txtMemBank
            // 
            this.txtMemBank.Location = new System.Drawing.Point(572, 39);
            this.txtMemBank.Name = "txtMemBank";
            this.txtMemBank.Size = new System.Drawing.Size(37, 20);
            this.txtMemBank.TabIndex = 12;
            this.txtMemBank.Visible = false;
            // 
            // m_lblNotification
            // 
            this.m_lblNotification.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblNotification.Location = new System.Drawing.Point(278, 38);
            this.m_lblNotification.Margin = new System.Windows.Forms.Padding(0);
            this.m_lblNotification.Name = "m_lblNotification";
            this.m_lblNotification.Size = new System.Drawing.Size(158, 27);
            this.m_lblNotification.TabIndex = 13;
            this.m_lblNotification.Text = "Notification Interval(millisecs)";
            this.m_lblNotification.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_lblNotification.Visible = false;
            // 
            // lblMemoryBank
            // 
            this.lblMemoryBank.Location = new System.Drawing.Point(482, 39);
            this.lblMemoryBank.Name = "lblMemoryBank";
            this.lblMemoryBank.Size = new System.Drawing.Size(83, 21);
            this.lblMemoryBank.TabIndex = 11;
            this.lblMemoryBank.Text = "Memory Bank";
            this.lblMemoryBank.Visible = false;
            // 
            // m_tbxNotification
            // 
            this.m_tbxNotification.Location = new System.Drawing.Point(442, 40);
            this.m_tbxNotification.MaxLength = 5;
            this.m_tbxNotification.Name = "m_tbxNotification";
            this.m_tbxNotification.Size = new System.Drawing.Size(120, 20);
            this.m_tbxNotification.TabIndex = 12;
            this.m_tbxNotification.Text = "5000";
            this.m_tbxNotification.Visible = false;
            // 
            // txtWriteData
            // 
            this.txtWriteData.Location = new System.Drawing.Point(419, 70);
            this.txtWriteData.Name = "txtWriteData";
            this.txtWriteData.Size = new System.Drawing.Size(190, 20);
            this.txtWriteData.TabIndex = 10;
            this.txtWriteData.Visible = false;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.BackColor = System.Drawing.Color.White;
            this.m_btnCancel.Enabled = false;
            this.m_btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCancel.Image")));
            this.m_btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnCancel.Location = new System.Drawing.Point(238, 68);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(60, 25);
            this.m_btnCancel.TabIndex = 2;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnCancel.UseVisualStyleBackColor = false;
            this.m_btnCancel.Visible = false;
            this.m_btnCancel.MouseLeave += new System.EventHandler(this.m_btnCancel_MouseLeave);
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            this.m_btnCancel.MouseEnter += new System.EventHandler(this.m_btnCancel_MouseEnter);
            // 
            // m_tbxtimeintervalMS
            // 
            this.m_tbxtimeintervalMS.Location = new System.Drawing.Point(442, 42);
            this.m_tbxtimeintervalMS.MaxLength = 10;
            this.m_tbxtimeintervalMS.Name = "m_tbxtimeintervalMS";
            this.m_tbxtimeintervalMS.Size = new System.Drawing.Size(120, 20);
            this.m_tbxtimeintervalMS.TabIndex = 14;
            this.m_tbxtimeintervalMS.Text = "1000";
            this.m_tbxtimeintervalMS.Visible = false;
            // 
            // m_chkbxlstoutpins
            // 
            this.m_chkbxlstoutpins.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_chkbxlstoutpins.CheckOnClick = true;
            this.m_chkbxlstoutpins.FormattingEnabled = true;
            this.m_chkbxlstoutpins.Location = new System.Drawing.Point(395, 12);
            this.m_chkbxlstoutpins.Name = "m_chkbxlstoutpins";
            this.m_chkbxlstoutpins.Size = new System.Drawing.Size(145, 92);
            this.m_chkbxlstoutpins.TabIndex = 11;
            this.m_chkbxlstoutpins.ThreeDCheckBoxes = true;
            this.m_chkbxlstoutpins.Visible = false;
            // 
            // m_txtTagID
            // 
            this.m_txtTagID.Location = new System.Drawing.Point(395, 38);
            this.m_txtTagID.MaxLength = 24;
            this.m_txtTagID.Name = "m_txtTagID";
            this.m_txtTagID.Size = new System.Drawing.Size(213, 20);
            this.m_txtTagID.TabIndex = 10;
            this.m_txtTagID.Visible = false;
            // 
            // m_lblTagID
            // 
            this.m_lblTagID.Location = new System.Drawing.Point(301, 40);
            this.m_lblTagID.Name = "m_lblTagID";
            this.m_lblTagID.Size = new System.Drawing.Size(87, 16);
            this.m_lblTagID.TabIndex = 9;
            this.m_lblTagID.Text = "Tag ID";
            this.m_lblTagID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_lblTagID.Visible = false;
            // 
            // m_lblAntenna
            // 
            this.m_lblAntenna.Location = new System.Drawing.Point(304, 65);
            this.m_lblAntenna.Name = "m_lblAntenna";
            this.m_lblAntenna.Size = new System.Drawing.Size(84, 16);
            this.m_lblAntenna.TabIndex = 8;
            this.m_lblAntenna.Text = "Antenna";
            this.m_lblAntenna.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_lblAntenna.Visible = false;
            // 
            // m_cmbAntenna
            // 
            this.m_cmbAntenna.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.m_cmbAntenna.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbAntenna.Location = new System.Drawing.Point(395, 64);
            this.m_cmbAntenna.Name = "m_cmbAntenna";
            this.m_cmbAntenna.Size = new System.Drawing.Size(213, 21);
            this.m_cmbAntenna.TabIndex = 7;
            this.m_cmbAntenna.Visible = false;
            // 
            // m_cboCmdList
            // 
            this.m_cboCmdList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.m_cboCmdList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCmdList.DropDownWidth = 264;
            this.m_cboCmdList.Location = new System.Drawing.Point(8, 34);
            this.m_cboCmdList.Name = "m_cboCmdList";
            this.m_cboCmdList.Size = new System.Drawing.Size(264, 21);
            this.m_cboCmdList.TabIndex = 0;
            this.m_cboCmdList.SelectedIndexChanged += new System.EventHandler(this.m_cboCmdList_SelectedIndexChanged);
            // 
            // m_btnSend
            // 
            this.m_btnSend.BackColor = System.Drawing.Color.White;
            this.m_btnSend.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSend.Image")));
            this.m_btnSend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnSend.Location = new System.Drawing.Point(8, 68);
            this.m_btnSend.Name = "m_btnSend";
            this.m_btnSend.Size = new System.Drawing.Size(127, 25);
            this.m_btnSend.TabIndex = 1;
            this.m_btnSend.Text = "Send";
            this.m_btnSend.UseVisualStyleBackColor = false;
            this.m_btnSend.Click += new System.EventHandler(this.m_btnSend_Click);
            // 
            // m_tabCtlOutput
            // 
            this.m_tabCtlOutput.Controls.Add(this.m_tabPgTableView);
            this.m_tabCtlOutput.Controls.Add(this.m_tabPgCmdOutput);
            this.m_tabCtlOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabCtlOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_tabCtlOutput.Location = new System.Drawing.Point(0, 196);
            this.m_tabCtlOutput.Name = "m_tabCtlOutput";
            this.m_tabCtlOutput.Padding = new System.Drawing.Point(3, 3);
            this.m_tabCtlOutput.SelectedIndex = 0;
            this.m_tabCtlOutput.Size = new System.Drawing.Size(859, 393);
            this.m_tabCtlOutput.TabIndex = 10;
            // 
            // m_tabPgTableView
            // 
            this.m_tabPgTableView.Controls.Add(this.listView1);
            this.m_tabPgTableView.Location = new System.Drawing.Point(4, 24);
            this.m_tabPgTableView.Name = "m_tabPgTableView";
            this.m_tabPgTableView.Size = new System.Drawing.Size(851, 365);
            this.m_tabPgTableView.TabIndex = 1;
            this.m_tabPgTableView.Text = "Live Tag Data";
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Window;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(851, 365);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tag ID";
            this.columnHeader1.Width = 302;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tag Type";
            this.columnHeader2.Width = 177;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "TimeStamp";
            this.columnHeader3.Width = 158;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Antenna Name";
            this.columnHeader4.Width = 240;
            // 
            // m_tabPgCmdOutput
            // 
            this.m_tabPgCmdOutput.Controls.Add(this.listView2);
            this.m_tabPgCmdOutput.Location = new System.Drawing.Point(4, 24);
            this.m_tabPgCmdOutput.Name = "m_tabPgCmdOutput";
            this.m_tabPgCmdOutput.Size = new System.Drawing.Size(851, 365);
            this.m_tabPgCmdOutput.TabIndex = 0;
            this.m_tabPgCmdOutput.Text = "Command Output";
            // 
            // listView2
            // 
            this.listView2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CmdOutputColumn});
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView2.Location = new System.Drawing.Point(0, 0);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(851, 365);
            this.listView2.TabIndex = 0;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // CmdOutputColumn
            // 
            this.CmdOutputColumn.Text = "";
            this.CmdOutputColumn.Width = 600;
            // 
            // TagID
            // 
            this.TagID.DisplayIndex = 0;
            this.TagID.Text = "Tag ID";
            this.TagID.Width = 368;
            // 
            // TagType
            // 
            this.TagType.DisplayIndex = 1;
            this.TagType.Text = "Tag Type";
            this.TagType.Width = 500;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.m_tabCtlOutput);
            this.panel1.Controls.Add(this.m_gbxCommand);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(859, 589);
            this.panel1.TabIndex = 0;
            // 
            // frmTest
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(859, 589);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmTest";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Test Reader";
            this.Load += new System.EventHandler(this.frmTest_Load);
            this.Activated += new System.EventHandler(this.frmTest_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmTest_Closing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.common);
            this.Resize += new System.EventHandler(this.frmTest_Resize);
            this.m_gbxCommand.ResumeLayout(false);
            this.m_gbxCommand.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.grpbxSensors.ResumeLayout(false);
            this.grpBxAntennaStatus.ResumeLayout(false);
            this.grpBxAntennaStatus.PerformLayout();
            this.grpBxInpLineStatus.ResumeLayout(false);
            this.grpBxInpLineStatus.PerformLayout();
            this.m_gpBxParameters.ResumeLayout(false);
            this.m_gpBxParameters.PerformLayout();
            this.m_tabCtlOutput.ResumeLayout(false);
            this.m_tabPgTableView.ResumeLayout(false);
            this.m_tabPgCmdOutput.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		
		#region Output Formattor
		
		private void AppendOutput(string[] strOutput)
		{
//			try
//			{
				

				ListViewItem item     = new ListViewItem(strOutput);
				//m_lvResponse.Items.Add(item);
				//m_lvResponse.Items[m_lvResponse.Items.Count-1].EnsureVisible();
//			}
//			catch(Exception ex)
//			{
//				//MessageBox.Show(ex.Message,this.MdiParent.Text,MessageBoxButtons.OK,MessageBoxIcon.Warning);
//			}
		}
		private void AppendOutput(string[] strOutput,bool isError)
		{
//			try
//			{
				
				ListViewItem item     = new ListViewItem(strOutput);
				
				if(!isError)
				{
					item.ForeColor =  Color.DarkBlue;
				}
				else
				{
					item.ForeColor = Color.Red;
				}
				
                //m_lvResponse.Items.Add(item);
                //m_lvResponse.Items[m_lvResponse.Items.Count-1].EnsureVisible();
//			}
//			catch(Exception ex)
//			{
//				//	MessageBox.Show(ex.Message,this.MdiParent.Text,MessageBoxButtons.OK,MessageBoxIcon.Warning);
//			}
		}

		private void AppendOutput(byte[] cmdByteArr,bool IsResponse)
		{
//			try
//			{
				string[] strByte =new string[2];
				if(IsResponse)
					strByte[0] = "Response Bytes";
				else
					strByte[0] = "Request Bytes";
				strByte[1] = "" ;
				ListViewItem item1 = new ListViewItem(strByte);
				if(IsResponse)
					item1.ForeColor =  Color.DarkBlue;
                //m_lvResponse.Items.Add(item1);
				for(int i=0; i<cmdByteArr.Length;)
				{
					strByte[0] = "";
					strByte[1] = "";
					for(int j=0;j<4 && i<cmdByteArr.Length;j++,i++)
					{
						//strByte +=" "+cmdByteArr[i].ToString("X");
						if( cmdByteArr[i].ToString("X").Length  == 1)
							strByte[1] += "0x0" + cmdByteArr[i].ToString("X") + " ";
						else
							strByte[1] += "0x" + cmdByteArr[i].ToString("X") + " ";
                    
					}
					ListViewItem item = new ListViewItem(strByte);
					if(IsResponse)
						item.ForeColor =  Color.DarkBlue;
                    //m_lvResponse.Items.Add(item);
					
				}
				
                //m_lvResponse.Items[m_lvResponse.Items.Count-1].EnsureVisible();
//			}
//			catch(Exception ex)
//			{
//				//	MessageBox.Show(ex.Message,this.MdiParent.Text,MessageBoxButtons.OK,MessageBoxIcon.Warning);
//			}
		}

		private void AppendSeprator()
		{
//			try
//			{
				string[] str = new string[2];
				str[0] = "------------------------------------";
				str[1] = "--------------------------------------------";
				ListViewItem lvItem = new ListViewItem(str);
                //m_lvResponse.Items.Add(lvItem);
				lvItem.EnsureVisible();
				//	m_lvResponse.Items[m_lvResponse.Items.Count-1].Selected = false;
//			}
//			catch(Exception ex)
//			{
//				//	MessageBox.Show(ex.Message,this.MdiParent.Text,MessageBoxButtons.OK,MessageBoxIcon.Warning);
//			}
		}
	
	
	
//        private void DisplayError(string cmdName,string strErr )
//        {
////			try
////			{
//                string[] strOutput =new string[2] ;
//                AppendSeprator();
//                if(m_chkBxTimeStamp.Checked==true)
//                    strOutput[0] = DateTime.Now.ToString();
//                else
//                    strOutput[0] = "";
//                strOutput[1] = "";
//                AppendOutput(strOutput,true);
//                strOutput[0] = "Response Received";
//                strOutput[1] = ": "+ cmdName;
//                AppendOutput(strOutput,true);
//                strOutput[0] = "Execute Status";
//                strOutput[1] = ": Failed";
//                AppendOutput(strOutput,true);
//                strOutput[0] = "Error";
//                strOutput[1] = ": "+strErr;
//                AppendOutput(strOutput,true);
//                AppendSeprator();
////			}
////			catch(ThreadAbortException ex)
////			{
////			}
////			finally
////			{
////			}
//        }

        //private void DisplayOutput(string cmdName, byte[] cmdReq,byte[] cmdRes)
        //{
        //    string[] strOutput = new string[2];
        //    try
        //    {
        //        if(m_chkBxEchoCmdResp.Checked)
        //        {
        //            AppendOutput(cmdReq,false);
        //        }
        //        AppendSeprator();

        //        if(m_chkBxTimeStamp.Checked==true)
        //            strOutput[0] = DateTime.Now.ToString();
        //        else
        //            strOutput[0] = "";
        //        strOutput[1] = "";
        //        AppendOutput(strOutput,false);

        //        strOutput[0] = "Response Received";
        //        strOutput[1] = ": "+ cmdName;
        //        AppendOutput(strOutput,false);
        //        strOutput[0] = "Execute Status";
        //        strOutput[1] = ": Success";
        //        AppendOutput(strOutput,false);

        //        if(m_chkBxEchoCmdResp.Checked)
        //            AppendOutput(cmdRes,true);
        //        AppendSeprator();
        //    }
        //    catch
        //    {
        //    }
        //}
        //private void DisplayOutput(string cmdName,byte[] cmdReq, byte[] cmdRes,ArrayList alParameter)
        //{
        //    string[] strOutput = new string[2];
        //    try
        //    {
        //        if(m_chkBxEchoCmdResp.Checked)
        //        {
        //            AppendOutput(cmdReq,false);
        //        }
        //        AppendSeprator();

        //        if(m_chkBxTimeStamp.Checked==true)
        //            strOutput[0] = DateTime.Now.ToString();
        //        else
        //            strOutput[0] = "";
        //        strOutput[1] = "";
        //        AppendOutput(strOutput,false);

        //        strOutput[0] = "Response Received";
        //        strOutput[1] = ": "+cmdName;
        //        AppendOutput(strOutput,false);

        //        strOutput[0] = "Execute Status";
        //        strOutput[1] = ": Success";
        //        AppendOutput(strOutput,false);

        //        for(int i=0;i<alParameter.Count;i++)
        //        {
        //            string[] str = (string[])alParameter[i];
        //            AppendOutput(str,false);
        //        }

        //        if(m_chkBxEchoCmdResp.Checked)
        //            AppendOutput(cmdRes,true);
        //        AppendSeprator();
        //    }
        //    catch
        //    {
        //    }
        //}
        //private string GetUserData(byte[] userdata)
        //{
        //    try
        //    {
        //        string strData ="";
        //        string valueUserData ="";
        //        for(int i=0; i<userdata.Length;i++)
        //        {
        //            if( userdata[i].ToString("X").Length  == 1)
        //                strData += "0x0" + userdata[i].ToString("X") + ",";
        //            else
        //                strData += "0x" + userdata[i].ToString("X") + ",";
        //            valueUserData += (char)userdata[i];
        //        }
        //        return strData +"("+ valueUserData +")";
				
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //    finally
        //    {
        //    }
        //}


		#endregion Output Formattor

		#region UI Event Handler
		private void m_btnConnect_Click(object sender, System.EventArgs e)
        {
            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            Connect();
            if (m_ClsReader.model == ReaderModel.RD5000)
            {
                SetProximProgBarDisp(true);
                SetMotionProgBarDisp(true);
            }
        }

		private void m_btnDisconnect_Click(object sender, System.EventArgs e)
        {
            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            m_btnConnect.Enabled = true;
            m_btnDisconnect.Enabled = false;
            Disconnect();
            if (m_ClsReader.model == ReaderModel.RD5000)
            {
                SetProximProgBarDisp(false);
                SetMotionProgBarDisp(false);
            }
            
        }
        	
		private void frmTest_Activated(object sender, System.EventArgs e)
		{
			try
			{
				this.Visible = true;
                				
			}
			catch(Exception ex)
			{
				string str= ex.Message ;
			}
			finally
			{
			}
		}

		private void frmTest_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				e.Cancel = true;
				this.Visible = false;
                //this.m_IsVisible = false;
                this.listView1.Items.Clear();
                this.listView2.Items.Clear();
                this.m_cboCmdList.SelectedIndex = 0;
			}
			catch(Exception ex)
			{
				string str= ex.Message ;
			}
			finally
			{
			}
		}

		private void m_btnClearResp_Click(object sender, System.EventArgs e)
		{
			try
			{
                if(m_tabPgCmdOutput.Visible)
                	listView2.Items.Clear();
                else if (m_tabPgTableView.Visible )
                    listView1.Items.Clear();
				GC.Collect();
			}
			catch
			{
			}
		}

        private void DefaultDisplay()
        {
            m_lblAntenna.Visible = false;
            m_lblTagID.Visible = false;
            m_cmbAntenna.Visible = false;
            m_txtTagID.Visible = false;

            m_chkbxlstoutpins.Visible = false;

            m_lblNotification.Visible = false;
            m_tbxNotification.Text = "5000";
            m_tbxNotification.Visible = false;

            m_lbltimeintervalMS.Visible = false;
            m_tbxtimeintervalMS.Visible = false;
            m_tbxtimeintervalMS.ResetText();

            txtCount.Visible = false;
            txtPointer.Visible = false;
            txtMemBank.Visible = false;
            txtWriteData.Visible = false;

            lblCount.Visible = false;
            lblMemoryBank.Visible = false;
            lblPointer.Visible = false;
            lblWriteData.Visible = false;
        }

		private void m_cboCmdList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
            string[] antennalist = null;

            DefaultDisplay();
            string selectedCmd = m_cboCmdList.SelectedItem.ToString().Trim();
			try
			{
                switch (selectedCmd)
                {
                    case m_WriteTagID:
                    case m_LockTags:
                            m_lblAntenna.Visible = true;
                            m_lblTagID.Visible = true;
                            m_cmbAntenna.Visible = true;
                            m_txtTagID.Visible = true;
                            m_txtTagID.ResetText();
                            m_cmbAntenna.Items.Clear();
                            antennalist = m_ClsReader.GetAntennaNames();
                            try
                            {
                                foreach (string st in antennalist)
                                    m_cmbAntenna.Items.Add(st);
                                m_cmbAntenna.SelectedIndex = 0;
                            }
                            catch
                            {
                                m_cmbAntenna.Items.Add("No Antennae") ;
                                m_cmbAntenna.SelectedIndex = 0;
                            }
                        break;
                    case m_SetOutputStatus:
                        SetOutPutPinsDisplay();
                        break;
                    case m_EnableInputStatusNotification:
                         m_lblNotification.Visible = true;
                        m_tbxNotification.Visible = true;
                        break;

                    case m_EnableMotionSensor:
                        try
                        {
                            m_tbxtimeintervalMS.Text = m_ClsReader.MotionTimeIntervalMS.ToString();
                            m_lbltimeintervalMS.Visible = true;
                            m_tbxtimeintervalMS.Visible = true;
                        }
                        catch
                        {
                            m_tbxtimeintervalMS.Text = "1000";
                        }
                        break;


                    case m_WriteTags:
                        txtPointer.Visible = true;
                        txtMemBank.Visible = true;
                        txtWriteData.Visible = true;

                        lblMemoryBank.Visible = true;
                        lblPointer.Visible = true;
                        lblWriteData.Visible = true;
                        break;

                    case m_ReadTagData:
                        txtPointer.Visible = true;
                        txtMemBank.Visible = true;
                        txtCount.Visible = true;

                        lblMemoryBank.Visible = true;
                        lblPointer.Visible = true;
                        lblCount.Visible = true;
                        break;
                    case m_EnableProximitySensor:
                        try
                        {
                            m_tbxtimeintervalMS.Text = m_ClsReader.ProximityTimeIntervalMS.ToString();
                            m_lbltimeintervalMS.Visible = true;
                            m_tbxtimeintervalMS.Visible = true;
                        }

                        catch
                        {
                            m_tbxtimeintervalMS.Text = "1000";
                        }
                        
                        break;
                    default:
                        m_lblAntenna.Visible = false;
                        m_lblTagID.Visible = false;
                        m_cmbAntenna.Visible = false;
                        m_txtTagID.Visible = false;

                         m_chkbxlstoutpins.Visible = false;

                        m_lblNotification.Visible = false;
                        m_tbxNotification.Text = "5000" ;
                        m_tbxNotification.Visible = false;

                        m_lbltimeintervalMS.Visible = false;
                        m_tbxtimeintervalMS.Visible = false;
                        m_tbxtimeintervalMS.ResetText();

                        break;
                }
			}
			catch
			{

			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}
		
	    private void frmTest_Load(object sender, System.EventArgs e)
		{
            try
            {
                if (m_ClsReader.GetReaderStatus() != ReaderStatus.ONLINE)
                {
                    m_lblActive.Visible = false;
                    m_lblInActive.Visible = true;
                    m_btnConnect.Enabled = true;
                    m_btnDisconnect.Enabled = false;
                    //m_btnSend.Enabled = false;
                   
                }
                else
                {
                    m_btnSend.Enabled = true;
                }
               
            }
            catch
            {
                MessageBox.Show("Error: Reader not initialized", "Symbol Reader WinApp",MessageBoxButtons.OKCancel);
                this.Close();
            }

            
            try
            {
                string reader_Model = m_ClsReader.GetModel().ToString();
                switch (reader_Model)
                {
                    case "XR480":
                    case "XR450":
                    case "XR440":
                    case "XR400":
                        grpBxAntennaStatus.Visible = true;

                        if(m_ClsReader.model == ReaderModel.XR480)
                        {
                            m_rdiobtnantena5.Visible = true;
                            m_lblAnt5.Visible = true;
                            m_rdiobtnantena6.Visible = true;
                            m_lblAnt6.Visible = true;
                            m_rdiobtnantena7.Visible = true;
                            m_lblAnt7.Visible = true;
                            m_rdiobtnantena8.Visible = true;
                            m_lblAnt8.Visible = true;
                        }

                        grpBxInpLineStatus.Visible = true;
                        
                        m_lblRFIDStatus.Visible = false;
                        break;
                    case "RD5000":
                        grpBxAntennaStatus.Visible = false;
                        grpBxInpLineStatus.Visible = false;
                        
                        m_lblRFIDStatus.Visible = true;
                        if (!m_ClsReader.GetRFIDModuleStatus())
                        {
                            m_lblRFIDStatus.Text = "RFID Module : INACTIVE";
                            m_lblRFIDStatus.BackColor = Color.Tan;
                        }
                        else
                        {
                            m_lblRFIDStatus.Text = "RFID Module : ACTIVE";
                            m_lblRFIDStatus.BackColor = Color.DarkSeaGreen;
                        }
                        break;
                }
            }
                catch
                    {}
            }

        private void SetInputPinsDisplay(IOPinStatus[] inputPinsStatus)
        {
            Color onColor = Color.SeaGreen;
            Color offColor = Color.Red;
            int index;
            for (index = 0; index < 6; index++)
            {
                switch (index)
                {
                    case 0:
                        m_rbtnInpPin1.BackColor = inputPinsStatus[index].pinStatus == true ? onColor : offColor;

                        break;
                    case 1:
                        m_rbtnInpPin2.BackColor = inputPinsStatus[index].pinStatus == true ? onColor : offColor;
                        break;
                    case 2:
                        m_rbtnInpPin3.BackColor = inputPinsStatus[index].pinStatus == true ? onColor : offColor;
                        break;
                    case 3:
                        m_rbtnInpPin4.BackColor = inputPinsStatus[index].pinStatus == true ? onColor : offColor;
                        break;
                    case 4:
                        m_rbtnInpPin5.BackColor = inputPinsStatus[index].pinStatus == true ? onColor : offColor;
                        break;
                    case 5:
                        m_rbtnInpPin6.BackColor = inputPinsStatus[index].pinStatus == true ? onColor : offColor;
                        break;
                    default:

                        break;

                }


            }  
        }
        
        private void SetOutPutPinsDisplay()
        {
            int outpinscount = 0;
            m_chkbxlstoutpins.Visible = true;
            try
            {
                IOPinStatus[] inpstatus = null;
                IOPinStatus[] opstatus = null;

                m_ClsReader.GetPinlevels(out inpstatus, out opstatus);
                outpinscount = opstatus.Length;

                m_chkbxlstoutpins.Items.Clear();
                for (int i = 0; i < outpinscount; i++)
                {
                    if (opstatus[i].pinStatus == true)
                        m_chkbxlstoutpins.Items.Add("Output Pin " + i.ToString(), true);
                    else
                        m_chkbxlstoutpins.Items.Add("Output Pin " + i.ToString(), false);
                }
            }

            catch
            {
                m_chkbxlstoutpins.Items.Clear();
                for (int i = 0; i < 6; i++)
                {
                    m_chkbxlstoutpins.Items.Add("Output Pin " + i.ToString());
                }

            }
            //hardcoded outpt pins
            if (m_chkbxlstoutpins.Items.Count == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    m_chkbxlstoutpins.Items.Add("Output Pin " + i.ToString());
                }
            }


        }
        
		private void m_btnSend_Click(object sender, System.EventArgs e)
		{
            
			try
			{
                //m_btnSend.Enabled = false;
               // if (m_cboRepeatCount.SelectedIndex == 0)
                ExecuteCmd();
                //else
                //{
                //    //m_CancelCmdExec = false;
                //    m_threadFunction = new ThreadStart(ExecuteCmd);
                //    m_threadExecute = new Thread(m_threadFunction);
                //    m_threadExecute.Start();

                //}
			}
			catch
			{
			}
			finally
			{
                //m_btnSend.Enabled = true; 
			}
		}

		private void common(object sender,KeyPressEventArgs e)
		{
			Validation objValidation = new Validation();
			objValidation.DisableKeysForByte(e);
			
		}

		private void m_btnCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				//m_CancelCmdExec = true;
                //if (m_threadExecute != null)
                //{
                //    m_threadExecute.Join(m_timeOut);
                //    m_threadExecute = null;
                //}
				
				//m_CancelCmdExec = false;
			}
			catch(Exception ex)
			{
				string str=ex.Message ;
			}		
		}

		private void m_btnCancel_MouseEnter(object sender, System.EventArgs e)
		{ 
			
			try
			{
				m_cursor = Cursor.Current ;
				Cursor.Current = Cursors.Default ;

			}
			catch(Exception ex)
			{
				string str=ex.Message ;
			}
		}

		private void m_btnCancel_MouseLeave(object sender, System.EventArgs e)
		{
			try
			{
				Cursor.Current = m_cursor ;
			}
			catch(Exception ex)
			{
				string str=ex.Message ;
			}		
		}
		
		#endregion UI Event Handler

		#region Properties
		public string ReaderInfo
		{
			get
			{
				return m_strReaderInfo;
			}
		}
		public ListView  CommandOutput
		{
			get
			{
				return listView2 ;
			}
		}
		public ListView  TagListView
		{
			get
			{
				return listView1;
			}
		}

		#endregion Properties

		/// <summary>
		/// It will validate each input provided by user and returns boolean value.
		/// </summary>
		/// <returns></returns>
		
		private void ExecuteCmd()
		{
            string strSelectedCmd = "";
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			object m_LockObj = new object();
            Color statusColor = Color.Green; 
                        
            int lstcount =0;
            
            string antennaName ="";
           
            string executionStatus = "Success";
            string errMsg = "";
            if(listView2.Items.Count > 0)
             lstcount = listView2.Items.Count;
			try
			{
				
				lock(m_Object)
                {
                    #region repeat cnt code
                    //m_cboCmdList.Enabled = false;
                    //m_cboRepeatCount.Enabled = false;
					
					//m_btnCancel.Focus();
					//m_btnSend.Enabled= false; 
                    //if(Convert.ToInt32(m_cboRepeatCount.Text) > 1)
                    //        m_btnCancel.Enabled = true;
                    //int counter = 1;
                    //for(int i=0;i<Convert.ToInt32(m_cboRepeatCount.Text) /*& counter <= 20*/ ;)
                    //{
                    //        listView1.BeginUpdate();

                    //       if (m_ClsReader.GetReaderStatus() != ReaderStatus.ONLINE)
                    //        {
                    //            counter ++;
                    //            if(counter == 20)
                    //                MessageBox.Show("Reader is not connected","Unable to Execute",MessageBoxButtons.OKCancel);
                    //            listView1.EndUpdate();
                    //            for(int j=0;j<1000;j++);
                    //            continue;
                    //        }

                    //        counter = 0;
                    //        if(m_CancelCmdExec)
                    //            break;
                    #endregion repeat cnt code

                    strSelectedCmd = m_cboCmdList.SelectedItem.ToString();
                    switch (strSelectedCmd)
					{
					    case m_StartAutoMode:
                                    m_tabPgTableView.BringToFront();
                                    m_tabPgTableView.Focus();
                                    m_tabCtlOutput.SelectedIndex = 0;
                                    CallStartAutoMode(); 
                                    break;

                        case m_OnDemandMode:
                                    m_tabPgCmdOutput.BringToFront();
                                    m_tabPgCmdOutput.Focus();
                                    m_tabCtlOutput.SelectedIndex = 1;
                                    CallStopAutoMode();
                                    m_lblAutoPoll.Text = "Autonomous Mode:OFF";
                                    m_lblAutoPoll.BackColor = Color.Tan;
                                    break;

                        case m_GetTags:
                                    m_tabPgCmdOutput.BringToFront();
                                    m_tabPgCmdOutput.Focus();
                                    m_tabCtlOutput.SelectedIndex = 1;
                                    CallGetTags();
                                    break;


                                case m_ReadTagData:
                                    m_tabPgCmdOutput.BringToFront();
                                    m_tabPgCmdOutput.Focus();
                                    m_tabCtlOutput.SelectedIndex = 1;
                                    CallReadData();
                                    break;
                        case m_WriteTagID:
                                if (IsValidParameter())
                                {
                                    antennaName = m_cmbAntenna.SelectedItem.ToString().Trim();
                                    m_ClsReader.WriteTagID(m_txtTagID.Text.Trim(), m_ClsReader.GetAntennaConfiguration(antennaName));
                                }
                                    break;
                        case m_KillTags:

                                        //dummy parameters
                                    TagType tagType1 = Symbol.RFID2.TagType.EPCClass0;
                                    byte[] tag = { 1, 2, 3, 4, 5 };
                                    //uint passcode1 = 0;
                                    
                                   // m_ClsReader.KillTag(tagType1, tag, passcode1);
                                    m_ClsReader.KillTag(tagType1, tag);
                                   
                                    break;
                        case m_EraseTags:
                                    TagType tagType2 = Symbol.RFID2.TagType.EPCClass0;
                                    m_ClsReader.EraseTag(tagType2);
                                   
                                    break;
                        case m_LockTags:

                                    //Gen2Parameters gen2Params = new Gen2Parameters();
                                    //gen2Params.sel = 0;
                                    //gen2Params.session = 0;
                                    //gen2Params.startingQ = 0;
                                    //gen2Params.target = 0;   
                                    //ushort lockMask =0;
                                    //ushort lockAction=0;
                                    //uint accessPassword = 0;
                            if (m_ClsReader.model != ReaderModel.RD5000)
                            {
                                if (IsValidParameter())
                                {
                                    antennaName = m_cmbAntenna.SelectedItem.ToString().Trim();
                                    m_ClsReader.LockTag(m_txtTagID.Text.Trim(), m_ClsReader.GetAntennaConfiguration(antennaName));
                                }
                            }
                            else
                            {
                                throw new Exception("Not Implemented for this Model");
                            }
                                    
                                    break;
                        case m_WriteTags:
                            char[] chTagID = txtWriteData.Text.ToCharArray();
                            ArrayList tagIdByteArr = new ArrayList();

                            for (int i = 0; i < chTagID.Length - 1; i += 2)
                            {
                                string strTemp = new string(new char[] { chTagID[i], chTagID[i + 1] });
                                byte idByte = Convert.ToByte(strTemp, 16);
                                tagIdByteArr.Add(idByte);
                            }
                            byte[] tagdata = (byte[])tagIdByteArr.ToArray(typeof(byte));

                            bool write = m_ClsReader.WriteTag(byte.Parse(txtMemBank.Text), ushort.Parse(txtPointer.Text), tagdata);
                            if (!write)
                                throw new Exception("Could not write tag");
                                    break;

                        case m_GetIOStatus:
                                    m_tabPgCmdOutput.BringToFront();
                                    m_tabPgCmdOutput.Focus();
                                    m_tabCtlOutput.SelectedIndex = 1;
                                    CallGetInputPinLevel();
                                      
                                    break;
                        case m_SetOutputStatus:
                                    m_tabPgCmdOutput.BringToFront();
                                    m_tabPgCmdOutput.Focus();
                                    m_tabCtlOutput.SelectedIndex = 1;
                                    
                                    CallSetOutputPinLevel();
                                    break;

                        case m_EnableInputStatusNotification:
                                    m_tabPgCmdOutput.BringToFront();
                                    m_tabPgCmdOutput.Focus();
                                    m_tabCtlOutput.SelectedIndex = 1;
                            
                                    IOPins[] selectedPins = null;
                                    try
                                    {
                                        IOPinStatus[] inputPinsStatus = null;
                                        IOPinStatus[] outputPinsStatus = null;

                                        m_ClsReader.GetPinlevels(out inputPinsStatus, out outputPinsStatus);
                                        selectedPins = new IOPins[inputPinsStatus.Length];
                                        for (int i = 0; i < inputPinsStatus.Length; i++)
                                        {
                                            selectedPins[i] = inputPinsStatus[i].pin;
                                        }
                                    }
                                    catch
                                    {
                                        selectedPins = new IOPins[6];
                                    }
                                    if (IsValidNotificationInterval())
                                    {
                                        int interval = Convert.ToInt32(m_tbxNotification.Text.Trim());
                                        m_ClsReader.EnableInputStatusNotification(selectedPins, interval);
                                    }
                                    break;
                        case m_DisableInputStatusNotification:
                                    m_tabPgCmdOutput.BringToFront();
                                    m_tabPgCmdOutput.Focus();
                                    m_tabCtlOutput.SelectedIndex = 1;
                            
                                    m_ClsReader.DisableInputStatusNotification();

                                    m_rbtnInpPin1.BackColor = Color.Red;
                                    m_rbtnInpPin2.BackColor = Color.Red; 
                                    m_rbtnInpPin3.BackColor = Color.Red;
                                    m_rbtnInpPin4.BackColor = Color.Red;
                                    m_rbtnInpPin5.BackColor = Color.Red;
                                    m_rbtnInpPin6.BackColor = Color.Red; 
                                    break;

                        case m_EnableMotionSensor:
                            if (isValidEnableSensorParams())
                            {
                                int timeInterval = Convert.ToInt32(m_tbxtimeintervalMS.Text.Trim());
                                m_ClsReader.EnableMotionSensor(timeInterval);

                                m_lblMotionX.Enabled = true;
                                m_lblMotionY.Enabled = true;
                                m_lblMotionZ.Enabled = true;
                                motionXProgBar.BackColor = Color.LemonChiffon; ;
                                motionYProgBar.BackColor = Color.LemonChiffon; ;
                                motionZProgBar.BackColor = Color.LemonChiffon; ;
                                m_tabPgCmdOutput.BringToFront();
                                m_tabPgCmdOutput.Focus();
                                m_tabCtlOutput.SelectedIndex = 1;
                                
                            }
                            
                            break;
                        case m_DisableMotionSensor:
                            m_ClsReader.DisableMotionSensor();
                            m_lblMotionX.Text = "Motion  X : " ;
                            m_lblMotionY.Text = "Motion  Y : " ;
                            m_lblMotionZ.Text = "Motion  Z : " ;
                            m_lblMotionX.Enabled = false;
                            m_lblMotionY.Enabled = false;
                            m_lblMotionZ.Enabled = false;
                            motionXProgBar.BackColor = Color.WhiteSmoke;
                            motionYProgBar.BackColor = Color.WhiteSmoke;
                            motionZProgBar.BackColor = Color.WhiteSmoke;
                            motionXProgBar.Value = 0;
                            motionYProgBar.Value = 0;
                            motionZProgBar.Value = 0;
                            m_tabPgCmdOutput.BringToFront();
                            m_tabPgCmdOutput.Focus();
                            m_tabCtlOutput.SelectedIndex = 1;
                            
                            break;
                        case m_EnableProximitySensor:
                            if (isValidEnableSensorParams())
                            {
                                
                                int timeInterval = Convert.ToInt32(m_tbxtimeintervalMS.Text.Trim());
                                m_ClsReader.EnableProximitySensor(timeInterval);
                                m_lblProximity.Enabled = true;
                                proximityProgBar.BackColor = Color.LemonChiffon;
                                m_tabPgCmdOutput.BringToFront();
                                m_tabPgCmdOutput.Focus();
                                m_tabCtlOutput.SelectedIndex = 1;
                                
                            }
                            break;
                        case m_DisableProximitySensor:
                            m_ClsReader.DisableProximitySensor();
                            m_lblProximity.Text = "Proximity : ";
                            m_lblProximity.Enabled = false;
                            proximityProgBar.BackColor = Color.WhiteSmoke;
                            proximityProgBar.Value = 0;
                            m_tabPgCmdOutput.BringToFront();
                            m_tabPgCmdOutput.Focus();
                            m_tabCtlOutput.SelectedIndex = 1;
                            break;
    
                        case m_EnableRFIDModule:
                            m_ClsReader.EnableRFIDModule();
                            m_tabPgCmdOutput.BringToFront();
                            m_tabPgCmdOutput.Focus();
                            m_tabCtlOutput.SelectedIndex = 1;
                            m_lblRFIDStatus.Text = "RFID Module : ACTIVE";
                            m_lblRFIDStatus.BackColor = Color.DarkSeaGreen;    

                            break;
                        case m_DisableRFIDModule:
                            m_ClsReader.DisableRFIDModule();
                            if (m_ClsReader.GetReadMode() == ReadMode.AUTONOMOUS)
                            {
                                strSelectedCmd = m_OnDemandMode;
                                ExecuteCmd();
                            }
                            m_tabPgCmdOutput.BringToFront();
                            m_tabPgCmdOutput.Focus();
                            m_tabCtlOutput.SelectedIndex = 1;
                            m_lblRFIDStatus.Text = "RFID Module : INACTIVE";
                            m_lblRFIDStatus.BackColor = Color.Tan;
                            m_lblAutoPoll.Text = "Autonomous Mode:OFF";
                            m_lblAutoPoll.BackColor = Color.Tan;
                            break;
                        case m_GetRFIDModuleStatus:
                            
                              string connected = "";
                              if(m_ClsReader.GetRFIDModuleStatus())
                              {
                                  connected = " ACTIVE ";
                                  m_lblRFIDStatus.Text = "RFID Module : ACTIVE";
                                  m_lblRFIDStatus.BackColor = Color.DarkSeaGreen;    

                              }
                              else
                              {
                                  connected = " INACTIVE ";
                                  m_lblRFIDStatus.Text = "RFID Module : INACTIVE";
                                  m_lblRFIDStatus.BackColor = Color.Tan;
                                  
                                  m_lblAutoPoll.Text = "Autonomous Mode:OFF";
                                  m_lblAutoPoll.BackColor = Color.Tan;

                              }
                                listView2.Items.Insert(0, new ListViewItem());
                                listView2.Items.Insert(0, new ListViewItem(String.Format("RFID Module Status:{0}", connected )));
                                listView2.Items[0].ForeColor = Color.Green;
                            
                            m_tabPgCmdOutput.BringToFront();
                            m_tabPgCmdOutput.Focus();
                            m_tabCtlOutput.SelectedIndex = 1;
                            break;
					    default:
                            executionStatus = "Failed";
                                    //statusColor = Color.Red;
                                break;

                        }

                        #region rpeat cnt code
                        //  if(listView1.Items.Count > 1000)
                    //    {								
                    //        int nDiff = listView1.Items.Count - 1000;
                    //        for(int l =0; l < nDiff; l++) 
                    //        {
                    //            listView1.Items.RemoveAt(0);
                    //        }
							
                    //    }
                    //    listView1.EndUpdate();
                    //    i++;

                        //}
                        #endregion rpeat cnt code
                    }
			}

            catch (ReaderException ex)
            {
                        executionStatus = "Failed";
                        statusColor = Color.Red;
                        //if (ex.InnerException!=null)
                        //    errMsg = ex.Message + " " + ex.InnerException.Message;
                        //else
                         errMsg = ex.Message;

            }
            catch (Exception  ex)
            {
                executionStatus = "Failed";
                statusColor = Color.Red;
                errMsg = ex.Message;

            }
			finally
			{
                       if (executionStatus == "Failed")
                        {
                            m_tabPgCmdOutput.BringToFront();
                            m_tabPgCmdOutput.Focus();
                            m_tabCtlOutput.SelectedIndex = 1;
                        }

                        if (listView2.Items.Count > 500)
                        {
                            for (int i = 0; i < 500; i++)
                            {
                                listView2.Items.RemoveAt(0);
                            }
                        }
                      
                        string[]  erMsgStrArr =  DisplayErrorMsg(errMsg);

                        for (int i = erMsgStrArr.Length - 1; i >= 0; i--)
                        {
                            listView2.Items.Insert(0, new ListViewItem(erMsgStrArr[i]));
                            listView2.Items[0].ForeColor = statusColor;
                        }
                       
                        listView2.Items.Insert(0, new ListViewItem("Execution Status: " + executionStatus));
                        listView2.Items[0].ForeColor = statusColor;
                        listView2.Items.Insert(0, new ListViewItem("TimeStamp: " + DateTime.Now.ToString(timeStampFormat)));
                        listView2.Items[0].ForeColor = statusColor;
                        if (m_cboCmdList.SelectedIndex == 3)
                        {
                            listView2.Items.Insert(0, new ListViewItem(String.Format("Antenna name:{0}",antennaName )));
                            listView2.Items[0].ForeColor = statusColor;
                        }
                        listView2.Items.Insert(0, new ListViewItem("Response:"));
                        listView2.Items[0].ForeColor = statusColor;
                        listView2.Items.Insert(0, new ListViewItem("Command Sent: " + strSelectedCmd.Substring(3)));
                        listView2.Items[0].ForeColor = Color.Blue;
                        listView2.Items.Insert((listView2.Items.Count-lstcount), new ListViewItem("----------------------------------------------------------------"));
                        m_cboCmdList.Enabled = true;
                      
                        Cursor.Current = Cursors.Default;                
				
			}

		}

        private void CallReadData()
        {
            try
            {
                IRFIDTag[] tagsRead = m_ClsReader.ReadData(byte.Parse(txtMemBank.Text),
                                                ushort.Parse(txtPointer.Text), byte.Parse(txtCount.Text));

                listView2.Items.Insert(0, new ListViewItem());

                UpdateResponse(tagsRead);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string[] DisplayErrorMsg(string errMsg)
        {
            ArrayList erMsgArr = new ArrayList();
            while (errMsg.Length >= 80)
            {

                erMsgArr.Add(errMsg.Substring(0, 80));
                errMsg = errMsg.Remove(0, 80);

            }
            if (errMsg != null || errMsg.Length > 0)
                erMsgArr.Add(errMsg);
            return((string[])erMsgArr.ToArray(typeof(string)));


        }

        private bool isValidEnableSensorParams()
        {
            Validation objValid = new Validation();
            string msg = "";

           objValid.IsNotEmpty(m_tbxtimeintervalMS.Text.Trim(), out msg);

            if (!msg.Equals(string.Empty))
                throw new ValidationException(msg + "Time Interval in millisecs");

            objValid.IsValidNumber(m_tbxtimeintervalMS.Text.Trim(), out msg);
            if (!msg.Equals(string.Empty))
                throw new ValidationException("Should Be Numeric Value");

            int timeIntervalMS = Convert.ToInt32(m_tbxtimeintervalMS.Text.Trim());
            if (timeIntervalMS < 500)
                throw new ValidationException("Minimum Permissible Value : 500 ");


            return true;
        }

        private bool IsValidHexTagid(string tagID)
        {
            string patternAlphaNum = @"[a-fA-F\d]*";

            if (new Regex(patternAlphaNum).Match(tagID).Length == tagID.Length)
                return true;
            else
                return false;
        }

        private bool isValidTagID()
        {
            Validation objValid = new Validation();
            string msg = "";

            objValid.IsNotEmpty(m_txtTagID.Text.Trim(), out msg);

            if (!msg.Equals(string.Empty))
                throw new ValidationException(msg + "Tag ID");
            if (!IsValidHexTagid(m_txtTagID.Text.Trim()))
                throw new ValidationException("Only Hexadecimal characters allowed.");

            return true;
        }

        private bool IsValidParameter()
        {
            Validation objValid = new Validation();
            string msg = "";
           
            bool retVal = true;

            retVal = isValidTagID();
           
            if(m_cmbAntenna.Items.Count ==0)
                throw new ValidationException(msg + "Antenna Name list is Empty");
            objValid.IsNotEmpty(m_cmbAntenna.SelectedItem.ToString().Trim(), out msg);
            if (!msg.Equals(string.Empty))
                throw new ValidationException(msg + "Antenna Name");
           
            return retVal;
        }

        private bool IsValidNotificationInterval()
        {
            Validation objValid = new Validation();
            string msg = "";
            
            string patternNum = @"[\d]*";
            objValid.IsNotEmpty(m_tbxNotification.Text.Trim(), out msg);
            if (!msg.Equals(String.Empty))
                throw new ValidationException(msg + "Notification Interval");
            int argVal = Convert.ToInt32(m_tbxNotification.Text.Trim());
            if (argVal > 99 && argVal <= 25500)
            {
                if (new Regex(patternNum).Match(m_tbxNotification.Text.Trim()).Length != m_tbxNotification.Text.Trim().Length)
                    throw new ValidationException("Only Numeric Input allowed.");
            }
            else
            {
                throw new ValidationException("Permissible Range of Numbers:100-25500");
            }
            return true;
        }

        private void SetProximProgBarDisp(bool isConnected)
        {


            if (!isConnected)
            {
                

                    proximityProgBar.BackColor = Color.WhiteSmoke;
                    m_lblProximity.Text = "Proximity : ";
                    m_lblProximity.Enabled = false;
                    proximityProgBar.Value = 0;

            }

        }
        private void SetMotionProgBarDisp(bool isConnected)
        {

            if (!isConnected)
            {
                  
                    m_lblMotionX.Text = "Motion  X : ";
                    m_lblMotionY.Text = "Motion  Y : ";
                    m_lblMotionZ.Text = "Motion  Z : ";
                    m_lblMotionX.Enabled = false;
                    m_lblMotionY.Enabled = false;
                    m_lblMotionZ.Enabled = false;
                    motionXProgBar.BackColor = Color.WhiteSmoke;
                    motionYProgBar.BackColor = Color.WhiteSmoke;
                    motionZProgBar.BackColor = Color.WhiteSmoke;
                    motionXProgBar.Value = 0;
                    motionYProgBar.Value = 0;
                    motionZProgBar.Value = 0;
            }
        }

		public void Connect()
		{
            try
            {
                if (m_ClsReader.GetReaderStatus() != ReaderStatus.ONLINE)
                {
                    m_ClsReader.Connect();
                    m_btnConnect.Enabled = false;
                    m_btnDisconnect.Enabled = true;
                    m_btnDisconnect.Enabled = true;
                    m_lblActive.Visible = true;
                    m_lblInActive.Visible = false;
                    //m_btnSend.Enabled = true;

                }
            }
            catch (Exception ex)
            {
               if(ex.InnerException!=null)
                MessageBox.Show("Unable to Connect. " + ex.Message + ". \n" + ex.InnerException.Message, this.MdiParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
               else
                MessageBox.Show("Unable to Connect. " + ex.Message, this.MdiParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
		}

		public void Disconnect()
		{
            try
            {
                m_ClsReader.DisConnect();
                m_lblActive.Visible = false;
                m_lblInActive.Visible = true;
                m_btnConnect.Enabled = true;
                m_btnDisconnect.Enabled = false;
                //m_lblAutoPoll.Text = "Autonomous Mode:OFF";
                //m_lblAutoPoll.BackColor = Color.Tan;
                //m_btnSend.Enabled = false;
            }
            catch (Exception ex)
            {
                if (ex.InnerException!=null)
                    MessageBox.Show("Unable to Disconnect . " + ex.Message + " . \n" + ex.InnerException.Message, this.MdiParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Unable to Disconnect . " + ex.Message , this.MdiParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
		}
		
		public void DisableConnectBtn(bool flag)
		{

			
			//When flag is true = reader is active
			m_btnConnect.Enabled	=	flag;
			m_lblActive.Visible		=	!flag;
			m_btnDisconnect.Enabled	=	!flag;
			m_lblInActive.Visible	=	flag;


		}

        private void UpdateResponse(IRFIDTag[] Tags)
        {
            try
            {
                byte[] tagSN = null;
                string tagIDStr = String.Empty;
                string tagDataStr = String.Empty;

                if (Tags == null || Tags.Length == 0)
                {
                    listView2.Items.Insert(0, new ListViewItem());
                    listView2.Items.Insert(0, new ListViewItem("No Tag Found"));
                    listView2.Items[0].ForeColor = Color.Red;
                    return;
                }

                foreach (IRFIDTag newTag in Tags)
                {

                    tagIDStr = String.Empty;
                    tagDataStr = String.Empty;

                    tagSN = newTag.TagID;
                    foreach (byte b in tagSN)
                        tagIDStr += b.ToString("X2") + " ";

                    listView2.Items.Insert(0, new ListViewItem());
                    listView2.Items.Insert(0, new ListViewItem(String.Format("Antenna name:{0}", newTag.AntennaName)));
                    listView2.Items[0].ForeColor = Color.Green;
                    listView2.Items.Insert(0, new ListViewItem(String.Format("Tag TimeStamp: {0}", newTag.LastSeen.ToString(timeStampFormat))));
                    listView2.Items[0].ForeColor = Color.Green;
                    listView2.Items.Insert(0, new ListViewItem("Tag Type:" + newTag.TagType.ToString()));
                    listView2.Items[0].ForeColor = Color.Green;
                    listView2.Items.Insert(0, new ListViewItem("Tag ID:" + tagIDStr));
                    listView2.Items[0].ForeColor = Color.Green;

                    if (newTag.TagData != null && newTag.TagData.Length > 0)
                    {
                        tagSN = newTag.TagData;
                        foreach (byte b in tagSN)
                            tagDataStr += b.ToString("X2") + " ";

                        listView2.Items.Insert(0, new ListViewItem("Tag Data:" + tagDataStr));
                        listView2.Items[0].ForeColor = Color.Green;
                    }
                }

                listView2.Items.Insert(0, new ListViewItem());
                if (Tags.Length == 1)
                    listView2.Items.Insert(0, new ListViewItem(string.Format("{0} Tag Found", Tags.Length)));
                else
                    listView2.Items.Insert(0, new ListViewItem(string.Format("{0} Tags Found", Tags.Length)));
                listView2.Items[0].ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Event Handlers
        private void InvokeUpdateListView(object sender, ReaderEventArgs args)
        {
            try
            {
                this.BeginInvoke(new AppReaderEventHandler(UpdateListView), new object[] { sender, args });
            }
            catch { }
        }
        private void UpdateListView(object sender, ReaderEventArgs args)
        {
            try
            {   
                foreach (IRFIDTag tag in ((TagEventArgs)args).Tags)
                {
                    if (tag == null)
                        return;

                    
                    byte[] tag_ID = tag.TagID;

                    if (tag_ID == null)
                        return;

                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in tag_ID)
                    {
                        sb.Append(b.ToString("X2"));
                        sb.Append(" ");
                            
                    }

                    ListViewItem lvItem = new ListViewItem(new string[] { sb.ToString(),
                        tag.TagType.ToString(), tag.LastSeen.ToString(timeStampFormat) , tag.AntennaName });

                    listView1.Items.Add(lvItem);
                    lvItem.EnsureVisible();
                    lvItem.Focused = true;
                    lvItem.Selected = true;

                    //if (listView1.Items.Count > 2000)
                    //{
                    //    for (int i = 0; i < 2000; i++)
                    //    {
                    //        listView1.Items.RemoveAt(0);
                    //    }
                    //}
                    if (listView1.Items.Count > 1000)
                    {
                        int nDiff = 500;
                        for (int i = 0; i < nDiff; i++)
                        {
                            listView1.Items.RemoveAt(0);
                        }
                    }


                }
            }
            catch 
            {
                
            }
            Application.DoEvents(); 
        }

        private void InvokeUpdateInputStatus(object sender, ReaderEventArgs args)
        {
            try
            {
                this.BeginInvoke(new AppReaderEventHandler(UpdateInputStatus), new object[] { sender, args });
            }
            catch
            { }
        }

        private void UpdateInputStatus(object sender, ReaderEventArgs args)
        {
            try
            {

                GPIStatusEventArgs arg = (GPIStatusEventArgs)args;
                bool[] inputPinStatus = arg.InputStatus;

                Color onColor = Color.SeaGreen;
                Color offColor = Color.Red;
                int index = 0;

                if (inputPinStatus == null)
                    return;

                for (index = 0; index < 6; index++)
                {
                    switch (index)
                    {
                        case 0:
                            m_rbtnInpPin1.BackColor = inputPinStatus[index] == true ? onColor : offColor;

                            break;
                        case 1:
                            m_rbtnInpPin2.BackColor = inputPinStatus[index] == true ? onColor : offColor;
                            break;
                        case 2:
                            m_rbtnInpPin3.BackColor = inputPinStatus[index] == true ? onColor : offColor;
                            break;
                        case 3:
                            m_rbtnInpPin4.BackColor = inputPinStatus[index] == true ? onColor : offColor;
                            break;
                        case 4:
                            m_rbtnInpPin5.BackColor = inputPinStatus[index] == true ? onColor : offColor;
                            break;
                        case 5:
                            m_rbtnInpPin6.BackColor = inputPinStatus[index] == true ? onColor : offColor;
                            break;
                        default:

                            break;

                    }
                }

            }
            catch
            {
            }
 
        }

        private void InvokeUpdateStatusMonitor(object sender, ReaderEventArgs args)
        {
            try
            {
                this.BeginInvoke(new AppReaderEventHandler(UpdateStatusMonitor), new object[] { sender, args });
            }
            catch { }
        }

        private void UpdateStatusMonitor(object sender, ReaderEventArgs args)
        {
            try
            {
                ManagementEventArgs arg = (ManagementEventArgs)args;
                if (arg.Equals(null))
                    return;
                try
                {
                    if (arg.ReaderStatus != ReaderStatus.ONLINE)
                    {
                        m_lblActive.Visible = false;
                        m_lblInActive.Visible = true;
                        m_btnConnect.Enabled = true;
                        m_btnDisconnect.Enabled = false;
                        m_lblAutoPoll.Text = "Autonomous Mode:OFF";
                        m_lblAutoPoll.BackColor = Color.Tan;
                        
                        if (m_ClsReader.model == ReaderModel.RD5000)
                        {
                            m_lblRFIDStatus.Text = "RFID Module : INACTIVE";
                            m_lblRFIDStatus.BackColor = System.Drawing.Color.Tan;
                            SetProximProgBarDisp(false);
                            SetMotionProgBarDisp(false);
                        }
                    }
                    else
                    {
                        m_lblActive.Visible = true;
                        m_lblInActive.Visible = false;
                        m_btnConnect.Enabled = false;
                        m_btnDisconnect.Enabled = true;

                        if (m_ClsReader.GetReadMode() == ReadMode.AUTONOMOUS)
                        {
                            m_lblAutoPoll.Text = "Autonomous Mode:ON";
                            m_lblAutoPoll.BackColor = Color.DarkSeaGreen;

                        }
                        if (m_ClsReader.model == ReaderModel.RD5000)
                        {
                            SetProximProgBarDisp(true);
                            SetMotionProgBarDisp(true);
                        }
                    }

                    //if (m_ClsReader.model == ReaderModel.RD5000)
                    //{
                    //    if (!m_ClsReader.GetRFIDModuleStatus())
                    //    {
                    //        m_lblRFIDStatus.Text = "RFID Module : INACTIVE";
                    //        m_lblRFIDStatus.BackColor = Color.Tan;
                    //    }
                    //    else
                    //    {
                    //        m_lblRFIDStatus.Text = "RFID Module : ACTIVE";
                    //        m_lblRFIDStatus.BackColor = Color.DarkSeaGreen;
                    //    }
                    //}
                }
                catch { }


                try
                {
                    if (arg.ReaderStatus != ReaderStatus.ONLINE)
                    {
                        m_rdiobtnantena1.BackColor = Color.Red;
                        m_rdiobtnantena2.BackColor = Color.Red;
                        m_rdiobtnantena3.BackColor = Color.Red;
                        m_rdiobtnantena4.BackColor = Color.Red;

                        if (m_ClsReader.model == ReaderModel.XR480)
                        {
                            m_rdiobtnantena5.BackColor = Color.Red;
                            m_rdiobtnantena6.BackColor = Color.Red;
                            m_rdiobtnantena7.BackColor = Color.Red;
                            m_rdiobtnantena8.BackColor = Color.Red;
                        }

                        return;
                    }
                    AntennaConfig[] antennaStatus = arg.AntennaStatus;
                    //int noOfAntennae = antennaStatus.Length;
                    m_rdiobtnantena1.BackColor = Color.Red;
                    m_rdiobtnantena2.BackColor = Color.Red;
                    m_rdiobtnantena3.BackColor = Color.Red;
                    m_rdiobtnantena4.BackColor = Color.Red;

                    if (m_ClsReader.model == ReaderModel.XR480)
                    {
                        m_rdiobtnantena5.BackColor = Color.Red;
                        m_rdiobtnantena6.BackColor = Color.Red;
                        m_rdiobtnantena7.BackColor = Color.Red;
                        m_rdiobtnantena8.BackColor = Color.Red;
                    }

                    int portNo = 0;
                    if (antennaStatus == null)
                        return;
                
                    foreach (AntennaConfig antenna in antennaStatus)
                    {
                        portNo = antenna.PortNumber;
                        if (antenna.IsConnected && antenna.IsEnabled)
                        {
                            switch (portNo)
                            {
                                case 1:
                                    m_rdiobtnantena1.BackColor = Color.SeaGreen;
                                    break;
                                case 2:
                                    m_rdiobtnantena2.BackColor = Color.SeaGreen;
                                    break;
                                case 3:
                                    m_rdiobtnantena3.BackColor = Color.SeaGreen;
                                    break;
                                case 4:
                                    m_rdiobtnantena4.BackColor = Color.SeaGreen;
                                    break;
                                case 5:
                                    m_rdiobtnantena5.BackColor = Color.SeaGreen;
                                    break;
                                case 6:
                                    m_rdiobtnantena6.BackColor = Color.SeaGreen;
                                    break;
                                case 7:
                                    m_rdiobtnantena7.BackColor = Color.SeaGreen;
                                    break;
                                case 8:
                                    m_rdiobtnantena8.BackColor = Color.SeaGreen;
                                    break;
                            }
                        }
                        else if(!antenna.IsEnabled)  // antenna is connected but disabled
                        {
                            switch (portNo)
                            {
                                case 1:
                                    m_rdiobtnantena1.BackColor = Color.Tan;
                                    break;
                                case 2:
                                    m_rdiobtnantena2.BackColor = Color.Tan;
                                    break;
                                case 3:
                                    m_rdiobtnantena3.BackColor = Color.Tan;
                                    break;
                                case 4:
                                    m_rdiobtnantena4.BackColor = Color.Tan;
                                    break;
                                case 5:
                                    m_rdiobtnantena5.BackColor = Color.Tan;
                                    break;
                                case 6:
                                    m_rdiobtnantena6.BackColor = Color.Tan;
                                    break;
                                case 7:
                                    m_rdiobtnantena7.BackColor = Color.Tan;
                                    break;
                                case 8:
                                    m_rdiobtnantena8.BackColor = Color.Tan;
                                    break;
                            }
                        }


                    }
                }
                catch { }
                if (m_ClsReader.model == ReaderModel.XR400 || m_ClsReader.model == ReaderModel.XR440 ||
                     m_ClsReader.model == ReaderModel.XR450 ||m_ClsReader.model == ReaderModel.XR480)
                {
                    try
                    {
                        IOPinStatus[] inputPinsStatus = null;
                        IOPinStatus[] outputPinsStatus = null;
                        m_ClsReader.GetPinlevels(out inputPinsStatus, out outputPinsStatus);
                        SetInputPinsDisplay(inputPinsStatus);
                    }
                    catch { }
                }
            }
            catch { }
            
        }

        void InvokeProximitySensorEvent(object sender, ReaderEventArgs args)
        {
            try
            {
                this.BeginInvoke(new AppReaderEventHandler(UpdateProximitySensor), new object[] { sender, args });
            }
            catch
            {
            }
        }

        private void UpdateProximitySensor(object sender, ReaderEventArgs args)
        {
            try
            {
                ProximityEventArgs arg = (ProximityEventArgs)args;

                if (arg == null)
                    return;

                proximityProgBar.Value = (int)arg.Proximity; ;

                m_lblProximity.Text = "Proximity : " + arg.Proximity.ToString();

            }
            catch { }
        }

        void InvokeMotionSensorEvent(object sender, ReaderEventArgs args)
        {
            try
            {
                this.BeginInvoke(new AppReaderEventHandler(UpdateMotionSensor), new object[] { sender, args });
            }
            catch
            {
            }
        }
        private void UpdateMotionSensor(object sender, ReaderEventArgs args)
        {
            try
            {
                MotionEventArgs arg = (MotionEventArgs)args;

                if (arg == null)
                    return;

                motionXProgBar.Value = (int)arg.XMotion;
                motionYProgBar.Value = (int)arg.YMotion;
                motionZProgBar.Value = (int)arg.ZMotion;

                m_lblMotionX.Text = "Motion  X : " + arg.XMotion.ToString();
                m_lblMotionY.Text = "Motion  Y : " + arg.YMotion.ToString();
                m_lblMotionZ.Text = "Motion  Z : " + arg.ZMotion.ToString();
            }
            catch { }
          

        }

        void m_ClsReader_appOnRFIDStatusMonitor(object sender, ReaderEventArgs args)
        {
            try
            {
                this.BeginInvoke(new AppReaderEventHandler(UpdateRFIDStatus), new object[] { sender, args });
            }
            catch
            {
            }
        }

        private void UpdateRFIDStatus(object sender, ReaderEventArgs args)
        {
            try
            {
                RFIDStatusMonitorEventArgs arg = (RFIDStatusMonitorEventArgs)args;

                if (arg == null)
                    return;
                if (arg.EnableRFID)
                {
                    m_lblRFIDStatus.Text = "RFID Module : ACTIVE";
                    m_lblRFIDStatus.BackColor = System.Drawing.Color.DarkSeaGreen;
                }
                else
                {
                    m_lblRFIDStatus.Text = "RFID Module : INACTIVE";
                    m_lblRFIDStatus.BackColor = System.Drawing.Color.Tan;
                    m_lblAutoPoll.Text = "Autonomous Mode:OFF";
                    m_lblAutoPoll.BackColor = Color.Tan;
                }
                
            }
            catch { }


        }

#endregion Event Handlers

        private void CallStartAutoMode()
        {
            try
            {
               
                m_ClsReader.StartAutonomousMode();
                //IRFIDTag tg = null; 
                //TagEventArgs tag =new SymTagEventArgs("XR480",tg);
                //UpdateListView(null,tag);
                
            }
            catch(Exception ex)
            {
                m_tabPgCmdOutput.BringToFront();
                m_tabPgCmdOutput.Focus();
                m_tabCtlOutput.SelectedIndex = 1;
                throw ex;
            }
            m_lblAutoPoll.Text = "Autonomous Mode:ON";
            m_lblAutoPoll.BackColor = Color.DarkSeaGreen;
        }
        private void CallStopAutoMode()
        {
            try
            {
                m_ClsReader.StopAutonomousMode();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        private void CallGetTags()
        {
            try
            {
                IRFIDTag[] tagsRead = m_ClsReader.GetTags();
                //IRFIDTag tag1 = new SymEPCTag(new byte[] { 1, 2, 3, 4, 1, 2, 3, 4 });
                //IRFIDTag tag2 = new SymEPCTag(new byte[] { 1, 2, 3, 4, 5, 6, 1, 2, 3, 4, 5, 6 });
                //IRFIDTag[] tagsRead = new IRFIDTag[] { tag1, tag2 };
                listView2.Items.Insert(0, new ListViewItem());

                UpdateResponse(tagsRead);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }
        private void CallGetInputPinLevel()
        {
            IOPinStatus[] inputPinsStatus = null;
            IOPinStatus[] outputPinsStatus = null;
            try
            {
                m_ClsReader.GetPinlevels(out inputPinsStatus, out outputPinsStatus);
                for (int i = (outputPinsStatus.Length - 1); i >= 0; i--)
                {
                    listView2.Items.Insert(0, new ListViewItem(String.Format("Output Pin {0} : {1}", i.ToString(), outputPinsStatus[i].pinStatus.ToString())));
                    listView2.Items[0].ForeColor = Color.Green;
                }

                listView2.Items.Insert(0, new ListViewItem());

                for (int i = (inputPinsStatus.Length - 1); i >= 0; i--)
                {
                    listView2.Items.Insert(0, new ListViewItem(String.Format("Input Pin {0} :    {1}", i.ToString(), inputPinsStatus[i].pinStatus.ToString())));
                    listView2.Items[0].ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        private void CallSetOutputPinLevel()
        {
            try
            {

               
            IOPinStatus[] selectedOutPins = null;
            selectedOutPins = new IOPinStatus[m_chkbxlstoutpins.Items.Count];
            selectedOutPins[0].pin = IOPins.PIN_0;
            selectedOutPins[1].pin = IOPins.PIN_1;
            selectedOutPins[2].pin = IOPins.PIN_2;
            selectedOutPins[3].pin = IOPins.PIN_3;
            selectedOutPins[4].pin = IOPins.PIN_4;
            selectedOutPins[5].pin = IOPins.PIN_5;
            selectedOutPins[0].pinStatus = false;
            selectedOutPins[1].pinStatus = false;
            selectedOutPins[2].pinStatus = false;
            selectedOutPins[3].pinStatus = false;
            selectedOutPins[4].pinStatus = false;
            selectedOutPins[5].pinStatus = false;
           
            for(int i = 0 ;i < m_chkbxlstoutpins.Items.Count; i++)
            {
                if (m_chkbxlstoutpins.CheckedIndices.Contains(i))
                {
                    
                    selectedOutPins[i].pinStatus = true;
                }
                else
                {
                    selectedOutPins[i].pinStatus = false;
                }

            }
               m_ClsReader.SetOutputPinlevels(selectedOutPins);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void m_btnReaderInfo_Click(object sender, EventArgs e)
        {
            try
            {
                m_ClsReader.InitializeParameters();
                frmReaderInfo readerInfo = new frmReaderInfo(m_ClsReader);
                readerInfo.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Reader Info Initialization Error","Reader Info Form", MessageBoxButtons.OKCancel);
                //throw ex;
            }
        }

        private void m_btnAntennaSetting_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                AntennaConfig antConfig;
                AntennaConfig[] arrAntennaConfig = m_ClsReader.GetAntennas();
                //dummy
                //AntennaConfig[] arrAntennaConfig = m_ClsReader.SetDummyAntenna();

                frmSetAntenna f_setAntenna = new frmSetAntenna(m_ClsReader.GetAntennaNames(), m_ClsReader.model);
                //dummy
                //frmSetAntenna f_setAntenna = new frmSetAntenna(m_ClsReader.GetDummyAntennaNames());
                f_setAntenna.AllAntenna = arrAntennaConfig;
                f_setAntenna.m_txMax = m_ObjReader.MaxTxPower;
                f_setAntenna.m_txMin = m_ObjReader.MinTxPower;

                f_setAntenna.ShowDialog();
                if (f_setAntenna.setAntennaConfig)
                {
                    antConfig = f_setAntenna.SelectedAntenna;
                    m_ClsReader.SetAntennaConfiguration(antConfig);

                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    MessageBox.Show(ex.Message + " . \n" + ex.InnerException.Message, "Symbol Reader WinApp ");
                else
                    MessageBox.Show(ex.Message, "Symbol Reader WinApp ");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void frmTest_Resize(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }

        private void m_btnreaderCapability_Click(object sender, EventArgs e)
        {
            try
            {
                frmReaderCapabilities frmReadCapabilities = new frmReaderCapabilities();
                frmReadCapabilities.Capabilty = m_ClsReader.ReaderCapability;
                frmReadCapabilities.ShowDialog();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    MessageBox.Show(ex.Message + " . \n " + ex.InnerException.Message, "Symbol Reader WinApp ");
                else
                    MessageBox.Show(ex.Message, "Symbol Reader WinApp ");
            }

        }

        private void m_lblRFIDStatus_Click(object sender, EventArgs e)
        {

        }

        //internal void SetMask(Symbol.RFID2.SymTagMask tagMask)
        //{
        //    m_ClsReader.SetMask(tagMask);
        //}
	}
}
