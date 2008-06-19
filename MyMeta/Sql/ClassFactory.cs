using System.Data;
using System.Data.OleDb;

namespace MyMeta.Sql
{
    public class ClassFactory : IClassFactory
    {
        #region IClassFactory Members

        public ITables CreateTables()
        {
            return new SqlTables();
        }

        public ITable CreateTable()
        {
            return new SqlTable();
        }

        public IColumn CreateColumn()
        {
            return new SqlColumn();
        }

        public IColumns CreateColumns()
        {
            return new SqlColumns();
        }

        public IDatabase CreateDatabase()
        {
            return new SqlDatabase();
        }

        public IDatabases CreateDatabases()
        {
            return new SqlDatabases();
        }

        public IProcedure CreateProcedure()
        {
            return new SqlProcedure();
        }

        public IProcedures CreateProcedures()
        {
            return new SqlProcedures();
        }

        public IView CreateView()
        {
            return new SqlView();
        }

        public IViews CreateViews()
        {
            return new SqlViews();
        }

        public IParameter CreateParameter()
        {
            return new SqlParameter();
        }

        public IParameters CreateParameters()
        {
            return new SqlParameters();
        }

        public IForeignKey CreateForeignKey()
        {
            return new SqlForeignKey();
        }

        public IForeignKeys CreateForeignKeys()
        {
            return new SqlForeignKeys();
        }

        public IIndex CreateIndex()
        {
            return new SqlIndex();
        }

        public IIndexes CreateIndexes()
        {
            return new SqlIndexes();
        }

        public IResultColumn CreateResultColumn()
        {
            return new SqlResultColumn();
        }

        public IResultColumns CreateResultColumns()
        {
            return new SqlResultColumns();
        }

        public IDomain CreateDomain()
        {
            return new SqlDomain();
        }

        public IDomains CreateDomains()
        {
            return new SqlDomains();
        }

        public IProviderType CreateProviderType()
        {
            return new ProviderType();
        }

        public IProviderTypes CreateProviderTypes()
        {
            return new ProviderTypes();
        }

        public IDbConnection CreateConnection()
        {
            return new OleDbConnection();
        }

        #endregion

        public static void Register()
        {
            var drv = new InternalDriver(typeof (ClassFactory),
                                         "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=sa;Initial Catalog=Northwind;Data Source=localhost",
                                         true) {RequiredDatabaseName = true};

            InternalDriver.Register("SQL", drv);
        }
    }
}