namespace Alsing.Lisp.Infrastructure
{
    public delegate object LazyValueDelegate();

    public class LazyValue
    {
        private readonly LazyValueDelegate del;
        private bool evaluated;
        private object result;

        public LazyValue(LazyValueDelegate del)
        {
            this.del = del;
        }

        public object Eval()
        {
            if (!evaluated)
            {
                evaluated = true;
                result = del();
            }

            return result;
        }


        public override string ToString()
        {
            if (evaluated)
                return string.Format("{0}", result);
            else
                return "LazyValue";
        }
    }

    public class TailRecursionValue
    {
        private readonly object expression;

        public TailRecursionValue(object expression)
        {
            this.expression = expression;
        }

        public object Expression
        {
            get { return expression; }
        }
    }

    //public class TailRecursionValue : LazyValue 
    //{
    //    public TailRecursionValue(LazyValueDelegate del) :base (del)
    //    {

    //    }
    //}
}