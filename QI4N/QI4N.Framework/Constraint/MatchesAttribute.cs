namespace QI4N.Framework.API
{
    using System.Text.RegularExpressions;

    public class MatchesAttribute : ConstraintAttribute
    {
        private readonly string pattern;

        public MatchesAttribute(string pattern)
        {
            this.pattern = pattern;
        }

        public override bool IsValid(object value)
        {
            string strValue = string.Format("{0}", value);
            bool match = Regex.IsMatch(strValue, this.pattern);
            return match;
        }
    }
}