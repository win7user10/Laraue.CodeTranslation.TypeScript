using System;
using System.Collections.Generic;
using System.Linq;
using Laraue.TypeScriptContractsGenerator.Generators;
using Laraue.TypeScriptContractsGenerator.Typing;
using Xunit;

namespace Laraue.TypeScriptContractsGenerator.UnitTests.Generators
{
	public class DefaultTsCodeGeneratorTests
	{
		private string GetPropertySourceCode(string propertyName)
		{
			var tsType = new TsType(typeof(MainClass), new DefaultTsTypeGenerator(), new DefaultTsCodeGenerator());
			var tsProperty = tsType.Properties.FirstOrDefault(x => x.PropertyInfo.Name == propertyName);
			var code = new DefaultTsCodeGenerator().GetTsPropertyCode(tsProperty);
			return code;
		}

		[Theory]
		[InlineData(nameof(MainClass.IntValue), "intValue: number = 0;")]
		[InlineData(nameof(MainClass.StringValue), "stringValue: string | null = null;")]
		[InlineData(nameof(MainClass.DoubleValue), "doubleValue: number = 0;")]
		[InlineData(nameof(MainClass.DecimalValue), "decimalValue: number = 0;")]
		[InlineData(nameof(MainClass.BigIntValue), "bigIntValue: number = 0;")]
		[InlineData(nameof(MainClass.GuidValue), "guidValue: string = '';")]
		[InlineData(nameof(MainClass.SubClassValue), "subClassValue: SubClass | null = null;")]
		[InlineData(nameof(MainClass.EnumerableIntValue), "enumerableIntValue: Array<number> | null = null;")]
		[InlineData(nameof(MainClass.ArrayIntValue), "arrayIntValue: Array<number> | null = null;")]
		[InlineData(nameof(MainClass.MultiDimensionIntArrayValue), "multiDimensionIntArrayValue: Array<Array<number>> | null = null;")]
		[InlineData(nameof(MainClass.EnumerableWithArrayIntValue), "enumerableWithArrayIntValue: Array<Array<number>> | null = null;")]
		[InlineData(nameof(MainClass.EnumerableSubClassValue), "enumerableSubClassValue: Array<SubClass> | null = null;")]
		[InlineData(nameof(MainClass.ArraySubClassValue), "arraySubClassValue: Array<SubClass> | null = null;")]
		[InlineData(nameof(MainClass.MultiDimensionArraySubClassValue), "multiDimensionArraySubClassValue: Array<Array<SubClass>> | null = null;")]
		[InlineData(nameof(MainClass.EnumerableWithArraySubClassValue), "enumerableWithArraySubClassValue: Array<Array<SubClass>> | null = null;")]
		[InlineData(nameof(MainClass.OneTypeGenericSubClassValue), "oneTypeGenericSubClassValue: OneTypeGenericSubClass<number> | null = null;")]
		[InlineData(nameof(MainClass.TwoTypesGenericSubValue), "twoTypesGenericSubValue: TwoTypeGenericSubClass<number, number> | null = null;")]
		[InlineData(nameof(MainClass.TwoTypesGenericSubValueArray), "twoTypesGenericSubValueArray: Array<TwoTypeGenericSubClass<number, number>> | null = null;")]
		[InlineData(nameof(MainClass.TwoTypesGenericSubValueEnumerable), "twoTypesGenericSubValueEnumerable: Array<TwoTypeGenericSubClass<number, number>> | null = null;")]
		public void PropertyTranslation(string propertyName, string exceptedCode)
		{
			var code = GetPropertySourceCode(propertyName);
			Assert.Equal(exceptedCode, code);
		}
	}

	public class MainClass
	{
		public int IntValue { get; set; }
		public string StringValue { get; set; }
		public double DoubleValue { get; set; }
		public decimal DecimalValue { get; set; }
		public long BigIntValue { get; set; }
		public Guid GuidValue { get; set; }
		public SubClass SubClassValue { get; set; }
		public IEnumerable<int> EnumerableIntValue { get; set; }
		public int[] ArrayIntValue { get; set; }
		public int[][] MultiDimensionIntArrayValue { get; set; }
		public IEnumerable<int[]> EnumerableWithArrayIntValue { get; set; }
		public IEnumerable<SubClass> EnumerableSubClassValue { get; set; }
		public SubClass[] ArraySubClassValue { get; set; }
		public SubClass[][] MultiDimensionArraySubClassValue { get; set; }
		public IEnumerable<SubClass[]> EnumerableWithArraySubClassValue { get; set; }
		public OneTypeGenericSubClass<int> OneTypeGenericSubClassValue { get; set; }
		public TwoTypeGenericSubClass<int, decimal> TwoTypesGenericSubValue { get; set; }
		public TwoTypeGenericSubClass<int, decimal>[] TwoTypesGenericSubValueArray { get; set; }
		public IEnumerable<TwoTypeGenericSubClass<int, decimal>> TwoTypesGenericSubValueEnumerable { get; set; }
	}

	public class SubClass
	{
		public int IntValue { get; set; }
	}

	public class OneTypeGenericSubClass<T>
	{
		public T GenericType { get; set; }
	}

	public class TwoTypeGenericSubClass<T, U>
	{
		public T FirstGenericType { get; set; }

		public U SecondGenericType { get; set; }
	}
}
