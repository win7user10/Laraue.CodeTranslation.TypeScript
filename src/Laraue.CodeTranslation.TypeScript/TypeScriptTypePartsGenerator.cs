using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Code;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Abstractions.Translation;
using Laraue.CodeTranslation.TypeScript.Types;
using Array = Laraue.CodeTranslation.TypeScript.Types.Array;
using Boolean = Laraue.CodeTranslation.TypeScript.Types.Boolean;
using Enum = Laraue.CodeTranslation.TypeScript.Types.Enum;
using String = Laraue.CodeTranslation.TypeScript.Types.String;

namespace Laraue.CodeTranslation.TypeScript
{
    public class TypeScriptTypePartsGenerator : ITypePartsCodeGenerator
    {
        private readonly CodeTranslatorOptions _options;

        public TypeScriptTypePartsGenerator(CodeTranslatorOptions options)
        {
            _options = options;
        }

        /// <inheritdoc />
        public string[] GenerateImportStrings(OutputType type)
        {
            var strings = type.UsedTypes.Select(usedType => GetImportString(type, usedType));
            return strings.ToArray();
        }

        /// <inheritdoc />
        public virtual string GenerateName(OutputTypeName name) => _options.TypeNamingStrategy.Resolve(name.Name);

        /// <inheritdoc />
        [NotNull] public virtual string[] GetFilePathParts(OutputType type) 
            => type.TypeMetadata
                   ?.ClrType
                   ?.FullName
                   ?.Split('.')
                   ?.Select(
                       x => _options
                           .PathSegmentNamingStrategy
                           .Resolve(x))
                   ?.ToArray()
               ?? System.Array.Empty<string>();

        [CanBeNull]
        public virtual string GetFileName(OutputType type) => _options.PathSegmentNamingStrategy.Resolve(type.Name.Name);

        /// <inheritdoc />
        public virtual string GenerateName(OutputPropertyType property) => _options.PropertiesNamingStrategy.Resolve(property.PropertyName);

        /// <inheritdoc />
        public virtual string GenerateDefaultValue(OutputPropertyType property)
        {
            if (IsNullableType(property))
            {
                return "null";
            }

            return property.OutputType switch
            {
                Number => "0",
                String => "''",
                Date => "new Date()",
                Enum => GenerateDefaultEnumValue(property),
                Boolean => "true",
                _ => throw new NotImplementedException($"{property?.OutputType?.GetType()?.ToString() ?? property.PropertyMetadata.ToString()} default value is unknown")
            };
        }

        /// <inheritdoc />
        public virtual string GeneratePropertyType(OutputPropertyType property)
        {
            var codeBuilder = new StringBuilder(property.OutputType.Name);
            if (IsNullableType(property))
            {
                codeBuilder.Append(" | null");
            }

            return codeBuilder.ToString();
        }

        /// <inheritdoc />
        public virtual bool ShouldBeUsedTypingInPropertyDefinition(OutputPropertyType property)
        {
            if (IsNullableType(property))
            {
                return true;
            }

            switch (property.OutputType)
            {
                case Number:
                case Boolean:
                case String:
                    return false;
                default:
                    return true;
            }
        }

        protected virtual bool IsNullableType(OutputPropertyType property)
        {
            var propertyType = property.PropertyMetadata.PropertyType;
            return propertyType.IsNullable
                   || property.OutputType is Array
                   || propertyType.ClrType.IsClass
                   || propertyType.ClrType.IsInterface;
        }

        protected virtual string GenerateDefaultEnumValue(OutputPropertyType property)
        {
            if (property.OutputType is not Enum enumType)
            {
                throw new InvalidOperationException($"Impossible to get enum value from the type {property.OutputType.GetType()}");
            }

            var enumName = GenerateName(property.OutputType.Name);
            var enumValues = enumType.EnumValues;
            var firstEnumValue = enumValues.OrderBy(x => x.Value).First().Key;
            return $"{enumName}.{firstEnumValue}";
        }

        public string GetImportString(OutputType importerType, OutputType importingType)
        {
            if (importerType == importingType)
            {
                throw new InvalidOperationException($"Importer type {importerType} attempts to import self");
            }

            if (importerType?.TypeMetadata == null)
            {
                throw new InvalidOperationException($"{importerType} does not contain a data to generate import string.");
            }

            if (importingType?.TypeMetadata == null)
            {
                throw new InvalidOperationException($"{importingType} does not contain a data to generate import string.");
            }

            var importerTypeParts = GetFilePathParts(importerType);
            using var importerTypePartsEnumerator = importerTypeParts!.Take(importerTypeParts.Length - 1).GetEnumerator();
            var importingTypeParts = GetFilePathParts(importingType);
            using var importingTypePartsEnumerator = importingTypeParts!.Take(importingTypeParts.Length - 1).GetEnumerator();

            var pathSegmentsToImport = new List<string>(5);
            var upperLevelPartsCount = 0;
            var isImportFromThisFolder = false;

            // TODO - simplify this algorithm.
            while (true)
            {
                var importerHasSegment = importerTypePartsEnumerator.MoveNext();
                var importingHasSegment = importingTypePartsEnumerator.MoveNext();

                switch (importingHasSegment)
                {
                    case true when importerHasSegment:
                    {
                        if (importerTypePartsEnumerator.Current == importingTypePartsEnumerator.Current)
                            continue;
                        pathSegmentsToImport.Add(importingTypePartsEnumerator.Current);
                        upperLevelPartsCount++;
                        while (importingTypePartsEnumerator.MoveNext())
                            pathSegmentsToImport.Add(importingTypePartsEnumerator.Current);
                        while (importerTypePartsEnumerator.MoveNext())
                            upperLevelPartsCount++;
                        break;
                    }
                    case true:
                    {
                        isImportFromThisFolder = true;
                        pathSegmentsToImport.Add(importingTypePartsEnumerator.Current);
                        while (importingTypePartsEnumerator.MoveNext())
                            pathSegmentsToImport.Add(importingTypePartsEnumerator.Current);
                        break;
                    }
                    default:
                    {
                        if (importerHasSegment)
                        {
                            upperLevelPartsCount++;
                            while (importerTypePartsEnumerator.MoveNext())
                                upperLevelPartsCount++;
                        }
                        else
                        {
                            isImportFromThisFolder = true;
                        }

                        break;
                    }
                }

                break;
            }

            if (upperLevelPartsCount > 0)
            {
                pathSegmentsToImport = Enumerable.Range(1, upperLevelPartsCount)
                    .Select(x => "..")
                    .Concat(pathSegmentsToImport)
                    .ToList();
            }

            pathSegmentsToImport.Add(GetFileName(importingType));

            var path = isImportFromThisFolder ? "./" : string.Empty;
            path += string.Join("/", pathSegmentsToImport.ToArray());

            return $"import {{ {GenerateName(importingType.Name)} }} from '{path}'";
        }
    }
}