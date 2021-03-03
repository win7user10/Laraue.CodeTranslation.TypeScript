using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output.Metadata
{
	public record OutputTypeMetadata
	{
		public TypeMetadata Source { get; init; }

		public OutputType OutputType { get; init; }

		public bool IsGeneric => GenericTypeArguments?.Length > 0;

		public OutputType[] GenericTypeArguments { get; init; }
	}
}