using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Interface : Class
	{
		public Interface(TypeMetadata metadata, IOutputTypeProvider provider) 
			: base(metadata, provider)
		{
		}
	}
}