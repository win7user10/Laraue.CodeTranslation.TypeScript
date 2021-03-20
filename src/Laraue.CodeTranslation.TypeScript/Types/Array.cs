using System.Collections.Generic;
using System.Linq;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Array : DynamicOutputType
	{
		public Array(OutputTypeName name, IEnumerable<OutputType> usedTypes, TypeMetadata typeMetadata) 
			: base(GetName(name), usedTypes, Enumerable.Empty<OutputPropertyType>(), typeMetadata)
		{
		}

		private static OutputTypeName GetName(OutputTypeName sourceType)
		{
			return new (sourceType, sourceType.GenericNames, true);
		}
	}
}
