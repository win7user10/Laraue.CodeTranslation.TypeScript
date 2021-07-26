using System;
using Laraue.CodeTranslation.Common.Extensions;

namespace Laraue.CodeTranslation.Common
{
	public abstract class BaseMetadataGenerator
	{
		protected virtual bool IsGeneric(Type type)
		{
			return type.GenericTypeArguments.Length > 0 || type.IsDictionary() || type.IsGenericEnumerable();
		}

		protected virtual bool IsEnum(Type type)
		{
			return type.IsEnum;
		}

		protected virtual bool IsEnumerable(Type type)
		{
			return IsDictionary(type) || type.IsArray || type.IsGenericEnumerable();
		}

		protected virtual bool IsDictionary(Type type)
		{
			return type.IsDictionary();
		}

		protected virtual bool IsNullable(Type type)
		{
			return Nullable.GetUnderlyingType(type) is not null;
		}

		protected virtual Type GetNotNullableType(Type type)
		{
			var underlyingType = Nullable.GetUnderlyingType(type);
			return underlyingType ?? type;
		}
	}
}