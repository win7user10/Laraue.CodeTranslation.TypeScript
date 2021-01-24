using Laraue.TypeScriptContractsGenerator.CodeGenerator;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Laraue.TypeScriptContractsGenerator.Storage
{
    public static class FileStorageExtensions
    {
        public static void Store(this IEnumerable<GeneratedType> types, string rootPath, bool dropRootFolderOnStart)
        {
            if (dropRootFolderOnStart && Directory.Exists(rootPath))
            {
                Directory.Delete(rootPath, true);
            }

            foreach (var type in types)
            {
                var fileFolder = Path.Combine(
                    rootPath, 
                    Path.Combine(type.RelativeFilePathSegments.Take(type.RelativeFilePathSegments.Length - 1).ToArray()));

                var fileName = type.RelativeFilePathSegments.Last() + ".ts";

                Directory.CreateDirectory(fileFolder);

                File.WriteAllText(Path.Combine(fileFolder, fileName), type.TsCode);
            }
        }
    }
}
