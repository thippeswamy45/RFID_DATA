using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Symbol.IO;

namespace CS_IOSample1
{
    public partial class IOForm : Form
    {

        private System.Windows.Forms.ColumnHeader NameColumn = null;
        private System.Windows.Forms.ColumnHeader NumberColumn = null;
        private System.Windows.Forms.ColumnHeader ItemColumn = null;
        private System.Windows.Forms.ColumnHeader ValueColumn = null;

        private System.Windows.Forms.ListView listViewIO = null;
		private Resources MyResources = null;

        private Timer timer1;
        private int selectedIndex = 0;
        System.EventHandler MyActivateHandler = null;
        //A formatting string used with listview items
        string itemNumberFormat = "";
        string currentListView = "";

        //the IO reference
        IO m_IO = null;

        private delegate void UpdateUIDelegate(PORT_TYPE portType);
        private UpdateUIDelegate updateUIHandler = null;

		const string ROOT = "Main";
		const string ABOUT = "About";
		const string BACK = "Back";
        private Panel panel1;
		const string EXITAPP = "ExitApp";

        // Used to adjust the row height of listViewIO.
        // (This is a workaround in the absense of an exposed API to control the 
        // row height of ListView control)
        private System.Windows.Forms.ImageList imageList1;

        // The factor(n) which defines the row height of the ListView. 
        // The default row height would be multiplied by this factor (ROW_HEIGHT_FACTOR = n).
        // Currently set to 2. So the row height would be doubled in this sample.
        // The user can set it to any value (i.e. 1,2,3,..., etc.) based on the requirements.
        private const int ROW_HEIGHT_FACTOR = 2;

