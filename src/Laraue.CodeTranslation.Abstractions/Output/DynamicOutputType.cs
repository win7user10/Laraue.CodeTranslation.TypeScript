using System.Collections.Generic;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class DynamicOutputType : OutputType
	{
		protected readonly IOutputTypeProvider TypeProvider;

		protected DynamicOutputType(OutputTypeName name, TypeMetadata metadata, IOutputTypeProvider provider)
		{
			Name = name;
			TypeProvider = provider;
			TypeMetadata = metadata;
		}

		/// <inheritdoc />
		public override IEnumerable<OutputPropertyType> Properties => TypeProvider.GetProperties(TypeMetadata);

		/// <inheritdoc />
		public override IEnumerable<OutputType> UsedTypes => TypeProvider.GetUsedTypes(TypeMetadata);
	}
}