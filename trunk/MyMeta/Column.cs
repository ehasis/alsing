using System;
using System.Runtime.InteropServices;
using System.Xml;

namespace MyMeta
{
    public class Column : Single, IColumn, INameValueItem
    {
        private static readonly ForeignKeys _emptyForeignKeys = new ForeignKeys();
        protected ForeignKeys _foreignKeys;
        internal Columns Columns;

        #region INameValueItem Members

        public string ItemName
        {
            get { return Name; }
        }

        public string ItemValue
        {
            get { return Name; }
        }

        #endregion

        internal virtual Column Clone()
        {
            var c = (Column) dbRoot.ClassFactory.CreateColumn();

            c.dbRoot = dbRoot;
            c.Columns = Columns;
            c._row = _row;

            c._foreignKeys = _emptyForeignKeys;

            return c;
        }

        #region Objects

        public ITable Table
        {
            get
            {
                ITable theTable = null;

                if (null != Columns.Table)
                {
                    theTable = Columns.Table;
                }
                else if (null != Columns.Index)
                {
                    theTable = Columns.Index.Indexes.Table;
                }
                else if (null != Columns.ForeignKey)
                {
                    theTable = Columns.ForeignKey.ForeignKeys.Table;
                }

                return theTable;
            }
        }

        public IView View
        {
            get
            {
                IView theView = null;

                if (null != Columns.View)
                {
                    theView = Columns.View;
                }

                return theView;
            }
        }

        public IDomain Domain
        {
            get
            {
                IDomain theDomain = null;

                if (HasDomain)
                {
                    theDomain = Columns.GetDatabase().Domains[DomainName];
                }

                return theDomain;
            }
        }

        #endregion

        #region Properties

        [DispId(0)]
        public override string Alias
        {
            get
            {
                XmlNode node = null;
                if (GetXmlNode(out node, false))
                {
                    string niceName = null;

                    if (GetUserData(node, "n", out niceName))
                    {
                        if (string.Empty != niceName)
                            return niceName;
                    }
                }

                // There was no nice name
                return Name;
            }

            set
            {
                XmlNode node = null;
                if (GetXmlNode(out node, true))
                {
                    SetUserData(node, "n", value);
                }
            }
        }

        public override string Name
        {
            get { return GetString(Columns.f_Name); }
        }

        public virtual Guid Guid
        {
            get { return GetGuid(Columns.f_Guid); }
        }

        public virtual Int32 PropID
        {
            get { return GetInt32(Columns.f_PropID); }
        }

        public virtual Int32 Ordinal
        {
            get { return GetInt32(Columns.f_Ordinal); }
        }

        public virtual Boolean HasDefault
        {
            get { return GetBool(Columns.f_HasDefault); }
        }

        public virtual string Default
        {
            get { return GetString(Columns.f_Default); }
        }

        public virtual Int32 Flags
        {
            get { return GetInt32(Columns.f_Flags); }
        }

        public virtual Boolean IsNullable
        {
            get { return GetBool(Columns.f_IsNullable); }
        }

        public virtual Int32 DataType
        {
            get { return GetInt32(Columns.f_DataType); }
        }

        public virtual Guid TypeGuid
        {
            get { return GetGuid(Columns.f_TypeGuid); }
        }

        public virtual Int32 CharacterMaxLength
        {
            get { return GetInt32(Columns.f_MaxLength); }
        }

        public virtual Int32 CharacterOctetLength
        {
            get { return GetInt32(Columns.f_OctetLength); }
        }

        public virtual Int32 NumericPrecision
        {
            get { return GetInt32(Columns.f_NumericPrecision); }
        }

        public virtual Int32 NumericScale
        {
            get { return GetInt32(Columns.f_NumericScale); }
        }

        public virtual Int32 DateTimePrecision
        {
            get { return GetInt32(Columns.f_DatetimePrecision); }
        }

        public virtual string CharacterSetCatalog
        {
            get { return GetString(Columns.f_CharSetCatalog); }
        }

