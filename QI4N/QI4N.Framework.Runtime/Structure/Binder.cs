namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public interface Binder
    {
        void Bind(Resolution resolution);
    }

    public class Resolution
    {
        public Resolution(ApplicationModel application,
                          LayerModel layer,
                          ModuleModel module,
                          ObjectDescriptor objectDescriptor,
                          CompositeMethodModel method,
                          FieldInfo field)
        {
            this.Application = application;
            this.Layer = layer;
            this.Module = module;
            this.ObjectDescriptor = objectDescriptor;
            this.Method = method;
            this.Field = field;
        }

        public ApplicationModel Application { get; private set; }

        public FieldInfo Field { get; private set; }

        public LayerModel Layer { get; private set; }

        public CompositeMethodModel Method { get; private set; }

        public ModuleModel Module { get; private set; }

        public ObjectDescriptor ObjectDescriptor { get; private set; }


        public Resolution ForField(FieldInfo injectedField)
        {
            return new Resolution(this.Application, this.Layer, this.Module, this.ObjectDescriptor, this.Method, injectedField);
        }

        public ObjectDescriptor GetObject()
        {
            return this.ObjectDescriptor;
        }
    }

    public class ObjectDescriptor
    {
    }
}