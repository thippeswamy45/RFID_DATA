using Symbol.Telemetry;

namespace CS_VBusSample1
{
    partial class VBusForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        // The ImageList reference which is used to control the row height of listViewMain.
        //  This is kind of a workaround in the absense of an exposed API to control the 
        //  row height of System.Windows.Forms.ListView.
        private System.Windows.Forms.ImageList imageList = null;
        private System.Windows.Forms.ListView listViewVBus;
        private System.Windows.Forms.ColumnHeader NameColumn;
        private System.Windows.Forms.ColumnHeader ItemColumn;
        private System.Windows.Forms.ColumnHeader NumberColumn;
        private System.Windows.Forms.ColumnHeader ValueColumn;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewVBus = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList();
            this.NameColumn = new System.Windows.Forms.ColumnHeader();
            this.NumberColumn = new System.Windows.Forms.ColumnHeader();
            this.ItemColumn = new System.Windows.Forms.ColumnHeader();
            this.ValueColumn = new System.Windows.Forms.ColumnHeader();
            this.timer1 = new System.Windows.Forms.Timer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewVBus
            // 
            this.listViewVBus.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewVBus.Columns.Add(this.NameColumn);
            this.listViewVBus.Columns.Add(this.NumberColumn);
            this.listViewVBus.Columns.Add(this.ItemColumn);
            this.listViewVBus.Columns.Add(this.ValueColumn);
            this.listViewVBus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewVBus.FullRowSelect = true;
            this.listViewVBus.Location = new System.Drawing.Point(0, 0);
            this.listViewVBus.Name = "listViewVBus";
            this.listViewVBus.Size = new System.Drawing.Size(323, 300);
            this.listViewVBus.TabIndex = 1;
            this.listViewVBus.View = System.Windows.Forms.View.Details;
            this.listViewVBus.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listViewVBus_KeyUp);
            this.listViewVBus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewVBus_KeyDown);
            // 
            // imageList
            // 
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "";
            this.NameColumn.Width = 0;
            // 
            // NumberColumn
            // 
            this.NumberColumn.Text = "#";
            this.NumberColumn.Width = 30;
            // 
            // ItemColumn
            // 
            this.ItemColumn.Text = "Item";
            this.ItemColumn.Width = 208;
            // 
            // ValueColumn
            // 
            this.ValueColumn.Text = "Value";
            this.ValueColumn.Width = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listViewVBus);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(323, 300);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // VBusForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(323, 300);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "VBusForm";
            this.Text = "CS_VBusSample1";
            this.Load += new System.EventHandler(this.VBusForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.VBusForm_Closing);
            this.Resize += new System.EventHandler(this.VBusForm_Resize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;


    }
}

