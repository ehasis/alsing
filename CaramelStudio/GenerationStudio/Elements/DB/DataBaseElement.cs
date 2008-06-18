using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Windows.Forms;
using GenerationStudio.AppCore;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;

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
            IDbConnection connection = GetConnection();
            try
            {
                connection.Open();
                MessageBox.Show("Connection was successful");
            }
            catch
            {
                MessageBox.Show("Connection failed");
            }
        }

        public IDbConnection GetConnection()
        {
            if (ProviderType == ProviderType.SqlServer)
                return new SqlConnection(ConnectionString);

            if (ProviderType == ProviderType.OleDb)
                return new OleDbConnection(ConnectionString);

            if (ProviderType == ProviderType.Odbc)
                return new OdbcConnection(ConnectionString);

            if (ProviderType == ProviderType.Oracle)
                return new OracleConnection(ConnectionString);

            throw new NotSupportedException("Unknown provider");
        }

        [ElementVerb("Reload structure from DB")]
        public void SyncFromDataSourceToTableModel(IHost host)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    DataTable schema = GetSchema(connection);
                    DataTable tables = GetSchema(connection, "tables");
                    DataTable columns = GetSchema(connection, "columns");

                    var tableElements = new Dictionary<string, TableElement>();
                    foreach (TableElement child in AllChildren)
                    {
                        string key = child.GetDisplayName();
                        tableElements.Add(key, child);
                    }

                    ClearChildren();

                    Engine.MuteNotify();


                    foreach (DataRow row in tables.Rows)
                    {
                        string tableName = row["table_name"].ToString();

                        TableElement table = null;
                        if (!tableElements.ContainsKey(tableName))
                        {
                            table = new TableElement();
                            table.Name = tableName;
                            tableElements.Add(tableName, table);
                        }

                        table = tableElements[tableName];
                        AddChild(table);

                        IDbCommand command = connection.CreateCommand();
                        command.CommandText = string.Format("select * from [{0}]", tableName);
                        IDataReader reader = command.ExecuteReader();
                        DataTable tableSchema = reader.GetSchemaTable();
                        reader.Close();

                        table.Columns.ClearChildren();
                        foreach (DataRow columnRow in tableSchema.Rows)
                        {
                            var column = new ColumnElement();
                            column.Name = (string) columnRow["ColumnName"];
                            column.IsNullable = (bool) columnRow["AllowDBNull"];
                            column.IsAutoIncrement = (bool) columnRow["IsAutoIncrement"];
                            column.IsIdentity = (bool) columnRow["IsIdentity"];
                            column.DefaultValue = "";
                            column.NativeType = (Type) columnRow["DataType"];
                            column.IsUnique = (bool) columnRow["IsUnique"];
                            column.Ordinal = (int) columnRow["ColumnOrdinal"];
                            column.DbType = (string) columnRow["DataTypeName"];
                            column.MaxLength = (int) columnRow["ColumnSize"];
                            table.Columns.AddChild(column);
                        }
                    }

                    //foreach (DataRow row in columns.Rows)
                    //{
                    //    string tableName = row["table_name"].ToString();
                    //    string columnName = row["column_name"].ToString();

                    //    TableElement table = tableElements[tableName];
                    //    ColumnElement column = new ColumnElement();
                    //    column.Name = columnName;
                    //    column.IsNullable = false;
                    //    column.IsPK = false;
                    //    column.DefaultValue = "";


                    //    table.AddChild(column);

                    //}

                    Engine.EnableNotify();
                }
                catch
                {
                    MessageBox.Show("Connection failed");
                }

                host.RefreshProjectTree();
            }
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