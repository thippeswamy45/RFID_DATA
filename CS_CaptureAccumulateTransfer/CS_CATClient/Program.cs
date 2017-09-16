using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CS_CATSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            FormMain fm = new FormMain();
            fm.DoScale();
            Application.Run(fm);
        }
    }
}