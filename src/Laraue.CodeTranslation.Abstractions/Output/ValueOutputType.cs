using System;
using System.Collections.Generic;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class ValueOutputType : OutputType
	{
		public override IEnumerable<TypeMetadata> UsedTypes { get; } = Array.Empty<TypeMetadata>();
	}
}