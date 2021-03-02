using System;
using System.Collections.Generic;
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
				ClrType = type ?? throw new ArgumentOutOfRangeException(),
				IsGeneric = IsGeneric(type),
				IsEnum = IsEnum(type),
				GenericTypeArguments = GetGenericTypeParameters(type),
				IsEnumerable = IsEnumerable(type),
				IsDictionary = IsDictionary(type),
				ParentTypeMetadata = type.BaseType is not null ? GetMetadata(type.BaseType) : null,
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