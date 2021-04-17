using System.Collections.Generic;
using System.Linq;
using Laraue.CodeTranslation.Abstractions.Translation;
using Laraue.CodeTranslation.Common;
using Laraue.CodeTranslation.TypeScript;
using Xunit;

namespace Laraue.CodeTranslation.UnitTests.Generators
{
	public class TypeScriptCodeGeneratorTests
	{
		private readonly DependenciesGraph _dependenciesGraph = new();
		private readonly OutputTypeProvider _provider;
		private readonly TypeScriptCodeGenerator _generator;
		private readonly OutputTypeMetadataGenerator _metadataGenerator;
		private readonly TypeScriptCodeTranslatorOptions _options = new ();

		public TypeScriptCodeGeneratorTests()
		{
			_provider = new TypeScriptOutputTypeProvider(_dependenciesGraph);
			_generator = new TypeScriptCodeGenerator(new TypeScriptTypePartsGenerator(_options), _options);
			_metadataGenerator = new TypeScriptOutputTypeMetadataGenerator(_options, _provider);
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
		[InlineData(nameof(MainClass.Boolean), "boolean = true;")]
		[InlineData(nameof(MainClass.NullableBoolean), "nullableBoolean: boolean | null = null;")]
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
		public void CodeTranslationForSomeParentClass()
		{
			var childCode = GetTypeSourceCode<ChildClass>();
			Assert.Equal(
				@"import { ParentClass } from './parentClass'

export class ChildClass extends ParentClass {
}", childCode);

			var parentCode = GetTypeSourceCode<ParentClass>();
			Assert.Equal(
				@"export class ParentClass {
  someProperty = 0;
}", parentCode);
		}

		internal class ParentClass { public int SomeProperty { get; set; } }
		internal class ChildClass : ParentClass { }


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

		[Fact]
		public void CodeTranslationForInheritedFrmInheritedClass()
		{
			var code = GetTypeSourceCode<InheritedFromInheritedClass>();
			Assert.Equal(
				@"import { InheritedClass } from './inheritedClass'

export class InheritedFromInheritedClass extends InheritedClass {
}", code);
		}

		internal class InheritedFromInheritedClass : InheritedClass { }

		private string GetPropertySourceCode<T>(string propertyName)
		{
			var metadataGenerator = new MetadataGenerator(new PropertyInfoResolver());
			var typeMetadata = metadataGenerator.GetMetadata(typeof(T));
			
			var outputType = _metadataGenerator.GetOutputType(typeMetadata);
			var propertyOutputType = outputType.Properties.Single(x => x.PropertyMetadata.Source.Name == propertyName);
			
			var propertyCode = _generator.GenerateCode(propertyOutputType);

			return propertyCode;
		}

		private string GetTypeSourceCode<T>()
		{
			var metadataGenerator = new MetadataGenerator(new PropertyInfoResolver());
			var typeMetadata = metadataGenerator.GetMetadata(typeof(T));

			var outputType = _metadataGenerator.GetOutputType(typeMetadata);

			return _generator.GenerateCode(outputType);
		}
	}
}
