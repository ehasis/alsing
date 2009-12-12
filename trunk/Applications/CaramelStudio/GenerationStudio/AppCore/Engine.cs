using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GenerationStudio.Attributes;
using GenerationStudio.Elements;

namespace GenerationStudio.AppCore
{
    public static class Engine
    {
        private static readonly IDictionary<Type, IList<Type>> ChildElements = new Dictionary<Type, IList<Type>>();
        private static readonly IList<Type> ElementTypes = new List<Type>();
        public static Element DragDropElement;
        private static bool mute;
        public static event EventHandler NotifyChange;

        public static void RegisterElementType(Type elementType)
        {
            if (!typeof (Element).IsAssignableFrom(elementType))
                throw new Exception("Type is not an element type");

            if (ElementTypes.Contains(elementType))
                return;


            ElementTypes.Add(elementType);

            object[] attributes = elementType.GetCustomAttributes(typeof (ElementParentAttribute), true);

            foreach (ElementParentAttribute parentAttribute in attributes)
            {
                RegisterElementType(parentAttribute.ParentType);

                if (!ChildElements.ContainsKey(parentAttribute.ParentType))
                    ChildElements.Add(parentAttribute.ParentType, new List<Type>());

                if (!elementType.IsAbstract)
                    ChildElements[parentAttribute.ParentType].Add(elementType);
            }
        }

        public static void RegisterAllElementTypes(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof (Element).IsAssignableFrom(type) && type != typeof (Element))
                    RegisterElementType(type);
            }
        }

        public static List<Type> GetChildTypes(Type type)
        {
            var childTypes = new List<Type>();
            Type currentType = type;
            while (currentType != typeof (Element))
            {
                if (ChildElements.ContainsKey(currentType))
                {
                    childTypes.AddRange(ChildElements[currentType]);
                }
                currentType = currentType.BaseType;
            }

            return childTypes.Distinct().ToList();
        }

        internal static void OnNotifyChange()
        {
            if (mute)
                return;

            if (NotifyChange != null)
                NotifyChange(null, EventArgs.Empty);
        }

        internal static void MuteNotify()
        {
            mute = true;
        }

        internal static void EnableNotify()
        {
            mute = false;
        }
    }
}