using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using AlbinoHorse.Model.Settings;
using Brushes=AlbinoHorse.Model.Settings.Brushes;
using Pens=AlbinoHorse.Model.Settings.Pens;

namespace AlbinoHorse.Model
{
    public class UmlEnum : UmlInstanceType
    {
        #region TypedDataSource property

        private IUmlClassData TypedDataSource
        {
            get { return DataSource as IUmlClassData; }
        }

        #endregion

        protected override IList<UmlTypeMemberSection> GetTypeMemberSections()
        {
            return new List<UmlTypeMemberSection>
                   {
                       new UmlTypeMemberSection(this, "Values")
                   };
        }

        protected override Brush GetCaptionBrush(Rectangle renderBounds)
        {
            if (Selected)
                return new LinearGradientBrush(renderBounds, Color.FromArgb(211, 204, 229), Color.White, 0, true);
            else
                return new LinearGradientBrush(renderBounds, Color.FromArgb(221, 214, 239), Color.White, 0, true);
        }

        protected override Brush GetSectionCaptionBrush()
        {
            return Brushes.EnumSectionCaption;
        }

        protected override string GetTypeKind()
        {
            return "Enum";
        }

        protected override Font GetTypeNameFont()
        {
            return Fonts.DefaultTypeName;
        }

        protected override Pen GetBorderPen()
        {
            return Pens.DefaultBorder;
        }

        protected override int GetRadius()
        {
            return 1;
        }

        protected override Font GetTypeMemberFont()
        {
            return Fonts.ClassTypeMember;
        }
    }
}