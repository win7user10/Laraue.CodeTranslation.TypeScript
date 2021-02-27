using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Typed.Abstractions.Code
{
	public interface IPropertyCodeGenerator : CodeTranslation.Abstractions.Code.IPropertyCodeGenerator
	{
		/// <summary>
		/// Get translated access modifier of a <see cref="PropertyMetadata">property</see>.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		[CanBeNull]
		string GetAccessModifier(PropertyMetadata metadata);

		/// <summary>
		/// Get translated type of a <see cref="PropertyMetadata">property</see>.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		[CanBeNull]
		string GetType(PropertyMetadata metadata);
	}
}