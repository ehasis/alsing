using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.Serialization;
using System.Windows.Forms;
using GenerationStudio.AppCore;
using GenerationStudio.Attributes;
using GenerationStudio.Design;
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

        [DefaultValue(""), Editor(typeof(ConnectionStringUITypeEditor), typeof(UITypeEditor)), Category("Data"),
         Description("The alternate connection string.")]
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
                Engine.MuteNotify();
                BeginUpdateChildren();

                foreach (ITable metaTable in myMeta.DefaultDatabase.Tables)
                {
                    var table = GetNamedChild<TableElement>(metaTable.Name);
                    AddChild(table);

                    table.Columns.BeginUpdateChildren();
                    foreach (IColumn metaColumn in metaTable.Columns)
                    {
                        var column = table.Columns.GetNamedChild<ColumnElement>(metaColumn.Name);

                        column.Name = metaColumn.Name;
                        column.IsNullable = metaColumn.IsNullable;
                        column.IsAutoKey = metaColumn.IsAutoKey;
                        column.IsInPrimaryKey = metaColumn.IsInPrimaryKey;
                        column.Default = metaColumn.Default;
                        column.Ordinal = metaColumn.Ordinal;
                        column.DbType = metaColumn.DataTypeName;
                        column.MaxLength = metaColumn.NumericPrecision;
                        column.AutoKeySeed = metaColumn.AutoKeySeed;
                        column.AutoKeyIncrement = metaColumn.AutoKeyIncrement;

                        table.Columns.AddChild(column);
                    }

                    table.ForeignKeys.BeginUpdateChildren();
                    foreach (IForeignKey metaForeignKey in metaTable.ForeignKeys)
                    {
                        var foreignKey = table.ForeignKeys.GetNamedChild<ForeignKeyElement>(metaForeignKey.Name);
                        foreignKey.ForeignTable = GetNamedChild<TableElement>(metaForeignKey.ForeignTable.Name);
                        table.ForeignKeys.AddChild(foreignKey); 
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