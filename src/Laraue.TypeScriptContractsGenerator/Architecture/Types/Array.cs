using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator.Architecture.Types
{
	public class Array : OutputType
	{
		private readonly TypeMetadata _metadata;

		/// <inheritdoc />
		public override string Name => "Array";

		public Array(TypeMetadata metadata)
		{
			_metadata = metadata;
		}
	}
}
