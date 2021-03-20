using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Laraue.CodeTranslation.TypeDiscovery.Extensions
{
    public static class TypeCollectionExtensions
    {
        public static TypeCollection AddTypesFromTypeAssembly<T>(this TypeCollection collection, Func<Type, bool> filter = null)
        {
            var assembly = typeof(T).Assembly;
            return collection.AddAssemblyTypes(assembly, filter);
        }

        public static TypeCollection AddTypesFromAllReferencedAssemblies(this TypeCollection collection, Func<Type, bool> filter = null)
        {
            var assemblyNames = (IEnumerable<string>)Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var assemblies = assemblyNames.Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));
            foreach (var assembly in assemblies)
            {
                collection.AddAssemblyTypes(assembly, filter);
            }

            return collection;
        }

        private static TypeCollection AddAssemblyTypes(this TypeCollection collection, Assembly assembly, Func<Type, bool> filter = null)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (filter is null || filter(type))
                {
                    collection.AddType(type);
                }
            }

            return collection;
        }
    }
}