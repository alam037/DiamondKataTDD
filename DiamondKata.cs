using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DiamondKataTDD
{
    public class DiamondKata
    {
        public static IEnumerable<string> Generate(char c)
        {
            return GenerateFinal(c);
        }

        private static IEnumerable<string> Generate1(char c)
        {
            yield return "A";
        }

        private static IEnumerable<string> Generate2(char c)
        {
            yield return " A ";
            yield return $"{c} {c}";
            yield return " A ";
        }

        private static IEnumerable<string> Generate3(char c)
        {
            if (c == 'A')
            {
                yield return "A";
            }
            else
            {
                yield return " A ";
                yield return $"{c} {c}";
                yield return " A ";
            }
        }

        private static IEnumerable<string> GenerateFinal(char c)
        {
            for (var i = 'A'; i < c; i++) yield return GenerateRow(i, c);
            for (var i = c; i >= 'A'; i--) yield return GenerateRow(i, c);
        }

        private static string GenerateRow(char currentChar, char maxChar)
        {
            var length = (maxChar - 'A' + 1) * 2 - 1;
            var padding = maxChar - currentChar;
            var sb = new StringBuilder();

            sb.Append(new string(' ', padding));
            sb.Append(currentChar);
            if (currentChar != 'A')
            {
                var insidePadding = length - padding * 2 - 2;
                sb.Append(new string(' ', insidePadding));
                sb.Append(currentChar);
            }

            sb.Append(new string(' ', padding));
            //Write the result into text file
           //WriteResult(sb.ToString());
            return sb.ToString();
        }

        public static void WriteResult(string strMessage)
        {
            try
            {
                var startDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", startDirectory, "TestResult.txt"), FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine(strMessage);
                objStreamWriter.Close();
                objFilestream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}