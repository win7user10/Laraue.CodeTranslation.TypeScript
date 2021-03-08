using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator.Types
{
	public class Number : ValueOutputType
	{
		public override OutputTypeName Name => "number";
	}
}