using System;
using Laraue.CodeTranslation.Abstractions.Code;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;
using Laraue.CodeTranslation.Abstractions.Output.Metadata;
using Laraue.CodeTranslation.Common;

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
        public static CodeTranslator Create(CodeTranslatorOptions options, Action<CodeTranslatorBuilder> configureServices = null)
        {
            var builder = new CodeTranslatorBuilder();

            builder
                .AddDependency<ICodeGenerator, TypeScriptCodeGenerator>()
                .AddDependency<ITypePartsCodeGenerator, TypeScriptTypePartsGenerator>()
                .AddDependency<IMetadataGenerator, MetadataGenerator>()
                .AddDependency<IPropertyInfoResolver, PropertyInfoResolver>()
                .AddDependency<IOutputTypeMetadataGenerator, TypeScriptOutputTypeMetadataGenerator>(_ => new TypeScriptOutputTypeMetadataGenerator(options.ConfigureTypeMap))
                .AddDependency<IIndentedStringBuilder, IndentedStringBuilder>(_ => new IndentedStringBuilder(options.IndentSize));

            configureServices?.Invoke(builder);

            return builder.Build();
        }
    }
}