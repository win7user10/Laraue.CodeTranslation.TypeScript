using System.Collections.Generic;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
    /// <summary>
    /// Class represents dependencies between different used types. It helps to generate class metadata sequentially
    /// from types without dependencies to types with resolved dependencies.
    /// </summary>
    public interface IDependenciesGraph
    {
        /// <summary>
        /// Returns list of <see cref="TypeMetadata"/> in right order to resolve dependencies.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IReadOnlyList<TypeMetadata> GetResolvingTypesSequence(TypeMetadata metadata, DependencyType type);

        /// <summary>
        /// Build dependencies graph for passed type.
        /// </summary>
        /// <param name="metadata"></param>
        public void AddToGraph(TypeMetadata metadata);
    }
}