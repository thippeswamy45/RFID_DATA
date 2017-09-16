using System;
using System.Windows.Forms;

using Symbol.MT2000.UserInterface;

namespace CS_MT2000_ScanInventory
{
	public partial class EditForm : Symbol.MT2000.UserInterface.BaseForm
	{
		// public variables
		public string ItemLocation;
		public uint ItemQuantity;
		public string ItemBarcode;

		// local variables
		private UnsignedIntegerValidator quantityValidator;

		/// <summary>
		/// initializes the form
		/// </summary>
		public EditForm(InventoryItem inventoryItem)
		{
			// initialize the controls
			InitializeComponent();
			TitleText = Properties.Resources.StrEditItem;
			LeftSoftKeyText = Properties.Resources.StrCancel;
			RightSoftKeyText = Properties.Resources.StrOK;

			// set the controls
			ItemLocation = inventoryItem.Location.Location;
			ItemQuantity = inventoryItem.Quantity;
			ItemBarcode = inventoryItem.Barcode;
			locationTextBox.Text = ItemLocation;
			quantityTextBox.Text = ItemQuantity.ToString();
			barcodeTextBox.Text = ItemBarcode;

			// create the quantity validator
			quantityValidator = new UnsignedIntegerValidator(1, Options.MaxQuantity, Options.MaxQuantityLength,
										string.Format(Properties.Resources.StrErrorBadQuantity, Options.MaxQuantity));
		}

		/// <summary>
		/// shows the form modally and waits for a soft key to be pressed
		/// </summary>
		/// <param name="owner">the EditForm will be displayed in front of this form</param>
		public void Show(Form owner)
		{
			Owner = owner;
			ShowDialog();
			Owner = null;
		}

		/// <summary>
		/// closes the form without saving the changes
		/// </summary>
		private void EditForm_LeftSoftKeyPressed(object sender, EventArgs e)
		{
			ItemLocation = "";
			Close();
		}

		/// <summary>
		/// closes the form and saves the changes
		/// </summary>
		private void EditForm_RightSoftKeyPressed(object sender, EventArgs e)
		{
			// make sure there's a location
			ItemLocation = locationTextBox.Text.Trim();
			if (string.IsNullOrEmpty(ItemLocation))
			{
				MsgBox.Error(this, Properties.Resources.StrErrorNoLocation);
				return;
			}

			// validate the quantity
			string value = quantityTextBox.Text.Trim();
			if (!quantityValidator.Validate(value))
			{
				return;
			}
			ItemQuantity = quantityValidator.Value;

			// make sure there's a barcode
			ItemBarcode = barcodeTextBox.Text;
			if (string.IsNullOrEmpty(ItemBarcode))
			{
				MsgBox.Error(this, Properties.Resources.StrErrorNoBarcode);
				return;
			}

			// close the form
			Close();
		}

		/// <summary>
		/// select the text when the location text box gets the focus
		/// </summary>
		private void locationTextBox_GotFocus(object sender, EventArgs e)
		{
			locationTextBox.SelectAll();
		}

		/// <summary>
		/// select the text when the quantity text box gets the focus
		/// </summary>
		private void quantityTextBox_GotFocus(object sender, EventArgs e)
		{
			quantityTextBox.SelectAll();
		}

		/// <summary>
		/// select the text when the barcode text box gets the focus
		/// </summary>
		private void barcodeTextBox_GotFocus(object sender, EventArgs e)
		{
			barcodeTextBox.SelectAll();
		}
	}
}
