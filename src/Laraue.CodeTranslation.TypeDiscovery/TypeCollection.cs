using System;
using System.Collections.Generic;

namespace Laraue.CodeTranslation.TypeDiscovery
{
    public class TypeCollection : HashSet<Type>
    {
        public TypeCollection()
        {

        }

        public TypeCollection(IEnumerable<Type> types) : base(types)
        {
        }

        public TypeCollection AddType(Type type)
        {
            Add(type);
            return this;
        }
    }
}
