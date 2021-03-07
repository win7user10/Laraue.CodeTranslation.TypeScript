using System.Collections.Generic;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class ReferenceOutputType : OutputType
	{
		public override OutputTypeName Name { get; }
		public override IEnumerable<OutputType> UsedTypes { get; }

		protected ReferenceOutputType(OutputTypeName name, IEnumerable<OutputType> usedTypes)
		{
			Name = name;
			UsedTypes = usedTypes;
		}
	}
}