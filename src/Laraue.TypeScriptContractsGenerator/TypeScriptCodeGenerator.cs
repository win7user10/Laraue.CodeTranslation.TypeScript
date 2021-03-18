using System;
using System.Text;
using Laraue.CodeTranslation;
using Laraue.CodeTranslation.Abstractions.Code;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator
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

			codeBuilder.AppendLine($"export class {_generator.GenerateName(type)} {{");
			using (codeBuilder.Indent())
			{
				foreach (var propertyType in type.Properties)
				{
					codeBuilder.AppendLine(GenerateCode(propertyType));
				}
			}
			codeBuilder.AppendLine("}");

			return codeBuilder.ToString();
		}
	}
}