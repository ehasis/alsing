using Alsing.Lisp.Infrastructure;
using Alsing.Lisp.Runtime;

namespace Alsing.Lisp.AST
{
    public class CommaNode : ValueNode
    {
        public object Expression { get; set; }

        public override object Eval(StackFrame stackFrame)
        {
            throw stackFrame.CreateException("CommaNodes should not be evaluated");
        }

        public override object Clone(CloneInfo info)
        {
            object res = (Utils.Eval(info.StackFrame, Expression));
            return res;
        }

        public override string ToString()
        {
            return string.Format(",{0}", Expression);
        }
    }
}