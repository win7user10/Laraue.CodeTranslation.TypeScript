using System;
using System.Collections.Generic;
using System.Reflection;

namespace Laraue.CodeTranslation.Abstractions.Metadata.Generators
{
	/// <summary>
	/// This type is used to determine, which properties from the type should be generated.
	/// </summary>
	public interface IPropertyInfoResolver
	{
		IEnumerable<PropertyInfo> GetProperties(Type clrType);
	}
}