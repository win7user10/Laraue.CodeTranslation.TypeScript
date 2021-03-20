using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Laraue.CodeTranslation.Extensions
{
    public static class FileStorageExtensions
    {
        public static void Store(this IEnumerable<GeneratedCode> types, string rootPath, bool dropRootFolderOnStart, string fileExtension = "ts")
        {
            if (dropRootFolderOnStart && Directory.Exists(rootPath))
            {
                Directory.Delete(rootPath, true);
            }

            foreach (var type in types)
            {
                var fileFolder = Path.Combine(
                    rootPath,
                    Path.Combine(type.FilePathSegments.Take(type.FilePathSegments.Length - 1).ToArray()));

                var fileName = type.FilePathSegments.Last() + "." + fileExtension;

                Directory.CreateDirectory(fileFolder);
                File.WriteAllText(Path.Combine(fileFolder, fileName), type.Code);
            }
        }
    }
}