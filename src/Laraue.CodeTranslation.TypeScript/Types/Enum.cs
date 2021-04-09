using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Enum : StaticOutputType
	{
		public Dictionary<string, int> EnumValues
		{
			get
			{
				var type = TypeMetadata!.ClrType;
				var enumNames = System.Enum.GetNames(type);
				return enumNames.ToDictionary(x => x, x => (int)System.Enum.Parse(type, x));
			}
		}

		public Enum([NotNull] TypeMetadata metadata)
		{
			Name = GetNonGenericStringTypeName(metadata);
			TypeMetadata = metadata;
		}
	}
}