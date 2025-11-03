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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Alsing.Windows.Forms.SyntaxBox
{
    /// <summary>
    /// Summary description for AutoListForm.
    /// </summary>
    [ToolboxItem(false)]
    public partial class AutoListForm : Form
    {
        /// <summary>
        /// The imagelist that should be used by the AutoListForm
        /// </summary>
        public ImageList Images;

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int message, int data, int id);

        /// <summary>
        /// Default AltoListControl constructor.
        /// </summary>
        public AutoListForm()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            //SetStyle(ControlStyles.ContainerControl  ,false);
            SetStyle(ControlStyles.Selectable, true);

            // TODO: Add any initialization after the InitForm call
        }

        /// <summary>
        /// Gets the "insert text" from the selected item.
        /// </summary>
        public string SelectedText
        {
            get
            {
                if (LB.SelectedItem == null)
                    return "";

                var li = (ListItem) LB.SelectedItem;
                return li.InsertText;
            }
        }

        public void SendKey(int KeyCode)
        {
            SendMessage(LB.Handle, (int) WindowMessage.WM_KEYDOWN, KeyCode, 0);
        }

        /// <summary>		
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(SystemColors.Control);
            ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, Border3DStyle.Raised);
        }

        public void SelectItem(string text)
        {
            text = text.ToLowerInvariant();

            for (int i = 0; i < LB.Items.Count; i++)
            {
                var li = (ListItem) LB.Items[i];
                string lis = li.Text.ToLowerInvariant();
                if (lis.StartsWith(text))
                {
                    LB.SelectedIndex = i;
                    break;
                }
            }
        }

        private void LB_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
            //	e.Handled =true;
        }

        private void LB_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
            //	e.Handled =true;
        }

        private void LB_KeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
            //	e.Handled =true;
        }

        /// <summary>
        /// For public use only.
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool IsInputKey(Keys keyData)
        {
            return true;
        }

        /// <summary>
        /// For public use only.
        /// </summary>
        /// <param name="charCode"></param>
        /// <returns></returns>
        protected override bool IsInputChar(char charCode)
        {
            return true;
        }

        /// <summary>
        /// Adds a new ListItem to the AutoListForm.
        /// </summary>
        /// <param name="text">Text of the new ListItem</param>
        /// <param name="ImageIndex">Image index that should be assigned to the new ListItem</param>
        /// <returns></returns>
        public ListItem Add(string text, int ImageIndex)
        {
            return Add(text, text, ImageIndex);
        }

        /// <summary>
        /// Adds a new ListItem to the AutoListForm.
        /// </summary>
        /// <param name="text">Text of the new ListItem</param>
        /// <param name="InsertText">text to insert when this item is selected</param>
        /// <param name="ImageIndex">Image index that should be assigned to the new ListItem</param>
        /// <returns></returns>
        public ListItem Add(string text, string InsertText, int ImageIndex)
        {
            var li = new ListItem(text, ImageIndex, "", InsertText);
            LB.Items.Add(li);


            //this.LB.Sorted =true;
            return li;
        }

        public ListItem Add(string text, string InsertText, string ToolTip, int ImageIndex)
        {
            var li = new ListItem(text, ImageIndex, "", InsertText);
            LB.Items.Add(li);
            li.ToolTip = ToolTip;
            //this.LB.Sorted =true;
            return li;
        }

        /// <summary>
        /// Clears the content of the AutoList.
        /// </summary>
        public void Clear()
        {
            LB.Items.Clear();
        }

        private void LB_DrawItem(object sender, DrawItemEventArgs e)
        {
            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            if (e.Index == - 1)
                return;

            const int Offset = 24;

            var li = (ListItem) LB.Items[e.Index];
            string text = li.Text;
            Brush bg, fg;

            if (selected)
            {
                bg = SystemBrushes.Highlight;
                fg = SystemBrushes.HighlightText;
                //fg=Brushes.Black;
            }
            else
            {
                bg = SystemBrushes.Window;
                fg = SystemBrushes.WindowText;
                //bg=Brushes.White;
                //fg=Brushes.Black;
            }

            if (!selected)
            {
                e.Graphics.FillRectangle(bg, 0, e.Bounds.Top, e.Bounds.Width, LB.ItemHeight);
                //e.Graphics.FillRectangle (SystemBrushes.Highlight,0,e.Bounds.Top,27 ,LB.ItemHeight); 
            }
            else
            {
                e.Graphics.FillRectangle(SystemBrushes.Window, Offset, e.Bounds.Top, e.Bounds.Width - Offset,
                                         LB.ItemHeight);
                e.Graphics.FillRectangle(SystemBrushes.Highlight,
                                         new Rectangle(Offset + 1, e.Bounds.Top + 1, e.Bounds.Width - Offset - 2,
                                                       LB.ItemHeight - 2));


                //e.Graphics.FillRectangle (SystemBrushes.Highlight,27,e.Bounds.Top,e.Bounds.Width-27 ,LB.ItemHeight); 
                //e.Graphics.FillRectangle (new SolidBrush(Color.FromArgb (182,189,210)),new Rectangle (1+27,e.Bounds.Top+1,e.Bounds.Width-2- ,LB.ItemHeight-2));


                ControlPaint.DrawFocusRectangle(e.Graphics,
                                                new Rectangle(Offset, e.Bounds.Top, e.Bounds.Width - Offset,
                                                              LB.ItemHeight));
            }


            e.Graphics.DrawString(text, e.Font, fg, Offset + 2, e.Bounds.Top + 1);


            if (Images != null)
                e.Graphics.DrawImage(Images.Images[li.Type], 6, e.Bounds.Top + 0);
        }

        private void LB_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(e);
        }

        public void BeginLoad()
        {
            LB.Sorted = false;
            LB.DrawMode = DrawMode.Normal;
            LB.SuspendLayout();
        }

        public void EndLoad()
        {
            LB.ResumeLayout();
            LB.Sorted = true;
            LB.DrawMode = DrawMode.OwnerDrawFixed;

            //set height
            Height = 0;
            if (LB.Items.Count > 10)
            {
                Height = LB.ItemHeight*11 + 12;
            }
            else
            {
                Height = LB.ItemHeight*(LB.Items.Count) + 12;
            }
            int max = 0;
            Graphics g = LB.CreateGraphics();
            foreach (ListItem li in LB.Items)
            {
                int w = (int) g.MeasureString(li.Text, LB.Font).Width + 45;
                if (w > max)
                    max = w;
            }
            Width = max + SystemInformation.VerticalScrollBarWidth;
            Refresh();
            g.Dispose();
        }


        private void AutoListForm_Resize(object sender, EventArgs e)
        {
            LB.Size = new Size(Width - 8, Height - 8);
        }

        private void LB_SelectedIndexChanged(object sender, EventArgs e)
        {
            var li = (ListItem) LB.SelectedItem;

            if (li.ToolTip != "")
            {
                tooltip.ShowAlways = true;
                tooltip.SetToolTip(LB, li.ToolTip);
                tooltip.InitialDelay = 2;
                tooltip.Active = true;
            }
        }

        private void LB_MouseDown(object sender, MouseEventArgs e)
        {
            SelectItem(e.X, e.Y);
        }

        private void SelectItem(int x, int y)
        {
            var p = new Point(x, y);
            int r = (p.Y/LB.ItemHeight) + LB.TopIndex;
            if (r != LB.SelectedIndex)
            {
                if (r < LB.Items.Count && r >= 0)
                {
                    LB.SelectedIndex = r;
                }
            }
        }

        private void LB_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != 0)
            {
                SelectItem(e.X, e.Y);
            }
        }
    }
}