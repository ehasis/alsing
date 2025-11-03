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
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Alsing.Windows.Forms
{
    /// <summary>
    /// Summary description for InfoTip.
    /// </summary>
    public partial class InfoTipForm : Form
    {
        private WeakReference _Control;
        private int _Count = 1;
        private int _SelectedIndex;

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int message, int _data, int _id);

        public event EventHandler SelectedIndexChanged = null;

        public InfoTipForm()
        {
            InitializeComponent();
        }

        public InfoTipForm(Control parent)
        {
            ParentControl = parent;
            if (CreateParams != null) CreateParams.ClassName = "tooltips_class32";

            InitializeComponent();
        }

        private Control ParentControl
        {
            get { return _Control != null ? (Control)_Control.Target : null; }
            set { _Control = new WeakReference(value); }
        }

        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (value > _Count)
                    value = 1;
                if (value < 1)
                    value = _Count;

                _SelectedIndex = value;
                OnSelectedIndexChanged();
                SetPos();
            }
        }

        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

        public Image Image
        {
            get { return picIcon.Image; }
            set
            {
                picIcon.Image = value;
                if (value == null)
                {
                    pnlImage.Visible = false;
                }
                else
                {
                    pnlImage.Visible = true;
                    pnlImage.Width = Image.Width + 6;
                    picIcon.Size = Image.Size;
                }
                DoResize();
            }
        }

        public string Data
        {
            get { return InfoText.Text; }
            set { InfoText.Text = value; }
        }

        private void InfoText_Resize(object sender, EventArgs e)
        {
            DoResize();
        }

        private void DoResize()
        {
            int w = InfoText.Left + InfoText.Width + 8;
            if (Count > 1)
            {
                w += pnlSelect.Width;
            }
            if (picIcon.Image != null)
            {
                w += pnlImage.Width;
            }

            int h = InfoText.Top + InfoText.Height + 6;
            if (Image != null && Image.Height + picIcon.Top * 2 > h)
                h = Image.Height + picIcon.Top * 2;

            ClientSize = new Size(w, h);
        }

        public void Init()
        {
            SelectedIndex = 1;
            SetPos();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            SelectedIndex++;
            SetPos();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            SelectedIndex--;
            SetPos();
        }

        private void btnPrev_DoubleClick(object sender, EventArgs e)
        {
            SelectedIndex--;
            SetPos();
        }

        private void btnNext_DoubleClick(object sender, EventArgs e)
        {
            SelectedIndex++;
            SetPos();
        }

        private void SetPos()
        {
            if (Count == 1)
            {
                pnlSelect.Visible = false;
            }
            else
            {
                pnlSelect.Visible = true;
            }
            DoResize();

            lblIndex.Text = SelectedIndex.ToString((CultureInfo.InvariantCulture)) + " of " +
                            Count.ToString(CultureInfo.InvariantCulture);

            if (ParentControl != null)
                ParentControl.Focus();
        }

        private void InfoTipForm_Enter(object sender, EventArgs e)
        {
            ParentControl.Focus();
        }

        private void InfoText_Enter(object sender, EventArgs e)
        {
            ParentControl.Focus();
        }

        private void OnSelectedIndexChanged()
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, null);
        }
    }
}
