using System;
using System.Reflection;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;

namespace Laraue.CodeTranslation
{
	public class PropertyMetadataGenerator : MetadataGenerator, IPropertyMetadataGenerator
	{
		/// <inheritdoc />
		public PropertyMetadata GetMetadata(MemberInfo property)
		{
			return new()
			{
				ClrType = GetClrType(property) ?? throw new ArgumentOutOfRangeException(),
				IsGeneric = IsGeneric(property.ReflectedType),
			};
		}

		protected virtual Type GetClrType(MemberInfo property)
		{
			return property.ReflectedType;
		}
	}
}