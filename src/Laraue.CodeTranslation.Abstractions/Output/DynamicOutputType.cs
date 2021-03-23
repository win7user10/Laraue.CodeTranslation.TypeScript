using System.Collections.Generic;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class DynamicOutputType : OutputType
	{
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