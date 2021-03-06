using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Laraue.CodeTranslation;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.TypeScriptContractsGenerator.Architecture.Types;
using Array = Laraue.TypeScriptContractsGenerator.Architecture.Types.Array;
using String = Laraue.TypeScriptContractsGenerator.Architecture.Types.String;

namespace Laraue.TypeScriptContractsGenerator.Architecture
{
	public class TypeScriptOutputTypeMetadataGenerator : OutputTypeMetadataGenerator
	{
		public TypeScriptOutputTypeMetadataGenerator(Action<MapCollection> setupMap = null) 
			: base(new MapCollection())
		{
			Collection
				.AddMap<int, Number>()
				.AddMap<decimal, Number>()
				.AddMap<double, Number>()
				.AddMap<long, Number>()
				.AddMap<short, Number>()
				.AddMap<float, Number>()
				.AddMap<string, String>()
				.AddMap<Guid, String>()
				.AddMap<Array>(metadata => metadata.IsEnumerable && !metadata.IsDictionary, GetArrayMetadata)
				.AddMap<Class>(metadata => metadata.ClrType.IsClass, GetClassMetadata);

			setupMap?.Invoke(Collection);
		}

		public virtual Class GetClassMetadata(TypeMetadata metadata)
		{
			var typeName = GetTypeName(metadata);
			return new (typeName, new HashSet<TypeMetadata>(GetUsedTypes(metadata)));
		}

		protected virtual Array GetArrayMetadata(TypeMetadata metadata)
		{
			var typeName = GetTypeName(metadata);
			return new(typeName, new HashSet<TypeMetadata>(GetUsedTypes(metadata)));
		}

		protected virtual IEnumerable<TypeMetadata> GetUsedTypes(TypeMetadata metadata)
		{
			var parentType = GetUsedParentType(metadata);

			if (parentType is not null)
			{
				yield return parentType;
			}

			foreach (var type in GetUsedGenericTypes(metadata))
			{
				yield return type;
			}
		}

		[CanBeNull]
		protected virtual TypeMetadata GetUsedParentType(TypeMetadata metadata)
		{
			throw new NotImplementedException();
		}

		[NotNull]
		protected virtual IEnumerable<TypeMetadata> GetUsedGenericTypes(TypeMetadata metadata)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public override OutputType GetOutputType(TypeMetadata metadata)
		{
			var descriptor = Collection.GetMap(metadata);
			return descriptor.GetOutputType(metadata);
		}

		protected virtual TypeMetadata[] GetGenericTypeArguments(TypeMetadata metadata)
		{
			return metadata.GenericTypeArguments.ToArray();
		}

		protected virtual string GetNonGenericStringTypeName(Metadata metadata)
		{
			var typeName = metadata.ClrType.Name;
			return Regex.Replace(typeName, @"`\d+", string.Empty);
		}

		public OutputTypeName GetTypeName(TypeMetadata metadata)
		{
			var typeName = GetNonGenericStringTypeName(metadata);
			var genericArgs = GetGenericTypeArguments(metadata);
			var genericOutputTypes = genericArgs.Select(GetOutputType).ToArray();
			return new OutputTypeName(typeName, genericOutputTypes.Select(x => x.Name));
		}
	}
}