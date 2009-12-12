namespace Alsing.SourceCode.SyntaxDocumentParsers
{
    public class ScanResultSegment
    {
        public bool HasContent;
        public bool IsEndSegment;
        public Pattern Pattern;
        public int Position;
        public Scope Scope;
        public Span span;
        public SpanDefinition spanDefinition;
        public string Token = "";
    }
}