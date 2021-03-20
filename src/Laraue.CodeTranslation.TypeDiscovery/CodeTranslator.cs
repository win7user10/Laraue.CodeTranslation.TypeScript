using System;
using Microsoft.Extensions.DependencyInjection;

namespace Laraue.CodeTranslation.TypeDiscovery
{
    public class CodeTranslator : IDisposable
    {
        private readonly ServiceProvider _provider;

        internal CodeTranslator(ServiceProvider provider)
        {
            _provider = provider;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _provider.Dispose();
        }
    }
}