using Laraue.CodeTranslation.Abstractions.Translation;
using Laraue.CodeTranslation.NamingStrategies;

namespace Laraue.CodeTranslation.TypeScript
{
    /// <inheritdoc />
    public class TypeScriptCodeTranslatorOptions : CodeTranslatorOptions
    {
        public TypeScriptCodeTranslatorOptions()
        {
            TypeNamingStrategy = new PascalCaseStrategy();
            PathSegmentNamingStrategy = new CamelCaseNamingStrategy();
            PropertiesNamingStrategy = new CamelCaseNamingStrategy();
        }
    }
}