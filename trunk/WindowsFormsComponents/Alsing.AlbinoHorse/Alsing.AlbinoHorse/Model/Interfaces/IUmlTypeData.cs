namespace AlbinoHorse.Model
{
    public interface IUmlTypeData
    {
        string TypeName { get; set; }
        int X { get; set; }
        int Y { get; set; }
        int Width { get; set; }
        bool Expanded { get; set; }
    }
}