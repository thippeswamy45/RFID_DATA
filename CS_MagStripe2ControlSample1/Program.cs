using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CS_MagStripe2ControlSample1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Form1 f1 = new Form1();
            f1.DoScale();
            Application.Run(f1);
        }
    }
}