using System;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	[Flags]
	public enum DependencyType : byte
	{
		This = 1,
		Parent = 2,
		Properties = 4,
		Generic = 8,
	}
}