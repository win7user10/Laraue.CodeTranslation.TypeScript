using System;
using System.Collections.Generic;

namespace Laraue.CodeTranslation
{
    public interface ICodeTranslator
    {
        /// <summary>
        /// Generates code for passed type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public GeneratedCode GenerateTypeCode(Type type);

        /// <summary>
        /// Generates code for passed types.
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public IEnumerable<GeneratedCode> GenerateTypesCode(IEnumerable<Type> types);
    }
}