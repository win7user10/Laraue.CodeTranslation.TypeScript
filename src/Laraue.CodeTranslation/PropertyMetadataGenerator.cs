using System;
using System.Reflection;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;

namespace Laraue.CodeTranslation
{
	public class PropertyMetadataGenerator : TypeMetadataGenerator, IPropertyMetadataGenerator
	{
		/// <inheritdoc />
		public PropertyMetadata GetMetadata(PropertyInfo property)
		{
			var type = GetClrType(property);

			return new()
			{
				ClrType = type ?? throw new ArgumentOutOfRangeException(),
				IsGeneric = IsGeneric(type),
				IsEnum = IsEnum(type),
				GenericTypeArguments = GetGenericTypeParameters(type),
				IsEnumerable = IsEnumerable(type),
			};
		}

		protected virtual Type GetClrType(PropertyInfo property)
		{
			return property.PropertyType;
		}
	}
}