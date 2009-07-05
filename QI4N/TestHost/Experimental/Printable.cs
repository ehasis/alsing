namespace ConsoleApplication23.Experimental
{
    using System;
    using System.Linq;

    using QI4N.Framework;
    using QI4N.Framework.Runtime;

    [Mixins(typeof(PrintableMixin))]
    public interface Printable
    {
        void Print();

        void Print(string propertyPath);
    }

    public class PrintableMixin : Printable
    {
        [State]
        private StateHolder state;

        [This]
        private object target;

        public void Print()
        {
        //    var c = this.target as CompositeInstance;

            string targetName = string.Format("{0}.", target.GetType().Name);
            this.Print(targetName);
        }

        public void Print(string propertyPath)
        {
            foreach (Property property in this.state.GetProperties().OrderBy(p => p.QualifiedName))
            {
                object value = property.Value;
                if (value is Printable)
                {
                    string newPath = propertyPath + property.QualifiedName + ".";
                    ((Printable)value).Print(newPath);
                }
                else
                {
                    Console.WriteLine("{0}{1} = '{2}'", propertyPath, property.QualifiedName, property);
                }
            }
        }
    }
}