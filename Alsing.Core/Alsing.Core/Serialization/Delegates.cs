using System;

namespace Alsing.Serialization
{
    public delegate void FieldMissingHandler(string fieldName, object instance, object value);

    public delegate void TypeMissingHandler(string typeName, ref Type substitutionType);

    public delegate void ObjectCreatedHandler(object instance);

    public delegate void ObjectConfiguredHandler(object instance);
}