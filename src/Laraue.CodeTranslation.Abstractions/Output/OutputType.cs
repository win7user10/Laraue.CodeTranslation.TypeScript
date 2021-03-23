using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class OutputType
	{
		public OutputTypeName Name { get; protected set; }

		[NotNull]
		public IEnumerable<OutputType> UsedTypes { get; protected set; } = Array.Empty<OutputType>();

		[NotNull]
		public IEnumerable<OutputPropertyType> Properties { get; protected set; } = Array.Empty<OutputPropertyType>();

		[CanBeNull]
		public TypeMetadata TypeMetadata { get; protected set; }

		[CanBeNull]
		public OutputTypeName ParentTypeName { get; protected set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return $"ClrName = {GetType().Name} OutputName = {Name}";
		}
	}
}