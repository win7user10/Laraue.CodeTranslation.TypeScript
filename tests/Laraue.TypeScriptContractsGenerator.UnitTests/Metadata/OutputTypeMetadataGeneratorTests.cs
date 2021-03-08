﻿using System;
using System.Collections.Generic;
using System.Linq;
using Laraue.CodeTranslation;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output.Metadata;
using Laraue.TypeScriptContractsGenerator.Architecture;
using Laraue.TypeScriptContractsGenerator.Architecture.Types;
using Xunit;
using Array = Laraue.TypeScriptContractsGenerator.Architecture.Types.Array;
using String = Laraue.TypeScriptContractsGenerator.Architecture.Types.String;

namespace Laraue.TypeScriptContractsGenerator.UnitTests.Metadata
{
	public class OutputTypeMetadataGeneratorTests
	{
		private readonly IOutputTypeMetadataGenerator _generator = new TypeScriptOutputTypeMetadataGenerator();

		[Theory]
		[InlineData(typeof(int), typeof(Number))]
		[InlineData(typeof(double), typeof(Number))]
		[InlineData(typeof(decimal), typeof(Number))]
		[InlineData(typeof(float), typeof(Number))]
		[InlineData(typeof(long), typeof(Number))]
		[InlineData(typeof(short), typeof(Number))]
		[InlineData(typeof(string), typeof(String))]
		[InlineData(typeof(Guid), typeof(String))]
		[InlineData(typeof(MainClass), typeof(Class))]
		[InlineData(typeof(MainClass[]), typeof(Array))]
		[InlineData(typeof(int?), typeof(Number))]
		public void GenerateOutputTypes(Type clrType, Type tsType)
		{
			var metadata = _generator.Generate(GetTypeMetadata(clrType));
			Assert.Equal(tsType, metadata.OutputType.GetType());
		}

		[Theory]
		[InlineData(typeof(TwoTypeGenericSubClass<int, decimal>), "TwoTypeGenericSubClass<number, number>")]
		[InlineData(typeof(TwoTypeGenericSubClass<int, TwoTypeGenericSubClass<int, decimal>>), "TwoTypeGenericSubClass<number, TwoTypeGenericSubClass<number, number>>")]
		[InlineData(typeof(IEnumerable<int>), "number[]")]
		[InlineData(typeof(IEnumerable<IEnumerable<int>>), "number[][]")]
		[InlineData(typeof(IEnumerable<int[]>), "number[][]")]
		[InlineData(typeof(IEnumerable<TwoTypeGenericSubClass<int, string>>), "TwoTypeGenericSubClass<number, string>[]")]
		[InlineData(typeof(IEnumerable<OneTypeGenericSubClass<OneTypeGenericSubClass<int[]>>>), "OneTypeGenericSubClass<OneTypeGenericSubClass<number[]>>[]")]
		public void GenerateTypeName(Type inputType, string exceptedName)
		{
			var typeMetadata = GetTypeMetadata(inputType);
			var metadata = _generator.Generate(typeMetadata);
			Assert.Equal(exceptedName, metadata.OutputType.Name);
		}

		[Theory]
		[InlineData(typeof(int), new string[0])]
		[InlineData(typeof(TwoTypeGenericSubClass<int, decimal>), new string[0])]
		[InlineData(typeof(TwoTypeGenericSubClass<int, OneTypeGenericSubClass<int>>), new[]{ "OneTypeGenericSubClass" })]
		[InlineData(typeof(SubClass), new[]{ "MainClass" })]
		public void GenerateUsedTypes(Type inputType, string[] exceptedTypes)
		{
			var typeMetadata = GetTypeMetadata(inputType);
			var metadata = _generator.Generate(typeMetadata);
			Assert.Equal(exceptedTypes, metadata.OutputType.UsedTypes.Select(x => x.Name.Name));
		}

		[Fact]
		public void GeneratePropertiesShouldNotThrowAnyException()
		{
			var typeMetadata = GetTypeMetadata(typeof(MainClass));
			var metadata = _generator.Generate(typeMetadata);
			_ = metadata.Source.PropertiesMetadata.ToArray();
		}

		private TypeMetadata GetTypeMetadata(Type type)
		{
			return new MetadataGenerator(new PropertyInfoResolver()).GetMetadata(type);
		}
	}
}