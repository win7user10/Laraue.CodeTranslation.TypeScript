using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Laraue.CodeTranslation.UnitTests
{
	[ShouldBeTaken]
	public class MainClass
	{
		public EnumStartsWith0 EnumStartsWith0 { get; set; }
		public EnumStartsWith10[] EnumerableEnum { get; set; }
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
		public Dictionary<int, string> DictionaryIntStringValue { get; set; }
		public JObject JObjectValue { get; set; }
		public int? NullableIntValue { get; set; }
		public Guid? NullableGuidValue { get; set; }
		public bool Boolean { get; set; }
		public bool? NullableBoolean { get; set; }

	}

	public class SubClass : MainClass
	{
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

	public enum EnumStartsWith0
	{
		Value0,
		Value1,
	}

	public enum EnumStartsWith10
	{
		Value0 = 10,
		Value1,
	}
}
