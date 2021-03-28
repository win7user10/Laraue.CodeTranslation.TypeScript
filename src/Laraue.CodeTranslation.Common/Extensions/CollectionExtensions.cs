using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Laraue.CodeTranslation.Common.Extensions
{
    /// <summary>
    /// Extensions for collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Add some elements to the hashset.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashSet"></param>
        /// <param name="values"></param>
		public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                hashSet.Add(value);
            }
        }

        /// <summary>
        /// Enqueue some elements to queue.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue"></param>
        /// <param name="values"></param>
        public static void EnqueueRange<T>(this Queue<T> queue, [CanBeNull]IEnumerable<T> values)
        {
            if (values is null) return;
            foreach (var value in values)
            {
                if (value != null)
                {
                    queue.Enqueue(value);
                }
            }
        }

        /// <summary>
        /// Get value by key, or add it using passed function and return it.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="getValue"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> getValue)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                return value;
            }

            value = getValue();
            dictionary.Add(key, value);

            return value;
        }

        /// <summary>
        /// Get value of dictionary if it exists or return default value.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value) ? value : default;
        }
    }
}