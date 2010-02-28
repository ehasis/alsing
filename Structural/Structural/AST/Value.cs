namespace Structural.AST
{
    public class Value : AstNode
    {
        public static readonly  EmptyValue Empty = new EmptyValue();
    }

    public class EmptyValue : Value
    {
        
    }
}