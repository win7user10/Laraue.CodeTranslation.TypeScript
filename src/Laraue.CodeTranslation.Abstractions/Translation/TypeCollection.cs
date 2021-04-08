using System;
using System.Collections.Generic;

namespace Laraue.CodeTranslation.Abstractions.Translation
{
    /// <summary>
    /// <see cref="HashSet{T}"/> for types. 
    /// </summary>
    public class TypeCollection : HashSet<Type>
    {

        /// <summary>
        /// Initializes new empty <see cref="TypeCollection"/>.
        /// </summary>
        public TypeCollection()
        {
        }

        /// <summary>
        /// Initializes <see cref="TypeCollection"/> using passed types.
        /// </summary>
        /// <param name="types"></param>
        public TypeCollection(IEnumerable<Type> types) : base(types)
        {
        }

        /// <summary>
        /// Add new <see cref="Type"/> to the collection.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TypeCollection AddType(Type type)
        {
            Add(type);
            return this;
        }
    }
}
