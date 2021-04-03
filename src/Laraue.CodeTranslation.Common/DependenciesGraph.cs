using System;
using System.Collections.Generic;
using System.Linq;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Common.Extensions;

namespace Laraue.CodeTranslation.Common
{
    public class DependenciesGraph : IDependenciesGraph
    {
        private readonly Dictionary<TypeMetadata, Node> _typeMetadataGraph = new();

        /// <summary>
        /// Get dependencies of some <see cref="TypeMetadata"/>.
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public Node GetNode(TypeMetadata metadata)
        {
            if (_typeMetadataGraph.TryGetValue(metadata, out var allUsedTypes)) return allUsedTypes;

            allUsedTypes = new Node();
            _typeMetadataGraph.Add(metadata, allUsedTypes);

            return allUsedTypes;
        }

        public IReadOnlyList<TypeMetadata> GetResolvingTypesSequence(TypeMetadata metadata, DependencyType type)
        {
            var sequence = GetResolvingTypesSequence(metadata, type, new List<TypeMetadata>(), new List<TypeMetadata>());
            return sequence
                .AsReadOnly();
        }

        private List<TypeMetadata> GetResolvingTypesSequence(TypeMetadata metadata, DependencyType type, List<TypeMetadata> resolved, List<TypeMetadata> seen)
        {
            var node = GetNode(metadata);
            seen.Add(metadata);

            var needEdges = node
                .Edges
                .Where(x => x.Type.HasFlag(type));

            foreach (var edge in needEdges)
            {
                if (resolved.Contains(edge.Metadata))
                {
                    continue;
                }

                if (seen.Contains(edge.Metadata))
                {
                    continue;
                }

                GetResolvingTypesSequence(edge.Metadata, type, resolved, seen);
            }

            resolved.Add(metadata);
            return resolved;
        }

        /// <summary>
        /// Add types from type generic types to hashset with used properties.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="dependencyType"></param>
        private void AddUsedGenericTypesToGraph(TypeMetadata metadata, DependencyType dependencyType)
        {
            if (metadata is null) return;
            var typesToInspect = new Queue<TypeMetadata>(metadata?.GenericTypeArguments ?? Enumerable.Empty<TypeMetadata>());
            while (typesToInspect.Count > 0)
            {
                var type = typesToInspect.Dequeue();
                var node = GetNode(metadata);
                if (!node.AddEdge(type, dependencyType | DependencyType.Generic)) continue;
                typesToInspect.EnqueueRange(type.GenericTypeArguments);
            }
        }

        /// <summary>
        /// Add types from type parent to hashset with used properties.
        /// </summary>
        /// <param name="metadata"></param>
        private void AddUsedParentTypesToGraph(TypeMetadata metadata)
        {
            var parentType = metadata.ParentTypeMetadata;
            while (true)
            {
                if (parentType is null) break;
                var node = GetNode(metadata);
                if (node.AddEdge(parentType, DependencyType.Parent))
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
                var node = GetNode(metadata);

                if (node.AddEdge(type, DependencyType.Properties))
                {
                    AddUsedGenericTypesToGraph(type, DependencyType.Properties);
                }
            }
        }

        /// <summary>
        /// Build dependencies graph for passed type.
        /// </summary>
        /// <param name="metadata"></param>
        public void AddToGraph(TypeMetadata metadata)
        {
            AddUsedGenericTypesToGraph(metadata, DependencyType.This);
            AddUsedParentTypesToGraph(metadata);
            AddPropertiesGenericTypesToGraph(metadata);
        }
    }
}