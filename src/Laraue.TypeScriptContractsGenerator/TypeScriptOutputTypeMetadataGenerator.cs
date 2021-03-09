using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Laraue.CodeTranslation;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.TypeScriptContractsGenerator.Types;
using Array = Laraue.TypeScriptContractsGenerator.Types.Array;
using String = Laraue.TypeScriptContractsGenerator.Types.String;

namespace Laraue.TypeScriptContractsGenerator
{
	public class TypeScriptOutputTypeMetadataGenerator : OutputTypeMetadataGenerator
	{
		public TypeScriptOutputTypeMetadataGenerator(Action<MapCollection> setupMap = null) 
			: base(new MapCollection())
		{
			Collection
				.AddMap<Array>(metadata => metadata.IsEnumerable && !metadata.IsDictionary && metadata.ClrType != typeof(string), GetArrayMetadata)
				.AddMap<int, Number>()
				.AddMap<decimal, Number>()
				.AddMap<double, Number>()
				.AddMap<long, Number>()
				.AddMap<short, Number>()
				.AddMap<float, Number>()
				.AddMap<string, String>()
				.AddMap<Guid, String>()
				.AddMap<Class>(metadata => metadata.ClrType.IsClass, GetClassMetadata);

			setupMap?.Invoke(Collection);
		}

		public virtual Class GetClassMetadata(TypeMetadata metadata)
		{
			var typeName = GetTypeName(metadata);
			var propertiesMetadata = metadata.PropertiesMetadata.Select(GetOutputPropertyType);
			return new (typeName, GetUsedTypes(metadata), propertiesMetadata);
		}

		protected virtual OutputPropertyType GetOutputPropertyType(PropertyMetadata metadata)
		{
			return new ()
			{
				Source = metadata.Source,
				OutputType = GetOutputType(metadata.PropertyType),
				PropertyName = metadata.PropertyName,
			};
		}

		protected virtual Array GetArrayMetadata(TypeMetadata metadata)
		{
			var genericArgs = metadata.GenericTypeArguments?.ToArray();
			if (genericArgs is null || genericArgs.Length != 1)
			{
				throw new ArgumentOutOfRangeException(nameof(genericArgs));
			}

			var enumerableType = genericArgs[0];

			var type = GetOutputType(enumerableType);
			return new(type.Name, GetUsedTypes(enumerableType));
		}

		protected virtual IEnumerable<OutputType> GetUsedTypes(TypeMetadata metadata)
		{
			var allUsedTypes = new List<OutputType>(16);
			var parentType = GetUsedParentType(metadata);

			if (parentType is not null)
			{
				allUsedTypes.AddRange(GetUsedGenericTypes(parentType));
			}

			allUsedTypes.AddRange(GetUsedGenericTypes(metadata.GenericTypeArguments?.ToArray()));

			var result = new HashSet<OutputType>(allUsedTypes, new UsedOutputTypesEqualityComparer());
			return result.ToArray();
		}

		[CanBeNull]
		protected virtual TypeMetadata GetUsedParentType(TypeMetadata metadata)
		{
			var parentMetadata = metadata.ParentTypeMetadata;
			if (parentMetadata is null) return null;
			return parentMetadata.ClrType.Assembly.FullName.Contains("System") ? null : parentMetadata;
		}

		[NotNull]
		protected virtual IEnumerable<OutputType> GetUsedGenericTypes([CanBeNull] params TypeMetadata[] metadata)
		{
			return metadata?.Select(GetOutputType).Where(x => x is not ValueOutputType) ?? System.Array.Empty<OutputType>();
		}

		/// <inheritdoc />
		public override OutputType GetOutputType(TypeMetadata metadata)
		{
			var descriptor = Collection.GetMap(metadata);
			return descriptor.GetOutputType(metadata);
		}

		[NotNull]
		protected virtual TypeMetadata[] GetGenericTypeArguments(TypeMetadata metadata)
		{
			return metadata.GenericTypeArguments?.ToArray() ?? System.Array.Empty<TypeMetadata>();
		}

		protected virtual string GetNonGenericStringTypeName(TypeMetadata metadata)
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