using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Laraue.TypeScriptContractsGenerator.CodeGenerator
{
    public static class TypeScriptGeneratorHelper
    {
        public static IEnumerable<Assembly> LoadAssemblies(Func<IEnumerable<string>, IEnumerable<string>> configureLoadingAssemblies)
        {
            var assemblyNames = (IEnumerable<string>)Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            assemblyNames = configureLoadingAssemblies?.Invoke(assemblyNames);
            return assemblyNames.Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));
        }
    }
}