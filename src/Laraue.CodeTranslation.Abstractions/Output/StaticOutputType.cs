using System;
using System.Collections.Generic;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class StaticOutputType : OutputType
	{
		/// <inheritdoc />
		public override IEnumerable<OutputPropertyType> Properties { get; } = Array.Empty<OutputPropertyType>();

		/// <inheritdoc />
		public override IEnumerable<OutputType> UsedTypes { get; } = Array.Empty<OutputType>();
	}
}