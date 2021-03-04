using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output.Metadata
{
	public interface IOutputTypeMetadataGenerator
	{
		public OutputTypeMetadata Generate(TypeMetadata metadata);
	}
}