using System;
using System.Collections.Generic;
using System.Linq;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Common.Extensions;

namespace Laraue.CodeTranslation.Common
{
    /// <summary>
    /// Provider, which can resolve <see cref="OutputType"/> fro passed <see cref="Type"/>.
    /// </summary>
    public class OutputTypeProvider : IOutputTypeProvider
    {
        private readonly Dictionary<TypeMetadata, OutputType> _cache = new();
        public IDependenciesGraph DependenciesGraph { get; }

        public OutputTypeProvider(IDependenciesGraph dependenciesGraph)
        {
            DependenciesGraph = dependenciesGraph;
        }

        /// <inheritdoc />
        public OutputType GetOrAdd(TypeMetadata key, Func<OutputType> getValue) => _cache.GetOrAdd(key, getValue);

        /// <inheritdoc />
        public OutputType Get(TypeMetadata key) => _cache.Get(key);

        /// <inheritdoc />
        public IEnumerable<OutputType> GetUsedTypes(TypeMetadata key)
        {
            var dependencies = DependenciesGraph.GetResolvingTypesSequence(key, DependencyType.Properties | DependencyType.This)
                .Where(x => x != key);

            var result = dependencies
                .Select(x => Get(key))
                .Where(x => x is not null);

            return result;
        }

        /// <inheritdoc />
        public IEnumerable<OutputPropertyType> GetProperties(TypeMetadata key)
        {
            var dependencies = DependenciesGraph.GetResolvingTypesSequence(key, DependencyType.Properties);
            var propertiesMetadataTypes = key.PropertiesMetadata.ToArray();

            // Todo - make sorting before iteration if will be problems with performance.
            foreach (var dependency in dependencies)
            {
                var propertiesWithThisType = propertiesMetadataTypes
                    .Where(x => x.PropertyType == dependency)
                    .ToArray();

                foreach (var propertyMetadata in propertiesWithThisType)
                {
                    var outputPropertyType = new OutputPropertyType
                    {
                        OutputType = Get(propertyMetadata.PropertyType),
                        PropertyName = propertyMetadata.PropertyName,
                        PropertyMetadata = propertyMetadata,
                    };

                    yield return outputPropertyType;
                }
            }
        }
    }
}