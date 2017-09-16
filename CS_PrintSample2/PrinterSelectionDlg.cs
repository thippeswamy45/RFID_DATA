//--------------------------------------------------------------------
// FILENAME: PrinterSelectionDlg.cs
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
using System.ComponentModel;
using System.Windows.Forms;

using Symbol;
using Symbol.Printing;
using Symbol.StandardForms;

namespace CS_PrintSample2
{
	/// <summary>
	/// Summary description for PrinterSelectionDlg.
	/// </summary>
	public class PrinterSelectionDlg
	{
		private Symbol.Generic.Device printer;
		private int selectIndex;

		public PrinterSelectionDlg()
		{
			selectIndex = 0;
		}

		public DialogResult ShowDialog()
		{
            try
            {
			printer = Symbol.StandardForms.SelectDevice.Select("Printer",
						PrinterSettings.AvailableDevices,
						selectIndex);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Printer driver not found", "CS_PrinterSample2");
                return DialogResult.Cancel;
            }

			if ( printer == null )
			{
				return DialogResult.Cancel;
			}
			else
			{
				return DialogResult.OK;
			}
		}

		public string PrinterName
		{
			get
			{
				if ( printer == null )
				{
					return null;
				}

				return printer.DeviceName;
			}
		}

		public string SelectPrinterName
		{
			set
			{
				int i;
				selectIndex = 0;
				for (i=0; i<PrinterSettings.AvailableDevices.Length; i++)
					if (PrinterSettings.AvailableDevices[i].DeviceName == value)
					{
						selectIndex = i;
						break;
					}
			}
		}
	}
}
