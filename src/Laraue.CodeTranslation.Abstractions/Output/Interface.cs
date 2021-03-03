using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public class Interface : OutputType
	{
		public readonly TypeMetadata Metadata;

		public override string Name => Metadata.ClrType.Name;

		public Interface(TypeMetadata metadata)
		{
			Metadata = metadata;
		}
	}
}