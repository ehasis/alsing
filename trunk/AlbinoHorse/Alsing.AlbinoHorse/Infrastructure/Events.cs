using AlbinoHorse.Model;

namespace AlbinoHorse.Infrastructure
{
    public delegate void Action();

    public delegate void DrawRelation(Shape start, Shape end);

    public delegate void ShapeCancelHandler(object sender, ShapeCancelEventArgs args);
}