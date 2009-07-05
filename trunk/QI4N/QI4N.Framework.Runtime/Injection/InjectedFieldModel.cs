namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    using API.Reflection;

    public class InjectedFieldModel
    {
        private readonly Field injectedField;

        private readonly InjectionAttribute injectionAttribute;

        private readonly InjectionProvider injectionProvider;

        public InjectedFieldModel(FieldInfo injectedField, InjectionAttribute injectionAttribute)
        {
            this.injectedField = new Field(injectedField);
            this.injectionAttribute = injectionAttribute;
            this.injectionProvider = InjectionProviderLookup.ProviderFor(injectionAttribute);
        }

        public void Inject(InjectionContext context, object instance)
        {
            object value = this.injectionProvider.ProvideInjection(context, this.injectionAttribute, this.injectedField.FieldType);
            this.injectedField.SetValue(instance, value);
        }
    }
}