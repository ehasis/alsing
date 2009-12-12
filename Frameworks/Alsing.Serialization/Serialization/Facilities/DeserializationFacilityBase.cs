using System;

namespace Alsing.Serialization
{
    public class DeserializationFacilityBase : IDeserializationFacility
    {
        #region IDeserializationFacility Members

        public virtual void FieldMissing(string fieldName, object instance, object value)
        {
        }

        public virtual void TypeMissing(string typeName, ref Type substitutionType)
        {
        }

        public virtual void ObjectCreated(object instance)
        {
        }

        public virtual void ObjectConfigured(object instance)
        {
        }

        #endregion
    }
}