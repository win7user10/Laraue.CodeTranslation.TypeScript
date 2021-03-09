using System.Linq;
using Laraue.CodeTranslation;
using Xunit;

namespace Laraue.TypeScriptContractsGenerator.UnitTests.Generators
{
	public class DefaultTsCodeGeneratorTests
	{
		[Theory]
		[InlineData(nameof(MainClass.IntValue), "intValue: number = 0;")]
		[InlineData(nameof(MainClass.StringValue), "stringValue: string | null = null;")]
		[InlineData(nameof(MainClass.DoubleValue), "doubleValue: number = 0;")]
		[InlineData(nameof(MainClass.DecimalValue), "decimalValue: number = 0;")]
		[InlineData(nameof(MainClass.BigIntValue), "bigIntValue: number = 0;")]
		[InlineData(nameof(MainClass.GuidValue), "guidValue: string = '';")]
		[InlineData(nameof(MainClass.SubClassValue), "subClassValue: SubClass | null = null;")]
		[InlineData(nameof(MainClass.EnumerableIntValue), "enumerableIntValue: Array<number> | null = null;")]
		[InlineData(nameof(MainClass.ArrayIntValue), "arrayIntValue: Array<number> | null = null;")]
		[InlineData(nameof(MainClass.MultiDimensionIntArrayValue), "multiDimensionIntArrayValue: Array<Array<number>> | null = null;")]
		[InlineData(nameof(MainClass.EnumerableWithArrayIntValue), "enumerableWithArrayIntValue: Array<Array<number>> | null = null;")]
		[InlineData(nameof(MainClass.EnumerableSubClassValue), "enumerableSubClassValue: Array<SubClass> | null = null;")]
		[InlineData(nameof(MainClass.ArraySubClassValue), "arraySubClassValue: Array<SubClass> | null = null;")]
		[InlineData(nameof(MainClass.MultiDimensionArraySubClassValue), "multiDimensionArraySubClassValue: Array<Array<SubClass>> | null = null;")]
		[InlineData(nameof(MainClass.EnumerableWithArraySubClassValue), "enumerableWithArraySubClassValue: Array<Array<SubClass>> | null = null;")]
		[InlineData(nameof(MainClass.OneTypeGenericSubClassValue), "oneTypeGenericSubClassValue: OneTypeGenericSubClass<number> | null = null;")]
		[InlineData(nameof(MainClass.TwoTypesGenericSubValue), "twoTypesGenericSubValue: TwoTypeGenericSubClass<number, number> | null = null;")]
		[InlineData(nameof(MainClass.TwoTypesGenericSubValueArray), "twoTypesGenericSubValueArray: Array<TwoTypeGenericSubClass<number, number>> | null = null;")]
		[InlineData(nameof(MainClass.TwoTypesGenericSubValueEnumerable), "twoTypesGenericSubValueEnumerable: Array<TwoTypeGenericSubClass<number, number>> | null = null;")]
		public void PropertyTranslation(string propertyName, string exceptedCode)
		{
			var code = GetPropertySourceCode(propertyName);
			Assert.Equal(exceptedCode, code);
		}

		private string GetPropertySourceCode(string propertyName)
		{
			var metadataGenerator = new MetadataGenerator(new PropertyInfoResolver());
			var typeMetadata = metadataGenerator.GetMetadata(typeof(MainClass));

			var outputTypeGenerator = new TypeScriptOutputTypeMetadataGenerator();
			var outputType = outputTypeGenerator.GetOutputType(typeMetadata);
			var propertyOutputType = outputType.Properties.Single(x => x.Source.Name == propertyName);

			var propertyCodeGenerator = new TypeScriptPropertyCodeGenerator();
			var propertyCode = propertyCodeGenerator.GenerateCode(propertyOutputType);

			return propertyCode;
		}
	}
}
