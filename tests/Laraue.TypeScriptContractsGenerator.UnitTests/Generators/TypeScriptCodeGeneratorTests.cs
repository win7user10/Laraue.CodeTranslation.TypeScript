using System.Linq;
using Laraue.CodeTranslation;
using Xunit;

namespace Laraue.TypeScriptContractsGenerator.UnitTests.Generators
{
	public class TypeScriptCodeGeneratorTests
	{
		[Theory]
		[InlineData(nameof(MainClass.IntValue), "intValue = 0;")]
		[InlineData(nameof(MainClass.StringValue), "stringValue: string | null = null;")]
		[InlineData(nameof(MainClass.DoubleValue), "doubleValue = 0;")]
		[InlineData(nameof(MainClass.DecimalValue), "decimalValue = 0;")]
		[InlineData(nameof(MainClass.BigIntValue), "bigIntValue = 0;")]
		[InlineData(nameof(MainClass.GuidValue), "guidValue = '';")]
		[InlineData(nameof(MainClass.SubClassValue), "subClassValue: SubClass | null = null;")]
		[InlineData(nameof(MainClass.EnumerableIntValue), "enumerableIntValue: number[] | null = null;")]
		[InlineData(nameof(MainClass.ArrayIntValue), "arrayIntValue: number[] | null = null;")]
		[InlineData(nameof(MainClass.MultiDimensionIntArrayValue), "multiDimensionIntArrayValue: number[][] | null = null;")]
		[InlineData(nameof(MainClass.EnumerableWithArrayIntValue), "enumerableWithArrayIntValue: number[][] | null = null;")]
		[InlineData(nameof(MainClass.EnumerableSubClassValue), "enumerableSubClassValue: SubClass[] | null = null;")]
		[InlineData(nameof(MainClass.ArraySubClassValue), "arraySubClassValue: SubClass[] | null = null;")]
		[InlineData(nameof(MainClass.MultiDimensionArraySubClassValue), "multiDimensionArraySubClassValue: SubClass[][] | null = null;")]
		[InlineData(nameof(MainClass.EnumerableWithArraySubClassValue), "enumerableWithArraySubClassValue: SubClass[][] | null = null;")]
		[InlineData(nameof(MainClass.OneTypeGenericSubClassValue), "oneTypeGenericSubClassValue: OneTypeGenericSubClass<number> | null = null;")]
		[InlineData(nameof(MainClass.TwoTypesGenericSubValue), "twoTypesGenericSubValue: TwoTypeGenericSubClass<number, number> | null = null;")]
		[InlineData(nameof(MainClass.TwoTypesGenericSubValueArray), "twoTypesGenericSubValueArray: TwoTypeGenericSubClass<number, number>[] | null = null;")]
		[InlineData(nameof(MainClass.TwoTypesGenericSubValueEnumerable), "twoTypesGenericSubValueEnumerable: TwoTypeGenericSubClass<number, number>[] | null = null;")]
		[InlineData(nameof(MainClass.EnumStartsWith0), "enumStartsWith0: EnumStartsWith0 = EnumStartsWith0.Value0;")]
		public void PropertyTranslation(string propertyName, string exceptedCode)
		{
			var code = GetPropertySourceCode(propertyName);
			Assert.Equal(exceptedCode, code);
		}

		[Fact]
		public void CodeTranslation()
		{
			var code = GetTypeSourceCode();
		}

		private string GetPropertySourceCode(string propertyName)
		{
			var metadataGenerator = new MetadataGenerator(new PropertyInfoResolver());
			var typeMetadata = metadataGenerator.GetMetadata(typeof(MainClass));

			var outputTypeGenerator = new TypeScriptOutputTypeMetadataGenerator();
			var outputType = outputTypeGenerator.GetOutputType(typeMetadata);
			var propertyOutputType = outputType.Properties.Single(x => x.Source.Name == propertyName);

			var propertyCodeGenerator = new TypeScriptCodeGenerator();
			var propertyCode = propertyCodeGenerator.GenerateCode(propertyOutputType);

			return propertyCode;
		}

		private string GetTypeSourceCode()
		{
			var metadataGenerator = new MetadataGenerator(new PropertyInfoResolver());
			var typeMetadata = metadataGenerator.GetMetadata(typeof(MainClass));

			var outputTypeGenerator = new TypeScriptOutputTypeMetadataGenerator();
			var outputType = outputTypeGenerator.GetOutputType(typeMetadata);

			var codeGenerator = new TypeScriptCodeGenerator();
			return codeGenerator.GenerateCode(outputType);
		}
	}
}
