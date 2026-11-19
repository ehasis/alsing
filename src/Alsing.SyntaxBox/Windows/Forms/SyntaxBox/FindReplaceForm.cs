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
using System.Windows.Forms;

namespace Alsing.Windows.Forms.SyntaxBox
{
    public partial class FindReplaceForm : Form
    {
        private WeakReference _Control;
        private string _Last = "";

        public FindReplaceForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a FindReplaceForm that will be assigned to a specific Owner control.
        /// </summary>
        /// <param name="Owner">The SyntaxBox that will use the FindReplaceForm</param>
        public FindReplaceForm(EditViewControl Owner)
        {
            InitializeComponent();

            mOwner = Owner;
        }

        private EditViewControl mOwner
        {
            get
            {
                if (_Control != null)
                    return (EditViewControl)_Control.Target;
                else
                    return null;
            }
            set { _Control = new WeakReference(value); }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //				unchecked
                //				{
                //					int i= (int)0x80000000;
                //					cp.Style |=i;
                //				}
                return cp;
            }
        }

        /// <summary>
        /// Displays the FindReplaceForm and sets it in "Find" mode.
        /// </summary>
        public void ShowFind()
        {
            pnlReplace.Visible = false;
            pnlReplaceButtons.Visible = false;
            Text = "Find";
            Show();
            ClientSize = new System.Drawing.Size(ClientSize.Width, 136);
            btnDoReplace.Visible = false;
            btnReplace.Visible = true;
            _Last = "";
            cboFind.Focus();
        }

        /// <summary>
        /// Displays the FindReplaceForm and sets it in "Replace" mode.
        /// </summary>
        public void ShowReplace()
        {
            pnlReplace.Visible = true;
            pnlReplaceButtons.Visible = true;
            Text = "Replace";
            Show();
            ClientSize = new System.Drawing.Size(ClientSize.Width, 176);
            btnDoReplace.Visible = true;
            btnReplace.Visible = false;
            _Last = "";
            cboFind.Focus();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            ShowReplace();
        }

        private void FindReplace_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            FindNext();
            cboFind.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            mOwner.Focus();
            Hide();
        }

        private void btnDoReplace_Click(object sender, EventArgs e)
        {
            mOwner.ReplaceSelection(cboReplace.Text);
            btnFind_Click(null, null);
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            string text = cboFind.Text;
            if (text == "")
                return;

            bool found = false;
            foreach (string s in cboFind.Items)
            {
                if (s == text)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                cboFind.Items.Add(text);

            int x = mOwner.Caret.Position.X;
            int y = mOwner.Caret.Position.Y;
            mOwner.Caret.Position.X = 0;
            mOwner.Caret.Position.Y = 0;
            while (mOwner.SelectNext(cboFind.Text, chkMatchCase.Checked, chkWholeWord.Checked, chkRegEx.Checked))
            {
                mOwner.ReplaceSelection(cboReplace.Text);
            }

            mOwner.Selection.ClearSelection();
            //	mOwner.Caret.Position.X=x;
            //	mOwner.Caret.Position.Y=y;
            //	mOwner.ScrollIntoView ();

            cboFind.Focus();
        }

        private void btnMarkAll_Click(object sender, EventArgs e)
        {
            string text = cboFind.Text;
            if (text == "")
                return;

            bool found = false;
            foreach (string s in cboFind.Items)
            {
                if (s == text)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                cboFind.Items.Add(text);

            int x = mOwner.Caret.Position.X;
            int y = mOwner.Caret.Position.Y;
            mOwner.Caret.Position.X = 0;
            mOwner.Caret.Position.Y = 0;
            while (mOwner.SelectNext(cboFind.Text, chkMatchCase.Checked, chkWholeWord.Checked, chkRegEx.Checked))
            {
                mOwner.Caret.CurrentRow.Bookmarked = true;
            }

            mOwner.Selection.ClearSelection();
            //	mOwner.Caret.Position.X=x;
            //	mOwner.Caret.Position.Y=y;
            //	mOwner.ScrollIntoView ();

            cboFind.Focus();
        }

        public void FindNext()
        {
            string text = cboFind.Text;

            if (_Last != "" && _Last != text)
            {
                mOwner.Caret.Position.X = 0;
                mOwner.Caret.Position.Y = 0;
                mOwner.ScrollIntoView();
            }

            _Last = text;

            if (text == "")
                return;

            bool found = false;
            foreach (string s in cboFind.Items)
            {
                if (s == text)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                cboFind.Items.Add(text);

            mOwner.SelectNext(cboFind.Text, chkMatchCase.Checked, chkWholeWord.Checked, chkRegEx.Checked);
        }
    }
}
