using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alsing.Serialization
{
    public class BackingFieldAutoPromoteFacility
    {
        public void Attach(DeserializerEngine engine)
        {
            engine.FieldMissing += engine_FieldMissing;
        }

        static void engine_FieldMissing(string fieldName, object instance, object value)
        {
            string pattern = ">__BackingField";
            if (!fieldName.EndsWith(pattern))
                return;

            string newName = fieldName.Substring(1, fieldName.Length - pattern.Length - 1).ToLowerInvariant();

            

        }
    }
}
