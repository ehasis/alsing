using System;
using System.Data;
using System.Windows.Forms;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof (DataBaseElement))]
    [ElementName("Table")]
    [ElementIcon("GenerationStudio.Images.table.gif")]
    public class TableElement : NamedElement
    {
        public TableElement()
        {
            Columns = new ColumnsElement();
            AddChild(Columns);

            Keys = new KeysElement();
            AddChild(Keys);
        }

        public ColumnsElement Columns { get; set; }
        public KeysElement Keys { get; set; }


        [ElementVerb("View table data")]
        public void ViewTableContent(IHost host)
        {
            try
            {
                var db = Parent as DataBaseElement;
                IDbConnection connection = db.GetConnection();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = string.Format("select * from [{0}]", Name);
                connection.Open();
                IDataReader reader = command.ExecuteReader();


                var dt = new DataTable();
                dt.Load(reader);

                reader.Close();
                connection.Close();
                var editor = host.GetEditor<TableDataView>(this, "View Table");
                editor.Data = dt;
                host.ShowEditor(editor);
            }
            catch (Exception x)
            {
                MessageBox.Show("An error occured");
            }
        }

        public override bool GetDefaultExpanded()
        {
            return false;
        }

        [ElementVerb("Exclude / Include")]
        public void ExcludeInclude(IHost host)
        {
            Excluded = !Excluded;
        }
    }
}