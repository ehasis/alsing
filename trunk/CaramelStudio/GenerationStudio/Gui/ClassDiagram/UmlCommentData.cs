using AlbinoHorse.Model;
using GenerationStudio.Elements;

namespace GenerationStudio.Gui
{
    public class UmlCommentData : IUmlCommentData
    {
        public DiagramCommentElement Owner { get; set; }

        #region IUmlCommentData Members

        public int X
        {
            get { return Owner.X; }
            set { Owner.X = value; }
        }

        public int Y
        {
            get { return Owner.Y; }
            set { Owner.Y = value; }
        }

        public int Width
        {
            get { return Owner.Width; }
            set { Owner.Width = value; }
        }

        public int Height
        {
            get { return Owner.Height; }
            set { Owner.Height = value; }
        }

        public string Text
        {
            get { return Owner.Text; }
            set { Owner.Text = value; }
        }

        #endregion
    }
}