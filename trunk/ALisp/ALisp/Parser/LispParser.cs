using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Alsing.Lisp.AST;
using Alsing.Lisp.Runtime;

namespace Alsing.Lisp.Parser
{
    public class ParseInfo
    {
        public string Code { get; set; }
        public string CodeName { get; set; }
        public bool HideFromCallStack { get; set; }
        public Engine Engine { get; set; }
    }

    public class LispParser
    {
        public RootNode Parse(ParseInfo info)
        {
            int index = 0;
            var root = new RootNode();
            SetupNode(info, root);

            root.Args = ParseArgs(info, ref index);

            return root;
        }

        private ConsNode ParseFunction(ParseInfo info, ref int index)
        {
            var funcNode = new ConsNode();
            SetupNode(info, funcNode);
            funcNode.CodeStartIndex = index;

            EnsureToken(info, ref index, "(");
            List<object> args = ParseArgs(info, ref index);
            funcNode.Args = args;
            index++;
            funcNode.CodeLength = index - funcNode.CodeStartIndex;

            return funcNode;
        }

        //public Cons MakeSExpression(Cons funcNode)
        //{
        //    Cons head = new Cons();
        //    Cons current = head;

        //    Cons last = null;
        //    foreach (ValueNode node in funcNode.Args)
        //    {


        //        if (node is IdentifierNode)
        //        {
        //            current.Car = node;
        //        }
        //        else if (node is PrimitiveValueNode<int> || node is PrimitiveValueNode<double> || node is PrimitiveValueNode<string>)
        //        {
        //            current.Car = node.Eval(null);
        //        }
        //        else if (node is Cons)
        //        {
        //            current.Car = MakeSExpression(node as Cons);
        //        }

        //        last = current;
        //        current.Cdr = new Cons();
        //        current = current.Cdr;
        //    }
        //    last.Cdr = null;

        //    return head;
        //}

        private void SetupNode(ParseInfo info, ValueNode node)
        {
            node.CodeName = info.CodeName;
            node.HideFromCallstack = info.HideFromCallStack;
            node.Code = info.Code;
            node.Stack = info.Engine.Stack;
            info.Engine.Stack.AllNodes.Add(node);
        }

        private List<object> ParseArgs(ParseInfo info, ref int index)
        {
            var args = new List<object>();
            while (!PeekToken(info, ref index, ")"))
            {
                if (EOF(info, ref index))
                {
                    break;
                }

                object arg = ParseArg(info, ref index);
                args.Add(arg);
            }

            return args;
        }

        private bool EOF(ParseInfo info, ref int index)
        {
            while (index < info.Code.Length && " \t\n\r".Contains(info.Code[index]))
                index++;

            if (index >= info.Code.Length)
                return true;
            else
                return false;
        }

        private bool PeekToken(ParseInfo info, ref int index, string token)
        {
            //move past whitespace
            while (index < info.Code.Length && " \t\n\r".Contains(info.Code[index]))
                index++;

            if (index > info.Code.Length - token.Length)
                return false;

            string current = info.Code.Substring(index, token.Length);

            if (current == token)
                return true;

            return false;
        }

        private object ParseArg(ParseInfo info, ref int index)
        {
            while (PeekToken(info, ref index, ";"))
            {
                ParseComment(info, ref index);
            }

            if (PeekToken(info, ref index, "\""))
                return ParseString(info, ref index);

            if (PeekToken(info, ref index, "("))
                return ParseFunction(info, ref index);

            if (PeekToken(info, ref index, "`"))
                return ParseQuote(info, ref index);

            if (PeekToken(info, ref index, "'"))
                return ParseQuote(info, ref index);

            if (PeekToken(info, ref index, ",@"))
                return ParseCommaAt(info, ref index);

            if (PeekToken(info, ref index, ","))
                return ParseComma(info, ref index);


            return ParseLiteral(info, ref index);
        }

        private void ParseComment(ParseInfo info, ref int index)
        {
            while (info.Code[index] != '\n')
            {
                index++;
            }
            index++;
        }

        private ValueNode ParseQuote(ParseInfo info, ref int index)
        {
            var node = new ConsNode();
            node.Args = new List<object>();
            var idNode = new SymbolNode();
            idNode.Name = "quote";
            idNode.CodeStartIndex = index;
            idNode.CodeLength = 1;
            SetupNode(info, idNode);
            node.Args.Add(idNode);


            node.CodeStartIndex = index;
            index++;
            object expression = ParseArg(info, ref index);

            node.Args.Add(expression);

            node.CodeLength = index - node.CodeStartIndex;
            SetupNode(info, node);

            return node;
        }

        private ValueNode ParseComma(ParseInfo info, ref int index)
        {
            var node = new CommaNode();
            node.CodeStartIndex = index;
            index++;
            object expression = ParseArg(info, ref index);
            node.Expression = expression;
            node.CodeLength = index - node.CodeStartIndex;
            SetupNode(info, node);

            return node;
        }

        private ValueNode ParseCommaAt(ParseInfo info, ref int index)
        {
            var node = new CommaAtNode();
            node.CodeStartIndex = index;
            index++;
            index++;
            object expression = ParseArg(info, ref index);
            node.Expression = expression;
            node.CodeLength = index - node.CodeStartIndex;
            SetupNode(info, node);

            return node;
        }

        private object ParseString(ParseInfo info, ref int index)
        {
            int startIndex = index;
            index++;
            int scanIndex = index;
            string text = "";
            while (info.Code[scanIndex] != '"')
            {
                if (info.Code[scanIndex] == '\\')
                {
                    scanIndex++;
                }
                text += info.Code[scanIndex];
                scanIndex++;
            }

            index = scanIndex;
            index++;

            return text;
        }

        private object ParseLiteral(ParseInfo info, ref int index)
        {
            string current = GetToken(info, ref index);

            double doubleValue;
            if (double.TryParse(current, NumberStyles.Number, CultureInfo.InvariantCulture, out doubleValue))
            {
                if (current.IndexOf(".") >= 0)
                {
                    return doubleValue;
                }
                else
                {
                    return (int) doubleValue;
                }
            }
            else
            {
                if (current.Length > 1 && current.EndsWith("b"))
                {
                    string prefix = current.Substring(0, current.Length - 1);
                    long l = 0;
                    if (long.TryParse(prefix, out l))
                    {
                        return new BigInteger(l);
                    }
                }

                var valueNode = new SymbolNode();
                SetupNode(info, valueNode);
                valueNode.Name = current;
                valueNode.CodeStartIndex = index - current.Length;
                valueNode.CodeLength = current.Length;
                return valueNode;
            }
        }

        private string GetToken(ParseInfo info, ref int index)
        {
            //move past whitespace
            while (index < info.Code.Length && " \t\n\r".Contains(info.Code[index]))
                index++;

            if (index >= info.Code.Length)
                throw new Exception("EOF");

            int scanIndex = index;
            while (scanIndex < info.Code.Length && !" \t\n\r()".Contains(info.Code[scanIndex]))
                scanIndex++;

            string token = info.Code.Substring(index, scanIndex - index);


            index = scanIndex;
            return token;
        }

        private void EnsureToken(ParseInfo info, ref int index, string token)
        {
            //move past whitespace
            while (index < info.Code.Length && " \t\n\r".Contains(info.Code[index]))
                index++;

            if (index >= info.Code.Length - token.Length)
                throw new Exception("EOF");

            string current = info.Code.Substring(index, token.Length);

            if (current != token)
                throw new Exception(string.Format("Expected token '{0}'", token));

            index += token.Length;
        }
    }
}