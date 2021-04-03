using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Common;
using Laraue.CodeTranslation.TypeScript.Types;

namespace Laraue.CodeTranslation.TypeScript
{
	public class TypeScriptOutputTypeProvider : OutputTypeProvider
	{
		/// <inheritdoc />
		public TypeScriptOutputTypeProvider(IDependenciesGraph dependenciesGraph) : base(dependenciesGraph)
		{
		}

		/// <inheritdoc />
		protected override bool ShouldBeImported(OutputType type)
		{
			if (type?.TypeMetadata is null) return false;

			return type switch
			{
				Enum => true,
				StaticOutputType => false,
				Array => false,
				_ => !type.TypeMetadata.ClrType.Assembly.FullName.Contains("System")
			};
		}
	}
}