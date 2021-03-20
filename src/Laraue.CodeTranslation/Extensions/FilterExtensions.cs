using System;
using System.Reflection;

namespace Laraue.CodeTranslation.Extensions
{
    public static class FilterExtensions
    {
        /// <summary>
        /// Where condition for classes by attribute.
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool WithAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            return type.GetCustomAttribute(typeof(TAttribute)) != null;
        }
    }
}