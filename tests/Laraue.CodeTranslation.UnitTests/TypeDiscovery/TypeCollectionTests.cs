using Laraue.CodeTranslation.TypeDiscovery;
using Laraue.CodeTranslation.TypeDiscovery.Extensions;
using Xunit;

namespace Laraue.CodeTranslation.UnitTests.TypeDiscovery
{
    public class TypeCollectionTests
    {
        [Fact]
        public void TypeDiscoveryTest()
        {
            var typeCollection = new TypeCollection()
                .AddAssemblyTypes<MainClass>(
                    x => x.WithAttribute<ShouldBeTakenAttribute>());

            Assert.Equal(2, typeCollection.Count);
        }
    }
}