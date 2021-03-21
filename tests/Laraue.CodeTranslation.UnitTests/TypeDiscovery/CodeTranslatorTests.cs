using System;
using Laraue.CodeTranslation.TypeScript;
using Xunit;

namespace Laraue.CodeTranslation.UnitTests.TypeDiscovery
{
    public class CodeTranslatorTests : IDisposable
    {
        private readonly CodeTranslator _translator = TypeScriptTranslatorBuilder.Create(new CodeTranslatorOptions());

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