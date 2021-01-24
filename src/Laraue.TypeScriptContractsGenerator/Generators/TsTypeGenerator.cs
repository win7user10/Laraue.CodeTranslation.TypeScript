using Laraue.TypeScriptContractsGenerator.Typing;
using System;

namespace Laraue.TypeScriptContractsGenerator.Generators
{
    public abstract class TsTypeGenerator
    {
        public abstract bool IsArray(Type type);

        public abstract Type GetArrayType(Type type);

        public abstract bool IsEnum(Type type);

        public abstract bool IsDictionary(Type type);

        public abstract bool IsAssignableFromNullable(Type type);

        public abstract Type NotNullableType(Type type);

        public abstract TsTypes GetTypeScriptType(Type type);
    }
}