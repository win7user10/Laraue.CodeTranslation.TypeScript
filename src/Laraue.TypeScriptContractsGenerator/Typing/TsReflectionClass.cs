using Laraue.TypeScriptContractsGenerator.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Laraue.TypeScriptContractsGenerator.Typing
{
    public abstract class TsReflectionClass
    {
        public Type ClrType { get; }

        protected readonly TsTypeGenerator TypeGenerator;

        protected readonly TsCodeGenerator CodeGenerator;

        internal TsReflectionClass(Type clrType, TsTypeGenerator tsTypeGenerator, TsCodeGenerator tsCodeGenerator)
        {
            ClrType = clrType;
            TypeGenerator = tsTypeGenerator;
            CodeGenerator = tsCodeGenerator;
        }

        public TsProperty[] Properties => ClrType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Select(propertyInfo => new TsProperty(propertyInfo, TypeGenerator, CodeGenerator))
            .ToArray();

        public TsType[] GenericTypes
        {
            get
            {
                if (IsArray)
                    return new[] { new TsType(TypeGenerator.GetArrayType(ClrType), TypeGenerator, CodeGenerator) };

                return ClrType
                    .GenericTypeArguments
                    .Select(type => new TsType(type, TypeGenerator, CodeGenerator))
                    .ToArray();
            }
        }

        public bool IsGeneric => GenericTypes.Length > 0;

        public TsType ParentType => ClrType.BaseType != null && CodeGenerator.ShouldBeInherited(ClrType.BaseType)
            ? new TsType(ClrType.BaseType, TypeGenerator, CodeGenerator)
            : null;

        public TsReflectionClass NotNullableType => IsAssignableFromNullable
            ? new TsType(TypeGenerator.NotNullableType(ClrType), TypeGenerator, CodeGenerator)
            : this;

        public string TsParentName => ParentType != null ? CodeGenerator.GetPropertyType(ParentType) : null;

        public IEnumerable<TsReflectionClass> ImportingTypes
        {
            get
            {
                var result = new HashSet<TsReflectionClass>();
                if (ParentType != null && ParentType.ShouldBeImported)
                    result.Add(ParentType.NotNullableType);

                var properties = new HashSet<TsReflectionClass>(Properties.Cast<TsReflectionClass>());
                while (true)
                {
                    var propertiesToRecursiveSelect = new HashSet<TsReflectionClass>();
                    foreach (var property in properties)
                    {
                        var @class = property;
                        if (@class.IsArray && @class.GenericTypes.Length > 0)
                            @class = @class.GenericTypes[0].NotNullableType;

                        if (@class.ShouldBeImported && !result.Contains(@class))
                        {
                            result.Add(@class.NotNullableType);
                            propertiesToRecursiveSelect.Add(@class.NotNullableType);
                        }
                    }

                    if (propertiesToRecursiveSelect.Count == 0)
                        break;

                    properties = propertiesToRecursiveSelect;
                }

                result.Remove(this);
                return result.ToArray();
            }
        }

        public string TsGenericDefinition
        {
            get
            {
                if (!IsGeneric)
                    return null;

                var childGenerics = GenericTypes.Select(x => x.TsGenericDefinition)
                    .Where(x => x != null)
                    .ToArray();

                var childGenericsString = string.Empty;
                if (childGenerics.Length > 0)
                    childGenericsString = string.Join(", ", childGenerics);

                var generics = !IsAssignableFromNullable
                    ? GenericTypes.Select(x => x.TsType)
                    : Enumerable.Empty<string>();

                if (IsAssignableFromNullable && childGenerics.Length < 1)
                    return null;

                var genericsString = string.Join(", ", generics);
                return $"<{genericsString}{childGenericsString}>";
            }
        }

        public TsTypes TypeScriptType => TypeGenerator.GetTypeScriptType(ClrType);

        public bool IsAssignableFromNullable => TypeGenerator.IsAssignableFromNullable(ClrType);

        public bool IsArray => TypeGenerator.IsArray(ClrType);

        public abstract string TsName { get; }

        public bool ShouldBeImported => CodeGenerator.ShouldBeImported(this);

        public bool IsEnum => TypeGenerator.IsEnum(ClrType);

        public string TsType => CodeGenerator.GetPropertyType(this);

        public Dictionary<string, int> EnumValues => IsEnum ? CodeGenerator.GetEnumValues(this) : null;

        public override string ToString() => $"Name = {TsName} TsType = {TsType}{TsGenericDefinition}";

        public override bool Equals(object obj) => obj is TsReflectionClass @class && @class.ClrType == ClrType;

        public override int GetHashCode() => ClrType.GetHashCode();

        public string[] TsImports => ImportingTypes.Select(x => CodeGenerator.GetTsImportCode(NotNullableType, x)).ToArray();

        public abstract string TypeScriptCode { get; }
    }
}
