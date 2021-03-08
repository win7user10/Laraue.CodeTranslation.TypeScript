using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;
using Laraue.CodeTranslation.Extensions;

namespace Laraue.CodeTranslation
{
	public class MetadataGenerator : BaseMetadataGenerator, ITypeMetadataGenerator, IPropertyMetadataGenerator
	{
		protected readonly IPropertyInfoResolver PropertyInfoResolver;

		public MetadataGenerator(IPropertyInfoResolver propertyInfoResolver)
		{
			PropertyInfoResolver = propertyInfoResolver ?? throw new ArgumentNullException(nameof(propertyInfoResolver));
		}

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
				PropertiesMetadata = GetPropertiesMetadata(type),
			};
		}

		public virtual PropertyMetadata GetMetadata(PropertyInfo property)
		{
			var clrType = property.PropertyType;

			return new()
			{
				Source = property,
				PropertyType = GetMetadata(clrType),
				PropertyName = property.Name,
			};
		}

		protected IEnumerable<PropertyMetadata> GetPropertiesMetadata(Type type)
		{
			return PropertyInfoResolver
				.GetProperties(type)
				.Select(GetMetadata);
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