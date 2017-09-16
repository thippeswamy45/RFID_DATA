using System;
using System.Windows.Forms;

using Symbol.MT2000.UserInterface;
using Symbol.MT2000.ScannerServices; 

namespace CS_MT2000_ScanInventory
{
	public partial class MainForm : Symbol.MT2000.UserInterface.ListForm
	{
		/// <summary>
		/// initializes the form
		/// </summary>
		public MainForm()
		{
			// initialize the controls
			InitializeComponent();
			Application.DoEvents();

			// create the screens
			Screens.Add("main", new MainScreen(this));
			Screens.Add("about", new AboutScreen(this));
			Screens.Add("options", new OptionsScreen(this));
			Screens.Add("view", new InventoryScreen(this));

            // Add event handlers. 
            this.Activated += new EventHandler(FormActivated);
            this.Deactivate += new EventHandler(FormDeactivated); 


			// display the main screen
			PushScreen(Screens["main"]);
		}
        private void FormActivated(object sender, EventArgs e)
        {
            if (!Program.ScannerServicesClient.Connect(true))
            {
                MsgBox.Show(null, Properties.Resources.StrScanInventory, Properties.Resources.StrErrorCantStartScannerServices);
                this.Close();
            }
            if (RESULTCODE.E_OK != Program.ScannerServicesClient.SetMode(SCANNERSVC_MODE.SVC_MODE_DECODE))
            {
                MsgBox.Show(null, Properties.Resources.StrScanInventory, Properties.Resources.StrErrorCantSetScannerServicesMode);
                Program.ScannerServicesClient.Disconnect();
                Program.ScannerServicesClient.Dispose();
                this.Close();
            }
            // start an asynchronous scanner read
            Program.ScannerServicesClient.BeginReadLabel();
        }
        private void FormDeactivated(object sender, EventArgs e)
        {
            Program.ScannerServicesClient.Disconnect();
        }

	}
}
