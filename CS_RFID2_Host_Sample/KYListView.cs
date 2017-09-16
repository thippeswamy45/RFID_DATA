using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;




namespace CS_RFID2_Host_Sample
{
	/// <summary>
	/// Summary description for KYListView.
	/// </summary>
	public class KYListView : System.Windows.Forms.ListView
	{
		

		public KYListView()
		{
			
		}

		/// <summary>
		///  Make sure those Unhandled Exceptions are   ignored 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnHandleDestroyed(EventArgs e)
		{
			try 
			{
				this.Clear() ; 
			}
			catch ( Exception ) {} 


			try {
				base.OnHandleDestroyed (e);
			}
			catch ( Exception ) {} 


		}

	}
}
