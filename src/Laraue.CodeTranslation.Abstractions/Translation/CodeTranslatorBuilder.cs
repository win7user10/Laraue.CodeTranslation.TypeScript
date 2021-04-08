using System;
using Microsoft.Extensions.DependencyInjection;

namespace Laraue.CodeTranslation.Abstractions.Translation
{
    /// <summary>
    /// Class for building <see cref="ICodeTranslator"/>.
    /// </summary>
    public class CodeTranslatorBuilder
    {
        private readonly ServiceCollection _services = new ();

        /// <summary>
        /// Add some dependency to the container.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        public CodeTranslatorBuilder AddDependency<TService, TImplementation>()
            where TService : class 
            where TImplementation : class, TService
        {
            _services.AddSingleton<TService, TImplementation>();
            return this;
        }

        /// <summary>
        /// Add some dependency to the container.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="implementation"></param>
        /// <returns></returns>
        public CodeTranslatorBuilder AddDependency<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation)
            where TService : class
            where TImplementation : class, TService
        {
            _services.AddSingleton<TService, TImplementation>(implementation);
            return this;
        }

        /// <summary>
        /// Add some dependency to the container.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="service"></param>
        /// <returns></returns>
        public CodeTranslatorBuilder AddDependency<TService>(TService service)
            where TService : class
        {
            _services.AddSingleton(service);
            return this;
        }

        /// <summary>
        /// Build container and return new <see cref="ICodeTranslator"/>.
        /// </summary>
        /// <returns></returns>
        public ICodeTranslator Build()
        {
            var provider = _services.BuildServiceProvider();
            return provider.GetRequiredService<ICodeTranslator>();
        }
    }
}