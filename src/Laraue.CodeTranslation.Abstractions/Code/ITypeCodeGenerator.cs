using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Code
{
	/// <summary>
	/// Class can generates result code for <see cref="TypeMetadata">type</see>.
	/// </summary>
	public interface ITypeCodeGenerator : ICodeGenerator
	{
		/// <summary>
		/// Generate code for some <see cref="TypeMetadata">type</see>.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		string GenerateCode(TypeMetadata metadata);
	}
}