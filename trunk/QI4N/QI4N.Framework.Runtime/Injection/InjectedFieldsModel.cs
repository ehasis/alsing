namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Reflection;

    public class InjectedFieldsModel
    {
        private readonly List<InjectedFieldModel> fields = new List<InjectedFieldModel>();

        public InjectedFieldsModel(Type fragmentClass)
        {
            foreach (FieldInfo field in fragmentClass.GetAllFields())
            {
                var injectionAnnotation = field.GetAttribute<InjectionAttribute>();
                if (injectionAnnotation != null)
                {
                    this.AddModel(fragmentClass, field, injectionAnnotation);
                }
            }
        }

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public void Inject(InjectionContext context, object instance)
        {
            foreach (InjectedFieldModel field in this.fields)
            {
                field.Inject(context, instance);
            }
        }

        private void AddModel(Type fragmentClass, FieldInfo field, InjectionAttribute injectionAttribute)
        {
            //  bool optional = field.HasAttribute<OptionalAttribute>() || injectionAttribute.IsOptional();
            //  var dependencyModel = new DependencyModel(injectionAttribute, field.FieldType, fragmentClass, optional);
            var injectedFieldModel = new InjectedFieldModel(field, injectionAttribute);
            this.fields.Add(injectedFieldModel);
        }
    }
}