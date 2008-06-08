namespace Alsing.Lisp.Infrastructure
{
    public class Symbol
    {
        private bool bound;
        private object value;

        public object Value
        {
            get
            {
                //if (!bound)
                //{
                //    throw new Exception("Variable is not bound to a value");
                //}

                return value;
            }
            set
            {
                bound = true;
                this.value = value;
            }
        }

        public bool IsBound
        {
            get { return bound; }
        }
    }
}