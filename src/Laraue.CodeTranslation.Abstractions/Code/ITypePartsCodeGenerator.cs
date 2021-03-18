using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.Abstractions.Code
{
    public interface ITypePartsCodeGenerator
    {
        /// <summary>
        /// Generates import strings for some <see cref="OutputType"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        string[] GenerateImportStrings(OutputType type);

        /// <summary>
        /// Generates result name in output code for some <see cref="OutputType"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        string GenerateName(OutputType type);

        /// <summary>
        /// Generates result name in output code for some <see cref="OutputPropertyType"/>.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        string GenerateName(OutputPropertyType property);

        /// <summary>
        /// Generates default value for some property. E.g. "0" for number, "''" - for string.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        string GenerateDefaultValue(OutputPropertyType property);

        /// <summary>
        /// Generates result type of the passed <see cref="OutputPropertyType"/>.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        string GeneratePropertyType(OutputPropertyType property);

        /// <summary>
        /// Returns, should type generates for this <see cref="OutputPropertyType"/> or no.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        bool ShouldBeUsedTypingInPropertyDefinition(OutputPropertyType property);
    }
}