using System;
using JetBrains.Annotations;

namespace Laraue.TypeScriptContractsGenerator.Abstractions.Metadata.Generators
{
	/// <summary>
	/// Class can generates <see cref="ITypeMetadata">metadata</see> for <see cref="ITypeMetadata">type</see>.
	/// </summary>
	public interface ITypeMetadataGenerator : IMetadataGenerator
	{
		/// <summary>
		/// Generates <see cref="ITypeMetadata">metadata</see> for some <see cref="Type">Clr type</see>.
		/// </summary>
		/// <returns></returns>
		[NotNull]
		ITypeMetadata GetMetadata(Type type);
	}
}