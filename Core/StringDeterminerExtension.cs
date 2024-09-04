using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core
{
    public static class StringDeterminerExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxCharperLine"></param>
        /// <returns></returns>
        public static PrintModel EvaluteString(this string str, int maxCharPerLine = 22)
        {
            string input = "<p>Pastikan Kalibrasi mesin</p><p><br></p><p>Berikut adalah contoh dari text print yang panjang dengan kriteria;</p><ul><li>bentukannya seperti ini, apakah mereka bisa menangkap apa yang dimaksud?</li><li>adapun pendek seperti ini</li><li>belum ditambahkan satu gambar</li></ul>";
            int availableLines = 9;
            var ListOfStr = ParseStringToList(input);

            //start dari satu
            int index = 1;

            Console.WriteLine("Write the output");
            foreach (var item in ListOfStr)
            {
                Console.WriteLine($"{index}. {item}");
                index++;
            }

            Console.WriteLine("Expected print:");
            using (var printString = new PrintModel())
            {
                index = 1;
                int lineUsed = 0;
                foreach (var item in ListOfStr)
                {
                    var (evaluatedString, usedLine) = EvaluteStringPerLine(StripHtmlTags(item), maxChar: maxCharPerLine);
                    //if (evaluatedString.Count != 1)
                    //{
                    //    int subIndex = 1;
                    //    foreach (var perString in evaluatedString)
                    //    {
                    //        Console.WriteLine($"{index}.{subIndex}. {perString}");
                    //        subIndex++;
                    //    }
                    //}
                    //else
                    //{
                    //    Console.WriteLine($"{index}. {StripHtmlTags(item)}");
                    //}
                    if (lineUsed < availableLines)
                    {
                        Console.WriteLine($"{index}. {StripHtmlTags(item)}");
                    }
                    else
                    {
                        Console.WriteLine("breakpage");
                        Console.WriteLine($"{index}. {StripHtmlTags(item)}");
                        lineUsed = 0;
                    }
                    lineUsed += usedLine;
                    index++;
                }

                Console.WriteLine($"estimated line used: {lineUsed}");
                return printString;
            }
        }

        static List<string> ParseStringToList(this string input)
        {
            var result = new List<string>();

            // Regular expression to match full tags (including nested ones)
            var regex = new Regex(@"(<[^>]+>.*?<\/[^>]+>|<[^>]+>)");

            // Find matches in the input string
            var matches = regex.Matches(input);

            // Add each match to the result list
            foreach (Match match in matches)
            {
                result.Add(match.Value);
            }

            return result;
        }
        static string StripHtmlTags(string input)
        {
            // Regular expression to match HTML tags
            var regex = new Regex("<.*?>");

            // Replace HTML tags with an empty string
            string result = regex.Replace(input, string.Empty);

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxChar"></param>
        /// <returns></returns>
        static (List<string>, int) EvaluteStringPerLine(string str, int maxChar)
        {
            var result = new List<string>();
            string[] words = str.Split(' ');

            string currentLine = string.Empty;

            foreach (var word in words)
            {
                if ((currentLine + word).Length > maxChar)
                {
                    result.Add(currentLine.Trim());
                    currentLine = word + " ";
                }
                else
                {
                    currentLine += word + " ";
                }
            }

            if (!string.IsNullOrEmpty(currentLine))
            {
                result.Add(currentLine.Trim());
            }

            return (result, result.Count);
        }
    }
}
