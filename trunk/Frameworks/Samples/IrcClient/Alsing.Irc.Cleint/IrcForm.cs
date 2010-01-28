using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alsing.Irc.Cleint
{
    public partial class IrcForm : Form
    {
 //        0 white
 //1 black
 //2 blue     (navy)
 //3 green
 //4 red
 //5 brown    (maroon)
 //6 purple
 //7 orange   (olive)
 //8 yellow
 //9 lt.green (lime)
 //10 teal    (a kinda green/blue cyan)
 //11 lt.cyan (cyan ?) (aqua)
 //12 lt.blue (royal)
 //13 pink    (light purple) (fuchsia)
 //14 grey
 //15 lt.grey (silver)


        private static readonly Color[] colors = new Color[] 
        {
            Color.White,
            Color.Black,
            Color.Navy,
            Color.Green,
            Color.Red,
            Color.Maroon,
            Color.Purple,
            Color.Olive,
            Color.Yellow,
            Color.Lime,
            Color.Teal,
            Color.Aqua,
            Color.RoyalBlue,
            Color.Pink,
            Color.Gray,
            Color.Silver
        };

        public IrcForm()
        {
            InitializeComponent();
        }

        public IrcConnection Connection { get; set; }

        public void Write(string message)
        {
            txtOutput.SelectionStart = txtOutput.TextLength;
            string[] parts = message.Split((char)3);
            txtOutput.SelectedText = parts[0];
            //if (message.Contains("0,2"))
            //{
            //}
            for (int i = 1; i < parts.Length;i++ )
            {
                int state = 0;
                string colorCode = "0";
                string backColorCode = "0";
                string partText = "";
                foreach (char c in parts[i])
                {
                    if (char.IsDigit(c) && state == 0)
                    {
                        colorCode += c;
                        continue;
                    }
                    if (c == ',' && state == 0)
                    {
                        state = 1;
                        continue;
                    }
                    if (char.IsDigit(c) && state == 1)
                    {
                        backColorCode += c;
                        continue;
                    }
                    state = 2;

                    partText += c;
                }
                int cc = int.Parse(colorCode);
                int bgcc = int.Parse(backColorCode);
                txtOutput.SelectionColor = colors[cc];
                txtOutput.SelectionBackColor = colors[bgcc];
                txtOutput.SelectedText = partText;
            }
            txtOutput.ScrollToCaret();
        }

        public void Write(string message,Color foreColor)
        {
            txtOutput.SelectionColor = foreColor;
            Write(message);
        }

        public void WriteLine(string message)
        {
            Write(message + Environment.NewLine);
        }

        private void IrcForm_Load(object sender, EventArgs e)
        {
            
        }

        private void IrcForm_Activated(object sender, EventArgs e)
        {
            txtInput.Focus();
        }

        public void WriteLine(string message, Color foreColor, Color backColor)
        {
            txtOutput.SelectionColor = foreColor;
            txtOutput.SelectionBackColor = backColor;
            WriteLine(message);
        }

        public void WriteLine(string message, Color foreColor)
        {
            txtOutput.SelectionColor = foreColor;
            WriteLine(message);
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string text = txtInput.Text.Trim();
                if (text.StartsWith("/"))
                {
                    string command = text.Substring(1);
                    Connection.Commands.Raw(command);                    
                }
                else
                {
                    OnEnterKey(text);
                    txtInput.Text = "";
                }
            }
        }

        protected virtual void OnEnterKey(string text)
        {
                
        }

        public virtual bool ContainsUser(string user)
        {
            return false;
        }

        private void IrcForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
