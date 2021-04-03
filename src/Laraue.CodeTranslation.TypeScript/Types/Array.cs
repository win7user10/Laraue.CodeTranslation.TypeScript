using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Array : DynamicOutputType
	{
		public Array(OutputTypeName name, TypeMetadata metadata, IOutputTypeProvider provider)
			: base(GetName(name), metadata, provider)
		{
		}

		private static OutputTypeName GetName(OutputTypeName sourceType)
		{
			return new (sourceType, sourceType.GenericNames, true);
		}
	}
}
