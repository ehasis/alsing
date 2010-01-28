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
    public partial class ServerForm : IrcForm
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Commands.RfcQuit();
            Connection.Close();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
