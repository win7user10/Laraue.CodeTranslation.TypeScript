using System;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;

namespace Laraue.CodeTranslation
{
	public class TypeMetadataGenerator : MetadataGenerator, ITypeMetadataGenerator
	{
		/// <inheritdoc />
		public TypeMetadata GetMetadata(Type type)
		{
			return new()
			{
				ClrType = type,
				IsGeneric = IsGeneric(type),
			};
		}
	}
}