using System;
using System.Collections.Generic;

namespace Laraue.TypeScriptContractsGenerator.UnitTests
{
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
