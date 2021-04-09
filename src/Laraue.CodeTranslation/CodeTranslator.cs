using System;
using System.Collections.Generic;
using System.Linq;
using Laraue.CodeTranslation.Abstractions.Code;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;
using Laraue.CodeTranslation.Abstractions.Output.Metadata;
using Laraue.CodeTranslation.Abstractions.Translation;

namespace Laraue.CodeTranslation
{
    /// <inheritdoc />
    public class CodeTranslator : ICodeTranslator
    {
        private readonly IMetadataGenerator _metadataGenerator;
        private readonly IOutputTypeMetadataGenerator _outputTypeMetadataGenerator;
        private readonly ICodeGenerator _codeGenerator;
        private readonly ITypePartsCodeGenerator _typePartsCodeGenerator;

        public CodeTranslator(IMetadataGenerator metadataGenerator, IOutputTypeMetadataGenerator outputTypeMetadataGenerator, ICodeGenerator codeGenerator, ITypePartsCodeGenerator typePartsCodeGenerator)
        {
            _metadataGenerator = metadataGenerator;
            _outputTypeMetadataGenerator = outputTypeMetadataGenerator;
            _codeGenerator = codeGenerator;
            _typePartsCodeGenerator = typePartsCodeGenerator;
        }

        /// <inheritdoc />
        public GeneratedCode GenerateTypeCode(Type type)
        {
            var typeMetadata = _metadataGenerator.GetMetadata(type);
            var outputType = _outputTypeMetadataGenerator.Generate(typeMetadata).OutputType;

            var fileParts = _typePartsCodeGenerator.GetFilePathParts(outputType);
            var code = _codeGenerator.GenerateCode(outputType);

            return new GeneratedCode { Code = code, FilePathSegments = fileParts };
        }


        /// <inheritdoc />
        public IEnumerable<GeneratedCode> GenerateTypesCode(IEnumerable<Type> types)
        {
            return types.Select(GenerateTypeCode);
        }
    }
}