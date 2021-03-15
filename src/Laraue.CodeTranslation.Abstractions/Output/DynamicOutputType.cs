using System.Collections.Generic;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class DynamicOutputType : OutputType
	{
		public override OutputTypeName Name { get; }

		[CanBeNull]
		public override IEnumerable<OutputType> UsedTypes { get; }

		[CanBeNull]
		public override IEnumerable<OutputPropertyType> Properties { get; }

		[NotNull]
		public override TypeMetadata TypeMetadata { get; }

		protected DynamicOutputType(OutputTypeName name, IEnumerable<OutputType> usedTypes, IEnumerable<OutputPropertyType> properties, TypeMetadata typeMetadata)
		{
			Name = name;
			UsedTypes = usedTypes;
			Properties = properties;
			TypeMetadata = typeMetadata;
		}

		protected DynamicOutputType()
		{ }
	}
}