using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Laraue.CodeTranslation;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.TypeScriptContractsGenerator.Types;
using Newtonsoft.Json.Linq;
using Array = Laraue.TypeScriptContractsGenerator.Types.Array;
using Boolean = Laraue.TypeScriptContractsGenerator.Types.Boolean;
using Enum = Laraue.TypeScriptContractsGenerator.Types.Enum;
using String = Laraue.TypeScriptContractsGenerator.Types.String;

namespace Laraue.TypeScriptContractsGenerator
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
				.AddMap<bool, Boolean>()
				.AddMap<Guid, String>()
				.AddMap<JObject, Any>()
				.AddMap<JToken, Any>()
				.AddMap<Enum>(metadata => metadata.IsEnum, GetEnumMetadata)
				.AddMap<Array>(metadata => metadata.IsEnumerable && !metadata.IsDictionary && metadata.ClrType != typeof(string), GetArrayMetadata)
				.AddMap<Class>(metadata => metadata.ClrType.IsClass && !metadata.IsDictionary && metadata.ClrType != typeof(string), GetClassMetadata)
				.AddMap<Dictionary>(metadata => metadata.IsDictionary, GetDictionaryMetadata);

			setupMap?.Invoke(Collection);
		}

		public virtual Class GetClassMetadata(TypeMetadata metadata, int callNumber)
		{
			var typeName = GetTypeName(metadata);
			var propertiesMetadata = metadata.PropertiesMetadata.Select(x => GetOutputPropertyType(x, callNumber));
			return new (typeName, GetAllUsedTypes(metadata, callNumber), propertiesMetadata, metadata);
		}

		public virtual Enum GetEnumMetadata(TypeMetadata metadata, int callNumber)
		{
			var typeName = GetTypeName(metadata);
			return new(typeName, metadata);
		}

		protected virtual OutputPropertyType GetOutputPropertyType(PropertyMetadata metadata, int callNumber)
		{
			return new ()
			{
				Source = metadata.Source,
				OutputType = GetOutputType(metadata.PropertyType, callNumber),
				PropertyName = metadata.PropertyName,
				PropertyMetadata = metadata,
			};
		}

		protected virtual Array GetArrayMetadata(TypeMetadata metadata, int callNumber)
		{
			var genericArgs = metadata.GenericTypeArguments?.ToArray();
			if (genericArgs is null || genericArgs.Length != 1)
			{
				throw new ArgumentOutOfRangeException(nameof(genericArgs));
			}

			var enumerableType = genericArgs[0];

			var type = GetOutputType(enumerableType, callNumber);
			return type is not null ? new(type.Name, GetAllUsedTypes(enumerableType, callNumber), metadata) : null;
		}

		protected virtual Dictionary GetDictionaryMetadata(TypeMetadata metadata, int callNumber)
		{
			var genericArgs = metadata.GenericTypeArguments?.ToArray();
			if (genericArgs is null || genericArgs.Length != 2)
			{
				throw new ArgumentOutOfRangeException(nameof(genericArgs));
			}

			var keyValueTypeNames = genericArgs.Select(x => GetOutputType(x, callNumber)).Select(x => x?.Name);
			return new(keyValueTypeNames, GetAllUsedTypes(metadata, callNumber));
		}

		protected virtual IEnumerable<OutputType> GetAllUsedTypes(TypeMetadata metadata, int callNumber)
		{
			var allUsedTypes = new List<OutputType>(16);
			var parentType = GetUsedParentType(metadata);

			if (parentType is not null)
			{
				allUsedTypes.AddRange(FilterImportingTypes(new []{ parentType }, callNumber));
			}

			allUsedTypes.AddRange(FilterImportingTypes(metadata.GenericTypeArguments?.ToArray(), callNumber));
			allUsedTypes.AddRange(FilterImportingTypes(metadata.PropertiesMetadata.Select(x => x.PropertyType).ToArray(), callNumber));

			var result = new HashSet<OutputType>(allUsedTypes, new UsedOutputTypesEqualityComparer());
			return result.ToArray();
		}

		[CanBeNull]
		protected virtual TypeMetadata GetUsedParentType(TypeMetadata metadata)
		{
			return metadata.ParentTypeMetadata;
		}

		[NotNull]
		protected virtual IEnumerable<TypeMetadata> GetUsedGenericTypes([CanBeNull] params TypeMetadata[] metadata)
		{
			return metadata?.SelectMany(x => x.GenericTypeArguments) ?? Enumerable.Empty<TypeMetadata>();
		}

		[NotNull]
		protected virtual IEnumerable<OutputType> FilterImportingTypes([CanBeNull]TypeMetadata[] metadata, int callNumber)
		{
			var typeMetadata = metadata?.Select(x => GetOutputType(x, callNumber));
			var result = typeMetadata?
				.Where(x => x is not StaticOutputType)
				.Where(x => x is not Array)
				.Where(x => x?.TypeMetadata != null)
				.Where(x => !x.TypeMetadata.ClrType.Assembly.FullName.Contains("System"));

			return result ?? System.Array.Empty<OutputType>();
		}

		/// <inheritdoc />
		public override OutputType GetOutputType(TypeMetadata metadata, int callNumber = 0)
		{
			if (callNumber > 10)
			{
				return null;
			}

			var descriptor = Collection.GetMap(metadata);
			return descriptor?.GetOutputType(metadata, ++callNumber);
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