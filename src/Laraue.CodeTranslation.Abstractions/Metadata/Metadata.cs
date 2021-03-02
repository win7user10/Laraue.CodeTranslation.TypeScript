using System;
using JetBrains.Annotations;

namespace Laraue.CodeTranslation.Abstractions.Metadata
{
	/// <summary>
	/// Contains meta information of some <see cref="Type">Clr type</see> or <see cref="System.Reflection.MemberInfo">Clr property</see>.
	/// </summary>
	public abstract record Metadata
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
		public Metadata[] GenericTypeArguments { get; init; }

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
	}
}