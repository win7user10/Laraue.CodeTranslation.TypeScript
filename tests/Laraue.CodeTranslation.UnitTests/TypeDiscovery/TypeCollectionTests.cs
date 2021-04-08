using Laraue.CodeTranslation.Abstractions.Translation;
using Laraue.CodeTranslation.Extensions;
using Xunit;

namespace Laraue.CodeTranslation.UnitTests.TypeDiscovery
{
    public class TypeCollectionTests
    {
        [Fact]
        public void DirectDiscovery()
        {
            var typeCollection = new TypeCollection()
                .AddTypesFromTypeAssembly<MainClass>(
                    x => x.HasAttribute<ShouldBeTakenAttribute>());

            Assert.Equal(2, typeCollection.Count);
        }

        [Fact]
        public void AutoDiscovery()
        {
            var typeCollection = new TypeCollection()
                .AddTypesFromAllReferencedAssemblies(filterLoadingType: x => x.HasAttribute<ShouldBeTakenAttribute>());

            Assert.Equal(2, typeCollection.Count);
        }
    }
}