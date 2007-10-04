#region Header
/**
 * JsonMapperTest.cs
 *   Tests for the JsonMapper class.
 *
 * The authors disclaim copyright to this source code. For more details, see
 * the COPYING file included with this distribution.
 **/
#endregion


using LitJson;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;


namespace LitJson.Test
{
    // Sample classes to test json->object and object->json conversions
    public enum Planets
    {
        Jupiter,
        Saturn,
        Uranus,
        Neptune,
        Pluto
    }

    [Flags]
    public enum Instruments
    {
        Bass   = 1,
        Guitar = 2,
        Drums  = 4,
        Harp   = 8
    }

    public class EnumsTest
    {
        public Planets FavouritePlanet;

        public Instruments Band;
    }

    public class PropertyReadOnly
    {
        private int x;


        public int X {
            get { return x; }
            set { x = value; }
        }

        public int Y {
            get { return x * 2; }
        }
    }

    public class PropertyWriteOnly
    {
        private int x;


        public int X {
            set { x = value; }
        }


        public int GetX ()
        {
            return x;
        }
    }

    public class UiImage
    {
        public string src;
        public string name;
        public int    hOffset;
        public int    vOffset;
        public string alignment;
    }

    public class UiSample
    {
        private UiWidget _widget;

        public UiWidget widget {
            get { return _widget; }
            set { _widget = value; }
        }

        public UiSample ()
        {
            _widget = new UiWidget ();
        }
    }

    public class UiText
    {
        public string data;
        public int    size;
        public string style;
        public string name;
        public int    hOffset;
        public int    vOffset;
        public string alignment;
        public string onMouseUp;
    }

    public class UiWidget
    {
        private UiImage  _image;
        private UiText   _text;
        private UiWindow _window;

        public bool debug;

        public UiWindow window {
            get { return _window; }
            set { _window = value; }
        }

        public UiImage image {
            get { return _image; }
            set { _image = value; }
        }

        public UiText text {
            get { return _text; }
            set { _text = value; }
        }

        public UiWidget ()
        {
            _image = new UiImage ();
            _text = new UiText ();
            _window = new UiWindow ();
        }
    }

    public class UiWindow
    {
        public string title;
        public string name;
        public int width;
        public int height;
    }

    public class ValueTypesTest
    {
        public byte     TestByte;
        public char     TestChar;
        public DateTime TestDateTime;
        public decimal  TestDecimal;
        public sbyte    TestSByte;
        public short    TestShort;
        public ushort   TestUShort;
        public uint     TestUInt;
        public ulong    TestULong;
    }


    [TestFixture]
    public class JsonMapperTest
    {
        [Test]
        public void CustomExporterTest ()
        {
            // Custom DateTime exporter that only uses the Year value
            ExporterFunc<DateTime> exporter =
                delegate (DateTime obj, JsonWriter writer) {
                    writer.Write (obj.Year);
                };

            JsonMapper.RegisterExporter<DateTime> (exporter);

            OrderedDictionary sample = new OrderedDictionary ();

            sample.Add ("date", new DateTime (1980, 12, 8));

            string json = JsonMapper.ToJson (sample);
            string expected = "{\"date\":1980}";

            JsonMapper.UnregisterExporters ();

            Assert.AreEqual (expected, json);
        }

        [Test]
        public void CustomImporterTest ()
        {
            // Custom DateTime importer that only uses the Year value
            // (assuming January 1st of that year)
            ImporterFunc<int, DateTime> importer =
                delegate (int obj) {
                    return new DateTime (obj, 1, 1);
                };

            JsonMapper.RegisterImporter<int, DateTime> (importer);

            string json = "{ \"TestDateTime\" : 1980 }";

            ValueTypesTest sample =
                JsonMapper.ToObject<ValueTypesTest> (json);

            JsonMapper.UnregisterImporters ();

            Assert.AreEqual (new DateTime (1980, 1, 1), sample.TestDateTime);
        }

