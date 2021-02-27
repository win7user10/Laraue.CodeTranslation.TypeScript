using System.Reflection;
using JetBrains.Annotations;

namespace Laraue.CodeTranslation.Abstractions.Metadata.Generators
{
	/// <summary>
	/// Class can generates <see cref="IPropertyMetadata">metadata</see> for <see cref="IPropertyMetadata">property</see>.
	/// </summary>
	public interface IPropertyMetadataGenerator : IMetadataGenerator
	{
		/// <summary>
		/// Generates <see cref="IPropertyMetadata">metadata</see> for some <see cref="MemberInfo">Clr property</see>.
		/// </summary>
		/// <returns></returns>
		[NotNull]
		IPropertyMetadata GetMetadata(MemberInfo property);
	}
}