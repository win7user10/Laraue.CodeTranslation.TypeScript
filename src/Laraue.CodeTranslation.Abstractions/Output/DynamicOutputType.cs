using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;

namespace Laraue.CodeTranslation.Abstractions.Output
{
	public abstract class DynamicOutputType : OutputType
	{
		protected readonly IOutputTypeProvider TypeProvider;

		protected DynamicOutputType(TypeMetadata metadata, IOutputTypeProvider provider)
		{
			TypeProvider = provider;
			TypeMetadata = metadata;
		}

		/// <inheritdoc />
		public override IEnumerable<OutputPropertyType> Properties => TypeProvider.GetProperties(TypeMetadata);

		/// <inheritdoc />
		public override IEnumerable<OutputType> UsedTypes => TypeProvider.GetUsedTypes(TypeMetadata);

		/// <summary>
		/// Return generic type arguments generated names for passed type.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		[NotNull]
		protected virtual OutputTypeName[] GetGenericTypeNames(TypeMetadata metadata)
		{
			var names = metadata
				?.GenericTypeArguments
				?.Select(x => TypeProvider.Get(x)?.Name)
				.Where(x => x != null)
				.ToArray();

			return names ?? System.Array.Empty<OutputTypeName>();
		}
	}
}