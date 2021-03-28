using System;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public record MapDescriptor
	{
		/// <summary>
		/// Method which allows to return output type from <see cref="TypeMetadata"/>.
		/// </summary>
		public Func<TypeMetadata, OutputType> GetOutputType { get; init; }

		public Func<TypeMetadata, bool> IsApplicable { get; init; }
	}
}