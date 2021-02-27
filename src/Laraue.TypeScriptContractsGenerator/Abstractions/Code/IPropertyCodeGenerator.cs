using Laraue.TypeScriptContractsGenerator.Abstractions.Metadata;

namespace Laraue.TypeScriptContractsGenerator.Abstractions.Code
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
	}
}