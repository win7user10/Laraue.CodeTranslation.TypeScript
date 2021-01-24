using Laraue.TypeScriptContractsGenerator.Generators;
using System.Reflection;

namespace Laraue.TypeScriptContractsGenerator.Typing
{
    public class TsProperty : TsReflectionClass
    {
        public PropertyInfo PropertyInfo { get; }

        internal TsProperty(PropertyInfo propertyInfo, TsTypeGenerator tsTypeGenerator, TsCodeGenerator tsCodeGenerator)
            : base (propertyInfo.PropertyType, tsTypeGenerator, tsCodeGenerator)
        {
            PropertyInfo = propertyInfo;
        }

        public override string TsName => CodeGenerator.GetPropertyName(this);

        public bool IsNullable => CodeGenerator.IsNullable(this);

        public string DefaultValue => CodeGenerator.GetDefaultPropertyValue(this);

        public override string TypeScriptCode => CodeGenerator.GetTsPropertyCode(this);

        public override string ToString() => $"Name = {TsType} TsType = {TsType}{TsGenericDefinition ?? string.Empty }";
    }
}
