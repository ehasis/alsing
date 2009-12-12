// *
// * Copyright (C) 2008 Roger Alsing : http://www.rogeralsing.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace Alsing
{
    public static class FormatExtensions
    {
        /// <summary>
        /// Uses the string as a format
        /// </summary>
        /// <param name="format">A String reference</param>
        /// <param name="args">Object parameters that should be formatted</param>
        /// <returns>Formatted string</returns>
        public static string FormatWith(this string format, params object[] args)
        {
            format.Require("format")
                .NotNullOrEmpty();

            return string.Format(format, args);
        }


        /// <summary>
        /// Applies a format to the item
        /// </summary>
        /// <param name="item">Item to format</param>
        /// <param name="format">Format string</param>
        /// <returns>Formatted string</returns>
        public static string FormatAs(this object item, string format)
        {
            format.Require("format")
                .NotNullOrEmpty();

            return string.Format(format, item);
        }

        /// <summary>
        /// Indicates whether the specified String object is null or an empty string
        /// </summary>
        /// <param name="item">A String reference</param>
        /// <returns>True if null or emptry</returns>
        public static bool IsNullOrEmpty(this string item)
        {            
            return string.IsNullOrEmpty(item);
        }

        /// <summary>
        /// Returns a copy of this string in Proper Case format
        /// </summary>
        /// <param name="item">A string Reference</param>
        /// <returns>Formatted string</returns>
        public static string ToProperCase(this string item)
        {
            item.Require("item")
                .NotNull();

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(item.ToLower());
        }

        /// <summary>
        /// Performs a regex match on a string
        /// </summary>
        /// <param name="text">A String reference</param>
        /// <param name="regexPattern">A regex pattern string</param>
        /// <returns>True if pattern matches the input string</returns>
        public static bool Match(this string text, string regexPattern)
        {
            regexPattern.Require("regexPattern")
                .NotNull();

            if (text == null)
                return false;

            var regex = new Regex(regexPattern);
            Match m = regex.Match(text);
            return m.Success;
        }

        /// <summary>
        /// Performs a wildcard pattern match on a string
        /// </summary>
        /// <param name="text">A String reference</param>
        /// <param name="pattern">A wildcard pattern string</param>
        /// <returns>True if pattern matches the input string</returns>
        public static bool Like(this string text, string pattern)
        {
            pattern.Require("pattern")
                .NotNull();

            if (text == null)
                return false;


            // Stolen somewhere a long time ago
            //
            // Notify me (www.rogeralsing.com) if you know the source of it

            text = text.ToLower();
            pattern = pattern.ToLower();

            int cp = 0, mp = 0;

            int i = 0;
            int j = 0;
            while ((i < text.Length) && (pattern[j] != '%'))
            {
                if ((pattern[j] != text[i]) && (pattern[j] != '?'))
                {
                    return false;
                }
                i++;
                j++;
            }

            while (i < text.Length)
            {
                if (j < pattern.Length && pattern[j] == '%')
                {
                    if ((j++) >= pattern.Length)
                    {
                        return true;
                    }
                    mp = j;
                    cp = i + 1;
                }
                else if (j < pattern.Length && (pattern[j] == text[i] || pattern[j] == '?'))
                {
                    j++;
                    i++;
                }
                else
                {
                    j = mp;
                    i = cp++;
                }
            }

            while (j < pattern.Length && pattern[j] == '%')
            {
                j++;
            }
            return j >= pattern.Length;
        }
    }
}