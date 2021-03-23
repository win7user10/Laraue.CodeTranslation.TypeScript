using System.Reflection;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public record OutputPropertyType
	{
		public string PropertyName { get; init; }

		public PropertyInfo Source { get; init; }

		[NotNull]
		public OutputType OutputType { get; init; }

		public PropertyMetadata PropertyMetadata { get; init; }
	}
}