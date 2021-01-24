using System;
using System.Collections.Generic;
using System.Linq;

namespace Laraue.TypeScriptContractsGenerator.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsGenericEnumerable(this Type type) => type.HasGenericEnumerableDefinition()
            || type.GetInterfaces().Any(x => x.HasGenericEnumerableDefinition());

        public static Type GetGenericEnumerableType(this Type type) =>
            type.HasGenericEnumerableDefinition()
                ? type.GenericTypeArguments.First()
                : type.GetInterfaces().First(x => x.HasGenericEnumerableDefinition()).GenericTypeArguments.First();

        private static bool HasGenericEnumerableDefinition(this Type type)
            => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);

        public static bool IsDictionary(this Type type) => type.HasDictionaryDefinition()
            || type.GetInterfaces().Any(x => x.HasDictionaryDefinition());

        private static bool HasDictionaryDefinition(this Type type)
            => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>);

    }
}
