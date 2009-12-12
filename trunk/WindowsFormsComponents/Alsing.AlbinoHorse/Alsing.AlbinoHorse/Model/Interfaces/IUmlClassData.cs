using System.Collections.Generic;

namespace AlbinoHorse.Model
{
    public interface IUmlClassData : IUmlInstanceTypeData
    {
        string InheritsTypeName { get; set; }
        bool IsAbstract { get; set; }
        IList<string> GetImplementedInterfaces();
    }
}