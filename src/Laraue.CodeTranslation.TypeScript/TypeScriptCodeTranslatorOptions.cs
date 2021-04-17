using Laraue.CodeTranslation.Abstractions.Translation;
using Laraue.CodeTranslation.Extensions.NamingStrategies;

namespace Laraue.CodeTranslation.TypeScript
{
    /// <inheritdoc />
    public class TypeScriptCodeTranslatorOptions : CodeTranslatorOptions
    {
        public TypeScriptCodeTranslatorOptions()
        {
            TypeNamingStrategy = new PascalCaseNamingStrategy();
            PathSegmentNamingStrategy = new CamelCaseNamingStrategy();
            PropertiesNamingStrategy = new CamelCaseNamingStrategy();
        }
    }
}