using System.Collections.Generic;

namespace AlbinoHorse.Model
{
    public class DefaultUmlClassData : DefaultUmlInstanceTypeData, IUmlInstanceTypeData
    {
        public string InheritsTypeName { get; set; }
        public bool IsAbstract { get; set; }

        public IList<string> GetImplementedInterfaces()
        {
            return new List<string>();
        }
    }
}