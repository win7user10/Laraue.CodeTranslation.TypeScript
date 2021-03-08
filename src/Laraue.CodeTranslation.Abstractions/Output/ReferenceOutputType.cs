using System.Collections.Generic;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class ReferenceOutputType : OutputType
	{
		public override OutputTypeName Name { get; }
		public override IEnumerable<OutputType> UsedTypes { get; }
		public override IEnumerable<OutputPropertyType> Properties { get; }

		protected ReferenceOutputType(OutputTypeName name, IEnumerable<OutputType> usedTypes, IEnumerable<OutputPropertyType> properties)
		{
			Name = name;
			UsedTypes = usedTypes;
			Properties = properties;
		}

		protected ReferenceOutputType()
		{

		}
	}
}