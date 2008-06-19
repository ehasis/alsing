using System;
using System.Data;
using System.Runtime.InteropServices;

namespace MyMeta
{
    public class Single : MetaObject
    {
        #region Properties

        public virtual string Alias
        {
            get { return string.Empty; }

            set { }
        }

        public virtual string Name
        {
            get { return string.Empty; }
        }

        #endregion

        #region Property Helpers

        protected string GetString(DataColumn col)
        {
            if (null != col)
            {
                object o = _row[col];

                if (DBNull.Value != o)
                {
                    var s = (string) o;
                    if (dbRoot.StripTrailingNulls)
                    {
                        if (s.EndsWith(dbRoot.TrailingNull))
                        {
                            s = s.Remove(s.Length - 1, 1);
                        }
                    }
                    return s;
                }
            }

            return string.Empty;
        }

        protected Guid GetGuid(DataColumn col)
        {
            if (null != col)
            {
                object o = _row[col];

                if (DBNull.Value != o)
                    return (Guid) o;
            }

            return Guid.Empty;
        }

        protected DateTime GetDateTime(DataColumn col)
        {
            if (null != col)
            {
                object o = _row[col];

                if (DBNull.Value != o)
                    return (DateTime) o;
            }

            return new DateTime(1, 1, 1, 1, 1, 1, 1);
        }

        protected Boolean GetBool(DataColumn col)
        {
            if (null != col)
            {
                object o = _row[col];

                if (DBNull.Value != o)
                {
                    if (o is Boolean)
                        return (Boolean) o;
                    else
                    {
                        int i = Convert.ToInt32(o);
                        return i == 0 ? false : true;
                    }
                }
            }

            return false;
        }

        protected Int16 GetInt16(DataColumn col)
        {
            if (null != col)
            {
                object o = _row[col];

                if (DBNull.Value != o)
                    return Convert.ToInt16(o);
            }

            return 0;
        }

        protected Int32 GetInt32(DataColumn col)
        {
            if (null != col)
            {
                object o = _row[col];

                if (DBNull.Value != o)
                    return Convert.ToInt32(o);
            }

            return 0;
        }

        protected Decimal GetDecimal(DataColumn col)
        {
            if (null != col)
            {
                object o = _row[col];

                if (DBNull.Value != o)
                    return Convert.ToDecimal(o);
            }

            return 0;
        }

        protected Byte[] GetByteArray(DataColumn col)
        {
            if (null != col)
            {
                object o = _row[col];

                if (DBNull.Value == o)
                    return null;
                return null;
            }

            return null;
        }

        #endregion

        protected PropertyCollection _properties;
        internal DataRow _row;

        internal DataRow Row
        {
            set { _row = value; }
        }

        public virtual IPropertyCollection Properties
        {
            get
            {
                if (null == _properties)
                {
                    _properties = new PropertyCollection {Parent = this};
                    _properties.LoadAll();
                }

                return _properties;
            }
        }

        public virtual object DatabaseSpecificMetaData(string key)
        {
            return null;
        }
    }
}