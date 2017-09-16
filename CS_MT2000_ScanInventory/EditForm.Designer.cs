namespace CS_MT2000_ScanInventory
{
	partial class EditForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.locationTextBox = new System.Windows.Forms.TextBox();
			this.quantityTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.barcodeTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(62)))), ((int)(((byte)(153)))));
			this.label1.Location = new System.Drawing.Point(8, 59);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 27);
			this.label1.Text = "Location:";
			// 
			// locationTextBox
			// 
			this.locationTextBox.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.locationTextBox.Location = new System.Drawing.Point(130, 54);
			this.locationTextBox.MaxLength = 8;
			this.locationTextBox.Name = "locationTextBox";
			this.locationTextBox.Size = new System.Drawing.Size(177, 32);
			this.locationTextBox.TabIndex = 1;
			this.locationTextBox.GotFocus += new System.EventHandler(this.locationTextBox_GotFocus);
			// 
			// quantityTextBox
			// 
			this.quantityTextBox.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.quantityTextBox.Location = new System.Drawing.Point(130, 94);
			this.quantityTextBox.MaxLength = 5;
			this.quantityTextBox.Name = "quantityTextBox";
			this.quantityTextBox.Size = new System.Drawing.Size(177, 32);
			this.quantityTextBox.TabIndex = 2;
			this.quantityTextBox.GotFocus += new System.EventHandler(this.quantityTextBox_GotFocus);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(62)))), ((int)(((byte)(153)))));
			this.label2.Location = new System.Drawing.Point(8, 99);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116, 27);
			this.label2.Text = "Quantity:";
			// 
			// barcodeTextBox
			// 
			this.barcodeTextBox.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.barcodeTextBox.Location = new System.Drawing.Point(130, 134);
			this.barcodeTextBox.Name = "barcodeTextBox";
			this.barcodeTextBox.Size = new System.Drawing.Size(177, 32);
			this.barcodeTextBox.TabIndex = 3;
			this.barcodeTextBox.GotFocus += new System.EventHandler(this.barcodeTextBox_GotFocus);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(62)))), ((int)(((byte)(153)))));
			this.label3.Location = new System.Drawing.Point(8, 139);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116, 27);
			this.label3.Text = "Barcode:";
			// 
			// EditForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
			this.ClientSize = new System.Drawing.Size(320, 240);
			this.Controls.Add(this.locationTextBox);
			this.Controls.Add(this.barcodeTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.quantityTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.LeftSoftKeyText = "Cancel";
			this.Name = "EditForm";
			this.RightSoftKeyText = "OK";
			this.TitleText = "Edit Item";
			this.LeftSoftKeyPressed += new System.EventHandler(this.EditForm_LeftSoftKeyPressed);
			this.RightSoftKeyPressed += new System.EventHandler(this.EditForm_RightSoftKeyPressed);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.quantityTextBox, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.barcodeTextBox, 0);
			this.Controls.SetChildIndex(this.locationTextBox, 0);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox barcodeTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox quantityTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox locationTextBox;
		private System.Windows.Forms.Label label1;
	}
}
