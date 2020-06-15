// *
// * Copyright (C) 2008 Roger Alsing : http://www.RogerAlsing.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using Alsing.Drawing.GDI;

namespace Alsing.Windows.Forms.SyntaxBox.Painter
{
    /// <summary>
    /// Struct used by the NativePainter class.
    /// </summary>
    public struct RenderItems
    {
        /// <summary>
        /// For internal use only
        /// </summary>
        public GDISurface BackBuffer; //backbuffer surface

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIBrush BackgroundBrush; //background brush

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIFont FontBold; //Font , bold

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIFont FontBoldItalic; //Font , bold & italic

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIFont FontBoldItalicUnderline; //Font , bold & italic

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIFont FontBoldUnderline; //Font , bold

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIFont FontItalic; //Font , italic

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIFont FontItalicUnderline; //Font , italic

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIFont FontNormal; //Font , no decoration		

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIFont FontUnderline; //Font , no decoration		

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIBrush GutterMarginBorderBrush; //Gutter magrin brush

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIBrush GutterMarginBrush; //Gutter magrin brush

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIBrush HighLightLineBrush; //background brush

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIBrush LineNumberMarginBorderBrush; //linenumber margin brush

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIBrush LineNumberMarginBrush; //linenumber margin brush

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDIBrush OutlineBrush; //background brush

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDISurface SelectionBuffer; //backbuffer surface

        /// <summary>
        /// For internal use only
        /// </summary>
        public GDISurface StringBuffer; //backbuffer surface
    }
}
