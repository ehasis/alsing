namespace CompositeDiagrammer
{
    using QI4N.Framework;

    [Mixins(typeof(TextualMixin))]
    public interface Textual
    {
        void SetText(string text);
    }

    public interface TextualState
    {
        string Text { get; set; }
    }
}