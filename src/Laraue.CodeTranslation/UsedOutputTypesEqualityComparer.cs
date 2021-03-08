using System.Collections.Generic;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation
{
	public class UsedOutputTypesEqualityComparer : IEqualityComparer<OutputType>
	{
		public bool Equals(OutputType x, OutputType y)
		{
			if (ReferenceEquals(x, y)) return true;
			if (x is null || y is null) return false;
			return x.GetType() == y.GetType() && Equals(x.Name.Name, y.Name.Name);
		}

		public int GetHashCode(OutputType obj)
		{
			return obj.Name?.Name != null ? obj.Name.Name.GetHashCode() : 0;
		}
	}
}