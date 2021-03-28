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

		private MapCollection AddToSource(MapDescriptor descriptor)
		{
			_source.Add(descriptor);
			return this;
		}

		public MapCollection AddMap<TInput, TOutput>(TOutput outputType)
			where TOutput : OutputType
		{
			return AddToSource(new MapDescriptor()
			{
				GetOutputType = _ => outputType,
				IsApplicable = (metadata) => metadata.ClrType == typeof(TInput),
			});
		}

		public MapCollection AddMap<TOutput>(Func<TypeMetadata, bool> whenShouldBeUsed, Func<TypeMetadata, TOutput> getOutputTypeFunc)
			where TOutput : OutputType
		{
			return AddToSource(new MapDescriptor()
			{
				GetOutputType = getOutputTypeFunc,
				IsApplicable = whenShouldBeUsed,
			});
		}

		public MapDescriptor GetMap(TypeMetadata metadata)
		{
			return _source.LastOrDefault(x => x.IsApplicable(metadata));
		}
	}
}