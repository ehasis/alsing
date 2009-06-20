namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    using Bootstrap;

    public abstract class AbstractCompositeDeclarationImpl<T, CT> : AbstractCompositeDeclaration<T, CT> where T : AbstractCompositeDeclaration<T, CT>
    {
        protected readonly T asT;

        protected readonly List<Type> compositeTypes = new List<Type>();

        protected readonly List<Type> concerns = new List<Type>();

        protected readonly List<Type> mixins = new List<Type>();

        protected readonly List<Type> sideEffects = new List<Type>();

        protected MetaInfo metaInfo = new MetaInfo();

        protected Visibility visibility;

        protected AbstractCompositeDeclarationImpl()
        {
            this.asT = (T)(object)this;
        }

        public T Include<K>() where K : CT
        {
            this.compositeTypes.Add(typeof(K));
            return this.asT;
        }

        public IList<Type> CompositeTypes
        {
            get
            {
                return this.compositeTypes;
            }
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

        public T WithSideEffect<K>()
        {
            this.sideEffects.Add(typeof(K));
            return this.asT;
        }
    }
}