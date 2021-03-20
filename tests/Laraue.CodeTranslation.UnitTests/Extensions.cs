using System.Reflection;

namespace Laraue.CodeTranslation.UnitTests
{
	public static class Extensions
	{
		public static PropertyInfo GetPropertyInfo<T>(this string memberInfo)
		{
			return typeof(T).GetProperty(memberInfo);
		}
	}
}
