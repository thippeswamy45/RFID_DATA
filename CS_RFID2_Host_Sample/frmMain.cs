using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Reflection;

using System.IO;
using System.Text;
using Symbol.RFID2;
//using WTSDK;

namespace CS_RFID2_Host_Sample
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
        

        private const string m_ReaderName = "READER_01";
        private const string m_ReaderModel = "RD5000";
       // private Hashtable htInParams;


		#region Private Variables
		private System.Windows.Forms.MainMenu m_mainMenu1;
		private System.Windows.Forms.MenuItem m_mnuItemConfigure;
		private System.Windows.Forms.MenuItem m_mnuItemAdd;
		private System.Windows.Forms.MenuItem m_mnuItmTestReader;
		private System.Windows.Forms.MenuItem m_mnuItemHelp;
		private System.Windows.Forms.ContextMenu m_contextMenu1;
        private System.Windows.Forms.MenuItem m_CMnuTest;
		private System.Windows.Forms.MenuItem m_CMnuDelete;
		private System.Windows.Forms.MenuItem m_CMnuConnect;
		private System.Windows.Forms.MenuItem m_CMnuDisConnect;
		private System.Windows.Forms.MenuItem m_mnuItemDeleteReader;

		private MenuItem mnuItem;

        public Hashtable m_hashTestForm = new Hashtable();
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.MenuItem m_mItmAbout;
		private System.Windows.Forms.MenuItem m_mnuItemExit;
		private System.Windows.Forms.MenuItem m_mnuItmSaveCmdOut;
		private System.Windows.Forms.MenuItem m_munItmSaveTagInfo;
		private System.Windows.Forms.SaveFileDialog m_saveFileDialog;
        private System.Windows.Forms.MenuItem m_mnuItmUtilites;
        private MenuItem menuMonitorIntrvl;
        private MenuItem m_munItmDebugMode;
        private MenuItem menuOff;
        private MenuItem menuError;
        private MenuItem menuInfo;
        private MenuItem menuVerbose;
        private Panel panel1;
        private TreeView m_readerTree;
        private Panel panel2;
     //   private MenuItem menuSetMask;
		private System.ComponentModel.IContainer components;
		#endregion Private Variables

		#region Constructor
		public frmMain()
		{

            frmSplashScreen objSplashScreen = new frmSplashScreen();
            objSplashScreen.ShowDialog(this);
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            m_readerTree.SelectedNode = m_readerTree.Nodes[0];

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

           

		}

		#endregion Constructor
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// 
		#region Dispose Method
		protected override void Dispose( bool disposing )
		{
            try 
            {
                ReaderFactory.DeleteAllReaders();
            }
            catch { }
			if( disposing )
			{

				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion Dispose Method

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Readers");
            this.m_mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.m_mnuItemConfigure = new System.Windows.Forms.MenuItem();
            this.m_mnuItemAdd = new System.Windows.Forms.MenuItem();
            this.m_mnuItemExit = new System.Windows.Forms.MenuItem();
            this.m_mnuItmTestReader = new System.Windows.Forms.MenuItem();
            this.m_mnuItemDeleteReader = new System.Windows.Forms.MenuItem();
            this.m_mnuItmUtilites = new System.Windows.Forms.MenuItem();
            this.menuMonitorIntrvl = new System.Windows.Forms.MenuItem();
            this.m_munItmDebugMode = new System.Windows.Forms.MenuItem();
            this.menuOff = new System.Windows.Forms.MenuItem();
            this.menuError = new System.Windows.Forms.MenuItem();
            this.menuInfo = new System.Windows.Forms.MenuItem();
            this.menuVerbose = new System.Windows.Forms.MenuItem();
            this.m_mnuItmSaveCmdOut = new System.Windows.Forms.MenuItem();
            this.m_munItmSaveTagInfo = new System.Windows.Forms.MenuItem();
            this.m_mnuItemHelp = new System.Windows.Forms.MenuItem();
            this.m_mItmAbout = new System.Windows.Forms.MenuItem();
            this.m_contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.m_CMnuTest = new System.Windows.Forms.MenuItem();
            this.m_CMnuDelete = new System.Windows.Forms.MenuItem();
            this.m_CMnuConnect = new System.Windows.Forms.MenuItem();
            this.m_CMnuDisConnect = new System.Windows.Forms.MenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_readerTree = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_mainMenu1
            // 
            this.m_mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_mnuItemConfigure,
            this.m_mnuItmTestReader,
            this.m_mnuItemDeleteReader,
            this.m_mnuItmUtilites,
            this.m_mnuItemHelp});
            // 
            // m_mnuItemConfigure
            // 
            this.m_mnuItemConfigure.Index = 0;
            this.m_mnuItemConfigure.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_mnuItemAdd,
            this.m_mnuItemExit});
            this.m_mnuItemConfigure.Text = "Configure";
            // 
            // m_mnuItemAdd
            // 
            this.m_mnuItemAdd.Index = 0;
            this.m_mnuItemAdd.Text = "Add Reader";
            this.m_mnuItemAdd.Click += new System.EventHandler(this.mnuItemAdd_Click);
            // 
            // m_mnuItemExit
            // 
            this.m_mnuItemExit.Index = 1;
            this.m_mnuItemExit.Text = "Exit";
            this.m_mnuItemExit.Click += new System.EventHandler(this.m_mnuItemExit_Click);
            // 
            // m_mnuItmTestReader
            // 
            this.m_mnuItmTestReader.Index = 1;
            this.m_mnuItmTestReader.Text = "Test Reader";
            // 
            // m_mnuItemDeleteReader
            // 
            this.m_mnuItemDeleteReader.Index = 2;
            this.m_mnuItemDeleteReader.Text = "Delete Reader";
            this.m_mnuItemDeleteReader.Click += new System.EventHandler(this.m_mnuItemDeleteReader_Click);
            // 
            // m_mnuItmUtilites
            // 
            this.m_mnuItmUtilites.Index = 3;
            this.m_mnuItmUtilites.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuMonitorIntrvl,
            this.m_munItmDebugMode,
            this.m_mnuItmSaveCmdOut,
            this.m_munItmSaveTagInfo});
            this.m_mnuItmUtilites.Text = "Utilities";
            // 
            // menuMonitorIntrvl
            // 
            this.menuMonitorIntrvl.Index = 0;
            this.menuMonitorIntrvl.Text = "Set Monitor Interval";
            this.menuMonitorIntrvl.Click += new System.EventHandler(this.menuMonitorIntrvl_Click);
            // 
            // m_munItmDebugMode
            // 
            this.m_munItmDebugMode.Index = 1;
            this.m_munItmDebugMode.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuOff,
            this.menuError,
            this.menuInfo,
            this.menuVerbose});
            this.m_munItmDebugMode.Text = "Enable Logging";
            this.m_munItmDebugMode.Click += new System.EventHandler(this.m_munItmDebugMode_Click);
            // 
            // menuOff
            // 
            this.menuOff.Index = 0;
            this.menuOff.RadioCheck = true;
            this.menuOff.Text = "Off";
            this.menuOff.Click += new System.EventHandler(this.menuOff_Click);
            // 
            // menuError
            // 
            this.menuError.Index = 1;
            this.menuError.RadioCheck = true;
            this.menuError.Text = "Error";
            this.menuError.Click += new System.EventHandler(this.menuOff_Click);
            // 
            // menuInfo
            // 
            this.menuInfo.Index = 2;
            this.menuInfo.RadioCheck = true;
            this.menuInfo.Text = "Info";
            this.menuInfo.Click += new System.EventHandler(this.menuOff_Click);
            // 
            // menuVerbose
            // 
            this.menuVerbose.Index = 3;
            this.menuVerbose.RadioCheck = true;
            this.menuVerbose.Text = "Verbose";
            this.menuVerbose.Click += new System.EventHandler(this.menuOff_Click);
            // 
            // m_mnuItmSaveCmdOut
            // 
            this.m_mnuItmSaveCmdOut.Index = 2;
            this.m_mnuItmSaveCmdOut.Text = "Save Command Output";
            this.m_mnuItmSaveCmdOut.Click += new System.EventHandler(this.m_mnuItmSaveCmdOut_Click);
            // 
            // m_munItmSaveTagInfo
            // 
            this.m_munItmSaveTagInfo.Index = 3;
            this.m_munItmSaveTagInfo.Text = "Save Live Tag Data";
            this.m_munItmSaveTagInfo.Click += new System.EventHandler(this.m_munItmSaveTagInfo_Click);
            // 
            // m_mnuItemHelp
            // 
            this.m_mnuItemHelp.Index = 4;
            this.m_mnuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_mItmAbout});
            this.m_mnuItemHelp.Text = "Help";
            // 
            // m_mItmAbout
            // 
            this.m_mItmAbout.Index = 0;
            this.m_mItmAbout.Text = "About";
            this.m_mItmAbout.Click += new System.EventHandler(this.m_mItmAbout_Click);
            // 
            // m_contextMenu1
            // 
            this.m_contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_CMnuTest,
            this.m_CMnuDelete,
            this.m_CMnuConnect,
            this.m_CMnuDisConnect});
            this.m_contextMenu1.Popup += new System.EventHandler(this.m_contextMenu1_Popup);
            // 
            // m_CMnuTest
            // 
            this.m_CMnuTest.Index = 0;
            this.m_CMnuTest.Text = "Test";
            this.m_CMnuTest.Click += new System.EventHandler(this.m_CMnuTest_Click);
            // 
            // m_CMnuDelete
            // 
            this.m_CMnuDelete.Index = 1;
            this.m_CMnuDelete.Text = "Delete";
            this.m_CMnuDelete.Click += new System.EventHandler(this.m_CMnuDelete_Click);
            // 
            // m_CMnuConnect
            // 
            this.m_CMnuConnect.Index = 2;
            this.m_CMnuConnect.Text = "Connect";
            this.m_CMnuConnect.Click += new System.EventHandler(this.m_CMnuConnect_Click);
            // 
            // m_CMnuDisConnect
            // 
            this.m_CMnuDisConnect.Index = 3;
            this.m_CMnuDisConnect.Text = "Disconnect";
            this.m_CMnuDisConnect.Click += new System.EventHandler(this.m_CMnuDisConnect_Click);
            // 
            // m_saveFileDialog
            // 
            this.m_saveFileDialog.DefaultExt = "*.txt,*.doc";
            this.m_saveFileDialog.Title = "Save File";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_readerTree);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(154, 376);
            this.panel1.TabIndex = 7;
            // 
            // m_readerTree
            // 
            this.m_readerTree.BackColor = System.Drawing.Color.White;
            this.m_readerTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_readerTree.ContextMenu = this.m_contextMenu1;
            this.m_readerTree.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_readerTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_readerTree.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_readerTree.ForeColor = System.Drawing.Color.Navy;
            this.m_readerTree.HideSelection = false;
            this.m_readerTree.HotTracking = true;
            this.m_readerTree.Location = new System.Drawing.Point(0, 0);
            this.m_readerTree.Name = "m_readerTree";
            treeNode1.Name = "";
            treeNode1.Text = "Readers";
            this.m_readerTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.m_readerTree.ShowLines = false;
            this.m_readerTree.Size = new System.Drawing.Size(154, 276);
            this.m_readerTree.TabIndex = 9;
            this.m_readerTree.DoubleClick += new System.EventHandler(this.m_CMnuTest_Click);
            this.m_readerTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.m_readerTree_NodeMouseClick);
            this.m_readerTree.MouseHover += new System.EventHandler(this.m_readerTree_MouseHover);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 276);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(154, 100);
            this.panel2.TabIndex = 8;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(728, 376);
            this.Controls.Add(this.panel1);
            this.IsMdiContainer = true;
            this.Menu = this.m_mainMenu1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Symbol Reader Application";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// 
		#region Main Method
		[STAThread]
		static void Main() 
		{
			try
			{

				//Application.Run(new frmMain());
                ClsReader.RunSingleInstance(new frmMain());
			}
			catch
			{
				//Console.WriteLine("Exception in Main ---"+ ex.ToString());   
                Application.Exit();
			}
		}
		#endregion Main Method


		#region Public Method
		public void RefreshTree()
		{
			try
			{
				m_mnuItmTestReader.MenuItems.Clear();
				m_readerTree.Nodes[0].Nodes.Clear();
				foreach(string strTreeHeader in m_hashTestForm.Keys)
				{
					NewReaderAdded(strTreeHeader);
				}
				
			}
			catch(Exception ex)
			{
                if (ex.InnerException!=null)
                {

                    MessageBox.Show("Unable to refresh reader list : " + ex.Message +" " + ex.InnerException.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Unable to refresh reader list : " + ex.Message , this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
			}
			finally
			{
			}
		}

		public void OpenTestWindow(string keyName)
		{
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				frmTest objfrmTest = (frmTest)m_hashTestForm[keyName];
                if (objfrmTest != null)
                {
                    objfrmTest.MdiParent = this;
                    objfrmTest.Show();
                    //objfrmTest.LoadTestWindow();
                    objfrmTest.Visible = true;
                    objfrmTest.Activate();
                }
			}
			catch(Exception ex)
			{
                if(ex.InnerException!=null)
				    MessageBox.Show(ex.Message + " " + ex.InnerException.Message,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                else
                    MessageBox.Show(ex.Message , this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		#endregion Public Method

		
		#region Private Methods
		
		private void mnuItemAdd_Click(object sender, System.EventArgs e)
		{
			frmAdd addScreen = new frmAdd();
			addScreen.ShowDialog(this);
		}
		private void frmMain_Load(object sender, System.EventArgs e)
		{
            try
            {
                frmAdd addScreen = new frmAdd();
                addScreen.ShowDialog(this);
            }
            catch
            {
            }
              try
                {
                    switch (ReaderFactory.LogLevel)
                    {
                        case Level.Off:
                            menuOff.Checked = true;
                            break;
                       
                        //case  Level.Fatal:
                        //    menuFatal.Checked = true;
                        //    break;
                        
                        case Level.Error:
                            menuError.Checked = true;
                            break;
                        
                        //case Level.Warning:
                        //    menuWarn.Checked = true;
                        //    break;
                        
                        case Level.Info:
                            menuInfo.Checked = true;
                            break;
                        
                        case Level.Verbose:
                            menuVerbose.Checked = true;
                            break;
                    }
                }
                catch { }
            
            
		}

		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Dispose(true);
            Application.Exit();
		}

		private void m_mnuItemExit_Click(object sender, System.EventArgs e)
		{
			this.Dispose(true);
            Application.Exit();
		}

		private void NewReaderAdded(string strReaderName)
		{
			m_readerTree.Nodes[0].Nodes.Add(strReaderName);

			mnuItem = new MenuItem(strReaderName);
			this.mnuItem.Click += new System.EventHandler(this.mnuOpenTestWindow);
			m_mnuItmTestReader.MenuItems.Add(mnuItem);
			
		}

		private void m_CMnuTest_Click(object sender, System.EventArgs e)
		{
            if (!m_readerTree.SelectedNode.Text.Equals("Reader"))
                OpenTestWindow(m_readerTree.SelectedNode.Text);
            
		}

		private void mnuOpenTestWindow(object sender, System.EventArgs e)
		{
			OpenTestWindow(((MenuItem)sender).Text);
		}

		private void m_readerTree_MouseHover(object sender, System.EventArgs e)
		{
			try
			{
				if(m_hashTestForm.Contains(m_readerTree.SelectedNode.Text))
				{
					frmTest objTest = (frmTest)m_hashTestForm[m_readerTree.SelectedNode.Text];
					toolTip.SetToolTip(m_readerTree,objTest.ReaderInfo);
					toolTip.AutoPopDelay = 1000 ;
				}
				else
				{
					toolTip.SetToolTip(m_readerTree,"Symbol RFID Readers");
				}
			}
			catch
			{
			}
		}
		

		private void m_CMnuDelete_Click(object sender, System.EventArgs e)
		{
            if (!m_readerTree.SelectedNode.Text.Equals("Reader"))
            {
                DeleteReader(m_readerTree.SelectedNode.Text);
                
            }
		}

		private void DeleteReader(string keyName)
		{
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				DialogResult diagResult = MessageBox.Show("Are you sure, you want to delete "+keyName+" reader",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Information);
				if(diagResult == DialogResult.Yes)
				{
					if(m_hashTestForm.Contains(keyName))
					{
						frmTest objTest = (frmTest)m_hashTestForm[keyName];

						objTest.Dispose();
						m_hashTestForm.Remove(keyName);
                        ReaderFactory.DeleteReader(keyName);
                        RefreshTree();
					}

				}
			}
			catch(Exception ex)
			{
                if(ex.InnerException!=null)
				    MessageBox.Show("Unable to delete : "+ ex.Message + " " + ex.InnerException.Message ,this.Text, MessageBoxButtons.OK,MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Unable to delete : " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}
		
	
		private void m_mnuItemDeleteReader_Click(object sender, System.EventArgs e)
		{
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				frmDelete objfrmDelete = new frmDelete();
				objfrmDelete.ShowDialog(this);
				RefreshTree();
			
			}
			catch(Exception ex)
			{
                if (ex.InnerException!=null)
                    MessageBox.Show("Unable to open delete window : " + ex.Message + " " + ex.InnerException.Message,this.Text, MessageBoxButtons.OK,MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Unable to open delete window : " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			finally
			{
			}
		}

		private void m_mItmAbout_Click(object sender, System.EventArgs e)
		{
            try
            {
                FrmAbout objFrmAbout = new FrmAbout();
                objFrmAbout.ShowDialog(this);
            }
            catch { }
		}

		private void m_CMnuConnect_Click(object sender, System.EventArgs e)
		{
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				if(m_hashTestForm.Contains(m_readerTree.SelectedNode.Text))
				{
					frmTest objTest = (frmTest)m_hashTestForm[m_readerTree.SelectedNode.Text];
					objTest.Connect();
				}

				
			}
			catch(Exception ex)
			{
                if(ex.InnerException!=null)
				    MessageBox.Show("Unable to connect : "+ex.Message +" "+ ex.InnerException.Message ,this.Text, MessageBoxButtons.OK,MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Unable to connect : " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void m_CMnuDisConnect_Click(object sender, System.EventArgs e)
		{
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				if(m_hashTestForm.Contains(m_readerTree.SelectedNode.Text))
				{
					frmTest objTest = (frmTest)m_hashTestForm[m_readerTree.SelectedNode.Text];
					objTest.Disconnect();
				}

				
			}
			catch(Exception ex)
			{
               if(ex.InnerException!=null) 
				    MessageBox.Show("Unable to disconnect : "+ex.Message + " " + ex.InnerException.Message,this.Text, MessageBoxButtons.OK,MessageBoxIcon.Warning);
               else
                    MessageBox.Show("Unable to disconnect : " + ex.Message , this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void m_mnuItmSaveCmdOut_Click(object sender, System.EventArgs e)
		{
			try
			{
				m_saveFileDialog.Title = "Save Command Output";
			
				ListView lv = null;
				if(m_hashTestForm.Count == 0)
				{
					MessageBox.Show("No reader is configured" ,this.Text, MessageBoxButtons.OK,MessageBoxIcon.Warning);
					return;
				}
				foreach(frmTest objfrmTest in m_hashTestForm.Values)
				{
					if(objfrmTest.ContainsFocus)
						lv = objfrmTest.CommandOutput;
				}
				if(lv != null)
				{
					

					if( m_saveFileDialog.ShowDialog() != DialogResult.Cancel )
					{
						Stream saveStream ;
						if((saveStream = m_saveFileDialog.OpenFile()) != null)
						{
							Cursor prevCursor = Cursor.Current;
							Cursor.Current = Cursors.WaitCursor;
							//Create a file to write to.
							using (StreamWriter sw = new StreamWriter(saveStream)) 
							{
								lock(lv)
								{
									foreach(ListViewItem lvi in lv.Items)
									{
										string msg = "";
										foreach(ListViewItem.ListViewSubItem subItem in lvi.SubItems)
										{
											msg += subItem.Text +"   ";	
										}
										sw.WriteLine(msg);
									}
								}
							}
							saveStream.Close();
							MessageBox.Show("The file has been successfully saved.\r\n" ,this.Text, MessageBoxButtons.OK,MessageBoxIcon.Information);
					
							//mnuFileSave.Enabled = false;
							Cursor.Current = prevCursor ;
						}
					}

				}
				else
				{
					MessageBox.Show("No reader test window is active" ,this.Text, MessageBoxButtons.OK,MessageBoxIcon.Warning);
				}
			}
			catch(IOException ex)
			{
               if(ex.InnerException!=null) 
				    MessageBox.Show("Cannot save the file : "+ex.Message + " " + ex.InnerException.Message ,this.Text, MessageBoxButtons.OK,MessageBoxIcon.Warning);
               else
                    MessageBox.Show("Cannot save the file : " + ex.Message , this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			
			}

		}

		private void m_munItmSaveTagInfo_Click(object sender, System.EventArgs e)
		{
		
			try
			{
				m_saveFileDialog.Title = "Save Tag Information";
			
				ListView lv = null;
				if(m_hashTestForm.Count == 0)
				{
					MessageBox.Show("No reader is configured" ,this.Text, MessageBoxButtons.OK,MessageBoxIcon.Warning);
					return;
				}
				foreach(frmTest objfrmTest in m_hashTestForm.Values)
				{
					if(objfrmTest.ContainsFocus)
						lv = objfrmTest.TagListView;
				}
				if(lv != null)
				{
					

					if( m_saveFileDialog.ShowDialog() != DialogResult.Cancel )
					{
						Stream saveStream ;
						if((saveStream = m_saveFileDialog.OpenFile()) != null)
						{
							Cursor prevCursor = Cursor.Current;
							Cursor.Current = Cursors.WaitCursor;
							//Create a file to write to.
							using (StreamWriter sw = new StreamWriter(saveStream)) 
							{
								lock(lv)
								{
									foreach(ListViewItem lvi in lv.Items)
									{
										string msg = "";
										foreach(ListViewItem.ListViewSubItem subItem in lvi.SubItems)
										{
											msg += subItem.Text +"   ";	
										}
										sw.WriteLine(msg);
									}
								}
							}
							saveStream.Close();
							MessageBox.Show("The file has been successfully saved.\r\n" ,this.Text, MessageBoxButtons.OK,MessageBoxIcon.Information);
					
							//mnuFileSave.Enabled = false;
							Cursor.Current = prevCursor ;
						}
					}

				}
				else
				{
					MessageBox.Show("No reader test window is active" ,this.Text, MessageBoxButtons.OK,MessageBoxIcon.Warning);
				}
			}
			catch(IOException ex)
			{
                if (ex.InnerException!=null)
                    MessageBox.Show("Cannot save the file : " + ex.Message + " " + ex.InnerException.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Cannot save the file : " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			
			}
			catch
			{
			}
		}

        #region newly added methods
                
        private void CreateFile(string path)
        {
            try
            {
                XmlTextWriter wr = new XmlTextWriter(@path, Encoding.UTF8);
                wr.WriteStartDocument();

                wr.WriteStartElement("", "ReaderConfig", @"http://www.w3.org/2001/XMLSchema-instance");

                wr.WriteStartElement("ComPortSettings");

                wr.WriteStartElement("COMPort");
                wr.WriteString("COM7");
                wr.WriteEndElement();

                wr.WriteStartElement("BaudRate");
                wr.WriteString("57600");
                wr.WriteEndElement();

                wr.WriteEndElement();
                wr.WriteEndElement();

                wr.WriteEndDocument();
                wr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "RFID SDK Demo");
            }
        }

       

       
        #endregion newly added methods

        private void frmMain_Resize(object sender, EventArgs e)
        {
            Form[] children = this.MdiChildren;
            foreach (Form ch in children)
            {
                //if(ch.Name == frmTest ) 
                ch.Dock = DockStyle.Fill;
            }

        }
        #endregion Private Methods

        

        private void m_readerTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                if (!e.Node.IsSelected)
                    m_readerTree.SelectedNode = e.Node;
        }

        private void m_contextMenu1_Popup(object sender, EventArgs e)
        {
            Point ptClick = m_readerTree.PointToClient(Control.MousePosition);
            
            if(m_readerTree.SelectedNode.Bounds.Contains(ptClick) & (m_readerTree.SelectedNode.Text!="Readers"))
                {
                // Show all the menu items so that the menu will appear
                foreach ( MenuItem item in m_contextMenu1.MenuItems )
                        item.Visible = true;
                }
                else
                {
                // Hide all the menu items so that the menu will not appear
                foreach ( MenuItem item in m_contextMenu1.MenuItems )
                    item.Visible = false;
                }
            }

        private void menuMonitorIntrvl_Click(object sender, EventArgs e)
        {
            FrmMonitorInterval fMonitor = new FrmMonitorInterval();
            fMonitor.ShowDialog();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void m_munItmDebugMode_Click(object sender, EventArgs e)
        {
            
            
        }

      
        private void menuOff_Click(object sender, EventArgs e)
        {
            menuOff.Checked = false;
  //          menuFatal.Checked = false;
            menuError.Checked = false;
//            menuWarn.Checked = false;
            menuInfo.Checked = false;
            menuVerbose.Checked = false;

            switch (((MenuItem)sender).Text)
            { 
                case "Off":
                    menuOff.Checked = true;
                    ReaderFactory.LogLevel = Level.Off;
                    //pzhu Reader.DebugMode = false;
                    break;
                //case "Fatal":
                //    menuFatal.Checked = true;
                //    ReaderFactory.LogLevel = Level.Fatal;
                //    Reader.DebugMode = true;
                //    break;
                case "Error":
                    menuError.Checked = true;
                    ReaderFactory.LogLevel = Level.Error;
                    //pzhu Reader.DebugMode = true;
                    break;
                //case "Warning":
                //    menuWarn.Checked = true;
                //    ReaderFactory.LogLevel = Level.Warning;
                //    Reader.DebugMode = true;
                //    break;
                case "Info":
                    menuInfo.Checked = true;
                    ReaderFactory.LogLevel = Level.Info;
                    //pzhu Reader.DebugMode = true;
                    break;
                case "Verbose":
                    menuVerbose.Checked = true;
                    ReaderFactory.LogLevel = Level.Verbose;
                    //pzhu Reader.DebugMode = true;
                    break;
            
            }
            

        }

  

        //private void menuSetMask_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        FrmSetMask setMask = new FrmSetMask();
        //        setMask.TagMask = ClsReader.getTagMask;
        //        setMask.ShowDialog();
        //        foreach (frmTest test in m_hashTestForm.Values)
        //        {
        //            test.SetMask(setMask.TagMask);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}

        
	}
}
