using Laraue.TypeScriptContractsGenerator.CodeGenerator;
using Laraue.TypeScriptContractsGenerator.Typing;
using System;
using System.Collections.Generic;

namespace Laraue.TypeScriptContractsGenerator.Generators
{
    public abstract class TsCodeGenerator
    {
        public abstract string GetTypeName(TsReflectionClass tsReflectionClass);

        public abstract string GetDefaultPropertyValue(TsProperty tsProperty);

        public abstract string GetPropertyName(TsProperty tsProperty);

        public abstract string GetPropertyType(TsReflectionClass tsProperty);

        public abstract bool IsNullable(TsProperty tsProperty);

        public abstract bool ShouldBeImported(TsReflectionClass tsProperty);

        public abstract bool ShouldBeInherited(Type clrType);

        public abstract Dictionary<string, int> GetEnumValues(TsReflectionClass tsType);

        public abstract string GetFileName(TsReflectionClass tsReflectionClass);

        public abstract string[] GetFilePathParts(TsReflectionClass tsReflectionClass);

        public abstract string GetTsPropertyCode(TsProperty tsProperty);

        public abstract string GetTsImportCode(TsReflectionClass importer, TsReflectionClass importing);

        public abstract string GetTsCode(TsReflectionClass tsReflectionClass, IndentedStringBuilder sb);
    }
}
