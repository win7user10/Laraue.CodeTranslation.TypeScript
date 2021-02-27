using JetBrains.Annotations;

namespace Laraue.TypeScriptContractsGenerator.Abstractions.Metadata
{
	/// <summary>
	/// Contains meta information of some <see cref="System.Type">Clr type</see>.
	/// </summary>
	public interface ITypeMetadata : IMetadata
	{
		[CanBeNull]
		public ITypeMetadata ParentTypeMetadata { get; set; }
	}
}