using System.Reflection;
using JetBrains.Annotations;

namespace Laraue.CodeTranslation.Abstractions.Metadata.Generators
{
	/// <summary>
	/// Class can generates <see cref="PropertyMetadata">metadata</see> for <see cref="PropertyMetadata">property</see>.
	/// </summary>
	public interface IPropertyMetadataGenerator : IMetadataGenerator
	{
		/// <summary>
		/// Generates <see cref="PropertyMetadata">metadata</see> for some <see cref="PropertyInfo">Clr property</see>.
		/// </summary>
		/// <returns></returns>
		[NotNull]
		PropertyMetadata GetMetadata(PropertyInfo property);
	}
}