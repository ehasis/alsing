namespace ConsoleApplication23.Experimental
{
    using System;
    using System.Linq;

    using QI4N.Framework;

    [Mixins(typeof(PrintableMixin))]
    public interface Printable
    {
        void Print();
    }

    public class PrintableMixin : Printable
    {
        [State]
        private StateHolder state;

        public void Print()
        {
            foreach (Property property in this.state.GetProperties().OrderBy(p => p.QualifiedName))
            {
                object value = property.Value;
                if (value is Printable)
                {
                    Console.WriteLine();
                    Console.WriteLine("Sub Property {0}:", property.QualifiedName);
                    ((Printable)value).Print();
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("{0} = {1}", property.QualifiedName, property.Value);
                }
            }
        }
    }
}