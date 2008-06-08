using Alsing.Lisp.AST;
using Alsing.Lisp.Infrastructure;
using Alsing.Lisp.Parser;
using Alsing.Lisp.Properties;

namespace Alsing.Lisp.Runtime
{
    public delegate void BeginCall(StackFrame stackFrame, ConsNode cons, LispFunc func, string funcName);

    public delegate void EndCall(StackFrame stackFrame, ConsNode cons, LispFunc func, string funcName, object res);

    public delegate void Print(string text);

    public class Engine
    {
        private ConsNode root;

        public Engine()
        {
            Stack = new RuntimeStack();
            Stack.Engine = this;
            Parser = new LispParser();

            CoreConfigurator.Configure(Stack);

            ParseInfo info = SetupParseInfo(Resources.CoreLib.Replace("\r\n", "\n"), "core", true);

            ConsNode root = Parser.Parse(info);
            root.Eval(Stack.StackFrame);
        }

        public RuntimeStack Stack { get; set; }
        public LispParser Parser { get; set; }
        public bool UseCallStack { get; set; }

        public void Parse(string code)
        {
            try
            {
                ParseInfo info = SetupParseInfo(code, "user", false);
                root = Parser.Parse(info);
                //     Cons rootCons = Parser.MakeSExpression(root);
            }
            catch
            {
                throw;
            }
        }

        public object EvaluateString(string code)
        {
            return EvaluateString(code, "user");
        }


        public object EvaluateString(string code, string codeName)
        {
            try
            {
                ParseInfo info = SetupParseInfo(code, codeName, false);
                ConsNode node = Parser.Parse(info);

                //          Cons rootCons = Parser.MakeSExpression(node);

                return node.Eval(Stack.StackFrame);
            }
            catch
            {
                throw;
            }
        }

        public object Execute()
        {
            try
            {
                object res = root.Eval(Stack.StackFrame);
                return res;
            }
            catch
            {
                throw;
            }
        }

        public ConsNode GetRoot()
        {
            return root;
        }

        private ParseInfo SetupParseInfo(string code, string codeName, bool hideFromCallStack)
        {
            string tmp = code;
            var info = new ParseInfo();
            info.Engine = this;
            info.Code = tmp;
            info.CodeName = codeName;
            info.HideFromCallStack = hideFromCallStack;
            return info;
        }


        public event BeginCall BeginNotifyCall;
        public event EndCall EndNotifyCall;
        public event Print Print;

        protected internal virtual void OnPrint(string text)
        {
            if (Print != null)
                Print(text);
        }

        protected internal virtual void OnBeginNotifyCall(StackFrame stackFrame, ConsNode cons, LispFunc func,
                                                          string funcName)
        {
            if (BeginNotifyCall != null)
                BeginNotifyCall(stackFrame, cons, func, funcName);
        }

        protected internal virtual void OnEndNotifyCall(StackFrame stackFrame, ConsNode cons, LispFunc func,
                                                        string funcName, object res)
        {
            if (EndNotifyCall != null)
                EndNotifyCall(stackFrame, cons, func, funcName, res);
        }
    }
}