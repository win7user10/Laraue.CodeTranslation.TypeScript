using System;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	/// <summary>
	/// Represents function which can resolve <see cref="OutputType"/> from a <see cref="TypeMetadata"/>.
	/// </summary>
	public record MapDescriptor
	{
		/// <summary>
		/// Method which allows to return output type from <see cref="TypeMetadata"/>.
		/// </summary>
		public Func<TypeMetadata, OutputType> GetOutputType { get; init; }

		/// <summary>
		/// Returns, can descriptor be applied to a some <see cref="TypeMetadata"/>.
		/// </summary>
		public Func<TypeMetadata, bool> IsApplicable { get; init; }
	}
}