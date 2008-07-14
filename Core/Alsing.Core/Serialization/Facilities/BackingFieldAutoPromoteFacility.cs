using System.Reflection;

namespace Alsing.Serialization
{
    public class BackingFieldAutoPromoteFacility : IDeserializationFacility
    {
        public void Attach(DeserializerEngine engine)
        {
            engine.FieldMissing += engine_FieldMissing;
        }

        static void engine_FieldMissing(string fieldName, object instance, object value)
        {
            const string pattern = ">k__BackingField";
            if (!fieldName.EndsWith(pattern))
                return;

            string newName = fieldName.Substring(1, fieldName.Length - pattern.Length - 1).ToLowerInvariant();

            FieldInfo field = instance.GetType().GetAnyField(newName);
            
            if (field == null)
                return;

            field.SetValue(instance,value);
        }
    }
}
