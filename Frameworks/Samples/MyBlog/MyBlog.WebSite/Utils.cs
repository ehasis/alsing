using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.WebSite
{
    public class Utils
    {
        public static string FormatDate(DateTime date)
        {
            return date.ToLongDateString();
        }

        public static string FormatDateTime(DateTime date)
        {
            return date.ToLongDateString() + " - " + date.ToLongTimeString();
        }

        public static string FormatText(string text)
        {
            return HttpContext.Current.Server.HtmlEncode(text).Replace("\r","<br/>");
        }
    }
}
