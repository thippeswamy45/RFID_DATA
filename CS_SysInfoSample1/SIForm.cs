//-----------------------------------------------------------------------------------
// FILENAME: SIForm.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION: The source file for the SIForm (System Information Form).
//
// ----------------------------------------------------------------------------------
//  
//	This sample demonstrates the usage of following C functions in C# in order to
//      obtain the system information of the device.
//	
//		1. SystemParametersInfo(...)
//      2. CAD_GetOemVersionNumber(...)
//		3. CAD_GetOemBuildNumber(...)
//		4. CAD_GetLoaderVersionNumber(...)
//	
// ----------------------------------------------------------------------------------
// 
//-----------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using Symbol.Exceptions;
using System.IO;
using System.Runtime.InteropServices;

namespace CS_SysInfoSample1
{
	/// <summary>
	/// Summary description for SIForm.
	/// </summary>
	public class SIForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ColumnHeader NumberColumn = null;
		private System.Windows.Forms.ColumnHeader ItemColumn = null;
		private System.Windows.Forms.ColumnHeader NameColumn = null;
		private System.Windows.Forms.ColumnHeader ValueColumn = null;

        // The ImageList reference which is used to control the row height of listViewMain.
        //  This is kind of a workaround in the absense of an exposed API to control the 
        //  row height of System.Windows.Forms.ListView.
        private ImageList imageList = null;

		private System.Windows.Forms.ListView listViewSI = null;
        private Panel panel1;

		private Resources MyResources = null;

		private const int SPI_GETOEMINFO = 258;
		private const int MAX_OEM_NAME_LENGTH = 128;
        private const int WCHAR_SIZE = 2;

        private Timer timer1 = null;
        private int selectedIndex = 0;

        System.EventHandler MyActivateHandler = null;
        
		//A formatting string used with listview items
        string itemNumberFormat = "";
		
		const string ROOT = "Main";
		const string ABOUT = "About";
		const string EXITAPP = "ExitApp";
		const string OS_VERSION = "OSVersion";
		const string OEM_NAME= "OEMName";
		const string OEM_VERSION = "OEMVersion";
		const string IPL_VERSION = "IPLVersion";

		private System.Windows.Forms.StatusBar statusBar1 = null;

        // The factor(n) which defines the row height of the ListView. 
        //  The default row height would be multiplied by this factor (ROW_HEIGHT_FACTOR = n).
        private const int ROW_HEIGHT_FACTOR = 2; // Currently set to 2. So the row height would be doubled in this sample.
                                                 // The user can set it to any value (i.e. 1,2,3,..., etc.) based on the requirements.

