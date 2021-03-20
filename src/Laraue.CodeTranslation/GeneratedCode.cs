namespace Laraue.CodeTranslation
{
    public record GeneratedCode
    {
        public string[] FilePathSegments { get; init; }

        public string Code { get; init; }
    }
}