using System;
using System.Collections.Generic;
using GenerationStudio.Attributes;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementParent(typeof (NamespaceElement))]
    [AllowMultiple(false)]
    [ElementName("Namespace/DataBase Mapping")]
    [ElementIcon("GenerationStudio.Images.mapping.gif")]
    public class NamespaceDbMappingElement : Element
    {
        public DataBaseElement MappedDataBase { get; set; }

        public override string GetDisplayName()
        {
            string dataBaseName = "*missing*";
            if (MappedDataBase != null)
                dataBaseName = MappedDataBase.Name;

            string namespaceName = "*missing*";
            if (Parent != null)
                namespaceName = Parent.GetDisplayName();

            return string.Format("Mapping: {0} <-> {1}", namespaceName, dataBaseName);
        }

        public override IList<ElementError> GetErrors()
        {
            var errors = new List<ElementError>();
            if (MappedDataBase == null)
                errors.Add(new ElementError(this,
                                            string.Format("Namespace {0} is missing database mapping",
                                                          Parent.GetDisplayName())));

            return errors;
        }

        public override int GetSortPriority()
        {
            return -1;
        }
    }
}