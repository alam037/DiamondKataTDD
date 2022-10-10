using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;

namespace DiamondKataTDD
{
    public class DiamondTDD
    {

        [Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        private Property AlphabetError_When_NumberIsEntered(char c)
        {
           if (Char.IsDigit(c)) throw new Exception("Number is not allowed!");
            else
              return true.ToProperty();

        }
        [Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        private Property AlphabetError_When_SpecialCharacterIsEntered(char c)
        {
            if (!Char.IsLetterOrDigit(c)) throw new Exception("Special Character is not allowed!");
            else
                return true.ToProperty();
         }
        [Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        private Property OneAlphabetError_When_MoreThanOneAlphabetsAreEntered(string c)
        {
            if (c.Length >1) throw new Exception("More than one alphabet are not allowed!");
            else
                return true.ToProperty();
        }
        //Draw empty space
         [Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property NotEmpty(char c)
        {
            return DiamondKata.Generate(c).All(s => s != string.Empty).ToProperty();
        }

        ////First row draw char A
        [Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property FirstLineContainsA(char c)
        {
            return DiamondKata.Generate(c).First().Contains('A').ToProperty();
        }

        //Last row draw char A
        [Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property LastLineContainsA(char c)
        {
            return DiamondKata.Generate(c).Last().Contains('A').ToProperty();
        }
        //Add Spaces Per Row
        [Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property SpacesPerRow(char c)
        {
            return DiamondKata.Generate(c).All(row =>
                CountLeadingSpaces(row) == CountTrailingSpaces(row)
            ).ToProperty();
        }

        //Rows Contain Correct Letter In Correct Order
        [Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property GetCharInCorrectOrder(char c)
        {
            var expected = new List<char>();
            for (var i = 'A'; i < c; i++) expected.Add(i);

            for (var i = c; i >= 'A'; i--) expected.Add(i);

            var actual = DiamondKata.Generate(c).ToList().Select(GetCharInRow);
            return actual.SequenceEqual(expected).ToProperty();
        }

        //Draw Diamond Width Equals Height
        [Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property DiamondWidthEqualsHeight(char c)
        {
            var diamond = DiamondKata.Generate(c).ToList();
            return diamond.All(row => row.Length == diamond.Count).ToProperty();
        }


        //All Rows Except First And Last Contain Two Identical Letters
        [Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property FisrtLastContaintwoIdenticalLatter(char c)
        {
            if (c == 'A') return true.ToProperty();

            var diamond = DiamondKata.Generate(c).ToArray()[1..^1];
            return diamond.All(x =>
            {
                var s = x.Replace(" ", string.Empty);
                var b = s.Length == 2 && s.First() == s.Last();
                return b;
            }).ToProperty();
        }

        //Around Horizontal Axis
        [Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property ReversebottomHalf(char c)
        {
            var diamond = DiamondKata.Generate(c).ToArray();
            var half = diamond.Length / 2;
            var topHalf = diamond[..half];
            var bottomHalf = diamond[(half + 1)..];
            return topHalf.Reverse().SequenceEqual(bottomHalf).ToProperty();
        }

        //Around Vertical Axis
        [Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        public Property ReversesecondHalf(char c)
        {
            return DiamondKata.Generate(c).ToArray()
                .All(row =>
                {
                    var half = row.Length / 2;
                    var firstHalf = row[..half];
                    var secondHalf = row[(half + 1)..];

                    return firstHalf.Reverse().SequenceEqual(secondHalf);
                }).ToProperty();
        }

        [Property(Arbitrary = new[] { typeof(LetterGenerator) })]
        //Input Letter Row Contains Number Outside Padding Spaces
        public Property OutsidePaddingSpaces(char c)
        {
            var inputLetterRow = DiamondKata.Generate(c).ToArray().First(x => GetCharInRow(x) == c);
            return (inputLetterRow[0] != ' ' && inputLetterRow[^1] != ' ').ToProperty();
        }

        private int CountLeadingSpaces(string s)
        {
            return s.IndexOf(GetCharInRow(s));
        }

        private static char GetCharInRow(string row)
        {
            return row.First(x => x != ' ');
        }

        private int CountTrailingSpaces(string s)
        {
            var i = s.LastIndexOf(GetCharInRow(s));
            return s.Length - i - 1;
        }
    }
}
