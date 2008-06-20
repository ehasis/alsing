using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using GenerationStudio.AppCore;
using GenerationStudio.Attributes;
using GenerationStudio.Drawing;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementIcon("GenerationStudio.Images.dummy.bmp")]
    public abstract class Element
    {
        private readonly IList<Element> children = new List<Element>();
        private bool excluded;

        //if the node does not have a parent, it is considered invalid
        [Browsable(false)]
        public virtual bool IsValid
        {
            get { return Parent != null; }
        }

        [Browsable(false)]
        public IEnumerable<Element> AllChildren
        {
            get
            {
                IEnumerable<Element> res = from child in children select child;

                return res;
            }
        }

        [Browsable(false)]
        public IEnumerable<Element> Children
        {
            get
            {
                IEnumerable<Element> res = from child in children where !child.Excluded select child;

                return res;
            }
        }

        [Browsable(false)]
        public Element Parent { get; set; }

        [Browsable(false)]
        public bool Excluded
        {
            get { return excluded; }
            set
            {
                excluded = value;
                OnNotifyChange();
            }
        }

        [Browsable(false)]
        public RootElement Root
        {
            get
            {
                Element current = this;
                while (current.Parent != null)
                    current = current.Parent;

                return (RootElement) current;
            }
        }

        public IList<T> GetChildren<T>() where T : Element
        {
            IEnumerable<Element> res = from child in children where !child.Excluded && child is T select child;

            return res.Cast<T>().ToList();
        }

        public T GetChild<T>() where T : Element
        {
            IEnumerable<Element> res = from child in children where !child.Excluded && child is T select child;

            return res.Cast<T>().FirstOrDefault();
        }

        public virtual int GetSortPriority()
        {
            return 0;
        }


        public virtual string GetDisplayName()
        {
            return "foo";
        }

        public virtual void OnNotifyChange()
        {
            Engine.OnNotifyChange();
        }

        public virtual bool GetDefaultExpanded()
        {
            return true;
        }

        public virtual string GetIconName()
        {
            return GetType().GetElementIconName();
        }

        public virtual Image GetIcon()
        {
            Stream stream = GetType().Assembly.GetManifestResourceStream(GetIconName());
            Image img = Image.FromStream(stream);

            if (Excluded)
            {
                stream = typeof (Element).Assembly.GetManifestResourceStream("GenerationStudio.Images.exclude.gif");
                Image exclude = Image.FromStream(stream);
                Image bw = Utils.MakeGrayscale((Bitmap) img);
                Image tmp = new Bitmap(16, 16);
                Graphics g = Graphics.FromImage(tmp);
                g.DrawImage(bw, 0, 0);
                g.DrawImage(exclude, 0, 0);
                return tmp;
            }
            if (GetErrorsRecursive().Count > 0)
            {
                stream = typeof (Element).Assembly.GetManifestResourceStream("GenerationStudio.Images.error.gif");
                Image exclude = Image.FromStream(stream);
                Image tmp = new Bitmap(16, 16);
                Graphics g = Graphics.FromImage(tmp);
                g.DrawImage(img, 0, 0);
                g.DrawImage(exclude, 0, 0);
                return tmp;
            }

            return img;
        }

        public IList<ElementError> GetErrorsRecursive()
        {
            var allErrors = new List<ElementError>();

            allErrors.AddRange(GetErrors());

            foreach (Element child in AllChildren)
            {
                //ignore excluded items
                if (child.Excluded)
                    continue;

                allErrors.AddRange(child.GetErrorsRecursive());
            }

            return allErrors;
        }

        public virtual IList<ElementError> GetErrors()
        {
            return new List<ElementError>();
        }

        public string GetIconKey()
        {
            return string.Format("{0}|{1}|{2}", GetIconName(), Excluded, GetErrorsRecursive().Count > 0);
        }

        public void ClearChildren()
        {
            children.Clear();
        }

        public void AddChild(Element child)
        {
            children.Add(child);
            child.Parent = this;
            OnNotifyChange();
        }

        public void RemoveChild(Element child)
        {
            if (!child.AllowDelete())
                return;

            children.Remove(child);
            child.Parent = null;
            OnNotifyChange();
        }

        public virtual bool AllowDelete()
        {
            return true;
        }

        public virtual bool HideChildren()
        {
            return false;
        }
    }
}