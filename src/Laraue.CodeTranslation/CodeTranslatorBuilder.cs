using System;
using Microsoft.Extensions.DependencyInjection;

namespace Laraue.CodeTranslation
{
    public class CodeTranslatorBuilder
    {
        private readonly ServiceCollection _services = new ();

        public CodeTranslatorBuilder AddDependency<TService, TImplementation>()
            where TService : class 
            where TImplementation : class, TService
        {
            _services.AddSingleton<TService, TImplementation>();
            return this;
        }

        public CodeTranslatorBuilder AddDependency<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation)
            where TService : class
            where TImplementation : class, TService
        {
            _services.AddSingleton<TService, TImplementation>(implementation);
            return this;
        }

        public ICodeTranslator Build()
        {
            var provider = _services.BuildServiceProvider();
            return provider.GetRequiredService<ICodeTranslator>();
        }
    }
}