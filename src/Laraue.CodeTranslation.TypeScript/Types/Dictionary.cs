using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Dictionary : DynamicOutputType
	{
		public Dictionary(TypeMetadata metadata, IOutputTypeProvider provider) : base(GetName(), metadata, provider)
		{
		}

		private static OutputTypeName GetName()
		{
			return new Any().Name;
			/*try
			{
				return new("Dictionary", typeNames);
			}
			catch (Exception)
			{
				return new Any().Name;
			}*/
		}
	}
}
