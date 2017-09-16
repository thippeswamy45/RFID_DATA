namespace CS_RFID3Sample6
{
    partial class RadioPowerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.radioState_CB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // radioState_CB
            // 
            this.radioState_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.radioState_CB.Items.Add("Off");
            this.radioState_CB.Items.Add("On");
            this.radioState_CB.Location = new System.Drawing.Point(135, 18);
            this.radioState_CB.Name = "radioState_CB";
            this.radioState_CB.Size = new System.Drawing.Size(89, 20);
            this.radioState_CB.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.Text = "Current State";
            // 
            // RadioPowerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.radioState_CB);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "RadioPowerForm";
            this.Text = "Radio Power";
            this.Load += new System.EventHandler(this.RadioPowerForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.RadioPowerForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox radioState_CB;
        private System.Windows.Forms.Label label1;
    }
}