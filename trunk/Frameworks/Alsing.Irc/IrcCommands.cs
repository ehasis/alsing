using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alsing.Irc
{
    public class IrcCommands
    {
        private Action<string> writeLine;
        public IrcCommands(Action<string> writeLine)
        {
            this.writeLine = writeLine;
        }

        private void WriteLine(string data)
        {
            writeLine(data);
            Console.WriteLine(data);
        }

        public void Op(string channel, string nickname)
        {
            WriteLine(Rfc2812.Mode(channel, "+o " + nickname));
        }

        public void Deop(string channel, string nickname)
        {
            WriteLine(Rfc2812.Mode(channel, "-o " + nickname));
        }

        public void Voice(string channel, string nickname)
        {
            WriteLine(Rfc2812.Mode(channel, "+v " + nickname));
        }

        public void Devoice(string channel, string nickname)
        {
            WriteLine(Rfc2812.Mode(channel, "-v " + nickname));
        }

        public void Ban(string channel)
        {
            WriteLine(Rfc2812.Mode(channel, "+b"));
        }

        public void Ban(string channel, string hostmask)
        {
            WriteLine(Rfc2812.Mode(channel, "+b " + hostmask));
        }

        public void Unban(string channel, string hostmask)
        {
            WriteLine(Rfc2812.Mode(channel, "-b " + hostmask));
        }

        // RFC commands
        public void RfcPass(string password)
        {
            WriteLine(Rfc2812.Pass(password));
        }

        public void RfcUser(string username, int usermode, string realname)
        {
            WriteLine(Rfc2812.User(username, usermode, realname));
        }

        public void RfcOper(string name, string password)
        {
            WriteLine(Rfc2812.Oper(name, password));
        }

        public void RfcPrivmsg(string destination, string message)
        {
            WriteLine(Rfc2812.Privmsg(destination, message));
        }

        public void RfcNotice(string destination, string message)
        {
            WriteLine(Rfc2812.Notice(destination, message));
        }

        public void RfcJoin(string channel)
        {
            WriteLine(Rfc2812.Join(channel));
        }

        public void RfcJoin(string[] channels)
        {
            WriteLine(Rfc2812.Join(channels));
        }

        public void RfcJoin(string channel, string key)
        {
            WriteLine(Rfc2812.Join(channel, key));
        }

        public void RfcJoin(string[] channels, string[] keys)
        {
            WriteLine(Rfc2812.Join(channels, keys));
        }

        public void RfcPart(string channel)
        {
            WriteLine(Rfc2812.Part(channel));
        }

        public void RfcPart(string[] channels)
        {
            WriteLine(Rfc2812.Part(channels));
        }

        public void RfcPart(string channel, string partmessage)
        {
            WriteLine(Rfc2812.Part(channel, partmessage));
        }

        public void RfcPart(string[] channels, string partmessage)
        {
            WriteLine(Rfc2812.Part(channels, partmessage));
        }

        public void RfcKick(string channel, string nickname)
        {
            WriteLine(Rfc2812.Kick(channel, nickname));
        }

        public void RfcKick(string[] channels, string nickname)
        {
            WriteLine(Rfc2812.Kick(channels, nickname));
        }

        public void RfcKick(string channel, string[] nicknames)
        {
            WriteLine(Rfc2812.Kick(channel, nicknames));
        }

        public void RfcKick(string[] channels, string[] nicknames)
        {
            WriteLine(Rfc2812.Kick(channels, nicknames));
        }

        public void RfcKick(string channel, string nickname, string comment)
        {
            WriteLine(Rfc2812.Kick(channel, nickname, comment));
        }

        public void RfcKick(string[] channels, string nickname, string comment)
        {
            WriteLine(Rfc2812.Kick(channels, nickname, comment));
        }

        public void RfcKick(string channel, string[] nicknames, string comment)
        {
            WriteLine(Rfc2812.Kick(channel, nicknames, comment));
        }

        public void RfcKick(string[] channels, string[] nicknames, string comment)
        {
            WriteLine(Rfc2812.Kick(channels, nicknames, comment));
        }

        public void RfcMotd()
        {
            WriteLine(Rfc2812.Motd());
        }

        public void RfcMotd(string target)
        {
            WriteLine(Rfc2812.Motd(target));
        }

        public void RfcLuser()
        {
            WriteLine(Rfc2812.Luser());
        }

        public void RfcLuser(string mask)
        {
            WriteLine(Rfc2812.Luser(mask));
        }

        public void RfcLuser(string mask, string target)
        {
            WriteLine(Rfc2812.Luser(mask, target));
        }

        public void RfcVersion()
        {
            WriteLine(Rfc2812.Version());
        }

        public void RfcVersion(string target)
        {
            WriteLine(Rfc2812.Version(target));
        }

        public void RfcStats()
        {
            WriteLine(Rfc2812.Stats());
        }
        
        public void RfcStats(string query)
        {
            WriteLine(Rfc2812.Stats(query));
        }

        public void RfcStats(string query, string target)
        {
            WriteLine(Rfc2812.Stats(query, target));
        }

        public void RfcLinks()
        {
            WriteLine(Rfc2812.Links());
        }

        public void RfcLinks(string server_mask)
        {
            WriteLine(Rfc2812.Links(server_mask));
        }

        public void RfcLinks(string remote_server, string server_mask)
        {
            WriteLine(Rfc2812.Links(remote_server, server_mask));
        }

        public void RfcTime()
        {
            WriteLine(Rfc2812.Time());
        }

        public void RfcTime(string target)
        {
            WriteLine(Rfc2812.Time(target));
        }

        public void RfcConnect(string target_server, string port)
        {
            WriteLine(Rfc2812.Connect(target_server, port));
        }

        public void RfcConnect(string target_server, string port, string remote_server)
        {
            WriteLine(Rfc2812.Connect(target_server, port, remote_server));
        }

        public void RfcTrace()
        {
            WriteLine(Rfc2812.Trace());
        }

        public void RfcTrace(string target)
        {
            WriteLine(Rfc2812.Trace(target));
        }

        public void RfcAdmin()
        {
            WriteLine(Rfc2812.Admin());
        }

        public void RfcAdmin(string target)
        {
            WriteLine(Rfc2812.Admin(target));
        }

        public void RfcInfo()
        {
            WriteLine(Rfc2812.Info());
        }

        public void RfcInfo(string target)
        {
            WriteLine(Rfc2812.Info(target));
        }

        public void RfcServlist()
        {
            WriteLine(Rfc2812.Servlist());
        }

        public void RfcServlist(string mask)
        {
            WriteLine(Rfc2812.Servlist(mask));
        }

        public void RfcServlist(string mask, string type)
        {
            WriteLine(Rfc2812.Servlist(mask, type));
        }

        public void RfcSquery(string servicename, string text)
        {
            WriteLine(Rfc2812.Squery(servicename, text));
        }

        public void RfcList(string channel)
        {
            WriteLine(Rfc2812.List(channel));
        }

        public void RfcList(string[] channels)
        {
            WriteLine(Rfc2812.List(channels));
        }

        public void RfcList(string channel, string target)
        {
            WriteLine(Rfc2812.List(channel, target));
        }

        public void RfcList(string[] channels, string target)
        {
            WriteLine(Rfc2812.List(channels, target));
        }

        public void RfcNames(string channel)
        {
            WriteLine(Rfc2812.Names(channel));
        }

        public void RfcNames(string[] channels)
        {
            WriteLine(Rfc2812.Names(channels));
        }

        public void RfcNames(string channel, string target)
        {
            WriteLine(Rfc2812.Names(channel, target));
        }

        public void RfcNames(string[] channels, string target)
        {
            WriteLine(Rfc2812.Names(channels, target));
        }

        public void RfcTopic(string channel)
        {
            WriteLine(Rfc2812.Topic(channel));
        }

        public void RfcTopic(string channel, string newtopic)
        {
            WriteLine(Rfc2812.Topic(channel, newtopic));
        }

        public void RfcMode(string target)
        {
            WriteLine(Rfc2812.Mode(target));
        }

        public void RfcMode(string target, string newmode)
        {
            WriteLine(Rfc2812.Mode(target, newmode));
        }

        public void RfcService(string nickname, string distribution, string info)
        {
            WriteLine(Rfc2812.Service(nickname, distribution, info));
        }

        public void RfcInvite(string nickname, string channel)
        {
            WriteLine(Rfc2812.Invite(nickname, channel));
        }

        public void RfcNick(string newnickname)
        {
            WriteLine(Rfc2812.Nick(newnickname));
        }

        public void RfcWho()
        {
            WriteLine(Rfc2812.Who());
        }

        public void RfcWho(string mask)
        {
            WriteLine(Rfc2812.Who(mask));
        }

        public void RfcWho(string mask, bool ircop)
        {
            WriteLine(Rfc2812.Who(mask, ircop));
        }

        public void RfcWhois(string mask)
        {
            WriteLine(Rfc2812.Whois(mask));
        }

        public void RfcWhois(string[] masks)
        {
            WriteLine(Rfc2812.Whois(masks));
        }

        public void RfcWhois(string target, string mask)
        {
            WriteLine(Rfc2812.Whois(target, mask));
        }

        public void RfcWhois(string target, string[] masks)
        {
            WriteLine(Rfc2812.Whois(target, masks));
        }

        public void RfcWhowas(string nickname)
        {
            WriteLine(Rfc2812.Whowas(nickname));
        }

        public void RfcWhowas(string[] nicknames)
        {
            WriteLine(Rfc2812.Whowas(nicknames));
        }

        public void RfcWhowas(string nickname, string count)
        {
            WriteLine(Rfc2812.Whowas(nickname, count));
        }

        public void RfcWhowas(string[] nicknames, string count)
        {
            WriteLine(Rfc2812.Whowas(nicknames, count));
        }

        public void RfcWhowas(string nickname, string count, string target)
        {
            WriteLine(Rfc2812.Whowas(nickname, count, target));
        }

        public void RfcWhowas(string[] nicknames, string count, string target)
        {
            WriteLine(Rfc2812.Whowas(nicknames, count, target));
        }

        public void RfcKill(string nickname, string comment)
        {
            WriteLine(Rfc2812.Kill(nickname, comment));
        }

        public void RfcPing(string server)
        {
            WriteLine(Rfc2812.Ping(server));
        }

        public void RfcPing(string server, string server2)
        {
            WriteLine(Rfc2812.Ping(server, server2));
        }

        public void RfcPong(string server)
        {
            WriteLine(Rfc2812.Pong(server));
        }

        public void RfcPong(string server, string server2)
        {
            WriteLine(Rfc2812.Pong(server, server2));
        }

        public void RfcAway()
        {
            WriteLine(Rfc2812.Away());
        }

        public void RfcAway(string text)
        {
            WriteLine(Rfc2812.Away(text));
        }

        public void RfcRehash()
        {
            WriteLine(Rfc2812.Rehash());
        }

        public void RfcDie()
        {
            WriteLine(Rfc2812.Die());
        }

        public void RfcRestart()
        {
            WriteLine(Rfc2812.Restart());
        }

        public void RfcSummon(string user)
        {
            WriteLine(Rfc2812.Summon(user));
        }

        public void RfcSummon(string user, string target)
        {
            WriteLine(Rfc2812.Summon(user, target));
        }

        public void RfcSummon(string user, string target, string channel)
        {
            WriteLine(Rfc2812.Summon(user, target, channel));
        }

        public void RfcUsers()
        {
            WriteLine(Rfc2812.Users());
        }

        public void RfcUsers(string target)
        {
            WriteLine(Rfc2812.Users(target));
        }

        public void RfcWallops(string text)
        {
            WriteLine(Rfc2812.Wallops(text));
        }

        public void RfcUserhost(string nickname)
        {
            WriteLine(Rfc2812.Userhost(nickname));
        }

        public void RfcUserhost(string[] nicknames)
        {
            WriteLine(Rfc2812.Userhost(nicknames));
        }

        public void RfcIson(string nickname)
        {
            WriteLine(Rfc2812.Ison(nickname));
        }

        public void RfcIson(string[] nicknames)
        {
            WriteLine(Rfc2812.Ison(nicknames));
        }

        public void RfcQuit()
        {
            WriteLine(Rfc2812.Quit());
        }

        public void RfcQuit(string quitmessage)
        {
            WriteLine(Rfc2812.Quit(quitmessage));
        }

        public void RfcSquit(string server, string comment)
        {
            WriteLine(Rfc2812.Squit(server, comment));
        }

        public void Raw(string command)
        {
            WriteLine(command);
        }
    }
}
