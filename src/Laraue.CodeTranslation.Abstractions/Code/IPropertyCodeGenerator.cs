using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Code
{
	/// <summary>
	/// Class can generates result code for <see cref="PropertyMetadata">property</see>.
	/// </summary>
	public interface IPropertyCodeGenerator : ICodeGenerator
	{
		/// <summary>
		/// Generates code for some <see cref="PropertyMetadata">property</see>.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		string GenerateCode(PropertyMetadata metadata);

		/// <summary>
		/// Get translated name of <see cref="PropertyMetadata">property</see>.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		string GetName(PropertyMetadata metadata);
	}
}