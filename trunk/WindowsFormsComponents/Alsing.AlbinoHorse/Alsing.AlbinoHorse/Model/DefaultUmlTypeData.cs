using System.Collections.Generic;

namespace AlbinoHorse.Model
{
    public class DefaultUmlInstanceTypeData : IUmlInstanceTypeData
    {
        private readonly List<UmlTypeMember> members = new List<UmlTypeMember>();

        #region IUmlInstanceTypeData Members

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public bool Expanded { get; set; }
        public string TypeName { get; set; }

        public void RemoveTypeMember(UmlTypeMember property)
        {
            members.Remove(property);
        }

        public IList<UmlTypeMember> GetTypeMembers()
        {
            return members;
        }

        public UmlTypeMember CreateTypeMember(string sectionName)
        {
            var member = new UmlTypeMember();
            var data = new DefaultUmlTypeMemberData();
            data.SectionName = sectionName;
            member.DataSource = data;
            members.Add(member);
            return member;
        }

        #endregion
    }
}