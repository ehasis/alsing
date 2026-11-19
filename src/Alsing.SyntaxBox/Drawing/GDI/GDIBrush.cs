// *
// * Copyright (C) 2008 Roger Alsing : http://www.RogerAlsing.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;
using System.Drawing;
using Alsing.Windows;

namespace Alsing.Drawing.GDI
{
    //wrapper class for gdi brushes
    public class GDIBrush : GDIObject
    {
        public IntPtr hBrush;
        protected bool mSystemBrush;

        public GDIBrush(Color color)
        {
            hBrush = NativeMethods.CreateSolidBrush(NativeMethods.ColorToInt(color));
            Create();
        }

        public GDIBrush(Bitmap pattern)
        {
            hBrush = NativeMethods.CreatePatternBrush(pattern.GetHbitmap());
            Create();
        }

        public GDIBrush(IntPtr hBMP_Pattern)
        {
            hBrush = NativeMethods.CreatePatternBrush(hBMP_Pattern);
            Create();
        }

        public GDIBrush(int Style, Color color)
        {
            hBrush = NativeMethods.CreateHatchBrush(Style, NativeMethods.ColorToInt(color));
            Create();
        }

        public GDIBrush(int BrushIndex)
        {
            hBrush = (IntPtr)BrushIndex;
            mSystemBrush = true;
            Create();
        }

        protected override void Destroy()
        {
            //only destroy if brush is created by us
            if (!mSystemBrush)
            {
                if (hBrush != (IntPtr)0)
                    NativeMethods.DeleteObject(hBrush);
            }

            base.Destroy();
            hBrush = IntPtr.Zero;
        }
    }
}