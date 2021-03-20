using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Abstractions.Output.Metadata;

namespace Laraue.CodeTranslation.Common
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

		[CanBeNull]
		public abstract OutputType GetOutputType(TypeMetadata metadata, int callNumber = 0);
	}
}
