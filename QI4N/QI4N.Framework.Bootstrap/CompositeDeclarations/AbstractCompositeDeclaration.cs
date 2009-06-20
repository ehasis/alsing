namespace QI4N.Framework.Bootstrap
{
    using System;
    using System.Collections.Generic;

    public interface AbstractCompositeDeclaration<T, CT>
    {
        T VisibleIn(Visibility visibility);

        T WithConcern<K>();

        T WithSideEffect<K>();

        T WithMixin<K>();

        T Include<K>() where K : CT;

        IList<Type> Includes { get; }
    }
}