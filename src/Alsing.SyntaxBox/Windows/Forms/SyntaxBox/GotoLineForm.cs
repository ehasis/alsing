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
using System.Globalization;
using System.Windows.Forms;

namespace Alsing.Windows.Forms.SyntaxBox
{
    public partial class GotoLineForm : Form
    {
        private readonly EditViewControl mOwner;

        public GotoLineForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a GotoLineForm that will be assigned to a specific Owner control.
        /// </summary>
        /// <param name="Owner">The SyntaxBox that will use the GotoLineForm</param>
        /// <param name="RowCount">The number of lines in the owner control</param>
        public GotoLineForm(EditViewControl Owner, int RowCount)
        {
            InitializeComponent();

            lblLines.Text = "Line number (1-" + RowCount.ToString(CultureInfo.InvariantCulture) + "):";
            mOwner = Owner;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                int row = int.Parse(txtRow.Text) - 1;
                mOwner.GotoLine(row);
            }
            catch { }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GotoLine_Closing(object sender, CancelEventArgs e)
        {
            //e.Cancel =true;
            //this.Hide ();
        }

        private void GotoLine_Activated(object sender, EventArgs e)
        {
            txtRow.Focus();
        }
    }
}