        public virtual string CharacterSetSchema
        {
            get { return GetString(Columns.f_CharSetSchema); }
        }

        public virtual string CharacterSetName
        {
            get { return GetString(Columns.f_CharSetName); }
        }

        public virtual string DomainCatalog
        {
            get { return GetString(Columns.f_DomainCatalog); }
        }

        public virtual string DomainSchema
        {
            get { return GetString(Columns.f_DomainSchema); }
        }

        public virtual string DomainName
        {
            get { return GetString(Columns.f_DomainName); }
        }

        public virtual string Description
        {
            get { return GetString(Columns.f_Description); }
        }

        public virtual Int32 LCID
        {
            get { return GetInt32(Columns.f_LCID); }
        }

        public virtual Int32 CompFlags
        {
            get { return GetInt32(Columns.f_CompFlags); }
        }

        public virtual Int32 SortID
        {
            get { return GetInt32(Columns.f_SortID); }
        }

        public virtual Byte[] TDSCollation
        {
            get { return GetByteArray(Columns.f_TDSCollation); }
        }

        public virtual Boolean IsComputed
        {
            get { return GetBool(Columns.f_IsComputed); }
        }

        public virtual Boolean IsInPrimaryKey
        {
            get
            {
                Boolean isPrimaryKey = false;

                if (null != Columns.Table)
                {
                    IColumn c = Columns.Table.PrimaryKeys[Name];

                    if (null != c)
                    {
                        isPrimaryKey = true;
                    }
                }

                return isPrimaryKey;
            }
        }

        public virtual Boolean IsAutoKey
        {
            get { return GetBool(Columns.f_IsAutoKey); }
        }

        public virtual string DataTypeName
        {
            get
            {
                if (dbRoot.DomainOverride)
                {
                    if (HasDomain)
                    {
                        if (Domain != null)
                        {
                            return Domain.DataTypeName;
                        }
                    }
                }

                return GetString(null);
            }
        }

        public virtual string LanguageType
        {
            get
            {
                if (dbRoot.DomainOverride)
                {
                    if (HasDomain)
                    {
                        if (Domain != null)
                        {
                            return Domain.LanguageType;
                        }
                    }
                }

                if (dbRoot.LanguageNode != null)
                {
                    string xPath = @"./Type[@From='" + DataTypeName + "']";

                    XmlNode node = dbRoot.LanguageNode.SelectSingleNode(xPath, null);

                    if (node != null)
                    {
                        string languageType = "";
                        if (GetUserData(node, "To", out languageType))
                        {
                            return languageType;
                        }
                    }
                }

                return "Unknown";
            }
        }

        public virtual string DbTargetType
        {
            get
            {
                if (dbRoot.DomainOverride)
                {
                    if (HasDomain)
                    {
                        if (Domain != null)
                        {
                            return Domain.DbTargetType;
                        }
                    }
                }

                if (dbRoot.DbTargetNode != null)
                {
                    string xPath = @"./Type[@From='" + DataTypeName + "']";

                    XmlNode node = dbRoot.DbTargetNode.SelectSingleNode(xPath, null);

                    if (node != null)
                    {
                        string driverType = "";
                        if (GetUserData(node, "To", out driverType))
                        {
                            return driverType;
                        }
                    }
                }

                return "Unknown";
            }
        }

        public virtual string DataTypeNameComplete
        {
            get
            {
                if (dbRoot.DomainOverride)
                {
                    if (HasDomain)
                    {
                        if (Domain != null)
                        {
                            return Domain.DataTypeNameComplete;
                        }
                    }
                }

                return "Unknown";
            }
        }

        public virtual Boolean IsInForeignKey
        {
            get
            {
                if (ForeignKeys == _emptyForeignKeys)
                    return true;
                else
                    return ForeignKeys.Count > 0 ? true : false;
            }
        }

        public virtual Int32 AutoKeySeed
        {
            get { return GetInt32(Columns.f_AutoKeySeed); }
        }

        public virtual Int32 AutoKeyIncrement
        {
            get { return GetInt32(Columns.f_AutoKeyIncrement); }
        }

