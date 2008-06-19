// #define IGNORE_VISTA // if defined in csproj compile without VISTADB
// #define ENTERPRISE // if defined in csproj create com component

// #define PLUGINS_FROM_SUBDIRS  // if defined Plugins can also live in subdirectories of the MyMeta-bin-dir
/*
 * PLUGINS_FROM_SUBDIRS disabled because k3b found no way to use dll-s in scrips
 *  tried <%#REFERENCE subdir\myDll.dll  %> ==> script compiler error not found
 *  csc.exe /lib:plugins  ==> script compiler error not found
 */


using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using FirebirdSql.Data.FirebirdClient;
using MyMeta.VistaDB;
using MySql.Data.MySqlClient;
using Npgsql;
using ClassFactory=MyMeta.Sql.ClassFactory;

namespace MyMeta
{


    /// <summary>
    /// MyMeta is the root of the MyMeta meta-data. MyMeta is an intrinsic object available to your script and configured based on the settings
    /// you have entered in the Default Settings dialog. It is already connected before you script execution begins.
    /// </summary>
    /// <remarks>
    ///	MyMeta has 1 Collection:
    /// <list type="table">
    ///		<item><term>Databases</term><description>Contains a collection of all of the databases in your system</description></item>
    ///	</list>
    /// There is a property collection on every entity in your database, you can add key/value
    /// pairs to the User Meta Data either through the user interface of MyGeneration or 
    /// programmatically in your scripts.  User meta data is stored in XML and never writes to your database.
    ///
    /// This can be very useful, you might need more meta data than MyMeta supplies, in fact,
    /// MyMeta will eventually offer extended meta data using this feature as well. The current plan
    /// is that any extended data added via MyGeneration will have a key that beings with "MyMeta.Something"
    /// where 'Something' equals the description. 
    /// </remarks>
    /// <example>
    ///	VBScript - ****** NOTE ****** You never have to actually write this code, this is for education purposes only.
    ///	<code>
    ///	MyMeta.Connect "SQL", "Provider=SQLOLEDB.1;Persist Security Info=True;User ID=sa;Data Source=localhost"
    ///	
    ///	MyMeta.DbTarget	= "SqlClient"
    ///	MyMeta.DbTargetMappingFileName = "C:\Program Files\MyGeneration\Settings\DbTargets.xml"
    ///	
    /// MyMeta.Language = "VB.NET"
    /// MyMeta.LanguageMappingFileName = "C:\Program Files\MyGeneration\Settings\Languages.xml"
    /// 
    /// MyMeta.UserMetaDataFileName = "C:\Program Files\MyGeneration\Settings\UserMetaData.xml"
    /// </code>
    ///	JScript - ****** NOTE ****** You never have to actually write this code, this is for education purposes only.
    ///	<code>
    ///	MyMeta.Connect("SQL", "Provider=SQLOLEDB.1;Persist Security Info=True;User ID=sa;Data Source=localhost")
    ///	
    ///	MyMeta.DbTarget	= "SqlClient";
    ///	MyMeta.DbTargetMappingFileName = "C:\Program Files\MyGeneration\Settings\DbTargets.xml";
    ///	
    /// MyMeta.Language = "VB.NET";
    /// MyMeta.LanguageMappingFileName = "C:\Program Files\MyGeneration\Settings\Languages.xml";
    /// 
    /// MyMeta.UserMetaDataFileName = "C:\Program Files\MyGeneration\Settings\UserMetaData.xml";
    /// </code>
    /// The above code is done for you long before you execute your script and the values come from the Default Settings Dialog.
    /// However, you can override these defaults as many of the sample scripts do. For instance, if you have a script that is for SqlClient
    /// only go ahead and set the MyMeta.DbTarget in your script thus overriding the Default Settings.
    /// </example>
	public class dbRoot
    {
        private static Hashtable plugins;
        private string _connectionString = "";
        private Databases _databases;
        private string _defaultDatabase = "";
        private bool _domainOverride = true;

        private dbDriver _driver = dbDriver.None;
        private string _driverString = "NONE";
        private bool _isConnected;
        private Hashtable _parsedConnectionString;
        private ProviderTypes _providerTypes;
        private bool _showSystemData;

        private OleDbConnection _theConnection = new OleDbConnection();
        private XmlNode _xmlNode;
        public IClassFactory ClassFactory;
        public bool IgnoreCase = true;
        public bool requiredDatabaseName;
        public bool requiresSchemaName;
        public bool StripTrailingNulls;

        public string TrailingNull;
        public XmlDocument UserData = new XmlDocument();

