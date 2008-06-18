using System.Collections.Generic;
using System.Windows.Forms;
using GenerationStudio.Elements;
using GenerationStudio.Forms.Docking;

namespace GenerationStudio.Gui
{
    public partial class MainForm : IHost
    {
        private Dictionary<Element, Dictionary<string, Control>> elementEditors =
            new Dictionary<Element, Dictionary<string, Control>>();

        #region IHost Members

        public T GetEditor<T>(Element owner, string name) where T : Control, new()
        {
            if (!elementEditors.ContainsKey(owner))
                elementEditors.Add(owner, new Dictionary<string, Control>());

            if (!elementEditors[owner].ContainsKey(name))
                elementEditors[owner].Add(name, new T());

            var editor = (T) elementEditors[owner][name];
            return editor;
        }

        public void ShowEditor(Control editor)
        {
            var form = new DocumentForm();
            form.SetContent(editor, "Blah");
            form.Show(DockPanel);
        }

        public void RefreshProjectTree()
        {
            FillTreeView();
        }

        #endregion
    }
}