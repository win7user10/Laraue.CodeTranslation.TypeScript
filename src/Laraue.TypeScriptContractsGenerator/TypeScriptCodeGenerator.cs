using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Laraue.CodeTranslation.Abstractions.Code;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.TypeScriptContractsGenerator.Extensions;
using Laraue.TypeScriptContractsGenerator.Types;
using Array = Laraue.TypeScriptContractsGenerator.Types.Array;
using Enum = Laraue.TypeScriptContractsGenerator.Types.Enum;
using String = Laraue.TypeScriptContractsGenerator.Types.String;

namespace Laraue.TypeScriptContractsGenerator
{
	public class TypeScriptCodeGenerator : ICodeGenerator
	{
		/// <inheritdoc />
		public string GenerateCode(OutputPropertyType property)
		{
			var codeBuilder = new StringBuilder();
			codeBuilder.Append(GenerateName(property));

			if (ShouldBeUsedTypingInPropertyDefinition(property))
			{
				codeBuilder.Append($": {GenerateType(property)}");
			}

			codeBuilder.Append($" = {GenerateDefaultValue(property)};");

			return codeBuilder.ToString();
		}

		public string GenerateCode(OutputType type)
		{
			throw new NotImplementedException();
		}

		protected virtual string GenerateName(OutputPropertyType property) => property.PropertyName.ToCamelCase();
		protected virtual string GenerateName(OutputType type) => type.Name.Name.ToPascalCase();

		protected virtual string GenerateType(OutputPropertyType property)
		{
			var codeBuilder = new StringBuilder(property.OutputType.Name);
			if (IsNullableType(property))
			{
				codeBuilder.Append(" | null");
			}

			return codeBuilder.ToString();
		}

		protected virtual string GenerateDefaultValue(OutputPropertyType property)
		{
			if (IsNullableType(property))
			{
				return "null";
			}

			if (property.OutputType is Number)
			{
				return "0";
			}

			if (property.OutputType is String)
			{
				return "''";
			}

			if (property.OutputType is Enum)
			{
				return GenerateDefaultEnumValue(property);
			}

			throw new NotImplementedException($"{property.OutputType.GetType()} default value is unknown");
		}

		protected virtual string GenerateDefaultEnumValue(OutputPropertyType property)
		{
			if (property.OutputType is not Enum enumType)
			{
				throw new InvalidOperationException($"Impossible to get enum value from the type {property.OutputType.GetType()}");
			}

			var enumName = GenerateName(property.OutputType);
			var enumValues = enumType.EnumValues;
			var firstEnumValue = enumValues.OrderBy(x => x.Value).First().Key;
			return $"{enumName}.{firstEnumValue}";

		}

		protected virtual bool ShouldBeUsedTypingInPropertyDefinition(OutputPropertyType property)
		{
			if (property.OutputType is Number)
			{
				return false;
			}

			if (property.OutputType is String)
			{
				if (!IsNullableType(property))
				{
					return false;
				}
			}

			return true;
		}

		protected virtual bool IsNullableType(OutputPropertyType property)
		{
			var propertyType = property.PropertyMetadata.PropertyType;
			return propertyType.IsNullable
				   || property.OutputType is Array
			       || propertyType.ClrType.IsClass;
		}
	}
}