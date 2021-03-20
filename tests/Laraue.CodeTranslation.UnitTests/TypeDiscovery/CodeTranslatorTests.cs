using System;
using Laraue.CodeTranslation.TypeDiscovery;
using Xunit;

namespace Laraue.CodeTranslation.UnitTests.TypeDiscovery
{
    public class CodeTranslatorTests : IDisposable
    {
        private readonly CodeTranslator _translator = CodeTranslatorBuilder.Create();

        [Fact]
        public void BuiltCodeTranslatorShouldGenerateCodeWithoutException()
        {
            var code = _translator.GenerateTypeCode<MainClass>();
            Assert.NotEmpty(code);
        }

        public void Dispose()
        {
            _translator.Dispose();
        }
    }
}