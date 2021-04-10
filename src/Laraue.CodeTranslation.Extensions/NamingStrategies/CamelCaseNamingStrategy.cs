using Laraue.CodeTranslation.Abstractions.Translation;

namespace Laraue.CodeTranslation.Extensions.NamingStrategies
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