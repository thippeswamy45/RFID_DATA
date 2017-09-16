using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CS_RFID3Sample6
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new AppForm());
        }
    }
}