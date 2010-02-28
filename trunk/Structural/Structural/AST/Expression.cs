using System.Collections.Generic;

namespace Structural.AST
{
    [AstKey("exp")]
    public class Expression : Value
    {
        public List<AstNode> Operands { get; set; }
        public Expression()
        {
            Operands = new List<AstNode>();
        }
    }
}