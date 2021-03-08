using System;
using System.Collections.Generic;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class ValueOutputType : OutputType
	{
		public override IEnumerable<OutputType> UsedTypes { get; } = Array.Empty<OutputType>();
		public override IEnumerable<OutputPropertyType> Properties { get; } = Array.Empty<OutputPropertyType>();
	}
}