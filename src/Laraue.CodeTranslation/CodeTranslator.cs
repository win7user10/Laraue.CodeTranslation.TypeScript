using System;
using System.Collections.Generic;
using System.Linq;
using Laraue.CodeTranslation.Abstractions.Code;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;
using Laraue.CodeTranslation.Abstractions.Output.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Laraue.CodeTranslation
{
    public class CodeTranslator : IDisposable
    {
        private readonly ServiceProvider _provider;

        internal CodeTranslator(ServiceProvider provider)
        {
            _provider = provider;
        }

        public GeneratedCode GenerateTypeCode(Type type)
        {
            var metadataGenerator = _provider.GetRequiredService<IMetadataGenerator>();
            var outputTypeGenerator = _provider.GetRequiredService<IOutputTypeMetadataGenerator>();
            var codeGenerator = _provider.GetRequiredService<ICodeGenerator>();
            var typePartsGenerator = _provider.GetRequiredService<ITypePartsCodeGenerator>();

            var typeMetadata = metadataGenerator.GetMetadata(type);
            var outputType = outputTypeGenerator.Generate(typeMetadata).OutputType;

            var fileParts = typePartsGenerator.GetFilePathParts(outputType);
            var code = codeGenerator.GenerateCode(outputType);

            return new GeneratedCode { Code = code, FilePathSegments = fileParts };
        }

        public IEnumerable<GeneratedCode> GenerateTypesCode(IEnumerable<Type> types)
        {
            return types.Select(GenerateTypeCode);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _provider.Dispose();
        }
    }
}