        [Test]
        public void EmptyObjectsTest ()
        {
            JsonData empty_obj = JsonMapper.ToObject ("{}");
            Assert.IsTrue (empty_obj.IsObject, "A1");

            string empty_json = JsonMapper.ToJson (empty_obj);
            Assert.AreEqual ("{}", empty_json, "A2");

            JsonData empty_array = JsonMapper.ToObject ("[]");
            Assert.IsTrue (empty_array.IsArray, "B1");

            empty_json = JsonMapper.ToJson (empty_array);
            Assert.AreEqual ("[]", empty_json, "B2");
        }

        [Test]
        public void ExportArrayOfIntsTest ()
        {
            int[] numbers = new int[] { 1, 1, 2, 3, 5, 8, 13 };

            string json = JsonMapper.ToJson (numbers);

            Assert.AreEqual ("[1,1,2,3,5,8,13]", json);
        }

        [Test]
        public void ExportDictionaryTest ()
        {
            IDictionary hash = new OrderedDictionary ();

            hash.Add ("product", "ACME rocket skates");
            hash.Add ("quantity", 5);
            hash.Add ("price", 45.95);

            string expected = "{\"product\":\"ACME rocket skates\"," +
                "\"quantity\":5,\"price\":45.95}";

            string json = JsonMapper.ToJson (hash);

            Assert.AreEqual (expected, json);
        }

        [Test]
        public void ExportEnumsTest ()
        {
            EnumsTest e_test = new EnumsTest ();

            e_test.FavouritePlanet = Planets.Saturn;
            e_test.Band = Instruments.Bass | Instruments.Harp;

            string json = JsonMapper.ToJson (e_test);

            Assert.AreEqual ("{\"FavouritePlanet\":1,\"Band\":9}", json);
        }

        [Test]
        public void ExportObjectTest ()
        {
            UiSample sample = new UiSample ();

            sample.widget.window.title = "FooBar";
            sample.widget.window.name  = "foo_window";
            sample.widget.window.width = 400;
            sample.widget.window.height = 300;

            sample.widget.image.src = "logo.png";
            sample.widget.image.name = "Foo Logo";
            sample.widget.image.hOffset = 10;
            sample.widget.image.vOffset = 20;
            sample.widget.image.alignment = "right";

            sample.widget.text.data = "About Us";
            sample.widget.text.size = 24;
            sample.widget.text.style = "normal";
            sample.widget.text.name = "about";
            sample.widget.text.alignment = "center";

            string expected = "{\"widget\":{\"window\":" +
                "{\"title\":\"FooBar\",\"name\":\"foo_window\"," +
                "\"width\":400,\"height\":300},\"image\":{\"src\":" +
                "\"logo.png\",\"name\":\"Foo Logo\",\"hOffset\":10," +
                "\"vOffset\":20,\"alignment\":\"right\"},\"text\":{" +
                "\"data\":\"About Us\",\"size\":24,\"style\":\"normal\"," +
                "\"name\":\"about\",\"hOffset\":0,\"vOffset\":0," +
                "\"alignment\":\"center\",\"onMouseUp\":null}," +
                "\"debug\":false}}";

            string json = JsonMapper.ToJson (sample);

            Assert.AreEqual (expected, json);
        }

        [Test]
        public void ExportPrettyPrint ()
        {
            OrderedDictionary sample = new OrderedDictionary ();

            sample["rolling"] = "stones";
            sample["flaming"] = "pie";
            sample["nine"] = 9;

            string expected = @"
{
    ""rolling"" : ""stones"",
    ""flaming"" : ""pie"",
    ""nine""    : 9
}";

            JsonWriter writer = new JsonWriter ();
            writer.PrettyPrint = true;

            JsonMapper.ToJson (sample, writer);

            Assert.AreEqual (expected, writer.ToString (), "A1");

            writer.Reset ();
            writer.IndentValue = 8;

            expected = @"
{
        ""rolling"" : ""stones"",
        ""flaming"" : ""pie"",
        ""nine""    : 9
}";
            JsonMapper.ToJson (sample, writer);

            Assert.AreEqual (expected, writer.ToString (), "A2");
        }

