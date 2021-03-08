using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Abstractions.Output.Metadata;

namespace Laraue.CodeTranslation
{
	public abstract class OutputTypeMetadataGenerator : IOutputTypeMetadataGenerator
	{
		protected readonly MapCollection Collection;

		protected OutputTypeMetadataGenerator(MapCollection collection)
		{
			Collection = collection;
		}

		/// <inheritdoc />
		public OutputTypeMetadata Generate(TypeMetadata metadata)
		{
			return new OutputTypeMetadata
			{
				Source = metadata,
				OutputType = GetOutputType(metadata),
			};
		}

		public abstract OutputType GetOutputType(TypeMetadata metadata);
	}
}
