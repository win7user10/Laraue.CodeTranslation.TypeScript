using System;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	/// <summary>
	/// Tells, where how some dependency related to a some type.
	/// </summary>
	[Flags]
	public enum DependencyType : byte
	{
		/// <summary>
		/// This means that dependency was discovered in a some type. It can be it generic types or parent of parent type of a some type.
		/// </summary>
		Parent = 1,

		/// <summary>
		/// This means that dependency was discovered in properties of a some type. It can be it generic types or parent of parent type of a some type.
		/// </summary>
		Properties = 2,

		/// <summary>
		/// This means that dependency was discovered in generic types of some type.
		/// </summary>
		Generic = 4,
	}
}