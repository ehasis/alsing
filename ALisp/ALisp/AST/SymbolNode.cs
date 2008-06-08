using Alsing.Lisp.Infrastructure;

namespace Alsing.Lisp.AST
{
    public class SymbolNode : ValueNode
    {
        public string Name { get; set; }

        public override object Eval(StackFrame stackFrame)
        {
            return GetSymbol(stackFrame).Value;
        }

        public override string ToString()
        {
            return Name;
        }

        public override object Clone(CloneInfo info)
        {
            if (info.BackQuote)
            {
                var clone = new SymbolNode();
                BaseClone(clone);
                clone.Name = Name;
                return clone;
            }
            else
            {
                if (info.LocalIdentifiers.Contains(Name) || !info.StackFrame.Scope.ContainsSymbol(Name))
                {
                    var clone = new SymbolNode();
                    BaseClone(clone);
                    clone.Name = Name;
                    return clone;
                }
                else
                {
                    var clone = new SymbolSnapshotNode();
                    BaseClone(clone);
                    clone.staticSymbol = new Symbol();
                    clone.StaticSymbol.Value = info.StackFrame.Scope.GetSymbol(Name).Value;
                    clone.Name = Name;
                    return clone;
                }
            }
        }

        public Symbol GetSymbol(StackFrame stackFrame)
        {
            return stackFrame.Scope.GetSymbol(Name);
        }
    }
}