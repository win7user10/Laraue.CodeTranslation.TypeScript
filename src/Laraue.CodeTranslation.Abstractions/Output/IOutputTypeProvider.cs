using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public interface IOutputTypeProvider
	{
		/// <summary>
		/// Get <see cref="OutputType"/> for some type.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[CanBeNull]
		public OutputType Get(TypeMetadata key);

		public IEnumerable<OutputType> GetUsedTypes(TypeMetadata key);

		public IEnumerable<OutputPropertyType> GetProperties(TypeMetadata key);

		public OutputType GetOrAdd(TypeMetadata key, Func<OutputType> getValue);

		public IDependenciesGraph DependenciesGraph { get; }
	}
}