using Laraue.TypeScriptContractsGenerator.Generators;
using Laraue.TypeScriptContractsGenerator.Typing;
using System;
using System.Collections.Generic;

namespace Laraue.TypeScriptContractsGenerator.CodeGenerator
{
    public class TypeScriptGenerator
    {
        /// <summary>
        /// For these types will be generated TS code.
        /// </summary>
        public IEnumerable<Type> SourceTypes { get; }

        /// <summary>
        /// Ident size of string builder.
        /// </summary>
        public int IndentSize { get; }

        private TsCodeGenerator _tsCodeGenerator { get; }

        private TsTypeGenerator _tsTypeGenerator { get; }

        public TypeScriptGenerator(
            IEnumerable<Type> sourceTypes,
            int indentSize,
            TsCodeGenerator tsCodeGenerator,
            TsTypeGenerator tsTypeGenerator)
        {
            SourceTypes = sourceTypes ?? throw new ArgumentNullException(nameof(sourceTypes));
            if (indentSize < 1)
                throw new ArgumentOutOfRangeException(nameof(indentSize), "Ident size should be a positive number");

            IndentSize = indentSize;
            _tsCodeGenerator = tsCodeGenerator;
            _tsTypeGenerator = tsTypeGenerator;
        }

        /// <summary>
        /// Generate ts code for configured source types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GeneratedType> GenerateTsCode()
        {
            foreach (var clrType in SourceTypes)
            {
                var tsType = new TsType(clrType, _tsTypeGenerator, _tsCodeGenerator);
                var code = _tsCodeGenerator.GetTsCode(tsType, new IndentedStringBuilder(IndentSize));
                var path = _tsCodeGenerator.GetFilePathParts(tsType);
                yield return new GeneratedType(clrType, code, path);
            }
        }
    }
}
