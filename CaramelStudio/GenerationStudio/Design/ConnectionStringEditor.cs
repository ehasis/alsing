using System;
using System.ComponentModel;
using System.Drawing.Design;
using ADODB;
using MSDASC;

namespace GenerationStudio.Design
{
    public class ConnectionStringUITypeEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            return EditValue(value as string);
        }

        public string EditValue()
        {
            return EditValue(string.Empty);
        }


        public string EditValue(string value)
        {
            DataLinks dataLinks = new DataLinksClass();

            var con = (Connection)dataLinks.PromptNew();
            if (con == null)
                return value;
            return con.ConnectionString;
        }
    }
}