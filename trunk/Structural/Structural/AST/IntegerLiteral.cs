namespace Structural.AST
{
    [AstKey("int")]
    public class IntegerLiteral : Value
    {
        public int Value { get; set; }
    }
}