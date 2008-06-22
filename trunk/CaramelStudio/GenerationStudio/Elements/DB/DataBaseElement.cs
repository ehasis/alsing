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

        public DataBaseElement()
        {
            Procedures = new ProceduresElement();
            AddChild(Procedures);

            Views = new ViewsElement();
            AddChild(Views);

            Tables = new TablesElement();
            AddChild(Tables);
        }

        [Browsable(false)]
        public ProceduresElement Procedures { get; private set; }
        [Browsable(false)]
        public ViewsElement Views { get; private set; }
        [Browsable(false)]
        public TablesElement Tables { get; private set; }

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
                var tablesTrans = Tables.BeginTransaction();
                foreach (ITable metaTable in myMeta.DefaultDatabase.Tables)
                {
                    var table = tablesTrans.GetNamedChild<TableElement>(metaTable.Name);
                    Tables.AddChild(table);

                    var columnsTrans = table.Columns.BeginTransaction();
                    foreach (IColumn metaColumn in metaTable.Columns)
                    {
                        var column = columnsTrans.GetNamedChild<ColumnElement>(metaColumn.Name);

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
                }

                //update foreign keys
                //must be done in a 2nd step since all columns are not available in the first loop
                foreach (ITable metaTable in myMeta.DefaultDatabase.Tables)
                {
                    var table = tablesTrans.GetNamedChild<TableElement>(metaTable.Name);
                    var foreignKeysTrans = table.ForeignKeys.BeginTransaction();
                    foreach (IForeignKey metaForeignKey in metaTable.ForeignKeys)
                    {
                        if (metaForeignKey.PrimaryTable.Name == metaTable.Name)
                            continue;

                        var foreignKey = foreignKeysTrans.GetNamedChild<ForeignKeyElement>(metaForeignKey.Name);
                        foreignKey.PrimaryTable = tablesTrans.GetNamedChild<TableElement>(metaForeignKey.PrimaryTable.Name);
                        table.ForeignKeys.AddChild(foreignKey);
                        var foreignKeyTrans = foreignKey.BeginTransaction();

                        for (int i = 0; i < metaForeignKey.ForeignColumns.Count; i++)
                        {
                            var columnPair = new ColumnPairElement();
                            columnPair.PrimaryColumn = foreignKey.PrimaryTable.Columns.GetNamedChild<ColumnElement>(((IColumn)metaForeignKey.PrimaryColumns[i]).Name);
                            columnPair.ForeignColumn = foreignKey.ForeignTable.Columns.GetNamedChild<ColumnElement>(((IColumn)metaForeignKey.ForeignColumns[i]).Name);

                            foreignKey.AddChild(columnPair);
                        }
                    }
                }

                var viewTrans = Views.BeginTransaction();
                foreach (IView metaView in myMeta.DefaultDatabase.Views)
                {
                    var view = viewTrans.GetNamedChild<ViewElement>(metaView.Name);

                    view.Name = metaView.Name;
                    Views.AddChild(view);
                }

                var procTrans = Procedures.BeginTransaction();
                foreach (IProcedure metaProcedure in myMeta.DefaultDatabase.Procedures)
                {
                    if (metaProcedure.Type == 3)
                        continue;

                    var procedure = procTrans.GetNamedChild<ProcedureElement>(metaProcedure.Name);

                    procedure.Name = metaProcedure.Name;
                    Procedures.AddChild(procedure);
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