using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	/// <summary>
	/// Provider which can resolve some dependencies.
	/// </summary>
	public interface IOutputTypeProvider
	{
		/// <summary>
		/// <see cref="IDependenciesGraph"/> dependency.
		/// </summary>
		[NotNull] IDependenciesGraph DependenciesGraph { get; }

		/// <summary>
		/// Get <see cref="OutputType"/> for some type.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[CanBeNull] OutputType Get(TypeMetadata key);

		/// <summary>
		/// Returns all using types for specified <see cref="TypeMetadata"/>.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[NotNull] IEnumerable<OutputType> GetUsedTypes(TypeMetadata key);

		/// <summary>
		/// Get all <see cref="OutputPropertyType"/> for passed <see cref="TypeMetadata"/>.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[NotNull] IEnumerable<OutputPropertyType> GetProperties(TypeMetadata key);

		/// <summary>
		/// Get <see cref="OutputType"/> by specified key or add it by specified function and the return it.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="getValue"></param>
		/// <returns></returns>
		[CanBeNull] OutputType GetOrAdd(TypeMetadata key, Func<OutputType> getValue);

		/// <summary>
		/// Returns, should some <see cref="OutputType"/> be imported and used for inheritance.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		bool ShouldBeImported([CanBeNull] OutputType type);
	}
}