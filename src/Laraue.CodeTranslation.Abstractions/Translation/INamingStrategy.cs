namespace Laraue.CodeTranslation.Abstractions.Translation
{
    /// <summary>
    /// Strategy for creating some result string from some source string.
    /// </summary>
    public interface INamingStrategy
    {
        /// <summary>
        /// Transforming source string to the result string.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string Resolve(string name);
    }
}