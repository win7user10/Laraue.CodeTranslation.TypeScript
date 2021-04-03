using System.Collections.Generic;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class OutputType
	{
		[NotNull] public OutputTypeName Name { get; protected set; }

		[NotNull] public abstract IEnumerable<OutputType> UsedTypes { get; }

		[NotNull] public abstract IEnumerable<OutputPropertyType> Properties { get; }

		[CanBeNull] public TypeMetadata TypeMetadata { get; protected set; }

		[CanBeNull] public OutputTypeName ParentTypeName { get; protected set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return $"Clr = {TypeMetadata?.ClrType} Output = {GetType().Name} Name = {Name}";
		}
	}
}