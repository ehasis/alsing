namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    using API.Reflection;

    public class InjectedFieldModel
    {
        public InjectedFieldModel(FieldInfo injectedField, InjectionAttribute injectionAttribute)
        {
            this.InjectedField = new Field(injectedField);
            this.InjectionAttribute = injectionAttribute;
        }

        public Field InjectedField { get; private set; }

        public InjectionAttribute InjectionAttribute { get; private set; }

        public void Inject(InjectionContext context, object instance)
        {
            //  object value = this.DependencyModel.Inject(context);
            //  this.InjectedField.SetValue(instance, value);

            object value = null;
            if (this.InjectionAttribute is ConcernForAttribute)
            {
                value = context.Next;
            }
            if (this.InjectionAttribute is SideEffectForAttribute)
            {
                value = context.Next;
            }
            if (this.InjectionAttribute is StateAttribute)
            {
                if (typeof(StateHolder).IsAssignableFrom(this.InjectedField.FieldType))
                {
                    value = context.State;
                }
            }
            if (this.InjectionAttribute is ThisAttribute)
            {
                value = context.CompositeInstance.NewProxy(this.InjectedField.FieldType);
            }
            if (this.InjectionAttribute is UsesAttribute)
            {
                value = context.Uses.UseForType(this.InjectedField.FieldType);
            }

            this.InjectedField.SetValue(instance, value);
        }
    }
}