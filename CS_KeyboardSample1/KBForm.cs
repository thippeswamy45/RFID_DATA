//--------------------------------------------------------------------
// FILENAME: KBForm.cs
//
// Copyright © 2011 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION:
//
// NOTES:
//
// 
//--------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;

using Symbol.Keyboard;

namespace CS_KeyboardSample1
{
	/// <summary>
	/// CS_KeyboardSample1 KBForm class.
	/// </summary>
	public class KBForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageState;
        private System.Windows.Forms.Button buttonAbout;
		private System.Windows.Forms.CheckBox checkBoxUnShift;
		private System.Windows.Forms.CheckBox checkBoxShift;
		private System.Windows.Forms.TabPage tabPageAlpha;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxAlpha;
		private System.Windows.Forms.TabPage tabPageKeyLite;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxTimeout;
		private System.Windows.Forms.ComboBox comboBoxKeyLite;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox checkBoxNum;
        private System.Windows.Forms.CheckBox checkBoxCaps;
		private System.Windows.Forms.Button buttonAlpha;
		private System.Windows.Forms.Button buttonKeyLite;
		private System.Windows.Forms.CheckBox checkBoxFunc;
		private System.Windows.Forms.CheckBox checkBoxAlt;
        private System.Windows.Forms.CheckBox checkBoxCtrl;
        private CheckBox checkBoxOrangeTemp;
        private CheckBox checkBoxShiftLock;
        private CheckBox checkBoxFuncLock;
        private CheckBox checkBoxNumLock;
        private CheckBox checkBoxOrangeLock;
        private CheckBox checkBoxOrangeShiftLock;
        private Label label5;
        private Label label4;
        private Label label1;
		private System.Windows.Forms.Button buttonKeyState;
		private System.Windows.Forms.CheckBox checkBoxReg1;
		private System.Windows.Forms.Button buttonExit;

		private Symbol.Keyboard.KeyPad keypad;

        private static bool bPortrait = true;   // The default dispaly orientation 
        // has been set to Portrait.

        private bool bSkipMaxLen = false;    // The restriction on the maximum 
        // physical length is considered by default.

        private bool bInitialScale = true;   // The flag to track whether the 
        // scaling logic is applied for
        // the first time (from scatch) or not.
        // Based on that, the (outer) width/height values
        // of the form will be set or not.
        // Initially set to true.

        private int resWidthReference = 242;   // The (cached) width of the form. 
        // INITIALLY HAS TO BE SET TO THE WIDTH OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private int resHeightReference = 295;  // The (cached) height of the form.
        // INITIALLY HAS TO BE SET TO THE HEIGHT OF THE FORM AT DESIGN TIME (IN PIXELS).
        // This setting is also obtained from the platform only on
        // Windows CE devices before running the application on the device, as a verification.
        // For PocketPC (& Windows Mobile) devices, the failure to set this properly may result in the distortion of GUI/viewability.

        private const double maxLength = 5.5;  // The maximum physical width/height of the sample (in inches).
        // The actual value on the device may slightly deviate from this
        // since the calculations based on the (received) DPI & resolution values 
        // would provide only an approximation, so not 100% accurate.

