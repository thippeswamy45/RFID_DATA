using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Symbol.RFID2;

namespace CS_RFID2_Host_Sample
{
	/// <summary>
	/// Summary description for frmDelete.
	/// </summary>
	public class frmDelete : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button m_btnDelete;
		private System.Windows.Forms.Button m_btnCancel;
		private System.Windows.Forms.GroupBox m_gbxReaderList;
		internal KYListView m_lbxReaderList;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmDelete()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_gbxReaderList = new System.Windows.Forms.GroupBox();
            this.m_lbxReaderList = new CS_RFID2_Host_Sample.KYListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnDelete = new System.Windows.Forms.Button();
            this.m_gbxReaderList.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_gbxReaderList
            // 
            this.m_gbxReaderList.Controls.Add(this.m_lbxReaderList);
            this.m_gbxReaderList.Controls.Add(this.m_btnCancel);
            this.m_gbxReaderList.Controls.Add(this.m_btnDelete);
            this.m_gbxReaderList.Location = new System.Drawing.Point(8, 8);
            this.m_gbxReaderList.Name = "m_gbxReaderList";
            this.m_gbxReaderList.Size = new System.Drawing.Size(376, 288);
            this.m_gbxReaderList.TabIndex = 1;
            this.m_gbxReaderList.TabStop = false;
            // 
            // m_lbxReaderList
            // 
            this.m_lbxReaderList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lbxReaderList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lbxReaderList.Location = new System.Drawing.Point(16, 16);
            this.m_lbxReaderList.Name = "m_lbxReaderList";
            this.m_lbxReaderList.Size = new System.Drawing.Size(344, 232);
            this.m_lbxReaderList.TabIndex = 3;
            this.m_lbxReaderList.UseCompatibleStateImageBehavior = false;
            this.m_lbxReaderList.View = System.Windows.Forms.View.List;
            // 
            // columnHeader1
            // 
            this.columnHeader1.DisplayIndex = 0;
            this.columnHeader1.Width = 300;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCancel.Location = new System.Drawing.Point(220, 256);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(80, 23);
            this.m_btnCancel.TabIndex = 2;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_btnDelete
            // 
            this.m_btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnDelete.Location = new System.Drawing.Point(76, 256);
            this.m_btnDelete.Name = "m_btnDelete";
            this.m_btnDelete.Size = new System.Drawing.Size(88, 24);
            this.m_btnDelete.TabIndex = 1;
            this.m_btnDelete.Text = "Delete";
            this.m_btnDelete.Click += new System.EventHandler(this.m_btnDelete_Click);
            // 
            // frmDelete
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(392, 309);
            this.Controls.Add(this.m_gbxReaderList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDelete";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Delete Reader";
            this.Load += new System.EventHandler(this.frmDelete_Load);
            this.m_gbxReaderList.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void frmDelete_Load(object sender, System.EventArgs e)
		{
			try
			{
				frmMain objfrmMain = (frmMain)this.Owner;
				foreach(frmTest objTest in objfrmMain.m_hashTestForm.Values)
				{
					m_lbxReaderList.Items.Add(objTest.ReaderInfo);
				}
			}
			catch
			{
			}
			finally
			{
			}
			
		}

		private void m_btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void m_btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{	ArrayList arrDeletedKeys= new ArrayList();
				frmMain objfrmMain = (frmMain)this.Owner;
				foreach(string key in objfrmMain.m_hashTestForm.Keys)
				{
					frmTest objTest = (frmTest)objfrmMain.m_hashTestForm[key];
					//if(m_lbxReaderList.SelectedItems.Contains(objTest.ReaderInfo))
					foreach(ListViewItem item in m_lbxReaderList.SelectedItems)
					{
						if(item.Text.Equals(objTest.ReaderInfo ))
						{
							arrDeletedKeys.Add(key);
							objTest.Dispose();
						}
					}
				}
				foreach(string key in arrDeletedKeys)
				{
					objfrmMain.m_hashTestForm.Remove(key);
                    ReaderFactory.DeleteReader(key);
                    
				}

				this.Close();

			}
			catch(Exception ex)
			{
				string str=ex.Message;
			}
			finally
			{
			}
		}
	}
}
