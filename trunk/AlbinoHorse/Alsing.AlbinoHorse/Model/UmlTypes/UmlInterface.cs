using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using AlbinoHorse.Model.Settings;
using Brushes=AlbinoHorse.Model.Settings.Brushes;
using Pens=AlbinoHorse.Model.Settings.Pens;

namespace AlbinoHorse.Model
{
    public class UmlInterface : UmlInstanceType
    {
        #region TypedDataSource property

        private IUmlInterfaceData TypedDataSource
        {
            get { return DataSource as IUmlInterfaceData; }
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
                return new LinearGradientBrush(renderBounds, Color.FromArgb(220, 230, 209), Color.White, 0, true);
            else
                return new LinearGradientBrush(renderBounds, Color.FromArgb(230, 240, 219), Color.White, 0, true);
        }

        protected override string GetTypeKind()
        {
            return "Interface";
        }

        protected override Font GetTypeNameFont()
        {
            return Fonts.DefaultTypeName;
        }

        protected override Pen GetBorderPen()
        {
            return Pens.DefaultBorder;
        }

        protected override Brush GetSectionCaptionBrush()
        {
            return Brushes.InterfaceSectionCaption;
        }

        protected override Font GetTypeMemberFont()
        {
            return Fonts.InterfaceTypeMember;
        }
    }
}