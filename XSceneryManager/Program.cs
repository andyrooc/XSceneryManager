using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using log4net.Config;

namespace XSceneryManager
{
    static class Program
    {
        public static MainForm mf = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BasicConfigurator.Configure();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mf = new MainForm();
            Application.Run(mf);
        }
    }
}
