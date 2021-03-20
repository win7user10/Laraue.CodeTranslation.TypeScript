using System;
using System.Reflection;
using JetBrains.Annotations;

namespace Laraue.CodeTranslation.Abstractions.Metadata.Generators
{
	/// <summary>
	/// Class can generates metadata for types and properties.
	/// </summary>
	public interface IMetadataGenerator
	{
		/// <summary>
		/// Generates <see cref="TypeMetadata">metadata</see> for some <see cref="Type">Clr type</see>.
		/// </summary>
		/// <returns></returns>
		[NotNull]
		TypeMetadata GetMetadata(Type type);

		/// <summary>
		/// Generates <see cref="PropertyMetadata"/> for a some <see cref="PropertyInfo"/>.
		/// </summary>
		/// <returns></returns>
		[NotNull]
		PropertyMetadata GetMetadata(PropertyInfo property);
	}
}