		/// <summary>
		/// KBForm constructor.
		/// </summary>
		public KBForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            buttonAbout.Focus();
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageState = new System.Windows.Forms.TabPage();
            this.checkBoxShift = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxOrangeShiftLock = new System.Windows.Forms.CheckBox();
            this.checkBoxNumLock = new System.Windows.Forms.CheckBox();
            this.checkBoxOrangeLock = new System.Windows.Forms.CheckBox();
            this.checkBoxShiftLock = new System.Windows.Forms.CheckBox();
            this.checkBoxFuncLock = new System.Windows.Forms.CheckBox();
            this.checkBoxOrangeTemp = new System.Windows.Forms.CheckBox();
            this.checkBoxCtrl = new System.Windows.Forms.CheckBox();
            this.checkBoxAlt = new System.Windows.Forms.CheckBox();
            this.checkBoxFunc = new System.Windows.Forms.CheckBox();
            this.checkBoxUnShift = new System.Windows.Forms.CheckBox();
            this.checkBoxNum = new System.Windows.Forms.CheckBox();
            this.checkBoxCaps = new System.Windows.Forms.CheckBox();
            this.checkBoxReg1 = new System.Windows.Forms.CheckBox();
            this.buttonKeyState = new System.Windows.Forms.Button();
            this.tabPageAlpha = new System.Windows.Forms.TabPage();
            this.comboBoxAlpha = new System.Windows.Forms.ComboBox();
            this.buttonAlpha = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageKeyLite = new System.Windows.Forms.TabPage();
            this.comboBoxKeyLite = new System.Windows.Forms.ComboBox();
            this.textBoxTimeout = new System.Windows.Forms.TextBox();
            this.buttonKeyLite = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageState.SuspendLayout();
            this.tabPageAlpha.SuspendLayout();
            this.tabPageKeyLite.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageState);
            this.tabControl1.Controls.Add(this.tabPageAlpha);
            this.tabControl1.Controls.Add(this.tabPageKeyLite);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 233);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageState
            // 
            this.tabPageState.Controls.Add(this.checkBoxShift);
            this.tabPageState.Controls.Add(this.label5);
            this.tabPageState.Controls.Add(this.label4);
            this.tabPageState.Controls.Add(this.label1);
            this.tabPageState.Controls.Add(this.checkBoxOrangeShiftLock);
            this.tabPageState.Controls.Add(this.checkBoxNumLock);
            this.tabPageState.Controls.Add(this.checkBoxOrangeLock);
            this.tabPageState.Controls.Add(this.checkBoxShiftLock);
            this.tabPageState.Controls.Add(this.checkBoxFuncLock);
            this.tabPageState.Controls.Add(this.checkBoxOrangeTemp);
            this.tabPageState.Controls.Add(this.checkBoxCtrl);
            this.tabPageState.Controls.Add(this.checkBoxAlt);
            this.tabPageState.Controls.Add(this.checkBoxFunc);
            this.tabPageState.Controls.Add(this.checkBoxUnShift);
            this.tabPageState.Controls.Add(this.checkBoxNum);
            this.tabPageState.Controls.Add(this.checkBoxCaps);
            this.tabPageState.Controls.Add(this.checkBoxReg1);
            this.tabPageState.Controls.Add(this.buttonKeyState);
            this.tabPageState.Location = new System.Drawing.Point(4, 30);
            this.tabPageState.Name = "tabPageState";
            this.tabPageState.Size = new System.Drawing.Size(232, 199);
            this.tabPageState.Text = "Key State";
            // 
            // checkBoxShift
            // 
            this.checkBoxShift.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxShift.Location = new System.Drawing.Point(75, 3);
            this.checkBoxShift.Name = "checkBoxShift";
            this.checkBoxShift.Size = new System.Drawing.Size(65, 24);
            this.checkBoxShift.TabIndex = 2;
            this.checkBoxShift.Text = "SHIFT";
            this.checkBoxShift.CheckStateChanged += new System.EventHandler(this.checkBoxShift_CheckStateChanged);
            this.checkBoxShift.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allCheckBoxes_KeyDown);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(3, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 20);
            this.label5.Text = "FUNC KEY:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(3, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.Text = "ORANGE KEY:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.Text = "NUM KEY:";
            // 
            // checkBoxOrangeShiftLock
            // 
            this.checkBoxOrangeShiftLock.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxOrangeShiftLock.Location = new System.Drawing.Point(75, 61);
            this.checkBoxOrangeShiftLock.Name = "checkBoxOrangeShiftLock";
            this.checkBoxOrangeShiftLock.Size = new System.Drawing.Size(157, 24);
            this.checkBoxOrangeShiftLock.TabIndex = 7;
            this.checkBoxOrangeShiftLock.Text = "ORANGE SHIFT LOCK";
            this.checkBoxOrangeShiftLock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allCheckBoxes_KeyDown);
            // 
            // checkBoxNumLock
            // 
            this.checkBoxNumLock.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxNumLock.Location = new System.Drawing.Point(164, 91);
            this.checkBoxNumLock.Name = "checkBoxNumLock";
            this.checkBoxNumLock.Size = new System.Drawing.Size(68, 24);
            this.checkBoxNumLock.TabIndex = 9;
            this.checkBoxNumLock.Text = "LOCK";
            this.checkBoxNumLock.CheckStateChanged += new System.EventHandler(this.checkBoxNumLock_CheckStateChanged);
            this.checkBoxNumLock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allCheckBoxes_KeyDown);
            // 
            // checkBoxOrangeLock
            // 
            this.checkBoxOrangeLock.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxOrangeLock.Location = new System.Drawing.Point(164, 116);
            this.checkBoxOrangeLock.Name = "checkBoxOrangeLock";
            this.checkBoxOrangeLock.Size = new System.Drawing.Size(68, 24);
            this.checkBoxOrangeLock.TabIndex = 11;
            this.checkBoxOrangeLock.Text = "LOCK";
            this.checkBoxOrangeLock.CheckStateChanged += new System.EventHandler(this.checkBoxOrangeLock_CheckStateChanged);
            this.checkBoxOrangeLock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allCheckBoxes_KeyDown);
            // 
            // checkBoxShiftLock
            // 
            this.checkBoxShiftLock.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxShiftLock.Location = new System.Drawing.Point(141, 3);
            this.checkBoxShiftLock.Name = "checkBoxShiftLock";
            this.checkBoxShiftLock.Size = new System.Drawing.Size(91, 24);
            this.checkBoxShiftLock.TabIndex = 3;
            this.checkBoxShiftLock.Text = "SHIFTLOCK";
            this.checkBoxShiftLock.CheckStateChanged += new System.EventHandler(this.checkBoxShiftLock_CheckStateChanged);
            this.checkBoxShiftLock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allCheckBoxes_KeyDown);
            // 
            // checkBoxFuncLock
            // 
            this.checkBoxFuncLock.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxFuncLock.Location = new System.Drawing.Point(164, 141);
            this.checkBoxFuncLock.Name = "checkBoxFuncLock";
            this.checkBoxFuncLock.Size = new System.Drawing.Size(68, 24);
            this.checkBoxFuncLock.TabIndex = 13;
            this.checkBoxFuncLock.Text = "LOCK";
            this.checkBoxFuncLock.CheckStateChanged += new System.EventHandler(this.checkBoxFuncLock_CheckStateChanged);
            this.checkBoxFuncLock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allCheckBoxes_KeyDown);
            // 
            // checkBoxOrangeTemp
            // 
            this.checkBoxOrangeTemp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxOrangeTemp.Location = new System.Drawing.Point(98, 116);
            this.checkBoxOrangeTemp.Name = "checkBoxOrangeTemp";
            this.checkBoxOrangeTemp.Size = new System.Drawing.Size(68, 24);
            this.checkBoxOrangeTemp.TabIndex = 10;
            this.checkBoxOrangeTemp.Text = "TEMP";
            this.checkBoxOrangeTemp.CheckStateChanged += new System.EventHandler(this.checkBoxOrangeTemp_CheckStateChanged);
            this.checkBoxOrangeTemp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allCheckBoxes_KeyDown);
            // 
            // checkBoxCtrl
            // 
            this.checkBoxCtrl.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxCtrl.Location = new System.Drawing.Point(3, 61);
            this.checkBoxCtrl.Name = "checkBoxCtrl";
            this.checkBoxCtrl.Size = new System.Drawing.Size(66, 24);
            this.checkBoxCtrl.TabIndex = 6;
            this.checkBoxCtrl.Text = "CTRL";
            this.checkBoxCtrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allCheckBoxes_KeyDown);
            // 
            // checkBoxAlt
            // 
            this.checkBoxAlt.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxAlt.Location = new System.Drawing.Point(3, 32);
            this.checkBoxAlt.Name = "checkBoxAlt";
            this.checkBoxAlt.Size = new System.Drawing.Size(53, 24);
            this.checkBoxAlt.TabIndex = 4;
            this.checkBoxAlt.Text = "ALT";
            this.checkBoxAlt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allCheckBoxes_KeyDown);
            // 
            // checkBoxFunc
            // 
            this.checkBoxFunc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxFunc.Location = new System.Drawing.Point(98, 141);
            this.checkBoxFunc.Name = "checkBoxFunc";
            this.checkBoxFunc.Size = new System.Drawing.Size(68, 24);
            this.checkBoxFunc.TabIndex = 12;
            this.checkBoxFunc.Text = "TEMP";
            this.checkBoxFunc.CheckStateChanged += new System.EventHandler(this.checkBoxFunc_CheckStateChanged);
            this.checkBoxFunc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allCheckBoxes_KeyDown);
            // 
            // checkBoxUnShift
            // 
            this.checkBoxUnShift.Checked = true;
            this.checkBoxUnShift.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUnShift.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxUnShift.Location = new System.Drawing.Point(3, 3);
            this.checkBoxUnShift.Name = "checkBoxUnShift";
            this.checkBoxUnShift.Size = new System.Drawing.Size(84, 24);
            this.checkBoxUnShift.TabIndex = 1;
            this.checkBoxUnShift.Text = "UNSHIFT";
            this.checkBoxUnShift.CheckStateChanged += new System.EventHandler(this.checkBoxUnShift_CheckStateChanged);
            this.checkBoxUnShift.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allCheckBoxes_KeyDown);
            // 
            // checkBoxNum
            // 
            this.checkBoxNum.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxNum.Location = new System.Drawing.Point(98, 91);
            this.checkBoxNum.Name = "checkBoxNum";
            this.checkBoxNum.Size = new System.Drawing.Size(68, 24);
            this.checkBoxNum.TabIndex = 8;
            this.checkBoxNum.Text = "TEMP";
            this.checkBoxNum.CheckStateChanged += new System.EventHandler(this.checkBoxNum_CheckStateChanged);
            this.checkBoxNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allCheckBoxes_KeyDown);
            // 
            // checkBoxCaps
            // 
            this.checkBoxCaps.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxCaps.Location = new System.Drawing.Point(75, 32);
            this.checkBoxCaps.Name = "checkBoxCaps";
            this.checkBoxCaps.Size = new System.Drawing.Size(96, 24);
            this.checkBoxCaps.TabIndex = 5;
            this.checkBoxCaps.Text = "CAPSLOCK";
            this.checkBoxCaps.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allCheckBoxes_KeyDown);
            // 
            // checkBoxReg1
            // 
            this.checkBoxReg1.Location = new System.Drawing.Point(3, 168);
            this.checkBoxReg1.Name = "checkBoxReg1";
            this.checkBoxReg1.Size = new System.Drawing.Size(112, 24);
            this.checkBoxReg1.TabIndex = 14;
            this.checkBoxReg1.Text = "Update Registry";
            this.checkBoxReg1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allCheckBoxes_KeyDown);
            // 
            // buttonKeyState
            // 
            this.buttonKeyState.Location = new System.Drawing.Point(148, 168);
            this.buttonKeyState.Name = "buttonKeyState";
            this.buttonKeyState.Size = new System.Drawing.Size(72, 24);
            this.buttonKeyState.TabIndex = 15;
            this.buttonKeyState.Text = "Update";
            this.buttonKeyState.Click += new System.EventHandler(this.buttonKeyState_Click);
            this.buttonKeyState.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonKeyState_KeyDown);
            // 
            // tabPageAlpha
            // 
            this.tabPageAlpha.Controls.Add(this.comboBoxAlpha);
            this.tabPageAlpha.Controls.Add(this.buttonAlpha);
            this.tabPageAlpha.Controls.Add(this.label2);
            this.tabPageAlpha.Location = new System.Drawing.Point(4, 30);
            this.tabPageAlpha.Name = "tabPageAlpha";
            this.tabPageAlpha.Size = new System.Drawing.Size(232, 199);
            this.tabPageAlpha.Text = "Alpha Mode";
            // 
            // comboBoxAlpha
            // 
            this.comboBoxAlpha.Items.Add("Off");
            this.comboBoxAlpha.Items.Add("On");
            this.comboBoxAlpha.Location = new System.Drawing.Point(128, 64);
            this.comboBoxAlpha.Name = "comboBoxAlpha";
            this.comboBoxAlpha.Size = new System.Drawing.Size(64, 27);
            this.comboBoxAlpha.TabIndex = 0;
            // 
            // buttonAlpha
            // 
            this.buttonAlpha.Location = new System.Drawing.Point(80, 136);
            this.buttonAlpha.Name = "buttonAlpha";
            this.buttonAlpha.Size = new System.Drawing.Size(72, 24);
            this.buttonAlpha.TabIndex = 1;
            this.buttonAlpha.Text = "Update";
            this.buttonAlpha.Click += new System.EventHandler(this.buttonAlpha_Click);
            this.buttonAlpha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonAlpha_KeyDown);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 32);
            this.label2.Text = "Alpha Mode:";
            // 
            // tabPageKeyLite
            // 
            this.tabPageKeyLite.Controls.Add(this.comboBoxKeyLite);
            this.tabPageKeyLite.Controls.Add(this.textBoxTimeout);
            this.tabPageKeyLite.Controls.Add(this.buttonKeyLite);
            this.tabPageKeyLite.Controls.Add(this.label7);
            this.tabPageKeyLite.Controls.Add(this.label3);
            this.tabPageKeyLite.Location = new System.Drawing.Point(4, 30);
            this.tabPageKeyLite.Name = "tabPageKeyLite";
            this.tabPageKeyLite.Size = new System.Drawing.Size(232, 199);
            this.tabPageKeyLite.Text = "Key Lite";
            // 
            // comboBoxKeyLite
            // 
            this.comboBoxKeyLite.Items.Add("Off");
            this.comboBoxKeyLite.Items.Add("On");
            this.comboBoxKeyLite.Items.Add("TrackBackLight");
            this.comboBoxKeyLite.Items.Add("Timeout");
            this.comboBoxKeyLite.Location = new System.Drawing.Point(136, 48);
            this.comboBoxKeyLite.Name = "comboBoxKeyLite";
            this.comboBoxKeyLite.Size = new System.Drawing.Size(88, 27);
            this.comboBoxKeyLite.TabIndex = 0;
            // 
            // textBoxTimeout
            // 
            this.textBoxTimeout.Location = new System.Drawing.Point(136, 96);
            this.textBoxTimeout.Name = "textBoxTimeout";
            this.textBoxTimeout.Size = new System.Drawing.Size(64, 27);
            this.textBoxTimeout.TabIndex = 1;
            // 
            // buttonKeyLite
            // 
            this.buttonKeyLite.Location = new System.Drawing.Point(80, 144);
            this.buttonKeyLite.Name = "buttonKeyLite";
            this.buttonKeyLite.Size = new System.Drawing.Size(72, 24);
            this.buttonKeyLite.TabIndex = 2;
            this.buttonKeyLite.Text = "Update";
            this.buttonKeyLite.Click += new System.EventHandler(this.buttonKeyLite_Click);
            this.buttonKeyLite.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonKeyLite_KeyDown);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(24, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 24);
            this.label7.Text = "Key Lite Timeout:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 32);
            this.label3.Text = "Key Lite State:";
            // 
            // buttonAbout
            // 
            this.buttonAbout.Location = new System.Drawing.Point(50, 237);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(64, 24);
            this.buttonAbout.TabIndex = 0;
            this.buttonAbout.Text = "About";
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            this.buttonAbout.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonAbout_KeyDown);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(125, 237);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(64, 24);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "Exit";
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            this.buttonExit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonExit_KeyDown);
            // 
            // KBForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 270);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.buttonExit);
            this.Name = "KBForm";
            this.Text = "CS_KeyboardSample1";
            this.Load += new System.EventHandler(this.KBForm_Load);
            this.Resize += new System.EventHandler(this.KBForm_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPageState.ResumeLayout(false);
            this.tabPageAlpha.ResumeLayout(false);
            this.tabPageKeyLite.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        /// <summary>
        /// This function does the (initial) scaling of the form
        /// by re-setting the related parameters (if required) &
        /// then calling the Scale(...) internally. 
        /// </summary>
        /// 
        public void DoScale()
        {
            if (Screen.PrimaryScreen.Bounds.Width > Screen.PrimaryScreen.Bounds.Height)
            {
                bPortrait = false; // If the display orientation is not portrait (so it's landscape), set the flag to false.
            }

            if (this.WindowState == FormWindowState.Maximized)    // If the form is maximized by default.
            {
                this.bSkipMaxLen = true; // we need to skip the max. length restriction
            }

            if ((Symbol.Win32.PlatformType.IndexOf("WinCE") != -1) || (Symbol.Win32.PlatformType.IndexOf("WindowsCE") != -1) || (Symbol.Win32.PlatformType.IndexOf("Windows CE") != -1)) // Only on Windows CE devices
            {
                this.resWidthReference = this.Width;   // The width of the form at design time (in pixels) is obtained from the platorm.
                this.resHeightReference = this.Height; // The height of the form at design time (in pixels) is obtained from the platform.
            }

            Scale(this); // Initial scaling of the GUI
        }

        /// <summary>
        /// This function scales the given Form & its child controls in order to
        /// make them completely viewable, based on the screen width & height.
        /// </summary>
        private static void Scale(KBForm frm)
        {
            int PSWAW = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;    // The width of the working area (in pixels).
            int PSWAH = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;   // The height of the working area (in pixels).

            // The entire screen has been taken in to account below 
            // in order to decide the half (S)VGA settings etc.
            if (!((Screen.PrimaryScreen.Bounds.Width <= (1.5) * (Screen.PrimaryScreen.Bounds.Height))
            && (Screen.PrimaryScreen.Bounds.Height <= (1.5) * (Screen.PrimaryScreen.Bounds.Width))))
            {
                if ((Screen.PrimaryScreen.Bounds.Width) > (Screen.PrimaryScreen.Bounds.Height))
                {
                    PSWAW = (int)((1.33) * PSWAH);  // If the width/height ratio goes beyond 1.5,
                    // the (longer) effective width is made shorter.
                }

            }

            System.Drawing.Graphics graphics = frm.CreateGraphics();

            float dpiX = graphics.DpiX; // Get the horizontal DPI value.

            if (frm.bInitialScale == true) // If an initial scale (from scratch)
            {
                if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1) // If the platform is either Pocket PC or Windows Mobile
                {
                    frm.Width = PSWAW;  // Set the form width. However this setting
                    // would be the ultimate one for Pocket PC (& Windows Mobile)devices.
                    // Just for the sake of consistency, it's explicitely specified here.
                }
                else
                {
                    frm.Width = (int)((frm.Width) * (PSWAW)) / (frm.resWidthReference); // Set the form width for others (Windows CE devices).

                }
            }
            if ((frm.Width <= maxLength * dpiX) || frm.bSkipMaxLen == true) // The calculation of the width & left values for each control
            // without taking the maximum length restriction into consideration.
            {
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {
                    cntrl.Width = ((cntrl.Width) * (frm.Width)) / (frm.resWidthReference);
                    cntrl.Left = ((cntrl.Left) * (frm.Width)) / (frm.resWidthReference);

                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Width = (((cntrl2.Width) * (frm.Width)) / (frm.resWidthReference));
                                cntrl2.Left = (((cntrl2.Left) * (frm.Width)) / (frm.resWidthReference));
                            }
                        }
                    }
                }

            }
            else
            {   // The calculation of the width & left values for each control
                // with the maximum length restriction taken into consideration.
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {
                    cntrl.Width = (int)(((cntrl.Width) * (PSWAW) * (maxLength * dpiX)) / (frm.resWidthReference * (frm.Width)));
                    cntrl.Left = (int)(((cntrl.Left) * (PSWAW) * (maxLength * dpiX)) / (frm.resWidthReference * (frm.Width)));

                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Width = (int)(((cntrl2.Width) * (PSWAW) * (maxLength * dpiX)) / (frm.resWidthReference * (frm.Width)));
                                cntrl2.Left = (int)(((cntrl2.Left) * (PSWAW) * (maxLength * dpiX)) / (frm.resWidthReference * (frm.Width)));
                            }
                        }
                    }
                }

                frm.Width = (int)((frm.Width) * (maxLength * dpiX)) / (frm.Width);

            }

            frm.resWidthReference = frm.Width; // Set the reference width to the new value.


            // A similar calculation is performed below for the height & top values for each control ...

            if (!((Screen.PrimaryScreen.Bounds.Width <= (1.5) * (Screen.PrimaryScreen.Bounds.Height))
            && (Screen.PrimaryScreen.Bounds.Height <= (1.5) * (Screen.PrimaryScreen.Bounds.Width))))
            {
                if ((Screen.PrimaryScreen.Bounds.Height) > (Screen.PrimaryScreen.Bounds.Width))
                {
                    PSWAH = (int)((1.33) * PSWAW);
                }

            }

            float dpiY = graphics.DpiY;

            if (frm.bInitialScale == true)
            {
                if (Symbol.Win32.PlatformType.IndexOf("PocketPC") != -1)
                {
                    frm.Height = PSWAH;
                }
                else
                {
                    frm.Height = (int)((frm.Height) * (PSWAH)) / (frm.resHeightReference);

                }
            }

            if ((frm.Height <= maxLength * dpiY) || frm.bSkipMaxLen == true)
            {
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {

                    cntrl.Height = ((cntrl.Height) * (frm.Height)) / (frm.resHeightReference);
                    cntrl.Top = ((cntrl.Top) * (frm.Height)) / (frm.resHeightReference);


                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Height = ((cntrl2.Height) * (frm.Height)) / (frm.resHeightReference);
                                cntrl2.Top = ((cntrl2.Top) * (frm.Height)) / (frm.resHeightReference);
                            }
                        }
                    }

                }

            }
            else
            {
                foreach (System.Windows.Forms.Control cntrl in frm.Controls)
                {

                    cntrl.Height = (int)(((cntrl.Height) * (PSWAH) * (maxLength * dpiY)) / (frm.resHeightReference * (frm.Height)));
                    cntrl.Top = (int)(((cntrl.Top) * (PSWAH) * (maxLength * dpiY)) / (frm.resHeightReference * (frm.Height)));


                    if (cntrl is System.Windows.Forms.TabControl)
                    {
                        foreach (System.Windows.Forms.TabPage tabPg in cntrl.Controls)
                        {
                            foreach (System.Windows.Forms.Control cntrl2 in tabPg.Controls)
                            {
                                cntrl2.Height = (int)(((cntrl2.Height) * (PSWAH) * (maxLength * dpiY)) / (frm.resHeightReference * (frm.Height)));
                                cntrl2.Top = (int)(((cntrl2.Top) * (PSWAH) * (maxLength * dpiY)) / (frm.resHeightReference * (frm.Height)));
                            }
                        }
                    }

                }

                frm.Height = (int)((frm.Height) * (maxLength * dpiY)) / (frm.Height);

            }

            frm.resHeightReference = frm.Height;

            if (frm.bInitialScale == true)
            {
                frm.bInitialScale = false; // If this was the initial scaling (from scratch), it's now complete.
            }
            if (frm.bSkipMaxLen == true)
            {
                frm.bSkipMaxLen = false; // No need to consider the maximum length restriction now.
            }


        }

        /// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main() 
		{
            KBForm kbf = new KBForm();
            kbf.DoScale();
            Application.Run(kbf);

		}

		/// <summary>
		/// Occurs before the form is displayed for the first time.
		/// </summary>
		private void KBForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				keypad = new Symbol.Keyboard.KeyPad();
			}
			catch
			{
				MessageBox.Show("Keyboard support not available!", this.Text);
				this.Close();
				return;
			}
			try
			{
				keypad.KeyStateNotify +=new Symbol.Keyboard.KeyPad.KeyboardEventHandler(keypad_KeyStateNotify);
				keypad.AlphaNotify +=new Symbol.Keyboard.KeyPad.KeyboardEventHandler(keypad_AlphaNotify);
			}
			catch
			{
			}
		}

		/// <summary>
		/// Refresh some controls in the tab pages with the current settings.
		/// </summary>
		private void RefreshTabPages()
		{
			try
			{
				this.comboBoxKeyLite.SelectedIndex = (int)keypad.KeyLiteMode;
				this.textBoxTimeout.Text = keypad.KeyLiteTimeout.ToString();
			}
			catch
			{
				this.comboBoxKeyLite.Enabled = false;
				this.buttonKeyLite.Enabled = false;
			}

			try
			{
				this.comboBoxAlpha.SelectedIndex = keypad.AlphaMode ? 1 : 0;
			}
			catch
			{
				this.comboBoxAlpha.Enabled = false;
				this.buttonAlpha.Enabled = false;
			}
		}

		/// <summary>
		/// Refresh the tab pages when the selection changed.
		/// </summary>
		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			RefreshTabPages();
		}

		/// <summary>
		/// Handle the AlphaNotify event.
		/// </summary>
		private void keypad_AlphaNotify(object sender, Symbol.Keyboard.KeyboardEventArgs e)
		{
			this.comboBoxAlpha.SelectedIndex = keypad.AlphaMode ? 1 : 0;
			this.Update();
		}

		/// <summary>
		/// Handle the KeyStateNotify event.
		/// </summary>
		private void keypad_KeyStateNotify(object sender, Symbol.Keyboard.KeyboardEventArgs e)
		{
            // Reset check box states
            checkBoxOrangeShiftLock.Checked = false;
            checkBoxFuncLock.Checked = false;
            checkBoxOrangeLock.Checked = false;
            checkBoxNumLock.Checked = false;
            checkBoxShiftLock.Checked = false;
            checkBoxUnShift.Checked = false;
            checkBoxShift.Checked = false;
            checkBoxCtrl.Checked = false;
            checkBoxAlt.Checked = false;
            checkBoxNum.Checked = false;
            checkBoxCaps.Checked = false;
            checkBoxFunc.Checked = false;
            checkBoxOrangeTemp.Checked = false;

            bool lockedState = false;

            // Checking for a lock first as it cannot be combined with others
            switch (e.KeyState)
            {
                case KeyStates.KEYSTATE_ORANGE_SHIFT_LOCK:
                    checkBoxOrangeShiftLock.Checked = true;
                    lockedState = true;
                    break;
                case KeyStates.KEYSTATE_FUNCTION_LOCK:
                    checkBoxFuncLock.Checked = true;
                    lockedState = true;
                    break;
                case KeyStates.KEYSTATE_ORANGE_LOCK:
                    checkBoxOrangeLock.Checked = true;
                    lockedState = true;
                    break;
                case KeyStates.KEYSTATE_NUMERIC_LOCK:
                    checkBoxNumLock.Checked = true;
                    lockedState = true;
                    break;
                case KeyStates.KEYSTATE_SHIFT_LOCK:
                    checkBoxShiftLock.Checked = true;
                    lockedState = true;
                    break;
                default:
                    break;
            }

            if (lockedState)
            {
                // No need to continue if a locked state
                this.Update();
                return;
            }

            // Process unlock states if any
            this.checkBoxUnShift.Checked = (e.KeyState & KeyStates.KEYSTATE_UNSHIFT) != 0;
            this.checkBoxShift.Checked = (e.KeyState & KeyStates.KEYSTATE_SHIFT) != 0;
            this.checkBoxCtrl.Checked = (e.KeyState & KeyStates.KEYSTATE_CTRL) != 0;
            this.checkBoxAlt.Checked = (e.KeyState & KeyStates.KEYSTATE_ALT) != 0;
            this.checkBoxNum.Checked = (e.KeyState & KeyStates.KEYSTATE_NUMLOCK) != 0;
            this.checkBoxCaps.Checked = (e.KeyState & KeyStates.KEYSTATE_CAPSLOCK) != 0;
            this.checkBoxFunc.Checked = (e.KeyState & KeyStates.KEYSTATE_FUNC) != 0;
            this.checkBoxOrangeTemp.Checked = (e.KeyState & KeyStates.KEYSTATE_ORANGE_TEMP) != 0;

            this.Update();
		}

		/// <summary>
		/// Set KeyState when the buttonKeyState is clicked.
		/// </summary>
		private void buttonKeyState_Click(object sender, System.EventArgs e)
		{
            try
            {
                int state = 0;
                if (this.checkBoxUnShift.Checked)
                    state |= KeyStates.KEYSTATE_UNSHIFT;
                if (this.checkBoxShift.Checked)
                    state |= KeyStates.KEYSTATE_SHIFT;
                if (this.checkBoxShiftLock.Checked)
                    state |= KeyStates.KEYSTATE_SHIFT_LOCK;
                if (this.checkBoxCtrl.Checked)
                    state |= KeyStates.KEYSTATE_CTRL;
                if (this.checkBoxAlt.Checked)
                    state |= KeyStates.KEYSTATE_ALT;
                if (this.checkBoxNum.Checked)
                    state |= KeyStates.KEYSTATE_NUMLOCK;
                if (this.checkBoxNumLock.Checked)
                    state |= KeyStates.KEYSTATE_NUMERIC_LOCK;
                if (this.checkBoxCaps.Checked)
                    state |= KeyStates.KEYSTATE_CAPSLOCK;
                if (this.checkBoxFunc.Checked)
                    state |= KeyStates.KEYSTATE_FUNC;
                if (this.checkBoxFuncLock.Checked)
                    state |= KeyStates.KEYSTATE_FUNCTION_LOCK;
                if (this.checkBoxOrangeTemp.Checked)
                    state |= KeyStates.KEYSTATE_ORANGE_TEMP;
                if (this.checkBoxOrangeLock.Checked)
                    state |= KeyStates.KEYSTATE_ORANGE_LOCK;
                if (this.checkBoxOrangeShiftLock.Checked)
                    state |= KeyStates.KEYSTATE_ORANGE_SHIFT_LOCK;

                keypad.SetKeyState(state, 0, this.checkBoxReg1.Checked);
            }
            catch (Symbol.Exceptions.OperationFailureException opEx)
            {
                MessageBox.Show("OperationFailureException : " + opEx.Message + ", result = " + opEx.Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.Message);
            }
		}

        private void buttonKeyState_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                buttonKeyState_Click(this, e);
        }

		/// <summary>
		/// Set/clear Alpha mode when the buttonAlpha is clicked.
		/// </summary>
		private void buttonAlpha_Click(object sender, System.EventArgs e)
		{
            try
            {
                keypad.AlphaMode = (this.comboBoxAlpha.SelectedItem.ToString() == "On");
            }
            catch (Symbol.Exceptions.OperationFailureException opEx)
            {
                MessageBox.Show("OperationFailureException : " + opEx.Message + ", result = " + opEx.Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.Message);
            }
		}

        private void buttonAlpha_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                buttonAlpha_Click(this, e);
        }

		/// <summary>
		/// Update Key Lite state when the buttonKeyLite is clicked.
		/// </summary>
		private void buttonKeyLite_Click(object sender, System.EventArgs e)
		{
            try
            {
                keypad.KeyLiteTimeout = Int32.Parse(this.textBoxTimeout.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update KeyLiteTimeout - " + ex.Message);
                this.textBoxTimeout.Text = keypad.KeyLiteTimeout.ToString();
            }

			try
			{
				if (this.comboBoxKeyLite.SelectedIndex != -1)
					keypad.KeyLiteMode = (Symbol.Keyboard.KeyLiteStates)this.comboBoxKeyLite.SelectedIndex;
			}
			catch (Exception)
			{
				MessageBox.Show("Failed to set KeyLiteMode. The mode may be unsupported.");
				this.comboBoxKeyLite.SelectedIndex = (int)keypad.KeyLiteMode;
			}
			
		}

        private void buttonKeyLite_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                buttonKeyLite_Click(this, e);
        }

		/// <summary>
		/// Display the About box when buttonAbout is clicked.
		/// </summary>
		private void buttonAbout_Click(object sender, System.EventArgs e)
		{
			string sVerInfo = "CS_KeyboardSample1\r\n";
			
			sVerInfo += "Assembly Version \t - v" + keypad.Version.AssemblyVersion + "\r\n";

			Symbol.StandardForms.About.Run(null, sVerInfo);

            this.buttonAbout.Focus();
		}

        private void buttonAbout_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                buttonAbout_Click(this, e);
		}

		/// <summary>
		/// Close the application when buttonExit is clicked.
		/// </summary>
		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			keypad.Dispose();
			this.Close();
		}

        private void buttonExit_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
                buttonExit_Click(this, e);
        }

		/// <summary>
		/// Make sure UNSHIFT, SHIFT, SHIFT_LOCK are not checked together.
		/// </summary>
		private void checkBoxUnShift_CheckStateChanged(object sender, System.EventArgs e)
		{
            if (checkBoxUnShift.Checked)
            {
                // When select unshift, both others will uncheck
                checkBoxShift.Checked = false;
                checkBoxShiftLock.Checked = false;
            }
            else
            {
                // When unselect unshift, it goes to shift state if not lock state activated
                if (!checkBoxShiftLock.Checked)
                {
                    checkBoxShift.Checked = true;
                }
            }
		}

		/// <summary>
        /// Make sure UNSHIFT, SHIFT, SHIFT_LOCK are not checked together.
		/// </summary>
		private void checkBoxShift_CheckStateChanged(object sender, System.EventArgs e)
		{
            if (checkBoxShift.Checked)
            {
                // When select shift, both others will uncheck
                checkBoxUnShift.Checked = false;
                checkBoxShiftLock.Checked = false;
            }
            else
            {
                // When unselect shift, it goes to unshift state if not lock state activated
                if (!checkBoxShiftLock.Checked)
                {
                    checkBoxUnShift.Checked = true;
                }
            }
		}

        /// <summary>
        /// Make sure UNSHIFT, SHIFT, SHIFT_LOCK are not checked together.
        /// </summary>
        private void checkBoxShiftLock_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxShiftLock.Checked)
            {
                // When select shiftlock, both others will uncheck
                checkBoxUnShift.Checked = false;
                checkBoxShift.Checked = false;
            }
            else
            {
                // When unselect shiftlock, it goes to unshift state if not shift state activated
                if (!checkBoxShift.Checked)
                {
                    checkBoxUnShift.Checked = true;
                }
            }
        }

        private void allCheckBoxes_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the key pressed was an enter button (character code 13)
            if (e.KeyValue == (char)13)
            {
                ((System.Windows.Forms.CheckBox)(sender)).Checked = !(((System.Windows.Forms.CheckBox)(sender)).Checked);
            }
		}

        private void checkBoxNum_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxNum.Checked)
            {
                // Only one can be checked, but both can be unchecked
                checkBoxNumLock.Checked = false;
            }
        }

        private void checkBoxNumLock_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxNumLock.Checked)
            {
                // Only one can be checked, but both can be unchecked
                checkBoxNum.Checked = false;
            }
        }

        private void checkBoxOrangeTemp_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxOrangeTemp.Checked)
            {
                // Only one can be checked, but both can be unchecked
                checkBoxOrangeLock.Checked = false;
            }
        }

        private void checkBoxOrangeLock_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxOrangeLock.Checked)
            {
                // Only one can be checked, but both can be unchecked
                checkBoxOrangeTemp.Checked = false;
            }
        }

        private void checkBoxFunc_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxFunc.Checked)
            {
                // Only one can be checked, but both can be unchecked
                checkBoxFuncLock.Checked = false;
            }
        }

        private void checkBoxFuncLock_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxFuncLock.Checked)
            {
                // Only one can be checked, but both can be unchecked
                checkBoxFunc.Checked = false;
            }
        }

        private void KBForm_Resize(object sender, EventArgs e)
        {
            if (bInitialScale == true)
            {
                return; // Return if the initial scaling (from scratch)is not complete.
            }

            if (Screen.PrimaryScreen.Bounds.Width > Screen.PrimaryScreen.Bounds.Height) // If landscape orientation
            {
                if (bPortrait != false) // If an orientation change has occured to landscape
                {
                    bPortrait = false; // Set the orientation flag accordingly.
                    bInitialScale = true; // An initial scaling is required due to orientation change.
                    Scale(this); // Scale the GUI.
                }
                else
                {   // No orientation change has occured
                    bSkipMaxLen = true; // Initial scaling is now complete, so skipping the max. length restriction is now possible.
                    Scale(this); // Scale the GUI.
                }
            }
            else
            {
                // Similarly for the portrait orientation...
                if (bPortrait != true)
                {
                    bPortrait = true;
                    bInitialScale = true;
                    Scale(this);
                }
                else
                {
                    bSkipMaxLen = true;
                    Scale(this);
                }
            }
        }
	}
}