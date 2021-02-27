using Laraue.CodeTranslation.Abstractions.Code;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Typed.Abstractions.Code
{
	public interface IGenericCodeGenerator : ICodeGenerator
	{
		/// <summary>
		/// Get translated generic type from a <see cref="IMetadata">metadata</see>.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		public string GenerateCode(IMetadata metadata);
	}
}