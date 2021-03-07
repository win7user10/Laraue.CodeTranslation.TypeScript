using System;
using System.Collections.Generic;
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
			var notNullableType = GetNotNullableType(type);
			return new()
			{
				ClrType = notNullableType ?? throw new ArgumentOutOfRangeException(),
				IsGeneric = IsGeneric(notNullableType),
				IsEnum = IsEnum(notNullableType),
				GenericTypeArguments = GetGenericTypeParameters(notNullableType),
				IsEnumerable = IsEnumerable(notNullableType),
				IsDictionary = IsDictionary(notNullableType),
				ParentTypeMetadata = notNullableType.BaseType is not null ? GetMetadata(notNullableType.BaseType) : null,
				IsNullable = IsNullable(type),
			};
		}

		protected virtual IEnumerable<TypeMetadata> GetGenericTypeParameters(Type type)
		{
			var clrTypes = type.IsDictionary()
				? type.GetDictionaryTypes()
				: type.IsGenericEnumerable()
					? new[] { type.GetGenericEnumerableType() }
					: type.GenericTypeArguments;

			foreach (var clrType in clrTypes)
			{
				yield return GetMetadata(clrType);
			}
		}
	}
}