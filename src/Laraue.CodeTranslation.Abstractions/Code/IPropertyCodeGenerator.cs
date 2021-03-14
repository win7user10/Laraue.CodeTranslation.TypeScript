using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.Abstractions.Code
{
	/// <summary>
	/// Class can generates result code for <see cref="OutputPropertyType">property</see>.
	/// </summary>
	public interface IPropertyCodeGenerator : ICodeGenerator
	{
		/// <summary>
		/// Generates code for some <see cref="OutputPropertyType">property</see>.
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		string GenerateCode(OutputPropertyType property);
	}
}