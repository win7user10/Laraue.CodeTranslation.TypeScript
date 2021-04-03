using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	/// <summary>
	/// Represents one of node in <see cref="IDependenciesGraph"/>.
	/// </summary>
	public class Node
	{
		private readonly HashSet<NodeValue> _edges = new();

		/// <summary>
		/// Add new edge to node.
		/// </summary>
		/// <param name="metadata"></param>
		/// <param name="dependencyType"></param>
		/// <returns></returns>
		public bool AddEdge([NotNull] TypeMetadata metadata, DependencyType dependencyType)
		{
			var existsEdge = _edges.FirstOrDefault(x => x.Metadata == metadata);
			if (existsEdge is null)
			{
				return _edges.Add(new NodeValue { Metadata = metadata, Type = dependencyType });
			}

			if (existsEdge.Type.HasFlag(dependencyType))
			{
				return false;
			}

			existsEdge.Type |= dependencyType;
			return true;

		}

		/// <summary>
		/// Returns list of all edges of the node.
		/// </summary>
		public IEnumerable<NodeValue> Edges => _edges;

		/// <inheritdoc />
		public override string ToString()
		{
			return string.Join(", ", _edges.Select(x => x.Metadata.ClrType.Name));
		}
	}

	public class NodeValue
	{
		public TypeMetadata Metadata { get; set; }

		public DependencyType Type { get; set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return $"{Metadata}:{Type}";
		}
	}
}