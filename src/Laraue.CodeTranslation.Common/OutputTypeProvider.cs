using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Common.Extensions;

namespace Laraue.CodeTranslation.Common
{
    /// <summary>
    /// Provider, which can resolve <see cref="OutputType"/> fro passed <see cref="Type"/>.
    /// </summary>
    public class OutputTypeProvider
    {
        private readonly Dictionary<Type, OutputType> _cache = new();

        internal OutputType GetOrAdd(Type key, Func<OutputType> getValue) => _cache.GetOrAdd(key, getValue);

        /// <summary>
        /// Get <see cref="OutputType"/> for some type.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [CanBeNull]
        public OutputType Get(Type key) => _cache.Get(key);
    }
}