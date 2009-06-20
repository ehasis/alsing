namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Reflection;

    public class SideEffectsDeclaration
    {
        private readonly Dictionary<MethodInfo, MethodSideEffectsModel> methodSideEffects = new Dictionary<MethodInfo, MethodSideEffectsModel>();

        private readonly List<SideEffectDeclaration> sideEffectDeclarations = new List<SideEffectDeclaration>();

        public SideEffectsDeclaration(Type type, IEnumerable<Type> sideEffects)
        {
            List<Type> types = AsSideEffectsTargetTypes(type);

            foreach (Type aType in types)
            {
                this.AddSideEffectDeclaration(aType);
            }

            // Add sideeffects from assembly
            foreach (Type sideEffect in sideEffects)
            {
                this.sideEffectDeclarations.Add(new SideEffectDeclaration(sideEffect, null));
            }
        }

        public MethodSideEffectsModel SideEffectsFor(MethodInfo method, Type compositeType)
        {
            if (this.methodSideEffects.ContainsKey(method))
            {
                return this.methodSideEffects[method];
            }

            List<Type> matchingSideEffects = this.MatchingSideEffectClasses(method, compositeType);
            MethodSideEffectsModel methodConcerns = MethodSideEffectsModel.CreateForMethod(method, matchingSideEffects);
            this.methodSideEffects.Add(method, methodConcerns);
            return methodConcerns;
        }

        private static List<Type> AsSideEffectsTargetTypes(Type type)
        {
            // Find side-effect declarations
            if (type.IsInterface)
            {
                //TODO: What?
                return GenericInterfacesOf(type);
            }

            //TODO: What?
            return Singleton(type);
        }

        private static List<Type> GenericInterfacesOf(Type type)
        {
            return new List<Type>();
        }

        private static List<Type> Singleton(Type type)
        {
            return new List<Type>();
        }

        private void AddSideEffectDeclaration(Type type)
        {
            if (type.IsClass)
            {
                Type clazz = type;
                var annotation = type.GetAttribute<SideEffectsAttribute>();
                if (annotation != null)
                {
                    Type[] sideEffectClasses = annotation.SideEffectTypes;
                    foreach (Type sideEffectClass in sideEffectClasses)
                    {
                        this.sideEffectDeclarations.Add(new SideEffectDeclaration(sideEffectClass, clazz));
                    }
                }
            }
        }

        private List<Type> MatchingSideEffectClasses(MethodInfo method, Type compositeType)
        {
            var result = new List<Type>();

            foreach (SideEffectDeclaration sideEffectDeclaration in this.sideEffectDeclarations)
            {
                if (sideEffectDeclaration.AppliesTo(method, compositeType))
                {
                    result.Add(sideEffectDeclaration.ModifierClass);
                }
            }
            return result;
        }
    }

    public class SideEffectsAttribute : Attribute
    {
        public Type[] SideEffectTypes;
    }

    public class SideEffectDeclaration : AbstractModifierDeclaration
    {
        public SideEffectDeclaration(Type modifierClass, Type declaredIn) : base(modifierClass, declaredIn)
        {
        }
    }
}