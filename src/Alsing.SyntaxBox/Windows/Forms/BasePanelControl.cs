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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Alsing.Drawing;

namespace Alsing.Windows.Forms
{
    [ToolboxItem(true)]
    public partial class BasePanelControl : Panel
    {
        private const int WS_BORDER = unchecked(0x00800000);
        private const int WS_EX_CLIENTEDGE = unchecked(0x00000200);
        private Color borderColor = Color.Black;
        private BorderStyle borderStyle;
        private bool RunOnce = true;


        public BasePanelControl()
        {
            SetStyle(ControlStyles.EnableNotifyMessage, true);
            BorderStyle = BorderStyle.FixedSingle;
            InitializeComponent();
        }

        public Color BorderColor
        {
            get { return borderColor; }

            set
            {
                borderColor = value;
                Refresh();
                Invalidate();
                UpdateStyles();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                if (BorderStyle == BorderStyle.None)
                    return cp;

                cp.ExStyle &= (~WS_EX_CLIENTEDGE);
                cp.Style &= (~WS_BORDER);

                return cp;
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public new BorderStyle BorderStyle
        {
            get { return borderStyle; }
            set
            {
                try
                {
                    if (borderStyle != value)
                    {
                        borderStyle = value;
                        UpdateStyles();
                        Refresh();
                    }
                }
                catch {}
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)] // Obsolete("Do not use!", true)
        public override Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { base.BackgroundImage = value; }
        }


        [Browsable(false)]
        public int ClientWidth
        {
            get { return Width - (BorderWidth*2); }
        }

        [Browsable(false)]
        public int ClientHeight
        {
            get { return Height - (BorderWidth*2); }
        }

        [Browsable(false)]
        public int BorderWidth
        {
            get
            {
                switch (borderStyle)
                {
                    case BorderStyle.None:
                        {
                            return 0;
                        }
                    case BorderStyle.Sunken:
                        {
                            return 2;
                        }
                    case BorderStyle.SunkenThin:
                        {
                            return 1;
                        }
                    case BorderStyle.Raised:
                        {
                            return 2;
                        }

                    case BorderStyle.Etched:
                        {
                            return 2;
                        }
                    case BorderStyle.Bump:
                        {
                            return 6;
                        }
                    case BorderStyle.FixedSingle:
                        {
                            return 1;
                        }
                    case BorderStyle.FixedDouble:
                        {
                            return 2;
                        }
                    case BorderStyle.RaisedThin:
                        {
                            return 1;
                        }
                    case BorderStyle.Dotted:
                        {
                            return 1;
                        }
                    case BorderStyle.Dashed:
                        {
                            return 1;
                        }
                }


                return Height;
            }
        }

        public event EventHandler Load = null;

        protected virtual void OnLoad(EventArgs e)
        {
            if (Load != null)
                Load(this, e);
            Refresh();
        }

        protected override unsafe void WndProc(ref Message m)
        {
            if (m.Msg == (int) WindowMessage.WM_NCPAINT)
            {
                try
                {
                    RenderBorder();
                }
                catch {}
                base.WndProc(ref m);
                //	RenderBorder();
            }
            else if (m.Msg == (int) WindowMessage.WM_SHOWWINDOW)
            {
                if (RunOnce)
                {
                    RunOnce = false;
                    OnLoad(null);
                    base.WndProc(ref m);
                    UpdateStyles();
                }
                else
                {
                    UpdateStyles();
                    base.WndProc(ref m);
                }
            }
            else if (m.Msg == (int) WindowMessage.WM_NCCREATE)
            {
                base.WndProc(ref m);
            }
            else if (m.Msg == (int) WindowMessage.WM_NCCALCSIZE)
            {
                if (m.WParam == (IntPtr) 0)
                {
                    //APIRect* pRC=(APIRect*)m.LParam;
                    //pRC->left -=3;
                    base.WndProc(ref m);
                }
                else if (m.WParam == (IntPtr) 1)
                {
                    var pNCP = (_NCCALCSIZE_PARAMS*) m.LParam;

                    base.WndProc(ref m);

                    int t = pNCP->NewRect.top + BorderWidth;
                    int l = pNCP->NewRect.left + BorderWidth;
                    int b = pNCP->NewRect.bottom - BorderWidth;
                    int r = pNCP->NewRect.right - BorderWidth;


                    pNCP->NewRect.top = t;
                    pNCP->NewRect.left = l;
                    pNCP->NewRect.right = r;
                    pNCP->NewRect.bottom = b;

                    return;
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void RenderBorder()
        {
            IntPtr hdc = NativeMethods.GetWindowDC(Handle);
            var s = new APIRect();
            NativeMethods.GetWindowRect(Handle, ref s);

            using (Graphics g = Graphics.FromHdc(hdc))
            {
                DrawingTools.DrawBorder((BorderStyle2) (int) BorderStyle, BorderColor, g,
                                        new Rectangle(0, 0, s.Width, s.Height));
            }
            NativeMethods.ReleaseDC(Handle, hdc);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
        }
    }
}