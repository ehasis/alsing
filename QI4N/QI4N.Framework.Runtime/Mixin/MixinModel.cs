namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    using Reflection;

    [DebuggerDisplay("MixinType {MixinType}")]
    public class MixinModel
    {
        private readonly ConcernsDeclaration concernsDeclaration;

        private readonly InjectedObjectBuilder injectedObjectBuilder;

        private readonly SideEffectsDeclaration sideEffectsDeclaration;

        private readonly IEnumerable<Type> thisMixinTypes;

        public MixinModel(Type mixinType)
        {
            this.MixinType = mixinType;

            this.injectedObjectBuilder = new InjectedObjectBuilder(mixinType);

            var concerns = new List<ConcernDeclaration>();
            ConcernsDeclaration.ConcernDeclarations(mixinType, concerns);
            this.concernsDeclaration = new ConcernsDeclaration(concerns);
            this.sideEffectsDeclaration = new SideEffectsDeclaration(mixinType, Enumerable.Empty<Type>());

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

        public MethodConcernsModel ConcernsFor(MethodInfo method, Type type)
        {
            return this.concernsDeclaration.ConcernsFor(method, type);
        }

        public IEnumerable<Type> GetThisMixinTypes()
        {
            return this.thisMixinTypes;
        }

        public object NewInstance(CompositeInstance compositeInstance, StateHolder stateHolder)
        {
            return this.NewInstance(compositeInstance, stateHolder, UsesInstance.NoUses);
        }

        [DebuggerStepThrough]
        //[DebuggerHidden]
        public object NewInstance(CompositeInstance compositeInstance, StateHolder stateHolder, UsesInstance uses)
        {
            var injectionContext = new InjectionContext(compositeInstance, uses, stateHolder);
            object mixin = this.injectedObjectBuilder.NewInstance(injectionContext);
            return mixin;
        }


        [DebuggerStepThrough]
        //[DebuggerHidden]
        public FragmentInvocationHandler NewInvocationHandler(Type methodType)
        {
            if (typeof(InvocationHandler).IsAssignableFrom(this.MixinType) && !methodType.IsAssignableFrom(this.MixinType))
            {
                return new GenericFragmentInvocationHandler();
            }

            return new TypedFragmentInvocationHandler();
        }

        public MethodSideEffectsModel SideEffectsFor(MethodInfo method, Type mixinType)
        {
            var methodSideEffectsModels = new List<MethodSideEffectModel>();
            var model = new MethodSideEffectsModel(method, methodSideEffectsModels);
            return model;
        }

        private IEnumerable<Type> BuildThisMixinTypes()
        {
            IEnumerable<Type> thisTypes = this.MixinType.GetAllFields()
                    .Where(f => f.HasAttribute(typeof(ThisAttribute)))
                    .Select(f => f.FieldType)
                    .Distinct();

            return thisTypes;
        }
    }
}