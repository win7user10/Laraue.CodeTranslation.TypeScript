using System.Collections.Generic;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator.Types
{
	public class Array : DynamicOutputType
	{
		/// <inheritdoc />
		public override OutputTypeName Name { get; }

		/// <inheritdoc />
		public override IEnumerable<OutputType> UsedTypes { get; }

		public Array(OutputTypeName name, IEnumerable<OutputType> usedTypes, TypeMetadata typeMetadata)
		{
			Name = GetName(name);
			UsedTypes = usedTypes;
		}

		public OutputTypeName GetName(OutputTypeName sourceType)
		{
			return new (sourceType, sourceType.GenericNames, true);
		}
	}
}
