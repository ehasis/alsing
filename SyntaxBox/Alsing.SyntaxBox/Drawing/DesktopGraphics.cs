using System;
using System.Drawing;
using Alsing.Windows;

namespace Alsing.Drawing
{
    public class DesktopGraphics : IDisposable
    {
        public readonly Graphics Graphics;
        protected IntPtr handle = new IntPtr(0);
        protected IntPtr hdc = new IntPtr(0);

        public DesktopGraphics()
        {
            handle = NativeMethods.GetDesktopWindow();
            hdc = NativeMethods.GetWindowDC(hdc);
            Graphics = Graphics.FromHdc(hdc);
        }

        #region IDisposable Members

        public void Dispose()
        {
            NativeMethods.ReleaseDC(handle, hdc);
        }

        #endregion
    }
}