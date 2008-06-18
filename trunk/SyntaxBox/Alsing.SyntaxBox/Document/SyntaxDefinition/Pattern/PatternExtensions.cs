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
    public partial class Pattern
    {
        /// <summary>
        /// For public use only
        /// </summary>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool HasSeparators(string text, int position)
        {
            return (CharIsSeparator(text, position - 1) && CharIsSeparator(text, position + StringPattern.Length));
        }


        private bool CharIsSeparator(string Text, int Position)
        {
            if (Position < 0 || Position >= Text.Length)
                return true;

            string s = Text.Substring(Position, 1);
            if (Separators.IndexOf(s) >= 0)
                return true;
            return false;
        }

        /// <summary>
        /// Returns the index of the pattern in a string
        /// </summary>
        /// <param name="this"></param>
        /// <param name="text">The string in which to find the pattern</param>
        /// <param name="startPosition">Start index in the string</param>
        /// <param name="matchCase">true if a case sensitive match should be performed</param>
        /// <param name="separators"></param>
        /// <returns>A PatternScanResult containing information on where the pattern was found and also the text of the pattern</returns>
        public PatternScanResult IndexIn(string text, int startPosition, bool matchCase, string separators)
        {
            if (separators == null) {}
            else
            {
                Separators = separators;
            }

            if (!IsComplex)
            {
                if (!IsKeyword)
                    return SimpleFind(text, startPosition, matchCase);

                return SimpleFindKeyword(text, startPosition, matchCase);
            }
            if (!IsKeyword)
                return ComplexFind(text, startPosition);

            return ComplexFindKeyword(text, startPosition);
        }


        private PatternScanResult SimpleFind(string text, int startPosition, bool matchCase)
        {
            int Position = matchCase
                               ? text.IndexOf(StringPattern, startPosition)
                               : text.ToLowerInvariant().IndexOf(LowerStringPattern, startPosition);

            PatternScanResult Result;
            if (Position >= 0)
            {
                Result.Index = Position;
                Result.Token = text.Substring(Position, StringPattern.Length);
            }
            else
            {
                Result.Index = 0;
                Result.Token = "";
            }

            return Result;
        }

        private PatternScanResult SimpleFindKeyword(string text, int startPosition, bool matchCase)
        {
            PatternScanResult res;
            while (true)
            {
                res = SimpleFind(text, startPosition, matchCase);
                if (res.Token == "")
                    return res;

                if (CharIsSeparator(text, res.Index - 1) && CharIsSeparator(text, res.Index + res.Token.Length))
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


        private PatternScanResult ComplexFindKeyword(string text, int startPosition)
        {
            PatternScanResult res;
            while (true)
            {
                res = ComplexFind(text, startPosition);
                if (res.Token == "")
                    return res;

                if (CharIsSeparator(text, res.Index - 1) && CharIsSeparator(text, res.Index + res.Token.Length))
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

        private PatternScanResult ComplexFind(string text, int startPosition)
        {
            MatchCollection mc = rx.Matches(text);
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