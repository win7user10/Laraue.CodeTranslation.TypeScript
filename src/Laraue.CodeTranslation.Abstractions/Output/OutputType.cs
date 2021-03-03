namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class OutputType
	{
		public abstract string Name { get; }

		/// <inheritdoc />
		public override string ToString()
		{
			return $"ClrName = {GetType().Name} OutputName = {Name}";
		}
	}
}