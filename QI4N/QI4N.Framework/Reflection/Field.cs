namespace QI4N.Framework.API.Reflection
{
    using System;
    using System.Reflection;

    //should be optimized to IL generation later on instad of reflection
    public class Field
    {
        private readonly FieldInfo fieldInfo;

        public Field(FieldInfo fieldInfo)
        {
            this.fieldInfo = fieldInfo;
        }

        public Type FieldType
        {
            get
            {
                return this.fieldInfo.FieldType;
            }
        }

        public object GetValue(object instance)
        {
            return this.fieldInfo.GetValue(instance);
        }

        public void SetValue(object instance, object value)
        {
            this.fieldInfo.SetValue(instance, value);
        }
    }
}