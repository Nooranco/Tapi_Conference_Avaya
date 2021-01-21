using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OutgoingCalls
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += (s, e) => { Application.Exit(); };

            if (args.Length > 0)
            {
                Application.Run(new frmMain(
                    args[0].ToString(), 
                    args[1].ToString(), 
                    args[2].ToString()));
            }
            else
            {
                Application.Run(new frmMain());
            }
        }
    }
}