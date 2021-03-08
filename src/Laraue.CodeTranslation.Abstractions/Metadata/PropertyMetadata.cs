using System.Reflection;
using JetBrains.Annotations;

namespace Laraue.CodeTranslation.Abstractions.Metadata
{
	/// <summary>
	/// Contains meta information of <see cref="MemberInfo">Clr property</see>.
	/// </summary>
	public record PropertyMetadata
	{
		public string PropertyName { get; init; }

		[NotNull]
		public TypeMetadata PropertyType { get; init; }

		[NotNull]
		public PropertyInfo Source { get; init; }
	}
}