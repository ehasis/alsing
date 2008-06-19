using System;
using System.Runtime.InteropServices;
using System.Xml;

namespace MyMeta
{
    public class Parameter : Single, IParameter, INameValueItem
    {
        #region Collections

        internal PropertyCollectionAll _allProperties;

        public virtual IPropertyCollection GlobalProperties
        {
            get
            {
                Database db = Parameters.Procedure.Procedures.Database;
                if (null == db._parameterProperties)
                {
                    db._parameterProperties = new PropertyCollection {Parent = this};

                    string xPath = GlobalUserDataXPath;
                    XmlNode xmlNode = dbRoot.UserData.SelectSingleNode(xPath, null);

                    if (xmlNode == null)
                    {
                        XmlNode parentNode = db.CreateGlobalXmlNode();

                        xmlNode = parentNode.OwnerDocument.CreateNode(XmlNodeType.Element, "Parameter", null);
                        parentNode.AppendChild(xmlNode);
                    }

                    db._parameterProperties.LoadAllGlobal(xmlNode);
                }

                return db._parameterProperties;
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
            get { return GetString(Parameters.f_ParameterName); }
        }

        public virtual Int32 Ordinal
        {
            get { return GetInt32(Parameters.f_Ordinal); }
        }

        public virtual Int32 ParameterType
        {
            get { return GetInt32(Parameters.f_Type); }
        }

        public virtual Boolean HasDefault
        {
            get { return GetBool(Parameters.f_HasDefault); }
        }

        public virtual string Default
        {
            get
            {
                if (HasDefault && null != Parameters.f_Default)
                {
                    object o = _row[Parameters.f_Default];

                    if (o == DBNull.Value)
                    {
                        return "<null>";
                    }
                }

                return GetString(Parameters.f_Default);
            }
        }

        public virtual Boolean IsNullable
        {
            get { return GetBool(Parameters.f_IsNullable); }
        }

        public virtual Int32 DataType
        {
            get { return GetInt32(Parameters.f_DataType); }
        }

        public virtual Int32 CharacterMaxLength
        {
            get { return GetInt32(Parameters.f_CharMaxLength); }
        }

        public virtual Int32 CharacterOctetLength
        {
            get { return GetInt32(Parameters.f_CharOctetLength); }
        }

        public virtual Int32 NumericPrecision
        {
            get { return GetInt32(Parameters.f_NumericPrecision); }
        }

        public virtual Int32 NumericScale
        {
            get { return GetInt16(Parameters.f_NumericScale); }
        }

        public virtual string Description
        {
            get { return GetString(Parameters.f_Description); }
        }

        public virtual string TypeName
        {
            get { return GetString(Parameters.f_TypeName); }
        }

        public virtual string LocalTypeName
        {
            get { return GetString(Parameters.f_LocalTypeName); }
        }

        public virtual ParamDirection Direction
        {
            get
            {
                Int32 dir = ParameterType;

                switch (dir)
                {
                    case 1:
                        return ParamDirection.Input;
                    case 2:
                        return ParamDirection.InputOutput;
                    case 3:
                        return ParamDirection.Output;
                    case 4:
                        return ParamDirection.ReturnValue;
                    default:
                        return ParamDirection.Unknown;
                }
            }
        }

        public virtual string LanguageType
        {
            get
            {
                if (dbRoot.LanguageNode != null)
                {
                    string xPath = @"./Type[@From='" + TypeName + "']";

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
                    string xPath = @"./Type[@From='" + TypeName + "']";

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

        public virtual string DataTypeNameComplete
        {
            get { return GetString(Parameters.f_FullTypeName); }
        }

        #endregion

        #region XML User Data
         public override string GlobalUserDataXPath
        {
            get { return Parameters.Procedure.Procedures.Database.GlobalUserDataXPath + "/Parameter"; }
        }

        public override string UserDataXPath
        {
            get { return Parameters.UserDataXPath + @"/Parameter[@p='" + Name + "']"; }
        }

        internal override bool GetXmlNode(out XmlNode node, bool forceCreate)
        {
            node = null;
            bool success = false;

            if (null == _xmlNode)
            {
                // Get the parent node
                XmlNode parentNode;
                if (Parameters.GetXmlNode(out parentNode, forceCreate))
                {
                    // See if our user data already exists
                    string xPath = @"./Parameter[@p='" + Name + "']";
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
            XmlNode myNode = parentNode.OwnerDocument.CreateNode(XmlNodeType.Element, "Parameter", null);
            parentNode.AppendChild(myNode);

            XmlAttribute attr = parentNode.OwnerDocument.CreateAttribute("p");
            attr.Value = Name;
            myNode.Attributes.Append(attr);

            attr = parentNode.OwnerDocument.CreateAttribute("n");
            attr.Value = "";
            myNode.Attributes.Append(attr);
        }

        #endregion

        internal Parameters Parameters;

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
    }
}