using Laraue.TypeScriptContractsGenerator.Generators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Laraue.TypeScriptContractsGenerator.CodeGenerator
{
    public class TypeScriptGeneratorBuilder
    {
        /// <summary>
        /// Ident size for strings.
        /// </summary>
        private int _indentSize = 2;

        /// <summary>
        /// Action to configure loading from assemblies types.
        /// </summary>
        private Func<IEnumerable<Type>, IEnumerable<Type>> _configureTypes;

        /// <summary>
        /// Action to configure loading assemblies.
        /// </summary>
        private Func<IEnumerable<string>, IEnumerable<string>> _configureAssemblies;

        /// <summary>
        /// Strategies to generate typescript code using <see cref="TsType"/>.
        /// </summary>
        private TsCodeGenerator _tsCodeGenerator = new DefaultTsCodeGenerator();

        /// <summary>
        /// Strategies to generate <see cref="TsType"/> from CSharp <see cref="Type"/>.
        /// </summary>
        private TsTypeGenerator _tsTypeGenerator = new DefaultTsTypeGenerator();

        /// <summary>
        /// Setter for ident size.
        /// </summary>
        /// <param name="indentSize"></param>
        /// <returns></returns>
        public TypeScriptGeneratorBuilder WithIndentSize(int indentSize)
        {
            _indentSize = indentSize;
            return this;
        }

        /// <summary>
        /// Opportunity to modify collection of source types uses for code generation.
        /// </summary>
        /// <param name="configureTypes"></param>
        /// <returns></returns>
        public TypeScriptGeneratorBuilder ConfigureTypes(Func<IEnumerable<Type>, IEnumerable<Type>> configureTypes)
        {
            _configureTypes = configureTypes;
            return this;
        }

        /// <summary>
        /// Configure loading assemblies.
        /// </summary>
        /// <param name="configureAssemblyNames"></param>
        /// <returns></returns>
        public TypeScriptGeneratorBuilder ConfigureAssemblies(Func<IEnumerable<string>, IEnumerable<string>> configureAssemblyNames)
        {
            _configureAssemblies = configureAssemblyNames;
            return this;
        }

        /// <summary>
        /// Setup generator for TS code.
        /// </summary>
        /// <param name="codeGenerator"></param>
        /// <returns></returns>
        public TypeScriptGeneratorBuilder UseCodeGenerator(TsCodeGenerator codeGenerator)
        {
            _tsCodeGenerator = codeGenerator;
            return this;
        }

        /// <summary>
        /// Setup generator type meta for TS code.
        /// </summary>
        /// <param name="typeGenerator"></param>
        /// <returns></returns>
        public TypeScriptGeneratorBuilder UseTypeGenerator(TsTypeGenerator typeGenerator)
        {
            _tsTypeGenerator = typeGenerator;
            return this;
        }

        /// <summary>
        /// Finish generator setup.
        /// </summary>
        /// <returns></returns>
        public TypeScriptGenerator Build()
        {
            var assemblyTypes = TypeScriptGeneratorHelper.LoadAssemblies(_configureAssemblies)
                .SelectMany(x => x.GetTypes());
            assemblyTypes = _configureTypes?.Invoke(assemblyTypes);

            return new TypeScriptGenerator(
                assemblyTypes,
                _indentSize,
                _tsCodeGenerator,
                _tsTypeGenerator);
        }
    }
}
