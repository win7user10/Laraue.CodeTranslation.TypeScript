using System;
using Laraue.CodeTranslation.Extensions;

namespace Laraue.CodeTranslation
{
	public abstract class MetadataGenerator
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
	}
}