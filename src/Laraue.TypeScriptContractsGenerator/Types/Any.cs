using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator.Types
{
	public class Any : StaticOutputType
	{
		public override OutputTypeName Name => "any";
	}
}