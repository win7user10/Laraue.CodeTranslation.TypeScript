using System;
using System.Collections.Generic;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.TypeScriptContractsGenerator.Types
{
	public class Dictionary : DynamicOutputType
	{
		/// <inheritdoc />
		public override OutputTypeName Name => GetName(_genericArgNames);

		/// <inheritdoc />
		public override IEnumerable<OutputType> UsedTypes { get; }

		/// <summary>
		/// Lazy <see cref="OutputTypeName"/> collection to avoid stack overflow exception in some cases.
		/// </summary>
		private readonly IEnumerable<OutputTypeName> _genericArgNames;

		public Dictionary(IEnumerable<OutputTypeName> genericArgNames, IEnumerable<OutputType> usedTypes)
		{
			_genericArgNames = genericArgNames;
			UsedTypes = usedTypes;
		}

		public OutputTypeName GetName(IEnumerable<OutputTypeName> typeNames)
		{
			try
			{
				return new("Dictionary", typeNames);
			}
			catch (Exception)
			{
				return new Any().Name;
			}
		}
	}
}