		public SIForm()
		{

			//save the current cursor
			Cursor savedCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor; 

			MyResources = new Resources();
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.ItemColumn.Text = Resources.GetString("Item");
			this.ValueColumn.Text = Resources.GetString("Value");
			this.timer1.Interval = 1000;

			Cursor.Current = savedCursor;
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.listViewSI = new System.Windows.Forms.ListView();
            this.NumberColumn = new System.Windows.Forms.ColumnHeader();
            this.ItemColumn = new System.Windows.Forms.ColumnHeader();
            this.ValueColumn = new System.Windows.Forms.ColumnHeader();
            this.NameColumn = new System.Windows.Forms.ColumnHeader();
            this.imageList = new System.Windows.Forms.ImageList();
            this.timer1 = new System.Windows.Forms.Timer();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewSI
            // 
            this.listViewSI.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewSI.Columns.Add(this.NumberColumn);
            this.listViewSI.Columns.Add(this.ItemColumn);
            this.listViewSI.Columns.Add(this.ValueColumn);
            this.listViewSI.Columns.Add(this.NameColumn);
            this.listViewSI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewSI.FullRowSelect = true;
            this.listViewSI.Location = new System.Drawing.Point(0, 0);
            this.listViewSI.Name = "listViewSI";
            this.listViewSI.Size = new System.Drawing.Size(318, 268);
            this.listViewSI.TabIndex = 1;
            this.listViewSI.View = System.Windows.Forms.View.Details;
            this.listViewSI.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListViewSI_KeyUp);
            this.listViewSI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewSI_KeyDown);
            // 
            // NumberColumn
            // 
            this.NumberColumn.Text = "#";
            this.NumberColumn.Width = 30;
            // 
            // ItemColumn
            // 
            this.ItemColumn.Text = "ColumnHeader";
            this.ItemColumn.Width = 100;
            // 
            // ValueColumn
            // 
            this.ValueColumn.Text = "";
            this.ValueColumn.Width = 108;
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "ColumnHeader";
            this.NameColumn.Width = 0;
            // 
            // imageList
            // 
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 268);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(318, 24);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listViewSI);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 268);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // SIForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(318, 292);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusBar1);
            this.MinimizeBox = false;
            this.Name = "SIForm";
            this.Text = "CS_SysInfoSample1";
            this.Load += new System.EventHandler(this.SIForm_Load);
            this.Resize += new System.EventHandler(this.SIForm_Resize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main() 
		{
            SIForm siForm = new SIForm();
            Application.Run(siForm);
		}

		private void SIForm_Load(object sender, System.EventArgs e)
		{
			MyActivateHandler = new System.EventHandler(this.listViewSI_ItemActivate);
			this.listViewSI.ItemActivate += MyActivateHandler;

			// Add MainMenu if Pocket PC
			if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
			{
				this.Menu = new MainMenu();
			}
	
			loadMainListViewItems();

			//resize the table column width
			listViewSI.Columns[2].Width =this.Width-32;

            setGridLines(this.listViewSI);
            setRowHeight(this.listViewSI);

			// Ensure that the keyboard focus is set on a control.
			this.listViewSI.Focus();
		}

		#region Main listView
		/// <summary>
		/// Add an item to listViewSI
		/// </summary>
		/// <param name="number">the number to display in Number column</param>
		/// <param name="itemName">The string to displsy in Item column</param>
        private void addListViewItem(int number, string itemName, string value )
		{
			string[] item;
            item = new string[] {number.ToString(itemNumberFormat), Resources.GetString(itemName), value, itemName };
			ListViewItem li = new ListViewItem(item);
			listViewSI.Items.Add(li);
		}


		/// <summary>
		/// Add items to the Start page of the Form
		/// </summary>
		private void loadMainListViewItems()
		{
            itemNumberFormat = "";

            int i = 0;
            addListViewItem(i++, EXITAPP, "");
            addListViewItem(i++, OS_VERSION, getOSVersion());
            addListViewItem(i++, OEM_NAME, getOEMName());
			addListViewItem(i++, OEM_VERSION, getOEMVersion());
			addListViewItem(i++, IPL_VERSION, getIPLVersion());
			addListViewItem(i++, ABOUT, "");

		}

		/// <summary>
		/// Refresh the main list view after unloading it
		/// </summary>
		private void loadMainListView()
		{
            //Add column headers
			this.listViewSI.Columns.Add(this.NumberColumn);
			this.listViewSI.Columns.Add(this.ItemColumn);
            this.listViewSI.Columns.Add(this.ValueColumn);
			this.listViewSI.Columns.Add(this.NameColumn);
			this.loadMainListViewItems();
		}
		/// <summary>
		/// Unload the start window
		/// </summary>
		private void unloadMainListView()
		{
			//Remove all items
			this.listViewSI.Clear();
			//Remove column headers
			this.listViewSI.Columns.Remove(this.NumberColumn);
			this.listViewSI.Columns.Remove(this.ItemColumn);
			this.listViewSI.Columns.Remove(this.ValueColumn);
			this.listViewSI.Columns.Remove(this.NameColumn);
			
        }

		/// <summary>
		/// Gives the OS version.
		/// </summary>
		private string getOSVersion()
		{
            string OSVersion = "";

            try
            {
                OSVersion = System.Environment.OSVersion.Version.ToString();

                return OSVersion;
            }
            catch
            {
                return ""; // In case of any exception, return an empty string.
            }
		}

		/// <summary>
		/// Gives the OEM name.
		/// </summary>
		private string getOEMName()
		{
            int numOfBytes = MAX_OEM_NAME_LENGTH * WCHAR_SIZE;

            char[] OEMNameChArr = new char[MAX_OEM_NAME_LENGTH];
            string OEMName = new string(OEMNameChArr);

            int status = SystemParametersInfo(SPI_GETOEMINFO, numOfBytes, OEMName, 0);

            if (System.Convert.ToBoolean(status)) // If the call has succeeded, return OEM Name. 
            {
                return OEMName; 
            }
            else // If failed, return an empty string.
            {
                return "";
            }

		}

		/// <summary>
		/// Gives the OEM version.
		/// </summary>
		private string getOEMVersion()
		{
			System.UInt16 nMajor = 0;
			System.UInt16 nMinor = 0;
			uint nBuild = 0;

			int status = CAD_GetOemVersionNumber (ref nMajor,ref nMinor);
			int statusBuild = CAD_GetOemBuildNumber (ref nBuild);

            if (((System.Convert.ToBoolean(status)) && (System.Convert.ToBoolean(statusBuild)))) // If both calls have succeeded, return OEM Version.
            {
                string sMajor = String.Format("{0:00}", nMajor); //in 2-digits
                string sMinor = String.Format("{0:00}", nMinor); //in 2-digits
                string sBuild = String.Format("{0:0000}", nBuild); //in 4-digits

                return (sMajor + "." + sMinor + "." + sBuild);
            }
            else // If failed, return "00.00.0000".
            {
                return ("00.00.0000");
            }

		}

		/// <summary>
		/// Gives the IPL version.
		/// </summary>
		private string getIPLVersion()
		{
			System.UInt16 nMajor = 0;
			System.UInt16 nMinor = 0;
			
			int status = CAD_GetLoaderVersionNumber (ref nMajor,ref nMinor);

            if (System.Convert.ToBoolean(status)) // If the call has succeeded, return IPL Version. 
            {
                string sMajor = String.Format("{0:00}", nMajor); //in 2-digits
                string sMinor = String.Format("{0:00}", nMinor); //in 2-digits

                return (sMajor + "." + sMinor);
            }
            else // If failed, return "00.00".
            {
                return ("00.00");
            }

		}


		#endregion 


        /// <summary>
		/// This deligate is called when a listview item is double clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewSI_ItemActivate(object sender, System.EventArgs e)
		{
			Cursor savedCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
            try
			{
				//Compare the 4 element in the selected row in the listwiew.
				//This column has been made invisible, but it's used to track the currently active/highlighted row.
				switch (listViewSI.Items[listViewSI.SelectedIndices[0]].SubItems[3].Text)
				{
					case EXITAPP:
						this.Close();
						break;
					case ABOUT:  
                        AboutForm myAbout = new AboutForm();
						Cursor.Current = savedCursor;
                        myAbout.ShowDialog();
						Cursor.Current = Cursors.WaitCursor;
						// Ensure that the keyboard focus is set on a control.
						this.listViewSI.Focus();
                        break;
					default: 
						break;
				}
   			}
			catch(ArgumentOutOfRangeException)
			{
				System.Windows.Forms.MessageBox.Show(Resources.GetString("ItemNotSelected"), "CS_SysInfoSample1");
				this.listViewSI.Focus();
			}
			Cursor.Current = savedCursor;
		}

		/// <summary>
		/// The handler for the KeyUp event.
		/// </summary>
        private void ListViewSI_KeyUp(object sender, KeyEventArgs e)
        {
            this.listViewSI.Focus();
        }

        /// <summary>
        /// The handler for the KeyDown event.
        /// Handle keyboard navigation of the listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewSI_KeyDown(object sender, KeyEventArgs e)
        {
			//process softkeys first of all
			switch(e.KeyCode)
			{
				case Keys.Right:
					this.Close();
					return;
			}

			//Process rest of the keys
            char c = System.Convert.ToChar(e.KeyValue);
            int tmpIndex;

            if ((c >= '0') && (c <= '9'))
            {
                //A number is pressed
                if (timer1.Enabled)
                {//This is the second number being pressed as a pair
                    //stop the timer after the second digit
                    timer1.Enabled = false;
                    tmpIndex = selectedIndex * 10 + (int)(c - '0');
                    if (listViewSI.Items.Count > tmpIndex)
                    {
                        selectedIndex = tmpIndex;
                        gotoListItem();
                    }
                    else
                    {
                        selectedIndex = 0;
                    }
                }
                else
                {//The first number being pressed
                    tmpIndex = (int)(c - '0');
                    if (listViewSI.Items.Count > tmpIndex)
                    {
                        selectedIndex = tmpIndex;

						if (listViewSI.Items[0].SubItems[0].Text.Length < 2 )
                        {
                            //list view is one digit so process here
                            gotoListItem();
                            //reset selected index for the next cycle
                            selectedIndex = 0;

                        }
                        else
                        {
                            //list view has more than 10 items so wait for the next cycle or tick
                             //start timer
                             timer1.Enabled = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Stop the timer and reset index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //stop timer after one tic
            timer1.Enabled = false;
            //reset selected index for the next cycle
            selectedIndex = 0;
        }

        /// <summary>
        /// Go to the selected item and expand it if possible
        /// </summary>
        private void gotoListItem()
        {
            if (selectedIndex <= listViewSI.Items.Count)
            {
                //remove all previous selections
                for (int i = 0; i < listViewSI.Items.Count; i++)
                {
                    listViewSI.Items[i].Selected = false;
                }

				if (selectedIndex == 0)
				{
					this.Close();
					return;
				}

                //select the desired item
                listViewSI.Items[selectedIndex].Selected = true;
                listViewSI.Invoke(this.MyActivateHandler);       
            }
        }

		/// <summary>
		/// The handler called when resizing SIForm. The UI is re-calculated
		/// & adjusted based on the dimentions of the screen.
		/// </summary>
		private void SIForm_Resize(object sender, System.EventArgs e)
		{
            // If it is CE
            if (Symbol.Win32.PlatformType.IndexOf("PocketPC", 0) <= 0)
            {
                this.Width = (Screen.PrimaryScreen.WorkingArea.Width > 320 ? 320 : Screen.PrimaryScreen.WorkingArea.Width);
                this.Height = (Screen.PrimaryScreen.WorkingArea.Height > 320 ? 320 : Screen.PrimaryScreen.WorkingArea.Height);
            }

		}

        private void SetListViewColumnWidth()
        {
            listViewSI.Width = this.Width;

            this.NumberColumn.Width = (10 * listViewSI.Width) / 100;
            this.ItemColumn.Width = (40 * listViewSI.Width) / 100;
            this.ValueColumn.Width = (50 * listViewSI.Width) / 100;

        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            SetListViewColumnWidth();
        }


		// Platform Invoke function definitions.

        //----------------------------------------------------------------------------------
        //
        //  FUNCTION:  BOOL SystemParametersInfo(UINT uiAction, UINT uiParam, PVOID pvParam,UINT fWinIni);
        //
        //  PURPOSE:   Queries or sets the system-wide parameters. In this sample, we are intested 
        //              only in the OEM Version. 
        //              This is a Windows API call. Please refer to the MSDN documentation
        //              for additional information.
        //
        //  PARAMETERS:
        //		uiAction - Specifies the system-wide parameter.  
        //		uiParam - Depends on the system parameter specified.
        //      pvParam - Depends on the system parameter specified.
        //      fWinIni - Used if a system parameter is being set.
        //
        //  RETURN VALUE:
        //      If the function succeeds, the return value is TRUE.
        //		If the function fails, the return value is FALSE.
        //
        //----------------------------------------------------------------------------------
		
		[DllImport("coreDLL.dll")]
        public static extern int SystemParametersInfo(int uiAction, int uiParam, string pBuf, int fWinIni);

        //----------------------------------------------------------------------------------
        //
        //  FUNCTION:  BOOL CAD_GetOemVersionNumber(LPWORD lpwMajor, LPWORD lpwMinor) 
        //
        //  PURPOSE:   Queries major and minor OEM version numbers.  
        //
        //  PARAMETERS:
        //		lpwMajor - Pointer to returned major OEM version number. 
        //		lpwMinor - Pointer to returned minor OEM version number. 
        //
        //  RETURN VALUE:
        //      If the function succeeds, the return value is TRUE.
        //		If the function fails, the return value is FALSE.
        //
        //----------------------------------------------------------------------------------
		
        [DllImport("CAD.dll")]
		public static extern int CAD_GetOemVersionNumber (ref System.UInt16 lpwMajor, ref System.UInt16  lpwMinor);
        
        //----------------------------------------------------------------------------------
        //
        //  FUNCTION:  BOOL CAD_GetOemBuildNumber(LPDWORD lpdwBuild)
        //
        //  PURPOSE:   Queries the OEM build number.  
        //
        //  PARAMETERS:
        //		lpdwBuild - Pointer to returned major OEM build number. 
        //
        //  RETURN VALUE:
        //      If the function succeeds, the return value is TRUE.
        //		If the function fails, the return value is FALSE.
        //
        //----------------------------------------------------------------------------------
       
        [DllImport("CAD.dll")]
		public static extern int CAD_GetOemBuildNumber(ref uint lpdwBuild);
        
        //----------------------------------------------------------------------------------
        //
        //  FUNCTION:  BOOL CAD_GetLoaderVersionNumber(LPWORD lpwMajor, LPWORD lpwMinor) 
        //
        //  PURPOSE:   Queries the loader version number.  
        //
        //  PARAMETERS:
        //		lpwMajor - Pointer to returned major loader version number.  
        //		lpwMinor - Pointer to returned minor loader version number.  
        //
        //  RETURN VALUE:
        //      If the function succeeds, the return value is TRUE.
        //		If the function fails, the return value is FALSE.
        //
        //----------------------------------------------------------------------------------
		
		[DllImport("CAD.dll")]
		public static extern int CAD_GetLoaderVersionNumber(ref System.UInt16 lpwMajor, ref System.UInt16  lpwMinor);



        private const int LVM_GETITEMPOSITION = (0x1010);
        private const int LVM_GETEXTENDEDLISTVIEWSTYLE = 0x1037;
        private const int LVM_SETEXTENDEDLISTVIEWSTYLE = 0x1036;
        private const int LVS_EX_GRIDLINES = 0x1;

        public struct Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [DllImport("coredll.dll")]
        private static extern int SendMessageW(int hWnd, uint wMsg, int wParam, ref Point lParam);

        [DllImport("coredll.dll")]
        private static extern int SendMessageW(int hWnd, int wMsg, int wParam, int lParam);

        public void setRowHeight(System.Windows.Forms.ListView lvw)
        {
            Point positionItem1 = new Point(0, 0);
            Point positionItem2 = new Point(0, 0);

            SendMessageW((int)(lvw.Handle), LVM_GETITEMPOSITION, 0, ref positionItem1);

            SendMessageW((int)(lvw.Handle), LVM_GETITEMPOSITION, 1, ref positionItem2);

            int rowHeight = positionItem2.y - positionItem1.y;

            // Adjust the row height of listViewMain by multiplying the current factor by ROW_HEIGHT_FACTOR.
            //  The usage of this imageList is kind of a workaround in the absense of an exposed API to control the 
            //  row height of System.Windows.Forms.ListView.
            this.imageList.ImageSize = new Size(1, (int)(rowHeight * ROW_HEIGHT_FACTOR));
            lvw.SmallImageList = this.imageList;
        }

        public void setGridLines(System.Windows.Forms.ListView lvw)
        {
            lvw.Focus();
            int extendedStyle = SendMessageW((int)(lvw.Handle), LVM_GETEXTENDEDLISTVIEWSTYLE, 0, 0);
            extendedStyle |= LVS_EX_GRIDLINES;
            SendMessageW((int)(lvw.Handle), LVM_SETEXTENDEDLISTVIEWSTYLE, 0, extendedStyle);
        }
	}
}
