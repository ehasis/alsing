using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structural.AST
{
    public class AstKeyAttribute : Attribute
    {
        public string Name { get; set; }
        public AstKeyAttribute(string name)
        {
            Name = name;
        }
    }
}
