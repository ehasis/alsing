using System;
using System.Windows.Forms;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;
using System.ComponentModel;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof (TablesElement))]
    [ElementName("Table")]
    [ElementIcon("GenerationStudio.Images.table.gif")]
    public class TableElement : NamedElement
    {
        public TableElement()
        {
            Columns = new ColumnsElement();
            AddChild(Columns);

            ForeignKeys = new ForeignKeysElement();
            AddChild(ForeignKeys);
        }

        [Browsable(false)]
        public ColumnsElement Columns { get; private set; }
        [Browsable(false)]
        public ForeignKeysElement ForeignKeys { get; private set; }


        [ElementVerb("View table data")]
        public void ViewTableContent(IHost host)
        {
            try
            {
                //var db = Parent as DataBaseElement;
                //IDbConnection connection = db.GetConnection();
                //IDbCommand command = connection.CreateCommand();
                //command.CommandText = string.Format("select * from [{0}]", Name);
                //connection.Open();
                //IDataReader reader = command.ExecuteReader();


                //var dt = new DataTable();
                //dt.Load(reader);

                //reader.Close();
                //connection.Close();
                //var editor = host.GetEditor<TableDataView>(this, "View Table");
                //editor.Data = dt;
                //host.ShowEditor(editor);
            }
            catch (Exception)
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