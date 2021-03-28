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
		protected override OutputType GetOutputTypeInternal(TypeMetadata metadata)
		{
			var descriptor = Collection.GetMap(metadata);
			return descriptor?.GetOutputType(metadata);
		}

		protected virtual OutputPropertyType GetOutputPropertyType(PropertyMetadata metadata)
		{
			return new()
			{
				Source = metadata.Source,
				OutputType = TypeProvider.Get(metadata.PropertyType.ClrType) ?? throw new InvalidOperationException($"Mapping for {metadata.PropertyType.ClrType} is not registered"),
				PropertyName = metadata.PropertyName,
				PropertyMetadata = metadata,
			};
		}

		[CanBeNull]
		public virtual Class GetClassMetadata([CanBeNull]TypeMetadata metadata)
		{
			if (metadata is null) return null;
			var typeName = GetTypeName(metadata);
			var propertiesMetadata = metadata.PropertiesMetadata.Select(GetOutputPropertyType).ToArray();
			var parentTypeMetadata = GetClassMetadata(GetUsedParentType(metadata));
			return new(typeName, GetAllUsedTypes(metadata), propertiesMetadata, metadata, ShouldBeImported(parentTypeMetadata) ? parentTypeMetadata.Name : null);
		}

		public virtual Enum GetEnumMetadata(TypeMetadata metadata)
		{
			var typeName = GetTypeName(metadata);
			return new(typeName, metadata);
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
			return type is not null ? new(type.Name, GetAllUsedTypes(enumerableType), metadata) : null;
		}

		protected virtual Dictionary GetDictionaryMetadata(TypeMetadata metadata)
		{
			var genericArgs = metadata.GenericTypeArguments?.ToArray();
			if (genericArgs is null || genericArgs.Length != 2)
			{
				throw new ArgumentOutOfRangeException(nameof(genericArgs));
			}

			var keyValueTypeNames = genericArgs.Select(x => TypeProvider.Get(x.ClrType)).Select(x => x?.Name);
			return new(keyValueTypeNames, GetAllUsedTypes(metadata));
		}

		protected virtual OutputType[] GetAllUsedTypes(TypeMetadata metadata)
		{
			var allUsedTypes = DependenciesGraph.GetResolvingTypesSequence(metadata);
			var usedMetadataOutputTypes = allUsedTypes.Select(x => TypeProvider.Get(x.ClrType));
			var filteredTypes = FilterImportingTypes(usedMetadataOutputTypes.ToArray());
			return filteredTypes;
		}

		/// <inheritdoc />
		protected override TypeMetadata GetUsedParentType(TypeMetadata metadata)
		{
			return metadata?.ParentTypeMetadata;
		}
		
		/// <summary>
		/// Filters all passed types and takes only should be imported in result code.
		/// </summary>
		/// <param name="types"></param>
		/// <returns></returns>
		[NotNull]
		protected virtual OutputType[] FilterImportingTypes([CanBeNull]OutputType[] types)
		{
			var result = types?.Where(ShouldBeImported)?.ToArray();
			return result ?? System.Array.Empty<OutputType>();
		}

		/// <summary>
		/// Returns true, if this type should be imported in generated code.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Return generic type arguments for some type.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
		[NotNull]
		protected virtual TypeMetadata[] GetGenericTypeArguments([NotNull] TypeMetadata metadata)
		{
			return metadata.GenericTypeArguments?.ToArray() ?? System.Array.Empty<TypeMetadata>();
		}

		/// <summary>
		/// Returns clear type name without generic parameters.
		/// </summary>
		/// <param name="metadata"></param>
		/// <returns></returns>
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
			var genericOutputTypes = genericArgs.Select(x => TypeProvider.Get(x.ClrType)).Where(x => x is not null).ToArray();
			return new OutputTypeName(typeName, genericOutputTypes.Select(x => x.Name));
		}
	}
}