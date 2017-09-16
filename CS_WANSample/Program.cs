using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WANSampleTest
{
	static class Program
	{ 
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[MTAThread]
		static void Main()
		{
            FormMain fm = new FormMain(); // Creates the form object

            fm.DoScale(); // Scales the form.

            Application.Run(fm);
		}
	}
}