        public dbRoot()
        {
            Access.ClassFactory.Register();
            Advantage.ClassFactory.Register();
            DB2.ClassFactory.Register();
            Firebird.ClassFactory.Register();
            ISeries.ClassFactory.Register();
            MySql.ClassFactory.Register();
            MySql5.ClassFactory.Register();
            Oracle.ClassFactory.Register();
            Pervasive.ClassFactory.Register();
            PostgreSQL.ClassFactory.Register();
            PostgreSQL8.ClassFactory.Register();
            Sql.ClassFactory.Register();
            SQLite.ClassFactory.Register();
#if !IGNORE_VISTA
            VistaDB.ClassFactory.Register();
#endif
            Reset();
        }


        private void Reset()
        {
            UserData = null;

            IgnoreCase = true;
            requiredDatabaseName = false;
            requiresSchemaName = false;
            StripTrailingNulls = false;
            TrailingNull = ((char) 0x0).ToString();

            ClassFactory = null;

            _showSystemData = false;

            _driver = dbDriver.None;
            _driverString = "NONE";
            _databases = null;
            _connectionString = "";
            _theConnection = new OleDbConnection();
            _isConnected = false;
            _parsedConnectionString = null;
            _defaultDatabase = "";

            // Language
            _languageMappingFileName = string.Empty;
            _language = string.Empty;
            _languageDoc = null;
            LanguageNode = null;

            UserData = new XmlDocument();
            UserData.AppendChild(UserData.CreateNode(XmlNodeType.Element, "MyMeta", null));

            // DbTarget
            _dbTargetMappingFileName = string.Empty;
            _dbTarget = string.Empty;
            _dbTargetDoc = null;
            DbTargetNode = null;
        }
        #region XML User Data

        private string _userMetaDataFileName = "";

        public string UserDataXPath
        {
            get { return @"//MyMeta"; }
        }

        /// <summary>
        /// The full path of the XML file that contains the user defined meta data. See IPropertyCollection
        /// </summary>
        public string UserMetaDataFileName
        {
            get { return _userMetaDataFileName; }
            set
            {
                _userMetaDataFileName = value;

                try
                {
                    UserData = new XmlDocument();
                    UserData.Load(_userMetaDataFileName);
                }
                catch
                {
                    UserData = new XmlDocument();
                }
            }
        }

        internal bool GetXmlNode(out XmlNode node, bool forceCreate)
        {
            node = null;
            bool success = false;

            if (null == _xmlNode)
            {
                if (!UserData.HasChildNodes)
                {
                    _xmlNode = UserData.CreateNode(XmlNodeType.Element, "MyMeta", null);
                    UserData.AppendChild(_xmlNode);
                }
                else
                {
                    _xmlNode = UserData.SelectSingleNode("./MyMeta");
                }
            }

            if (null != _xmlNode)
            {
                node = _xmlNode;
                success = true;
            }

            return success;
        }

        /// <summary>
        /// Call this method to save any user defined meta data that you may have modified. See <see cref="UserMetaDataFileName"/>
        /// </summary>
        /// <returns>True if saved, False if not</returns>
        public bool SaveUserMetaData()
        {
            if (null != UserData && string.Empty != _userMetaDataFileName)
            {
                var f = new FileInfo(_userMetaDataFileName);
                if (!f.Exists)
                {
                    if (!f.Directory.Exists) f.Directory.Create();
                }

                UserData.Save(_userMetaDataFileName);
                return true;
            }

            return false;
        }

        #endregion

        #region XML Language Mapping

        private string _language = string.Empty;
        private XmlDocument _languageDoc;
        private string _languageMappingFileName = string.Empty;
        internal XmlNode LanguageNode;

        /// <summary>
        /// The full path of the XML file that contains the language mappings. The data in this file plus the value you provide 
        /// to <see cref="Language"/> determine the value of IColumn.Language.
        /// </summary>
        public string LanguageMappingFileName
        {
            get { return _languageMappingFileName; }
            set
            {
                try
                {
                    _languageMappingFileName = value;

                    _languageDoc = new XmlDocument();
                    _languageDoc.Load(_languageMappingFileName);
                    _language = string.Empty;
                    ;
                    LanguageNode = null;
                }
                catch {}
            }
        }

        /// <summary>
        /// Use this to choose your Language, for example, "C#". See <see cref="LanguageMappingFileName"/> for more information
        /// </summary>
        public string Language
        {
            get { return _language; }

            set
            {
                if (null != _languageDoc)
                {
                    _language = value;
                    string xPath = @"//Languages/Language[@From='" + _driverString + "' and @To='" + _language + "']";
                    LanguageNode = _languageDoc.SelectSingleNode(xPath, null);
                }
            }
        }

        /// <summary>
        /// Returns all of the languages currently configured for the DBMS set when Connect was called.
        /// </summary>
        /// <returns>An array with all of the possible languages.</returns>
        public string[] GetLanguageMappings()
        {
            return GetLanguageMappings(_driverString);
        }