        [Test]
        public void ExportValueTypesTest ()
        {
            ValueTypesTest test = new ValueTypesTest ();

            test.TestByte     = 200;
            test.TestChar     = 'P';
            test.TestDateTime = new DateTime (2012, 12, 22);
            test.TestDecimal  = 10.333m;
            test.TestSByte    = -5;
            test.TestShort    = 1024;
            test.TestUShort   = 30000;
            test.TestUInt     = 90000000;
            test.TestULong    = 1l;

            string json = JsonMapper.ToJson (test);
            string expected =
                "{\"TestByte\":200,\"TestChar\":\"P\",\"TestDateTime\":" +
                "\"12/22/2012 00:00:00\",\"TestDecimal\":10.333," +
                "\"TestSByte\":-5,\"TestShort\":1024,\"TestUShort\":30000" +
                ",\"TestUInt\":90000000,\"TestULong\":1}";

            Assert.AreEqual (expected, json);
        }

        [Test]
        public void ImportArrayOfStringsTest ()
        {
            string json = @"[
                ""Adam"",
                ""Danny"",
                ""James"",
                ""Justin""
            ]";

            string[] names = JsonMapper.ToObject<string[]> (json);

            Assert.IsTrue (names.Length == 4, "A1");
            Assert.AreEqual (names[1], "Danny", "A2");
        }

        [Test]
        public void ImportEnumsTest ()
        {
            string json = @"
                {
                    ""FavouritePlanet"" : 4,
                    ""Band"" : 6
                }";

            EnumsTest e_test = JsonMapper.ToObject<EnumsTest> (json);

            Assert.AreEqual (Planets.Pluto, e_test.FavouritePlanet, "A1");
            Assert.AreEqual (Instruments.Guitar
                             | Instruments.Drums, e_test.Band, "A2");
        }

        [Test]
        public void ImportExtendedGrammarTest ()
        {
            string json = @"
                {
                    // The domain name
                    ""domain"" : ""example.com"",

                    /******************
                     * The IP address *
                     ******************/
                    'ip_address' : '127.0.0.1'
                }
                ";

            JsonData data = JsonMapper.ToObject (json);

            Assert.AreEqual ("example.com", (string) data["domain"], "A1");
            Assert.AreEqual ("127.0.0.1", (string) data["ip_address"], "A2");
        }

        [Test]
        public void ImportFromFileTest ()
        {
            JsonData data;

            Assembly asmb = typeof (JsonMapperTest).Assembly;

            StreamReader stream = new StreamReader (
                asmb.GetManifestResourceStream ("json-example.txt"));

            using (stream) {
                data = JsonMapper.ToObject (stream);
            }

            Assert.AreEqual (
                "cofaxCDS",
                (string) data["web-app"]["servlet"][0]["servlet-name"],
                "A1");
            Assert.AreEqual (
                false,
                (bool) data["web-app"]["servlet"][0]["init-param"]["useJSP"],
                "A2");
            Assert.AreEqual (
                "cofax.tld",
                (string) data["web-app"]["taglib"]["taglib-uri"],
                "A1");
        }

        [Test]
        public void ImportJsonDataArrayTest ()
        {
            string json = " [ 1, 10, 100, 1000 ] ";

            JsonData data = JsonMapper.ToObject (json);

            Assert.AreEqual (4, data.Count, "A1");
            Assert.AreEqual (1000, (int) data[3], "A2");
        }

