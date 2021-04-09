using System;
using System.Linq;
using JetBrains.Annotations;
using Laraue.CodeTranslation.Abstractions.Metadata;
using Laraue.CodeTranslation.Abstractions.Output;
using Laraue.CodeTranslation.Abstractions.Translation;
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
		public TypeScriptOutputTypeMetadataGenerator(CodeTranslatorOptions options, IOutputTypeProvider provider) 
			: base(new MapCollection(), provider)
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

			options.ConfigureTypeMap?.Invoke(Collection);
		}

		/// <inheritdoc />
		protected override OutputType GetOutputTypeInternal(TypeMetadata metadata)
		{
			var descriptor = Collection.GetMap(metadata);
			return descriptor?.GetOutputType(metadata);
		}

		[CanBeNull]
		public virtual Class GetClassMetadata([CanBeNull]TypeMetadata metadata)
		{
			if (metadata is null) return null;
			return new(metadata, TypeProvider);
		}

		public virtual Enum GetEnumMetadata(TypeMetadata metadata)
		{
			return new(metadata);
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
			return type is not null ? new(type.Name, metadata, TypeProvider) : null;
		}

		protected virtual Dictionary GetDictionaryMetadata(TypeMetadata metadata)
		{
			var genericArgs = metadata.GenericTypeArguments?.ToArray();
			if (genericArgs is null || genericArgs.Length != 2)
			{
				throw new ArgumentOutOfRangeException(nameof(genericArgs));
			}

			return new(metadata, TypeProvider);
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
	}
}