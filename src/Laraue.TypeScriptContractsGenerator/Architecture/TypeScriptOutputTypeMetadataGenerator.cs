using System;
using Laraue.CodeTranslation;
using Laraue.TypeScriptContractsGenerator.Architecture.Types;
using String = Laraue.TypeScriptContractsGenerator.Architecture.Types.String;

namespace Laraue.TypeScriptContractsGenerator.Architecture
{
	public class TypeScriptOutputTypeMetadataGenerator : OutputTypeMetadataGenerator
	{
		public TypeScriptOutputTypeMetadataGenerator()
		{
			AddMap<int, Number>();
			AddMap<decimal, Number>();
			AddMap<double, Number>();
			AddMap<long, Number>();
			AddMap<short, Number>();
			AddMap<float, Number>();
			AddMap<string, String>();
			AddMap<Guid, String>();
		}
	}
}