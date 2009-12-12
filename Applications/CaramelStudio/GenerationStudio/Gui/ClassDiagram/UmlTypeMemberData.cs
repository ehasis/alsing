using System.Collections.Generic;
using System.Drawing;
using AlbinoHorse.Model;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public class UmlTypeMemberData : IUmlTypeMemberData
    {
        private static readonly Dictionary<string, Image> imageLookup = new Dictionary<string, Image>();
        public TypeMemberElement Owner { get; set; }

        #region IUmlTypeMemberData Members

        public string Name
        {
            get { return Owner.Name; }
            set { Owner.Name = value; }
        }

        public string SectionName
        {
            get
            {
                if (Owner is PropertyElement)
                    return "Properties";

                if (Owner is MethodElement)
                    return "Methods";

                if (Owner is EnumValueElement)
                    return "Values";

                return "";
            }
        }

        public Image GetImage()
        {
            string iconKey = Owner.GetIconKey();

            Image res = null;
            if (!imageLookup.TryGetValue(iconKey, out res))
            {
                res = Owner.GetIcon();
                imageLookup.Add(iconKey, res);
            }

            return res;
        }

        public object DataObject
        {
            get { return Owner; }
        }

        #endregion
    }
}