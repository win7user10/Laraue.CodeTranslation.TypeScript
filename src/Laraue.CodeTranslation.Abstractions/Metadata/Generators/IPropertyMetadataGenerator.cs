using System.Reflection;
using JetBrains.Annotations;

namespace Laraue.CodeTranslation.Abstractions.Metadata.Generators
{
	/// <summary>
	/// Class can generates <see cref="PropertyMetadata"/> for a <see cref="PropertyInfo"/>.
	/// </summary>
	public interface IPropertyMetadataGenerator : IMetadataGenerator
	{
		/// <summary>
		/// Generates <see cref="PropertyMetadata"/> for a some <see cref="PropertyInfo"/>.
		/// </summary>
		/// <returns></returns>
		[NotNull]
		PropertyMetadata GetMetadata(PropertyInfo property);
	}
}