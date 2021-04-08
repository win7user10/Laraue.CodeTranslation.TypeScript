using System;
using Laraue.CodeTranslation.Abstractions.Code;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Abstractions.Output.Metadata;
using Laraue.CodeTranslation.Abstractions.Translation;
using Laraue.CodeTranslation.Common;
using Laraue.CodeTranslation.NamingStrategies;
using Microsoft.Extensions.DependencyInjection;

namespace Laraue.CodeTranslation.TypeScript
{
    public class TypeScriptTranslatorBuilder
    {
        /// <summary>
        /// Creates <see cref="CodeTranslator"/> with default dependencies for TypeScript.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configureServices"></param>
        /// <returns></returns>
        public static ICodeTranslator Create(CodeTranslatorOptions options, Action<CodeTranslatorBuilder> configureServices = null)
        {
            var builder = new CodeTranslatorBuilder();

            options.ClassNamingStrategy ??= new PascalCaseStrategy();
            options.PathSegmentNamingStrategy ??= new CamelCaseNamingStrategy();

            builder
                .AddDependency<ICodeTranslator, CodeTranslator>()
                .AddDependency<ICodeGenerator, TypeScriptCodeGenerator>()
                .AddDependency<ITypePartsCodeGenerator, TypeScriptTypePartsGenerator>()
                .AddDependency<IMetadataGenerator, MetadataGenerator>()
                .AddDependency<IPropertyInfoResolver, PropertyInfoResolver>()
                .AddDependency<IOutputTypeProvider, TypeScriptOutputTypeProvider>()
                .AddDependency<IDependenciesGraph, DependenciesGraph>()
                .AddDependency<IOutputTypeMetadataGenerator, TypeScriptOutputTypeMetadataGenerator>(
                    sp => new TypeScriptOutputTypeMetadataGenerator(options.ConfigureTypeMap, sp.GetRequiredService<IOutputTypeProvider>()))
                .AddDependency<IIndentedStringBuilder, IndentedStringBuilder>(_ => new IndentedStringBuilder(options.IndentSize));

            configureServices?.Invoke(builder);

            return builder.Build();
        }
    }
}