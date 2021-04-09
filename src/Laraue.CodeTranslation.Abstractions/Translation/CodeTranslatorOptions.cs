using System;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.Abstractions.Translation
{
    /// <summary>
    /// Options for <see cref="ICodeTranslator"/>.
    /// </summary>
    public class CodeTranslatorOptions
    {
        /// <summary>
        /// Strings indent size in a result code.
        /// </summary>
        public int IndentSize { get; set; } = 2;

        /// <summary>
        /// Modifying default type mapping.
        /// </summary>
        [CanBeNull] public Action<MapCollection> ConfigureTypeMap { get; set; }

        /// <summary>
        /// Strategy for resolving type names in the result code.
        /// </summary>
        public INamingStrategy TypeNamingStrategy { get; set; }

        /// <summary>
        /// Strategy for resolving path segments of result types.
        /// </summary>
        public INamingStrategy PathSegmentNamingStrategy { get; set; }

        /// <summary>
        /// Strategy for resolving property names of result types.
        /// </summary>
        public INamingStrategy PropertiesNamingStrategy { get; set; }
    }
}