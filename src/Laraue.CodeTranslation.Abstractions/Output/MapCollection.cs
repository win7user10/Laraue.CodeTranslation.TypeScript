using System;
using System.Collections.Generic;
using System.Linq;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	/// <summary>
	/// Collection of mappings between <see cref="TypeMetadata"/> and functions returns <see cref="OutputType"/> represented by <see cref="MapDescriptor"/>.
	/// </summary>
	public class MapCollection
	{
		private readonly List<MapDescriptor> _source = new();

		/// <summary>
		/// Adds new map between <see cref="TypeMetadata"/> and <see cref="OutputType"/> when result type does not depends from input.
		/// </summary>
		/// <typeparam name="TInput"></typeparam>
		/// <typeparam name="TOutput"></typeparam>
		/// <returns></returns>
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

		/// <summary>
		/// Adds new map between <see cref="TypeMetadata"/> and <see cref="OutputType"/> when result type does not depends from input.
		/// </summary>
		/// <typeparam name="TInput"></typeparam>
		/// <typeparam name="TOutput"></typeparam>
		/// <param name="outputType"></param>
		/// <returns></returns>
		public MapCollection AddMap<TInput, TOutput>(TOutput outputType)
			where TOutput : OutputType
		{
			return AddToSource(new MapDescriptor()
			{
				GetOutputType = _ => outputType,
				IsApplicable = (metadata) => metadata.ClrType == typeof(TInput),
			});
		}

		/// <summary>
		/// Adds new map between <see cref="TypeMetadata"/> and <see cref="OutputType"/> when result type depends from input type.
		/// </summary>
		/// <typeparam name="TOutput"></typeparam>
		/// <param name="whenShouldBeUsed"></param>
		/// <param name="getOutputTypeFunc"></param>
		/// <returns></returns>
		public MapCollection AddMap<TOutput>(Func<TypeMetadata, bool> whenShouldBeUsed, Func<TypeMetadata, TOutput> getOutputTypeFunc)
			where TOutput : OutputType
		{
			return AddToSource(new MapDescriptor()
			{
				GetOutputType = getOutputTypeFunc,
				IsApplicable = whenShouldBeUsed,
			});
		}

		/// <summary>
		/// Get first applicable <see cref="MapDescriptor"/> for the passed type.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		public MapDescriptor GetMap(TypeMetadata metadata)
		{
			return _source.LastOrDefault(x => x.IsApplicable(metadata));
		}
	}
}