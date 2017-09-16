using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Symbol.RFID2;

namespace CS_RFID2_Sample
    {
        static class Program
        {
            public static ReaderModel m_ReaderModel;

            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            [MTAThread]
            static void Main()
            {
                #region CheckRunningInstance
                FileStream file;
                try
                {
                    file = File.Open(@"\Windows\CS_RFID2_SamplesStarted",
                             FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                }
                catch (IOException)
                {
                    return;
                }
                #endregion CheckRunningInstance

                switch (System.Environment.OSVersion.Version.ToString().Substring(0,3))
                {
                    case "5.0":                         //RD5000
                        m_ReaderModel = ReaderModel.RD5000;
                        Application.Run(new MainForm());
                        break;
                    case "5.1":                     //MC9090
                    default:
                        m_ReaderModel = ReaderModel.MC9090;
                        Application.Run(new MainFormMobil());
                        break;

                    //case "4.2":                     // MC9000 not supported
                    //default:
                        //m_ReaderModel = ReaderModel.MC9000;
                        //Application.Run(new MainFormMobil());
                        //break;
                    
                }
                
            }
        }
    }
