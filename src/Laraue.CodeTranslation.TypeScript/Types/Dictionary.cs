using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Dictionary : DynamicOutputType
	{
		public Dictionary(TypeMetadata metadata, IOutputTypeProvider provider) : base(metadata, provider)
		{
			Name = new Any().Name;
		}
	}
}
