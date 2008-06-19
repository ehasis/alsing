using System;
using System.Xml;

namespace MyMeta
{
    public class ResultColumn : Single, IResultColumn, INameValueItem
    {
        #region Collections

        internal PropertyCollectionAll _allProperties;

        public virtual IPropertyCollection GlobalProperties
        {
            get
            {
                Database db = ResultColumns.Procedure.Procedures.Database;
                if (null == db._resultColumnProperties)
                {
                    db._resultColumnProperties = new PropertyCollection {Parent = this};

                    string xPath = GlobalUserDataXPath;
                    XmlNode xmlNode = dbRoot.UserData.SelectSingleNode(xPath, null);

                    if (xmlNode == null)
                    {
                        XmlNode parentNode = db.CreateGlobalXmlNode();

                        xmlNode = parentNode.OwnerDocument.CreateNode(XmlNodeType.Element, "ResultColumn", null);
                        parentNode.AppendChild(xmlNode);
                    }

                    db._resultColumnProperties.LoadAllGlobal(xmlNode);
                }

                return db._resultColumnProperties;
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

        #endregion

        #region Properties

        public override string Alias
        {
            get
            {
                XmlNode node;
                if (GetXmlNode(out node, false))
                {
                    string niceName;

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
                XmlNode node;
                if (GetXmlNode(out node, true))
                {
                    SetUserData(node, "n", value);
                }
            }
        }

        public override string Name
        {
            get { return GetString(null); }
        }

        public virtual Int32 DataType
        {
            get { return 0; }
        }

        public virtual string DataTypeName
        {
            get { return GetString(null); }
        }

        public virtual string DataTypeNameComplete
        {
            get { return GetString(null); }
        }

        public virtual Int32 Ordinal
        {
            get { return GetInt32(null); }
        }

        public virtual string LanguageType
        {
            get
            {
                if (dbRoot.LanguageNode != null)
                {
                    string xPath = @"./Type[@From='" + DataTypeName + "']";

                    XmlNode node = dbRoot.LanguageNode.SelectSingleNode(xPath, null);

                    if (node != null)
                    {
                        string languageType;
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
                if (dbRoot.DbTargetNode != null)
                {
                    string xPath = @"./Type[@From='" + DataTypeName + "']";

                    XmlNode node = dbRoot.DbTargetNode.SelectSingleNode(xPath, null);

                    if (node != null)
                    {
                        string driverType;
                        if (GetUserData(node, "To", out driverType))
                        {
                            return driverType;
                        }
                    }
                }

                return "Unknown";
            }
        }

        #endregion

        #region XML User Data

        public override string GlobalUserDataXPath
        {
            get { return ResultColumns.Procedure.Procedures.Database.GlobalUserDataXPath + "/ResultColumn"; }
        }

        public override string UserDataXPath
        {
            get { return ResultColumns.UserDataXPath + @"/ResultColumn[@p='" + Name + "']"; }
        }
        internal override bool GetXmlNode(out XmlNode node, bool forceCreate)
        {
            node = null;
            bool success = false;

            if (null == _xmlNode)
            {
                // Get the parent node
                XmlNode parentNode;
                if (ResultColumns.GetXmlNode(out parentNode, forceCreate))
                {
                    // See if our user data already exists
                    string xPath = @"./ResultColumn[@p='" + Name + "']";
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
            XmlNode myNode = parentNode.OwnerDocument.CreateNode(XmlNodeType.Element, "ResultColumn", null);
            parentNode.AppendChild(myNode);

            XmlAttribute attr = parentNode.OwnerDocument.CreateAttribute("p");
            attr.Value = Name;
            myNode.Attributes.Append(attr);

            attr = parentNode.OwnerDocument.CreateAttribute("n");
            attr.Value = "";
            myNode.Attributes.Append(attr);
        }

        #endregion

        internal ResultColumns ResultColumns;

        #region INameValueItem Members

        public string ItemName
        {
            get { return Name; }
        }

        public string ItemValue
        {
            get { return Ordinal.ToString(); }
        }

        #endregion
    }
}