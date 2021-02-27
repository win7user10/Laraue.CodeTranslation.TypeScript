using Laraue.TypeScriptContractsGenerator.Abstractions.Metadata;

namespace Laraue.TypeScriptContractsGenerator.Abstractions.Code
{
	/// <summary>
	/// Class can generates result code for <see cref="ITypeMetadata">type</see>.
	/// </summary>
	public interface ITypeCodeGenerator : ICodeGenerator
	{
		/// <summary>
		/// Generate code for some <see cref="ITypeMetadata">type</see>.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		string GenerateCode(ITypeMetadata metadata);
	}
}