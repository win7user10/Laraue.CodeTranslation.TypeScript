using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Laraue.TypeScriptContractsGenerator.Extensions
{
    public static class EnumerableStringExtensions
    {
        /// <summary>
        /// Filter string by passed pattern.
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="masks"></param>
        /// <returns></returns>
        public static IEnumerable<string> MatchAnyPattern(this IEnumerable<string> strings, IEnumerable<string> masks)
            => strings.Where(@string => masks.Any(pattern => Regex.IsMatch(@string, pattern)));
    }
}
