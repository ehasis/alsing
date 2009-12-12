using Alsing.Lisp.Infrastructure;

namespace Alsing.Lisp.AST
{
    public abstract class ValueNode
    {
        public int CodeStartIndex { get; set; }
        public int CodeLength { get; set; }
        public string Code { get; set; }
        public string CodeName { get; set; }
        public bool HideFromCallstack { get; set; }
        public RuntimeStack Stack { get; set; }
        public abstract object Eval(StackFrame stackFrame);

        public abstract object Clone(CloneInfo info);

        protected void BaseClone(ValueNode clone)
        {
            clone.Code = Code;
            clone.CodeStartIndex = CodeStartIndex;
            clone.CodeLength = CodeLength;
            clone.CodeName = CodeName;
            clone.HideFromCallstack = HideFromCallstack;
            clone.Stack = Stack;
        }
    }
}