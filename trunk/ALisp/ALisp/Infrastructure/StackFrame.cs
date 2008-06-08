using System;
using System.Text;
using Alsing.Lisp.AST;

namespace Alsing.Lisp.Infrastructure
{
    public class StackFrame
    {
        public ConsNode Cons;
        public LispFunc Function;
        public string FunctionName;

        public StackFrame PreviousStackFrame;
        public RuntimeStack Root;
        public Scope Scope;


        public StackFrame(StackFrame previousStackFrame, ConsNode cons)
        {
            PreviousStackFrame = previousStackFrame;
            Cons = cons;

            if (previousStackFrame != null)
            {
                Root = previousStackFrame.Root;
                Scope = previousStackFrame.Scope;
            }
        }

        internal Exception CreateException(string message)
        {
            string fullMessage = message + "\n--------------------\n" + ToString();
            var ex = new Exception(fullMessage);
            return ex;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            StackFrame frame = this;
            while (frame != null && frame.Cons != null)
            {
                if (!frame.Cons.HideFromCallstack)
                {
                    string func = frame.FunctionName;
                    string body = frame.Cons.ToString();

                    sb.AppendFormat("{0} : {1}\n\n", func, body);
                }
                frame = frame.PreviousStackFrame;
            }

            return sb.ToString();
        }
    }
}