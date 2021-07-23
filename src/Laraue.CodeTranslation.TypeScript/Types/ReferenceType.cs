using System.Collections.Generic;
using System.Linq;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;

namespace Laraue.CodeTranslation.TypeScript.Types
{
	public abstract class ReferenceType : DynamicOutputType
    {
        protected ReferenceType(TypeMetadata metadata, IOutputTypeProvider provider)
            : base(metadata, provider)
        {
        }

        /// <inheritdoc />
        public override OutputTypeName Name => GetOutputTypeName(TypeMetadata);

        /// <summary>
        /// Implemented interfaces output types.
        /// </summary>
        public IEnumerable<OutputType> Interfaces => TypeMetadata?.ImplementedInterfaces
            .Select(TypeProvider.Get)
            .Where(TypeProvider.ShouldBeImported) ?? Enumerable.Empty<OutputType>();

        protected OutputTypeName GetOutputTypeName(TypeMetadata metadata)
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