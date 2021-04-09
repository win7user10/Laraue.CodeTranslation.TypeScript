using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	/// <summary>
	/// Represents generated name of property for <see cref="OutputType"/>.
	/// </summary>
	public record OutputTypeName
	{
		/// <summary>
		/// Type name without generic and array parameters.
		/// </summary>
		[NotNull] public string Name { get; init; }

		/// <summary>
		/// Depth of child used to calculate array depth.
		/// </summary>
		[CanBeNull] public OutputTypeName ChildName { get; init; }

		/// <summary>
		/// Generic type names.
		/// </summary>
		[NotNull] public IEnumerable<OutputTypeName> GenericNames { get; init; }

		/// <summary>
		/// Is this type is array.
		/// </summary>
		public bool IsArray { get; init; }

		public OutputTypeName([NotNull] string name, [NotNull] IEnumerable<OutputTypeName> genericNames, bool isArray = false)
		{
			Name = name;
			GenericNames = genericNames.Where(x => x != null);
			IsArray = isArray;
		}

		private OutputTypeName(string name)
		{
			Name = name;
			GenericNames = Array.Empty<OutputTypeName>();
		}

		/// <summary>
		/// New name from a string.
		/// </summary>
		/// <param name="typeName"></param>
		public static implicit operator string([CanBeNull] OutputTypeName typeName)
			=> typeName?.ToString();

		/// <summary>
		/// New string name from <see cref="OutputTypeName"/>.
		/// </summary>
		/// <param name="typeName"></param>
		public static implicit operator OutputTypeName([CanBeNull] string typeName)
			=> new (typeName);

		/// <inheritdoc />
		public override string ToString()
		{
			var result = Name;
			var computedGenericNames = GenericNames.ToArray();

			if (computedGenericNames.Length> 0)
			{
				var genericNames = string.Join(", ", computedGenericNames.Select(x => x.ToString()));
				result += $"<{genericNames}>";
			}

			for (var i = 0; i < GetArrayDepth(); i++)
			{
				result += "[]";
			}

			return result;
		}

		private int GetArrayDepth()
		{
			var result = 0;
			if (IsArray) result++;
			result += ChildName?.GetArrayDepth() ?? 0;
			return result;
		}
	}
}