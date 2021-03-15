using System.Collections.Generic;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class OutputType
	{
		public abstract OutputTypeName Name { get; }

		public abstract IEnumerable<OutputType> UsedTypes { get; }

		public abstract IEnumerable<OutputPropertyType> Properties { get; }

		[CanBeNull]
		public abstract TypeMetadata TypeMetadata { get; }

		/// <inheritdoc />
		public override string ToString()
		{
			return $"ClrName = {GetType().Name} OutputName = {Name}";
		}
	}
}