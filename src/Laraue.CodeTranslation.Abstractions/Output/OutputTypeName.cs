using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public record OutputTypeName
	{
		public string Name { get; init; }

		public OutputTypeName[] GenericNames { get; init; }

		public OutputTypeName(string name, IEnumerable<string> genericNames = null)
		{
			Name = name;
			GenericNames = genericNames?.Select(x => new OutputTypeName(x)).ToArray() ?? Array.Empty<OutputTypeName>();
		}

		public OutputTypeName(string name, [NotNull] IEnumerable<OutputTypeName> genericNames)
		{
			Name = name;
			GenericNames = genericNames.ToArray();
		}

		public static implicit operator string(OutputTypeName typeName)
			=> typeName.ToString();

		public static implicit operator OutputTypeName(string typeName)
			=> new (typeName);

		/// <inheritdoc />
		public override string ToString()
		{
			if (GenericNames.Length == 0) return Name;
			var genericNames = string.Join(", ", GenericNames.Select(x => x.ToString()));
			return Name + $"<{genericNames}>";
		}
	}
}