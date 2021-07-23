using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public class Class : ReferenceType
	{
		public Class(TypeMetadata metadata, IOutputTypeProvider provider) 
			: base(metadata, provider)
		{
		}

		/// <inheritdoc />
		public override OutputTypeName ParentTypeName => TypeProvider.ShouldBeImported(TypeProvider.Get(TypeMetadata?.ParentTypeMetadata)) 
			? GetOutputTypeName(TypeMetadata?.ParentTypeMetadata)
			: null;

		/// <summary>
		/// Return if this class is abstract.
		/// </summary>
		public bool IsAbstract => TypeMetadata?.IsAbstract ?? false;
	}
}