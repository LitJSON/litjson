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


    [TestFixture]
    public class JsonMapperTest
    {
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
    }
}
