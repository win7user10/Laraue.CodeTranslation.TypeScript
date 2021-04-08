using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	/// <summary>
	/// Contains full info about <see cref="OutputType"/> of the some property
	/// and information which can be required to generate property result code.
	/// </summary>
	public record OutputPropertyType
	{
		/// <summary>
		/// Source name of the property.
		/// </summary>
		[NotNull] public string PropertyName { get; init; }

		/// <summary>
		/// Property <see cref="OutputType"/>.
		/// </summary>
		[CanBeNull] public OutputType OutputType { get; init; }

		/// <summary>
		/// Source metadata of the property.
		/// </summary>
		[NotNull] public PropertyMetadata PropertyMetadata { get; init; }
	}
}