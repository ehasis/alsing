namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Bootstrap;

    [DebuggerDisplay("Name: {Name}")]
    public class LayerAssemblyImpl : LayerAssembly
    {
        private readonly ApplicationAssembly applicationAssembly;

        private readonly MetaInfo metaInfo = new MetaInfo();

        private readonly List<ModuleAssembly> moduleAssemblies;

        private readonly HashSet<LayerAssembly> uses;

        private string name;

        public LayerAssemblyImpl(ApplicationAssembly applicationAssembly, String name)
        {
            this.applicationAssembly = applicationAssembly;
            this.name = name;

            this.moduleAssemblies = new List<ModuleAssembly>();
            this.uses = new HashSet<LayerAssembly>();
        }

        public ApplicationAssembly ApplicationAssembly
        {
            get
            {
                return this.applicationAssembly;
            }
        }


        public MetaInfo MetaInfo
        {
            get
            {
                return this.metaInfo;
            }
        }

        public IList<ModuleAssembly> Modules
        {
            get
            {
                return this.moduleAssemblies;
            }
        }


        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public ModuleAssembly NewModuleAssembly(string name)
        {
            var moduleAssembly = new ModuleAssemblyImpl(this, name,metaInfo);
            this.moduleAssemblies.Add(moduleAssembly);
            return moduleAssembly;
        }

        public ModuleAssembly NewModuleAssembly()
        {
            return this.NewModuleAssembly("");
        }

        public LayerAssembly SetMetaInfo(Object info)
        {
            this.metaInfo.Set(info);
            return this;
        }

        public LayerAssembly SetName(string name)
        {
            this.name = name;
            return this;
        }

        public override string ToString()
        {
            return "LayerAssembly [" + this.Name + "]";
        }

        public LayerAssembly Uses(params LayerAssembly[] layerAssembly)
        {
            layerAssembly
                    .ToList()
                    .ForEach(l => this.uses.Add(l));

            return this;
        }


        public void Visit(AssemblyVisitor visitor)
        {
            visitor.VisitLayer(this);
            foreach (ModuleAssemblyImpl moduleAssembly in this.moduleAssemblies)
            {
                moduleAssembly.Visit(visitor);
            }
        }
    }
}