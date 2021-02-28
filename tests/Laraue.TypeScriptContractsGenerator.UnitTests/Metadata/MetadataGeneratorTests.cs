using Laraue.CodeTranslation;
using Laraue.CodeTranslation.Abstractions.Metadata.Generators;
using Xunit;

namespace Laraue.TypeScriptContractsGenerator.UnitTests.Metadata
{
	public class MetadataGeneratorTests
	{
		private readonly IPropertyMetadataGenerator _generator = new PropertyMetadataGenerator();

		[Fact]
		public void GenerateMetadata()
		{
			var meta = _generator.GetMetadata(nameof(MainClass.IntValue).GetPropertyInfo<MainClass>());
		}
	}
}
