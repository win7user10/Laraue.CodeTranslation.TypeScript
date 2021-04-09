using Laraue.CodeTranslation.Abstractions.Translation;
using Laraue.CodeTranslation.Extensions;

namespace Laraue.CodeTranslation.NamingStrategies
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