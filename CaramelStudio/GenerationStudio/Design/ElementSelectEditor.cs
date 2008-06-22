using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using GenerationStudio.Elements;
using System.Drawing;

namespace GenerationStudio.Design
{
    public class ElementSelectEditor : UITypeEditor
    {
        private IWindowsFormsEditorService edSvc;
        private ListBox ItemsListBox;
        private bool handleLostfocus;

        //private void LB_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

        //    if (e.Index == -1)
        //        return;


        //    object li = ItemsListBox.Items[e.Index];
        //    string text = li.ToString();

        //    Brush fg = selected ? SystemBrushes.HighlightText : SystemBrushes.WindowText;

        //    if (selected)
        //    {
        //        const int ofs = 37;
        //        e.Graphics.FillRectangle(SystemBrushes.Window,
        //                                 new Rectangle(ofs, e.Bounds.Top, e.Bounds.Width - ofs, ItemsListBox.ItemHeight));
        //        e.Graphics.FillRectangle(SystemBrushes.Highlight,
        //                                 new Rectangle(ofs + 1, e.Bounds.Top + 1, e.Bounds.Width - ofs - 2,
        //                                               ItemsListBox.ItemHeight - 2));
        //        ControlPaint.DrawFocusRectangle(e.Graphics,
        //                                        new Rectangle(ofs, e.Bounds.Top, e.Bounds.Width - ofs,
        //                                                      ItemsListBox.ItemHeight));
        //    }
        //    else
        //    {
        //        e.Graphics.FillRectangle(SystemBrushes.Window, 0, e.Bounds.Top, e.Bounds.Width, ItemsListBox.ItemHeight);
        //    }


        //    e.Graphics.DrawString(text, e.Font, fg, 38, e.Bounds.Top + 4);

        //    e.Graphics.SetClip(new Rectangle(1, e.Bounds.Top + 2, 34, ItemsListBox.ItemHeight - 4));


        //    e.Graphics.FillRectangle(SystemBrushes.Highlight,
        //                             new Rectangle(1, e.Bounds.Top + 2, 34, ItemsListBox.ItemHeight - 4));

        //    IntPtr hdc = e.Graphics.GetHdc();
        //    var gf = new GDIFont(text, 9);
        //    int a = 0;
        //    IntPtr res = NativeMethods.SelectObject(hdc, gf.hFont);
        //    NativeMethods.SetTextColor(hdc, ColorTranslator.ToWin32(SystemColors.Window));
        //    NativeMethods.SetBkMode(hdc, 0);
        //    NativeMethods.TabbedTextOut(hdc, 3, e.Bounds.Top + 5, "abc", 3, 0, ref a, 0);
        //    NativeMethods.SelectObject(hdc, res);
        //    gf.Dispose();
        //    e.Graphics.ReleaseHdc(hdc);
        //    e.Graphics.DrawRectangle(Pens.Black, new Rectangle(1, e.Bounds.Top + 2, 34, ItemsListBox.ItemHeight - 4));
        //    e.Graphics.ResetClip();
        //}

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null && context.Instance != null && provider != null)
            {
                edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                if (edSvc != null)
                {
                    List<Element> items = GetItems(context);

                    // Create a CheckedListBox and populate it with all the enum values
                    ItemsListBox = new ListBox { DrawMode = DrawMode.Normal, BorderStyle = BorderStyle.None, Sorted = true };
                    ItemsListBox.MouseDown += OnMouseDown;
                    ItemsListBox.DoubleClick += ValueChanged;
                 //   ItemsListBox.DrawItem += LB_DrawItem;
                    ItemsListBox.ItemHeight = 20;
                    ItemsListBox.Height = Math.Min(200, items.Count*SystemFonts.DefaultFont.Height);
                    ItemsListBox.Width = 180;


                    
                    
                    foreach (var element in items)
                    {
                        ItemsListBox.Items.Add(element);
                    }
                    edSvc.DropDownControl(ItemsListBox);
                    if (ItemsListBox.SelectedItem != null)
                        return ItemsListBox.SelectedItem;
                }
            }

            return value;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!handleLostfocus && ItemsListBox.ClientRectangle.Contains(ItemsListBox.PointToClient(new Point(e.X, e.Y))))
            {
                ItemsListBox.LostFocus += ValueChanged;
                handleLostfocus = true;
            }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            if (edSvc != null)
            {
                edSvc.CloseDropDown();
            }
        }

        public override void PaintValue(PaintValueEventArgs e)
        {            
        //    var bp = new Bitmap(e.Bounds.Width, e.Bounds.Height);
        //    Graphics g = Graphics.FromImage(bp);

            //g.Clear(Color.White);
            //e.Graphics.DrawImage(bp, e.Bounds.Left, e.Bounds.Top);


            var item = (Element)e.Value;
            Image icon = item.GetIcon();
            Rectangle bounds = new Rectangle(3, -1, 16, 16);

            e.Graphics.DrawImageUnscaledAndClipped(icon,bounds);
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        private static List<Element> GetItems(ITypeDescriptorContext context) {
            ElementSelectAttribute selector = GetSelector(context);
            string selectPath = selector.Path;

            var tmp = (Element)context.Instance;

            while(selectPath.StartsWith("^"))
            {
                tmp = tmp.Parent;
                selectPath = selectPath.Substring(1);
            }

            var container = (Element)tmp.GetType().GetProperty(selectPath).GetValue(tmp, null);
            var items = container.AllChildren.ToList();
            return items;
        }

        private static ElementSelectAttribute GetSelector(ITypeDescriptorContext context) {
            string propertyName = context.PropertyDescriptor.Name;
            PropertyInfo pi = context.Instance.GetType().GetProperty(propertyName);

            return pi.GetCustomAttributes(typeof(ElementSelectAttribute), true).Cast<ElementSelectAttribute>().FirstOrDefault();
        }
    }

    public class ElementSelectAttribute : Attribute
    {
        public Type Type { get; set; }
        public string Path { get; set; }

        public ElementSelectAttribute(Type type,string path)
        {
            Type = type;
            Path = path;
        }

        public ElementSelectAttribute(string path)
        {
            Type = null;
            Path = path;
        }  
    }
}