        [Test]
        public void ImportManyJsonTextPiecesTest ()
        {
            string json_arrays = @"
                [ true, true, false, false ]
                [ 10, 0, -10 ]
                [ ""war is over"", ""if you want it"" ]
                ";

            JsonReader reader;
            JsonData   arrays;

            reader = new JsonReader (json_arrays);
            arrays = JsonMapper.ToObject (reader);

            Assert.IsFalse (reader.EndOfInput, "A1");

            Assert.IsTrue (arrays.IsArray, "A2");
            Assert.AreEqual (4, arrays.Count, "A3");
            Assert.AreEqual (true, (bool) arrays[0], "A4");

            arrays = JsonMapper.ToObject (reader);

            Assert.IsFalse (reader.EndOfInput, "A5");

            Assert.IsTrue (arrays.IsArray, "A6");
            Assert.AreEqual (3, arrays.Count, "A7");
            Assert.AreEqual (10, (int) arrays[0], "A8");

            arrays = JsonMapper.ToObject (reader);

            Assert.IsTrue (arrays.IsArray, "A9");
            Assert.AreEqual (2, arrays.Count, "A10");
            Assert.AreEqual ("war is over", (string) arrays[0], "A11");

            reader.Close ();

            string json_objects = @"
                {
                  ""title""  : ""First"",
                  ""name""   : ""First Window"",
                  ""width""  : 640,
                  ""height"" : 480
                }

                {
                  ""title""  : ""Second"",
                  ""name""   : ""Second Window"",
                  ""width""  : 800,
                  ""height"" : 600
                }
                ";

            reader = new JsonReader (json_objects);
            UiWindow window;

            window = JsonMapper.ToObject<UiWindow> (reader);

            Assert.IsFalse (reader.EndOfInput, "A12");

            Assert.AreEqual ("First", window.title, "A13");
            Assert.AreEqual (640, window.width, "A14");

            window = JsonMapper.ToObject<UiWindow> (reader);

            Assert.AreEqual ("Second", window.title, "A15");
            Assert.AreEqual (800, window.width, "A16");

            reader.Close ();

            // Read them in a loop to make sure we get the correct number of
            // iterations
            reader = new JsonReader (json_objects);

            int i = 0;

            while (! reader.EndOfInput) {
                window = JsonMapper.ToObject<UiWindow> (reader);
                i++;
            }

            Assert.AreEqual (2, i, "A17");
        }

        [Test]
        public void ImportNumbersTest ()
        {
            double[]  d_array;
            float[]   f_array;
            decimal[] m_array;

            string json = " [ 0, 5, 10 ] ";

            d_array = JsonMapper.ToObject<double[]> (json);

            Assert.AreEqual (3, d_array.Length, "A1");
            Assert.AreEqual (10.0, d_array[2], "A2");

            f_array = JsonMapper.ToObject<float[]> (json);

            Assert.AreEqual (3, f_array.Length, "A3");
            Assert.AreEqual (10.0, f_array[2], "A4");

            m_array = JsonMapper.ToObject<decimal[]> (json);

            Assert.AreEqual (3, m_array.Length, "A5");
            Assert.AreEqual (10m, m_array[2], "A6");
        }

        [Test]
        public void ImportObjectTest ()
        {
            string json = @"
{
  ""widget"": {
    ""debug"": true,

    ""window"": {
      ""title"": ""Sample Widget"",
      ""name"": ""main_window"",
      ""width"": 500,
      ""height"": 500
    },

    ""image"": {
      ""src"": ""Images/Sun.png"",
      ""name"": ""sun1"",
      ""hOffset"": 250,
      ""vOffset"": 250,
      ""alignment"": ""center""
    },

    ""text"": {
      ""data"": ""Click Here"",
      ""size"": 36,
      ""style"": ""bold"",
      ""name"": ""text1"",
      ""hOffset"": 250,
      ""vOffset"": 100,
      ""alignment"": ""center"",
      ""onMouseUp"": ""sun1.opacity = (sun1.opacity / 100) * 90;""
    }
  }
}";

            UiSample sample = JsonMapper.ToObject<UiSample> (json);

            Assert.IsNotNull (sample, "A1");
            Assert.AreEqual ("Sample Widget", sample.widget.window.title,
                             "A2");
            Assert.AreEqual (500, sample.widget.window.width, "A3");
            Assert.AreEqual ("sun1", sample.widget.image.name, "A4");
            Assert.AreEqual ("Click Here", sample.widget.text.data, "A5");
        }

