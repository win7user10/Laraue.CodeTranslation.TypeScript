using System.Collections.Generic;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator.Architecture.Types
{
	public class Class : ReferenceOutputType
	{
		public Class(OutputTypeName name, IEnumerable<OutputType> usedTypes) 
			: base(name, usedTypes)
		{
		}
	}
}