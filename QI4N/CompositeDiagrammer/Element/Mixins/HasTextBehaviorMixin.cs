namespace CompositeDiagrammer.Element.Mixins
{
    using QI4N.Framework;

    public class HasTextBehaviorMixin : HasTextBehavior
    {
        [This]
        private HasTextState state;

        public void SetText(string text)
        {
            this.state.Text = text;
        }
    }
}