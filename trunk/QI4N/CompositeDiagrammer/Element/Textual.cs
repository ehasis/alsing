namespace CompositeDiagrammer.Element
{
    using QI4N.Framework;

    public interface Textual :  TextualBehavior
    {
    }

    public interface TextualState
    {
        string Text { get; set; }
    }

    [Mixins(typeof(TextualBehaviorMixin))]
    public interface TextualBehavior
    {
        void SetText(string text);
    }
}