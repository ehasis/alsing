namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using JavaProxy;

    public class CompositeModel : AbstractCompositeModel
    {

        public CompositeModel(Type compositeType, Visibility visibility, MetaInfo metaInfo, AbstractMixinsModel mixinsModel, StateModel stateModel, CompositeMethodsModel compositeMethodsModel)
            : base(compositeType, visibility, metaInfo, mixinsModel, stateModel, compositeMethodsModel)
        {

        }

        public static CompositeModel NewModel(Type compositeType,
                                              Visibility visibility,
                                              MetaInfo metaInfo,
                                              PropertyDeclarations propertyDeclarations,
                                              IEnumerable<object> assemblyConcerns,
                                              IEnumerable<object> sideEffects, IList<Type> mixins)
        {
            var constraintsModel = new ConstraintsModel( compositeType );
            bool immutable = metaInfo.Get( typeof(ImmutableAttribute) ) != null;
            var propertiesModel = new PropertiesModel( constraintsModel, propertyDeclarations, immutable );
            var stateModel = new StateModel( propertiesModel );
            var mixinsModel = new AbstractMixinsModel( compositeType, mixins );

            var concerns = new List<ConcernDeclaration>();
            ConcernsDeclaration.ConcernDeclarations( assemblyConcerns, concerns );
            ConcernsDeclaration.ConcernDeclarations( compositeType, concerns );
            var concernsModel = new ConcernsDeclaration( concerns );

            var sideEffectsModel = new SideEffectsDeclaration( compositeType, sideEffects );
            var compositeMethodsModel = new CompositeMethodsModel( compositeType, constraintsModel, concernsModel, sideEffectsModel, mixinsModel );
            stateModel.AddStateFor( compositeMethodsModel.Methods, compositeType);

            return new CompositeModel(compositeType, visibility, metaInfo, mixinsModel, stateModel, compositeMethodsModel );
        }

        public CompositeInstance NewCompositeInstance(ModuleInstance moduleInstance, UsesInstance uses, StateHolder stateHolder)
        {
            object[] mixins = this.mixinsModel.NewMixinHolder();
            CompositeInstance compositeInstance = new DefaultCompositeInstance(this, moduleInstance, mixins, stateHolder);

            this.mixinsModel.NewMixins(compositeInstance, uses, stateHolder, mixins);

            return compositeInstance;
        }

        public object NewProxy(InvocationHandler handler, Type mixinType)
        {
            return Proxy.NewProxyInstance(mixinType, handler);
        }
    }

    public class ConstraintsModel
    {
        public ConstraintsModel(Type type)
        {
        }
    }

    public interface PropertyDeclarations
    {
        MetaInfo GetMetaInfo(MethodInfo accessor);

        object GetInitialValue(MethodInfo accessor);
    }

    public class MetaInfo
    {
        public object Get(Type type)
        {
            return null;
        }
    }
}