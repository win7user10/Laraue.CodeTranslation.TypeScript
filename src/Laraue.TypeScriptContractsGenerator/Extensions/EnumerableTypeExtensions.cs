using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Laraue.TypeScriptContractsGenerator.Extensions
{
    public static class EnumerableTypeExtensions
    {
        /// <summary>
        /// Where condition for classes by attribute.
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IEnumerable<Type> WithAttribure<TAttribute>(this IEnumerable<Type> types) where TAttribute : Attribute
            => types.Where(type => type.GetCustomAttribute(typeof(TAttribute)) != null);
    }
}
