using System;
using System.Collections.Generic;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Dictionary : DynamicOutputType
	{
		public Dictionary(IEnumerable<OutputTypeName> genericArgNames, IEnumerable<OutputType> usedTypes)
		{
			UsedTypes = usedTypes;
			Name = GetName(genericArgNames);
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
