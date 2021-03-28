using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Array : DynamicOutputType
	{
		public Array(OutputTypeName name, OutputType[] usedTypes, TypeMetadata typeMetadata) 
			: base(GetName(name), usedTypes, System.Array.Empty<OutputPropertyType>(), typeMetadata)
		{
		}

		private static OutputTypeName GetName(OutputTypeName sourceType)
		{
			return new (sourceType, sourceType.GenericNames, true);
		}
	}
}
