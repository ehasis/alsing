using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Alsing.SourceCode;
using GenerationStudio.AppCore;
using GenerationStudio.Elements;
using GenerationStudio.Properties;
using GenerationStudio.TemplateEngine;
using My.Scripting;

namespace GenerationStudio.Gui
{
    public partial class TemplateEditor : UserControl
    {
        public TemplateEditor()
        {
            InitializeComponent();
        }

        public TemplateElement Node { get; set; }

        private void TemplateEditor_Load(object sender, EventArgs e)
        {
            var sl = new SyntaxDefinitionLoader();
            SyntaxDefinition lang = sl.LoadXML(Resources.CSharpTemplate);
            TemplateSyntaxBox.Document.Parser.Init(lang);

            lang = sl.LoadXML(Resources.CSharp);
            SourceSyntaxBox.Document.Parser.Init(lang);
            OutputSyntaxBox.Document.Parser.Init(lang);
        }

        private void MainToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {}

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = OpenDialog.FileName;
                OpenFile(fileName);
            }
        }

        public void OpenFile(string fileName)
        {
            try
            {
                using (var sr = new StreamReader(fileName, Encoding.Default))
                {
                    string text = sr.ReadToEnd();
                    TemplateSyntaxBox.Document.Text = text;
                    Node.FilePath = fileName;
                    Engine.OnNotifyChange();
                }
            }
            catch
            {
                MessageBox.Show("Could not open file");
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (SaveDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = SaveDialog.FileName;
                SaveFileDialog(fileName);
            }
        }

        private void SaveFileDialog(string fileName)
        {
            using (var sw = new StreamWriter(fileName, false, Encoding.Default))
            {
                sw.Write(TemplateSyntaxBox.Document.Text);
                Node.FilePath = fileName;
                Engine.OnNotifyChange();
            }
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            TemplateSyntaxBox.Document.ParseAll(true);

            var sbHeader = new StringBuilder();
            var sbCode = new StringBuilder();

            foreach (Row row in TemplateSyntaxBox.Document)
            {
                foreach (Word word in row)
                {
                    if (word.Span.spanDefinition.Name == "CS Directive")
                    {
                        if (word.Style.Name == "CS Scope")
                            continue;

                        sbHeader.Append(word.Text);
                    }
                }
                if (row.endSpan.spanDefinition.Name == "CS Directive")
                    sbHeader.AppendLine();
            }

            sbCode.Append("output.Write(\"");
            bool special = false;
            foreach (Row row in TemplateSyntaxBox.Document)
            {
                foreach (Word word in row)
                {
                    if (word.Span.spanDefinition.Name != "CS Directive")
                    {
                        if (word.Style.Name == "CS Scope")
                        {
                            if (word.Text == "%>")
                            {
                                if (special)
                                {
                                    sbCode.Append(");");
                                    special = false;
                                }
                                sbCode.AppendLine();
                                sbCode.Append("output.Write(\"");
                            }

                            if (word.Text == "<%")
                            {
                                sbCode.Append("\");");
                                special = false;
                            }

                            if (word.Text == "<%=")
                            {
                                sbCode.Append("\");");
                                sbCode.AppendLine();
                                sbCode.Append("output.Write(");
                                special = true;
                            }
                        }
                        else
                        {
                            if (word.Span.spanDefinition.Name == "Text")
                            {
                                string text = word.Text;
                                text = text.Replace("\\", "\\\\");
                                text = text.Replace("\"", "\\\"");
                                sbCode.Append(text);
                            }
                            else
                            {
                                sbCode.Append(word.Text);
                            }
                        }
                    }
                }
                if (row.endSpan.spanDefinition.Name == "Text")
                {
                    sbCode.Append("\\r\\n");
                }
                else if (row.endSpan.spanDefinition.Name == "CS Directive") {}
                else
                {
                    sbCode.AppendLine();
                }
            }
            sbCode.Append("\");");

            sbHeader.AppendLine("using System;");
            sbHeader.AppendLine("using System.Collections.Generic;");
            sbHeader.AppendLine("using System.Linq;");
            sbHeader.AppendLine("using System.Text;");
            sbHeader.AppendLine("using System.IO;");
            sbHeader.AppendLine("using GenerationStudio.Elements;");
            sbHeader.AppendLine("using GenerationStudio.TemplateEngine;");
            sbHeader.AppendLine("namespace Runtime.Code");
            sbHeader.AppendLine("{");
            sbHeader.AppendLine("   public class MyTemplate : ITemplate");
            sbHeader.AppendLine("   {");
            sbHeader.AppendLine("       public void Render(TextWriter output, RootElement root)");
            sbHeader.AppendLine("       {");
            sbHeader.AppendLine("       " + sbCode);
            sbHeader.AppendLine("       }");
            sbHeader.AppendLine("   }");
            sbHeader.AppendLine("}");
            SourceSyntaxBox.Document.Text = sbHeader.ToString();

            var compiler = new ScriptCompiler();
            try
            {
                ITemplate templateObj = compiler.Compile(sbHeader.ToString());

                var sbOutput = new StringBuilder();
                var sw = new StringWriter(sbOutput);

                templateObj.Render(sw, Node.Root);
//                MyTemplate d = new MyTemplate();
//                d.Render(sw, Node.Root);
                sw.Flush();
                OutputSyntaxBox.Document.Text = sbOutput.ToString();
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }

        private void CodePage_CheckedChanged(object sender, EventArgs e)
        {
            ShowEditors();
        }

        private void ShowEditors()
        {
            if (TemplateButton.Checked)
            {
                TemplateSyntaxBox.Visible = true;
                SourceSyntaxBox.Visible = false;
                OutputSyntaxBox.Visible = false;
            }

            if (SourceButton.Checked)
            {
                TemplateSyntaxBox.Visible = false;
                SourceSyntaxBox.Visible = true;
                OutputSyntaxBox.Visible = false;
            }

            if (OutputButton.Checked)
            {
                TemplateSyntaxBox.Visible = false;
                SourceSyntaxBox.Visible = false;
                OutputSyntaxBox.Visible = true;
            }
        }
    }
}