        /// <summary>
        /// Returns all of the languages for a given driver, regardless of MyMeta's current connection
        /// </summary>
        /// <returns>An array with all of the possible languages.</returns>
        public string[] GetLanguageMappings(string driverString)
        {
            string[] mappings = null;

            if ((null != _languageDoc) && (driverString != null))
            {
                driverString = driverString.ToUpper();
                string xPath = @"//Languages/Language[@From='" + driverString + "']";
                XmlNodeList nodes = _languageDoc.SelectNodes(xPath, null);

                if ((null != nodes) && (nodes.Count > 0))
                {
                    int nodeCount = nodes.Count;
                    mappings = new string[nodeCount];

                    for (int i = 0; i < nodeCount; i++)
                    {
                        mappings[i] = nodes[i].Attributes["To"].Value;
                    }
                }
            }

            return mappings;
        }

        #endregion

        #region XML DbTarget Mapping

        private string _dbTarget = string.Empty;
        private XmlDocument _dbTargetDoc;
        private string _dbTargetMappingFileName = string.Empty;
        internal XmlNode DbTargetNode;

        /// <summary>
        /// The full path of the XML file that contains the DbTarget mappings. The data in this file plus the value you provide 
        /// to <see cref="DbTarget"/> determine the value of IColumn.DbTarget.
        /// </summary>
        public string DbTargetMappingFileName
        {
            get { return _dbTargetMappingFileName; }
            set
            {
                try
                {
                    _dbTargetMappingFileName = value;

                    _dbTargetDoc = new XmlDocument();
                    _dbTargetDoc.Load(_dbTargetMappingFileName);
                    _dbTarget = string.Empty;
                    ;
                    DbTargetNode = null;
                }
                catch {}
            }
        }

        /// <summary>
        /// Use this to choose your DbTarget, for example, "SqlClient". See <see cref="DbTargetMappingFileName"/>  for more information
        /// </summary>
        public string DbTarget
        {
            get { return _dbTarget; }

            set
            {
                if (null != _dbTargetDoc)
                {
                    _dbTarget = value;
                    string xPath = @"//DbTargets/DbTarget[@From='" + _driverString + "' and @To='" + _dbTarget + "']";
                    DbTargetNode = _dbTargetDoc.SelectSingleNode(xPath, null);
                }
            }
        }

        /// <summary>
        /// Returns all of the dbTargets currently configured for the DBMS set when Connect was called.
        /// </summary>
        /// <returns>An array with all of the possible dbTargets.</returns>
        public string[] GetDbTargetMappings()
        {
            return GetDbTargetMappings(_driverString);
        }

        /// <summary>
        /// Returns all of the dbTargets for a given driver, regardless of MyMeta's current connection
        /// </summary>
        /// <returns>An array with all of the possible dbTargets.</returns>
        public string[] GetDbTargetMappings(string driverString)
        {
            string[] mappings = null;

            if ((null != _dbTargetDoc) && (driverString != null))
            {
                driverString = driverString.ToUpper();
                string xPath = @"//DbTargets/DbTarget[@From='" + driverString + "']";
                XmlNodeList nodes = _dbTargetDoc.SelectNodes(xPath, null);

                if (null != nodes && nodes.Count > 0)
                {
                    int nodeCount = nodes.Count;
                    mappings = new string[nodeCount];

                    for (int i = 0; i < nodeCount; i++)
                    {
                        mappings[i] = nodes[i].Attributes["To"].Value;
                    }
                }
            }

            return mappings;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Contains all of the databases in your DBMS system.
        /// </summary>
        public IDatabases Databases
        {
            get
            {
                if (null == _databases)
                {
                    if (ClassFactory != null)
                    {
                        _databases = (Databases) ClassFactory.CreateDatabases();
                        _databases.dbRoot = this;
                        _databases.LoadAll();
                    }
                }

                return _databases;
            }
        }

        /// <summary>
        /// This is the default database as defined in your connection string, or if not provided your DBMS system may provide one.
        /// Finally, for single database systems like Microsoft Access it will be the default database.
        /// </summary>
        public IDatabase DefaultDatabase
        {
            get
            {
                IDatabase defDatabase = null;
                try
                {
                    var dbases = Databases as Databases;

                    if (_defaultDatabase != null && _defaultDatabase != "")
                        defDatabase = dbases.GetByPhysicalName(_defaultDatabase);
                    else
                    {
                        if (dbases.Count == 1)
                        {
                            defDatabase = dbases[0];
                        }
                    }
                }
                catch {}

                return defDatabase;
            }
        }

        public IProviderTypes ProviderTypes
        {
            get
            {
                if (null == _providerTypes)
                {
                    _providerTypes = (ProviderTypes) ClassFactory.CreateProviderTypes();
                    _providerTypes.dbRoot = this;
                    _providerTypes.LoadAll();
                }

                return _providerTypes;
            }
        }

        #endregion

        #region Connection 

        internal OleDbConnection TheConnection
        {
            get
            {
                if (_theConnection.State != ConnectionState.Open)
                {
                    _theConnection.ConnectionString = _connectionString;
                    _theConnection.Open();
                }

                return _theConnection;
            }
        }

        /// <summary>
        /// True if MyMeta has been successfully connected to your DBMS, False if not.
        /// </summary>
        public bool IsConnected
        {
            get { return _isConnected; }
        }

        /// <summary>
        /// Returns MyMeta's current dbDriver enumeration value as defined by its current connection.
        /// </summary>
        public dbDriver Driver
        {
            get { return _driver; }
        }

        /// <summary>
        /// Returns MyMeta's current DriverString as defined by its current connection.
        /// </summary>
        /// <remarks>
        /// These are the current possible values.
        /// <list type="table">
        ///		<item><term>"ACCESS"</term><description>Microsoft Access 97 and higher</description></item>
        ///		<item><term>"DB2"</term><description>IBM DB2</description></item>	
        ///		<item><term>"MYSQL"</term><description>Currently limited to only MySQL running on Microsoft Operating Systems</description></item>
        ///		<item><term>"ORACLE"</term><description>Oracle 8i - 9</description></item>
        ///		<item><term>"SQL"</term><description>Microsoft SQL Server 2000 and higher</description></item>	
        ///		<item><term>"PostgreSQL"</term><description>PostgreSQL</description></item>	///		
        ///	</list>
        ///	</remarks>
        public string DriverString
        {
            get { return _driverString; }
        }

        /// <summary>
        /// Returns the current connection string. ** WARNING ** Currently the password is returned, the password will be stripped from this
        /// property in the very near future.
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
        }

