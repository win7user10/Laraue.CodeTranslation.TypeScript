using System;
using System.Collections.Generic;
using System.Linq;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Abstractions.Output.Metadata;

namespace Laraue.CodeTranslation
{
	public abstract class OutputTypeMetadataGenerator : IOutputTypeMetadataGenerator
	{
		private List<MappingDescriptor> _mappings = new();

		/// <inheritdoc />
		public OutputTypeMetadata Generate(TypeMetadata metadata)
		{
			return new OutputTypeMetadata
			{
				Source = metadata,
				OutputType = GetOutputType(metadata)
			};
		}

		public virtual OutputType GetOutputType(TypeMetadata metadata)
		{
			return GetMap(metadata.ClrType) ?? (metadata.ClrType.IsClass ? new Class(metadata) : new Interface(metadata));
		}

		public IOutputTypeMetadataGenerator AddMap<TInput, TOutput>()
			where TOutput : OutputType, new()
		{
			_mappings.Add(new MappingDescriptor
			{
				ClrType = typeof(TInput), 
				OutputType = new TOutput(),
			});

			return this;
		}

		/// <inheritdoc />
		public OutputType GetMap(Type type)
		{
			return _mappings.Forward().FirstOrDefault(x => x.ClrType == type)?.OutputType;
		}
	}

	public record MappingDescriptor
	{
		public Type ClrType { get; init; }
		public OutputType OutputType { get; init; }
	}

	internal static class ListExtensions
	{
		public static IEnumerable<T> Forward<T>(this List<T> items)
		{
			for (int i = items.Count - 1; i >= 0; i--)
			{
				yield return items[i];
			}
		}
	}
}
