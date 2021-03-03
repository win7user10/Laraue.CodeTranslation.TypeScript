using System;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output.Metadata
{
	public interface IOutputTypeMetadataGenerator
	{
		public OutputTypeMetadata Generate(TypeMetadata metadata);
		public IOutputTypeMetadataGenerator AddMap<TInput, TOutput>() where TOutput : OutputType, new();
		public OutputType GetMap(Type type);
	}
}