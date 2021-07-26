using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Class : DynamicOutputType
	{
		public Class(TypeMetadata metadata, IOutputTypeProvider provider) 
			: base(metadata, provider)
		{
		}

		/// <inheritdoc />
		public override OutputTypeName Name => GetOutputTypeName(TypeMetadata);

		/// <inheritdoc />
		public override OutputTypeName ParentTypeName => TypeProvider.ShouldBeImported(TypeProvider.Get(TypeMetadata?.ParentTypeMetadata)) 
			? GetOutputTypeName(TypeMetadata?.ParentTypeMetadata)
			: null;

		private OutputTypeName GetOutputTypeName(TypeMetadata metadata)
		{
			if (metadata is null)
			{
				return string.Empty;
			}

			var className = GetNonGenericStringTypeName(metadata);
			var genericTypeNames = GetGenericTypeNames(metadata);
			return new OutputTypeName(className, genericTypeNames);
		}
	}
}