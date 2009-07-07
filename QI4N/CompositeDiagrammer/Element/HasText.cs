namespace CompositeDiagrammer.Element
{
    public interface HasText :  HasTextBehavior
    {
    }

    public interface HasTextState
    {
        string Text { get; set; }
    }

    public interface HasTextBehavior
    {
        void SetText(string text);
    }
}