using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Common
{
    public class DependenciesGraph
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

        public IReadOnlyList<TypeMetadata> GetResolvingTypesSequence(TypeMetadata metadata)
        {
            return GetResolvingTypesSequence(metadata, new List<TypeMetadata>(), new List<TypeMetadata>())
                .AsReadOnly();
        }

        private List<TypeMetadata> GetResolvingTypesSequence(TypeMetadata metadata, List<TypeMetadata> resolved, List<TypeMetadata> seen)
        {
            var node = GetNode(metadata);
            seen.Add(metadata);
            foreach (var edge in node.Edges)
            {
                if (!resolved.Contains(edge))
                {
                    if (seen.Contains(edge))
                    {
                        throw new StackOverflowException($"Type {edge} circular dependency");
                    }

                    GetResolvingTypesSequence(edge, resolved, seen);
                }
            }

            resolved.Add(metadata);
            return resolved;
        }
    }

    public class Node
    {
        private HashSet<TypeMetadata> _edges = new ();

        public bool AddEdge([NotNull]TypeMetadata metadata)
        {
            return _edges.Add(metadata);
        }

        public IEnumerable<TypeMetadata> Edges => _edges;

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Join(", ", _edges.Select(x => x.GetType().Name));
        }
    }
}