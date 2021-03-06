namespace Laraue.CodeTranslation.Abstractions.Output
{
	public class Class : OutputType
	{
		public override OutputTypeName Name { get; }

		public Class(OutputTypeName name)
		{
			Name = name;
		}
	}
}