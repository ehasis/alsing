using System;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace MyMeta.Firebird
{
    public class ClassFactory : IClassFactory
    {
        #region IClassFactory Members

        public ITables CreateTables()
        {
            return new FirebirdTables();
        }

        public ITable CreateTable()
        {
            return new FirebirdTable();
        }

        public IColumn CreateColumn()
        {
            return new FirebirdColumn();
        }

        public IColumns CreateColumns()
        {
            return new FirebirdColumns();
        }

        public IDatabase CreateDatabase()
        {
            return new FirebirdDatabase();
        }

        public IDatabases CreateDatabases()
        {
            return new FirebirdDatabases();
        }

        public IProcedure CreateProcedure()
        {
            return new FirebirdProcedure();
        }

        public IProcedures CreateProcedures()
        {
            return new FirebirdProcedures();
        }

        public IView CreateView()
        {
            return new FirebirdView();
        }

        public IViews CreateViews()
        {
            return new FirebirdViews();
        }

        public IParameter CreateParameter()
        {
            return new FirebirdParameter();
        }

        public IParameters CreateParameters()
        {
            return new FirebirdParameters();
        }

        public IForeignKey CreateForeignKey()
        {
            return new FirebirdForeignKey();
        }

        public IForeignKeys CreateForeignKeys()
        {
            return new FirebirdForeignKeys();
        }

        public IIndex CreateIndex()
        {
            return new FirebirdIndex();
        }

        public IIndexes CreateIndexes()
        {
            return new FirebirdIndexes();
        }

        public IResultColumn CreateResultColumn()
        {
            return new FirebirdResultColumn();
        }

        public IResultColumns CreateResultColumns()
        {
            return new FirebirdResultColumns();
        }

        public IDomain CreateDomain()
        {
            return new FirebirdDomain();
        }

        public IDomains CreateDomains()
        {
            return new FirebirdDomains();
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
            return new FbConnection();
        }

        #endregion

        public static void Register()
        {
            InternalDriver.Register("FIREBIRD",
                                    new FileDbDriver(typeof (ClassFactory), @"Database=", @"C:\firebird\EMPLOYEE.GDB",
                                                     @";User=SYSDBA;Password=wow;Dialect=3;Server=localhost",
                                                     "FirebirdDB (*.GDB)|*.GDB|all files (*.*|*.*"));
            InternalDriver.Register("INTERBASE",
                                    new FileDbDriver(typeof (ClassFactory), @"Database=", @"C:\firebird\EMPLOYEE.GDB",
                                                     @";User=SYSDBA;Password=wow;Server=localhost",
                                                     "InterbaseDB (*.GDB)|*.GDB|all files (*.*)|*.*"));
        }

        #region Nested type: MyInternalDriver

        internal class MyInternalDriver : InternalDriver
        {
            internal MyInternalDriver(Type factory, string connString, bool isOleDB)
                : base(factory, connString, isOleDB) {}

            public override string GetDataBaseName(IDbConnection con)
            {
                string result = GetDataBaseName(con);
                int index = result.LastIndexOf("\\");
                if (index >= 0)
                {
                    result = result.Substring(index + 1);
                }
                return result;
            }
        }

        #endregion
    }
}