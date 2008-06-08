using System;
using System.Linq;

namespace Alsing.Text
{
    public sealed class Token
    {

        public Token(string text,object[] tags)
        {
            if (tags == null)
                tags = new object[0];

            Text = text;
            Tags = tags;
        }

        public string Text { get; private set; }
        public object[] Tags { get; private set; }

        public override string ToString()
        {
            return Text;
        }

        public bool HasTag(object tag)
        {
            return Tags.Contains(tag);
        }
    }
}
