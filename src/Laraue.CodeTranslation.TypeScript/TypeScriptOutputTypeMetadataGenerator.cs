using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Common;
using Laraue.CodeTranslation.TypeScript.Types;
using Newtonsoft.Json.Linq;
using Array = Laraue.CodeTranslation.TypeScript.Types.Array;
using Boolean = Laraue.CodeTranslation.TypeScript.Types.Boolean;
using Enum = Laraue.CodeTranslation.TypeScript.Types.Enum;
using String = Laraue.CodeTranslation.TypeScript.Types.String;

namespace Laraue.CodeTranslation.TypeScript
{
	public class TypeScriptOutputTypeMetadataGenerator : OutputTypeMetadataGenerator
	{
		private readonly Dictionary<Type, OutputType> _cache = new();

		public TypeScriptOutputTypeMetadataGenerator(Action<MapCollection> setupMap = null) 
			: base(new MapCollection())
		{
			Collection
				.AddMap<Dictionary>(metadata => metadata.IsDictionary, GetDictionaryMetadata)
				.AddMap<Class>(metadata => metadata.ClrType.IsClass && !metadata.IsDictionary && metadata.ClrType != typeof(string), GetClassMetadata)
				.AddMap<Array>(metadata => metadata.IsEnumerable && !metadata.IsDictionary && metadata.ClrType != typeof(string), GetArrayMetadata)
				.AddMap<Enum>(metadata => metadata.IsEnum, GetEnumMetadata)
				.AddMap<int, Number>()
				.AddMap<decimal, Number>()
				.AddMap<double, Number>()
				.AddMap<long, Number>()
				.AddMap<short, Number>()
				.AddMap<float, Number>()
				.AddMap<string, String>()
				.AddMap<bool, Boolean>()
				.AddMap<Guid, String>()
				.AddMap<DateTime, Date>()
				.AddMap<DateTimeOffset, Date>()
				.AddMap<Uri, String>()
				.AddMap<JObject, Any>()
				.AddMap<JToken, Any>();
			
			setupMap?.Invoke(Collection);
		}

		/// <inheritdoc />
		public override OutputType GetOutputType(TypeMetadata metadata, int callNumber = 0)
		{
			if (_cache.TryGetValue(metadata.ClrType, out var result))
			{
				return result;
			}

			if (callNumber > 8)
			{
				return null;
			}

			var descriptor = Collection.GetMap(metadata);
			result = descriptor?.GetOutputType(metadata, ++callNumber);
			
			if (_cache.ContainsKey(metadata.ClrType))
			{
				_cache.Add(metadata.ClrType, result);
			}

			return result;
		}

		protected virtual OutputPropertyType GetOutputPropertyType(PropertyMetadata metadata, int callNumber)
		{
			return new()
			{
				Source = metadata.Source,
				OutputType = GetOutputType(metadata.PropertyType, callNumber) ?? throw new InvalidOperationException($"Mapping for {metadata.PropertyType.ClrType} is not registered"),
				PropertyName = metadata.PropertyName,
				PropertyMetadata = metadata,
			};
		}

		[CanBeNull]
		public virtual Class GetClassMetadata([CanBeNull]TypeMetadata metadata, int callNumber)
		{
			if (metadata is null) return null;
			var typeName = GetTypeName(metadata);
			var propertiesMetadata = metadata.PropertiesMetadata.Select(x => GetOutputPropertyType(x, callNumber));
			var parentTypeMetadata = GetClassMetadata(GetUsedParentType(metadata), callNumber);
			return new(typeName, GetAllUsedTypes(metadata, callNumber), propertiesMetadata, metadata, ShouldBeImported(parentTypeMetadata) ? parentTypeMetadata.Name : null);
		}

		public virtual Enum GetEnumMetadata(TypeMetadata metadata, int callNumber)
		{
			var typeName = GetTypeName(metadata);
			return new(typeName, metadata);
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
				allUsedTypes.Add(GetOutputType(parentType, callNumber));
			}

			allUsedTypes.AddRange(metadata?.GenericTypeArguments?.Select(x => GetOutputType(x, callNumber)) ?? Enumerable.Empty<OutputType>());
			allUsedTypes.AddRange(metadata?.PropertiesMetadata?.Select(x => x.PropertyType).Select(x => GetOutputType(x, callNumber)) ?? Enumerable.Empty<OutputType>());

			var filteredTypes = FilterImportingTypes(allUsedTypes.ToArray());

			var result = new HashSet<OutputType>(filteredTypes, new UsedOutputTypesEqualityComparer());
			return result.ToArray();
		}

		[CanBeNull]
		protected virtual TypeMetadata GetUsedParentType([CanBeNull] TypeMetadata metadata)
		{
			return metadata?.ParentTypeMetadata;
		}
		

		[NotNull]
		protected virtual IEnumerable<OutputType> FilterImportingTypes([CanBeNull]OutputType[] types)
		{
			var result = types?.Where(ShouldBeImported);
			return result ?? System.Array.Empty<OutputType>();
		}

		protected virtual bool ShouldBeImported([CanBeNull] OutputType type)
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

		[NotNull]
		protected virtual TypeMetadata[] GetGenericTypeArguments(TypeMetadata metadata)
		{
			return metadata.GenericTypeArguments?.ToArray() ?? System.Array.Empty<TypeMetadata>();
		}

		[NotNull]
		protected virtual string GetNonGenericStringTypeName([NotNull] TypeMetadata metadata)
		{
			var typeName = metadata.ClrType.Name;
			return Regex.Replace(typeName, @"`\d+", string.Empty);
		}

		/// <summary>
		/// Get <see cref="OutputTypeName"/> for type. 
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		[NotNull]
		public OutputTypeName GetTypeName([NotNull] TypeMetadata metadata)
		{
			var typeName = GetNonGenericStringTypeName(metadata);
			var genericArgs = GetGenericTypeArguments(metadata);
			var genericOutputTypes = genericArgs.Select(GetOutputType).ToArray();
			return new OutputTypeName(typeName, genericOutputTypes.Select(x => x.Name));
		}
	}
}