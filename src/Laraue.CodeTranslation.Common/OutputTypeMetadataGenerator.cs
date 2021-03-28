using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Abstractions.Output.Metadata;
using Laraue.CodeTranslation.Common.Extensions;

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
		/// Dependencies graph for called types.
		/// </summary>
		protected readonly DependenciesGraph DependenciesGraph = new();

		/// <summary>
		/// Resolver, which contains all computed <see cref="OutputType"/> for <see cref="Type"/> keys.
		/// </summary>
		protected readonly OutputTypeProvider TypeProvider = new();

		/// <summary>
		/// Initialize instance of type <see cref="OutputTypeMetadataGenerator"/> using passed mapping.
		/// </summary>
		/// <param name="collection"></param>
		protected OutputTypeMetadataGenerator(MapCollection collection)
		{
			Collection = collection;
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
			FillDependenciesGraph(metadata);

			var dependencyGraph = DependenciesGraph.GetResolvingTypesSequence(metadata);
			var usedTypes = dependencyGraph.Take(dependencyGraph.Count - 1);

			OutputType lastOutputType = null;
			foreach (var type in usedTypes)
			{
				lastOutputType = TypeProvider.GetOrAdd(type.ClrType, () => GetOutputTypeInternal(type));
			}

			return lastOutputType;
		}

		/// <summary>
		/// Get output type for some <see cref="TypeMetadata"/>.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		protected abstract OutputType GetOutputTypeInternal(TypeMetadata metadata);

		/// <summary>
		/// Add types from type generic types to hashset with used properties.
		/// </summary>
		/// <param name="metadata"></param>
		private void AddUsedGenericTypesToGraph(TypeMetadata metadata)
		{
			if (metadata is null) return;
			var typesToInspect = new Queue<TypeMetadata>(metadata?.GenericTypeArguments ?? Enumerable.Empty<TypeMetadata>());
			while (typesToInspect.Count > 0)
			{
				var type = typesToInspect.Dequeue();
				var node = DependenciesGraph.GetNode(metadata);
				if (!node.AddEdge(type)) continue;
				typesToInspect.EnqueueRange(type.GenericTypeArguments);
			}
		}

		/// <summary>
		/// Add types from type parent to hashset with used properties.
		/// </summary>
		/// <param name="metadata"></param>
		private void AddUsedParentTypesToGraph(TypeMetadata metadata)
		{
			var parentType = GetUsedParentType(metadata);
			while (true)
			{
				if (parentType is null) break;
				var node = DependenciesGraph.GetNode(metadata);
				if (node.AddEdge(parentType))
				{
					metadata = parentType;
					parentType = parentType.ParentTypeMetadata;
				}
				else
				{
					break;
				}
			}
		}

		/// <summary>
		/// Add types from type properties to hashset with used properties.
		/// </summary>
		/// <param name="metadata"></param>
		private void AddPropertiesGenericTypesToGraph(TypeMetadata metadata)
		{
			var types = new Queue<TypeMetadata>(metadata?.PropertiesMetadata.Select(x => x.PropertyType) ?? Enumerable.Empty<TypeMetadata>());
			while (types.Count > 0)
			{
				var type = types.Dequeue();
				var node = DependenciesGraph.GetNode(metadata);

				if (node.AddEdge(type))
				{
					AddUsedGenericTypesToGraph(type);
				}
			}
		}

		/// <summary>
		/// Returns <see cref="TypeMetadata"/> represents some parent type if it exists.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		[CanBeNull]
		protected abstract TypeMetadata GetUsedParentType([CanBeNull] TypeMetadata metadata);

		/// <summary>
		/// Build dependencies graph for passed type.
		/// </summary>
		/// <param name="metadata"></param>
		private void FillDependenciesGraph(TypeMetadata metadata)
		{
			AddUsedGenericTypesToGraph(metadata);
			AddUsedParentTypesToGraph(metadata);
			AddPropertiesGenericTypesToGraph(metadata);
		}
	}
}
