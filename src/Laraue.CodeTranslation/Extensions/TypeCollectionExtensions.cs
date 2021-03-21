using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Laraue.CodeTranslation.Extensions
{
    public static class TypeCollectionExtensions
    {
        /// <summary>
        /// Loads all types referenced to assembly with passed as generic <see cref="Type"/> and creates new <see cref="TypeCollection"/> with types from it satisfied passed <paramref name="filterLoadingType"/> condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="filterLoadingType"></param>
        /// <returns></returns>
        public static TypeCollection AddTypesFromTypeAssembly<T>(this TypeCollection collection, Func<Type, bool> filterLoadingType = null)
        {
            var assembly = typeof(T).Assembly;
            return collection.AddAssemblyTypes(assembly, filterLoadingType);
        }

        /// <summary>
        /// <para>Loads all referenced to run project assemblies satisfied passed <paramref name="filterLoadingAssemblyName"/> condition.</para>
        /// <para>From these assemblies will be taken only types satisfied passed <paramref name="filterLoadingType"/> condition.</para>
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="filterLoadingAssemblyName"></param>
        /// <param name="filterLoadingType"></param>
        /// <returns></returns>
        public static TypeCollection AddTypesFromAllReferencedAssemblies(this TypeCollection collection, [CanBeNull]Func<string, bool> filterLoadingAssemblyName = null, [CanBeNull]Func<Type, bool> filterLoadingType = null)
        {
            var assemblyNames = (IEnumerable<string>)Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            if (filterLoadingAssemblyName is not null)
            {
                assemblyNames = assemblyNames.Where(filterLoadingAssemblyName);
            }

            var assemblies = assemblyNames.Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));
            foreach (var assembly in assemblies)
            {
                collection.AddAssemblyTypes(assembly, filterLoadingType);
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