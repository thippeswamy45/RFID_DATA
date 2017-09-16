//--------------------------------------------------------------------
// FILENAME: AboutForm.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION: The source file for the AboutForm dialog.
//
//--------------------------------------------------------------------

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CS_SysInfoSample1
{
    public class AboutForm : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel panel1;

        private TextBox textBox1;

		/// <summary>
		/// AboutForm constructor.
		/// </summary>
		public AboutForm()
        {
            InitializeComponent();
            // Add MainMenu if Pocket PC
            if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
            {
                this.Menu = new MainMenu();
            }

            textBox1.Text = "CS_SysInfoSample1\r\nv1.00.01\r\nCopyright © 2011 Motorola Solutions, Inc. All rights reserved.\r\n\r\n"
                + "This sample application is provided to demonstrate various programming practices and is not intended for production use.\r\n\r\nPress any key to exit.";
            
           }

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 292);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(318, 292);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Sample text 1";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AboutForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(318, 292);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Text = "CS_SysInfoSample1";
            this.Resize += new System.EventHandler(this.AboutForm_Resize);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AboutForm_KeyPress);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

        #endregion

        private void AboutForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.Close();
		}

		private void AboutForm_Resize(object sender, System.EventArgs e)
		{
            // If it is CE
            if (Symbol.Win32.PlatformType.IndexOf("PocketPC", 0) <= 0)
            {
                this.Width = (Screen.PrimaryScreen.WorkingArea.Width > 320 ? 320 : Screen.PrimaryScreen.WorkingArea.Width);
                this.Height = (Screen.PrimaryScreen.WorkingArea.Height > 320 ? 320 : Screen.PrimaryScreen.WorkingArea.Height);
            }
		}

    }
}
