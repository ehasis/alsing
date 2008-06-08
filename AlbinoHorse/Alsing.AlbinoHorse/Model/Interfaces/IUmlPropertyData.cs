using System.Drawing;

namespace AlbinoHorse.Model
{
    public interface IUmlTypeMemberData
    {
        string Name { get; set; }
        string SectionName { get; }
        object DataObject { get; }
        Image GetImage();
    }
}