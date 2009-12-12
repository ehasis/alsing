using Alsing.Lisp.Infrastructure;

namespace Alsing.Lisp.AST
{
    //used to hold snapshots of non local values inside a lambda
    //binding identifiers to values at the time of the lambda declaration
    public class SymbolSnapshotNode : SymbolNode
    {
        internal Symbol staticSymbol;

        public Symbol StaticSymbol
        {
            get { return staticSymbol; }
        }


        public override object Eval(StackFrame stackFrame)
        {
            return staticSymbol.Value;
        }

        public override object Clone(CloneInfo info)
        {
            var clone = new SymbolSnapshotNode();
            BaseClone(clone);
            clone.staticSymbol = new Symbol();
            clone.StaticSymbol.Value = StaticSymbol.Value;
            clone.Name = Name;
            return clone;
        }
    }
}