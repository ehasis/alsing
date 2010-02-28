using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Structural.AST;
using Structural.Projections;

namespace Structural
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs args)
        {
            
        }

        private void Form1_Paint(object sender, PaintEventArgs args)
        {
            var e = new StructureEngine();

            var i = new If();
            
            i.Condition = null;
            e.Root.Body.Statements.Add(i);

            var v = new VariableDeclaration();
            v.Name = null;
            v.InitialValue = null;

            var p = new Print();

            var s1 = new StringLiteral
                        {
                                Value = "hej du glade Tag en spade och..."
                        };
            var s2 = new StringLiteral
            {
                Value =  "Mer text",
            };

            var exp = new AddOperator()
            {
                LeftOperand = s1,
                RightOperand = s2,
            };

            p.Value = exp;

            i.Body.Statements.Add(v);
            i.Body.Statements.Add(p);

            args.Graphics.Clear(Color.White);
            e.Render(args.Graphics);

        }
    }
}
