namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Reflection;

    public class MixinModel
    {
        private readonly HashSet<Type> thisMixinTypes;

        public MixinModel(Type mixinType)
        {
            this.MixinType = mixinType;

            //constructorsModel = new ConstructorsModel( mixinClass );
            //injectedFieldsModel = new InjectedFieldsModel( mixinClass );
            //injectedMethodsModel = new InjectedMethodsModel( mixinClass );

            //List<ConcernDeclaration> concerns = new ArrayList<ConcernDeclaration>();
            //ConcernsDeclaration.concernDeclarations( mixinClass, concerns );
            //concernsDeclaration = new ConcernsDeclaration( concerns );
            //sideEffectsDeclaration = new SideEffectsDeclaration( mixinClass, Collections.<Class<?>>emptyList() );

            this.thisMixinTypes = this.BuildThisMixinTypes();
        }

        public bool IsGeneric
        {
            get
            {
                return typeof(InvocationHandler).IsAssignableFrom(this.MixinType);
            }
        }


        public Type MixinType { get; set; }

        public IEnumerable<Type> GetThisMixinTypes()
        {
            return this.thisMixinTypes;
        }

        public object NewInstance(CompositeInstance compositeInstance, StateHolder stateHolder, UsesInstance uses)
        {
            throw new NotImplementedException();
        }

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        public FragmentInvocationHandler NewInvocationHandler(MethodInfo method)
        {
            if (this.IsGeneric)
            {
                return new GenericFragmentInvocationHandler();
            }

            return new TypedFragmentInvocationHandler();
        }

        private HashSet<Type> BuildThisMixinTypes()
        {
            var thisDependencies = new HashSet<Type>();

            IEnumerable<Type> thisTypes = this.MixinType
                    .GetAllFields()
                    .Where(f => TypeExtensions.HasAttribute(f, typeof(ThisAttribute)))
                    .Select(f => f.FieldType);

            foreach (Type type in thisTypes)
            {
                thisDependencies.Add(type);
            }

            return thisDependencies;
        }
    }
}