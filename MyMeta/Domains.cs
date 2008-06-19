using System;
using System.Collections;
using System.Data;
using System.Runtime.InteropServices;
using System.Xml;

namespace MyMeta
{
#if ENTERPRISE
    
    [ComVisible(false), ClassInterface(ClassInterfaceType.AutoDual)]
#endif
    public class Domains : Collection, IDomains, IEnumerable, ICollection
    {
        #region DataColumn Binding Stuff

        internal DataColumn f_CharSetCatalog;
        internal DataColumn f_CharSetName;
        internal DataColumn f_CharSetSchema;
        internal DataColumn f_CollationCatalog;
        internal DataColumn f_CollationName;
        internal DataColumn f_CollationSchema;
        internal DataColumn f_DataType;
        internal DataColumn f_DatetimePrecision;
        internal DataColumn f_Default;
        internal DataColumn f_DomainCatalog;
        internal DataColumn f_DomainName;
        internal DataColumn f_DomainSchema;
        internal DataColumn f_HasDefault;
        internal DataColumn f_IsNullable;
        internal DataColumn f_MaxLength;
        internal DataColumn f_NumericPrecision;
        internal DataColumn f_NumericScale;
        internal DataColumn f_OctetLength;

        private void BindToColumns(DataTable metaData)
        {
            if (false == _fieldsBound)
            {
                if (metaData.Columns.Contains("DOMAIN_CATALOG")) f_DomainCatalog = metaData.Columns["DOMAIN_CATALOG"];
                if (metaData.Columns.Contains("DOMAIN_SCHEMA")) f_DomainSchema = metaData.Columns["DOMAIN_SCHEMA"];
                if (metaData.Columns.Contains("DOMAIN_NAME")) f_DomainName = metaData.Columns["DOMAIN_NAME"];
                if (metaData.Columns.Contains("DATA_TYPE")) f_DataType = metaData.Columns["DATA_TYPE"];
                if (metaData.Columns.Contains("CHARACTER_MAXIMUM_LENGTH"))
                    f_MaxLength = metaData.Columns["CHARACTER_MAXIMUM_LENGTH"];
                if (metaData.Columns.Contains("CHARACTER_OCTET_LENGTH"))
                    f_OctetLength = metaData.Columns["CHARACTER_OCTET_LENGTH"];
                if (metaData.Columns.Contains("COLLATION_CATALOG"))
                    f_CollationCatalog = metaData.Columns["COLLATION_CATALOG"];
                if (metaData.Columns.Contains("COLLATION_SCHEMA"))
                    f_CollationSchema = metaData.Columns["COLLATION_SCHEMA"];
                if (metaData.Columns.Contains("COLLATION_NAME")) f_CollationName = metaData.Columns["COLLATION_NAME"];
                if (metaData.Columns.Contains("CHARACTER_SET_CATALOG"))
                    f_CharSetCatalog = metaData.Columns["CHARACTER_SET_CATALOG"];
                if (metaData.Columns.Contains("CHARACTER_SET_SCHEMA"))
                    f_CharSetSchema = metaData.Columns["CHARACTER_SET_SCHEMA"];
                if (metaData.Columns.Contains("CHARACTER_SET_NAME"))
                    f_CharSetName = metaData.Columns["CHARACTER_SET_NAME"];
                if (metaData.Columns.Contains("NUMERIC_PRECISION"))
                    f_NumericPrecision = metaData.Columns["NUMERIC_PRECISION"];
                if (metaData.Columns.Contains("NUMERIC_SCALE")) f_NumericScale = metaData.Columns["NUMERIC_SCALE"];
                if (metaData.Columns.Contains("DATETIME_PRECISION"))
                    f_DatetimePrecision = metaData.Columns["DATETIME_PRECISION"];
                if (metaData.Columns.Contains("DOMAIN_DEFAULT")) f_Default = metaData.Columns["DOMAIN_DEFAULT"];
                if (metaData.Columns.Contains("IS_NULLABLE")) f_IsNullable = metaData.Columns["IS_NULLABLE"];
            }
        }

        #endregion

        internal Database Database;

        #region IDomains Members

        public IEnumerator GetEnumerator()
        {
            return new Enumerator(_array);
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { }
        }

        #endregion

        #region XML User Data

#if ENTERPRISE
        [ComVisible(false)]
#endif
            public override string UserDataXPath
        {
            get { return Database.UserDataXPath + @"/Domains"; }
        }


#if ENTERPRISE
        [ComVisible(false)]
#endif
        internal override bool GetXmlNode(out XmlNode node, bool forceCreate)
        {
            node = null;
            bool success = false;

            if (null == _xmlNode)
            {
                // Get the parent node
                XmlNode parentNode = null;
                if (Database.GetXmlNode(out parentNode, forceCreate))
                {
                    // See if our user data already exists
                    string xPath = @"./Domains";
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

#if ENTERPRISE
        [ComVisible(false)]
#endif
        public override void CreateUserMetaData(XmlNode parentNode)
        {
            XmlNode myNode = parentNode.OwnerDocument.CreateNode(XmlNodeType.Element, "Columns", null);
            parentNode.AppendChild(myNode);
        }

        #endregion

        internal virtual void LoadAll() {}

        internal void PopulateArray(DataTable metaData)
        {
            BindToColumns(metaData);

            Domain domain = null;

            if (metaData.DefaultView.Count > 0)
            {
                IEnumerator enumerator = metaData.DefaultView.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var rowView = enumerator.Current as DataRowView;

                    domain = (Domain) dbRoot.ClassFactory.CreateDomain();
                    domain.dbRoot = dbRoot;
                    domain.Domains = this;
                    domain.Row = rowView.Row;
                    _array.Add(domain);
                }
            }
        }

        #region indexers

#if ENTERPRISE
        [DispId(0)]
#endif
        public IDomain this[object index]
        {
            get
            {
                if (index.GetType() == Type.GetType("System.String"))
                {
                    return GetByPhysicalName(index as String);
                }
                else
                {
                    int idx = Convert.ToInt32(index);
                    return _array[idx] as IDomain;
                }
            }
        }

#if ENTERPRISE
        [ComVisible(false)]
#endif
        public Domain GetByName(string name)
        {
            Domain obj = null;
            Domain tmp = null;

            int count = _array.Count;
            for (int i = 0; i < count; i++)
            {
                tmp = _array[i] as Domain;

                if (CompareStrings(name, tmp.Name))
                {
                    obj = tmp;
                    break;
                }
            }

            return obj;
        }

#if ENTERPRISE
        [ComVisible(false)]
#endif
        internal Domain GetByPhysicalName(string name)
        {
            Domain obj = null;
            Domain tmp = null;

            int count = _array.Count;
            for (int i = 0; i < count; i++)
            {
                tmp = _array[i] as Domain;

                if (CompareStrings(name, tmp.Name))
                {
                    obj = tmp;
                    break;
                }
            }

            return obj;
        }

        #endregion
    }
}