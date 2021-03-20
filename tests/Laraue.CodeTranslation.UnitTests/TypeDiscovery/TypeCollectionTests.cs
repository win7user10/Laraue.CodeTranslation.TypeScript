using Laraue.CodeTranslation.Extensions;
using Xunit;

namespace Laraue.CodeTranslation.UnitTests.TypeDiscovery
{
    public class TypeCollectionTests
    {
        [Fact]
        public void TypeDiscoveryTest()
        {
            var typeCollection = new TypeCollection()
                .AddTypesFromTypeAssembly<MainClass>(
                    x => x.WithAttribute<ShouldBeTakenAttribute>());

            Assert.Equal(2, typeCollection.Count);
        }
    }
}