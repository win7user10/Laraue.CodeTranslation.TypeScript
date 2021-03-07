using System;
using System.Reflection;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;

namespace Laraue.CodeTranslation
{
	public class PropertyMetadataGenerator : TypeMetadataGenerator, IPropertyMetadataGenerator
	{
		/// <inheritdoc />
		public virtual PropertyMetadata GetMetadata(PropertyInfo property)
		{
			var clrType = GetClrType(property);
			var notNullableType = GetNotNullableType(clrType);

			return new()
			{
				ClrType = notNullableType ?? throw new ArgumentOutOfRangeException(),
				IsGeneric = IsGeneric(notNullableType),
				IsEnum = IsEnum(notNullableType),
				GenericTypeArguments = GetGenericTypeParameters(notNullableType),
				IsEnumerable = IsEnumerable(notNullableType),
				IsDictionary = IsDictionary(notNullableType),
				IsNullable = IsNullable(clrType),
			};
		}

		protected virtual Type GetClrType(PropertyInfo property)
		{
			return property.PropertyType;
		}
	}
}