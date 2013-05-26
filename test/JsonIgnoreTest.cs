using LitJson;
using NUnit.Framework;
using System.Collections;

namespace LitJson.Test
{
	class PrimitiveIgnoreFields
	{
		[JsonIgnore] public sbyte sbyteField;
		[JsonIgnore] public byte byteField;
		[JsonIgnore] public short shortField;
		[JsonIgnore] public ushort ushortField;
		[JsonIgnore] public int intField;
		[JsonIgnore] public uint uintField;
		[JsonIgnore] public long longField;
		[JsonIgnore] public ulong ulongField;
		[JsonIgnore] public decimal decimalField;
		[JsonIgnore] public double doubleField;
		[JsonIgnore] public float floatField;
		[JsonIgnore] public string stringField;
		[JsonIgnore] public bool boolField;
	}

	class PrimitiveIgnoreProperties
	{
		[JsonIgnore] public sbyte sbyteField { get; set; }
		[JsonIgnore] public byte byteField { get; set; }
		[JsonIgnore] public short shortField { get; set; }
		[JsonIgnore] public ushort ushortField { get; set; }
		[JsonIgnore] public int intField { get; set; }
		[JsonIgnore] public uint uintField { get; set; }
		[JsonIgnore] public long longField { get; set; }
		[JsonIgnore] public ulong ulongField { get; set; }
		[JsonIgnore] public decimal decimalField { get; set; }
		[JsonIgnore] public double doubleField { get; set; }
		[JsonIgnore] public float floatField { get; set; }
		[JsonIgnore] public string stringField { get; set; }
		[JsonIgnore] public bool boolField { get; set; }
	}

	class ObjectIgnoreFields
	{
		[JsonIgnore] public IDictionary dictField;
		[JsonIgnore] public IList listField;
		[JsonIgnore] public ObjectClass objectField;
	}

	class ObjectIgnoreProperties
	{
		[JsonIgnore] public IDictionary dictField { get; set; }
		[JsonIgnore] public IList listField { get; set; }
		[JsonIgnore] public ObjectClass objectField { get; set; }
	}

	class ObjectClass
	{
		public int ignored;
	}

	[TestFixture]
	public class JsonIgnoreTest
	{
		[Test]
		public void CanIgnorePrimitiveFields()
		{
			string json = @"{
				""sbyteField"": -1,
				""byteField"": 1,
				""shortField"": -1,
				""ushortField"": 1,
				""intField"": -1,
				""uintField"": 1,
				""longField"": -1,
				""ulongField"": 1,
				""decimalField"": 1.0,
				""doubleField"": 1.0,
				""floatField"": 1.0,
				""stringField"": ""value"",
				""boolField"": true
			}";

			PrimitiveIgnoreFields p_test = JsonMapper.ToObject<PrimitiveIgnoreFields>(json);

			Assert.AreNotEqual((sbyte)-1, p_test.sbyteField);
			Assert.AreNotEqual((byte)1, p_test.byteField);
			Assert.AreNotEqual((short)-1, p_test.shortField);
			Assert.AreNotEqual((ushort)1, p_test.ushortField);
			Assert.AreNotEqual(-1, p_test.intField);
			Assert.AreNotEqual(1, p_test.uintField);
			Assert.AreNotEqual(-1L, p_test.longField);
			Assert.AreNotEqual(1L, p_test.ulongField);
			Assert.AreNotEqual(1.0M, p_test.decimalField);
			Assert.AreNotEqual(1.0D, p_test.doubleField);
			Assert.AreNotEqual(1.0F, p_test.floatField);
			Assert.AreNotEqual("value", p_test.stringField);
			Assert.AreNotEqual(true, p_test.boolField);
		}

		[Test]
		public void CanIgnorePrimitiveProperties()
		{
			string json = @"{
				""sbyteField"": -1,
				""byteField"": 1,
				""shortField"": -1,
				""ushortField"": 1,
				""intField"": -1,
				""uintField"": 1,
				""longField"": -1,
				""ulongField"": 1,
				""decimalField"": 1.0,
				""doubleField"": 1.0,
				""floatField"": 1.0,
				""stringField"": ""value"",
				""boolField"": true
			}";

			PrimitiveIgnoreProperties p_test = JsonMapper.ToObject<PrimitiveIgnoreProperties>(json);

			Assert.AreNotEqual((sbyte)-1, p_test.sbyteField);
			Assert.AreNotEqual((byte)1, p_test.byteField);
			Assert.AreNotEqual((short)-1, p_test.shortField);
			Assert.AreNotEqual((ushort)1, p_test.ushortField);
			Assert.AreNotEqual(-1, p_test.intField);
			Assert.AreNotEqual(1, p_test.uintField);
			Assert.AreNotEqual(-1L, p_test.longField);
			Assert.AreNotEqual(1L, p_test.ulongField);
			Assert.AreNotEqual(1.0M, p_test.decimalField);
			Assert.AreNotEqual(1.0D, p_test.doubleField);
			Assert.AreNotEqual(1.0F, p_test.floatField);
			Assert.AreNotEqual("value", p_test.stringField);
			Assert.AreNotEqual(true, p_test.boolField);
		}

		[Test]
		public void CanIgnoreObjectFields()
		{
			string json = @"{
				""dictField"": {
					""key"": ""value""
				},
				""listField"": [1, 2, 3],
				""objectField"": {
					""ignored"": true
				}
			}";

			ObjectIgnoreFields o_test = JsonMapper.ToObject<ObjectIgnoreFields>(json);

			Assert.AreEqual(null, o_test.dictField);
			Assert.AreEqual(null, o_test.listField);
			Assert.AreEqual(null, o_test.objectField);
		}

		[Test]
		public void CanIgnoreObjectProperties()
		{
			string json = @"{
				""dictField"": {
					""key"": ""value""
				},
				""listField"": [1, 2, 3],
				""objectField"": {
					""ignored"": true
				}
			}";

			ObjectIgnoreProperties o_test = JsonMapper.ToObject<ObjectIgnoreProperties>(json);

			Assert.AreEqual(null, o_test.dictField);
			Assert.AreEqual(null, o_test.listField);
			Assert.AreEqual(null, o_test.objectField);
		}
	}
}
