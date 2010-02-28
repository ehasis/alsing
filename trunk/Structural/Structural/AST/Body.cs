using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structural.AST
{
    [AstKey("body")]
    public class Body : AstNode
    {
        public List<Statement> Statements { get; set; }

        public Body()
        {
            Statements = new List<Statement>();
        }
    }
}
