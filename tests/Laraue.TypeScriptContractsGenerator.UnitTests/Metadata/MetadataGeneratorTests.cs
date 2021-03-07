using System;
using System.Linq;
using Laraue.CodeTranslation;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Laraue.TypeScriptContractsGenerator.UnitTests.Metadata
{
	public class MetadataGeneratorTests
	{
		private readonly IPropertyMetadataGenerator _generator = new PropertyMetadataGenerator();

		[Fact]
		public void GenerateIntMetadata()
		{
			var meta = _generator.GetMetadata(nameof(MainClass.IntValue).GetPropertyInfo<MainClass>());
			Assert.False(meta.IsEnum);
			Assert.False(meta.IsEnumerable);
			Assert.False(meta.IsGeneric);
		}

		[Theory]
		[InlineData(nameof(MainClass.ArrayIntValue))]
		[InlineData(nameof(MainClass.EnumerableIntValue))]
		public void GenerateArrayIntMetadata(string propertyName)
		{
			var meta = _generator.GetMetadata(propertyName.GetPropertyInfo<MainClass>());
			Assert.False(meta.IsEnum);
			Assert.True(meta.IsEnumerable);
			Assert.True(meta.IsGeneric);
			var genericType = Assert.Single(meta.GenericTypeArguments);
			Assert.Equal(typeof(int), genericType.ClrType);
		}

		[Theory]
		[InlineData(nameof(MainClass.MultiDimensionIntArrayValue))]
		[InlineData(nameof(MainClass.EnumerableWithArrayIntValue))]
		public void GenerateMultiDimensionArrayIntMetadata(string propertyName)
		{
			var meta = _generator.GetMetadata(propertyName.GetPropertyInfo<MainClass>());
			Assert.False(meta.IsEnum);
			Assert.True(meta.IsEnumerable);
			Assert.True(meta.IsGeneric);
			var genericType = Assert.Single(meta.GenericTypeArguments);
			Assert.Equal(typeof(int[]), genericType.ClrType);
			var genericTypeGenericType = Assert.Single(genericType.GenericTypeArguments);
			Assert.Equal(typeof(int), genericTypeGenericType.ClrType);
		}

		[Theory]
		[InlineData(nameof(MainClass.DictionaryIntStringValue), typeof(int), typeof(string))]
		[InlineData(nameof(MainClass.JObjectValue), typeof(string), typeof(JToken))]
		public void GenerateDictionaryMetadata(string propertyName, Type exceptedKeyType, Type exceptedValueType)
		{
			var meta = _generator.GetMetadata(propertyName.GetPropertyInfo<MainClass>());
			Assert.False(meta.IsEnum);
			Assert.True(meta.IsEnumerable);
			Assert.True(meta.IsGeneric);
			Assert.True(meta.IsDictionary);
			var genericTypeArgs = meta.GenericTypeArguments.ToArray();
			Assert.Equal(2, genericTypeArgs.Length);
			var firstGenericType = genericTypeArgs[0];
			var secondGenericType = genericTypeArgs[1];
			Assert.Equal(exceptedKeyType, firstGenericType.ClrType);
			Assert.Equal(exceptedValueType, secondGenericType.ClrType);
		}


		[Theory]
		[InlineData(nameof(MainClass.TwoTypesGenericSubValueArray))]
		[InlineData(nameof(MainClass.TwoTypesGenericSubValueEnumerable))]
		public void GenerateTwoTypesGenericSubValueArrayMetadata(string propertyName)
		{
			var meta = _generator.GetMetadata(propertyName.GetPropertyInfo<MainClass>());
			Assert.False(meta.IsEnum);
			Assert.True(meta.IsEnumerable);
			Assert.True(meta.IsGeneric);
			var genericType = Assert.Single(meta.GenericTypeArguments);
			var genericTypeGenericTypeArgs = genericType.GenericTypeArguments.ToArray();
			Assert.Equal(2, genericTypeGenericTypeArgs.Length);
			Assert.Equal(typeof(int), genericTypeGenericTypeArgs[0].ClrType);
			Assert.Equal(typeof(decimal), genericTypeGenericTypeArgs[1].ClrType);
		}

		[Theory]
		[InlineData(nameof(MainClass.NullableIntValue), typeof(int))]
		[InlineData(nameof(MainClass.NullableGuidValue), typeof(Guid))]
		public void GenerateNullableMetadata(string propertyName, Type exceptedClrType)
		{
			var meta = _generator.GetMetadata(propertyName.GetPropertyInfo<MainClass>());
			Assert.False(meta.IsEnum);
			Assert.False(meta.IsEnumerable);
			Assert.False(meta.IsGeneric);
			Assert.True(meta.IsNullable);
			Assert.Equal(exceptedClrType, meta.ClrType);
		}
	}
}
