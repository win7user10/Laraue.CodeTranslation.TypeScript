using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public class Class : OutputType
	{
		public readonly TypeMetadata Metadata;

		public override string Name => Metadata.ClrType.Name;

		public Class(TypeMetadata metadata)
		{
			Metadata = metadata;
		}
	}
}