using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alsing.Serialization
{
    public class DeserializationFacilityBase : IDeserializationFacility
    {
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
    }
}
