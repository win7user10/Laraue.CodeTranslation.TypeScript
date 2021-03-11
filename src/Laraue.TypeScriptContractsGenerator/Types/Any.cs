using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator.Types
{
	public class Any : ValueOutputType
	{
		public override OutputTypeName Name => "any";
	}
}