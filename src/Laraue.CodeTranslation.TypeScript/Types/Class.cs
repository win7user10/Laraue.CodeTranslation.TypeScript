using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Class : DynamicOutputType
	{
		public Class(OutputTypeName name, TypeMetadata metadata, IOutputTypeProvider provider) 
			: base(name, metadata, provider)
		{
		}
	}
}