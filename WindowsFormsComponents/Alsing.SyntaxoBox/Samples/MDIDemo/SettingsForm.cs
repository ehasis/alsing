using System;
using System.Drawing;
using System.Windows.Forms;
using Alsing.SourceCode;
using Alsing.Windows.Forms;

namespace MDIDemo
{
    public partial class SettingsForm : Form
    {
        private readonly SyntaxBoxControl _sb;

        public SettingsForm(SyntaxBoxControl sb)
        {
            InitializeComponent();

            _sb = sb;

            TextStyle[] tss = sb.Document.Parser.SyntaxDefinition.Styles;

            lbStyles.Items.Clear();
            for (int i = 0; i < tss.Length; i++)
            {
                TextStyle ts = tss[i];
                var s = new Style { TextStyle = ts, Name = ts.Name ?? "Style " + i };
                lbStyles.Items.Add(s);
            }
            lbStyles.SelectedIndex = 0;
        }

        private void lbStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            pgStyles.SelectedObject = lbStyles.SelectedItem;
            var st = lbStyles.SelectedItem as Style;
            PreviewStyle(st);
        }

        private void pgStyles_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            var st = lbStyles.SelectedItem as Style;
            PreviewStyle(st);
            _sb.Refresh();
        }

        private void PreviewStyle(Style s)
        {
            lblPreview.ForeColor = s.ForeColor;
            lblPreview.BackColor = s.BackColor != Color.Transparent ? s.BackColor : _sb.BackColor;

            FontStyle fs = FontStyle.Regular;
            if (s.FontBold)
                fs |= FontStyle.Bold;
            if (s.FontItalic)
                fs |= FontStyle.Italic;
            if (s.FontUnderline)
                fs |= FontStyle.Underline;

            lblPreview.Font = new Font("Courier New", 11f, fs);
        }
    }
}
