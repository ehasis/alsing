namespace CompositeDiagrammer.Element
{
    using QI4N.Framework;

    public class TextualBehaviorMixin : TextualBehavior
    {
        [This]
        private TextualState state;

        public void SetText(string text)
        {
            this.state.Text = text;
        }
    }
}