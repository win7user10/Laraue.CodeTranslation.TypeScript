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
		/// Does type should be generic.
		/// </summary>
		public bool IsGeneric { get; init; }
	}
}