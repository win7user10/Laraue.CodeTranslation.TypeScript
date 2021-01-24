using Laraue.TypeScriptContractsGenerator.CodeGenerator;
using Laraue.TypeScriptContractsGenerator.Extensions;
using Laraue.TypeScriptContractsGenerator.Typing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Laraue.TypeScriptContractsGenerator.Generators
{
    public class DefaultTsCodeGenerator : TsCodeGenerator
    {
        public override string GetDefaultPropertyValue(TsProperty tsProperty)
        {
            if (tsProperty.IsNullable)
                return "null";

            return tsProperty.TypeScriptType switch
            {
                TsTypes.Any => "{}",
                TsTypes.Array => "[]",
                TsTypes.Boolean => "false",
                TsTypes.Enum => $"{tsProperty.TsType}.{tsProperty.EnumValues.First().Key}",
                TsTypes.Number => "0",
                TsTypes.String => "''",
                _ => $"new {tsProperty.TsType}()",
            };
        }

        public override Dictionary<string, int> GetEnumValues(TsReflectionClass tsType)
        {
            if (!tsType.IsEnum)
                return null;

            var result = new Dictionary<string, int>();
            var enumNames = Enum.GetNames(tsType.ClrType);

            foreach (var enumName in enumNames)
            {
                var enumValue = (int)Enum.Parse(tsType.ClrType, enumName);
                result.Add(enumName, enumValue);
            }

            return result;
        }

        public override string GetFileName(TsReflectionClass tsReflectionClass) => tsReflectionClass.TsType.ToCamelCase();

        public override string[] GetFilePathParts(TsReflectionClass tsReflectionClass)
        {
            var fileParts = tsReflectionClass.ClrType.Namespace.GetNamespaceParts()
                .Select(x => x.ToCamelCase())
                .ToList();
            fileParts.Add(GetFileName(tsReflectionClass));
            return fileParts.ToArray();
        }

        public override string GetPropertyName(TsProperty tsProperty) => tsProperty.PropertyInfo.Name.ToCamelCase();

        public override string GetPropertyType(TsReflectionClass tsProperty)
        {
            return tsProperty.TypeScriptType switch
            {
                // Should no be here, but no now idea how to handle this.
                TsTypes.Enum or TsTypes.Class => GetTypeName(tsProperty.NotNullableType),
                TsTypes.Date or TsTypes.Array => tsProperty.TypeScriptType.ToString(),
                _ => tsProperty.TypeScriptType.ToString().ToLowerInvariant(),
            };
        }

        public override bool IsNullable(TsProperty tsProperty)
            => tsProperty.IsAssignableFromNullable
                || new[]
                {
                    TsTypes.Class,
                    TsTypes.String,
                    TsTypes.Array,
                }.Contains(tsProperty.TypeScriptType);

        public override string GetTypeName(TsReflectionClass tsReflectionClass)
        {
            var typeName = tsReflectionClass
                .ClrType
                .Name;
            typeName = Regex.Replace(typeName, "`\\w+", "");
            return typeName.ToPascalCase();
        }

        public override string GetTsPropertyCode(TsProperty tsProperty)
        {
            var sb = new StringBuilder(30)
                .Append(tsProperty.TsName)
                .Append(": ")
                .Append(tsProperty.TsType)
                .Append(tsProperty.TsGenericDefinition);

            if (tsProperty.IsNullable)
                sb.Append(" | null");

            sb.Append(" = ")
                .Append(tsProperty.DefaultValue)
                .Append(';');

            return sb.ToString();
        }

        public override bool ShouldBeImported(TsReflectionClass tsProperty)
        {
            return new[] { TsTypes.Class, TsTypes.Enum }.Contains(tsProperty.TypeScriptType)
                && !new[] { typeof(object) }.Contains(tsProperty.NotNullableType.ClrType);
        }

        public override string GetTsImportCode(TsReflectionClass importerType, TsReflectionClass importingType)
        {
            if (importerType == importingType)
            {
                throw new InvalidOperationException($"Importer type {importerType} attempts to import self");
            }

            var importerTypeParts = GetFilePathParts(importerType);
            var importerTypePartsEnumerator = importerTypeParts.Take(importerTypeParts.Length - 1).GetEnumerator();
            var importingTypeParts = GetFilePathParts(importingType);
            var importingTypePartsEnumerator = importingTypeParts.Take(importingTypeParts.Length - 1).GetEnumerator();

            var pathSegmentsToImport = new List<string>(5);
            int upperLevelPartsCount = 0;
            bool isImportFromThisFolder = false;

            // TODO - simplify this algorythm
            while (true)
            {
                var importerHasSegment = importerTypePartsEnumerator.MoveNext();
                var importingHasSegment = importingTypePartsEnumerator.MoveNext();

                if (importingHasSegment && importerHasSegment)
                {
                    if (importerTypePartsEnumerator.Current == importingTypePartsEnumerator.Current)
                        continue;
                    pathSegmentsToImport.Add(importingTypePartsEnumerator.Current);
                    upperLevelPartsCount++;
                    while (importingTypePartsEnumerator.MoveNext())
                        pathSegmentsToImport.Add(importingTypePartsEnumerator.Current);
                    while (importerTypePartsEnumerator.MoveNext())
                        upperLevelPartsCount++;
                }
                else if (importingHasSegment && !importerHasSegment)
                {
                    isImportFromThisFolder = true;
                    pathSegmentsToImport.Add(importingTypePartsEnumerator.Current);
                    while (importingTypePartsEnumerator.MoveNext())
                        pathSegmentsToImport.Add(importingTypePartsEnumerator.Current);
                }
                else if (!importingHasSegment && importerHasSegment)
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

            if (upperLevelPartsCount > 0)
            {
                pathSegmentsToImport = Enumerable.Range(1, upperLevelPartsCount)
                    .Select(x => "..")
                    .Concat(pathSegmentsToImport)
                    .ToList();
            }

            pathSegmentsToImport.Add(GetFileName(importingType));

            string path = isImportFromThisFolder ? "./" : string.Empty;
            path += string.Join("/", pathSegmentsToImport.ToArray());

            return $"import {{ {importingType.TsType} }} from '{path}'";
        }

        public override string GetTsCode(TsReflectionClass tsReflectionClass, IndentedStringBuilder sb)
        {
            return tsReflectionClass.IsEnum
                ? GetTsEnumCode(tsReflectionClass, sb)
                : GetTsClassCode(tsReflectionClass, sb);
        }

        public virtual string GetTsClassCode(TsReflectionClass tsReflectionClass, IndentedStringBuilder sb)
        {
            var imports = tsReflectionClass.TsImports.ToArray();
            if (imports.Length > 0)
            {
                foreach (var import in imports)
                {
                    sb.AppendLine(import);
                }

                sb.AppendLine();
            }

            sb.Append($"export class {tsReflectionClass.TsName}");

            if (tsReflectionClass.ParentType != null)
                sb.Append($" extends {tsReflectionClass.ParentType.TsName}");

            sb.AppendLine(" {")
                .IncreaseIndent();

            foreach (var property in tsReflectionClass.Properties)
            {
                sb.AppendLine(property.TypeScriptCode);
            }

            sb.DecreaseIndent();
            sb.Append("}");

            return sb.ToString();
        }

        public virtual string GetTsEnumCode(TsReflectionClass tsReflectionClass, IndentedStringBuilder sb)
        {
            sb.Append($"export enum {tsReflectionClass.TsName}");

            sb.AppendLine(" {")
                .IncreaseIndent();

            var currentEnumValue = 0;
            foreach (var enumValue in tsReflectionClass.EnumValues)
            {
                string enumString;

                if (currentEnumValue != enumValue.Value)
                {
                    currentEnumValue = enumValue.Value;
                    enumString = $"{enumValue.Key} = {currentEnumValue}";
                }
                else
                {
                    enumString = enumValue.Key;
                }

                currentEnumValue++;
                if (enumValue.Key != tsReflectionClass.EnumValues.Last().Key)
                    enumString += ",";

                sb.AppendLine(enumString);
            }

            sb.DecreaseIndent()
                .Append("}");

            return sb.ToString();
        }

        public override bool ShouldBeInherited(Type clrType) => clrType != typeof(object);
    }
}
