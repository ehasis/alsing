using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structural.AST
{
    [AstKey("var")]
    public class VariableDeclaration : Statement
    {
        public string Name { get; set; }
        public Expression InitialValue { get; set; }
    }
}
