using System.Reflection;

namespace Laraue.CodeTranslation.UnitTests
{
	public static class PropertyInfoExtensions
	{
		public static PropertyInfo GetPropertyInfo<T>(this string memberInfo)
		{
			return typeof(T).GetProperty(memberInfo);
		}
	}
}
