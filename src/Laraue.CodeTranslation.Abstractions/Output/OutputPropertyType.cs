using System.Reflection;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public record OutputPropertyType
	{
		public string PropertyName { get; init; }

		public PropertyInfo Source { get; init; }

		public OutputType OutputType { get; init; }

		public PropertyMetadata PropertyMetadata { get; init; }
	}
}