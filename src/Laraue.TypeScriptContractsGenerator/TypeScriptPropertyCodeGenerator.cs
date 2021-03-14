using System;
using System.Text;
using Laraue.CodeTranslation.Abstractions.Code;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.TypeScriptContractsGenerator.Extensions;
using Laraue.TypeScriptContractsGenerator.Types;
using Array = Laraue.TypeScriptContractsGenerator.Types.Array;
using String = Laraue.TypeScriptContractsGenerator.Types.String;

namespace Laraue.TypeScriptContractsGenerator
{
	public class TypeScriptPropertyCodeGenerator : IPropertyCodeGenerator
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
		
		public virtual string GenerateName(OutputPropertyType property)
		{
			return property.PropertyName.ToCamelCase();
		}

		public virtual string GenerateType(OutputPropertyType property)
		{
			var codeBuilder = new StringBuilder(property.OutputType.Name);
			if (IsNullableType(property))
			{
				codeBuilder.Append(" | null");
			}

			return codeBuilder.ToString();
		}

		public virtual string GenerateDefaultValue(OutputPropertyType property)
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

			throw new NotImplementedException($"{property.OutputType.GetType()} default value is unknown!");
		}

		public virtual bool ShouldBeUsedTypingInPropertyDefinition(OutputPropertyType property)
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

		public virtual bool IsNullableType(OutputPropertyType property)
		{
			var propertyType = property.PropertyMetadata.PropertyType;
			return propertyType.IsNullable
				   || property.OutputType is Array
			       || propertyType.ClrType.IsClass;
		}
	}
}