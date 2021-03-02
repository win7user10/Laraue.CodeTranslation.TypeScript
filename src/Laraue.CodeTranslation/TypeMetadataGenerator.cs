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
				ClrType = type,
				IsGeneric = IsGeneric(type),
				GenericTypeArguments = GetGenericTypeParameters(type),
				IsEnumerable = IsEnumerable(type),
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