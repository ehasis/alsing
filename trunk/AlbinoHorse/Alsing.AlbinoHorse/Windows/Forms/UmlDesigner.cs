using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AlbinoHorse.Infrastructure;
using AlbinoHorse.Model;

namespace AlbinoHorse.Windows.Forms
{
    public partial class UmlDesigner : Control
    {
        private BoundingBox currentBoundingBox;
        private Shape currentShape;
        private DrawRelation endDrawRelation;
        private Action endInputAction;
        private bool isPanning;
        private Point mouseCurrentPoint;
        private Point mouseDownAutoscrollPoint;
        private Point mouseDownPoint;
        private string originalText;
        private Shape relationEnd;
        private Shape relationStart;

        public UmlDesigner()
        {
            InitializeComponent();
            Zoom = 1;
            Diagram = new UmlDiagram();

            MainCanvas.MouseMove += MainCanvas_MouseMove;
            MainCanvas.MouseDown += MainCanvas_MouseDown;
            MainCanvas.MouseUp += MainCanvas_MouseUp;
            MainCanvas.Paint += MainCanvas_Paint;
            MainCanvas.DoubleClick += MainCanvas_DoubleClick;
            MainCanvas.CanvasScroll += MainCanvas_CanvasScroll;
            MainCanvas.KeyPress += MainCanvas_KeyPress;
            MainCanvas.KeyDown += MainCanvas_KeyDown;

            PreviewCanvas.Paint += PreviewCanvas_Paint;
            PreviewCanvas.MouseDown += PreviewCanvas_MouseDown;
            PreviewCanvas.MouseMove += PreviewCanvas_MouseMove;
            BoundingBoxes = new List<BoundingBox>();
            GridSize = 21;
            ShowGrid = true;
            SnapToGrid = true;
            EditMode = EditMode.Normal;
        }

        public int GridSize { get; set; }
        public bool ShowGrid { get; set; }
        public bool SnapToGrid { get; set; }
        public EditMode EditMode { get; set; }

        private void MainCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (EditMode == EditMode.Normal)
            {
                if (currentShape != null)
                {
                    Shape shape = currentShape;
                    var args = new ShapeKeyEventArgs();
                    args.SnapToGrid = SnapToGrid;
                    args.Sender = this;
                    args.GridSize = GridSize;
                    args.Key = e.KeyCode;

                    shape.OnKeyPress(args);

                    if (args.Redraw)
                        Refresh();
                }
            }
        }

