using Laraue.TypeScriptContractsGenerator.Extensions;
using Laraue.TypeScriptContractsGenerator.Typing;
using System;
using System.Collections.Generic;
using System.Net;

namespace Laraue.TypeScriptContractsGenerator.Generators
{
    public class DefaultTsTypeGenerator : TsTypeGenerator
    {
        /// <summary>
        /// Mapping for default CLR types.
        /// </summary>
        public Dictionary<Type, TsTypes> Mapping { get; } = new Dictionary<Type, TsTypes>
        {
            [typeof(int)] = TsTypes.Number,
            [typeof(long)] = TsTypes.Number,
            [typeof(decimal)] = TsTypes.Number,
            [typeof(double)] = TsTypes.Number,
            [typeof(float)] = TsTypes.Number,
            [typeof(Guid)] = TsTypes.String,
            [typeof(Uri)] = TsTypes.String,
            [typeof(string)] = TsTypes.String,
            [typeof(DateTimeOffset)] = TsTypes.Date,
            [typeof(DateTime)] = TsTypes.Date,
            [typeof(bool)] = TsTypes.Boolean,
        };

        public override bool IsArray(Type type)
            => !type.IsDictionary() && type != typeof(string) && type.IsGenericEnumerable();

        public override Type GetArrayType(Type type) => IsArray(type) ? type.GetGenericEnumerableType() : null;

        public override bool IsEnum(Type type)
            => type.IsEnum;

        public override bool IsDictionary(Type type) 
            => type.IsDictionary();

        public override bool IsAssignableFromNullable(Type type) 
            => NotNullableType(type) != null;

        public override Type NotNullableType(Type type)
            => Nullable.GetUnderlyingType(type);

        public override TsTypes GetTypeScriptType(Type type)
        {
            if (IsAssignableFromNullable(type))
                type = NotNullableType(type);

            if (Mapping.TryGetValue(type, out var tsType))
                return tsType;

            if (type.IsEnum)
                return TsTypes.Enum;

            if (IsDictionary(type))
                return TsTypes.Any;

            if (IsArray(type))
                return TsTypes.Array;

            return TsTypes.Class;
        }
    }
}