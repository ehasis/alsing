namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public class TextualMixin : Textual
    {
        [This]
        private TextualState state;

        public void SetText(string text)
        {
            this.state.Text = text;
        }
    }
}