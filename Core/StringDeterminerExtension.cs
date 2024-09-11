using Core.Extensions;
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
        public static PrintModel EvaluteString(this string str, int maxCharPerLine = 22, bool printDebug = true)
        {
            string _inputv0 = "<p>Pastikan Kalibrasi mesin adalah serupa seperti apa yang sudah disampaikan sebelumnya</p><p><br></p><p>Berikut adalah contoh dari text print yang panjang dengan kriteria;</p><ul><li>bentukannya seperti ini, apakah mereka bisa menangkap apa yang dimaksud?</li><li>adapun pendek seperti ini</li><li>belum ditambahkan satu gambar</li></ul>";
            string input = "<p class=\"MsoNormal\" style=\"text-align: justify; margin-bottom: 0pt\">Pastikan pada bagian baling-baling mesin miniturbomixer: </p><p class=\"MsoNormal\" style=\"text-align: justify; margin-bottom: 0pt\"><br></p><ul><li style=\"text-align: justify; margin-bottom: 0pt\"><span >Tidak terdapat sisa produk dari produk sebelumnya/<i>batch </i>lainnya/produk lainnya.</span></li><li style=\"text-align: justify; margin-bottom: 0pt\"><span >Tidak terdapat debu/kotoran.</span></li><li style=\"text-align: justify; margin-bottom: 0pt\"><span ><b><i>Part</i> </b>dalam kondisi baik.</span></li><div style=\"text-align: justify\"><span >Catatan:</span></div><div style=\"text-align: justify\">1. Jika <span >digunakan:</span></div><ul style=\"margin-bottom: 1rem\"><li style=\"text-align: justify\">pilih <span >Sesuai, jika hasil memenuhi syarat,</span></li><li style=\"text-align: justify\">pilih <span >Tidak Sesuai, jika hasil tidak memenuhi syarat.</span></li></ul><div style=\"text-align: justify\">2. Jika <span >tidak digunakan</span>, maka pilih <span >NA.</span></div></ul>";
            //string input = "<p><p></p></p>";

            int availableLines = 9;
            var ListOfStr = input.Extract();

            //start dari satu
            int index = 1;

            foreach (var item in ListOfStr)
            {
                //switch (item.Item1)
                //{
                //    case "ul":
                //        if (printDebug)
                //        {
                //            Console.WriteLine($"{index}. UL Detected!");
                //        }
                //        foreach (var ulchild in item.Item2.ExtractUlChildren())
                //        {
                //            if (printDebug)
                //            {
                //                Console.WriteLine($"{ulchild.Item2}");
                //                Console.WriteLine($"Element = {ulchild.Item1}");
                //                Console.WriteLine();
                //            }
                //        }
                //        break;
                //    default:
                //        if (printDebug)
                //        {
                //            Console.WriteLine($"{index}. {item.Item2}");
                //            Console.WriteLine($"Element No. {index} = {item.Item1}");
                //        }
                //        break;
                //}
                if (printDebug)
                {
                    Console.WriteLine($"{index}. {item.Item2}");
                    Console.WriteLine($"Element No. {index} = {item.Item1}");
                    Console.WriteLine();
                }
                index++;
            }
            return new PrintModel();
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
        /// Mengevaluasi 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxChar"></param>
        /// <returns>List dari elemen html dan prakiraan pemakaian baris</returns>
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
