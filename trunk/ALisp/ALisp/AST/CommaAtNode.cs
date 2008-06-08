using System.Collections;
using Alsing.Lisp.Infrastructure;
using Alsing.Lisp.Runtime;

namespace Alsing.Lisp.AST
{
    public class CommaAtNode : ValueNode
    {
        public object Expression { get; set; }

        public override object Eval(StackFrame stackFrame)
        {
            throw stackFrame.CreateException("CommaNodes should not be evaluated");
        }

        public override object Clone(CloneInfo info)
        {
            object res = Utils.Eval(info.StackFrame, Expression);
            var list = res as IEnumerable;
            var splice = new Splice(list);
            return splice;
        }

        public override string ToString()
        {
            return string.Format(",@{0}", Expression);
        }
    }
}