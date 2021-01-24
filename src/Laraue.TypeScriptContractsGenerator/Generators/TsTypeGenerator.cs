using Laraue.TypeScriptContractsGenerator.Typing;
using System;
using System.Collections.Generic;

namespace Laraue.TypeScriptContractsGenerator.Generators
{
    /// <summary>
    /// Class to collect meta from CLR <see cref="Type"/>
    /// </summary>
    public abstract class TsTypeGenerator
    {
        /// <summary>
        /// Should typescript implementation of this class be an array.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract bool IsArray(Type type);

        /// <summary>
        /// Generic type of array, if it is really array.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract Type GetArrayType(Type type);

        /// <summary>
        /// Does passed <see cref="Type"/> an enum.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract bool IsEnum(Type type);

        /// <summary>
        /// Does passed <see cref="Type"/> implements <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract bool IsDictionary(Type type);

        /// <summary>
        /// Does passed <see cref="Type"/> implements <see cref="Nullable{T}"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract bool IsAssignableFromNullable(Type type);

        /// <summary>
        /// If passed <see cref="Type"/> is nullable this type should returns not-nullable CLR <see cref="Type"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract Type NotNullableType(Type type);

        /// <summary>
        /// Return <see cref="TsTypes"/> based on passed <see cref="Type"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract TsTypes GetTypeScriptType(Type type);
    }
}