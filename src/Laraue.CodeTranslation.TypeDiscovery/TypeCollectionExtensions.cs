using System;

namespace Laraue.CodeTranslation.TypeDiscovery
{
    public static class TypeCollectionExtensions
    {
        public static TypeCollection AddAssemblyTypes<T>(this TypeCollection collection, Func<Type, bool> filter)
        {
            var assembly = typeof(T).Assembly;
            foreach (var type in assembly.GetTypes())
            {
                if (filter(type))
                {
                    collection.AddType(type);
                }
            }

            return collection;
        }
    }
}