using System;
using System.Linq;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;
using Laraue.CodeTranslation.Extensions;

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
				GenericTypeArguments = GetGenericTypeParameters(type),
				IsEnumerable = IsEnumerable(type),
			};
		}

		protected virtual TypeMetadata[] GetGenericTypeParameters(Type type)
		{
			return type.IsGenericEnumerable()
				? new[] { GetMetadata(type.GetGenericEnumerableType()) }
				: type.GenericTypeArguments.Select(GetMetadata).ToArray();
		}
	}
}