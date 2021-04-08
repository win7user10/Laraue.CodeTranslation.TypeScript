using System.Collections.Generic;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	/// <summary>
	/// Base class for types in some programming language.
	/// </summary>
	public abstract class OutputType
	{
		/// <summary>
		/// Name of the class.
		/// </summary>
		[NotNull] public virtual OutputTypeName Name { get; protected set; }

		/// <summary>
		/// Dependencies of the type.
		/// </summary>
		[NotNull] public abstract IEnumerable<OutputType> UsedTypes { get; }

		/// <summary>
		/// Full information about properties in this type.
		/// </summary>
		[NotNull] public abstract IEnumerable<OutputPropertyType> Properties { get; }

		/// <summary>
		/// Source metadata. All properties of this class has been generated depends on it.
		/// </summary>
		[CanBeNull] public TypeMetadata TypeMetadata { get; protected set; }

		/// <summary>
		/// Name of parent class if it is exists.
		/// </summary>
		[CanBeNull] public virtual OutputTypeName ParentTypeName { get; protected set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return $"Clr = {TypeMetadata?.ClrType} Output = {GetType().Name} Name = {Name}";
		}

		/// <summary>
		/// Returns clear type name without generic parameters.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		[NotNull]
		protected string GetNonGenericStringTypeName([NotNull] TypeMetadata metadata)
		{
			var typeName = metadata.ClrType.Name;
			return Regex.Replace(typeName, @"`\d+", string.Empty);
		}
	}
}