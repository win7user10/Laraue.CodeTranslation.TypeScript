using System;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public record MapDescriptor
	{
		/// <summary>
		/// Method which allows to return output type from <see cref="TypeMetadata"/> and number of methodCall.
		/// </summary>
		public Func<TypeMetadata, int, OutputType> GetOutputType { get; init; }

		public Func<TypeMetadata, bool> IsApplicable { get; init; }
	}
}
