using System;
using JetBrains.Annotations;

namespace Laraue.TypeScriptContractsGenerator.Abstractions.Metadata
{
	/// <summary>
	/// Contains meta information of some <see cref="Type">Clr type</see> or <see cref="System.Reflection.MemberInfo">Clr property</see>.
	/// </summary>
	public interface IMetadata
	{
		/// <summary>
		/// Source Clr Type.
		/// </summary>
		[NotNull]
		Type ClrType { get; set; }

		/// <summary>
		/// Does type should be generic.
		/// </summary>
		bool IsGeneric { get; set; }
	}
}