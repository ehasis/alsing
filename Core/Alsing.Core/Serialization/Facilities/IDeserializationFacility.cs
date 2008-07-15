using System;

namespace Alsing.Serialization
{
    public interface IDeserializationFacility
    {
        void FieldMissing(string fieldName, object instance, object value);
        void TypeMissing(string typeName, ref Type substitutionType);
        void ObjectCreated(object instance);
        void ObjectConfigured(object instance);
    }
}
