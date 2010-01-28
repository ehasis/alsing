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
    public partial class WhisperForm : IrcForm
    {
        public string User { get; set; }

        public WhisperForm()
        {
            InitializeComponent();
        }

        private void WhisperForm_Load(object sender, EventArgs e)
        {

        }

        protected override void OnEnterKey(string text)
        {
            Connection.Commands.RfcPrivmsg(User, text);
        }

        public override bool ContainsUser(string user)
        {
            return this.User.ToLowerInvariant() == user.ToLowerInvariant();
        }
    }
}
