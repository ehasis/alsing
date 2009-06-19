namespace QI4N.Framework.Bootstrap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Runtime;
    using System.Diagnostics;

    public interface LayerAssembly
    {
        ModuleAssembly NewModuleAssembly(string name);

        ApplicationAssembly ApplicationAssembly { get; }

        string Name { get; }

        LayerAssembly SetName(string name);

        LayerAssembly SetMetaInfo(object info);

        LayerAssembly Uses(params LayerAssembly[] layerAssembly);

        void Visit(AssemblyVisitor visitor);

        ModuleAssembly NewModuleAssembly();


    }

    [DebuggerDisplay("Name: {Name}")]
    public class LayerAssemblyImpl : LayerAssembly
    {
        private readonly ApplicationAssembly applicationAssembly;
        private readonly List<ModuleAssemblyImpl> moduleAssemblies;
        private readonly HashSet<LayerAssembly> uses;

        private string name;
        private readonly MetaInfo metaInfo = new MetaInfo();

        public LayerAssemblyImpl( ApplicationAssembly applicationAssembly, String name )
        {
            this.applicationAssembly = applicationAssembly;
            this.name = name;

            moduleAssemblies = new List<ModuleAssemblyImpl>();
            uses = new HashSet<LayerAssembly>();
        }

        public ModuleAssembly NewModuleAssembly( string name )
        {
            var moduleAssembly = new ModuleAssemblyImpl( this, name );
            moduleAssemblies.Add( moduleAssembly );
            return moduleAssembly;
        }

        public ModuleAssembly NewModuleAssembly()
        {
            return NewModuleAssembly("");
        }

        public ApplicationAssembly ApplicationAssembly
        {
            get
            {
                return applicationAssembly;
            }
        }



        public LayerAssembly SetMetaInfo( Object info )
        {
            metaInfo.Set( info );
            return this;
        }

        public LayerAssembly Uses( params LayerAssembly[] layerAssembly )
        {
            layerAssembly
                .ToList()
                .ForEach(l => uses.Add(l));

            return this;
        }





        public void Visit( AssemblyVisitor visitor ) 
        {
            visitor.VisitLayer( this );
            foreach( ModuleAssemblyImpl moduleAssembly in moduleAssemblies )
            {
                moduleAssembly.Visit( visitor );
            }
        }

        public MetaInfo MetaInfo
        {
            get
            {
                return metaInfo;
            }
        }


        public string Name
        {
            get
            {
                return name;
            }
        }

        public LayerAssembly SetName(string name)
        {
            this.name = name;
            return this;
        }

        public override string ToString()
        {
            return "LayerAssembly [" + Name + "]";
        }
    }













}