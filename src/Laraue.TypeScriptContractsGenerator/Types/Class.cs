using System.Collections.Generic;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator.Types
{
	public class Class : ReferenceOutputType
	{
		public Class(OutputTypeName name, IEnumerable<OutputType> usedTypes, IEnumerable<OutputPropertyType> properties) 
			: base(name, usedTypes, properties)
		{
		}
	}
}