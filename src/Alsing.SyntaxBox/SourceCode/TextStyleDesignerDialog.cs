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

namespace Alsing.SourceCode
{
    public partial class TextStyleDesignerDialog : Form
    {
        private readonly TextStyle _Style;
        private readonly TextStyle _TmpStyle;

        public TextStyleDesignerDialog(TextStyle Style)
        {
            InitializeComponent();

            _Style = Style;
            _TmpStyle = (TextStyle)Style.Clone();
            pgStyles.SelectedObject = _TmpStyle;
            lblCaption.Text = _Style.ToString();
            PreviewStyle();
        }

        public static event EventHandler Change;

        protected static void OnChange()
        {
            if (Change != null)
                Change(null, EventArgs.Empty);
        }

        private void pgStyles_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            PreviewStyle();
        }

        private void PreviewStyle()
        {
            TextStyle s = _TmpStyle;

            lblPreview.ForeColor = s.ForeColor;
            if (s.BackColor != Color.Transparent)
                lblPreview.BackColor = s.BackColor;
            else
                lblPreview.BackColor = Color.White;

            FontStyle fs = FontStyle.Regular;
            if (s.Bold)
                fs |= FontStyle.Bold;
            if (s.Italic)
                fs |= FontStyle.Italic;
            if (s.Underline)
                fs |= FontStyle.Underline;

            lblPreview.Font = new Font("Courier New", 11f, fs);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _Style.BackColor = _TmpStyle.BackColor;
            _Style.ForeColor = _TmpStyle.ForeColor;
            _Style.Bold = _TmpStyle.Bold;
            _Style.Italic = _TmpStyle.Italic;
            _Style.Underline = _TmpStyle.Underline;
            OnChange();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
