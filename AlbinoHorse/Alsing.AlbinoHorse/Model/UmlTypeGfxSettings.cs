using System.Drawing;
using System.Drawing.Drawing2D;

namespace AlbinoHorse.Model.Settings
{

    #region GFX Settings

    public static class Margins
    {
        public const int typeBoxSideMargin = 10;
        public const int TypeMemberNameIndent = 30;
    }

    public static class Brushes
    {
        public static SolidBrush ClassSectionCaption = new SolidBrush(Color.FromArgb(240, 242, 249));
        public static SolidBrush EnumSectionCaption = new SolidBrush(Color.FromArgb(237, 233, 246));
        public static SolidBrush InterfaceSectionCaption = new SolidBrush(Color.FromArgb(243, 247, 240));
        public static SolidBrush SelectedTypeMember = new SolidBrush(SystemColors.Highlight);
        public static HatchBrush Selection = new HatchBrush(HatchStyle.Percent50, Color.Black, Color.WhiteSmoke);
        public static SolidBrush Shadow = new SolidBrush(Color.LightGray);
    }

    public static class Fonts
    {
        private const string fontName = "Tahoma";
        public static Font AbstractTypeName = new Font(fontName, 8f, FontStyle.Bold | FontStyle.Italic);
        public static Font ClassTypeMember = new Font(fontName, 8f, FontStyle.Regular);

        public static Font CommentText = new Font(fontName, 8f, FontStyle.Regular);
        public static Font DefaultTypeName = new Font(fontName, 8f, FontStyle.Bold);
        public static Font ImplementedInterfaces = new Font(fontName, 8f, FontStyle.Regular);
        public static Font InheritsTypeName = new Font(fontName, 7f, FontStyle.Regular);
        public static Font InterfaceTypeMember = new Font(fontName, 8f, FontStyle.Italic);
        public static Font NewTypeMember = new Font(fontName, 8f, FontStyle.Underline);
        public static Font SectionCaption = new Font(fontName, 8f, FontStyle.Regular);
        public static Font TypeKind = new Font(fontName, 7f, FontStyle.Regular);
    }


    public static class Pens
    {
        public static Pen AbstractBorder = MakeAbstractBorderPen();
        public static Pen AssociationBorder = MakeAssociationBorder();
        public static Pen AssociationLine = MakeAssociationLine();
        public static Pen CommentBorder = new Pen(Color.Gold, 1f);
        public static Pen DefaultBorder = new Pen(Color.FromArgb(130, 130, 130), 1f);
        public static Pen FakeLine = new Pen(Color.FromArgb(130, 130, 130), 1.6f);
        public static Pen InheritanceLine = new Pen(Color.FromArgb(130, 130, 130), 1.6f);
        public static Pen Lolipop = new Pen(Color.FromArgb(130, 130, 130), 1.6f);
        public static Pen SelectionInner = new Pen(Color.FromArgb(220, 220, 220), 1);
        public static Pen SelectionOuter = MakeSelectonPen();

        private static Pen MakeAssociationLine()
        {
            var pen = new Pen(Color.Goldenrod, 1.6f);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            return pen;
        }

        private static Pen MakeAssociationBorder()
        {
            var pen = new Pen(Color.White, 6f);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            return pen;
        }


        private static Pen MakeSelectonPen()
        {
            var pen = new Pen(Color.Gray, 1);
            pen.DashStyle = DashStyle.Dash;
            return pen;
        }

        private static Pen MakeAbstractBorderPen()
        {
            var pen = new Pen(Color.FromArgb(130, 130, 130), 1.3f);
            pen.DashStyle = DashStyle.Dash;
            pen.Alignment = PenAlignment.Center;
            return pen;
        }
    }

    #endregion
}