namespace Laraue.CodeTranslation.Abstractions.Output
{
	/// <summary>
	/// Represents static output type, which always have the same type for any metadata.
	/// </summary>
	public class StaticOutputType : OutputType
	{
		public StaticOutputType(string type) : base(x => new OutputTypeName { Name = type })
		{ }
	}
}