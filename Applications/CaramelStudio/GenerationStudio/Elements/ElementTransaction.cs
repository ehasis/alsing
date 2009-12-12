using System;
using System.Collections.Generic;

namespace GenerationStudio.Elements
{
    public class ElementTransaction
    {
        [NonSerialized] private IDictionary<string, Element> oldChildren;


        public ElementTransaction(IEnumerable<Element> children)
        {
            oldChildren = new Dictionary<string, Element>();
            foreach (Element child in children)
            {
                string key = string.Format("{0}|{1}", child.GetType().Name, child.GetDisplayName());
                oldChildren[key] = child;
            }
        }

        public T GetNamedChild<T>(string name) where T : NamedElement, new()
        {
            string key = string.Format("{0}|{1}", typeof (T).Name, name);
            if (oldChildren.ContainsKey(key))
                return (T) oldChildren[key];

            var newChild = new T {Name = name};
            return newChild;
        }
    }
}