        internal Hashtable ParsedConnectionString
        {
            get
            {
                //Provider=SQLOLEDB.1;Persist Security Info=False;User ID=sa;Initial Catalog=Northwind;Data Source=localhost
                if (null == _parsedConnectionString)
                {
                    string[] first = ConnectionString.Split(new[] {';'});

                    _parsedConnectionString = new Hashtable(first.GetUpperBound(0));

                    string[] kv = null;

                    for (int i = 0; i < first.GetUpperBound(0); i++)
                    {
                        kv = first[i].Split(new[] {'='});

                        if (1 == kv.GetUpperBound(0))
                        {
                            _parsedConnectionString.Add(kv[0].ToUpper(), kv[1]);
                        }
                        else
                        {
                            _parsedConnectionString.Add(kv[0].ToUpper(), "");
                        }
                    }
                }

                return _parsedConnectionString;
            }
        }

        [ComVisible(false)]
        public IDbConnection BuildConnection(string driver, string connectionString)
        {
            IDbConnection conn = null;
            switch (driver.ToUpper())
            {
                case MyMetaDrivers.MySql2:
                    conn = new MySqlConnection(connectionString);
                    break;

                case MyMetaDrivers.PostgreSQL:
                case MyMetaDrivers.PostgreSQL8:
                    conn = new NpgsqlConnection(connectionString);
                    break;

                case MyMetaDrivers.Firebird:
                case MyMetaDrivers.Interbase:
                    conn = new FbConnection(connectionString);
                    break;

                case MyMetaDrivers.SQLite:
                    conn = new SQLiteConnection(connectionString);
                    break;
#if !IGNORE_VISTA
                case MyMetaDrivers.VistaDB:
                    try
                    {
                        var mh = new MetaHelper();
                        conn = mh.GetConnection(connectionString);
                    }
                    catch
                    {
                        throw new Exception("Invalid VistaDB connection or VistaDB not installed");
                    }
                    break;
#endif
                default:
                    break;
            }
            return conn;
        }

