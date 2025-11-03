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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Alsing.Design
{
    [ToolboxItem(false)]
    public partial class CollectionEditorGui : UserControl
    {
        public CollectionEditorGui()
        {
            InitializeComponent();
        }

        public void Bind()
        {
            var e = EditValue as ICollection;
            if (e == null)
            {
                MessageBox.Show("EditValue is null");
            }
            else
            {
                lstMembers.Items.Clear();
                foreach (object o in e)
                {
                    lstMembers.Items.Add(o);
                }

                EnableRemove();
                SelectObject();
            }
        }

        private void lstMembers_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                if (e.Index == -1)
                    return;

                int c = lstMembers.Items.Count;
                SizeF s = e.Graphics.MeasureString(c.ToString(), lstMembers.Font);
                var maxwidth = (int) s.Width;
                if (maxwidth < 16 + 2)
                    maxwidth = 16 + 2;

                var r = new Rectangle(0, e.Bounds.Top, maxwidth, lstMembers.ItemHeight);
                bool SupportPaint = Editor.GetPaintValueSupported();
                int w = SupportPaint ? 20 : 0;

                var rcItem = new Rectangle(r.Right + w, r.Top, e.Bounds.Width - r.Right - w, lstMembers.ItemHeight);

                ControlPaint.DrawBorder3D(e.Graphics, r, Border3DStyle.Raised, Border3DSide.All);
                StringFormat sf = StringFormat.GenericDefault;
                sf.Alignment = StringAlignment.Far;
                r.Inflate(-1, -1);
                e.Graphics.DrawString(e.Index.ToString(), lstMembers.Font, Brushes.Black, r, sf);


                bool Selected = ((int) e.State & (int) DrawItemState.Selected) != 0;

                using (SolidBrush bg = GetBgBrush(Selected))
                using (SolidBrush fg = GetFgBrush(Selected))
                {
                    e.Graphics.FillRectangle(bg, rcItem);
                    if (Selected && e.Index != -1)
                    {
                        if (((int) e.State & (int) DrawItemState.Focus) != 0)
                        {
                            ControlPaint.DrawFocusRectangle(e.Graphics, rcItem);
                        }
                    }

                    if (e.Index >= 0)
                    {
                        object o = lstMembers.Items[e.Index];
                        string name = GetDisplayText(o);
                        e.Graphics.DrawString(name, lstMembers.Font, fg, rcItem);
                    }
                }
            }
            catch {}
        }

        private SolidBrush GetFgBrush(bool Selected)
        {
            SolidBrush fg = Selected ? new SolidBrush(SystemColors.HighlightText) : new SolidBrush(lstMembers.ForeColor);
            return fg;
        }

        private SolidBrush GetBgBrush(bool Selected)
        {
            SolidBrush bg = Selected ? new SolidBrush(SystemColors.Highlight) : new SolidBrush(lstMembers.BackColor);
            return bg;
        }

        private void lstMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableRemove();
            SelectObject();
        }

        private void SelectObject()
        {
            pygProperties.SelectedObject = lstMembers.SelectedIndex >= 0 ? lstMembers.SelectedItem : null;
        }

        private void EnableRemove()
        {
            btnRemove.Enabled = lstMembers.SelectedIndices.Count > 0;
        }


        private static string GetDisplayText(object Item)
        {
            string ObjectName = null;

            if (Item == null)
            {
                return string.Empty;
            }
            PropertyDescriptor descriptor1 = TypeDescriptor.GetProperties(Item)["Name"];
            if (descriptor1 != null)
            {
                ObjectName = ((string) descriptor1.GetValue(Item));
                if (!string.IsNullOrEmpty(ObjectName))
                {
                    return ObjectName;
                }
            }

            if (string.IsNullOrEmpty(ObjectName))
            {
                ObjectName = Item.GetType().Name;
            }
            return ObjectName;
        }

        public void AddObject(object o)
        {
            Editor.AddObject(o);
        }

        public void RemoveObject(object o)
        {
            Editor.RemoveObject(o);
        }

        private void btnDown_Click(object sender, EventArgs e) {}

        private void btnUp_Click(object sender, EventArgs e) {}

        #region PUBLIC PROPERTY EDITOR

        public ComponaCollectionEditor Editor { get; set; }

        #endregion

        #region PUBLIC PROPERTY EDITORSERVICE

        public IWindowsFormsEditorService EditorService { get; set; }

        #endregion

        #region PUBLIC PROPERTY EDITVALUE

        public object EditValue { get; set; }

        #endregion
    }
}