        [Test]
        [ExpectedException (typeof (JsonException))]
        public void ImportStrictCommentsTest ()
        {
            string json = @"
                [
                    /* This is a comment */
                    1,
                    2,
                    3
                ]";

            JsonReader reader = new JsonReader (json);
            reader.AllowComments = false;

            JsonData data = JsonMapper.ToObject (reader);

            if (data.Count != 3)
                data = JsonMapper.ToObject (reader);
        }

        [Test]
        [ExpectedException (typeof (JsonException))]
        public void ImportStrictStringsTest ()
        {
            string json = "[ 'Look! Single quotes' ]";

            JsonReader reader = new JsonReader (json);
            reader.AllowSingleQuotedStrings = false;

            JsonData data = JsonMapper.ToObject (reader);

            if (data[0] == null)
                data = JsonMapper.ToObject (reader);
        }

        [Test]
        public void ImportValueTypesTest ()
        {
            string json = @"
{
  ""TestByte"":     200,
  ""TestChar"":     'P',
  ""TestDateTime"": ""12/22/2012 00:00:00"",
  ""TestDecimal"":  10.333,
  ""TestSByte"":    -5,
  ""TestShort"":    1024,
  ""TestUShort"":   30000,
  ""TestUInt"":     90000000,
  ""TestULong"":    1
}";

            ValueTypesTest test = JsonMapper.ToObject<ValueTypesTest> (json);

            Assert.AreEqual (200, test.TestByte, "A1");
            Assert.AreEqual ('P', test.TestChar, "A2");
            Assert.AreEqual (new DateTime (2012, 12, 22),
                             test.TestDateTime, "A3");
            Assert.AreEqual (10.333m, test.TestDecimal, "A4");
            Assert.AreEqual (-5, test.TestSByte, "A5");
            Assert.AreEqual (1024, test.TestShort, "A6");
            Assert.AreEqual (30000, test.TestUShort, "A7");
            Assert.AreEqual (90000000, test.TestUInt, "A8");
            Assert.AreEqual (1l, test.TestULong, "A9");
        }

        [Test]
        public void NullConversionsTest ()
        {
            object[] MyObjects = new object[] {"Hi!", 123, true, null};
            string json = JsonMapper.ToJson (MyObjects);

            Assert.AreEqual ("[\"Hi!\",123,true,null]", json, "A1");

            JsonData data = JsonMapper.ToObject (json);

            Assert.AreEqual ("Hi!", (string) data[0], "A2");
            Assert.AreEqual (123, (int) data[1], "A3");

            Assert.IsTrue ((bool) data[2], "A4");
            Assert.IsNull (data[3], "A5");
        }

        [Test]
        public void PropertiesReadOnlyTest ()
        {
            PropertyReadOnly p_obj = new PropertyReadOnly ();

            p_obj.X = 10;

            string json = JsonMapper.ToJson (p_obj);

            Assert.AreEqual ("{\"X\":10,\"Y\":20}", json, "A1");

            PropertyReadOnly p_obj2 =
                JsonMapper.ToObject<PropertyReadOnly> (json);

            Assert.AreEqual (10, p_obj2.X, "A2");
            Assert.AreEqual (20, p_obj2.Y, "A3");
        }

        [Test]
        public void PropertiesWriteOnlyTest ()
        {
            string json = " { \"X\" : 3 } ";

            PropertyWriteOnly p_obj =
                JsonMapper.ToObject<PropertyWriteOnly> (json);

            Assert.AreEqual (3, p_obj.GetX (), "A1");

            json = JsonMapper.ToJson (p_obj);

            Assert.AreEqual ("{}", json, "A2");
        }
    }
}
