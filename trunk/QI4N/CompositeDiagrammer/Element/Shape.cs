namespace CompositeDiagrammer
{
    using System.Drawing.Drawing2D;

    public interface Shape
    {
        GraphicsPath GetPath();
    }
}