        /// <summary>
        /// This is how you connect to your DBMS system using MyMeta. This is already called for you before your script beings execution.
        /// </summary>
        /// <param name="driver">A string as defined in the remarks below</param>
        /// <param name="driverIn"></param>
        /// <param name="connectionString">A valid connection string for you DBMS</param>
        /// <returns>True if connected, False if not</returns>
        /// <remarks>
        /// These are the supported "drivers".
        /// <list type="table">
        ///		<item><term>"ACCESS"</term><description>Microsoft Access 97 and higher</description></item>
        ///		<item><term>"DB2"</term><description>IBM DB2</description></item>	
        ///		<item><term>"MYSQL"</term><description>Currently limited to only MySQL running on Microsoft Operating Systems</description></item>
        ///		<item><term>"MYSQL2"</term><description>Uses MySQL Connector/Net, Supports 4.x schema info on Windows or Linux</description></item>
        ///		<item><term>"ORACLE"</term><description>Oracle 8i - 9</description></item>
        ///		<item><term>"SQL"</term><description>Microsoft SQL Server 2000 and higher</description></item>	
        ///		<item><term>"PERVASIVE"</term><description>Pervasive 9.00+ (might work on lower but untested)</description></item>		
        ///		<item><term>"POSTGRESQL"</term><description>PostgreSQL 7.3+ (might work on lower but untested)</description></item>		
        ///		<item><term>"POSTGRESQL8"</term><description>PostgreSQL 8.0+</description></item>	
        ///		<item><term>"FIREBIRD"</term><description>Firebird</description></item>		
        ///		<item><term>"INTERBASE"</term><description>Borland's InterBase</description></item>		
        ///		<item><term>"SQLITE"</term><description>SQLite</description></item>		
        ///		<item><term>"VISTADB"</term><description>VistaDB Database</description></item>		
        ///		<item><term>"ADVANTAGE"</term><description>Advantage Database Server</description></item>	
        ///		<item><term>"ISERIES"</term><description>iSeries (AS400)</description></item>	
        ///	</list>
        /// Below are some sample connection strings. However, the "Data Link" dialog available on the Default Settings dialog can help you.
        /// <list type="table">
        ///		<item><term>"ACCESS"</term><description>Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\access\newnorthwind.mdb;User Id=;Password=</description></item>
        ///		<item><term>"DB2"</term><description>Provider=IBMDADB2.1;Password=sa;User ID=DB2Admin;Data Source=MyMeta;Persist Security Info=True</description></item>	
        ///		<item><term>"MYSQL"</term><description>Provider=MySQLProv;Persist Security Info=True;Data Source=test;UID=griffo;PWD=;PORT=3306</description></item>
        ///		<item><term>"MYSQL2"</term><description>Uses Database=Test;Data Source=Griffo;User Id=anonymous;</description></item>
        ///		<item><term>"ORACLE"</term><description>Provider=OraOLEDB.Oracle.1;Password=sa;Persist Security Info=True;User ID=GRIFFO;Data Source=dbMeta</description></item>
        ///		<item><term>"SQL"</term><description>Provider=SQLOLEDB.1;Persist Security Info=False;User ID=sa;Initial Catalog=Northwind;Data Source=localhost</description></item>
        ///		<item><term>"PERVASIVE"</term><description>Provider=PervasiveOLEDB.8.60;Data Source=demodata;Location=Griffo;Persist Security Info=False</description></item>		
        ///		<item><term>"POSTGRESQL"</term><description>Server=www.myserver.com;Port=5432;User Id=myuser;Password=aaa;Database=mygeneration;</description></item>		
        ///		<item><term>"POSTGRESQL8"</term><description>Server=www.myserver.com;Port=5432;User Id=myuser;Password=aaa;Database=mygeneration;</description></item>		
        ///		<item><term>"FIREBIRD"</term><description>Database=C:\firebird\EMPLOYEE.GDB;User=SYSDBA;Password=wow;Dialect=3;Server=localhost</description></item>		
        ///		<item><term>"INTERBASE"</term><description>Database=C:\interbase\EMPLOYEE.GDB;User=SYSDBA;Password=wow;Dialect=3;Server=localhost</description></item>		
        ///		<item><term>"SQLITE"</term><description>Data Source=C:\SQLite\employee.db;New=False;Compress=True;Synchronous=Off;Version=3</description></item>		
        ///		<item><term>"VISTADB"</term><description>DataSource=C:\Program Files\VistaDB 2.0\Data\Northwind.vdb</description></item>		
        ///		<item><term>"ADVANTAGE"</term><description>Provider=Advantage.OLEDB.1;Password="";User ID=AdsSys;Data Source=C:\task1;Initial Catalog=aep_tutorial.add;Persist Security Info=True;Advantage Server Type=ADS_LOCAL_SERVER;Trim Trailing Spaces=TRUE</description></item>		
        ///		<item><term>"ISERIES"</term><description>PROVIDER=IBMDA400; DATA SOURCE=MY_SYSTEM_NAME;USER ID=myUserName;PASSWORD=myPwd;DEFAULT COLLECTION=MY_LIBRARY;</description></item>		
        ///	</list>
        ///	</remarks>
        public bool Connect(string driverIn, string connectionString)
        {
            string driver = driverIn.ToUpper();
            switch (driver)
            {
                case MyMetaDrivers.None:
                    return true;
#if !IGNORE_VISTA
                case MyMetaDrivers.VistaDB:
#endif
                case MyMetaDrivers.SQL:
                case MyMetaDrivers.Oracle:
                case MyMetaDrivers.Access:
                case MyMetaDrivers.MySql:
                case MyMetaDrivers.MySql2:
                case MyMetaDrivers.DB2:
                case MyMetaDrivers.ISeries:
                case MyMetaDrivers.Pervasive:
                case MyMetaDrivers.PostgreSQL:
                case MyMetaDrivers.PostgreSQL8:
                case MyMetaDrivers.Firebird:
                case MyMetaDrivers.Interbase:
                case MyMetaDrivers.SQLite:
                case MyMetaDrivers.Advantage:
                    return Connect(MyMetaDrivers.GetDbDriverFromName(driver), driver, connectionString);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Same as <see cref="Connect(string, string)"/>(string, string) only this uses an enumeration.  
        /// </summary>
        /// <param name="driver">The driver enumeration for you DBMS system</param>
        /// <param name="connectionString">A valid connection string for you DBMS</param>
        /// <returns></returns>
        public bool Connect(dbDriver driver, string connectionString)
        {
            return Connect(driver, string.Empty, connectionString);
        }

        /// <summary>
        /// Same as <see cref="Connect(string, string)"/>(string, string) only this uses an enumeration.  
        /// </summary>
        /// <param name="driver">The driver enumeration for you DBMS system</param>
        /// <param name="pluginName">The name of the plugin</param>
        /// <param name="connectionString">A valid connection string for you DBMS</param>
        /// <returns></returns>
        public bool Connect(dbDriver driver, string pluginName, string connectionString)
        {
            Reset();

            string dbName;
            int index;

            _connectionString = connectionString.Replace("\"", "");
            _driver = driver;

            #region not fully implemented yet

/*
            InternalDriver drv = InternalDriver.Get(settings.DbDriver);
            if (drv != null)
            {
                this._driverString = drv.DriverId;
                this.StripTrailingNulls = drv.StripTrailingNulls;
                this.requiredDatabaseName = drv.RequiredDatabaseName;

                IDbConnection con = null;
		        try
		        {
                    ClassFactory = drv.CreateBuildInClass();
                    if (ClassFactory != null)
                        con = ClassFactory.CreateConnection();
                    else
                    {
                        IMyMetaPlugin plugin = drv.CreateMyMetaPluginClass();
                        if (plugin != null)
                        {
                            MyMetaPluginContext pluginContext = new MyMetaPluginContext(drv.DriverId, this._connectionString);
                            plugin.Initialize(pluginContext);
                            con = plugin.NewConnection;
                        }
                    }
                    if (con != null)
                    {
                        con.ConnectionString = this._connectionString;
			            // cn.Open();
                    }
			        this._defaultDatabase = drv.GetDataBaseName(cn);
		        }
		        catch(Exception Ex)
		        {
			        throw Ex;
		        } finally {
                    if (con != null)
			            cn.Close();
                }
            }
            else
            {
                // Error
            }           
*/

            #endregion not fully implemented yet

            switch (_driver)
            {
                case dbDriver.SQL:

                    ConnectUsingOleDb(_driver, _connectionString);
                    _driverString = MyMetaDrivers.SQL;
                    StripTrailingNulls = false;
                    requiredDatabaseName = true;
                    ClassFactory = new ClassFactory();
                    break;

                case dbDriver.Oracle:

                    ConnectUsingOleDb(_driver, _connectionString);
                    _driverString = MyMetaDrivers.Oracle;
                    StripTrailingNulls = false;
                    requiredDatabaseName = true;
                    ClassFactory = new Oracle.ClassFactory();
                    break;

                case dbDriver.Access:

                    ConnectUsingOleDb(_driver, _connectionString);
                    _driverString = MyMetaDrivers.Access;
                    StripTrailingNulls = false;
                    requiredDatabaseName = false;
                    ClassFactory = new Access.ClassFactory();
                    break;

                case dbDriver.MySql:

                    ConnectUsingOleDb(_driver, _connectionString);
                    _driverString = MyMetaDrivers.MySql;
                    StripTrailingNulls = true;
                    requiredDatabaseName = true;
                    ClassFactory = new MySql.ClassFactory();
                    break;

                case dbDriver.MySql2:

                    using (var mysqlconn = new MySqlConnection(_connectionString))
                    {
                        mysqlconn.Close();
                        mysqlconn.Open();
                        _defaultDatabase = mysqlconn.Database;
                    }

                    _driverString = MyMetaDrivers.MySql2;
                    StripTrailingNulls = true;
                    requiredDatabaseName = true;
                    ClassFactory = new MySql5.ClassFactory();
                    break;

                case dbDriver.DB2:

                    ConnectUsingOleDb(_driver, _connectionString);
                    _driverString = MyMetaDrivers.DB2;
                    StripTrailingNulls = false;
                    requiredDatabaseName = false;
                    ClassFactory = new DB2.ClassFactory();
                    break;

                case dbDriver.ISeries:

                    ConnectUsingOleDb(_driver, _connectionString);
                    _driverString = MyMetaDrivers.ISeries;
                    StripTrailingNulls = false;
                    requiredDatabaseName = false;
                    ClassFactory = new ISeries.ClassFactory();
                    break;

                case dbDriver.Pervasive:

                    ConnectUsingOleDb(_driver, _connectionString);
                    _driverString = MyMetaDrivers.Pervasive;
                    StripTrailingNulls = false;
                    requiredDatabaseName = false;
                    ClassFactory = new Pervasive.ClassFactory();
                    break;

                case dbDriver.PostgreSQL:

                    using (var cn = new NpgsqlConnection(_connectionString))
                    {
                        cn.Open();
                        _defaultDatabase = cn.Database;
                    }

                    _driverString = MyMetaDrivers.PostgreSQL;
                    StripTrailingNulls = false;
                    requiredDatabaseName = false;
                    ClassFactory = new PostgreSQL.ClassFactory();
                    break;

                case dbDriver.PostgreSQL8:

                    using (var cn8 = new NpgsqlConnection(_connectionString))
                    {
                        cn8.Open();
                        _defaultDatabase = cn8.Database;
                    }

                    _driverString = MyMetaDrivers.PostgreSQL8;
                    StripTrailingNulls = false;
                    requiredDatabaseName = false;
                    ClassFactory = new PostgreSQL8.ClassFactory();
                    break;

                case dbDriver.Firebird:

                    using (var cn1 = new FbConnection(_connectionString))
                    {
                        cn1.Open();
                        dbName = cn1.Database;
                    }

                    try
                    {
                        index = dbName.LastIndexOfAny(new[] {'\\'});
                        if (index >= 0)
                        {
                            _defaultDatabase = dbName.Substring(index + 1);
                        }
                    }
                    catch {}

                    _driverString = MyMetaDrivers.Firebird;
                    StripTrailingNulls = false;
                    requiredDatabaseName = false;
                    ClassFactory = new Firebird.ClassFactory();
                    break;

                case dbDriver.Interbase:

                    using (var cn2 = new FbConnection(_connectionString))
                    {
                        cn2.Open();
                        _defaultDatabase = cn2.Database;
                    }

                    _driverString = MyMetaDrivers.Interbase;
                    StripTrailingNulls = false;
                    requiredDatabaseName = false;
                    ClassFactory = new Firebird.ClassFactory();
                    break;

                case dbDriver.SQLite:

                    using (var sqliteConn = new SQLiteConnection(_connectionString))
                    {
                        sqliteConn.Open();
                        dbName = sqliteConn.Database;
                    }
                    _driverString = MyMetaDrivers.SQLite;
                    StripTrailingNulls = false;
                    requiredDatabaseName = false;
                    ClassFactory = new SQLite.ClassFactory();
                    break;
#if !IGNORE_VISTA
                case dbDriver.VistaDB:

                    try
                    {
                        var mh = new MetaHelper();
                        dbName = mh.LoadDatabases(_connectionString);

                        if (dbName == "") return false;

                        _defaultDatabase = dbName;

                        _driverString = MyMetaDrivers.VistaDB;
                        StripTrailingNulls = false;
                        requiredDatabaseName = false;
                        ClassFactory = new VistaDB.ClassFactory();
                    }
                    catch
                    {
                        throw new Exception("Invalid VistaDB connection or VistaDB not installed");
                    }

                    break;
#endif
                case dbDriver.Advantage:

                    ConnectUsingOleDb(_driver, _connectionString);
                    _driverString = MyMetaDrivers.Advantage;
                    StripTrailingNulls = false;
                    requiredDatabaseName = false;
                    ClassFactory = new Advantage.ClassFactory();
                    string[] s = _defaultDatabase.Split('.');
                    _defaultDatabase = s[0];
                    break;

                case dbDriver.Plugin:
                    break;

                case dbDriver.None:

                    _driverString = MyMetaDrivers.None;
                    break;
            }

            _isConnected = true;
            return true;
        }

        private void ConnectUsingOleDb(dbDriver driver, string connectionString)
        {
            var cn = new OleDbConnection(connectionString.Replace("\"", ""));
            cn.Open();
            _defaultDatabase = GetDefaultDatabase(cn, driver);
            cn.Close();
        }

        private static string GetDefaultDatabase(DbConnection cn, dbDriver driver)
        {
            string databaseName;

            switch (driver)
            {
                case dbDriver.Access:

                    int i = cn.DataSource.LastIndexOf(@"\");

                    databaseName = i == -1 ? cn.DataSource : cn.DataSource.Substring(++i);

                    break;

                default:

                    databaseName = cn.Database;
                    break;
            }

            return databaseName;
        }

        #endregion

        #region Settings

        /// <summary>
        /// Determines whether system tables and views and alike are shown, the default is False. If True, ONLY system data is shown.
        /// </summary>
        public bool ShowSystemData
        {
            get { return _showSystemData; }
            set { _showSystemData = value; }
        }

        /// <summary>
        /// If this is true then four IColumn properties are actually supplied by the Domain, if the Column has an IDomain. 
        /// The four properties are DataTypeName, DataTypeNameComplete, LanguageType, and DbTargetType.
        /// </summary>
        public bool DomainOverride
        {
            get { return _domainOverride; }
            set { _domainOverride = value; }
        }

        #endregion
    }

    /// <summary>
    /// The current list of support dbDrivers. Typically VBScript and JScript use the string version as defined by MyMeta.DriverString.
    /// </summary>
    public enum dbDriver
    {
        /// <summary>
        /// String form is "SQL" for DriverString property
        /// </summary>
        SQL,

        /// <summary>
        /// String form is "ORACLE" for DriverString property
        /// </summary>
        Oracle,

        /// <summary>
        /// String form is "ACCESS" for DriverString property
        /// </summary>
        Access,

        /// <summary>
        /// String form is "MYSQL" for DriverString property
        /// </summary>
        MySql,

        /// <summary>
        /// String form is "MYSQL" for DriverString property
        /// </summary>
        MySql2,

        /// <summary>
        /// String form is "DB2" for DriverString property
        /// </summary>
        DB2,

        /// <summary>
        /// String form is "ISeries" for DriverString property
        /// </summary>
        ISeries,

        /// <summary>
        /// String form is "PERVASIVE" for DriverString property
        /// </summary>
        Pervasive,

        /// <summary>
        /// String form is "POSTGRESQL" for DriverString property
        /// </summary>
        PostgreSQL,

        /// <summary>
        /// String form is "POSTGRESQL8" for DriverString property
        /// </summary>
        PostgreSQL8,

        /// <summary>
        /// String form is "FIREBIRD" for DriverString property
        /// </summary>
        Firebird,

        /// <summary>
        /// String form is "INTERBASE" for DriverString property
        /// </summary>
        Interbase,

        /// <summary>
        /// String form is "SQLITE" for DriverString property
        /// </summary>
        SQLite,

#if !IGNORE_VISTA
        /// <summary>
        /// String form is "VISTADB" for DriverString property
        /// </summary>
        VistaDB,
#endif

        /// <summary>
        /// String form is "ADVANTAGE" for DriverString property
        /// </summary>
        Advantage,

        /// <summary>
        /// This is a placeholder for plugin providers
        /// </summary>
        Plugin,

        /// <summary>
        /// Use this if you want know connection at all
        /// </summary>
        None
    }

    #region MyMetaDrivers string Constants

    public static class MyMetaDrivers
    {
        public const string Access = "ACCESS";
        public const string Advantage = "ADVANTAGE";
        public const string DB2 = "DB2";
        public const string Firebird = "FIREBIRD";
        public const string Interbase = "INTERBASE";
        public const string ISeries = "ISERIES";
        public const string MySql = "MYSQL";
        public const string MySql2 = "MYSQL2";
        public const string None = "NONE";
        public const string Oracle = "ORACLE";
        public const string Pervasive = "PERVASIVE";
        public const string PostgreSQL = "POSTGRESQL";
        public const string PostgreSQL8 = "POSTGRESQL8";
        public const string SQL = "SQL";
        public const string SQLite = "SQLITE";
#if !IGNORE_VISTA
        public const string VistaDB = "VISTADB";
#endif

        public static dbDriver GetDbDriverFromName(string name)
        {
            switch (name)
            {
                case SQL:
                    return dbDriver.SQL;
                case Oracle:
                    return dbDriver.Oracle;
                case Access:
                    return dbDriver.Access;
                case MySql:
                    return dbDriver.MySql;
                case MySql2:
                    return dbDriver.MySql2;
                case DB2:
                    return dbDriver.DB2;
                case ISeries:
                    return dbDriver.ISeries;
                case Pervasive:
                    return dbDriver.Pervasive;
                case PostgreSQL:
                    return dbDriver.PostgreSQL;
                case PostgreSQL8:
                    return dbDriver.PostgreSQL8;
                case Firebird:
                    return dbDriver.Firebird;
                case Interbase:
                    return dbDriver.Interbase;
                case SQLite:
                    return dbDriver.SQLite;
#if !IGNORE_VISTA
                case VistaDB:
                    return dbDriver.VistaDB;
#endif
                case Advantage:
                    return dbDriver.Advantage;
                case None:
                    return dbDriver.None;
                default:
                    return dbDriver.Plugin;
            }
        }
    }

    #endregion
}