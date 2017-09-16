using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CS_DocCapSample1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            MainForm mainForm = new MainForm();
            Application.Run(mainForm);
        }
    }
}