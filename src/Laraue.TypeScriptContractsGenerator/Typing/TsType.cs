using Laraue.TypeScriptContractsGenerator.Generators;
using System;

namespace Laraue.TypeScriptContractsGenerator.Typing
{
    public class TsType : TsReflectionClass
    {
        internal TsType(Type clrType, TsTypeGenerator tsTypeGenerator, TsCodeGenerator tsCodeGenerator)
            : base (clrType, tsTypeGenerator, tsCodeGenerator)
        {
        }

        public override string TsName => CodeGenerator.GetTypeName(this);

        public override string TypeScriptCode => "";
    }
}