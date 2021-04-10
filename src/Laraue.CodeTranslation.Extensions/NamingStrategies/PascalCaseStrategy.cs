using Laraue.CodeTranslation.Abstractions.Translation;

namespace Laraue.CodeTranslation.Extensions.NamingStrategies
{
    /// <summary>
    /// Transformation like newName -> NewName.
    /// </summary>
    public class PascalCaseStrategy : INamingStrategy
    {
        /// <inheritdoc />
        public string Resolve(string name) => name.ToPascalCase();
    }
}