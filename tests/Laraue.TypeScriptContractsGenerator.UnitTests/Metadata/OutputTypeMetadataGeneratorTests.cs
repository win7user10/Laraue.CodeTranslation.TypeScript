﻿using System;
using Laraue.CodeTranslation;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;
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
		public void GenerateOutputTypes(Type clrType, Type tsType)
		{
			var metadata = _generator.Generate(new TypeMetadata { ClrType = clrType, IsEnumerable = clrType.IsArray });
			Assert.Equal(tsType, metadata.OutputType.GetType());
		}

		[Theory]
		[InlineData(typeof(TwoTypeGenericSubClass<int, decimal>), "TwoTypeGenericSubClass<number, number>")]
		[InlineData(typeof(TwoTypeGenericSubClass<int, TwoTypeGenericSubClass<int, decimal>>), "TwoTypeGenericSubClass<number, TwoTypeGenericSubClass<number, number>>")]
		public void GenerateTypeName(Type inputType, string exceptedName)
		{
			var typeMetadata = GetTypeMetadata(inputType);
			var metadata = _generator.Generate(typeMetadata);
			Assert.Equal(exceptedName, metadata.OutputType.Name);
		}

		private TypeMetadata GetTypeMetadata(Type type)
		{
			return new TypeMetadataGenerator().GetMetadata(type);
		}
	}
}