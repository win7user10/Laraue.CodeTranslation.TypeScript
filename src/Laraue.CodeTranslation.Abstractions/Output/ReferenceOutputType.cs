using System.Collections.Generic;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class ReferenceOutputType : OutputType
	{
		public override OutputTypeName Name { get; }
		public override IEnumerable<TypeMetadata> UsedTypes { get; }

		protected ReferenceOutputType(OutputTypeName name, IEnumerable<TypeMetadata> usedTypes)
		{
			Name = name;
			UsedTypes = usedTypes;
		}
	}
}