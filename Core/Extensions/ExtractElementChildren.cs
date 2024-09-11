using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ExtractElementChildren
    {
        /// <summary>
        /// Memisahkan elemen parent dari gelonggongan string
        /// </summary>
        /// <param name="str">Berisi elemen html</param>
        /// <returns>
        /// <para>Item 1 : Tipe Element</para>
        /// <para>Item 2 : String Element</para>
        /// </returns>
        public static List<Tuple<string, string>> Extract(this string str)
        {
            var result = new List<Tuple<string, string>>();

            // Regular expression to match full tags including nested ones
            var regex = new Regex(@"<(?<tag>\w+)(\s+[^>]+)*>(?<content>.*?)<\/\k<tag>>", RegexOptions.Singleline);

            // Find matches in the input string
            var matches = regex.Matches(str);

            // Add each match to the result list
            foreach (Match match in matches)
            {
                var elementType = match.Groups["tag"].Value;
                var elementContent = match.Value;
                result.Add(new Tuple<string, string>(elementType, elementContent));
            }

            return result;
        }


        public static List<Tuple<string, string>> ExtractUlChildren(this string input)
        {
            var result = new List<Tuple<string, string>>();

            // Regex to match the entire <ul> element
            var ulRegex = new Regex(@"<ul[^>]*>(.*?)<\/ul>", RegexOptions.Singleline);

            // Find the first <ul> match
            var ulMatch = ulRegex.Match(input);
            if (ulMatch.Success)
            {
                // Extract the content inside the <ul>
                string ulContent = ulMatch.Groups[1].Value;

                // Regex to match direct child elements inside the <ul>
                // This regex captures the tag name and content of direct children
                var childRegex = new Regex(@"<(li|div)(\s[^>]*)?>.*?<\/\1>|<(ul)(\s[^>]*)?>.*?<\/\3>", RegexOptions.Singleline);

                // Find all matches for child elements
                var childMatches = childRegex.Matches(ulContent);

                // Add each match's tag name and content to the result list
                foreach (Match match in childMatches)
                {
                    var elementType = match.Groups[1].Success ? match.Groups[1].Value : match.Groups[3].Value;
                    var elementContent = match.Value;
                    result.Add(new Tuple<string, string>(elementType, elementContent));
                }
            }

            return result;
        }
    }
}
