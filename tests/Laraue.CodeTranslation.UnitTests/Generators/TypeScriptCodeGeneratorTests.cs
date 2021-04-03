using System.Collections.Generic;
using System.Linq;
using Laraue.CodeTranslation.Common;
using Laraue.CodeTranslation.TypeScript;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Laraue.CodeTranslation.UnitTests.Generators
{
	public class TypeScriptCodeGeneratorTests
	{
		private readonly DependenciesGraph _dependenciesGraph = new();
		private readonly OutputTypeProvider _provider;

		public TypeScriptCodeGeneratorTests()
		{
			_provider = new TypeScriptOutputTypeProvider(_dependenciesGraph);
		}

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
			var code = GetPropertySourceCode<MainClass>(propertyName);
			Assert.Equal(exceptedCode, code);
		}

		[Fact]
		public void CodeTranslationForEnum()
		{
			var code = GetTypeSourceCode<EnumStartsWith10>();
			Assert.Equal(
@"export enum EnumStartsWith10 {
  Value0 = 10,
  Value1
}", code);
		}

		[Fact]
		public void CodeTranslationForRecursiveClass()
		{
			var code = GetTypeSourceCode<RecursiveClass>();
			Assert.Equal(
@"export class RecursiveClass {
  recursiveProperty: RecursiveClass[] | null = null;
}", code);
		}

		internal class RecursiveClass { public IEnumerable<RecursiveClass> RecursiveProperty { get; set; } }


		[Fact]
		public void CodeTranslationForInheritedClass()
		{
			var code = GetTypeSourceCode<InheritedClass>();
			Assert.Equal(
@"import { RecursiveClass } from './recursiveClass'

export class InheritedClass extends RecursiveClass {
}", code);
		}

		internal class InheritedClass : RecursiveClass { }

		private string GetPropertySourceCode<T>(string propertyName)
		{
			var metadataGenerator = new MetadataGenerator(new PropertyInfoResolver());
			var typeMetadata = metadataGenerator.GetMetadata(typeof(T));

			var outputTypeGenerator = new TypeScriptOutputTypeMetadataGenerator(null, _provider);
			var outputType = outputTypeGenerator.GetOutputType(typeMetadata);
			var propertyOutputType = outputType.Properties.Single(x => x.PropertyMetadata.Source.Name == propertyName);

			var propertyCodeGenerator = new TypeScriptCodeGenerator(new TypeScriptTypePartsGenerator());
			var propertyCode = propertyCodeGenerator.GenerateCode(propertyOutputType);

			return propertyCode;
		}

		private string GetTypeSourceCode<T>()
		{
			var metadataGenerator = new MetadataGenerator(new PropertyInfoResolver());
			var typeMetadata = metadataGenerator.GetMetadata(typeof(T));

			var outputTypeGenerator = new TypeScriptOutputTypeMetadataGenerator(null, _provider);
			var outputType = outputTypeGenerator.GetOutputType(typeMetadata);

			var codeGenerator = new TypeScriptCodeGenerator(new TypeScriptTypePartsGenerator());
			return codeGenerator.GenerateCode(outputType);
		}
	}
}
