using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
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
                        var column = new ColumnElement();
                        column.Name = metaColumn.Name;
                        column.IsNullable = metaColumn.IsNullable;
                        column.IsAutoIncrement = metaColumn.IsAutoKey;
                        column.IsIdentity = metaColumn.IsInPrimaryKey;
                        column.DefaultValue = metaColumn.Default;
                        column.NativeType = null;// metaColumn.LanguageType;
                        column.IsUnique = false;
                        column.Ordinal = metaColumn.Ordinal;
                        column.DbType = metaColumn.DbTargetType;
                        column.MaxLength = metaColumn.NumericPrecision;
                        table.Columns.AddChild(column);
                    }
                }

                Engine.EnableNotify();
            }
            catch
            {
                MessageBox.Show("Connection failed");
            }

            host.RefreshProjectTree();
        }

        private DataTable GetSchema(IDbConnection connection)
        {
            if (connection is DbConnection)
            {
                return ((DbConnection) connection).GetSchema();
            }

            throw new NotSupportedException("Unknown provider");
        }

        private DataTable GetSchema(IDbConnection connection, string collectionName)
        {
            if (connection is DbConnection)
            {
                return ((DbConnection) connection).GetSchema(collectionName);
            }

            throw new NotSupportedException("Unknown provider");
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