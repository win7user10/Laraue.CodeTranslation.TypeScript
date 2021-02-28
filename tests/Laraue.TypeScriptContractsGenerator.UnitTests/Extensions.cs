using System.Reflection;

namespace Laraue.TypeScriptContractsGenerator.UnitTests
{
	public static class Extensions
	{
		public static PropertyInfo GetPropertyInfo<T>(this string memberInfo)
		{
			return typeof(T).GetProperty(memberInfo);
		}
	}
}
