using System;
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
			return new (metadata);
		}

		public virtual Array GetArrayMetadata(TypeMetadata metadata)
		{
			return new(metadata);
		}

		/// <inheritdoc />
		public override OutputType GetOutputType(TypeMetadata metadata)
		{
			var descriptor = Collection.GetMap(metadata);
			return descriptor.GetOutputType(metadata);
		}
	}
}