        private void MainCanvas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (EditMode == EditMode.Normal) {}
        }

        private void SetViewPort(int x, int y)
        {
            float width = 0;
            float height = 0;
            foreach (Shape type in diagram.Shapes)
            {
                if (type is UmlInstanceType)
                {
                    width = Math.Max(type.Bounds.Right, width);
                    height = Math.Max(type.Bounds.Bottom, height);
                }
            }

            float max = Math.Max(width, height) + 700;
            float zoom = PreviewCanvas.Width/max;


            var xx = (int) ((double) x/zoom);
            var yy = (int) ((double) y/zoom);


            xx -= MainCanvas.Width/2;
            yy -= MainCanvas.Height/2;

            MainCanvas.AutoScrollPosition = new Point(xx, yy);
        }

        private void PreviewCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                SetViewPort(e.X, e.Y);
        }

        private void PreviewCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                SetViewPort(e.X, e.Y);
        }

        private void MainCanvas_CanvasScroll(object sender, EventArgs e)
        {
            EndInput();
            PreviewCanvas.Invalidate();
        }

        private void MainCanvas_DoubleClick(object sender, EventArgs e)
        {
            var x = (int) ((mouseDownPoint.X - MainCanvas.AutoScrollPosition.X)/Zoom);
            var y = (int) ((mouseDownPoint.Y - MainCanvas.AutoScrollPosition.Y)/Zoom);

            for (int i = BoundingBoxes.Count - 1; i >= 0; i--)
            {
                BoundingBox bbox = BoundingBoxes[i];
                if (bbox.Bounds.Contains(x, y))
                {
                    if (bbox.Target is Shape)
                    {
                        currentBoundingBox = bbox;
                        var shape = (Shape) bbox.Target;
                        var args = new ShapeMouseEventArgs();
                        args.BoundingBox = bbox;
                        args.X = x;
                        args.Y = y;
                        args.Button = MouseButtons.Left;
                        args.Sender = this;
                        shape.OnDoubleClick(args);
                        if (args.Redraw)
                            Refresh();
                    }

                    return;
                }
            }
        }

        public void AutoLayout()
        {
            //Diagram.AutoLayout();
            Refresh();
            Refresh();
        }

        public Rectangle GetItemBounds(object item)
        {
            foreach (BoundingBox bbox in BoundingBoxes)
            {
                if (bbox.Target == item)
                    return bbox.Bounds;

                if (bbox.Data == item)
                    return bbox.Bounds;
            }

            return Rectangle.Empty;
        }

        public int TransformToZoom(int x)
        {
            double xx = x;
            xx *= Zoom;
            return (int) xx;
        }

        public float TransformToZoom(float x)
        {
            double xx = x;
            xx *= Zoom;
            return (float) xx;
        }

        public void BeginDrawRelation(DrawRelation endDrawRelation)
        {
            this.endDrawRelation = endDrawRelation;
            EditMode = EditMode.BeginDrawRelation;
        }

        public void BeginInput(Rectangle bounds, string text, Font font, Action endInputAction)
        {
            originalText = text;
            this.endInputAction = endInputAction;
            txtInput.Visible = false;
            txtInput.Multiline = false;
            txtInput.ScrollBars = ScrollBars.None;
            int x = bounds.Left;
            int y = bounds.Top;
            int width = bounds.Width;


            x = TransformToZoom(x) - 2;
            y = TransformToZoom(y) - 2;
            width = TransformToZoom(width);

            x += MainCanvas.AutoScrollPosition.X;
            y += MainCanvas.AutoScrollPosition.Y;

            float newFontSize = TransformToZoom(font.Size);
            var newFont = new Font(font.Name, newFontSize, font.Style);


            txtInput.Left = x;
            txtInput.Top = y;
            txtInput.Width = width;
            txtInput.Height = 1;
            txtInput.Text = text;
            txtInput.Font = newFont;
            txtInput.Visible = true;
            txtInput.SelectAll();
            txtInput.Focus();
        }

        public void BeginInputMultiLine(Rectangle bounds, string text, Font font, Action endInputAction)
        {
            originalText = text;
            this.endInputAction = endInputAction;
            txtInput.Visible = false;
            txtInput.Multiline = true;
            txtInput.ScrollBars = ScrollBars.None;
            int x = bounds.Left;
            int y = bounds.Top;
            int width = bounds.Width;
            int height = bounds.Height;


            x = TransformToZoom(x);
            y = TransformToZoom(y);
            width = TransformToZoom(width);
            height = TransformToZoom(height);

            x += MainCanvas.AutoScrollPosition.X;
            y += MainCanvas.AutoScrollPosition.Y;

            float newFontSize = TransformToZoom(font.Size);
            var newFont = new Font(font.Name, newFontSize, font.Style);


            txtInput.Left = x;
            txtInput.Top = y;
            txtInput.Width = width;
            txtInput.Height = height;
            txtInput.Text = text;
            txtInput.Font = newFont;
            txtInput.Visible = true;
            txtInput.SelectAll();
            txtInput.Focus();
        }

        public void EndInput()
        {
            if (endInputAction != null)
                endInputAction();
            else
                return;

            endInputAction = null;

            Font oldFont = txtInput.Font;

            txtInput.Font = Settings.inputFont;
            txtInput.Visible = false;

            oldFont.Dispose(); // throw away the zoomed font            
            Refresh();
            MainCanvas.Focus();
        }


        private void PreviewCanvas_Paint(object sender, PaintEventArgs e)
        {
            int x = 0;
            int y = 0;
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;

            float width = 0;
            float height = 0;
            foreach (Shape type in diagram.Shapes)
            {
                if (type is UmlInstanceType)
                {
                    width = Math.Max(type.Bounds.Right, width);
                    height = Math.Max(type.Bounds.Bottom, height);
                }
            }

            float max = Math.Max(width, height) + 700;
            float zoom = PreviewCanvas.Width/max;

            var visibleBounds = new Rectangle(x, y, PreviewCanvas.ClientSize.Width, PreviewCanvas.ClientSize.Height);


            var renderInfo = new RenderInfo();
            renderInfo.Graphics = e.Graphics;
            renderInfo.Preview = true;

            renderInfo.VisualBounds = visibleBounds;
            renderInfo.GridSize = GridSize;
            e.Graphics.ScaleTransform(zoom, zoom);
            e.Graphics.TranslateTransform((-x/zoom + 50), (-y/zoom + 50));
            renderInfo.Zoom = Zoom;
            Diagram.Draw(renderInfo);

            double vpWidth = MainCanvas.Width/Zoom;
            double vpHeight = MainCanvas.Height/Zoom;

            var viewPort = new Rectangle((int) (-MainCanvas.AutoScrollPosition.X/Zoom),
                                         (int) (-MainCanvas.AutoScrollPosition.Y/Zoom), (int) (vpWidth),
                                         (int) (vpHeight));

            var viewPortBrush = new SolidBrush(Color.FromArgb(100, 200, 200, 240));
            e.Graphics.FillRectangle(viewPortBrush, viewPort);
            e.Graphics.DrawRectangle(Pens.DarkBlue, viewPort);
            viewPortBrush.Dispose();
        }


        private void MainCanvas_Paint(object sender, PaintEventArgs e)
        {
            int x = (-MainCanvas.AutoScrollPosition.X);
            int y = (-MainCanvas.AutoScrollPosition.Y);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;


            var visibleBounds = new Rectangle(x, y, ClientSize.Width, ClientSize.Height);
            var renderInfo = new RenderInfo();
            renderInfo.Graphics = e.Graphics;
            renderInfo.VisualBounds = visibleBounds;
            renderInfo.GridSize = GridSize;
            renderInfo.ShowGrid = ShowGrid;
            e.Graphics.ScaleTransform((float) Zoom, (float) Zoom);
            e.Graphics.TranslateTransform((float) (-x/zoom), (float) (-y/zoom));
            renderInfo.Zoom = Zoom;
            Diagram.Draw(renderInfo);
            BoundingBoxes = renderInfo.BoundingBoxes;

            if (EditMode == EditMode.DrawRelation)
            {
                e.Graphics.DrawLine(Settings.DrawRelation, mouseDownPoint, mouseCurrentPoint);
            }

            SetCanvasScrollSize(renderInfo);
        }

        private void SetCanvasScrollSize(RenderInfo renderInfo)
        {
            Size newSize = renderInfo.ReturnedBounds.Size;
            newSize.Height += 600;
            newSize.Width += 600;

            MainCanvas.AutoScrollMinSize = newSize;
        }

        private void MainCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            var x = (int) ((e.X - MainCanvas.AutoScrollPosition.X)/Zoom);
            var y = (int) ((e.Y - MainCanvas.AutoScrollPosition.Y)/Zoom);

            if (EditMode == EditMode.DrawRelation)
            {
                relationEnd = null;


                for (int i = BoundingBoxes.Count - 1; i >= 0; i--)
                {
                    BoundingBox bbox = BoundingBoxes[i];
                    if (bbox.Bounds.Contains(x, y))
                    {
                        if (bbox.Target is Shape)
                        {
                            relationEnd = bbox.Target as Shape;
                        }
                    }
                }

                endDrawRelation(relationStart, relationEnd);

                //end drawing
                EditMode = EditMode.Normal;
                MainCanvas.Refresh();
            }
            else if (EditMode == EditMode.Normal)
            {
                Cursor = Cursors.Default;
                currentBoundingBox = null;
                isPanning = false;
                for (int i = BoundingBoxes.Count - 1; i >= 0; i--)
                {
                    BoundingBox bbox = BoundingBoxes[i];
                    if (bbox.Bounds.Contains(x, y))
                    {
                        if (bbox.Target is Shape)
                        {
                            var shape = (Shape) bbox.Target;
                            var args = new ShapeMouseEventArgs();
                            args.BoundingBox = bbox;
                            args.X = x;
                            args.Y = y;
                            args.Button = e.Button;
                            args.Sender = this;
                            args.GridSize = GridSize;
                            args.SnapToGrid = SnapToGrid;
                            shape.OnMouseUp(args);
                            if (args.Redraw)
                                Refresh();
                        }

                        return;
                    }
                }
            }
        }

        private void MainCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            EndInput();
            var x = (int) ((e.X - MainCanvas.AutoScrollPosition.X)/Zoom);
            var y = (int) ((e.Y - MainCanvas.AutoScrollPosition.Y)/Zoom);

            if (EditMode == EditMode.BeginDrawRelation)
            {
                mouseDownPoint = new Point(e.X, e.Y);
                EditMode = EditMode.DrawRelation;
                relationStart = null;

                for (int i = BoundingBoxes.Count - 1; i >= 0; i--)
                {
                    BoundingBox bbox = BoundingBoxes[i];
                    if (bbox.Bounds.Contains(x, y))
                    {
                        if (bbox.Target is Shape)
                        {
                            relationStart = bbox.Target as Shape;
                        }
                    }
                }
            }
            else if (EditMode == EditMode.Normal)
            {
                mouseDownPoint = new Point(e.X, e.Y);
                mouseDownAutoscrollPoint = new Point(-MainCanvas.AutoScrollPosition.X, -MainCanvas.AutoScrollPosition.Y);

                for (int i = BoundingBoxes.Count - 1; i >= 0; i--)
                {
                    BoundingBox bbox = BoundingBoxes[i];
                    if (bbox.Bounds.Contains(x, y))
                    {
                        if (bbox.Target is Shape)
                        {
                            currentBoundingBox = bbox;
                            var shape = (Shape) bbox.Target;
                            currentShape = shape;
                            var args = new ShapeMouseEventArgs();
                            args.BoundingBox = bbox;
                            args.X = x;
                            args.Y = y;
                            args.Button = e.Button;
                            args.Sender = this;
                            args.GridSize = GridSize;
                            args.SnapToGrid = SnapToGrid;
                            shape.OnMouseDown(args);
                            if (args.Redraw)
                                Refresh();
                        }

                        return;
                    }
                }
                isPanning = true;
            }
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (EditMode == EditMode.DrawRelation)
            {
                mouseCurrentPoint = new Point(e.X, e.Y);
                MainCanvas.Refresh();
            }
            else if (EditMode == EditMode.Normal)
            {
                var x = (int) ((e.X - MainCanvas.AutoScrollPosition.X)/Zoom);
                var y = (int) ((e.Y - MainCanvas.AutoScrollPosition.Y)/Zoom);

                if (e.Button != MouseButtons.None)
                {
                    if (currentBoundingBox == null)
                    {
                        if (isPanning)
                        {
                            int dx = mouseDownPoint.X - e.X;
                            int dy = mouseDownPoint.Y - e.Y;

                            var newPos = new Point(mouseDownAutoscrollPoint.X + dx, mouseDownAutoscrollPoint.Y + dy);
                            MainCanvas.AutoScrollPosition = newPos;
                            Cursor = Cursors.SizeAll;
                        }
                    }
                    else
                    {
                        var shape = (Shape) currentBoundingBox.Target;
                        var args = new ShapeMouseEventArgs();
                        args.BoundingBox = currentBoundingBox;
                        args.X = x;
                        args.Y = y;
                        args.Button = e.Button;
                        args.Sender = this;
                        args.GridSize = GridSize;
                        args.SnapToGrid = SnapToGrid;
                        shape.OnMouseMove(args);
                        if (args.Redraw)
                            MainCanvas.Refresh();
                    }
                }
                else
                {
                    for (int i = BoundingBoxes.Count - 1; i >= 0; i--)
                    {
                        BoundingBox bbox = BoundingBoxes[i];
                        if (bbox.Bounds.Contains(x, y))
                        {
                            if (bbox.Target is Shape)
                            {
                                var shape = (Shape) bbox.Target;
                                var args = new ShapeMouseEventArgs();
                                args.BoundingBox = bbox;
                                args.X = x;
                                args.Y = y;
                                args.Button = e.Button;
                                args.Sender = this;
                                shape.OnMouseMove(args);
                                if (args.Redraw)
                                    MainCanvas.Refresh();
                            }

                            return;
                        }
                    }
                }
            }
        }

        public virtual string GetInput()
        {
            return txtInput.Text;
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!txtInput.Multiline)
            {
                if (e.KeyChar == '\r')
                    e.Handled = true;
            }
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (!txtInput.Multiline)
                {
                    txtInput.Text = originalText;
                    EndInput();
                }
                else
                {
                    EndInput();
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (!txtInput.Multiline)
                {
                    e.Handled = true;
                    EndInput();
                }
                else if (e.Control && txtInput.Multiline)
                {
                    e.Handled = true;
                    EndInput();
                }
            }
        }

        //break out to a selection class
        public virtual void ClearSelection()
        {
            foreach (Shape shape in Diagram.Shapes)
            {
                shape.Selected = false;
            }
        }

        #region Property Diagram 

        private UmlDiagram diagram;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public UmlDiagram Diagram
        {
            get { return diagram; }
            set { diagram = value; }
        }

        #endregion

        #region Property Zoom 

        private double zoom;

        public double Zoom
        {
            get { return zoom; }
            set
            {
                if (value < 0.000001)
                    return;

                if (value > 3)
                    return;

                zoom = value;
                Refresh();
            }
        }

        #endregion

        #region Property BoundingBoxes

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<BoundingBox> BoundingBoxes { get; set; }

        #endregion

        #region Nested type: Settings

        private static class Settings
        {
            public static readonly Pen DrawRelation = MakeDrawRelationPen();
            public static readonly Font inputFont = new Font("Arial", 10f);

            private static Pen MakeDrawRelationPen()
            {
                var pen = new Pen(Color.Gray, 3);

                pen.DashStyle = DashStyle.Dash;
                return pen;
            }
        }

        #endregion
    }
}