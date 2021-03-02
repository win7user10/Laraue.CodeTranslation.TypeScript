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

		[Fact]
		public void GenerateIntStringDictionaryMetadata()
		{
			var meta = _generator.GetMetadata(nameof(MainClass.DictionaryIntStringValue).GetPropertyInfo<MainClass>());
			Assert.False(meta.IsEnum);
			Assert.True(meta.IsEnumerable);
			Assert.True(meta.IsGeneric);
			Assert.True(meta.IsDictionary);
			var genericTypeArgs = meta.GenericTypeArguments.ToArray();
			Assert.Equal(2, genericTypeArgs.Length);
			var firstGenericType = genericTypeArgs[0];
			var secondGenericType = genericTypeArgs[1];
			Assert.Equal(typeof(int), firstGenericType.ClrType);
			Assert.Equal(typeof(string), secondGenericType.ClrType);
		}

		[Fact]
		public void GenerateJObjectMetadata()
		{
			var meta = _generator.GetMetadata(nameof(MainClass.jObjectValue).GetPropertyInfo<MainClass>());
			Assert.False(meta.IsEnum);
			Assert.True(meta.IsEnumerable);
			Assert.True(meta.IsGeneric);
			Assert.True(meta.IsDictionary);
			var genericTypeArgs = meta.GenericTypeArguments.ToArray();
			Assert.Equal(2, genericTypeArgs.Length);
			var firstGenericType = genericTypeArgs[0];
			var secondGenericType = genericTypeArgs[1];
			Assert.Equal(typeof(string), firstGenericType.ClrType);
			Assert.Equal(typeof(JToken), secondGenericType.ClrType);
		}
	}
}
