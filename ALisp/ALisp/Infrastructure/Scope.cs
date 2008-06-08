using System.Collections.Generic;

namespace Alsing.Lisp.Infrastructure
{
    public class Scope
    {
        public Scope PreviousScope;
        public Dictionary<string, Symbol> Symbols;

        public Scope(Scope previousScope)
        {
            Symbols = new Dictionary<string, Symbol>(10);
            PreviousScope = previousScope;
        }

        public Symbol PushSymbol(string name)
        {
            var symbol = new Symbol();
            Symbols.Add(name, symbol);
            return symbol;
        }

        public Symbol PopSymbol(string name)
        {
            Symbol symbol = Symbols[name];
            Symbols.Remove(name);
            return symbol;
        }

        public void DeclareRef(string name, Symbol symbol)
        {
            Symbols.Add(name, symbol);
        }

        public void SetSymbolValue(string name, object value)
        {
            Symbol var = GetSymbol(name);
            var.Value = value;
        }

        public void SetSymbolValue(string name, LispFunc value)
        {
            Symbol var = GetSymbol(name);
            var.Value = value;
        }

        public Symbol GetSymbol(string name)
        {
            Symbol symbol = null;
            //Console.WriteLine("getting {0}:{1}", name,this.GetHashCode ());

            if (Symbols.TryGetValue(name, out symbol))
            {
                //all ok
            }
            else
            {
                if (PreviousScope != null)
                    return PreviousScope.GetSymbol(name);
            }

            if (symbol == null)
            {
                symbol = PushSymbol(name);
            }

            return symbol;
        }

        public bool ContainsSymbol(string name)
        {
            if (Symbols.ContainsKey(name))
                return true;
            else
            {
                if (PreviousScope != null)
                    return PreviousScope.ContainsSymbol(name);
            }

            return false;
        }
    }
}