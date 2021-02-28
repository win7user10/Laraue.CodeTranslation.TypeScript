using System;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	/// <summary>
	/// Represents dynamic output type, which name can be different for a different metadata.
	/// </summary>
	public class DynamicOutputType : OutputType
	{
		public DynamicOutputType(Func<Metadata.Metadata, OutputTypeName> getNameFunc) : base(getNameFunc)
		{ }
	}
}