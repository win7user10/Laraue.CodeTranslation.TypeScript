namespace Laraue.CodeTranslation.TypeScript.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Transform passed string to camel case convention.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string str)
            => string.IsNullOrEmpty(str) || str.Length < 2 ? str : char.ToLowerInvariant(str[0]) + str.Substring(1);

        /// <summary>
        /// Transform passed string to upper case convention.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string str)
            => string.IsNullOrEmpty(str) || str.Length < 2 ? str : char.ToUpperInvariant(str[0]) + str.Substring(1);
    }
}