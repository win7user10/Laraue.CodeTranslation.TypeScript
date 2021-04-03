using System.Linq;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Class : DynamicOutputType
	{
		public Class(TypeMetadata metadata, IOutputTypeProvider provider) 
			: base(metadata, provider)
		{
			var className = GetNonGenericStringTypeName(metadata);
			var genericTypeNames = GetGenericTypeNames();

			Name = className + (genericTypeNames.Length > 0 ? $"<{string.Join(", ", genericTypeNames.Select(x => x.ToString()))}>" : string.Empty);
		}
	}
}