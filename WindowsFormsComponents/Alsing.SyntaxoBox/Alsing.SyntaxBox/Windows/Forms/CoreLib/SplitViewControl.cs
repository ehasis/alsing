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
using System.Windows.Forms;

namespace Alsing.Windows.Forms.CoreLib
{
    /// <summary>
    /// Summary description for SplitView.
    /// </summary>
    public partial class SplitViewControl : Control
    {
        private enum SizeAction
        {
            None = 0,
            SizeH = 1,
            SizeV = 2,
            SizeA = 3
        }

        private Control _LowerLeft;
        private Control _LowerRight;
        private Control _UpperLeft;
        private Control _UpperRight;

        private SizeAction Action = 0;
        private bool FirstTime;
        private Point PrevPos = new Point(0);
        private Point StartPos = new Point(0, 0);

        /// <summary>
        /// Default constructor for the splitview control
        /// </summary>
        public SplitViewControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            SetStyle(ControlStyles.ContainerControl, true);

            InitializeComponent();

            DoResize();
            ReSize(0, 0);
        }

        /// <summary>
        /// Gets or Sets the control that should be confined to the upper left view.
        /// </summary>
        public Control UpperLeft
        {
            get { return _UpperLeft; }
            set
            {
                _UpperLeft = value;
                DoResize();
            }
        }

        /// <summary>
        /// Gets or Sets the control that should be confined to the upper right view.
        /// </summary>
        public Control UpperRight
        {
            get { return _UpperRight; }
            set
            {
                _UpperRight = value;
                DoResize();
            }
        }

        /// <summary>
        /// Gets or Sets the control that should be confined to the lower left view.
        /// </summary>
        public Control LowerLeft
        {
            get { return _LowerLeft; }
            set
            {
                _LowerLeft = value;
                DoResize();
            }
        }

        /// <summary>
        /// Gets or Sets the control that should be confined to the lower right view.
        /// </summary>
        public Control LowerRight
        {
            get { return _LowerRight; }
            set
            {
                _LowerRight = value;
                DoResize();
            }
        }

        public int SplitviewV
        {
            get { return Vertical.Left; }
            set
            {
                Vertical.Left = value;
                DoResize();
            }
        }

        public int SplitviewH
        {
            get { return Horizontal.Top; }
            set
            {
                Horizontal.Top = value;
                DoResize();
            }
        }

        /// <summary>
        /// an event fired when the split view is resized.
        /// </summary>
        public event EventHandler Resizing = null;

        /// <summary>
        /// an event fired when the top views are hidden.
        /// </summary>
        public event EventHandler HideTop = null;

        /// <summary>
        /// an event fired when the left views are hidden.
        /// </summary>
        public event EventHandler HideLeft = null;

        private void OnResizing()
        {
            if (Resizing != null)
                Resizing(this, new EventArgs());
        }

        private void OnHideLeft()
        {
            if (HideLeft != null)
                HideLeft(this, new EventArgs());
        }

        private void OnHideTop()
        {
            if (HideTop != null)
                HideTop(this, new EventArgs());
        }

        protected override void OnResize(EventArgs e)
        {
            DoResize();
        }

