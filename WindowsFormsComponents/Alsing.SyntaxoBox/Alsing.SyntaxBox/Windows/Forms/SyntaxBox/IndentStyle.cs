namespace Alsing.Windows.Forms.SyntaxBox
{
    /// <summary>
    /// Indent styles used by the control
    /// </summary>
    public enum IndentStyle
    {
        /// <summary>
        /// Caret is always confined to the first column when a new line is inserted
        /// </summary>
        None = 0,

        /// <summary>
        /// New lines inherit the same indention as the previous row.
        /// </summary>
        LastRow = 1,

        /// <summary>
        /// New lines get their indention from the scoping level.
        /// </summary>
        Scope = 2,

        /// <summary>
        /// New lines get thir indention from the scoping level or from the previous row
        /// depending on which is most indented.
        /// </summary>
        Smart = 3,
    }
}
