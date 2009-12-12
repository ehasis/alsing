using System.Collections.Generic;
using System.Drawing;
using AlbinoHorse.Infrastructure;

namespace AlbinoHorse.Model
{
    public class UmlDiagram
    {
        public UmlDiagram()
        {
            DataSource = new DefaultUmlDiagramData();
        }

        #region Property Shapes

        public IList<Shape> Shapes
        {
            get { return DataSource.GetShapes(); }
        }

        #endregion

        public IUmlDiagramData DataSource { get; set; }

        public virtual void Draw(RenderInfo info)
        {
            if (!info.Preview && info.ShowGrid)
            {
                int xo = info.VisualBounds.X%info.GridSize;
                int yo = info.VisualBounds.Y%info.GridSize;

                for (int y = info.VisualBounds.Y - yo;
                     y < (info.VisualBounds.Bottom + info.GridSize)/info.Zoom;
                     y += info.GridSize)
                {
                    for (int x = info.VisualBounds.X - xo;
                         x < (info.VisualBounds.Right + info.GridSize)/info.Zoom;
                         x += info.GridSize)
                    {
                        info.Graphics.FillRectangle(Brushes.Gray, x, y, 1, 1);
                    }
                }
            }

            int maxWidth = int.MinValue;
            int maxHeight = int.MinValue;
            int minWidth = int.MaxValue;
            int minHeight = int.MaxValue;

            foreach (Shape shape in Shapes)
            {
                if (info.Preview)
                    shape.PreviewDrawBackground(info);
                else
                    shape.DrawBackground(info);
            }

            foreach (Shape shape in Shapes)
            {
                if (info.Preview)
                    shape.DrawPreview(info);
                else
                    shape.Draw(info);


                if (shape.Bounds.Left*info.Zoom < minWidth)
                    minWidth = (int) (shape.Bounds.Left*info.Zoom);

                if (shape.Bounds.Top*info.Zoom < minHeight)
                    minHeight = (int) (shape.Bounds.Top*info.Zoom);

                if (shape.Bounds.Right*info.Zoom > maxWidth)
                    maxWidth = (int) (shape.Bounds.Right*info.Zoom);

                if (shape.Bounds.Bottom*info.Zoom > maxHeight)
                    maxHeight = (int) (shape.Bounds.Bottom*info.Zoom);
            }

            maxWidth += (int) (info.GridSize*info.Zoom);
            maxHeight += (int) (info.GridSize*info.Zoom);

            info.ReturnedBounds = new Rectangle(new Point(minWidth, minHeight),
                                                new Size(maxWidth - minWidth, maxHeight - minHeight));
        }
    }
}