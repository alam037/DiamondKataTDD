using System;
using System.Collections.Generic;
using System.Text;
using FsCheck;
namespace DiamondKataTDD
{
   
        public static class LetterGenerator
        {
            public static Arbitrary<char> Generate() =>
                Arb.Default.Char().Filter(c => c >= 'A' && c <= 'Z');
        }
    
}
