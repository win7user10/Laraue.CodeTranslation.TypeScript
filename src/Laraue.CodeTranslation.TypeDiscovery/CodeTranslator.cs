using System;
using Laraue.CodeTranslation.Abstractions.Code;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;
using Laraue.CodeTranslation.Abstractions.Output.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Laraue.CodeTranslation.TypeDiscovery
{
    public class CodeTranslator : IDisposable
    {
        private readonly ServiceProvider _provider;

        internal CodeTranslator(ServiceProvider provider)
        {
            _provider = provider;
        }

        public string GenerateTypeCode<T>()
        {
            var metadataGenerator = _provider.GetRequiredService<IMetadataGenerator>();
            var outputTypeGenerator = _provider.GetRequiredService<IOutputTypeMetadataGenerator>();
            var codeGenerator = _provider.GetRequiredService<ICodeGenerator>();

            var typeMetadata = metadataGenerator.GetMetadata(typeof(T));
            var outputTypeMetadata = outputTypeGenerator.Generate(typeMetadata);
            return codeGenerator.GenerateCode(outputTypeMetadata.OutputType);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _provider.Dispose();
        }
    }
}