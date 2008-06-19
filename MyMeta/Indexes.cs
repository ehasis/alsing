using System;
using System.Collections;
using System.Data;
using System.Runtime.InteropServices;
using System.Xml;

namespace MyMeta
{
    [ComVisible(false), ClassInterface(ClassInterfaceType.AutoDual)]
    public class Indexes : Collection, IIndexes, IEnumerable, ICollection
    {
        internal DataColumn f_AutoUpdate;
        internal DataColumn f_Cardinality;
        internal DataColumn f_Catalog;
        internal DataColumn f_Clustered;
        internal DataColumn f_Collation;
        internal DataColumn f_FillFactor;
        internal DataColumn f_FilterCondition;
        internal DataColumn f_IndexCatalog;
        internal DataColumn f_IndexName;
        internal DataColumn f_IndexSchema;
        internal DataColumn f_InitializeSize;
        internal DataColumn f_Integrated;
        internal DataColumn f_NullCollation;
        internal DataColumn f_Nulls;
        internal DataColumn f_Pages;
        internal DataColumn f_Schema;
        internal DataColumn f_SortBookmarks;
        internal DataColumn f_TableName;
        internal DataColumn f_Type;
        internal DataColumn f_Unique;
        internal Table Table;

        #region IIndexes Members

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

        public override string UserDataXPath
        {
            get { return Table.UserDataXPath + @"/Indexes"; }
        }

        internal override bool GetXmlNode(out XmlNode node, bool forceCreate)
        {
            node = null;
            bool success = false;

            if (null == _xmlNode)
            {
                // Get the parent node
                XmlNode parentNode;
                if (Table.GetXmlNode(out parentNode, forceCreate))
                {
                    // See if our user data already exists
                    string xPath = @"./Indexes";
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

        public override void CreateUserMetaData(XmlNode parentNode)
        {
            XmlNode myNode = parentNode.OwnerDocument.CreateNode(XmlNodeType.Element, "Indexes", null);
            parentNode.AppendChild(myNode);
        }

        #endregion

        private void BindToColumns(DataTable metaData)
        {
            if (false == _fieldsBound)
            {
                if (metaData.Columns.Contains("TABLE_CATALOG")) f_Catalog = metaData.Columns["TABLE_CATALOG"];
                if (metaData.Columns.Contains("TABLE_SCHEMA")) f_Schema = metaData.Columns["TABLE_SCHEMA"];
                if (metaData.Columns.Contains("TABLE_NAME")) f_TableName = metaData.Columns["TABLE_NAME"];
                if (metaData.Columns.Contains("INDEX_CATALOG")) f_IndexCatalog = metaData.Columns["INDEX_CATALOG"];
                if (metaData.Columns.Contains("INDEX_SCHEMA")) f_IndexSchema = metaData.Columns["INDEX_SCHEMA"];
                if (metaData.Columns.Contains("INDEX_NAME")) f_IndexName = metaData.Columns["INDEX_NAME"];
                if (metaData.Columns.Contains("UNIQUE")) f_Unique = metaData.Columns["UNIQUE"];
                if (metaData.Columns.Contains("CLUSTERED")) f_Clustered = metaData.Columns["CLUSTERED"];
                if (metaData.Columns.Contains("TYPE")) f_Type = metaData.Columns["TYPE"];
                if (metaData.Columns.Contains("FILL_FACTOR")) f_FillFactor = metaData.Columns["FILL_FACTOR"];
                if (metaData.Columns.Contains("INITIAL_SIZE")) f_InitializeSize = metaData.Columns["INITIAL_SIZE"];
                if (metaData.Columns.Contains("NULLS")) f_Nulls = metaData.Columns["NULLS"];
                if (metaData.Columns.Contains("SORT_BOOKMARKS")) f_SortBookmarks = metaData.Columns["SORT_BOOKMARKS"];
                if (metaData.Columns.Contains("AUTO_UPDATE")) f_AutoUpdate = metaData.Columns["AUTO_UPDATE"];
                if (metaData.Columns.Contains("NULL_COLLATION")) f_NullCollation = metaData.Columns["NULL_COLLATION"];
                if (metaData.Columns.Contains("COLLATION")) f_Collation = metaData.Columns["COLLATION"];
                if (metaData.Columns.Contains("CARDINALITY")) f_Cardinality = metaData.Columns["CARDINALITY"];
                if (metaData.Columns.Contains("PAGES")) f_Pages = metaData.Columns["PAGES"];
                if (metaData.Columns.Contains("FILTER_CONDITION"))
                    f_FilterCondition = metaData.Columns["FILTER_CONDITION"];
                if (metaData.Columns.Contains("INTEGRATED")) f_Integrated = metaData.Columns["INTEGRATED"];
            }
        }

        internal virtual void LoadAll() {}

        internal void PopulateArray(DataTable metaData)
        {
            BindToColumns(metaData);

            DataRowCollection rows = metaData.Rows;
            int count = rows.Count;
            for (int i = 0; i < count; i++)
            {
                string indexName = rows[i]["INDEX_NAME"] as string;

                Index index = GetByName(indexName);

                if (null == index)
                {
                    index = (Index) dbRoot.ClassFactory.CreateIndex();
                    index.dbRoot = dbRoot;
                    index.Indexes = this;
                    index.Row = metaData.Rows[i];
                    _array.Add(index);
                }

                index.AddColumn(rows[i]["COLUMN_NAME"] as string);
            }
        }

        internal void PopulateArrayNoHookup(DataTable metaData)
        {
            BindToColumns(metaData);

            Index index;

            DataRowCollection rows = metaData.Rows;
            int count = rows.Count;
            for (int i = 0; i < count; i++)
            {
                string indexName = rows[i]["INDEX_NAME"] as string;

                index = GetByName(indexName);

                if (null == index)
                {
                    index = (Index) dbRoot.ClassFactory.CreateIndex();
                    index.dbRoot = dbRoot;
                    index.Indexes = this;
                    index.Row = metaData.Rows[i];
                    _array.Add(index);
                }
            }
        }

        internal void AddIndex(Index index)
        {
            _array.Add(index);
        }

        #region indexers

        public virtual IIndex this[object index]
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
                    return _array[idx] as Index;
                }
            }
        }

        public Index GetByName(string name)
        {
            Index obj = null;
            Index tmp = null;

            int count = _array.Count;
            for (int i = 0; i < count; i++)
            {
                tmp = _array[i] as Index;

                if (CompareStrings(name, tmp.Name))
                {
                    obj = tmp;
                    break;
                }
            }

            return obj;
        }

        public Index GetByPhysicalName(string name)
        {
            Index obj = null;
            Index tmp = null;

            int count = _array.Count;
            for (int i = 0; i < count; i++)
            {
                tmp = _array[i] as Index;

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