using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Text;
using Alsing.Text.PatternMatchers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alsing.Core.Tests.Text
{
    [TestClass]
    public class TokenizerTests
    {
        private static readonly object TestTag = new object();

        [TestMethod]
        public void ParseCaseSensitiveTokens()
        {
            var tokenizer = new Tokenizer();

            tokenizer.AddToken("Roger", true, true, TestTag);
            tokenizer.AddToken("Alsing", true, true, TestTag);

            const string text = @"Roger ROGER rogeR Alsing ALSING __Roger__ __Alsing__";
            //                   "XXXXX             XXXXXX                            "

            // only two tokens should be found, the rest 
            // are either wrong casing or do not have separators next to them

            tokenizer.Text = text;
            var tokens = tokenizer.Tokenize();

            var testTokens = from token in tokens
                             where token.HasTag(TestTag)
                             select token;

            Assert.AreEqual(2, testTokens.ToList().Count);
        }

        [TestMethod]
        public void ParseCaseInsensitiveTokens()
        {
            var tokenizer = new Tokenizer();

            tokenizer.AddToken("Roger", false, true, TestTag);
            tokenizer.AddToken("Alsing", false, true, TestTag);

            const string text = @"Roger ROGER rogeR Alsing ALSING __Roger__ __Alsing__";
            //                   "XXXXX XXXXX XXXXX XXXXXX XXXXXX   -----     ------  "

            // only two tokens should be found, the rest 
            // are either wrong casing or do not have separators next to them

            tokenizer.Text = text;
            var tokens = tokenizer.Tokenize();

            var testTokens = from token in tokens
                             where token.HasTag(TestTag)
                             select token;

            Assert.AreEqual(5, testTokens.ToList().Count);
        }

        [TestMethod]
        public void ParseCaseSensitiveTokensWithoutSeparators()
        {
            var tokenizer = new Tokenizer();

            tokenizer.AddToken("Roger", true, false, TestTag);
            tokenizer.AddToken("Alsing", true, false, TestTag);

            const string text = @"Roger ROGER rogeR Alsing ALSING __Roger__ __Alsing__";
            //                   "XXXXX ----- ----- XXXXXX ------   XXXXX     XXXXXX  "

            // only two tokens should be found, the rest 
            // are either wrong casing or do not have separators next to them

            tokenizer.Text = text;
            var tokens = tokenizer.Tokenize();

            var testTokens = from token in tokens
                             where token.HasTag(TestTag)
                             select token;

            Assert.AreEqual(4, testTokens.ToList().Count);
        }

        [TestMethod]
        public void ParseCaseInsensitiveTokensWithoutSeparators()
        {
            var tokenizer = new Tokenizer();

            tokenizer.AddToken("Roger", false, false, TestTag);
            tokenizer.AddToken("Alsing", false, false, TestTag);

            const string text = @"Roger ROGER rogeR Alsing ALSING __Roger__ __Alsing__";
            //                   "XXXXX XXXXX XXXXX XXXXXX XXXXXX   XXXXX     XXXXXX  "

            // only two tokens should be found, the rest 
            // are either wrong casing or do not have separators next to them

            tokenizer.Text = text;
            var tokens = tokenizer.Tokenize();

            var testTokens = from token in tokens
                             where token.HasTag(TestTag)
                             select token;

            Assert.AreEqual(7, testTokens.ToList().Count);
        }

        [TestMethod]
        public void ParsePattern()
        {
            var tokenizer = new Tokenizer();

            tokenizer.AddPattern(IntPatternMatcher.Default,true,true,TestTag);

            tokenizer.AddToken("Alsing", false, true, TestTag);

            const string text = @"The quick brown 1337 fox jumped 0v3r the little pig 1234";
            //                                    XXXX            - -                 XXXX

            // only two tokens should be found, the rest 
            // are either wrong casing or do not have separators next to them

            tokenizer.Text = text;
            var tokens = tokenizer.Tokenize();

            var testTokens = from token in tokens
                             where token.HasTag(TestTag)
                             select token;

            Assert.AreEqual(2, testTokens.ToList().Count);
        }

        [TestMethod]
        public void ParsePatternWithoutSeparators()
        {
            var tokenizer = new Tokenizer();

            tokenizer.AddPattern(IntPatternMatcher.Default, true, false, TestTag);

            tokenizer.AddToken("Alsing", false, true, TestTag);

            const string text = @"The quick brown 1337 fox jumped 0v3r the little pig";
            //                                    XXXX            X X

            tokenizer.Text = text;
            var tokens = tokenizer.Tokenize();

            var testTokens = from token in tokens
                             where token.HasTag(TestTag)
                             select token;

            Assert.AreEqual(3, testTokens.ToList().Count);
        }
    }
}
