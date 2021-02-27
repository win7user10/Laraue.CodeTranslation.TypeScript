using JetBrains.Annotations;

namespace Laraue.CodeTranslation.Abstractions.Metadata
{
	/// <summary>
	/// Contains meta information of some <see cref="System.Type">Clr type</see>.
	/// </summary>
	public record TypeMetadata : Metadata
	{
		[CanBeNull]
		public TypeMetadata ParentTypeMetadata { get; set; }
	}
}