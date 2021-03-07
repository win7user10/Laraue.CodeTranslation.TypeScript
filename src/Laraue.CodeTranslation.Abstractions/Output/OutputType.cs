using System.Collections.Generic;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class OutputType
	{
		public abstract OutputTypeName Name { get; }

		public abstract IEnumerable<OutputType> UsedTypes { get; }

		/// <inheritdoc />
		public override string ToString()
		{
			return $"ClrName = {GetType().Name} OutputName = {Name}";
		}
	}
}