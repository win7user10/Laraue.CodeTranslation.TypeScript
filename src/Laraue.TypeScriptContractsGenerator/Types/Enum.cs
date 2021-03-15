using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator.Types
{
	public class Enum : StaticOutputType
	{
		public override OutputTypeName Name { get; }

		[NotNull]
		public override TypeMetadata TypeMetadata { get; }

		public Dictionary<string, int> EnumValues
		{
			get
			{
				var type = TypeMetadata.ClrType;
				var enumNames = System.Enum.GetNames(type);
				return enumNames.ToDictionary(x => x, x => (int)System.Enum.Parse(type, x));
			}
		}

		public Enum(OutputTypeName name, [NotNull]TypeMetadata typeMetadata)
		{
			Name = name;
			TypeMetadata = typeMetadata;
		}
	}
}