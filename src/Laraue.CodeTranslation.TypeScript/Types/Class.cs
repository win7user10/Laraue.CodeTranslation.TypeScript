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
		}

		/// <inheritdoc />
		public override OutputTypeName Name
		{
			get
			{
				if (TypeMetadata is null)
				{
					return string.Empty;
				}

				var className = GetNonGenericStringTypeName(TypeMetadata);
				var genericTypeNames = GetGenericTypeNames();
				return new OutputTypeName(className, genericTypeNames);
			}
		}
	}
}