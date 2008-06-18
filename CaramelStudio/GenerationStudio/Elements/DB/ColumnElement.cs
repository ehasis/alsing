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
        [OptionalField] private int autoIncrementSeed;
        [OptionalField] private int autoIncrementStep;
        [OptionalField] private string dbType;
        [OptionalField] private string defaultValue;
        [OptionalField] private bool isAutoIncrement;


        [OptionalField] private bool isIdentity;
        [OptionalField] private bool isNullable;
        [OptionalField] private bool isUnique;
        [OptionalField] private int maxLength;

        [OptionalField] private Type nativeType;

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

        public bool IsIdentity
        {
            get { return isIdentity; }
            set
            {
                isIdentity = value;
                OnNotifyChange();
            }
        }

        public Type NativeType
        {
            get { return nativeType; }
            set { nativeType = value; }
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

        public int AutoIncrementSeed
        {
            get { return autoIncrementSeed; }
            set { autoIncrementSeed = value; }
        }


        public int AutoIncrementStep
        {
            get { return autoIncrementStep; }
            set { autoIncrementStep = value; }
        }

        public bool IsUnique
        {
            get { return isUnique; }
            set { isUnique = value; }
        }

        public bool IsNullable
        {
            get { return isNullable; }
            set { isNullable = value; }
        }

        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }

        public bool IsAutoIncrement
        {
            get { return isAutoIncrement; }
            set { isAutoIncrement = value; }
        }

        public override string GetIconName()
        {
            if (IsIdentity)
                return "GenerationStudio.Images.pk.gif";
            else
                return base.GetIconName();
        }

        [ElementVerb("Toggle Identity")]
        public void ToggleIdentity(IHost host)
        {
            IsIdentity = !IsIdentity;
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