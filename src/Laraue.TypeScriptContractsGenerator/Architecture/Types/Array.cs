using System.Collections.Generic;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator.Architecture.Types
{
	public class Array : ReferenceOutputType
	{
		public Array(OutputTypeName name, IEnumerable<OutputType> usedTypes)
			: base(name, usedTypes)
		{
		}
	}
}
