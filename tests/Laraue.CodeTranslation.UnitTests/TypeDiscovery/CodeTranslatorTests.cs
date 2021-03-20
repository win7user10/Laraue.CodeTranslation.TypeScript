using System;
using Xunit;

namespace Laraue.CodeTranslation.UnitTests.TypeDiscovery
{
    public class CodeTranslatorTests : IDisposable
    {
        private readonly CodeTranslator _translator = CodeTranslatorBuilder.Create();

        [Fact]
        public void BuiltCodeTranslatorShouldGenerateCodeWithoutException()
        {
            var generatedCode = _translator.GenerateTypeCode(typeof(MainClass));
            Assert.NotEmpty(generatedCode.Code);
            Assert.NotEmpty(generatedCode.FilePathSegments);
        }

        public void Dispose()
        {
            _translator.Dispose();
        }
    }
}