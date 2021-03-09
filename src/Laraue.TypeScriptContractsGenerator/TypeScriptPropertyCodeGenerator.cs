using Laraue.CodeTranslation.Abstractions.Code;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator
{
	public class TypeScriptPropertyCodeGenerator : IPropertyCodeGenerator
	{
		/// <inheritdoc />
		public string GenerateCode(OutputPropertyType property)
		{
			return "";
		}

		/// <inheritdoc />
		public string GetName(OutputPropertyType property)
		{
			return "";
		}
	}
}