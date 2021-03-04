using System;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public record MapDescriptor
	{
		public Func<TypeMetadata, OutputType> GetOutputType { get; init; }

		public Func<TypeMetadata, bool> IsApplicable { get; init; }
	}
}
