using System.Linq;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Array : DynamicOutputType
	{
		public Array(OutputTypeName name, TypeMetadata metadata, IOutputTypeProvider provider)
			: base(metadata, provider)
		{
			Name = GetName(name);
		}

		private static OutputTypeName GetName(OutputTypeName sourceType)
		{
			return new (sourceType, Enumerable.Empty<OutputTypeName>(), true);
		}
	}
}
