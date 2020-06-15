namespace Alsing.Windows.Forms.SyntaxBox.Painter
{
    /// <summary>
    /// View point struct used by the SyntaxBoxControl.
    /// The struct contains information about various rendering parameters that the IPainter needs.
    /// </summary>
    public class ViewPoint
    {
        /// <summary>
        /// The action that the SyntaxBoxControl is currently performing
        /// </summary>
        public EditAction Action;

        /// <summary>
        /// Width of a char (space) in pixels
        /// </summary>
        public int CharWidth;

        /// <summary>
        /// Height of the client area in pixels
        /// </summary>
        public int ClientAreaStart;

        /// <summary>
        /// Width of the client area in pixels
        /// </summary>
        public int ClientAreaWidth;

        /// <summary>
        /// Index of the first visible column
        /// </summary>
        public int FirstVisibleColumn;

        /// <summary>
        /// Index of the first visible row
        /// </summary>
        public int FirstVisibleRow;

        /// <summary>
        /// Width of the gutter margin in pixels
        /// </summary>
        public int GutterMarginWidth;

        /// <summary>
        /// Width of the Linenumber margin in pixels
        /// </summary>
        public int LineNumberMarginWidth;

        /// <summary>
        /// Height of a row in pixels
        /// </summary>
        public int RowHeight;

        /// <summary>
        /// Width of the text margin (sum of gutter + linenumber + folding margins)
        /// </summary>
        public int TextMargin;

        /// <summary>
        ///
        /// </summary>
        public int TotalMarginWidth;

        /// <summary>
        /// Number of rows that can be displayed in the current view
        /// </summary>
        public int VisibleRowCount;

        /// <summary>
        /// Used for offsetting the screen in y axis.
        /// </summary>
        public int YOffset;
    }
}
