using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Abstractions.Output.Metadata;

namespace Laraue.CodeTranslation.Common
{
	/// <inheritdoc />
	public abstract class OutputTypeMetadataGenerator : IOutputTypeMetadataGenerator
	{
		/// <summary>
		/// Mappings for types.
		/// </summary>
		protected readonly MapCollection Collection;

		/// <summary>
		/// Resolver, which contains all computed <see cref="OutputType"/> for <see cref="Type"/> keys.
		/// </summary>
		protected readonly IOutputTypeProvider TypeProvider;

		/// <summary>
		/// Initialize instance of type <see cref="OutputTypeMetadataGenerator"/> using passed mapping.
		/// </summary>
		/// <param name="collection"></param>
		/// <param name="provider"></param>
		protected OutputTypeMetadataGenerator(MapCollection collection, IOutputTypeProvider provider)
		{
			Collection = collection;
			TypeProvider = provider;
		}

		/// <inheritdoc />
		public OutputTypeMetadata Generate(TypeMetadata metadata)
		{
			return new()
			{
				Source = metadata,
				OutputType = GetOutputType(metadata),
			};
		}

		/// <summary>
		/// Get output type for some <see cref="TypeMetadata"/>.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		[CanBeNull]
		public OutputType GetOutputType(TypeMetadata metadata)
		{
			CacheAllUsedTypes(metadata);
			return TypeProvider.Get(metadata);
		}

		private void CacheAllUsedTypes(TypeMetadata metadata)
		{
			if (TypeProvider.Get(metadata) != null)
			{
				return;
			}

			TypeProvider.DependenciesGraph.AddToGraph(metadata);

			void ResolveAndCache(IEnumerable<TypeMetadata> types)
			{
				foreach (var type in types)
				{
					TypeProvider.GetOrAdd(type, () => GetOutputTypeInternal(type));
				}
			}

			ResolveAndCache(TypeProvider.DependenciesGraph.GetResolvingTypesSequence(metadata, DependencyType.Generic));
			ResolveAndCache(TypeProvider.DependenciesGraph.GetResolvingTypesSequence(metadata, DependencyType.Parent));
			ResolveAndCache(TypeProvider.DependenciesGraph.GetResolvingTypesSequence(metadata, DependencyType.Properties));
		}

		/// <summary>
		/// Get output type for some <see cref="TypeMetadata"/>.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		protected abstract OutputType GetOutputTypeInternal(TypeMetadata metadata);

		/// <summary>
		/// Returns <see cref="TypeMetadata"/> represents some parent type if it exists.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		[CanBeNull]
		protected abstract TypeMetadata GetUsedParentType([CanBeNull] TypeMetadata metadata);
	}
}
