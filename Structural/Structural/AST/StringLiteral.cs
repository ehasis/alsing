namespace Structural.AST
{
    [AstKey("string")]
    public class StringLiteral : Value
    {
        public string Value { get; set; }
    }
}