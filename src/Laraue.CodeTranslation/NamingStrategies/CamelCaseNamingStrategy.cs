using Laraue.CodeTranslation.Abstractions.Translation;
using Laraue.CodeTranslation.Extensions;

namespace Laraue.CodeTranslation.NamingStrategies
{
    /// <summary>
    /// Transformation like NewName -> newName.
    /// </summary>
    public class CamelCaseNamingStrategy : INamingStrategy
    {
        /// <inheritdoc />
        public string Resolve(string name) => name.ToCamelCase();
    }
}