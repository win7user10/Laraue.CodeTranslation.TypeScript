using System;
using Laraue.CodeTranslation.Abstractions.Code;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;
using Laraue.CodeTranslation.Abstractions.Output.Metadata;
using Laraue.CodeTranslation.TypeScript;
using Microsoft.Extensions.DependencyInjection;

namespace Laraue.CodeTranslation.TypeDiscovery
{
    public class CodeTranslatorBuilder
    {
        private readonly ServiceCollection _services = new ServiceCollection();

        public CodeTranslatorBuilder AddDependency<TService, TImplementation>()
            where TService : class 
            where TImplementation : class, TService
        {
            _services.AddSingleton<TService, TImplementation>();
            return this;
        }

        public CodeTranslatorBuilder AddDependency<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation)
            where TService : class
            where TImplementation : class, TService
        {
            _services.AddSingleton<TService, TImplementation>(implementation);
            return this;
        }

        public static CodeTranslator Create(int indentSize = 2)
        {
            var builder = new CodeTranslatorBuilder();

            builder
                .AddDependency<ICodeGenerator, TypeScriptCodeGenerator>()
                .AddDependency<ITypePartsCodeGenerator, TypeScriptTypePartsGenerator>()
                .AddDependency<IMetadataGenerator, MetadataGenerator>()
                .AddDependency<IPropertyInfoResolver, PropertyInfoResolver>()
                .AddDependency<IOutputTypeMetadataGenerator, TypeScriptOutputTypeMetadataGenerator>()
                .AddDependency<IIndentedStringBuilder, IndentedStringBuilder>(sp =>
                    new IndentedStringBuilder(indentSize));

            return new CodeTranslator(builder._services.BuildServiceProvider());
        }
    }
}