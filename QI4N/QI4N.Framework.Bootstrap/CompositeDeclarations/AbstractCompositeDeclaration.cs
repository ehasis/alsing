namespace QI4N.Framework.Bootstrap
{
    using System;
    using System.Collections.Generic;

    using Runtime;

    public interface AbstractCompositeDeclaration<T>
    {
        T VisibleIn(Visibility visibility);

        T WithConcerns(params Type[] concerns);

        T WithSideEffects(params Type[] sideEffects);

        T WithMixins(params Type[] mixins);
    }

    public abstract class AbstractCompositeDeclarationImpl<T> : AbstractCompositeDeclaration<T> where T : AbstractCompositeDeclaration<T>
    {
        private readonly List<Type> concerns = new List<Type>();

        private readonly List<Type> mixins = new List<Type>();

        private readonly List<Type> sideEffects = new List<Type>();

        private readonly T asT;

        private MetaInfo metaInfo = new MetaInfo();

        private Type[] types;

        private Visibility visibility;

        protected AbstractCompositeDeclarationImpl(Type[] types)
        {
            //foreach (Type compositeType in types)
            //{
            //    if (!typeof(TransientComposite).IsAssignableFrom(compositeType))
            //    {
            //        throw new Exception("Type is not a transient composite " + compositeType.Name);
            //    }
            //}

            this.types = types;
            this.asT = (T)(object)this;
        }

        public T VisibleIn(Visibility visibility)
        {
            this.visibility = visibility;
            return this.asT;
        }

        public T WithConcerns(params Type[] concerns)
        {
            this.concerns.AddRange(concerns);
            return this.asT;
        }

        public T WithMixins(params Type[] mixins)
        {
            this.mixins.AddRange(mixins);
            return this.asT;
        }

        public T WithSideEffects(params Type[] sideEffects)
        {
            this.sideEffects.AddRange(sideEffects);
            return this.asT;
        }
    }
}