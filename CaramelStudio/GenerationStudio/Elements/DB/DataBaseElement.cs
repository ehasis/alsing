using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Forms;
using GenerationStudio.AppCore;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;
using MyMeta;

namespace GenerationStudio.Elements
{
    public enum ProviderType
    {
        SqlServer,
        OleDb,
        Odbc,
        Oracle,
    }

    [Serializable]
    [ElementParent(typeof (RootElement))]
    [ElementName("DataBase")]
    [ElementIcon("GenerationStudio.Images.database.gif")]
    public class DataBaseElement : NamedElement
    {
        private string connectionString;

        [OptionalField] private ProviderType providerType = ProviderType.SqlServer;

        public string ConnectionString
        {
            get { return connectionString; }

            set
            {
                connectionString = value;
                OnNotifyChange();
            }
        }

        public ProviderType ProviderType
        {
            get { return providerType; }
            set { providerType = value; }
        }

        [ElementVerb("Test connection")]
        public void TestConnection(IHost host)
        {
            var myMeta = new dbRoot();
            if (myMeta.Connect(dbDriver.SQL, ConnectionString))
            {
                MessageBox.Show("Connection was successful"); 
            }
            else
            {
                MessageBox.Show("Connection failed");
            }
        }

        [ElementVerb("Reload structure from DB")]
        public void SyncFromDataSourceToTableModel(IHost host)
        {
            var myMeta = new dbRoot();
            myMeta.Connect(dbDriver.SQL, ConnectionString);

            try
            {
                var tableElements = new Dictionary<string, TableElement>();
                foreach (TableElement child in AllChildren)
                {
                    string key = child.GetDisplayName();
                    tableElements.Add(key, child);
                }

                ClearChildren();

                Engine.MuteNotify();


                foreach (ITable metaTable in myMeta.Databases[0].Tables)
                {
                    string tableName = metaTable.Name;

                    TableElement table;
                    if (!tableElements.ContainsKey(tableName))
                    {
                        table = new TableElement {Name = tableName};
                        tableElements.Add(tableName, table);
                    }

                    table = tableElements[tableName];
                    AddChild(table);

                    table.Columns.ClearChildren();
                    foreach (IColumn metaColumn in metaTable.Columns)
                    {
                        var column = new ColumnElement
                                     {
                                         Name = metaColumn.Name,
                                         IsNullable = metaColumn.IsNullable,
                                         IsAutoKey = metaColumn.IsAutoKey,
                                         IsInPrimaryKey = metaColumn.IsInPrimaryKey,
                                         Default = metaColumn.Default,
                                         Ordinal = metaColumn.Ordinal,
                                         DbType = metaColumn.DataTypeName,
                                         MaxLength = metaColumn.NumericPrecision,
                                         AutoKeySeed = metaColumn.AutoKeySeed,
                                         AutoKeyIncrement = metaColumn.AutoKeyIncrement
                                     };
                        table.Columns.AddChild(column);
                    }

                    table.ForeignKeys.ClearChildren();
                    foreach(IForeignKey metaForeignKey in metaTable.ForeignKeys)
                    {
                        var key = new ForeignKeyElement
                                    {
                                        Name = metaForeignKey.Name                                        
                                    };
                        table.ForeignKeys.AddChild(key);
                    }
                }

                Engine.EnableNotify();
                Engine.OnNotifyChange();
            }
            catch
            {
                MessageBox.Show("Connection failed");
            }

            host.RefreshProjectTree();
        }


        [ElementVerb("Save structure to DB")]
        public void SyncTableModelToDataSource(IHost host) {}

        public override IList<ElementError> GetErrors()
        {
            var errors = new List<ElementError>();
            if (string.IsNullOrEmpty(ConnectionString))
                errors.Add(new ElementError(this,
                                            string.Format("DataBase {0} is missing connectionstring", GetDisplayName())));

            return errors;
        }
    }
}