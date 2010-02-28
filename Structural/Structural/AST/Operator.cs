namespace Structural.AST
{

    public abstract class Operator : Value
    {
        public Value LeftOperand { get; set; }

        public Value RightOperand { get; set; }
    }

    [AstKey("+")]
    public class AddOperator : Operator
    {
        
    }

    [AstKey("-")]
    public class SubOperator : Operator
    {
        
    }

    [AstKey("*")]
    public class MulOperator : Operator
    {
        
    }

    [AstKey("/")]
    public class DivOperator : Operator
    {
        
    }
}