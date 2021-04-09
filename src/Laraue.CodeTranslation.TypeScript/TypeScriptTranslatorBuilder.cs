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

            builder
                .AddDependency(options)
                .AddDependency<ICodeTranslator, CodeTranslator>()
                .AddDependency<ICodeGenerator, TypeScriptCodeGenerator>()
                .AddDependency<ITypePartsCodeGenerator, TypeScriptTypePartsGenerator>()
                .AddDependency<IMetadataGenerator, MetadataGenerator>()
                .AddDependency<IPropertyInfoResolver, PropertyInfoResolver>()
                .AddDependency<IOutputTypeProvider, TypeScriptOutputTypeProvider>()
                .AddDependency<IDependenciesGraph, DependenciesGraph>()
                .AddDependency<IOutputTypeMetadataGenerator, TypeScriptOutputTypeMetadataGenerator>()
                .AddDependency<IIndentedStringBuilder, IndentedStringBuilder>();

            configureServices?.Invoke(builder);

            return builder.Build();
        }
    }
}