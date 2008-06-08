using System.Collections.Generic;

namespace AlbinoHorse.Model
{
    public interface IUmlInstanceTypeData : IUmlTypeData
    {
        UmlTypeMember CreateTypeMember(string sectionName);
        void RemoveTypeMember(UmlTypeMember member);
        IList<UmlTypeMember> GetTypeMembers();
    }
}