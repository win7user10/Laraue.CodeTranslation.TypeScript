using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Interface : ReferenceType
	{
		public Interface(TypeMetadata metadata, IOutputTypeProvider provider) 
			: base(metadata, provider)
		{
		}
	}
}