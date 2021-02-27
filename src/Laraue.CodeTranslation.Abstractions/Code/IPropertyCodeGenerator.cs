using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Code
{
	/// <summary>
	/// Class can generates result code for <see cref="IPropertyMetadata">property</see>.
	/// </summary>
	public interface IPropertyCodeGenerator : ICodeGenerator
	{
		/// <summary>
		/// Generates code for some <see cref="IPropertyMetadata">property</see>.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		string GenerateCode(IPropertyMetadata metadata);

		/// <summary>
		/// Get translated name of <see cref="IPropertyMetadata">property</see>.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		string GetName(IPropertyMetadata metadata);
	}
}