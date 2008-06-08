using System.Drawing;
using System.Drawing.Drawing2D;
using AlbinoHorse.ClassDesigner.Properties;
using AlbinoHorse.Infrastructure;
using AlbinoHorse.Model.Settings;
using AlbinoHorse.Windows.Forms;
using Brushes=System.Drawing.Brushes;
using Pens=System.Drawing.Pens;

namespace AlbinoHorse.Model
{
    public abstract class UmlType : UmlShape
    {
        #region Properties

        #region DataSource property

        public IUmlTypeData DataSource { get; set; }

        #endregion

        #region Identifiers

        //bounding box identifiers
        protected readonly object CaptionIdentifier = new object();

        #endregion

        #region TypeName property

        public string TypeName
        {
            get { return DataSource.TypeName; }
            set { DataSource.TypeName = value; }
        }

        #endregion

        #region Expanded property

        public bool Expanded
        {
            get { return DataSource.Expanded; }
            set { DataSource.Expanded = value; }
        }

        #endregion

        #endregion

        #region Draw

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

            using (Brush captionBrush = GetCaptionBrush(renderBounds))
            {
                if (Expanded)
                    DrawExpanded(info, path, x, y, width, height, captionBrush, borderPen);
                else
                    DrawCollapsed(info, path, x, y, width, height, captionBrush, borderPen);
            }

            DrawTypeExpander(info, x, y, width);
            DrawSelection(info);
            DrawTypeName(info, x, y, width);
            DrawTypeKind(info, x, y, width);
            DrawCustomCaptionInfo(info, x, y, width);
        }


        protected void DrawTypeExpander(RenderInfo info, int x, int y, int width)
        {
            var typeExpanderBounds = new Rectangle(x + width - 20, y + 6, 13, 13);

            #region add type expander bbox

            var bboxTypeExpander = new BoundingBox();
            bboxTypeExpander.Target = this;
            bboxTypeExpander.Bounds = typeExpanderBounds;
            bboxTypeExpander.Data = TypeExpanderIdentifier;
            info.BoundingBoxes.Add(bboxTypeExpander);

            #endregion

            if (Expanded)
                info.Graphics.DrawImage(Resources.CollapseType, typeExpanderBounds);
            else
                info.Graphics.DrawImage(Resources.ExpandType, typeExpanderBounds);
        }

        protected virtual void DrawExpanded(RenderInfo info, GraphicsPath path, int x, int y, int width, int height,
                                            Brush captionBrush, Pen borderPen)
        {
            int currentY = y + DrawExpandedCaption(info, path, x, y, width, height, captionBrush);

            currentY = DrawExpandedBody(info, x, width, currentY);

            info.Graphics.DrawPath(borderPen, path);
        }

        protected abstract int DrawExpandedBody(RenderInfo info, int x, int width, int currentY);

        protected int DrawExpandedCaption(RenderInfo info, GraphicsPath path, int x, int y, int width, int height,
                                          Brush captionBrush)
        {
            int captionHeight = 48;
            var captionBounds = new Rectangle(x, y, width, captionHeight);

            #region add caption bbox

            var bboxCaption = new BoundingBox();
            bboxCaption.Bounds = captionBounds;
            bboxCaption.Target = this;
            bboxCaption.Data = CaptionIdentifier;
            info.BoundingBoxes.Add(bboxCaption);

            #endregion

            info.Graphics.SetClip(path);
            info.Graphics.FillRectangle(captionBrush, captionBounds);
            info.Graphics.FillRectangle(Brushes.White, x, y + captionHeight, width, height - captionHeight);
            info.Graphics.DrawLine(Pens.LightGray, x, y + captionHeight, x + width, y + captionHeight);
            info.Graphics.ResetClip();
            return captionHeight;
        }

        protected void DrawCollapsed(RenderInfo info, GraphicsPath path, int x, int y, int width, int height,
                                     Brush captionBrush, Pen borderPen)
        {
            var captionBounds = new Rectangle(x, y, width, height);

            var bboxCaption = new BoundingBox();
            bboxCaption.Bounds = captionBounds;
            bboxCaption.Target = this;
            bboxCaption.Data = CaptionIdentifier;
            info.BoundingBoxes.Add(bboxCaption);

            info.Graphics.FillPath(captionBrush, path);
            info.Graphics.DrawPath(borderPen, path);
        }

        protected virtual void DrawTypeKind(RenderInfo info, int x, int y, int width)
        {
            var typeKindBounds = new Rectangle(x + Margins.typeBoxSideMargin, y + 4 + 15,
                                               width - Margins.typeBoxSideMargin*2, 10);
            string kind = GetTypeKind();
            info.Graphics.DrawString(kind, Fonts.TypeKind, Brushes.Black, typeKindBounds,
                                     StringFormat.GenericTypographic);
        }

        protected virtual void DrawTypeName(RenderInfo info, int x, int y, int width)
        {
            var typeNameBounds = new Rectangle(x + Margins.typeBoxSideMargin, y + 4, width - Margins.typeBoxSideMargin*2,
                                               10);
            Font typeNameFont = GetTypeNameFont();
            info.Graphics.DrawString(TypeName, typeNameFont, Brushes.Black, typeNameBounds,
                                     StringFormat.GenericTypographic);
        }

        protected virtual void DrawCustomCaptionInfo(RenderInfo info, int x, int y, int width) {}

        #endregion

        protected abstract Brush GetCaptionBrush(Rectangle renderBounds);
        protected abstract string GetTypeKind();

        protected virtual Font GetTypeNameFont()
        {
            return Fonts.DefaultTypeName;
        }


        protected void BeginRenameType(UmlDesigner owner)
        {
            var inputBounds = new Rectangle(Bounds.Left + Margins.typeBoxSideMargin, Bounds.Top + 4,
                                            Bounds.Width - 25 - Margins.typeBoxSideMargin, 20);

            Action endRenameType = () => { DataSource.TypeName = owner.GetInput(); };

            owner.BeginInput(inputBounds, DataSource.TypeName, Fonts.DefaultTypeName, endRenameType);
        }
    }
}