        private void DoResize()
        {
            //			int OldWidth=Horizontal.Width ;
            //			int OldHeight=Vertical.Height;
            int NewHeight = Height;
            int NewWidth = Width;

            if (NewHeight != 0 && NewWidth != 0)
            {
                SuspendLayout();
                //				Horizontal.Top = (int)(NewHeight*HorizontalPos);
                //				Vertical.Left =(int)(NewWidth*VerticalPos);
                //
                //				int CenterY=(Horizontal.Top+Horizontal.Height /2)-Center.Height/2;
                //				int CenterX=(Vertical.Left+Vertical.Width /2)-Center.Width /2;
                //
                //				Center.Location =new Point (CenterX,CenterY);

                //ReSize (0,0);
                ReSize2();
                OnResizing();

                if (Horizontal.Top < 15)
                {
                    Horizontal.Top = 0 - Horizontal.Height;
                    OnHideTop();
                }

                if (Vertical.Left < 15)
                {
                    Vertical.Left = 0 - Vertical.Width;
                    OnHideLeft();
                }

                Horizontal.Width = Width;
                Vertical.Height = Height;
                Horizontal.SendToBack();
                Vertical.SendToBack();
                Horizontal.BackColor = SystemColors.Control;
                Vertical.BackColor = SystemColors.Control;

                //this.SendToBack ();
                int RightLeft = Vertical.Left + Vertical.Width;
                int RightLowerTop = Horizontal.Top + Horizontal.Height;
                int RightWidth = Width - RightLeft;
                int LowerHeight = Height - RightLowerTop;
                int UpperHeight = Horizontal.Top;
                int LeftWidth = Vertical.Left;

                if (LowerRight != null)
                {
                    LowerRight.BringToFront();
                    LowerRight.SetBounds(RightLeft, RightLowerTop, RightWidth, LowerHeight);
                }
                if (UpperRight != null)
                {
                    UpperRight.BringToFront();
                    UpperRight.SetBounds(RightLeft, 0, RightWidth, UpperHeight);
                }

                if (LowerLeft != null)
                {
                    LowerLeft.BringToFront();
                    LowerLeft.SetBounds(0, RightLowerTop, LeftWidth, LowerHeight);
                }
                if (UpperLeft != null)
                {
                    UpperLeft.BringToFront();
                    UpperLeft.SetBounds(0, 0, LeftWidth, UpperHeight);
                }
                ResumeLayout(); //ggf
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            DoResize();
        }

        private void Horizontal_MouseDown(object sender, MouseEventArgs e)
        {
            Action = SizeAction.SizeH;
            StartPos = new Point(e.X, e.Y);
            Horizontal.BringToFront();
            Horizontal.BackColor = SystemColors.ControlDark;
            FirstTime = true;
        }

        private void Vertical_MouseDown(object sender, MouseEventArgs e)
        {
            Action = SizeAction.SizeV;
            StartPos = new Point(e.X, e.Y);
            Vertical.BringToFront();
            Vertical.BackColor = SystemColors.ControlDark;
            FirstTime = true;
        }

        private void Center_MouseDown(object sender, MouseEventArgs e)
        {
            Action = SizeAction.SizeA;
            StartPos = new Point(e.X, e.Y);
            Vertical.BringToFront();
            Horizontal.BringToFront();
            Vertical.BackColor = SystemColors.ControlDark;
            Horizontal.BackColor = SystemColors.ControlDark;
            FirstTime = true;
        }

        private void Horizontal_MouseUp(object sender, MouseEventArgs e)
        {
            int xDiff = 0;
            int yDiff = StartPos.Y - e.Y;
            //	StartPos=new Point (e.X,e.Y);
            ReSize(xDiff, yDiff);

            Action = SizeAction.None;
            DoResize();
        }

        private void Vertical_MouseUp(object sender, MouseEventArgs e)
        {
            int xDiff = StartPos.X - e.X;
            int yDiff = 0;
            //	StartPos=new Point (e.X,e.Y);
            ReSize(xDiff, yDiff);

            Action = SizeAction.None;
            DoResize();
            Refresh();
        }

        private void Center_MouseUp(object sender, MouseEventArgs e)
        {
            int xDiff = StartPos.X - e.X;
            int yDiff = StartPos.Y - e.Y;
            //	StartPos=new Point (e.X,e.Y);
            ReSize(xDiff, yDiff);

            Action = SizeAction.None;
            DoResize();
        }

        private void Horizontal_MouseMove(object sender, MouseEventArgs e)
        {
            if (Action == SizeAction.SizeH && e.Button == MouseButtons.Left)
            {
                Point start;
                int x = e.X;
                int y = e.Y;

                if (y + Horizontal.Top > Height - 4)
                    y = Height - 4 - Horizontal.Top;
                if (y + Horizontal.Top < 0)
                    y = 0 - Horizontal.Top;

                if (!FirstTime)
                {
                    start = PointToScreen(Location);
                    start.Y += PrevPos.Y + Horizontal.Location.Y;
                    ControlPaint.FillReversibleRectangle(new Rectangle(start.X, start.Y, Width, 3), Color.Black);
                }
                else
                    FirstTime = false;

                start = PointToScreen(Location);
                start.Y += y + Horizontal.Location.Y;
                ControlPaint.FillReversibleRectangle(new Rectangle(start.X, start.Y, Width, 3), Color.Black);

                PrevPos = new Point(x, y);
            }
        }

        private void Vertical_MouseMove(object sender, MouseEventArgs e)
        {
            if (Action == SizeAction.SizeV && e.Button == MouseButtons.Left)
            {
                Point start;
                int x = e.X;
                int y = e.Y;

                if (x + Vertical.Left > Width - 4)
                    x = Width - 4 - Vertical.Left;
                if (x + Vertical.Left < 0)
                    x = 0 - Vertical.Left;

                if (!FirstTime)
                {
                    start = PointToScreen(Location);
                    start.X += PrevPos.X + Vertical.Location.X;
                    ControlPaint.FillReversibleRectangle(new Rectangle(start.X, start.Y, 3, Height), Color.Black);
                }
                else
                    FirstTime = false;

                start = PointToScreen(Location);
                start.X += x + Vertical.Location.X;
                ControlPaint.FillReversibleRectangle(new Rectangle(start.X, start.Y, 3, Height), Color.Black);

                PrevPos = new Point(x, y);
            }
        }

        private void Center_MouseMove(object sender, MouseEventArgs e)
        {
            if (Action == SizeAction.SizeA && e.Button == MouseButtons.Left)
            {
                Point start;
                int x = e.X - Center.Width / 2;
                int y = e.Y - Center.Height / 2;

                // ROB: Added fix for graphics splatter when sizing both splitters.
                if (y + Horizontal.Top > Height - 4)
                    y = Height - 4 - Horizontal.Top;
                if (y + Horizontal.Top < 0)
                    y = 0 - Horizontal.Top;

                if (x + Vertical.Left > Width - 4)
                    x = Width - 4 - Vertical.Left;
                if (x + Vertical.Left < 0)
                    x = 0 - Vertical.Left;
                // END-ROB

                if (!FirstTime)
                {
                    start = PointToScreen(Location);
                    start.X += PrevPos.X + Vertical.Location.X;
                    ControlPaint.FillReversibleRectangle(new Rectangle(start.X, start.Y, 3, Height), Color.Black);

                    start = PointToScreen(Location);
                    start.Y += PrevPos.Y + Horizontal.Location.Y;
                    ControlPaint.FillReversibleRectangle(new Rectangle(start.X, start.Y, Width, 3),
                                                         SystemColors.ControlDark);
                }
                else
                    FirstTime = false;

                start = PointToScreen(Location);
                start.X += x + Vertical.Location.X;
                ControlPaint.FillReversibleRectangle(new Rectangle(start.X, start.Y, 3, Height), Color.Black);

                start = PointToScreen(Location);
                start.Y += y + Horizontal.Location.Y;
                ControlPaint.FillReversibleRectangle(new Rectangle(start.X, start.Y, Width, 3), SystemColors.ControlDark);

                PrevPos = new Point(x, y);
            }
        }

        private void ReSize(int x, int y)
        {
            //if (x==0 && y==0)
            //	return;

            SuspendLayout();

            int xx = Vertical.Left - x;
            int yy = Horizontal.Top - y;

            if (xx < 0)
                xx = 0;

            if (yy < 0)
                yy = 0;

            if (yy > Height - Horizontal.Height - SystemInformation.VerticalScrollBarWidth * 3)
                yy = Height - Horizontal.Height - SystemInformation.VerticalScrollBarWidth * 3;

            if (xx > Width - Vertical.Width - SystemInformation.VerticalScrollBarWidth * 3)
                xx = Width - Vertical.Width - SystemInformation.VerticalScrollBarWidth * 3;

            if (xx != Vertical.Left)
                Vertical.Left = xx;

            if (yy != Horizontal.Top)
                Horizontal.Top = yy;

            int CenterY = (Horizontal.Top + Horizontal.Height / 2) - Center.Height / 2;
            int CenterX = (Vertical.Left + Vertical.Width / 2) - Center.Width / 2;

            Center.Location = new Point(CenterX, CenterY);
            ResumeLayout();
            Invalidate();

            try
            {
                if (UpperLeft != null)
                    UpperLeft.Refresh();
                if (UpperLeft != null)
                    UpperLeft.Refresh();
                if (UpperLeft != null)
                    UpperLeft.Refresh();
                if (UpperLeft != null)
                    UpperLeft.Refresh();
            }
            catch { }
            OnResizing();
            //DoResize();
            //this.Refresh ();
        }

        private void ReSize2()
        {
            int xx = Vertical.Left;
            int yy = Horizontal.Top;

            if (xx < 0)
                xx = 0;

            if (yy < 0)
                yy = 0;

            if (yy > Height - Horizontal.Height - SystemInformation.VerticalScrollBarWidth * 3)
            {
                yy = Height - Horizontal.Height - SystemInformation.VerticalScrollBarWidth * 3;
                if (yy != Horizontal.Top)
                    Horizontal.Top = yy;
            }

            if (xx > Width - Vertical.Width - SystemInformation.VerticalScrollBarWidth * 3)
            {
                xx = Width - Vertical.Width - SystemInformation.VerticalScrollBarWidth * 3;
                if (xx != Vertical.Left)
                    Vertical.Left = xx;
            }

            int CenterY = (Horizontal.Top + Horizontal.Height / 2) - Center.Height / 2;
            int CenterX = (Vertical.Left + Vertical.Width / 2) - Center.Width / 2;

            Center.Location = new Point(CenterX, CenterY);
        }

        private void Center_DoubleClick(object sender, EventArgs e)
        {
            Horizontal.Top = -100;
            Vertical.Left = -100;
            DoResize();
        }

        private void Vertical_DoubleClick(object sender, EventArgs e)
        {
            Vertical.Left = -100;
            DoResize();
        }

        private void Horizontal_DoubleClick(object sender, EventArgs e)
        {
            Horizontal.Top = -100;
            DoResize();
        }

        /// <summary>
        /// Splits the view horiziontally.
        /// </summary>
        public void Split5050h()
        {
            Horizontal.Top = Height / 2;
            DoResize();
        }

        /// <summary>
        /// Splits teh view vertically.
        /// </summary>
        public void Split5050v()
        {
            Vertical.Left = Width / 2;
            DoResize();
        }

        public void ResetSplitview()
        {
            Vertical.Left = 0;
            Horizontal.Top = 0;
            DoResize();
        }

        /// <summary>
        /// Start dragging the horizontal splitter.
        /// </summary>
        public void InvokeMouseDownh()
        {
            IntPtr hwnd = Horizontal.Handle;
            NativeMethods.SendMessage(hwnd, (int)WindowMessage.WM_LBUTTONDOWN, 0, 0);
        }

        /// <summary>
        /// Start dragging the vertical splitter.
        /// </summary>
        public void InvokeMouseDownv()
        {
            IntPtr hwnd = Vertical.Handle;
            NativeMethods.SendMessage(hwnd, (int)WindowMessage.WM_LBUTTONDOWN, 0, 0);
        }
    }
}
