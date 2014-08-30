using LitJson;
using NUnit.Framework;
using System.Collections;
using System.Text.RegularExpressions;

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

	class PrimitiveIgnoreFieldsSerializing
	{
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public sbyte sbyteField;
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public byte byteField;
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public short shortField;
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public ushort ushortField;
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public int intField;
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public uint uintField;
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public long longField;
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public ulong ulongField;
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public decimal decimalField;
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public double doubleField;
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public float floatField;
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public string stringField;
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public bool boolField;
	}

	class PrimitiveIgnoreFieldsDeserializing
	{
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public sbyte sbyteField;
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public byte byteField;
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public short shortField;
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public ushort ushortField;
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public int intField;
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public uint uintField;
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public long longField;
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public ulong ulongField;
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public decimal decimalField;
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public double doubleField;
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public float floatField;
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public string stringField;
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public bool boolField;
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

	class PrimitiveIgnorePropertiesSerializing
	{
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public sbyte sbyteField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public byte byteField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public short shortField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public ushort ushortField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public int intField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public uint uintField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public long longField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public ulong ulongField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public decimal decimalField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public double doubleField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public float floatField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public string stringField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public bool boolField { get; set; }
	}

	class PrimitiveIgnorePropertiesDeserializing
	{
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public sbyte sbyteField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public byte byteField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public short shortField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public ushort ushortField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public int intField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public uint uintField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public long longField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public ulong ulongField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public decimal decimalField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public double doubleField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public float floatField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public string stringField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public bool boolField { get; set; }
	}

	class ObjectIgnoreFields
	{
		[JsonIgnore] public Hashtable dictField;
		[JsonIgnore] public ArrayList listField;
		[JsonIgnore] public ObjectClass objectField;
	}

	class ObjectIgnoreFieldsSerializing
	{
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public Hashtable dictField;
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public ArrayList listField;
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public ObjectClass objectField;
	}

	class ObjectIgnoreFieldsDeserializing
	{
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public Hashtable dictField;
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public ArrayList listField;
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public ObjectClass objectField;
	}

	class ObjectIgnoreProperties
	{
		[JsonIgnore] public Hashtable dictField { get; set; }
		[JsonIgnore] public ArrayList listField { get; set; }
		[JsonIgnore] public ObjectClass objectField { get; set; }
	}

	class ObjectIgnorePropertiesSerializing
	{
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public Hashtable dictField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public ArrayList listField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Serializing)] public ObjectClass objectField { get; set; }
	}

	class ObjectIgnorePropertiesDeserializing
	{
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public Hashtable dictField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public ArrayList listField { get; set; }
		[JsonIgnore(JsonIgnoreWhen.Deserializing)] public ObjectClass objectField { get; set; }
	}

	class ObjectClass
	{
		public bool ignored;
	}

	[TestFixture]
	public class JsonIgnoreTest
	{
		[Test]
		public void CanIgnorePrimitiveFieldsWhenDeserializing()
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
		public void CanIgnorePrimitivePropertiesWhenDeserializing()
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
		public void CanIgnoreObjectFieldsWhenDeserializing()
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
		public void CanIgnoreObjectPropertiesWhenDeserializing()
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

		[Test]
		public void CanIgnorePrimitiveFieldsWhenSerializing()
		{
			PrimitiveIgnoreFields p_test = new PrimitiveIgnoreFields {
				sbyteField = -1,
				byteField = 1,
				shortField = -1,
				ushortField = 1,
				intField = -1,
				uintField = 1,
				longField = -1,
				ulongField = 1,
				decimalField = 1.0M,
				doubleField = 1.0D,
				floatField = 1.0F,
				stringField = "value",
				boolField = true
			};

			string json = JsonMapper.ToJson(p_test);

			Assert.AreEqual("{}", json);
		}

		[Test]
		public void CanIgnorePrimitivePropertiesWhenSerializing()
		{
			PrimitiveIgnoreProperties p_test = new PrimitiveIgnoreProperties {
				sbyteField = -1,
				byteField = 1,
				shortField = -1,
				ushortField = 1,
				intField = -1,
				uintField = 1,
				longField = -1,
				ulongField = 1,
				decimalField = 1.0M,
				doubleField = 1.0D,
				floatField = 1.0F,
				stringField = "value",
				boolField = true
			};

			string json = JsonMapper.ToJson(p_test);

			Assert.AreEqual("{}", json);
		}

		[Test]
		public void CanIgnoreObjectFieldsWhenSerializing()
		{
			ObjectIgnoreFields o_test = new ObjectIgnoreFields {
				dictField = new Hashtable(),
				listField = new ArrayList(),
				objectField = new ObjectClass()
			};

			string json = JsonMapper.ToJson(o_test);

			Assert.AreEqual("{}", json);
		}

		[Test]
		public void CanIgnoreObjectPropertiesWhenSerializing()
		{
			ObjectIgnoreProperties o_test = new ObjectIgnoreProperties {
				dictField = new Hashtable(),
				listField = new ArrayList(),
				objectField = new ObjectClass()
			};

			string json = JsonMapper.ToJson(o_test);

			Assert.AreEqual("{}", json);
		}

		[Test]
		public void CanIgnorePrimitiveFieldsOnlyWhenSerializing()
		{
			PrimitiveIgnoreFieldsSerializing p_test = new PrimitiveIgnoreFieldsSerializing {
				sbyteField = -1,
				byteField = 1,
				shortField = -1,
				ushortField = 1,
				intField = -1,
				uintField = 1,
				longField = -1,
				ulongField = 1,
				decimalField = 1.0M,
				doubleField = 1.0D,
				floatField = 1.0F,
				stringField = "value",
				boolField = true
			};

			string json = JsonMapper.ToJson(p_test);

			Assert.AreEqual("{}", json);

			json = @"{
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

			p_test = JsonMapper.ToObject<PrimitiveIgnoreFieldsSerializing>(json);

			Assert.AreEqual((sbyte)-1, p_test.sbyteField);
			Assert.AreEqual((byte)1, p_test.byteField);
			Assert.AreEqual((short)-1, p_test.shortField);
			Assert.AreEqual((ushort)1, p_test.ushortField);
			Assert.AreEqual(-1, p_test.intField);
			Assert.AreEqual(1, p_test.uintField);
			Assert.AreEqual(-1L, p_test.longField);
			Assert.AreEqual(1L, p_test.ulongField);
			Assert.AreEqual(1.0M, p_test.decimalField);
			Assert.AreEqual(1.0D, p_test.doubleField);
			Assert.AreEqual(1.0F, p_test.floatField);
			Assert.AreEqual("value", p_test.stringField);
			Assert.AreEqual(true, p_test.boolField);
		}

		[Test]
		public void CanIgnorePrimitivePropertiesOnlyWhenSerializing()
		{
			PrimitiveIgnorePropertiesSerializing p_test = new PrimitiveIgnorePropertiesSerializing {
				sbyteField = -1,
				byteField = 1,
				shortField = -1,
				ushortField = 1,
				intField = -1,
				uintField = 1,
				longField = -1,
				ulongField = 1,
				decimalField = 1.0M,
				doubleField = 1.0D,
				floatField = 1.0F,
				stringField = "value",
				boolField = true
			};

			string json = JsonMapper.ToJson(p_test);

			Assert.AreEqual("{}", json);

			json = @"{
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

			p_test = JsonMapper.ToObject<PrimitiveIgnorePropertiesSerializing>(json);

			Assert.AreEqual((sbyte)-1, p_test.sbyteField);
			Assert.AreEqual((byte)1, p_test.byteField);
			Assert.AreEqual((short)-1, p_test.shortField);
			Assert.AreEqual((ushort)1, p_test.ushortField);
			Assert.AreEqual(-1, p_test.intField);
			Assert.AreEqual(1, p_test.uintField);
			Assert.AreEqual(-1L, p_test.longField);
			Assert.AreEqual(1L, p_test.ulongField);
			Assert.AreEqual(1.0M, p_test.decimalField);
			Assert.AreEqual(1.0D, p_test.doubleField);
			Assert.AreEqual(1.0F, p_test.floatField);
			Assert.AreEqual("value", p_test.stringField);
			Assert.AreEqual(true, p_test.boolField);
		}

		[Test]
		public void CanIgnoreObjectFieldsOnlyWhenSerializing()
		{
			ObjectIgnoreFieldsSerializing o_test = new ObjectIgnoreFieldsSerializing {
				dictField = new Hashtable(),
				listField = new ArrayList(),
				objectField = new ObjectClass()
			};

			string json = JsonMapper.ToJson(o_test);

			Assert.AreEqual("{}", json);

			json = @"{
				""dictField"": {
					""key"": ""value""
				},
				""listField"": [1, 2, 3],
				""objectField"": {
					""ignored"": true
				}
			}";

			o_test = JsonMapper.ToObject<ObjectIgnoreFieldsSerializing>(json);

			Assert.AreNotEqual(null, o_test.dictField);
			Assert.AreNotEqual(null, o_test.listField);
			Assert.AreNotEqual(null, o_test.objectField);

			Assert.AreEqual(1, o_test.dictField.Count);
			Assert.AreEqual("value", (string)(JsonData)o_test.dictField["key"]);

			Assert.AreEqual(3, o_test.listField.Count);
			Assert.AreEqual(1, o_test.listField[0]);
			Assert.AreEqual(2, o_test.listField[1]);
			Assert.AreEqual(3, o_test.listField[2]);

			Assert.AreEqual(true, o_test.objectField.ignored);
		}

		[Test]
		public void CanIgnoreObjectPropertiesOnlyWhenSerializing()
		{
			ObjectIgnorePropertiesSerializing o_test = new ObjectIgnorePropertiesSerializing {
				dictField = new Hashtable(),
				listField = new ArrayList(),
				objectField = new ObjectClass()
			};

			string json = JsonMapper.ToJson(o_test);

			Assert.AreEqual("{}", json);

			json = @"{
				""dictField"": {
					""key"": ""value""
				},
				""listField"": [1, 2, 3],
				""objectField"": {
					""ignored"": true
				}
			}";

			o_test = JsonMapper.ToObject<ObjectIgnorePropertiesSerializing>(json);

			Assert.AreNotEqual(null, o_test.dictField);
			Assert.AreNotEqual(null, o_test.listField);
			Assert.AreNotEqual(null, o_test.objectField);

			Assert.AreEqual(1, o_test.dictField.Count);
			Assert.AreEqual("value", (string)(JsonData)o_test.dictField["key"]);

			Assert.AreEqual(3, o_test.listField.Count);
			Assert.AreEqual(1, o_test.listField[0]);
			Assert.AreEqual(2, o_test.listField[1]);
			Assert.AreEqual(3, o_test.listField[2]);

			Assert.AreEqual(true, o_test.objectField.ignored);
		}

		[Test]
		public void CanIgnorePrimitiveFieldsOnlyWhenDeserializing()
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

			PrimitiveIgnoreFieldsDeserializing p_test = JsonMapper.ToObject<PrimitiveIgnoreFieldsDeserializing>(json);

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

			p_test = new PrimitiveIgnoreFieldsDeserializing {
				sbyteField = -1,
				byteField = 1,
				shortField = -1,
				ushortField = 1,
				intField = -1,
				uintField = 1,
				longField = -1,
				ulongField = 1,
				decimalField = 1.0M,
				doubleField = 1.0D,
				floatField = 1.0F,
				stringField = "value",
				boolField = true
			};

			string json2 = JsonMapper.ToJson(p_test);

			Assert.AreEqual(Regex.Replace(json, @"\s+", ""), json2);
		}

		[Test]
		public void CanIgnorePrimitivePropertiesOnlyWhenDeserializing()
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

			PrimitiveIgnorePropertiesDeserializing p_test = JsonMapper.ToObject<PrimitiveIgnorePropertiesDeserializing>(json);

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

			p_test = new PrimitiveIgnorePropertiesDeserializing {
				sbyteField = -1,
				byteField = 1,
				shortField = -1,
				ushortField = 1,
				intField = -1,
				uintField = 1,
				longField = -1,
				ulongField = 1,
				decimalField = 1.0M,
				doubleField = 1.0D,
				floatField = 1.0F,
				stringField = "value",
				boolField = true
			};

			string json2 = JsonMapper.ToJson(p_test);

			Assert.AreEqual(Regex.Replace(json, @"\s+", ""), json2);
		}

		[Test]
		public void CanIgnoreObjectFieldsOnlyWhenDeserializing()
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

			ObjectIgnoreFieldsDeserializing o_test = JsonMapper.ToObject<ObjectIgnoreFieldsDeserializing>(json);

			Assert.AreEqual(null, o_test.dictField);
			Assert.AreEqual(null, o_test.listField);
			Assert.AreEqual(null, o_test.objectField);

			o_test = new ObjectIgnoreFieldsDeserializing {
				dictField = new Hashtable {
					{"key", "value"}
				},
				listField = new ArrayList(new int[]{1, 2, 3}),
				objectField = new ObjectClass {
					ignored = true
				}
			};

			string json2 = JsonMapper.ToJson(o_test);
			Assert.AreEqual(Regex.Replace(json, @"\s+", ""), json2);
		}

		[Test]
		public void CanIgnoreObjectPropertiesOnlyWhenDeserializing()
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

			ObjectIgnorePropertiesDeserializing o_test = JsonMapper.ToObject<ObjectIgnorePropertiesDeserializing>(json);

			Assert.AreEqual(null, o_test.dictField);
			Assert.AreEqual(null, o_test.listField);
			Assert.AreEqual(null, o_test.objectField);

			o_test = new ObjectIgnorePropertiesDeserializing {
				dictField = new Hashtable {
					{"key", "value"}
				},
				listField = new ArrayList(new int[]{1, 2, 3}),
				objectField = new ObjectClass {
					ignored = true
				}
			};

			string json2 = JsonMapper.ToJson(o_test);
			Assert.AreEqual(Regex.Replace(json, @"\s+", ""), json2);
		}
	}
}
