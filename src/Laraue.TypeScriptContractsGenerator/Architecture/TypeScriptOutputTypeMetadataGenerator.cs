using System;
using System.Linq;
using System.Text.RegularExpressions;
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
			return new (typeName);
		}

		protected virtual Array GetArrayMetadata(TypeMetadata metadata)
		{
			return new(metadata);
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