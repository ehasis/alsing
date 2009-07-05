namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class InjectedFieldModel
    {
        public InjectedFieldModel(FieldInfo injectedField, InjectionAttribute injectionAttribute)
        {
            this.InjectedField = injectedField;
            this.InjectionAttribute = injectionAttribute;
        }

        public InjectionAttribute InjectionAttribute { get; private set; }

        public FieldInfo InjectedField { get; private set; }

        public void Inject(InjectionContext context, object instance)
        {
          //  object value = this.DependencyModel.Inject(context);
          //  this.InjectedField.SetValue(instance, value);

            object value = null;
            if (InjectionAttribute is ConcernForAttribute)
            {
                value = context.Next;
            }
            if (InjectionAttribute is SideEffectForAttribute)
            {
                value = context.Next;
            }

            InjectedField.SetValue(instance,value);

        }
    }
}