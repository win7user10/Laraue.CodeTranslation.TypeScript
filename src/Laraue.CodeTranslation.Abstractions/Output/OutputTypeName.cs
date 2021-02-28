namespace Laraue.CodeTranslation.Abstractions.Output
{
	public record OutputTypeName
	{
		public string Name { get; init; }

		public OutputTypeName GenericOutputTypeName { get; init; }
	}
}