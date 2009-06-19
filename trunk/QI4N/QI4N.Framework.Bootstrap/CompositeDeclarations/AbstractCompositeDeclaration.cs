namespace QI4N.Framework.Bootstrap
{
    using System;
    using System.Collections.Generic;

    using Runtime;

    public interface AbstractCompositeDeclaration<T,CT>
    {
        T VisibleIn(Visibility visibility);

        T WithConcern<K>();

        T WithSideEffect<K>();

        T WithMixin<K>();

        T Include<K>() where K : CT ;
    }

    public abstract class AbstractCompositeDeclarationImpl<T,CT> : AbstractCompositeDeclaration<T,CT> where T : AbstractCompositeDeclaration<T,CT>
    {
        private readonly List<Type> concerns = new List<Type>();

        private readonly List<Type> mixins = new List<Type>();

        private readonly List<Type> sideEffects = new List<Type>();

        private readonly T asT;

        private MetaInfo metaInfo = new MetaInfo();

        private readonly List<Type> compositeTypes = new List<Type>();

        private Visibility visibility;

        protected AbstractCompositeDeclarationImpl()
        {
            this.asT = (T)(object)this;
        }

        public T VisibleIn(Visibility visibility)
        {
            this.visibility = visibility;
            return this.asT;
        }

        public T WithConcern<K>()
        {
            this.concerns.Add(typeof(K));
            return this.asT;
        }

        public T WithMixin<K>()
        {
            this.mixins.Add(typeof(K));
            return this.asT;
        }

        public T Include<K>() where K : CT
        {
            compositeTypes.Add(typeof(K));
            return this.asT;
        }

        public T WithSideEffect<K>()
        {
            this.sideEffects.Add(typeof(K));
            return this.asT;
        }
    }
}