namespace Laraue.CodeTranslation.Abstractions.Translation
{
    /// <summary>
    /// Represent information about generated code.
    /// </summary>
    public record GeneratedCode
    {
        /// <summary>
        /// Path segments of the result file.
        /// </summary>
        public string[] FilePathSegments { get; init; }

        /// <summary>
        /// Generated code.
        /// </summary>
        public string Code { get; init; }
    }
}