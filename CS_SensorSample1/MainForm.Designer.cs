namespace CS_SensorSample1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.VersionLabel = new System.Windows.Forms.Label();
            this.SensorLabel = new System.Windows.Forms.Label();
            this.DataLabel = new System.Windows.Forms.Label();
            this.UnitLabel = new System.Windows.Forms.Label();
            this.ExitButton = new System.Windows.Forms.Button();
            this.DataTextBox = new System.Windows.Forms.TextBox();
            this.UnitTextBox = new System.Windows.Forms.TextBox();
            this.UnitSensorLabel3 = new System.Windows.Forms.Label();
            this.SensorComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // VersionLabel
            // 
            this.VersionLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.VersionLabel.Location = new System.Drawing.Point(5, 14);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(144, 23);
            this.VersionLabel.Text = "Sensor Sample v2.0";
            // 
            // SensorLabel
            // 
            this.SensorLabel.Location = new System.Drawing.Point(5, 51);
            this.SensorLabel.Name = "SensorLabel";
            this.SensorLabel.Size = new System.Drawing.Size(51, 23);
            this.SensorLabel.Text = "Sensor:";
            // 
            // DataLabel
            // 
            this.DataLabel.Location = new System.Drawing.Point(5, 101);
            this.DataLabel.Name = "DataLabel";
            this.DataLabel.Size = new System.Drawing.Size(51, 23);
            this.DataLabel.Text = "Data:";
            // 
            // UnitLabel
            // 
            this.UnitLabel.Location = new System.Drawing.Point(5, 150);
            this.UnitLabel.Name = "UnitLabel";
            this.UnitLabel.Size = new System.Drawing.Size(51, 23);
            this.UnitLabel.Text = "Unit:";
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(13, 207);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(79, 28);
            this.ExitButton.TabIndex = 4;
            this.ExitButton.Text = "Exit";
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // DataTextBox
            // 
            this.DataTextBox.Location = new System.Drawing.Point(62, 101);
            this.DataTextBox.Name = "DataTextBox";
            this.DataTextBox.Size = new System.Drawing.Size(156, 23);
            this.DataTextBox.TabIndex = 6;
            // 
            // UnitTextBox
            // 
            this.UnitTextBox.Location = new System.Drawing.Point(62, 150);
            this.UnitTextBox.Name = "UnitTextBox";
            this.UnitTextBox.Size = new System.Drawing.Size(156, 23);
            this.UnitTextBox.TabIndex = 7;
            // 
            // UnitSensorLabel3
            // 
            this.UnitSensorLabel3.Font = new System.Drawing.Font("Tahoma", 6.5F, System.Drawing.FontStyle.Regular);
            this.UnitSensorLabel3.Location = new System.Drawing.Point(5, 191);
            this.UnitSensorLabel3.Name = "UnitSensorLabel3";
            this.UnitSensorLabel3.Size = new System.Drawing.Size(87, 13);
            // 
            // SensorComboBox
            // 
            this.SensorComboBox.Location = new System.Drawing.Point(62, 51);
            this.SensorComboBox.Name = "SensorComboBox";
            this.SensorComboBox.Size = new System.Drawing.Size(156, 23);
            this.SensorComboBox.TabIndex = 12;
            this.SensorComboBox.SelectedIndexChanged += new System.EventHandler(this.SensorComboBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 255);
            this.Controls.Add(this.SensorComboBox);
            this.Controls.Add(this.UnitSensorLabel3);
            this.Controls.Add(this.UnitTextBox);
            this.Controls.Add(this.DataTextBox);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.UnitLabel);
            this.Controls.Add(this.DataLabel);
            this.Controls.Add(this.SensorLabel);
            this.Controls.Add(this.VersionLabel);
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "CS_SensorSample1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label SensorLabel;
        private System.Windows.Forms.Label DataLabel;
        private System.Windows.Forms.Label UnitLabel;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.TextBox DataTextBox;
        private System.Windows.Forms.TextBox UnitTextBox;
        private System.Windows.Forms.Label UnitSensorLabel3;
        private System.Windows.Forms.ComboBox SensorComboBox;
    }
}

