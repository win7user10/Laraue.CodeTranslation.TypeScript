using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.Abstractions.Code
{
	/// <summary>
	/// Class can generates result code for <see cref="OutputPropertyType">property</see>.
	/// </summary>
	public interface ICodeGenerator
	{
		/// <summary>
		/// Generates code for some <see cref="OutputPropertyType">property</see>.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		string GenerateCode(OutputPropertyType property);

		/// <summary>
		/// Generate code for some <see cref="OutputType">type</see>.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		string GenerateCode(OutputType type);
	}
}