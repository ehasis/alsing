using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GenerationStudio.Attributes;
using GenerationStudio.Gui;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof (ColumnsElement))]
    [ElementName("Column")]
    [ElementIcon("GenerationStudio.Images.column.gif")]
    public class ColumnElement : NamedElement
    {
        [OptionalField] private int autoKeySeed;
        [OptionalField] private int autoKeyIncrement;
        [OptionalField] private string dbType;
        [OptionalField] private string @default;
        [OptionalField] private bool isAutoKey;


        [OptionalField] private bool isInPrimaryKey;
        [OptionalField] private bool isNullable;
        [OptionalField] private int maxLength;

        [OptionalField] private int ordinal;

        public string DbType
        {
            get { return dbType; }
            set
            {
                dbType = value;
                OnNotifyChange();
            }
        }

        public bool IsInPrimaryKey
        {
            get { return isInPrimaryKey; }
            set
            {
                isInPrimaryKey = value;
                OnNotifyChange();
            }
        }

        public Type NativeType
        {
            get { return typeof(string); }
        }

        public int Ordinal
        {
            get { return ordinal; }
            set { ordinal = value; }
        }

        public int MaxLength
        {
            get { return maxLength; }
            set { maxLength = value; }
        }

        public int AutoKeySeed
        {
            get { return autoKeySeed; }
            set { autoKeySeed = value; }
        }


        public int AutoKeyIncrement
        {
            get { return autoKeyIncrement; }
            set { autoKeyIncrement = value; }
        }

        public bool IsNullable
        {
            get { return isNullable; }
            set { isNullable = value; }
        }

        public string Default
        {
            get { return @default; }
            set { @default = value; }
        }

        public bool IsAutoKey
        {
            get { return isAutoKey; }
            set { isAutoKey = value; }
        }

        public override string GetIconName()
        {
            if (IsInPrimaryKey)
                return "GenerationStudio.Images.pk.gif";
            else
                return base.GetIconName();
        }

        [ElementVerb("Toggle Identity")]
        public void ToggleIdentity(IHost host)
        {
            IsInPrimaryKey = !IsInPrimaryKey;
        }

        public override int GetSortPriority()
        {
            return Ordinal;
        }

        public override IList<ElementError> GetErrors()
        {
            var errors = new List<ElementError>();
            if (string.IsNullOrEmpty(DbType))
                errors.Add(new ElementError(this,
                                            string.Format("Column {0}.{1} is missing DbType", Parent.GetDisplayName(),
                                                          GetDisplayName())));

            return errors;
        }
    }
}