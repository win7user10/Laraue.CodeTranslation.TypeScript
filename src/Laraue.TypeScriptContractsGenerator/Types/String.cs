using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator.Types
{
	public class String : ValueOutputType
	{
		public override OutputTypeName Name => "string";
	}
}