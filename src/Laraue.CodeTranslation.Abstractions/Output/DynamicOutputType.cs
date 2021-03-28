using System.Collections.Generic;
using System.Linq;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class DynamicOutputType : OutputType
	{
		protected DynamicOutputType(OutputTypeName name, OutputType[] usedTypes, OutputPropertyType[] properties, TypeMetadata typeMetadata)
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