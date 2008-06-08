using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AlbinoHorse.Infrastructure;
using AlbinoHorse.Model.Settings;
using AlbinoHorse.Windows.Forms;
using Brushes=System.Drawing.Brushes;
using Pens=AlbinoHorse.Model.Settings.Pens;

namespace AlbinoHorse.Model
{
    public class UmlComment : UmlShape
    {
        #region Properties

        #region DataSource property

        public IUmlCommentData DataSource { get; set; }

        #endregion

        #region Bounds property

        public override Rectangle Bounds
        {
            get { return new Rectangle(DataSource.X, DataSource.Y, DataSource.Width, DataSource.Height); }
            set
            {
                DataSource.X = value.X;
                DataSource.Y = value.Y;
                DataSource.Width = value.Width;
                DataSource.Height = value.Height;

                base.Bounds = value;
            }
        }

        #endregion

        #region Text property

        public string Text
        {
            get { return DataSource.Text; }
            set { DataSource.Text = value; }
        }

        #endregion

        #endregion

        #region Identifiers

        //bounding box identifiers
        protected readonly object TextIdentifier = new object();

        #endregion

        public override void Draw(RenderInfo info)
        {
            int grid = info.GridSize;
            Rectangle renderBounds = Bounds;

            var bboxThis = new BoundingBox();
            bboxThis.Bounds = renderBounds;
            bboxThis.Target = this;
            bboxThis.Data = BodyIdentifier;
            info.BoundingBoxes.Add(bboxThis);

            int x = renderBounds.X;
            int y = renderBounds.Y;
            int width = renderBounds.Width;
            int height = renderBounds.Height;

            GraphicsPath path = GetOutlinePath(x, y, width, height);
            Pen borderPen = GetBorderPen();

            info.Graphics.FillPath(SystemBrushes.Info, path);
            info.Graphics.DrawPath(borderPen, path);

            renderBounds = DrawText(info, renderBounds);

            DrawSelection(info);
        }

        protected virtual Rectangle DrawText(RenderInfo info, Rectangle renderBounds)
        {
            Rectangle textBounds = renderBounds;
            textBounds.Inflate(-10, -10);

            var bboxText = new BoundingBox();
            bboxText.Bounds = textBounds;
            bboxText.Target = this;
            bboxText.Data = TextIdentifier;
            info.BoundingBoxes.Add(bboxText);


            var textBoundsF = new RectangleF(textBounds.X, textBounds.Y, textBounds.Width, textBounds.Height);

            //info.Graphics.FillRectangle(Brushes.White, textBounds);
            info.Graphics.DrawString(Text, Fonts.CommentText, Brushes.Black, textBoundsF,
                                     StringFormat.GenericTypographic);
            return renderBounds;
        }

        public override void DrawBackground(RenderInfo info) {}

        protected override void DrawCustomSelection(RenderInfo info)
        {
            Rectangle renderBounds = Bounds;

            Rectangle textBounds = renderBounds;
            textBounds.Inflate(-10, -10);

            info.Graphics.DrawRectangle(Pens.SelectionOuter, textBounds);
        }

        protected override int GetRadius()
        {
            return 5;
        }

        protected override Pen GetBorderPen()
        {
            return Pens.CommentBorder;
        }

        #region Mouse Events

        public override void OnMouseDown(ShapeMouseEventArgs args)
        {
            args.Sender.ClearSelection();
            Selected = true;

            if (args.BoundingBox.Data == RightResizeIdentifier)
            {
                mouseDownPos = new Point(args.X, args.Y);
                SelectedObject = null;
                args.Redraw = true;
            }
            else if (args.BoundingBox.Data == LeftResizeIdentifier)
            {
                mouseDownPos = new Point(args.X, args.Y);
                SelectedObject = null;
                args.Redraw = true;
            }
            else
            {
                mouseDownPos = new Point(args.X, args.Y);
                mouseDownShapePos = Bounds.Location;
                SelectedObject = null;

                args.Redraw = true;
            }
        }

        public override void OnMouseUp(ShapeMouseEventArgs args)
        {
            args.Redraw = true;
        }

        public override void OnMouseMove(ShapeMouseEventArgs args)
        {
            if (args.BoundingBox.Data == RightResizeIdentifier && args.Button == MouseButtons.Left)
            {
                int diff = args.X - Bounds.Left;
                if (diff < 100)
                    diff = 100;

                Bounds = new Rectangle(Bounds.X, Bounds.Y, diff, Bounds.Height);
                args.Redraw = true;
            }

            if (args.BoundingBox.Data == LeftResizeIdentifier && args.Button == MouseButtons.Left)
            {
                int diff = Bounds.Right - args.X;
                if (diff < 100)
                    diff = 100;

                if (diff + args.X > Bounds.Right)
                {
                    Bounds = new Rectangle(Bounds.Right - 100, Bounds.Y, 100, Bounds.Height);
                    args.Redraw = true;
                }
                else
                {
                    Bounds = new Rectangle(args.X, Bounds.Y, diff, Bounds.Height);
                    args.Redraw = true;
                }
            }

            if ((args.BoundingBox.Data == BodyIdentifier || args.BoundingBox.Data == TextIdentifier) &&
                args.Button == MouseButtons.Left)
            {
                int dx = args.X - mouseDownPos.X;
                int dy = args.Y - mouseDownPos.Y;

                int shapeX = mouseDownShapePos.X + dx;
                int shapeY = mouseDownShapePos.Y + dy;

                //if (args.SnapToGrid)
                //{
                //    shapeX = shapeX - shapeX % args.GridSize;
                //    shapeY = shapeY - shapeY % args.GridSize;
                //}

                if (shapeX < 0)
                    shapeX = 0;

                if (shapeY < 0)
                    shapeY = 0;

                Bounds = new Rectangle(shapeX, shapeY, Bounds.Width, Bounds.Height);
                args.Redraw = true;
            }
        }

        public override void OnDoubleClick(ShapeMouseEventArgs args)
        {
            if (args.BoundingBox.Data == TextIdentifier)
            {
                BeginEditText(args.Sender);
            }
        }

        private void BeginEditText(UmlDesigner owner)
        {
            Rectangle inputBounds = Bounds;
            inputBounds.Inflate(-10, -10);

            Action endEditText = () => { DataSource.Text = owner.GetInput(); };

            owner.BeginInputMultiLine(inputBounds, DataSource.Text, Fonts.CommentText, endEditText);
        }

        #endregion
    }
}