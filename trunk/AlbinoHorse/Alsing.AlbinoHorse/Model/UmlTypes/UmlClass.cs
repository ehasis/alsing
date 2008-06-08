using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using AlbinoHorse.ClassDesigner.Properties;
using AlbinoHorse.Infrastructure;
using AlbinoHorse.Model.Settings;
using Brushes=System.Drawing.Brushes;
using Pens=AlbinoHorse.Model.Settings.Pens;

namespace AlbinoHorse.Model
{
    public class UmlClass : UmlInstanceType
    {
        #region TypedDataSource property

        private IUmlClassData TypedDataSource
        {
            get { return DataSource as IUmlClassData; }
        }

        #endregion

        #region IsAbstract property

        public bool IsAbstract
        {
            get { return TypedDataSource.IsAbstract; }
            set { TypedDataSource.IsAbstract = value; }
        }

        #endregion

        #region InheritsTypeName property

        public string InheritsTypeName
        {
            get { return TypedDataSource.InheritsTypeName; }
            set { TypedDataSource.InheritsTypeName = value; }
        }

        #endregion

        protected override IList<UmlTypeMemberSection> GetTypeMemberSections()
        {
            return new List<UmlTypeMemberSection>
                   {
                       new UmlTypeMemberSection(this, "Properties"),
                       new UmlTypeMemberSection(this, "Methods")
                   };
        }

        protected override Brush GetCaptionBrush(Rectangle renderBounds)
        {
            if (Selected)
                return new LinearGradientBrush(renderBounds, Color.FromArgb(190, 202, 230), Color.White, 0, true);
            else
                return new LinearGradientBrush(renderBounds, Color.FromArgb(210, 222, 240), Color.White, 0, true);
        }

        protected override string GetTypeKind()
        {
            if (IsAbstract)
                return "Abstract class";
            else
                return "Class";
        }

        protected override Font GetTypeNameFont()
        {
            if (IsAbstract)
                return Fonts.AbstractTypeName;
            else
                return Fonts.DefaultTypeName;
        }

        protected override Pen GetBorderPen()
        {
            if (IsAbstract)
                return Pens.AbstractBorder;
            else
                return Pens.DefaultBorder;
        }


        protected override void DrawCustomCaptionInfo(RenderInfo info, int x, int y, int width)
        {
            if (InheritsTypeName != null)
            {
                info.Graphics.DrawImage(Resources.InheritanceArrow, x + Margins.typeBoxSideMargin, y + 35);
                var typeInheritsBounds = new Rectangle(x + 24, y + 33, width - 26, 10);
                info.Graphics.DrawString(InheritsTypeName, Fonts.InheritsTypeName, Brushes.Black, typeInheritsBounds,
                                         StringFormat.GenericTypographic);
            }

            IList<string> implementedInterfaces = TypedDataSource.GetImplementedInterfaces();
            if (implementedInterfaces.Count > 0)
            {
                int offsetX = 20;
                int offsetY = 20 + (16*(implementedInterfaces.Count - 1));
                info.Graphics.DrawEllipse(Pens.Lolipop, x + offsetX, y - offsetY, 12, 12);
                info.Graphics.DrawLine(Pens.Lolipop, x + offsetX + 6, y - offsetY + 12, x + offsetX + 6, y);

                int yy = y - offsetY;
                foreach (string interfaceName in implementedInterfaces)
                {
                    info.Graphics.DrawString(interfaceName, Fonts.ImplementedInterfaces, Brushes.Black, x + offsetX + 16,
                                             yy);
                    yy += 16;
                }
            }
        }

        protected override Brush GetSectionCaptionBrush()
        {
            return Settings.Brushes.ClassSectionCaption;
        }

        protected override Font GetTypeMemberFont()
        {
            return Fonts.ClassTypeMember;
        }
    }
}