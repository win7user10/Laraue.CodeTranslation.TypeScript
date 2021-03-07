using System.Collections.Generic;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation
{
	public class UsedOutputTypesEqualityComparer : IEqualityComparer<OutputType>
	{
		public bool Equals(OutputType x, OutputType y)
		{
			if (ReferenceEquals(x, y)) return true;
			if (ReferenceEquals(x, null)) return false;
			if (ReferenceEquals(y, null)) return false;
			if (x.GetType() != y.GetType()) return false;
			return Equals(x.Name.Name, y.Name.Name);
		}

		public int GetHashCode(OutputType obj)
		{
			return obj.Name?.Name != null ? obj.Name.Name.GetHashCode() : 0;
		}
	}
}