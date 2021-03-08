using System;
using System.Collections.Generic;
using System.Reflection;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;

namespace Laraue.CodeTranslation
{
	public class PropertyInfoResolver : IPropertyInfoResolver
	{
		/// <inheritdoc />
		public IEnumerable<PropertyInfo> GetProperties(Type clrType)
		{
			return clrType
				.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
		}
	}
}