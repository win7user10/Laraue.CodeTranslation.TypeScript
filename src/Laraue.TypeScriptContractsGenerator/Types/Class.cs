using System.Collections.Generic;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator.Types
{
	public class Class : DynamicOutputType
	{
		public Class(OutputTypeName name, IEnumerable<OutputType> usedTypes, IEnumerable<OutputPropertyType> properties, TypeMetadata metadata) 
			: base(name, usedTypes, properties, metadata)
		{
		}
	}
}