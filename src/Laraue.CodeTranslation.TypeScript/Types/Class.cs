using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Class : DynamicOutputType
	{
		public Class(OutputTypeName name, OutputType[] usedTypes, OutputPropertyType[] properties, TypeMetadata metadata, OutputTypeName parentTypeName) 
			: base(name, usedTypes, properties, metadata)
		{
			ParentTypeName = parentTypeName;
		}
	}
}