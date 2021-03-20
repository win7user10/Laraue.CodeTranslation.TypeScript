using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Any : StaticOutputType
	{
		public override OutputTypeName Name => "any";
	}
}