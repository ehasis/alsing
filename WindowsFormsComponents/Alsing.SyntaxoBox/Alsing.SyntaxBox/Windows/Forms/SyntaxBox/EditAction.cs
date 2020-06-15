namespace Alsing.Windows.Forms.SyntaxBox
{
    /// <summary>
    /// Text actions that can be performed by the SyntaxBoxControl
    /// </summary>
    public enum EditAction
    {
        /// <summary>
        /// The control is not performing any action
        /// </summary>
        None = 0,

        /// <summary>
        /// The control is in Drag Drop mode
        /// </summary>
        DragText = 1,

        /// <summary>
        /// The control is selecting text
        /// </summary>
        SelectText = 2
    }
}
