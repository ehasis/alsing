using System.Drawing;
using AlbinoHorse.ClassDesigner.Properties;

namespace AlbinoHorse.Model
{
    public class DefaultUmlTypeMemberData : IUmlTypeMemberData
    {
        #region IUmlTypeMemberData Members

        public string Name { get; set; }
        public string SectionName { get; set; }

        public object DataObject
        {
            get { return null; }
        }

        public Image GetImage()
        {
            return Resources.Property;
        }

        #endregion
    }
}