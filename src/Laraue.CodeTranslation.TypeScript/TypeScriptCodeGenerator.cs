using System;
using System.Linq;
using System.Text;
using Laraue.CodeTranslation.Abstractions.Code;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Common;

namespace Laraue.CodeTranslation.TypeScript
{
	public class TypeScriptCodeGenerator : ICodeGenerator
	{
		private readonly ITypePartsCodeGenerator _generator;

		public TypeScriptCodeGenerator(ITypePartsCodeGenerator generator)
		{
			_generator = generator ?? throw new ArgumentNullException(nameof(generator));
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
			using var codeBuilder = new IndentedStringBuilder(2);

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
				_ => GenerateTypeCode(codeBuilder, type),
			};

			return resultBuilder.ToString();
		}

		protected virtual IndentedStringBuilder GenerateTypeCode(IndentedStringBuilder codeBuilder, OutputType type)
		{
			codeBuilder.Append($"export class {_generator.GenerateName(type.Name)} ");
			if (type?.ParentTypeName is not null)
			{
				codeBuilder.Append($"extends {_generator.GenerateName(type.ParentTypeName)}");
			}

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