        public virtual Boolean HasDomain
        {
            get
            {
                if (_row.Table.Columns.Contains("DOMAIN_NAME"))
                {
                    object o = _row["DOMAIN_NAME"];

                    if (o != null && o != DBNull.Value)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        #endregion

        #region Collections

        internal PropertyCollectionAll _allProperties;

        public IForeignKeys ForeignKeys
        {
            get
            {
                if (null == _foreignKeys)
                {
                    _foreignKeys = (ForeignKeys) dbRoot.ClassFactory.CreateForeignKeys();
                    _foreignKeys.dbRoot = dbRoot;

                    if (Columns.Table != null)
                    {
                        IForeignKeys fk = Columns.Table.ForeignKeys;
                    }
                }
                return _foreignKeys;
            }
        }

        public virtual IPropertyCollection GlobalProperties
        {
            get
            {
                var db = Columns.GetDatabase() as Database;
                if (null == db._columnProperties)
                {
                    db._columnProperties = new PropertyCollection {Parent = this};

                    string xPath = GlobalUserDataXPath;
                    XmlNode xmlNode = dbRoot.UserData.SelectSingleNode(xPath, null);

                    if (xmlNode == null)
                    {
                        XmlNode parentNode = db.CreateGlobalXmlNode();

                        xmlNode = parentNode.OwnerDocument.CreateNode(XmlNodeType.Element, "Column", null);
                        parentNode.AppendChild(xmlNode);
                    }

                    db._columnProperties.LoadAllGlobal(xmlNode);
                }

                return db._columnProperties;
            }
        }

        public virtual IPropertyCollection AllProperties
        {
            get
            {
                if (null == _allProperties)
                {
                    _allProperties = new PropertyCollectionAll();
                    _allProperties.Load(Properties, GlobalProperties);
                }

                return _allProperties;
            }
        }

        protected internal virtual void AddForeignKey(ForeignKey fk)
        {
            if (null == _foreignKeys)
            {
                _foreignKeys = (ForeignKeys) dbRoot.ClassFactory.CreateForeignKeys();
                _foreignKeys.dbRoot = dbRoot;
            }

            _foreignKeys.AddForeignKey(fk);
        }

        #endregion

        #region XML User Data

        [ComVisible(false)]
        public override string GlobalUserDataXPath
        {
            get { return ((Database) Columns.GetDatabase()).GlobalUserDataXPath + "/Column"; }
        }

        [ComVisible(false)]
        public override string UserDataXPath
        {
            get { return Columns.UserDataXPath + @"/Column[@p='" + Name + "']"; }
        }

        [ComVisible(false)]
        internal override bool GetXmlNode(out XmlNode node, bool forceCreate)
        {
            node = null;
            bool success = false;

            if (null == _xmlNode)
            {
                // Get the parent node
                XmlNode parentNode = null;
                if (Columns.GetXmlNode(out parentNode, forceCreate))
                {
                    // See if our user data already exists
                    string xPath = @"./Column[@p='" + Name + "']";
                    if (!GetUserData(xPath, parentNode, out _xmlNode) && forceCreate)
                    {
                        // Create it, and try again
                        CreateUserMetaData(parentNode);
                        GetUserData(xPath, parentNode, out _xmlNode);
                    }
                }
            }

            if (null != _xmlNode)
            {
                node = _xmlNode;
                success = true;
            }

            return success;
        }

        [ComVisible(false)]
        public override void CreateUserMetaData(XmlNode parentNode)
        {
            XmlNode myNode = parentNode.OwnerDocument.CreateNode(XmlNodeType.Element, "Column", null);
            parentNode.AppendChild(myNode);

            XmlAttribute attr;

            attr = parentNode.OwnerDocument.CreateAttribute("p");
            attr.Value = Name;
            myNode.Attributes.Append(attr);

            attr = parentNode.OwnerDocument.CreateAttribute("n");
            attr.Value = "";
            myNode.Attributes.Append(attr);
        }

        #endregion
    }
}