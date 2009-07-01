namespace QI4N.Framework.Runtime
{
    using System.Reflection;

    public interface Binder
    {
        void Bind(Resolution resolution);
    }

    public class Resolution
    {
        private readonly ApplicationModel application;

        private readonly FieldInfo field;

        private readonly LayerModel layer;

        private readonly CompositeMethodModel method;

        private readonly ModuleModel module;

        private readonly ObjectDescriptor objectDescriptor;

        public Resolution(ApplicationModel application,
                          LayerModel layer,
                          ModuleModel module,
                          ObjectDescriptor objectDescriptor,
                          CompositeMethodModel method,
                          FieldInfo field)
        {
            this.application = application;
            this.layer = layer;
            this.module = module;
            this.objectDescriptor = objectDescriptor;
            this.method = method;
            this.field = field;
        }

        public ApplicationModel Application
        {
            get
            {
                return this.application;
            }
        }

        public FieldInfo Field
        {
            get
            {
                return this.field;
            }
        }

        public LayerModel Layer
        {
            get
            {
                return this.layer;
            }
        }

        public CompositeMethodModel Method
        {
            get
            {
                return this.method;
            }
        }

        public ModuleModel Module
        {
            get
            {
                return this.module;
            }
        }

        public ObjectDescriptor ObjectDescriptor
        {
            get
            {
                return this.objectDescriptor;
            }
        }


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