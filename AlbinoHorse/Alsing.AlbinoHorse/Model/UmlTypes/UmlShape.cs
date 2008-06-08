using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using AlbinoHorse.Infrastructure;
using Brushes=AlbinoHorse.Model.Settings.Brushes;
using Pens=AlbinoHorse.Model.Settings.Pens;

namespace AlbinoHorse.Model
{
    public class UmlShape : Shape
    {
        protected Point mouseDownPos;
        protected Point mouseDownShapePos;

        #region Properties

        #region SelectedObject property

        private object selectedObject;

        public object SelectedObject
        {
            get { return selectedObject; }

            set
            {
                selectedObject = value;
                OnSelectedObjectChanged(EventArgs.Empty);
            }
        }

        #endregion

        #endregion

        #region Identifiers

        //bounding box identifiers

        protected readonly object BodyIdentifier = new object();
        protected readonly object BottomLeftResizeIdentifier = new object();
        protected readonly object BottomResizeIdentifier = new object();
        protected readonly object BottomRightResizeIdentifier = new object();
        protected readonly object LeftResizeIdentifier = new object();
        protected readonly object RightResizeIdentifier = new object();

        protected readonly object TopLeftResizeIdentifier = new object();
        protected readonly object TopResizeIdentifier = new object();
        protected readonly object TopRightResizeIdentifier = new object();
        protected readonly object TypeExpanderIdentifier = new object();

        #endregion

        #region Draw

        public override void DrawPreview(RenderInfo info)
        {
            info.Graphics.FillRectangle(System.Drawing.Brushes.White, Bounds);
            info.Graphics.DrawRectangle(System.Drawing.Pens.Black, Bounds);
        }

        protected virtual void DrawSelection(RenderInfo info)
        {
            if (Selected && SelectedObject == null)
            {
                Rectangle outerBounds = Bounds;
                outerBounds.Inflate(6, 6);
                outerBounds.Offset(1, 0);
                Rectangle innerBounds = outerBounds;
                innerBounds.Inflate(-3, -3);

                info.Graphics.SetClip(outerBounds, CombineMode.Replace);
                info.Graphics.SetClip(innerBounds, CombineMode.Xor);
                info.Graphics.FillRectangle(Brushes.Selection, outerBounds);
                info.Graphics.ResetClip();
                //info.Graphics.DrawRectangle(Settings.Pens.SelectionInner, outerBounds);
                //outerBounds.Offset(-1, 1);
                //info.Graphics.DrawRectangle(Settings.Pens.SelectionOuter, outerBounds);


                DrawSelectionHandle(info, new Point(outerBounds.Left, (outerBounds.Top + outerBounds.Bottom)/2),
                                    LeftResizeIdentifier);
                DrawSelectionHandle(info, new Point(outerBounds.Right, (outerBounds.Top + outerBounds.Bottom)/2),
                                    RightResizeIdentifier);

                DrawCustomSelection(info);
            }
        }


        protected virtual void DrawCustomSelection(RenderInfo info) {}

        public override void DrawBackground(RenderInfo info)
        {
            int grid = info.GridSize;
            Rectangle renderBounds = Bounds;

            int x = renderBounds.X + 4;
            int y = renderBounds.Y + 3;
            int width = renderBounds.Width;
            int height = renderBounds.Height;

            GraphicsPath shadowPath = GetOutlinePath(x, y, width, height);

            try
            {
                info.Graphics.FillPath(Brushes.Shadow, shadowPath);
            }
            catch {}
        }

        #endregion

        protected virtual void OnSelectedObjectChanged(EventArgs eventArgs) {}

        protected virtual int GetRadius()
        {
            return 16;
        }

        protected GraphicsPath GetOutlinePath(int x, int y, int width, int height)
        {
            int radius = GetRadius();

            var path = new GraphicsPath();
            path.AddLine(x + radius, y, x + width - radius, y);
            path.AddArc(x + width - radius, y, radius, radius, 270, 90);
            path.AddLine(x + width, y + radius, x + width, y + height - radius);
            path.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
            path.AddLine(x + width - radius, y + height, x + radius, y + height);
            path.AddArc(x, y + height - radius, radius, radius, 90, 90);
            path.AddLine(x, y + height - radius, x, y + radius);
            path.AddArc(x, y, radius, radius, 180, 90);

            path.CloseFigure();

            return path;
        }

        protected virtual Pen GetBorderPen()
        {
            return Pens.DefaultBorder;
        }
    }
}