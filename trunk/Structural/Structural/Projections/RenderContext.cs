namespace Structural.Projections
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using AST;

    public class RenderContext
    {
        public Dictionary<Type, object> projections = new Dictionary<Type, object>();

        private readonly Stack<ContextData> data = new Stack<ContextData>();

        public RenderContext()
        {
            this.projections.Add(typeof(Body), new BodyProjection());
            this.projections.Add(typeof(EmptyValue), new EmptyValueProjection());
            this.projections.Add(typeof(Expression), new ExpressionProjection());
            this.projections.Add(typeof(If), new IfProjection());
            this.projections.Add(typeof(IntegerLiteral), new IntegerLiteralProjection());
            this.projections.Add(typeof(AddOperator), new AddOperatorProjection());
            this.projections.Add(typeof(SubOperator), new SubOperatorProjection());
            this.projections.Add(typeof(MulOperator), new MulOperatorProjection());
            this.projections.Add(typeof(DivOperator), new DivOperatorProjection());
            this.projections.Add(typeof(Print), new PrintProjection());
            this.projections.Add(typeof(Root), new RootProjection());
            this.projections.Add(typeof(StringLiteral), new StringLiteralProjection());
            this.projections.Add(typeof(VariableDeclaration), new VariableDeclarationProjection());

            this.data.Push(new ContextData());
        }

        public Graphics Graphics { get; set; }

        private ContextData Current
        {
            get
            {
                return this.data.Peek();
            }
        }

        public void Dedent()
        {
            ContextData prev = this.Current;
            this.data.Pop();
            this.Current.Bottom = Math.Max(prev.Bottom, this.Current.Bottom);
            this.Current.Right = Math.Max(prev.Right, this.Current.Right);
        }

        public void Indent(int p)
        {
            var newData = new ContextData
                              {
                                      MinX = (this.Current.X + p),
                                      X = (this.Current.X + p),
                                      Y = this.Current.Y,
                                      Bottom = this.Current.Bottom,
                                      Right = this.Current.Right
                              };

            this.data.Push(newData);
        }

        public void NewLine()
        {
            this.Current.X = this.Current.MinX;
            this.Current.Y = this.Current.Bottom;
        }

        private static readonly Pen border = new Pen(Brushes.LightGray, 1)
        {
            Alignment = PenAlignment.Inset,
            DashStyle = DashStyle.Dash,
        };
        public void RenderItem<T>(T item) where T : AstNode
        {
            Type type = item.GetType();

            int x = this.Current.X;
            int y = this.Current.Y;

            var projection = this.projections[type] as Projection;
            if (projection != null)
            {
                projection.Render(item, this);
            }

            this.Graphics.DrawRectangle(border, x, y, this.Current.Right - x, this.Current.Bottom - y);
        }

        public void RenderString(string text)
        {
            this.RenderString(text, Color.Black);
        }

        public void RenderEmpty(string text,Color foreColor)
        {
            int x = this.Current.X;
            int y = this.Current.Y;

            this.RenderString(text,foreColor);

            this.Graphics.DrawRectangle(border, x, y, this.Current.Right - x, this.Current.Bottom - y);

        }

        public void RenderString(string text, Color foreColor)
        {
            using (var fgBrush = new SolidBrush(foreColor))
            {
                this.Graphics.DrawString(text, SystemFonts.CaptionFont, fgBrush, this.Current.X, this.Current.Y,StringFormat.GenericDefault);
                SizeF s = this.Graphics.MeasureString(text, SystemFonts.CaptionFont, 1000, StringFormat.GenericDefault);

                int maybeBottom = this.Current.Y + (int)s.Height;
                if (this.Current.Bottom < maybeBottom)
                {
                    this.Current.Bottom = maybeBottom;
                }

                int maybeRight = this.Current.X + (int)s.Width;

                if (this.Current.Right < maybeRight)
                {
                    this.Current.Right = maybeRight;
                }

                this.Current.X += (int)s.Width;
            }
        }
    }

    public class ContextData
    {
        public int Bottom { get; set; }

        public int MinX { get; set; }

        public int Right { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}