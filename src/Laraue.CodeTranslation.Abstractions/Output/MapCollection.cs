using System;
using System.Collections.Generic;
using System.Linq;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public class MapCollection
	{
		private readonly List<MapDescriptor> _source = new();

		public MapCollection AddMap<TInput, TOutput>()
			where TOutput : OutputType, new()
		{
			return AddMap<TInput, TOutput>(new TOutput());
		}

		public MapCollection AddMap<TInput, TOutput>(TOutput outputType)
			where TOutput : OutputType
		{
			_source.Add(new MapDescriptor()
			{
				GetOutputType = (metadata, callNumber) => outputType,
				IsApplicable = (metadata) => metadata.ClrType == typeof(TInput),
			});

			return this;
		}

		public MapCollection AddMap<TOutput>(Func<TypeMetadata, bool> whenShouldBeUsed, Func<TypeMetadata, int, TOutput> getOutputTypeFunc)
			where TOutput : OutputType
		{
			_source.Add(new MapDescriptor()
			{
				GetOutputType = getOutputTypeFunc,
				IsApplicable = whenShouldBeUsed,
			});

			return this;
		}

		public MapDescriptor GetMap(TypeMetadata metadata)
		{
			return _source.FirstOrDefault(x => x.IsApplicable(metadata));
		}
	}
}