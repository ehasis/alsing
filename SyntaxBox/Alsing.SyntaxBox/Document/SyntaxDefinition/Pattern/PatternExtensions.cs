// *
// * Copyright (C) 2008 Roger Alsing : http://www.RogerAlsing.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System.Text.RegularExpressions;

namespace Alsing.SourceCode
{
    public static class PatternExtensions
    {
        /// <summary>
        /// For public use only
        /// </summary>
        /// <param name="self"></param>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool HasSeparators(this Pattern self,string text, int position)
        {
            return (self.CharIsSeparator(text, position - 1) && self.CharIsSeparator(text, position + self.StringPattern.Length));
        }


        private static bool CharIsSeparator(this Pattern self,string Text, int Position)
        {
            if (Position < 0 || Position >= Text.Length)
                return true;

            string s = Text.Substring(Position, 1);
            if (self.Separators.IndexOf(s) >= 0)
                return true;
            return false;
        }

        /// <summary>
        /// Returns the index of the pattern in a string
        /// </summary>
        /// <param name="self"></param>
        /// <param name="text">The string in which to find the pattern</param>
        /// <param name="startPosition">Start index in the string</param>
        /// <param name="matchCase">true if a case sensitive match should be performed</param>
        /// <param name="separators"></param>
        /// <returns>A PatternScanResult containing information on where the pattern was found and also the text of the pattern</returns>
        public static PatternScanResult IndexIn(this Pattern self, string text, int startPosition, bool matchCase, string separators)
        {
            if (separators == null) { }
            else
            {
                self.Separators = separators;
            }

            if (!self.IsComplex)
            {
                if (!self.IsKeyword)
                    return self.SimpleFind(text, startPosition, matchCase);

                return self.SimpleFindKeyword(text, startPosition, matchCase);
            }
            if (!self.IsKeyword)
                return self.ComplexFind(text, startPosition);

            return self.ComplexFindKeyword(text, startPosition);
        }


        private static PatternScanResult SimpleFind(this Pattern self, string text, int startPosition, bool matchCase)
        {
            int Position = matchCase ? text.IndexOf(self.StringPattern, startPosition) : text.ToLowerInvariant().IndexOf(self.LowerStringPattern, startPosition);

            PatternScanResult Result;
            if (Position >= 0)
            {
                Result.Index = Position;
                Result.Token = text.Substring(Position, self.StringPattern.Length);
            }
            else
            {
                Result.Index = 0;
                Result.Token = "";
            }

            return Result;
        }

        private static PatternScanResult SimpleFindKeyword(this Pattern self, string text, int startPosition,
                                                    bool matchCase)
        {
            PatternScanResult res;
            while (true)
            {
                res = self.SimpleFind(text, startPosition, matchCase);
                if (res.Token == "")
                    return res;

                if (self.CharIsSeparator(text, res.Index - 1) && self.CharIsSeparator(text, res.Index + res.Token.Length))
                    return res;

                startPosition = res.Index + 1;
                if (startPosition >= text.Length)
                {
                    res.Token = "";
                    res.Index = 0;
                    return res;
                }
            }
        }


        private static PatternScanResult ComplexFindKeyword(this Pattern self, string text, int startPosition)
        {
            PatternScanResult res;
            while (true)
            {
                res = self.ComplexFind(text, startPosition);
                if (res.Token == "")
                    return res;

                if (self.CharIsSeparator(text, res.Index - 1) && self.CharIsSeparator(text, res.Index + res.Token.Length))
                    return res;

                startPosition = res.Index + 1;
                if (startPosition >= text.Length)
                {
                    res.Token = "";
                    res.Index = 0;
                    return res;
                }
            }
        }

        private static PatternScanResult ComplexFind(this Pattern self, string text, int startPosition)
        {
            MatchCollection mc = self.rx.Matches(text);
            foreach (Match m in mc)
            {
                int pos = m.Index;
                string p = m.Value;
                if (pos >= startPosition)
                {
                    PatternScanResult t;
                    t.Index = pos;
                    t.Token = p;
                    return t;
                }
            }
            PatternScanResult res;
            res.Index = 0;
            res.Token = "";
            return res;
        }
    }
}