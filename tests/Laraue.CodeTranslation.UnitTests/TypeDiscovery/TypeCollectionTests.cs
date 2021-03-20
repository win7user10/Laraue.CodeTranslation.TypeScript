using JetBrains.Annotations;
using Laraue.CodeTranslation.TypeDiscovery;
using Xunit;

namespace Laraue.CodeTranslation.UnitTests.TypeDiscovery
{
    public class TypeCollectionTests
    {
        [Fact]
        public void Test()
        {
            var typeCollection = new TypeCollection()
                .AddAssemblyTypes<MainClass>(
                    x => x.WithAttribute<NotNullAttribute>());
        }
    }
}