using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structural.AST
{
    public class Root : AstNode
    {
        public Body Body { get; set; }

        public Root()
        {
            Body = new Body();
        }
    }
}
