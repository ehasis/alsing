using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Alsing.Irc
{
    public static class Patterns
    {
        public static readonly Regex ReplyCodeRegex = new Regex("^:[^ ]+? ([0-9]{3}) (.+)$", RegexOptions.Compiled);
        public static readonly Regex PingRegex = new Regex("^PING :(.*)", RegexOptions.Compiled);
        public static readonly Regex ErrorRegex = new Regex("^ERROR :.*", RegexOptions.Compiled);
        public static readonly Regex ActionRegex = new Regex("^:(.+)!(.+) PRIVMSG (.).* :\x1ACTION .*\x1$", RegexOptions.Compiled);
        public static readonly Regex CtcpRequestRegex = new Regex("^:(.+)!(.+) PRIVMSG .* :\x1.*\x1$", RegexOptions.Compiled);
        public static readonly Regex MessageRegex = new Regex("^:(.+)!(.+) PRIVMSG (.[^:]+) :(.*)$", RegexOptions.Compiled);
        public static readonly Regex CtcpReplyRegex = new Regex("^:.*? NOTICE .* :" + "\x1" + ".*" + "\x1" + "$", RegexOptions.Compiled);
        public static readonly Regex NoticeRegex = new Regex("^:.*? NOTICE (.).* :(.*)$", RegexOptions.Compiled);
        public static readonly Regex InviteRegex = new Regex("^:.*? INVITE .* .*$", RegexOptions.Compiled);
        public static readonly Regex JoinRegex = new Regex("^:(.+)!(.+) JOIN :(.+)$", RegexOptions.Compiled);
        public static readonly Regex TopicRegex = new Regex("^:.*? TOPIC .* :.*$", RegexOptions.Compiled);
        public static readonly Regex NickRegex = new Regex("^:(.+)!(.+) NICK :(.+)$", RegexOptions.Compiled);
        public static readonly Regex KickRegex = new Regex("^:.*? KICK .* .*$", RegexOptions.Compiled);
        public static readonly Regex PartRegex = new Regex("^:(.+)!(.+) PART (.+)$", RegexOptions.Compiled);
        public static readonly Regex ModeRegex = new Regex("^:.*? MODE (.*) .*$", RegexOptions.Compiled);
        public static readonly Regex QuitRegex = new Regex("^:(.+)!(.+) QUIT :.*$", RegexOptions.Compiled);
    }
}

