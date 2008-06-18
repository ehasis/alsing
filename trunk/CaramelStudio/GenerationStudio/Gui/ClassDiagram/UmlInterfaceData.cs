using System.Collections.Generic;
using System.Linq;
using AlbinoHorse.Model;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public class UmlInterfaceData : IUmlInterfaceData
    {
        private readonly Dictionary<TypeMemberElement, UmlTypeMember> typeMemberLookup =
            new Dictionary<TypeMemberElement, UmlTypeMember>();

        public DiagramTypeElement Owner { get; set; }

        #region IUmlInterfaceData Members

        public bool Expanded
        {
            get { return Owner.Expanded; }
            set { Owner.Expanded = value; }
        }

        public int X
        {
            get { return Owner.X; }
            set { Owner.X = value; }
        }

        public int Y
        {
            get { return Owner.Y; }
            set { Owner.Y = value; }
        }

        public int Width
        {
            get { return Owner.Width; }
            set { Owner.Width = value; }
        }

        public string TypeName
        {
            get { return Owner.Type.Name; }
            set { Owner.Type.Name = value; }
        }

        public void RemoveTypeMember(UmlTypeMember property)
        {
            var pe = (TypeMemberElement) property.DataSource.DataObject;
            pe.Parent.RemoveChild(pe);
            typeMemberLookup.Remove(pe);
        }

        public IList<UmlTypeMember> GetTypeMembers()
        {
            IOrderedEnumerable<Element> res = GetValidProperties();

            var members = new List<UmlTypeMember>();
            foreach (TypeMemberElement pe in res)
                members.Add(GetTypeMember(pe));

            return members;
        }

        public UmlTypeMember CreateTypeMember(string sectionName)
        {
            var property = new UmlTypeMember();
            var data = new UmlTypeMemberData();
            if (sectionName == "Properties")
            {
                var pe = new PropertyElement();
                pe.Type = "string";
                pe.Name = "";
                data.Owner = pe;
                property.DataSource = data;

                typeMemberLookup.Add(pe, property);
                Owner.Type.AddChild(pe);
            }

            if (sectionName == "Methods")
            {
                var pe = new MethodElement();
                pe.Name = "";
                data.Owner = pe;
                property.DataSource = data;

                typeMemberLookup.Add(pe, property);
                Owner.Type.AddChild(pe);
            }

            return property;
        }

        #endregion

        private IOrderedEnumerable<Element> GetValidProperties()
        {
            IOrderedEnumerable<Element> res = from e in Owner.Type.AllChildren
                                              where !e.Excluded && (e is PropertyElement || e is MethodElement)
                                              orderby e.GetDisplayName()
                                              select e;
            return res;
        }

        private UmlTypeMember GetTypeMember(TypeMemberElement pe)
        {
            UmlTypeMember typeMember = null;
            if (typeMemberLookup.TryGetValue(pe, out typeMember))
            {
                return typeMember;
            }

            typeMember = new UmlTypeMember();
            var data = new UmlTypeMemberData();
            data.Owner = pe;
            typeMember.DataSource = data;

            typeMemberLookup.Add(pe, typeMember);

            return typeMember;
        }
    }
}