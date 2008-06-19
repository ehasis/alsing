using System.Data;
using System.Data.OleDb;

namespace MyMeta.Pervasive
{
    public class ClassFactory : IClassFactory
    {
        #region IClassFactory Members

        public ITables CreateTables()
        {
            return new PervasiveTables();
        }

        public ITable CreateTable()
        {
            return new PervasiveTable();
        }

        public IColumn CreateColumn()
        {
            return new PervasiveColumn();
        }

        public IColumns CreateColumns()
        {
            return new PervasiveColumns();
        }

        public IDatabase CreateDatabase()
        {
            return new PervasiveDatabase();
        }

        public IDatabases CreateDatabases()
        {
            return new PervasiveDatabases();
        }

        public IProcedure CreateProcedure()
        {
            return new PervasiveProcedure();
        }

        public IProcedures CreateProcedures()
        {
            return new PervasiveProcedures();
        }

        public IView CreateView()
        {
            return new PervasiveView();
        }

        public IViews CreateViews()
        {
            return new PervasiveViews();
        }

        public IParameter CreateParameter()
        {
            return new PervasiveParameter();
        }

        public IParameters CreateParameters()
        {
            return new PervasiveParameters();
        }

        public IForeignKey CreateForeignKey()
        {
            return new PervasiveForeignKey();
        }

        public IForeignKeys CreateForeignKeys()
        {
            return new PervasiveForeignKeys();
        }

        public IIndex CreateIndex()
        {
            return new PervasiveIndex();
        }

        public IIndexes CreateIndexes()
        {
            return new PervasiveIndexes();
        }

        public IResultColumn CreateResultColumn()
        {
            return new PervasiveResultColumn();
        }

        public IResultColumns CreateResultColumns()
        {
            return new PervasiveResultColumns();
        }

        public IDomain CreateDomain()
        {
            return new PervasiveDomain();
        }

        public IDomains CreateDomains()
        {
            return new PervasiveDomains();
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
            InternalDriver.Register("PERVASIVE",
                                    new InternalDriver(typeof (ClassFactory),
                                                       "Provider=PervasiveOLEDB.8.60;Data Source=demodata;Location=Griffo;Persist Security Info=False",
                                                       true));
        }
    }
}