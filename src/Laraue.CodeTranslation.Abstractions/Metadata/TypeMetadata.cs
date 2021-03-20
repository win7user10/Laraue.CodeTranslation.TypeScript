using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Laraue.CodeTranslation.Abstractions.Metadata
{
	/// <summary>
	/// Contains meta information of some <see cref="System.Type">Clr type</see>.
	/// </summary>
	public record TypeMetadata
	{
		/// <summary>
		/// Source Clr Type.
		/// </summary>
		[NotNull]
		public Type ClrType { get; init; }

		/// <summary>
		/// Is this type a generic type.
		/// </summary>
		public bool IsGeneric { get; init; }

		/// <summary>
		/// Generic types of the metadata.
		/// </summary>
		[CanBeNull]
		public IEnumerable<TypeMetadata> GenericTypeArguments { get; init; }

		/// <summary>
		/// Is the type an enum value.
		/// </summary>
		public bool IsEnum { get; init; }

		/// <summary>
		/// Is this type an array value.
		/// </summary>
		public bool IsEnumerable { get; init; }

		/// <summary>
		/// Is this type an array value.
		/// </summary>
		public bool IsDictionary { get; init; }

		/// <summary>
		/// Is this type assignable from <see cref="Nullable{T}"/>.
		/// </summary>
		public bool IsNullable { get; init; }

		[CanBeNull]
		public TypeMetadata ParentTypeMetadata { get; init; }

		[NotNull]
		public IEnumerable<PropertyMetadata> PropertiesMetadata { get; init; }

	}
}