using System;

namespace Laraue.CodeTranslation
{
	public abstract class MetadataGenerator
	{
		protected virtual bool IsGeneric(Type type)
		{
			return type.GenericTypeArguments.Length > 0;
		}

		protected virtual bool IsEnum(Type type)
		{
			return type.GenericTypeArguments.Length > 0;
		}
	}
}