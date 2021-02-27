using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Typed.Abstractions.Code
{
	public interface IPropertyCodeGenerator : CodeTranslation.Abstractions.Code.IPropertyCodeGenerator
	{
		/// <summary>
		/// Get translated access modifier of a <see cref="IPropertyMetadata">property</see>.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		[CanBeNull]
		string GetAccessModifier(IPropertyMetadata metadata);

		/// <summary>
		/// Get translated type of a <see cref="IPropertyMetadata">property</see>.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		[CanBeNull]
		string GetType(IPropertyMetadata metadata);
	}
}