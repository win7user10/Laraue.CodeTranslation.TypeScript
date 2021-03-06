using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator.Architecture.Types
{
	public class Number : ValueOutputType
	{
		public override OutputTypeName Name => "number";
	}
}