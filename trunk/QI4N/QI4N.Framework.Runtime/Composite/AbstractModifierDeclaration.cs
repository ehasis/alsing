namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    using Reflection;

    public class AbstractModifierDeclaration
    {
        private readonly Type declaredIn;

        private readonly Type modifierClass;

        private AppliesToFilter appliesToFilter;

        public AbstractModifierDeclaration(Type modifierClass, Type declaredIn)
        {
            this.modifierClass = modifierClass;
            this.declaredIn = declaredIn;
            this.CreateAppliesToFilter(modifierClass);
        }

        public bool AppliesTo(MethodInfo method, Type compositeType)
        {
            return this.appliesToFilter.AppliesTo(method, this.modifierClass, compositeType, this.modifierClass);
        }

        public override string ToString()
        {
            return this.modifierClass.Name + (this.declaredIn == null ? "" : " declared in " + this.declaredIn);
        }

        private void CreateAppliesToFilter(Type modifierClass)
        {
            if (!typeof(InvocationHandler).IsAssignableFrom(modifierClass))
            {
                this.appliesToFilter = new TypedModifierAppliesToFilter();

                if (modifierClass.IsAbstract)
                {
                    this.appliesToFilter = new AndAppliesToFilter(this.appliesToFilter, new ImplementsMethodAppliesToFilter());
                }
            }

            var appliesTo = modifierClass.GetAttribute<AppliesToAttribute>();
            if (appliesTo != null)
            {
                foreach (Type appliesToClass in appliesTo.AppliesToTypes)
                {
                    AppliesToFilter filter;
                    if (typeof(AppliesToFilter).IsAssignableFrom(appliesToClass))
                    {
                        try
                        {
                            filter = (AppliesToFilter)appliesToClass.NewInstance();
                        }
                        catch (Exception e)
                        {
                            throw; //new ConstructionException( e );
                        }
                    }
                    else if (typeof(Attribute).IsAssignableFrom(appliesToClass))
                    {
                        filter = new AnnotationAppliesToFilter(appliesToClass);
                    }
                    else // Type check
                    {
                        filter = new TypeCheckAppliesToFilter(appliesToClass);
                    }

                    if (this.appliesToFilter == null)
                    {
                        this.appliesToFilter = filter;
                    }
                    else
                    {
                        this.appliesToFilter = new AndAppliesToFilter(this.appliesToFilter, filter);
                    }
                }
            }

            if (this.appliesToFilter == null)
            {
                this.appliesToFilter = AppliesToEverything.Instance;
            }
        }
    }










}