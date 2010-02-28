using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structural.AST
{
    [AstKey("if")]
    public class If : Statement
    {
        public Expression Condition { get; set; }
        public Body Body { get; set; }
        public If Next { get; set; }

        public If()
        {
            Body = new Body();
        }
    }
}
