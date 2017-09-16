//-----------------------------------------------------------------------------------
// FILENAME: Program.cs
//
// Copyright © 2012 - 2013 Motorola Solutions, Inc. All rights reserved.
//
// DESCRIPTION: The source file for the application entry point.
//
//-----------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CS_SensorSample1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new MainForm());
        }
    }
}