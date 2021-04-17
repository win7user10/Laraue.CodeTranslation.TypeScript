using Laraue.CodeTranslation.Extensions.NamingStrategies;
using Xunit;

namespace Laraue.CodeTranslation.UnitTests.Extensions.NamingStrategies
{
    public class CamelCaseNamingStrategyTests
    {
        [Theory]
        [InlineData("PropertyName", "propertyName")]
        [InlineData("X", "x")]
        public void Resolve(string source, string excepted)
        {
            var strategy = new CamelCaseNamingStrategy();
            var result = strategy.Resolve(source);
            Assert.Equal(excepted, result);
        }
    }
}