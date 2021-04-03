using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public record OutputTypeName
	{
		[NotNull]
		public string Name { get; init; }

		[CanBeNull] 
		public OutputTypeName ChildName { get; init; }

		[NotNull]
		public IEnumerable<OutputTypeName> GenericNames { get; init; }

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

		public static implicit operator string([CanBeNull] OutputTypeName typeName)
			=> typeName?.ToString();

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