        public IOForm()
		{
            
            Cursor savedCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            MyResources = new Resources();
            //
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this.ItemColumn.Text = Resources.GetString("Item");
            this.ValueColumn.Text = Resources.GetString("Value");
            //set time interval for number input as 1 second
            this.timer1.Interval = 1000;

            // Used to adjust the row height of listViewIO.
            // (This is a workaround in the absense of an exposed API to control the 
            // row height of ListView control)
            imageList1 = new ImageList();

            //create IO object
            m_IO = new IO();

            Cursor.Current = savedCursor;

            // Add MainMenu if Pocket PC
            if (Symbol.Win32.PlatformType == "PocketPC")
            {
                this.Menu = new MainMenu();
            }
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
            this.listViewIO = new System.Windows.Forms.ListView();
            this.NameColumn = new System.Windows.Forms.ColumnHeader();
            this.NumberColumn = new System.Windows.Forms.ColumnHeader();
            this.ItemColumn = new System.Windows.Forms.ColumnHeader();
            this.ValueColumn = new System.Windows.Forms.ColumnHeader();
            this.timer1 = new System.Windows.Forms.Timer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewIO
            // 
            this.listViewIO.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewIO.Columns.Add(this.NameColumn);
            this.listViewIO.Columns.Add(this.NumberColumn);
            this.listViewIO.Columns.Add(this.ItemColumn);
            this.listViewIO.Columns.Add(this.ValueColumn);
            this.listViewIO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewIO.FullRowSelect = true;
            this.listViewIO.Location = new System.Drawing.Point(0, 0);
            this.listViewIO.Name = "listViewIO";
            this.listViewIO.Size = new System.Drawing.Size(240, 272);
            this.listViewIO.TabIndex = 0;
            this.listViewIO.View = System.Windows.Forms.View.Details;
            this.listViewIO.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListViewIO_KeyUp);
            this.listViewIO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewIO_KeyDown);
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "";
            this.NameColumn.Width = 0;
            // 
            // NumberColumn
            // 
            this.NumberColumn.Text = "#";
            this.NumberColumn.Width = 32;
            // 
            // ItemColumn
            // 
            this.ItemColumn.Text = "ColumnHeader";
            this.ItemColumn.Width = 250;
            // 
            // ValueColumn
            // 
            this.ValueColumn.Text = "ColumnHeader";
            this.ValueColumn.Width = 300;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listViewIO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 272);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // IOForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(240, 272);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "IOForm";
            this.Text = "IO Sample";
            this.Load += new System.EventHandler(this.IOForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.IOForm_Closing);
            this.Resize += new System.EventHandler(this.IOForm_Resize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
            IOForm ioForm = new IOForm();
            Application.Run(ioForm);

		}

		private void IOForm_Load(object sender, System.EventArgs e)
		{

            MyActivateHandler = new System.EventHandler(this.listViewIO_ItemActivate);
            this.listViewIO.ItemActivate += MyActivateHandler;

            currentListView = ROOT;
            loadMainListViewItems();

            setGridLines(this.listViewIO);
            setRowHeight(this.listViewIO);

            // Ensure that the keyboard focus is set on a control.
            this.listViewIO.Focus();

            m_IO.PortEventNotify += new IO.PortEventNotifyHandler(m_IO_PortEventNotify);
            m_IO.DI.DIPorts[DI.DI_PORT_NUM.DI_PORT_0].NotifyReceipt = true;
            m_IO.AN.ANPorts[AN.AN_PORT_NUM.IO_AMB_TEMP].NotifyReceipt = true;
            updateUIHandler = new UpdateUIDelegate(UpdateUI);
        }

        private void UpdateUI(PORT_TYPE portType)
        {
            if (ROOT == currentListView)
            {
                switch (portType)
                {
                    case PORT_TYPE.IO_DIGITAL_INPUT:
                        listViewIO.Items[1].SubItems[3].Text = m_IO.DI.DIPorts[DI.DI_PORT_NUM.DI_PORT_0].PortState.ToString();
                        break;
                    case PORT_TYPE.IO_ANALOG_INPUT:
                        listViewIO.Items[7].SubItems[3].Text = m_IO.AN.ANPorts[AN.AN_PORT_NUM.IO_AMB_TEMP].PortValue.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private void m_IO_PortEventNotify(object sender, IO.PortEventNotifyEventArgs ePortEventArg)
        {
            try
            {
                if (ROOT == currentListView)
                {
                    listViewIO.Invoke(this.updateUIHandler, new object[] { ePortEventArg.PortInfo.PortType });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ex : " + ex.Message);
            }

        }

		#region Main listView
		/// <summary>
		/// Add an item to listViewIO
		/// </summary>
		/// <param name="number">the number to display in Number column</param>
		/// <param name="itemName">The string to displsy in Item column</param>
        private void addListViewItem(int number, string itemName)
		{
			string[] item;
            item = new string[] { itemName, number.ToString(itemNumberFormat), Resources.GetString(itemName) };
			ListViewItem li = new ListViewItem(item);
			listViewIO.Items.Add(li);
		}
        /// <summary>
        /// Add an item to listViewIO
        /// </summary>
        /// <param name="number">The number to display in Number column</param>
        /// <param name="itemName">The string to display in Item column</param>
        /// <param name="itemValue">The string to display in Value column</param>
        private void addListViewItem(int number, string itemName, string itemValue)
        {
            string[] item;
            item = new string[] { itemName, number.ToString(itemNumberFormat), Resources.GetString(itemName), itemValue };
            ListViewItem li = new ListViewItem(item);
            listViewIO.Items.Add(li);
        }

        /// <summary>
        /// Add item to listViewIO with extentions to item name
        /// </summary>
        /// <param name="number">The number to display in Number column</param>
        /// <param name="itemName">The string to display in Item column</param>
        /// <param name="itemNameExtentions">The string to display as Item name extension</param>
        /// <param name="itemValue">The string to display in Value column</param>
        private void addListViewItem(int number, string itemName, string itemNameExtentions, string itemValue)
        {
            string[] item;
            item = new string[] { itemName, number.ToString(itemNumberFormat), Resources.GetString(itemName) + itemNameExtentions, itemValue };
            ListViewItem li = new ListViewItem(item);
            listViewIO.Items.Add(li);
        }

		/// <summary>
		/// Add items to the Start page of the Form
		/// </summary>
		private void loadMainListViewItems()
		{
            //Get the first adapter
            itemNumberFormat = "";

            int i = 0;
            addListViewItem(i++, EXITAPP, "");

            addListViewItem(i++, "DI0State", m_IO.DI.DIPorts[DI.DI_PORT_NUM.DI_PORT_0].PortState.ToString());
            addListViewItem(i++, "DI0SwitchConfig", m_IO.DI.DIPorts[DI.DI_PORT_NUM.DI_PORT_0].GetPortConfig().SwitchBatteryGround.ToString());
            addListViewItem(i++, "DI0WakeupConfig", m_IO.DI.DIPorts[DI.DI_PORT_NUM.DI_PORT_0].GetPortConfig().WakeupState.ToString());
            addListViewItem(i++, "DO0State", m_IO.DO.DOPorts[DO.DO_PORT_NUM.DO_PORT_0].PortState.ToString());
            addListViewItem(i++, "DO0ColdBootConfig", m_IO.DO.DOPorts[DO.DO_PORT_NUM.DO_PORT_0].GetPortConfig().AfterColdBootState.ToString());
            addListViewItem(i++, "DO0WarmBootConfig", m_IO.DO.DOPorts[DO.DO_PORT_NUM.DO_PORT_0].GetPortConfig().AfterWarmBootState.ToString());
            addListViewItem(i++, "AmbTemp", m_IO.AN.ANPorts[AN.AN_PORT_NUM.IO_AMB_TEMP].PortValue.ToString());
            addListViewItem(i++, "PowerSrc", m_IO.SI.PowerSourceType.ToString());

			addListViewItem(i++, ABOUT, "");


		}

		/// <summary>
		/// Refresh the main list view after unloading it
		/// </summary>
		private void loadMainListView()
		{
            currentListView = ROOT;
			//Add column headers
			this.listViewIO.Columns.Add(this.NameColumn);
			this.listViewIO.Columns.Add(this.NumberColumn);
            this.listViewIO.Columns.Add(this.ItemColumn);
            this.listViewIO.Columns.Add(this.ValueColumn);
			this.loadMainListViewItems();
		}
		/// <summary>
		/// Unload the start window
		/// </summary>
		private void unloadMainListView()
		{
			this.listViewIO.Clear();
			//Remove column headers
			this.listViewIO.Columns.Remove(this.NameColumn);
			this.listViewIO.Columns.Remove(this.NumberColumn);
			this.listViewIO.Columns.Remove(this.ItemColumn);
            this.listViewIO.Columns.Remove(this.ValueColumn);
        }

 

		#endregion 

        /// <summary>
		/// This deligate is called when a listview item is double clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewIO_ItemActivate(object sender, System.EventArgs e)
		{
            Cursor savedCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            try
			{
				//Compare the zeroth element in the selected row in the listwiew
				switch (listViewIO.Items[listViewIO.SelectedIndices[0]].SubItems[0].Text)
				{
					case EXITAPP :
						this.Close();
						break;
					case ABOUT :  
						// About is only in main List View
                        AboutForm myAbout = new AboutForm();
                        Cursor.Current = savedCursor;
                        myAbout.ShowDialog();
                        Cursor.Current = Cursors.WaitCursor;
                        //Ensure that the keyboard focus is set on a control.
                        this.listViewIO.Focus();
                        break;
					case BACK :  
						reloadLastListView();
						break;
					default: 
						break;
				}
   			}
			catch(ArgumentOutOfRangeException)
			{
				System.Windows.Forms.MessageBox.Show("Item not selected", "CS_IOSample1");
                this.listViewIO.Focus();
			}
            Cursor.Current = savedCursor;
		}


		/// <summary>
		/// Backtrace to the previous listView
		/// </summary>
		private void reloadLastListView()
		{
			loadMainListView();
		}

		private void IOForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            if (currentListView != ROOT)
            {
                //Cancell the closing event and reload the previous screen
                e.Cancel = true;
                reloadLastListView();
                return;
            }

            m_IO.Dispose();
		}

        private void ListViewIO_KeyUp(object sender, KeyEventArgs e)
        {
            this.listViewIO.Focus();
        }

        /// <summary>
        /// Handle keyboard navigation of the listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewIO_KeyDown(object sender, KeyEventArgs e)
        {
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
                    if (listViewIO.Items.Count > tmpIndex)
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
                    if (listViewIO.Items.Count > tmpIndex)
                    {
                        selectedIndex = tmpIndex;
                        if (listViewIO.Items.Count <= 10)
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
            if (selectedIndex <= listViewIO.Items.Count)
            {
                //remove all previous selections
                for (int i = 0; i < listViewIO.Items.Count; i++)
                {
                    listViewIO.Items[i].Selected = false;
                }

                //process keyboard input for exit first of all
                if (currentListView == ROOT)
                {
                    if (selectedIndex == 0)
                    {
                        this.Close();
                        return;
                    }
                }

                //select the desired item
                listViewIO.Items[selectedIndex].Selected = true;
                listViewIO.Invoke(this.MyActivateHandler);       
            }
        }


        private void IOForm_Resize(object sender, EventArgs e)
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
            listViewIO.Width = this.Width;

            // Main list
            this.NumberColumn.Width = (13 * listViewIO.Width) / 100;
            this.ItemColumn.Width = (36 * listViewIO.Width) / 100; ;
            this.ValueColumn.Width = (50 * listViewIO.Width) / 100;


        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            SetListViewColumnWidth();
        }

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
            this.imageList1.ImageSize = new Size(1, (int)(rowHeight * ROW_HEIGHT_FACTOR));
            lvw.SmallImageList = this.imageList1;
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