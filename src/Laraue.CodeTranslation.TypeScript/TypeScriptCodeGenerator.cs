using System;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Code;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Abstractions.Translation;
using Laraue.CodeTranslation.Common;
using Laraue.CodeTranslation.TypeScript.Types;

namespace Laraue.CodeTranslation.TypeScript
{
	public class TypeScriptCodeGenerator : ICodeGenerator
	{
		private readonly ITypePartsCodeGenerator _generator;
		private readonly CodeTranslatorOptions _options;

		public TypeScriptCodeGenerator(ITypePartsCodeGenerator generator, CodeTranslatorOptions options)
		{
			_generator = generator ?? throw new ArgumentNullException(nameof(generator));
			_options = options ?? throw new ArgumentNullException(nameof(options));
		}

		/// <inheritdoc />
		public string GenerateCode(OutputPropertyType property)
		{
			var codeBuilder = new StringBuilder();

			codeBuilder.Append(_generator.GenerateName(property));

			if (_generator.ShouldBeUsedTypingInPropertyDefinition(property))
			{
				codeBuilder.Append($": {_generator.GeneratePropertyType(property)}");
			}

			codeBuilder.Append($" = {_generator.GenerateDefaultValue(property)};");

			return codeBuilder.ToString();
		}

		public string GenerateCode(OutputType type)
		{
			using var codeBuilder = new IndentedStringBuilder(_options.IndentSize);

			// Import classes
			var importStrings = _generator.GenerateImportStrings(type);
			foreach (var importString in importStrings)
			{
				codeBuilder.AppendLine(importString);
			}

			if (importStrings.Length > 0)
			{
				codeBuilder.AppendLine();
			}

			// Type definition
			using var resultBuilder = type switch
			{
				Types.Enum enumType => GenerateEnumCode(codeBuilder, enumType),
				Interface interfaceType => GenerateInterfaceCode(codeBuilder, interfaceType),
				Class classType => GenerateClassCode(codeBuilder, classType),
			};

			return resultBuilder.ToString();
		}

		[NotNull] protected virtual string GenerateImplementInterfacesString(ReferenceType type)
		{
			var codeBuilder = new StringBuilder();

			var interfaces = type?.Interfaces?.ToArray();
			if (interfaces?.Length > 0)
			{
				codeBuilder.Append("implements ");
				var interfacesString = string.Join(", ", interfaces.Select(x => _generator.GenerateName(x.Name)));
				codeBuilder.Append(interfacesString);
				codeBuilder.Append(" ");
			}

			return codeBuilder.ToString();
		}

		protected virtual IndentedStringBuilder GenerateInterfaceCode(IndentedStringBuilder codeBuilder, Interface type)
		{
			codeBuilder.Append($"export interface {_generator.GenerateName(type.Name)} ");
			codeBuilder.Append(GenerateImplementInterfacesString(type));

			codeBuilder.AppendLine("{");

			using (codeBuilder.Indent())
			{
				foreach (var propertyType in type.Properties)
				{
					codeBuilder.AppendLine(GenerateCode(propertyType));
				}
			}
			codeBuilder.Append("}");

			return codeBuilder;
		}

		protected virtual IndentedStringBuilder GenerateClassCode(IndentedStringBuilder codeBuilder, Class type)
		{
			codeBuilder.Append("export ");
			if (type.IsAbstract)
			{
				codeBuilder.Append("abstract ");
			}

			codeBuilder.Append($"class {_generator.GenerateName(type.Name)} ");

			if (type?.ParentTypeName is not null)
			{
				codeBuilder.Append($"extends {_generator.GenerateName(type.ParentTypeName)} ");
			}

			codeBuilder.Append(GenerateImplementInterfacesString(type));

			codeBuilder.AppendLine("{");

			using (codeBuilder.Indent())
			{
				foreach (var propertyType in type.Properties)
				{
					codeBuilder.AppendLine(GenerateCode(propertyType));
				}
			}
			codeBuilder.Append("}");

			return codeBuilder;
		}

		protected virtual IndentedStringBuilder GenerateEnumCode(IndentedStringBuilder codeBuilder, Types.Enum type)
		{
			codeBuilder.AppendLine($"export enum {_generator.GenerateName(type.Name)} {{");
			using (codeBuilder.Indent())
			{
				var currentEnumValue = 0;

				foreach (var enumValue in type.EnumValues)
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
					if (enumValue.Key != type.EnumValues.Last().Key)
						enumString += ",";

					codeBuilder.AppendLine(enumString);
				}
			}
			codeBuilder.Append("}");

			return codeBuilder;
		}
	}
}