using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CS_VBusSample1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            VBusForm vf = new VBusForm();
            Application.Run(vf);
        }
    }
}