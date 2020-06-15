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
using System.Collections;
using System.Drawing;
using Alsing.Windows;

namespace Alsing.Drawing.GDI
{
    public class FontEnum
    {
        private Hashtable Fonts;


        public ICollection EnumFonts()
        {
            var bmp = new Bitmap(10, 10);
            Graphics g = Graphics.FromImage(bmp);

            IntPtr hDC = g.GetHdc();
            Fonts = new Hashtable();
            var lf = new LogFont {lfCharSet = 1};
            FONTENUMPROC callback = CallbackFunc;
            NativeMethods.EnumFontFamiliesEx(hDC, lf, callback, 0, 0);

            g.ReleaseHdc(hDC);
            g.Dispose();
            bmp.Dispose();
            return Fonts.Keys;
        }

        private int CallbackFunc(ENUMLOGFONTEX f, int a, int b, int LParam)
        {
            Fonts[f.elfLogFont.lfFaceName] = "abc";
            return 1;
        }
    }
}