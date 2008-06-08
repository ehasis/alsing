using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Alsing.Lisp.AST;
using Alsing.Lisp.Runtime;

namespace Alsing.Lisp.Infrastructure
{
    public class RuntimeStack
    {
        public Dictionary<LispFunc, int> OperatorPriority;
        public StackFrame StackFrame;

        public RuntimeStack()
        {
            AllNodes = new List<ValueNode>();
            OperatorPriority = new Dictionary<LispFunc, int>();
            //  FunctionMeta = new Dictionary<LispFunc, FunctionMeta>();

            StackFrame = new StackFrame(null, null);
            StackFrame.Root = this;
            StackFrame.Scope = new Scope(null);
            StackFrame.Scope.Symbols = new Dictionary<string, Symbol>(1000);
        }

        public List<ValueNode> AllNodes { get; set; }
        public Engine Engine { get; set; }

        public object HandleCall(FunctionInvocation call, string methodName)
        {
            var var = call.Args[1] as ValueNode;
            object target = var.Eval(call.StackFrame);
            int paramCount = call.Args.Count - 3;

            MethodInfo[] methods =
                (from mi in
                     target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public |
                                                 BindingFlags.FlattenHierarchy)
                 where mi.Name == methodName && mi.GetParameters().Length == paramCount
                 select mi).ToArray();

            MethodInfo method = methods[0];

            var methodArgs = new object[paramCount];
            for (int i = 0; i < paramCount; i++)
            {
                object arg = Utils.Eval(call.StackFrame, call.Args[i + 3]);
                if (arg is LispFunc)
                {
                    var func = arg as LispFunc;
                    Type del = method.GetParameters()[i].ParameterType;
                    MethodInfo invoke = del.GetMethod("Invoke");
                    ParameterInfo[] invokeParams = invoke.GetParameters();


                    EventHandler eh = (s, e) =>
                                      {
                                          var args = new List<object>(10);

                                          args.Add(new SymbolNode());
                                          args.Add(s);
                                          args.Add(e);

                                          var newStackFrame = new StackFrame(StackFrame, null);
                                          newStackFrame.Function = func;
                                          newStackFrame.FunctionName = "{native call}";

                                          var kall = new FunctionInvocation(func, args, newStackFrame);
                                          //foreach (ParameterInfo invokeParam in invokeParams)
                                          //{
                                          //    kall.Args.Add(new PrimitiveValueNode<object>(null));
                                          //}
                                          func(kall);
                                      };
                    arg = eh;
                }
                methodArgs[i] = arg;
            }


            object res = method.Invoke(target, methodArgs);
            return res;
        }

        public ValueNode GetNodeFromCodeIndex(string codeName, int codeIndex)
        {
            foreach (ValueNode node in AllNodes)
            {
                if (node.CodeStartIndex == codeIndex && node.CodeName == codeName)
                    return node;
            }
            return null;
        }
    }
}